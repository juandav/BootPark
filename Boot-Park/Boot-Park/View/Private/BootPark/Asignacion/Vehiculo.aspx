<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Vehiculo.aspx.cs" Inherits="Boot_Park.View.Private.BootPark.Asignacion.Vehiculo" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../../Content/css/desktop.css" rel="stylesheet" />
    <title>Vehiculo - Tag</title>
    <script type="text/javascript">

        var afterEdit = function (e) {
            var dataVehiculo = GPVEHICULO.selModel.getSelections();
            parametro.modificarTagalVehiculo(e.record.data.ETIQ_ID, dataVehiculo[0].data.VEHI_ID, e.record.data.ETVE_OBSERVACION, {
                success: function (result) {
                    Ext.net.Notification.show({
                        html: 'Actualización existosamente', title: 'Notificación'
                    });
                    GPETIQUETAIN.store.commitChanges();
                },
                failure: function (errorMsg) {
                    Ext.net.Notification.show({
                        html: 'Ha ocurrido un error!!', title: 'Notificación'
                    });
                }

            });
        };

        var addRow = function (store, record, ddSource) {
            // Search for duplicates
            var foundItem = store.findExact('ETIQ_ID', record.data.ETIQ_ID);

            // if not found
            if (foundItem == -1) {
                //Remove Record from the source
                ddSource.grid.store.remove(record);

                store.add(record);

                // Call a sort dynamically
                store.sort('ETIQ_ID', 'ASC');
            }
        };

        var notifyDrop1 = function (ddSource, e, data) {
            // Loop through the selections
            var dataVehiculo = GPVEHICULO.selModel.getSelections();
            Ext.each(ddSource.dragData.selections, function (record) {
                parametro.DesvincularTagalVehiculo(record.data.ETIQ_ID, dataVehiculo[0].data.VEHI_ID, {
                    success: function (result) {
                        Ext.net.Notification.show({
                            html: 'Tag desvinculado existosamente', title: 'Notificación'
                        });
                        addRow(SETIQUETAOUT, record, ddSource);
                    },
                    failure: function (errorMsg) {
                        Ext.net.Notification.show({
                            html: 'Ha ocurrido un error!!', title: 'Notificación'
                        });
                    }

                });
            });
            return true;
        }


        var notifyDrop2 = function (ddSource, e, data) {
            // Loop through the selections
            var dataVehiculo = GPVEHICULO.selModel.getSelections();
            Ext.each(ddSource.dragData.selections, function (record) {
                parametro.vincularTagAlVehiculo(record.data.ETIQ_ID, dataVehiculo[0].data.VEHI_ID, '', {
                    success: function (result) {
                        Ext.net.Notification.show({
                            html: 'Tag asignado existosamente', title: 'Notificación'
                        });
                        addRow(STIQUETAIN, record, ddSource);
                    },
                    failure: function (errorMsg) {
                        Ext.net.Notification.show({
                            html: 'Ha ocurrido un error!!', title: 'Notificación'
                        });

                    }

                });
            });

            return true;
        };
    </script>
