using System;
using System.ServiceProcess;
using System.Windows.Forms;
using System.Threading;
using zkemkeeper;
using SuperWebSocket;

namespace BPark
{
    public partial class BPark : ServiceBase
    {
        #region Constantes
        private const string _IP_BIOMETRICO = "192.168.1.201";
        //  private const string _IP_SOCKET = "127.0.0.1";
        private const string _PORT_SOCKET = "2012";
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

            Application.Run();
        }
        #endregion

        #region Eventos Lector Biometrico
        
        private void GetCard(int card)
        {
            foreach (WebSocketSession session in appServer.GetAllSessions())
            {
                session.Send(Convert.ToString(card));
            }
        }

       

        private void GetType(string a, int b, int c, int d, int e, int f, int g, int h, int i, int j, int k)
        {
            string type = d == 1 ? "HUELLA" : "TARJETA"; // d=tipo, a=usuario

            foreach (WebSocketSession session in appServer.GetAllSessions())
            {
                session.Send(Convert.ToString("Tipo: " + type + ", User: " + a));
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
            appServer.Start();
        }

        protected override void OnStop() {}
        #endregion

    }
}