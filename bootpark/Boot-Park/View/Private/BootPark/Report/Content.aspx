<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Content.aspx.cs" Inherits="Boot_Park.View.Private.BootPark.Report.Content" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        var addTab = function (tabPanel, id, url) {
            var tab = tabPanel.getComponent(id);

            if (!tab) {
                tab = tabPanel.add({ 
                    id       : id, 
                    title    : url, 
                    closable : true,                    
                    autoLoad : {
                        showMask : true,
                        url      : url,
                        mode     : "iframe",
                        maskMsg  : "Loading " + url + "..."
                    }                    
                });

                tab.on("activate", function () {
                    var item = MenuPanel1.menu.items.get(id + "_item");
                    
                    if (item) {
                        MenuPanel1.setSelection(item);
                    }
                }, this);
            }
            
            tabPanel.setActiveTab(tab);
        }
    </script>
</head>
<body>
    <form runat="server">
        <ext:ResourceManager runat="server" />
        
        <ext:Panel
            runat="server" 
            Title="Adding tab"
            Width="1200" 
            Height="500" 
            Icon="Link" 
            Layout="border">
            <Items>
                <ext:MenuPanel ID="MenuPanel1" runat="server" Width="200" Region="West">
                    <Menu runat="server">
                        <Items>
                            <ext:MenuItem runat="server" Text="Historial de Ingreso/Salida">
                                <Listeners>
                                    <Click Handler="addTab(#{TabPanel1}, 'idClt', 'View/HistorialControl.aspx');" />
                                </Listeners>
                            </ext:MenuItem>
                            
                            <ext:MenuSeparator />
                            
                            <ext:MenuItem runat="server" Text="Vehiculos Parqueados">
                                <Listeners>
                                    <Click Handler="addTab(#{TabPanel1}, 'idGgl', 'View/VehiculoActuales.aspx');" />
                                </Listeners>
                            </ext:MenuItem>
                            
                            <ext:MenuSeparator />
                           
                        </Items>
                    </Menu>
                </ext:MenuPanel>
                <ext:TabPanel ID="TabPanel1" runat="server" Region="Center" />
            </Items>
        </ext:Panel>
    </form>
</body>
</html>