</head>
<body>

    <ext:ResourceManager runat="server" />
    <form id="TAG" runat="server">
        <div>
            <ext:Viewport ID="VPPRINCIPAL" runat="server" Layout="border">
                <Items>
                    <ext:Panel ID="PVEHICULO" runat="server" Layout="Fit" Region="Center">
                        <Items>
                            <ext:GridPanel ID="GPVEHICULO" runat="server" Height="300" Collapsible="True" Split="True" AutoExpandColumn="CVEHI_OBSERVACION" Title="Vehiculos" Icon="Car">
                                <Store>
                                    <ext:Store ID="SVEHICULO" runat="server">
                                        <Reader>
                                            <ext:JsonReader>
                                                <Fields>
                                                    <ext:RecordField Name="VEHI_ID" />
                                                    <ext:RecordField Name="VEHI_OBSERVACION" />
                                                    <ext:RecordField Name="VEHI_PLACA" />
                                                    <ext:RecordField Name="VEHI_MODELO" />
                                                    <ext:RecordField Name="VEHI_MARCA" />
                                                    <ext:RecordField Name="VEHI_COLOR" />
                                                    <ext:RecordField Name="VEHI_OBSERVACION" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn />
                                        <ext:Column ColumnID="CVEHI_PLACA" DataIndex="VEHI_PLACA" Header="Placa" />
                                        <ext:Column ColumnID="CVEHI_MODELO" DataIndex="VEHI_MODELO" Header="Modelo" />
                                        <ext:Column ColumnID="CVEHI_MARCA" DataIndex="VEHI_MARCA" Header="Marca" />
                                        <ext:Column ColumnID="CVEHI_COLOR" DataIndex="VEHI_COLOR" Header="Color" />
                                        <ext:Column ColumnID="CVEHI_OBSERVACION" DataIndex="VEHI_OBSERVACION" Header="Observación" />
                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:RowSelectionModel SingleSelect="true">
                                        <Listeners>
                                            <RowSelect Handler="
                                                GPVEHICULO.collapse();
                                                PETIQUETA.expand();
                                                parametro.cargarEtiquetasOUT();
                                                parametro.cargarEtiquetasIN(record.data.VEHI_ID);" />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <Listeners>
                                    <Command Handler="if(command=='Detalle'){WDETALLEUSUARIO.show();}else{conectar();}" />
                                    <Expand Handler="PETIQUETA.collapse();" />
                                </Listeners>
                            </ext:GridPanel>
                            <ext:Panel ID="PETIQUETA" runat="server" Layout="Column" Padding="5" Collapsible="true" Collapsed="false" Title="Etiqueta" Icon="TagBlue">
                                <Items>
                                    <ext:GridPanel ID="GPETIQUETAOUT" runat="server" AutoExpandColumn="CETIQ_DESCRIPCION" ColumnWidth="0.5" Title="Etiquetas Disponibles" Icon="TagBlueAdd" EnableDragDrop="true" DDGroup="secondGridDDGroup">
                                        <Store>
                                            <ext:Store ID="SETIQUETAOUT" runat="server">
                                                <Reader>
                                                    <ext:JsonReader>
                                                        <Fields>
                                                            <ext:RecordField Name="ETIQ_ID" />
                                                            <ext:RecordField Name="ETIQ_TIPO" />
                                                            <ext:RecordField Name="ETIQ_ETIQUETA" />
                                                            <ext:RecordField Name="ETIQ_DESCRIPCION" />
                                                        </Fields>
                                                    </ext:JsonReader>
                                                </Reader>
                                            </ext:Store>
                                        </Store>
                                        <ColumnModel>
                                            <Columns>
                                                <ext:RowNumbererColumn />
                                                <ext:Column ColumnID="CETIQ_ETIQUETA" DataIndex="ETIQ_ETIQUETA" Header="Tag" />
                                                <ext:Column ColumnID="CETIQ_DESCRIPCION" DataIndex="ETIQ_DESCRIPCION" Header="Descripción" />
                                            </Columns>
                                        </ColumnModel>
                                        <SelectionModel>
                                            <ext:RowSelectionModel SingleSelect="true" />
                                        </SelectionModel>
                                    </ext:GridPanel>
                                    <ext:GridPanel ID="GPETIQUETAIN" runat="server" AutoExpandColumn="CETIQ_OBSERVACION" ColumnWidth="0.5" Title="Etiquetas Asignados" Icon="TagBlueEdit" EnableDragDrop="true" DDGroup="firstGridDDGroup">
                                        <Store>
                                            <ext:Store ID="STIQUETAIN" runat="server">
                                                <Reader>
                                                    <ext:JsonReader>
                                                        <Fields>
                                                            <ext:RecordField Name="ETIQ_ID" />
                                                            <ext:RecordField Name="ETIQ_TIPO" />
                                                            <ext:RecordField Name="ETIQ_ETIQUETA" />
                                                            <ext:RecordField Name="ETIQ_DESCRIPCION" />
                                                            <ext:RecordField Name="ETVE_OBSERVACION" />
                                                        </Fields>
                                                    </ext:JsonReader>
                                                </Reader>
                                            </ext:Store>
                                        </Store>
                                        <ColumnModel>
                                            <Columns>
                                                <ext:RowNumbererColumn />
                                                <ext:Column ColumnID="CETIQ_ETIQUETA" DataIndex="ETIQ_ETIQUETA" Header="Tag" />
                                                <ext:Column ColumnID="CETIQ_DESCRIPCION" DataIndex="ETIQ_DESCRIPCION" Width="150" Header="Descripción" />
                                                <ext:Column ColumnID="CETIQ_OBSERVACION" DataIndex="ETVE_OBSERVACION" Header="Observaciones">
                                                    <Editor>
                                                        <ext:TextField runat="server" />
                                                    </Editor>
                                                </ext:Column>
                                            </Columns>
                                        </ColumnModel>
                                        <SelectionModel>
                                            <ext:RowSelectionModel runat="server" SingleSelect="true" />
                                        </SelectionModel>
                                        <Listeners>
                                              <AfterEdit Fn="afterEdit" />
                                        </Listeners>
                                    </ext:GridPanel>
                                </Items>
                            </ext:Panel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Viewport>
        </div>
        <ext:DropTarget runat="server" Target="={GPETIQUETAOUT.view.scroller.dom}" Group="firstGridDDGroup">
            <NotifyDrop Fn="notifyDrop1" />
        </ext:DropTarget>

        <ext:DropTarget runat="server" Target="={GPETIQUETAIN.view.scroller.dom}" Group="secondGridDDGroup">
            <NotifyDrop Fn="notifyDrop2" />
        </ext:DropTarget>
    </form>
</body>
</html>


