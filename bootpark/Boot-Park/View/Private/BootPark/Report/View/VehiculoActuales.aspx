<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VehiculoActuales.aspx.cs" Inherits="Boot_Park.View.Private.BootPark.Report.View.VehiculoActuales" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
 

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

     <script type="text/javascript">

        var findVehiculo = function (Store, texto, e) {
            if (e.getKey() == 13) {
                var store = Store,
                    text = texto;
                store.clearFilter();
                if (Ext.isEmpty(text, false)) {
                    return;
                }
                var re = new RegExp(".*" + text + ".*", "i");
                store.filterBy(function (node) {
                    var RESUMEN = node.data.VEHICULO + node.data.USUARIOENTRADA;
                    var a = re.test(RESUMEN);
                    return a;
                });
            }
        };
    </script>
</head>
<body>
     <ext:ResourceManager runat="server" />
    <form id="FPRINCIPAL" runat="server">

        <div>
            <ext:viewport id="VPPRESENTACION" runat="server" layout="border">
                <Items>
                    <ext:Panel ID="PPRESENTACION" runat="server" Layout="Fit" Region="Center" Padding="5">
                        <Items>
                            <ext:GridPanel ID="GPVEHICULO" runat="server" >
                                <TopBar>
                                    <ext:Toolbar runat="server">
                                        <Items>
                                            <ext:TextField ID="TFfindVehiculo" runat="server" EmptyText="Vehiculo o usuario para buscar" Width="400" EnableKeyEvents="true" Icon="Magnifier">
                                                <Listeners>
                                                    <KeyPress Handler="findVehiculo(GPVEHICULO.store, TFfindVehiculo.getValue(), Ext.EventObject);" />
                                                </Listeners>
                                            </ext:TextField>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <Store>
                                    <ext:Store ID="SVEHICULO" runat="server">
                                        <Reader>
                                            <ext:JsonReader>
                                                <Fields>
                                                    <ext:RecordField Name="CODIGO"  />
                                                    <ext:RecordField Name="VEHICULO" />
                                                    <ext:RecordField Name="ENTRADA" />
                                                    <ext:RecordField Name="USUARIOENTRADA" />
                                                    <ext:RecordField Name="FECHAINI" />
                                                    <ext:RecordField Name="HORAINI" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn />
                                        <ext:Column ColumnID="CVEHICULO" DataIndex="VEHICULO" Header="VEHICULO" Width="350" />
                                        <ext:Column ColumnID="CUSUARIOENTRADA" DataIndex="USUARIOENTRADA" Header="USUARIO" Width="350" />
                                        <ext:Column ColumnID="CFECHAINI" DataIndex="FECHAINI" Header="FECHA" Width="100" />
                                        <ext:Column ColumnID="CHORAINI" DataIndex="HORAINI" Header="HORA" Width="100" />
                                    </Columns>
                                </ColumnModel>
                                
                            </ext:GridPanel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:viewport>
        </div>
    </form>
</body>
</html>
