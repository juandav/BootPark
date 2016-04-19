using System;
using System.ServiceProcess;
using System.Windows.Forms;
using System.Threading;
using zkemkeeper;
using SuperWebSocket;
using SuperSocket.SocketBase;
using System.Web.Script.Serialization;


namespace BPark
{
    public partial class BPark : ServiceBase
    {
        #region Constantes
        private const string _IP_BIOMETRICO = "192.168.1.201";
        //  private const string _IP_SOCKET = "127.0.0.1";
        private const string _PORT_SOCKET = "2012";
        private CZKEM _reader;
        #endregion

        private WebSocketServer appServer;

        public BPark()
        {
            InitializeComponent();
        }

        #region Metodos Lector Biometrico
        /// <summary>
        ///     Conecta al dispositivo Biometrico
        /// </summary>
        /// <param name="ip">ip del lector</param>
        /// <returns>true or false</returns>
        private void ConnectBiometric(string ip, CZKEM reader)
        {
            bool connection = reader.Connect_Net(ip, Convert.ToInt32(4370));

            if (connection)
            {
                bool events = reader.RegEvent(1, 65535);

                if (events)
                {
                    reader.OnHIDNum += new _IZKEMEvents_OnHIDNumEventHandler(GetCard);
                    reader.OnAttTransactionEx += new _IZKEMEvents_OnAttTransactionExEventHandler(GetType);
                }
            }
            else {
                MessageBox.Show("Error de conexión");
            }
            _reader = reader;
            Application.Run();
        }

        public void CapturarHuella(String usuarioID)
        {
            _reader.CancelOperation();
            if (_reader.StartEnrollEx(usuarioID, 2, 2))
            {
                _reader.StartIdentify();
                _reader.RefreshData(1);
                _reader.EnableDevice(1, true);
            }
            else
            {
                _reader.RefreshData(1);
            }
        }

        public string RecuperarHuella(string usuarioID)
        {
            int iFingerIndex = 2;
            int iFlag = 2;
            int iTmpLength;
            string ibyTmpData;
            _reader.EnableDevice(1, false);
            _reader.ReadAllTemplate(1);
            if (_reader.GetUserTmpExStr(1, usuarioID, iFingerIndex, out iFlag, out ibyTmpData, out iTmpLength))
            {
                HuellaDactilar h = new HuellaDactilar();
                _reader.RefreshData(1);
                _reader.EnableDevice(1, true);
                _reader.PlayVoiceByIndex(9);
                h.Identidad = usuarioID;
                h.FingerIndex = iFingerIndex;
                h.byTmpData = ibyTmpData;
                h.TmpLength = iTmpLength;


                string json = @"data =
                    {
                        'type': '" + "fingerout" + @"',
                        'payload': [
                            {
                                'user'  : '" + h.Identidad + @"',
                                'index' : '" + Convert.ToString(h.FingerIndex) + @"',
                                'data'  : '" + Convert.ToString(h.byTmpData) +  @"',
                                'length': '" + Convert.ToString(h.TmpLength) + @"'
                            }
                        ] 
                    }
                ";
                return json;
            }
            else
            {
                _reader.RefreshData(1);
                _reader.EnableDevice(1, true);
                return "{}";
            }
        }
      
        public void RegistrarCarnet(string Carnet, string Nombre, string usuarioID)
        {
            _reader.EnableDevice(1, false);
            _reader.SetStrCardNumber(Carnet);
            if (_reader.SSR_SetUserInfo(1, usuarioID, Nombre, usuarioID, 0, true)) //SSR_SetUserInfo(dispositivo,IdInterno,NombreEtiqueta,Contraseña,privilegio,estado);
            {
                _reader.RefreshData(1);
                _reader.EnableDevice(1, true);
                _reader.PlayVoiceByIndex(9);
               
            }
        }

        #endregion

        #region Eventos Lector Biometrico

        private void GetCard(int card)
        {
            foreach (WebSocketSession session in appServer.GetAllSessions())
            {
                session.Send(Convert.ToString("card = " + card));
            }
        }
 
        private void GetType(string a, int b, int c, int d, int e, int f, int g, int h, int i, int j, int k)
        {
            string type = d == 1 ? "HUELLA" : "TARJETA"; // d=tipo, a=usuario

            foreach (WebSocketSession session in appServer.GetAllSessions())
            {
                session.Send(Convert.ToString("type = " + type + ", user = " + a));
            }
        }
        
        #endregion

        #region Metodos del Servicio de Windows
        public void OnDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {

            Thread ComThread = new Thread(() =>
            {
                ConnectBiometric(_IP_BIOMETRICO, new CZKEM());
            });

            ComThread.SetApartmentState(ApartmentState.STA);
            ComThread.Start();

            RunServer();

        }

        private void RunServer()
        {
            appServer = new WebSocketServer();
            appServer.Setup(Convert.ToInt32(_PORT_SOCKET));
            appServer.NewMessageReceived += new SessionHandler<WebSocketSession, string>(appServer_Register);
            appServer.Start();
        }

        private void appServer_Register(WebSocketSession session, string message)
        {
          
            string json = message;
            
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] { new DynamicJsonConverter() });

            dynamic obj = serializer.Deserialize(json, typeof(object));

            string type = obj.type;
            string user = obj.payload[0].user;
            
            switch (type)
            {
                case "cardin":
                    string card = obj.payload[0].card;
                    string name = obj.payload[0].name;
                    this.RegistrarCarnet(card,name,user);
                    break;
                case "fingerin":
                    this.CapturarHuella(user);
                    break;
                case "fingerout":
                    string data = RecuperarHuella(user);
                    session.Send(data);
                    break;
                default:
                    break;
            }
            
        }

        protected override void OnStop() {}
        #endregion

    }
    #region ENTIDADES
    public class HuellaDactilar
    {
        public string Identidad { get; set; }
        public int FingerIndex { get; set; }
        public int Flag { get; set; }
        public string byTmpData { get; set; }
        public int TmpLength { get; set; }
    }
    #endregion
}