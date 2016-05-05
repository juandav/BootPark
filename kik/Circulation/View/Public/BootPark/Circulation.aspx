<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Circulation.aspx.cs" Inherits="Circulation.View.Public.BootPark.Circulation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Circulation </title>
    <style>
        .x-label-value {
            font: oblique bold 120% cursive;
            float: none;
            font-size: 19px;
        }
    </style>
    <script src='../../../content/js/socket.io-1.4.5.js'></script>
    <script src='../../../content/js/Circulations.js'></script>
</head>
<body>
    <ext:ResourceManager runat="server" Theme="Crisp" />
    <form id="fcirculation" runat="server">
        <div>
            <ext:Viewport runat="server" Layout="BorderLayout">
                <Items>
                    <ext:Panel runat="server" Region="Center" Title="Circulación" Icon="Car" ID="CIRCID" Layout="CenterLayout">
                        <Items>
                            <ext:Panel runat="server">
                                <Items>
                                    <ext:Panel runat="server">
                                        <Items>
                                            <ext:Image runat="server" Src="../../../content/img/gears.gif" Width="200" Height="200"></ext:Image>
                                        </Items>
                                    </ext:Panel>
                                    <ext:Panel runat="server">
                                        <Items>
                                            <ext:Label runat="server" Text="Esperando Usuario...." Cls="x-btn-text"></ext:Label>
                                        </Items>
                                    </ext:Panel>
                                </Items>
                            </ext:Panel>
                        </Items>
                    </ext:Panel>
                    <ext:Panel runat="server" Region="East" Title="Datos del Usuario" Collapsed="true" Collapsible="true" Width="600" Icon="User" ID="USERID" Layout="CenterLayout">
                        <Items>
                            <ext:Panel runat="server">
                                <Items>
                                    <ext:Panel runat="server">
                                        <Items>
                                            <ext:Image runat="server" ID="IMUSUARIO" Width="200" Height="200" />
                                        </Items>
                                    </ext:Panel>
                                    <ext:Panel runat="server">
                                        <Items>
                                            <ext:TextField runat="server" ID="TIDENTIFICACION" FieldLabel="Identíficación" ReadOnly="true" />
                                        </Items>
                                    </ext:Panel>
                                </Items>
                            </ext:Panel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Viewport>
        </div>
    </form>
</body>
</html>
