<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Validacion.aspx.cs" Inherits="Boot_Park.View.Public.BootPark.Circulacion.Validacion" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Validación Biometrico-RFID</title>
    <script type="text/javascript">

        // Contiene los llamados a los metodos de de conexion de los dispositivos e inicia como tal la app
        var init = function () {

        };

        //Función que permite la validación
        var verificarAutorizacion = function (algo, callback) {
            callback('mi error', 'la data');
        };

        //1. Non-Blocking I/O de la validación
        // verificarAutorizacion(algo, function(err, data){

        //});

        // Inicia la aplicación
        init();
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
                            <Items>
                                <ext:Image runat="server" ImageUrl="../../../Content/Images/desktop.jpg">
                                    <LoadMask />
                                </ext:Image>
                            </Items>
                        </ext:Panel>
                        <%-- 2. Segunda vista para la información del que ingresa o sale de la institución  --%>
                        <ext:Panel ID="PINFORMACION" runat="server" Collapsible="true" Layout="Fit" Title="Info. Usuario" Padding="5" Icon="User">
                            <Items>
                            </Items>
                        </ext:Panel>
                        <%-- 3. Muestra al usuario las gracias por confiar en la institución, esta pantalla es netamente innecesaria, sirve solo para adornar--%>
                        <ext:Panel ID="PDESPEDIDA" runat="server" Collapsible="true" Layout="Fit" Title="BootPark" Icon="ApplicationDelete">
                            <Items>
                                <ext:Image runat="server" ImageUrl="../../../Content/Images/desktop.jpg" />
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
