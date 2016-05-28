﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Desktop.aspx.cs" Inherits="Boot_Park.View.Private.Desktop" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <script src="../../Content/js/Desktop.js"></script>
     <LINK href="../../Content/css/desktop.css" rel="stylesheet" type="text/css">
    <title>Boot-Park</title>
    <script>
        'use strict'

        let accountPege = () => {

            let creds = JSON.parse(localStorage.getItem('accountSession'))
            login.createAccountSession(creds.PEGE_ID)
        }

    </script>
</head>
<body>
   <ext:ResourceManager runat="server" />
    <form id="form1" runat="server">
        <ext:Desktop runat="server" ID="DESCRITORIO" ShortcutTextColor="Black" Wallpaper="../../Content/Images/fondo.jpg">
            <Shortcuts>
                <ext:DesktopShortcut ShortcutID="DSVALIDACION" Text="Validación con Dispositivos" IconCls="shortcut-icon icon-circulacion" />
            </Shortcuts>

            <Listeners>
                <ShortcutClick Handler="var d=#{DESCRITORIO}.getDesktop(); if(id == 'DSVALIDACION'){ window.location.href = 'http://localhost/kik/View/Public/BootPark/Circulation.aspx' }else { alert('Link Malo'); }" />
            </Listeners>
            
            <StartMenu Title="BOOT - PARK " Icon="Car" Height="400">
                <Items>
                    <ext:MenuItem ID="MENUPARAMETRIZACION" runat="server" Text="PARAMETRIZACIÓN" Icon="Folder" HideOnClick="false">
                        <Menu>
                            <ext:Menu ID="METIQUETA" runat="server">
                                <Items>
                                    <ext:MenuItem Text="Etiqueta" Icon="Cart">
                                        <Listeners>
                                            <Click Handler="crearVentanaWindow(#{DESCRITORIO},'BootPark/Parametrizacion/Etiqueta.aspx','Etiqueta',900,500);" />
                                        </Listeners>
                                    </ext:MenuItem>
                                     <ext:MenuItem Text="Particular" Icon="User">
                                        <Listeners>
                                            <Click Handler="crearVentanaWindow(#{DESCRITORIO},'BootPark/Parametrizacion/Particular.aspx','Particular',900,500);" />
                                        </Listeners>
                                    </ext:MenuItem>
                                     <ext:MenuItem Text="Vehiculo" Icon="Car">
                                        <Listeners>
                                            <Click Handler="crearVentanaWindow(#{DESCRITORIO},'BootPark/Parametrizacion/Vehiculo.aspx','Vehiculo',900,500);" />
                                        </Listeners>
                                    </ext:MenuItem>
                                      <ext:MenuItem Text="Terminal" Icon="LaptopAdd">
                                        <Listeners>
                                            <Click Handler="crearVentanaWindow(#{DESCRITORIO},'BootPark/Parametrizacion/Terminal.aspx','Terminal',900,500);" />
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
                                    <ext:MenuItem Text="Tag" IconCls="shortcut-icon-TagCar icon-TagCart" >
                                        <Listeners>
                                            <Click Handler="crearVentanaWindow(#{DESCRITORIO},'BootPark/Asignacion/Vehiculo.aspx','Tag',1000,500);" />
                                        </Listeners>
                                    </ext:MenuItem>
                                </Items>
                            </ext:Menu>
                        </Menu>
                    </ext:MenuItem>
                     <ext:MenuItem ID="MENUAUTORIZACION" runat="server" Text="AUTORIZACIÓN" Icon="Folder" HideOnClick="false">
                        <Menu>
                            <ext:Menu ID="MAUTORIZACIONPROPIETARIO" runat="server">
                                <Items>
                                    <ext:MenuItem Text="AUTORIZACIÓN PROPIETARIO" Icon="UserKey" >
                                        <Listeners>
                                            <Click Handler="crearVentanaWindow(#{DESCRITORIO},'BootPark/Autorizacion/AutorizacionPropietario.aspx','Autorización Propietario',800,500);" />
                                        </Listeners>
                                    </ext:MenuItem>
                                    <ext:MenuItem Text="AUTORIZACIÓN ADMIN" Icon="GroupKey" >
                                        <Listeners>
                                            <Click Handler="crearVentanaWindow(#{DESCRITORIO},'BootPark/Autorizacion/AutorizacionAdmin.aspx','Autorización Admin',800,500);" />
                                        </Listeners>
                                    </ext:MenuItem>
                                </Items>
                            </ext:Menu>
                        </Menu>
                   </ext:MenuItem>
                     <%--<ext:MenuItem ID="MENUCIRCULACION" runat="server" Text="CIRCULACIÓN" Icon="Folder" HideOnClick="false">
                        <Menu>
                            <ext:Menu ID="MCIRCULACION" runat="server">
                                <Items>
                                    <ext:MenuItem Text="CIRCULACIÓN" Icon="CarRed" >
                                        <Listeners>
                                            <Click Handler="crearVentanaWindow(#{DESCRITORIO},'Public/test/test.aspx','Circulacion',800,500);" />
                                        </Listeners>
                                    </ext:MenuItem>
                                </Items>
                            </ext:Menu>
                        </Menu>
                    </ext:MenuItem>--%>
                    <ext:MenuSeparator />
                </Items>
            </StartMenu>
    </ext:Desktop>
    </form>
</body>
</html>
