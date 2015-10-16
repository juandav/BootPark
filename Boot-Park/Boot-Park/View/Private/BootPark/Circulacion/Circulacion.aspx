<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Circulacion.aspx.cs" Inherits="Boot_Park.View.Private.BootPark.Circulacion.Circulacion" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>

<script src="../../../../Content/js/BiometricDevice.js"></script>
<link href="../../../../Content/css/desktop.css" rel="stylesheet" />
<title>Validación Biometrico-RFID</title>
<script type="text/javascript">

    try {
        var obj = new ActiveXObject("BootParkBiom.PluginBiometrico");
        var TiempoApertura = 1; // Lapso de tiempo para iniciar abrir la puerta despues de validar todo.
        obj.ConectarConTerminal("192.168.1.201", "4370", 'Biometrico');
    }
    catch (e) {

    }

    var Inicio = function () {
       
        if (HiddenTag.getValue() != "") {
            LVEHICULO.setText('' + MensajePlaca.getValue());
            if (HiddenTipo.getValue() == "") {
                //parametro.ValidarUsuario(obj.Usuario, obj.TipoValidacion);
                _DetectarUsuarioBiometrico();
             
            } else {
                LUSUARIO.setText('' + MensajeUsuario.getValue());
                parametro.RegistrarCirculacion();
                HiddenTag.setValue("");
                HiddenTipo.setValue("");
                HiddenUsuario.setValue("");
                obj.Usuario = null;
                obj.TipoValidacion = null;

            }

        }

    }

    var _DetectarUsuarioBiometrico = function (data, callback) // 1. Consulta el Usuario en Biometrico
    {
        if (typeof (obj.Usuario) != undefined) {
            parametro.ValidarUsuario(obj.Usuario, obj.TipoValidacion, {
                success: function (result) {
                    parametro.CargarUsuario();
                }, failure: function (errorMsg) {
                    Ext.net.Notification.show({
                        html: ' Intente de Nuevo.',
                        title: 'Notificación'
                    });
                }
            });
            HiddenUsuario.setValue(obj.Usuario);
            HiddenTipo.setValue(obj.TipoValidacion);
            IMUSUARIO.Hidden = false;

        } else {
            callback("El usuario no ha puesto su huella o tarjeta en el lector", "", "");
        }
    }


</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CIRCULACION</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form runat="server">
        <ext:ResourceManager ID="resourceManager1" runat="server">
            <Listeners>
                <DocumentReady Handler="var msg = function (text) { 
                    #{LogArea}.setValue(
                        String.format('{0}\n{1} : {2}', 
                        #{LogArea}.getValue(), 
                        text, 
                        new Date().dateFormat('H:i:s'))); 
                    }" />
            </Listeners>
        </ext:ResourceManager>
        <ext:Hidden ID="HiddenTag" runat="server" />
        <ext:Hidden ID="HiddenUsuario" runat="server" />
        <ext:Hidden ID="HiddenTipo" runat="server" />
        <ext:Hidden ID="MensajePlaca" runat="server" />
        <ext:Hidden ID="MensajeUsuario" runat="server" />
        <ext:Viewport runat="server">
            <Items>
                <ext:BorderLayout runat="server">

                    <Center>
                        <ext:Panel runat="server" Title="CIRCULACIÓN" Icon="Car" Border="false">
                            <TopBar>
                                <ext:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <ext:Button
                                            ID="btnStartAll"
                                            runat="server"
                                            Text="Iniciar Validación"
                                            Icon="ControlPlayBlue"
                                            Disabled="true">
                                            <Listeners>
                                                <Click Handler="this.disable();#{TaskManager1}.startAll();#{btnStopAll}.enable()" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button
                                            ID="btnStopAll"
                                            runat="server"
                                            Text="Detener Validación"
                                            Icon="ControlStopBlue">
                                            <Listeners>
                                                <Click Handler="this.disable();#{TaskManager1}.stopAll();#{btnStartAll}.enable();" />
                                            </Listeners>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Items>

                                <ext:Panel
                                    ID="ServerTimeContainer"
                                    runat="server"
                                    Title="Validación"
                                    BodyStyle="text-align:center;"
                                    Padding="20"
                                    Layout="">
                                    <Items>
                                        <ext:Container ID="CIMAGENES" runat="server" Layout="HBoxLayout">
                                            <Items>
                                                <ext:Image ID="IMVEHICULO" runat="server" ImageUrl="../../../../Content/images/vehiculo.png" Width="250" Hidden="false" Height="250" />
                                                <ext:Image ID="IMACUMULADO" runat="server" ImageUrl="../../../../Content/images/suma.png" Width="250" Height="250" Hidden="false" />
                                                <ext:Image ID="IMUSUARIO" runat="server" ImageUrl="../../../../Content/images/user.png" Width="250" Height="250" Hidden="false" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container1" runat="server" Layout="HBoxLayout">
                                            <Items>
                                                <ext:Label ID="LVEHICULO" runat="server" Width="250" Text="TAG SIN DETECTAR!.." />
                                                <ext:Label ID="LACUMULADO" runat="server" AnchorHorizontal="300" Text=".      dfd             ." />
                                                <ext:Label ID="LUSUARIO" runat="server" Width="250" Text="INGRESE LA TARJETA O HUELLA" />
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Panel>

                            </Items>
                        </ext:Panel>
                    </Center>

                    <South>
                        <ext:Panel runat="server" Height="200" Border="false" Layout="Fit">
                            <Items>


                                <ext:TextArea ID="LogArea" runat="server" />
                            </Items>
                        </ext:Panel>
                    </South>
                </ext:BorderLayout>
            </Items>
        </ext:Viewport>

        <ext:TaskManager ID="TaskManager1" runat="server">
            <Tasks>
                <ext:Task
                    TaskID="servertime"
                    Interval="4000"
                    OnStart="
                        msg('Start Server')"
                    OnStop="
                        msg('Stop Server')">
                    <Listeners>
                        <Update Handler="Inicio(); parametro.detectarTag();" />
                    </Listeners>
                </ext:Task>
            </Tasks>
        </ext:TaskManager>
    </form>
</body>
</html>
