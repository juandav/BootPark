<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Validacion.aspx.cs" Inherits="Boot_Park.View.Public.BootPark.Circulacion.Validacion" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../../../../Content/js/BiometricDevice.js"></script>
    <title>Validación Biometrico-RFID</title>
    <script type="text/javascript">

        try {
            var obj = new ActiveXObject("BootParkBiom.PluginBiometrico");
            obj.ConectarConTerminal("192.168.1.201", "4370", 'Biometrico');
        }
        catch (e) {
            callback("Incompatibilidad con ActiveX", "", "");
        }

        /*
            For Example:
            //user: El identificador del usuario en el biometrico.
            //type: Define si es Huella o Tarjeta
            DetectarUsuarioBiometrico(function(user, type){
                console.log(user + type);
            });
        */

        //var data = {ip: "192.168.1.201", port: "4370" }
        var _DetectarUsuarioBiometrico = function (data, callback) // 1. Consulta el Usuario en Biometrico
        {
            if (typeof (obj.Usuario) != undefined) {
                callback("", obj.Usuario(), obj.TipoValidacion());
            } else {
                callback("El usuario no ha puesto su huella o tarjeta en el lector", "", "");
            }
        }

        //var data = {ip: "192.168.1.201", port: "4370" }
        var _ValidarUsuario = function (data, callback) {
            
            _DetectarUsuarioBiometrico(data, function (err, user, type) {
                if (err != "")
                    callback(err, false);

               
                parametro.ValidarUsuario("" + user, "" + type, {
                    success: function (result) {
                        callback("", result);
                    }
                });
            });
        }

        //var DetectarTag = function () { }
        //var ValidarTag = function () { }
        //var RegistrarCirculacion = function () { }

        var main = function (){
           
            var data = {
                ip: "192.168.1.201",
                port: "4370"
            }

            _ValidarUsuario(data, function (err, data) {
               
                if (data) {
                    var response = parametro.ValidarTag({
                        success: function (result) {
                            if (result) {
                                parametro.CargarUsuario();
                                parametro.RegistrarCirculacion();
                                var apertura = parametro.SeñalDeApertura({
                                    success: function (result) {
                                        alert('PUERTA ABRIENDOSE');
                                        //PASA SOBRE EL SENSOR AL NO DETECTAR EL VEHICULO SE PROCEDE A CERRAR ES DECIR SI DEJA DE DETECTAR EL SENSOR OBSTRUCCION CIERRA PUERTA
                                        var apertura = parametro.SeñalDeCierre({
                                            success: function (result) {
                                                alert('PUERTA CERRANDOSE');
                                            }
                                        });
                                    }
                                });
                            }
                        }
                    });
                } else {
                    Ext.Msg.alert('Error', err||"El usuario no existe en CHAIRA");
                }
            });
        }
        
       // main();

        //"use strict";
        // 1. Se crea la clase circulación
        //class Circulacion{
        //    constructor() {

        //        console.log("Metodo que iniciar la conexion de los dispositivos");

        //    }

        //    //1. Detectar la Huella en el dispositivo biometrico
        //    detectarHuella() {
        //        alert('entro');
        //    }

        //    //2. Validar la Huella en la base de datos
        //    validarHuella() {

        //    }//y devuelve el pege, junto a la lista de vehiculos

        //    // 3. Detectar el tag del vehiculo
        //    detectarTag() {

        //    } //Devuelve el tag leido del RFID

        //    // 4. Validar el tag del vehiculo con los traidos en el arreglo de vehiculos de la BD.
        //    validarTag() {

        //    }

        //    // 5. Verificar si el usuario tiene autorización con el vehiculo
        //    verificarAutorizacion() {

        //    }

        //    // 6. Verificar si la terminal de lectura biometrica es de entrada o salida.
        //    verificarTerminal() {

        //    }

        //    // 7. Registrar la circulacion del vehiculo de entrada o de salida segun sea el caso.
        //    registrarCirculacion() {

        //    }
        //}

        //var deteccion = new Circulacion();

    </script>
</head>
<body>
    <ext:ResourceManager runat="server" />
    <form id="FCIRCULACION" runat="server">
        <ext:Viewport ID="VPCIRCULACION" runat="server" Layout="border">
            <Items>
                <ext:Panel runat="server" Layout="Fit" Region="Center">
                    <Items>
                        <%-- 1. Primera vista de presentación para la validación --%>
                        <%--queda esperando que el usuario ingrese la huella.--%>
                        <ext:Panel ID="PPRESENTACION" runat="server" Collapsible="true" Layout="Fit" Title="Iniciando Validación" Icon="Application">
                            <BottomBar>
                                <ext:Toolbar runat="server">
                                    <Items>
                                        <ext:Button runat="server" Text="Validar">
                                            <Listeners>
                                                <Click Fn="main"/>
                                            </Listeners>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </BottomBar>
                            <Items>
                                <ext:GridPanel runat="server">
                                    <Store>
                                        <ext:Store ID="USUARIO" runat="server">
                                            <Reader>
                                                <ext:JsonReader>
                                                    <Fields>
                                                        <ext:RecordField Name="IDENTIFICACION" />
                                                        <ext:RecordField Name="NOMBRE" />
                                                        <ext:RecordField Name="APELLIDO" />
                                                        <ext:RecordField Name="TIPOUSUARIO" />
                                                    </Fields>
                                                </ext:JsonReader>
                                            </Reader>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel>
                                        <Columns>
                                            <ext:RowNumbererColumn />
                                            <ext:Column ColumnID="CIDENTIFICACION" DataIndex="IDENTIFICACION" Header="Identificación" />
                                            <ext:Column ColumnID="CNOMBRE" DataIndex="NOMBRE" Header="Nombre" />
                                            <ext:Column ColumnID="CAPELLIDO" DataIndex="APELLIDO" Header="Apellido" />
                                            <ext:Column ColumnID="CTIPOUSUARIO" DataIndex="TIPOUSUARIO" Header="Tipo de Usuario" />
                                        </Columns>
                                    </ColumnModel>
                                </ext:GridPanel>
                            </Items>
                            
                        </ext:Panel>
                        <%-- 2. Segunda vista para la información del que ingresa o sale de la institución  --%>
                        <ext:Panel ID="PINFORMACION" runat="server" Collapsible="true" Collapsed="true" Layout="Fit" Title="Info. Usuario" Padding="5" Icon="User">
                            <Items>
                            </Items>
                        </ext:Panel>
                        <%-- 3. Muestra al usuario las gracias por confiar en la institución, esta pantalla es netamente innecesaria, sirve solo para adornar--%>
                        <ext:Panel ID="PDESPEDIDA" runat="server" Collapsible="true" Collapsed="true" Layout="Fit" Title="BootPark" Icon="ApplicationDelete">
                            <Items>
                                <ext:Image runat="server" ImageUrl="../../../../Content/Images/desktop.jpg" />
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
