<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Desktop.aspx.cs" Inherits="Boot_Park.View.Private.Desktop" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script src="../../Content/js/Desktop.js"></script>
    <title></title>
</head>
<body>
   <ext:ResourceManager ID="resource1" runat="server" />
    <form id="form1" runat="server">
        <ext:Desktop runat="server" ID="DESCRITORIO" >
            <StartMenu Title="BOOT - PARK " Icon="Car" Height="400">
                <Items>
                    <ext:MenuItem ID="MENUPARAMETRIZACION" runat="server" Text="PARAMETRIZACIÓN" Icon="Folder" HideOnClick="false">
                        <Menu>
                            <ext:Menu ID="METIQUETA" runat="server">
                                <Items>
                                    <ext:MenuItem Text="Etiqueta" Icon="Cart">
                                        <Listeners>
                                            <Click Handler="crearVentanaWindow(#{DESCRITORIO},'BootPark/Parametrizacion/Etiqueta.aspx','Etiqueta',800,500);" />
                                        </Listeners>
                                    </ext:MenuItem>
                                     <ext:MenuItem Text="Particular" Icon="User">
                                        <Listeners>
                                            <Click Handler="crearVentanaWindow(#{DESCRITORIO},'BootPark/Parametrizacion/Particular.aspx','Particular',800,500);" />
                                        </Listeners>
                                    </ext:MenuItem>
                                     <ext:MenuItem Text="Vehiculo" Icon="Car">
                                        <Listeners>
                                            <Click Handler="crearVentanaWindow(#{DESCRITORIO},'BootPark/Parametrizacion/Vehiculo.aspx','Vehiculo',800,500);" />
                                        </Listeners>
                                    </ext:MenuItem>
                                </Items>
                            </ext:Menu>
                        </Menu>
                    </ext:MenuItem>
                      <ext:MenuItem ID="MENUASIGNACION" runat="server" Text="ASIGNACIÓN" Icon="Folder" HideOnClick="false">
                        <Menu>
                            <ext:Menu ID="MIDENTIDAD" runat="server">
                                <Items>
                                    <ext:MenuItem Text="Identidad" Icon="User" >
                                        <Listeners>
                                            <Click Handler="crearVentanaWindow(#{DESCRITORIO},'BootPark/Asignacion/Identidad.aspx','Identidad',1000,500);" />
                                        </Listeners>
                                    </ext:MenuItem>
                                </Items>
                            </ext:Menu>
                        </Menu>
                    </ext:MenuItem>
                     <ext:MenuItem ID="MENUAUTORIZACION" runat="server" Text="AUTORIZACIÓN" Icon="Folder" HideOnClick="false">
                        <Menu>
                            <ext:Menu ID="MAUTORIZACION" runat="server">
                                <Items>
                                    <ext:MenuItem Text="AUTORIZACIÓN" Icon="GroupKey" >
                                        <Listeners>
                                            <Click Handler="crearVentanaWindow(#{DESCRITORIO},'BootPark/Autorizacion/Autorizacion.aspx','Autorización',800,500);" />
                                        </Listeners>
                                    </ext:MenuItem>
                                </Items>
                            </ext:Menu>
                        </Menu>
                    </ext:MenuItem>
                     <ext:MenuItem ID="MENUCIRCULACION" runat="server" Text="CIRCULACIÓN" Icon="Folder" HideOnClick="false">
                        <Menu>
                            <ext:Menu ID="MCIRCULACION" runat="server">
                                <Items>
                                    <ext:MenuItem Text="CIRCULACIÓN" Icon="CarRed" >
                                        <Listeners>
                                            <Click Handler="crearVentanaWindow(#{DESCRITORIO},'BootPark/Circulacion/Circulacion.aspx','Circulacion',800,500);" />
                                        </Listeners>
                                    </ext:MenuItem>
                                </Items>
                            </ext:Menu>
                        </Menu>
                    </ext:MenuItem>
                    <ext:MenuSeparator />
                </Items>
            </StartMenu>
           
    </ext:Desktop>
    </form>
</body>
</html>
