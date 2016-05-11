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
    <ext:ResourceManager runat="server" Theme="NeptuneTouch" />
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
                                            <ext:Label ID="LESTADO" runat="server" Text="Esperando Usuario...." Cls="x-btn-text"></ext:Label>
                                        </Items>
                                    </ext:Panel>
                                </Items>
                            </ext:Panel>
                        </Items>
                    </ext:Panel>
                    <ext:FormPanel runat="server" Region="East"  Title="Datos del Usuario" Collapsed="true" Split="true"  Collapsible="true" AnimCollapseDuration="800" Width="600" Icon="User" ID="USERID">
                        <Items>
                            <ext:Panel runat="server">
                                <Items>
                                    <ext:FormPanel runat="server" >
                                        <Items>
                                            <ext:Panel runat="server" Layout="CenterLayout" Height="250">
                                                <Items>
                                                    <ext:Image runat="server" ID="IMUSUARIO" ImageUrl="../../../content/img/user.png" Width="200" Height="200" />
                                                </Items>
                                            </ext:Panel>
                                             
                                        </Items>
                                    </ext:FormPanel>
                                    <ext:FormPanel runat="server" Layout="CenterLayout" Height="100">
                                        <Items>
                                            <ext:FieldContainer runat="server"   Layout="HBoxLayout" >
                                                <Items>
                                                    <ext:Label runat="server" ID="LIDENTIFICACION" Height="100" MarginSpec="10 10 10 10" Text="dfdfddgf" />
                                                    <ext:Label runat="server" ID="LNOMBRE" Height="100" MarginSpec="10 10 10 10" Text="dfgfgfghghghg" />
                                                </Items>
                                           </ext:FieldContainer>
                                        </Items>
                                    </ext:FormPanel>
                                    
                                </Items>
                            </ext:Panel>
                            <ext:Panel runat="server" ID="PVEHICULO" Title="Vehiculo I/O" Region="South" Collapsed="true"  Collapsible="true" Icon="Car" Height="400">
                                <Items>
                                    <ext:FormPanel ID="FVHEHICULO" runat="server" Layout="HBoxLayout" Height="300" MarginSpec="10">
                                        <Items>
                                            <ext:Label runat="server" ID="LVEHICULO" MarginSpec="10 10 10 10" Flex="1" Height="200" Text="kdjfd" />
                                            <ext:Label runat="server" ID="LHORATIPO" MarginSpec="10 10 10 10" Flex="1" Height="200" Text="dfd" />
                                        </Items>
                                    </ext:FormPanel>
                                </Items>
                            </ext:Panel>
                        </Items>
                   </ext:FormPanel>
                   
                </Items>
            </ext:Viewport>
        </div>
    </form>
</body>
</html>
