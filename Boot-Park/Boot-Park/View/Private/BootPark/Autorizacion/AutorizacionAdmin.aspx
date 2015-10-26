<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutorizacionAdmin.aspx.cs" Inherits="Boot_Park.View.Private.BootPark.Autorizacion.AutorizacionAdmin" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Autorizacion Admin</title>
    <script type="text/javascript">

        var addRow = function (store, record, ddSource) {
            // Search for duplicates
            var foundItem = store.findExact('VEHI_ID', record.data.VEHI_ID);

            // if not found
            if (foundItem == -1) {
                //Remove Record from the source
                ddSource.grid.store.remove(record);

                store.add(record);

                // Call a sort dynamically
                store.sort('VEHI_ID', 'ASC');
            }
            return foundItem;
        };

        var notifyDrop1 = function (ddSource, e, data) {
            // Loop through the selections
            Ext.each(ddSource.dragData.selections, function (record) {
                parametro.desvincularVehiculoAlUsuario(record.data.VEHI_ID, HID_USER.getValue(), {
                    success: function (result) {
                        Ext.net.Notification.show({
                            html: 'vehiculo desautorizado exitosamente', title: 'Notificación'
                        });
                        addRow(SVEHICULOOUT, record, ddSource);
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

        var notifyDrop2 = function (ddSource, e, data) {
            // Loop through the selections
            Ext.each(ddSource.dragData.selections, function (record) {
                parametro.vincularVehiculoAlUsuario(record.data.VEHI_ID, HID_USER.getValue(), {
                    success: function (result) {
                        Ext.net.Notification.show({
                            html: 'vehiculo autorizado exitosamente', title: 'Notificación'
                        });
                        addRow(SVEHICULOIN, record, ddSource);
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
    <ext:Hidden ID="HID_USER" runat="server" />
    <ext:Hidden ID="HTIPO" runat="server" />
    <form id="FVEHICULO" runat="server">
        <div>
            <ext:Viewport ID="VPPRINCIPAL" runat="server" Layout="border">
                <Items>
                    <ext:Panel ID="PUSUARIO" runat="server" Layout="Fit" Region="Center">
                        <Items>
                            <ext:GridPanel ID="GPUSUARIO" runat="server" Height="300" Collapsible="True" Split="True" AutoExpandColumn="CAPELLIDO" Title="Usuarios" Icon="User">
                                <Store>
                                    <ext:Store ID="SUSUARIO" runat="server">
                                        <Reader>
                                            <ext:JsonReader>
                                                <Fields>
                                                    <ext:RecordField Name="ID" />
                                                    <ext:RecordField Name="IDENT" />
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
                                        <ext:Column ColumnID="CIDENT" DataIndex="IDENT" Header="Identificación" />
                                        <ext:Column ColumnID="CNOMBRE" DataIndex="NOMBRE" Header="Nombre" Width="300" />
                                        <ext:Column ColumnID="CAPELLIDO" DataIndex="APELLIDO" Header="Apellido" />
                                        <ext:Column ColumnID="CTIPO" DataIndex="TIPOUSUARIO" Header="Tipo" />

                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:RowSelectionModel SingleSelect="true">
                                        <Listeners>
                                            <RowSelect Handler="
                                                GPUSUARIO.collapse();
                                                PVEHICULO.expand();
                                                HID_USER.setValue(record.data.ID);
                                                HTIPO.setValue(record.data.TIPO);
                                                parametro.cargarVehiculosOUT();
                                                parametro.cargarVehiculosIN(record.data.ID);" />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <Listeners>
                                    <Command Handler="WDETALLEUSUARIO.show();" />
                                    <Expand Handler="PVEHICULO.collapse();" />
                                </Listeners>
                            </ext:GridPanel>
                            <ext:Panel ID="PVEHICULO" runat="server" Layout="Column" Padding="5" Collapsible="true" Collapsed="false" Title="Vehiculos" Icon="Car">
                                <Items>
                                    <ext:ColumnLayout runat="server" FitHeight="true">
                                        <Columns>
                                            <ext:LayoutColumn ColumnWidth="0.5">
                                                <ext:GridPanel ID="GPVEHICULOAOUT" runat="server" AutoExpandColumn="CVEHI_OBSERVACION" ColumnWidth="0.5" Title="Vehiculos Disponibles" Icon="CarAdd" EnableDragDrop="true" DDGroup="secondGridDDGroup">
                                                    <Store>
                                                        <ext:Store ID="SVEHICULOOUT" runat="server">
                                                            <Reader>
                                                                <ext:JsonReader>
                                                                    <Fields>
                                                                        <ext:RecordField Name="VEHI_ID" />
                                                                        <ext:RecordField Name="VEHI_PLACA" />
                                                                        <ext:RecordField Name="VEHI_MARCA" />
                                                                        <ext:RecordField Name="VEHI_MODELO" />
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
                                                            <ext:Column ColumnID="CVEHI_MARCA" DataIndex="VEHI_MARCA" Header="Marca" />
                                                            <ext:Column ColumnID="CVEHI_MODELO" DataIndex="VEHI_MODELO" Header="Modelo" />
                                                            <ext:Column ColumnID="CVEHI_OBSERVACION" DataIndex="VEHI_OBSERVACION" Header="Observaciones" />
                                                        </Columns>
                                                    </ColumnModel>
                                                    <SelectionModel>
                                                        <ext:RowSelectionModel SingleSelect="true" />
                                                    </SelectionModel>
                                                </ext:GridPanel>
                                            </ext:LayoutColumn>

                                            <ext:LayoutColumn>
                                                <ext:Panel
                                                    runat="server"
                                                    Width="35"
                                                    BodyStyle="background-color: transparent;"
                                                    Border="false"
                                                    Layout="Anchor">
                                                    <Items>
                                                        <ext:Panel runat="server" Border="false" BodyStyle="background-color: transparent;" AnchorVertical="40%" />
                                                        <ext:Panel runat="server" Border="false" BodyStyle="background-color: transparent;" Padding="5">
                                                            <Items>
                                                                <ext:Button runat="server" Icon="ResultsetNext" StyleSpec="margin-bottom:2px;">
                                                                    <Listeners>
                                                                   
                                                                         <Click Handler = " var records  = GPVEHICULOAOUT.selModel.getSelections();
                                                                                            Ext.each(records, function (record) {
                                                                                                parametro.vincularVehiculoAlUsuario(records[0].data.VEHI_ID, HID_USER.getValue(), {
                                                                                                    success: function (result) {
                                                                                                        Ext.net.Notification.show({
                                                                                                            html: 'vehiculo autorizado exitosamente', title: 'Notificación'
                                                                                                        });
                                                                                                        GPVEHICULOAOUT.deleteSelected();
                                                                                                        GPVEHICULOIN.store.addSorted(records);  
                                                                                                    },
                                                                                                    failure: function (errorMsg) {
                                                                                                        Ext.net.Notification.show({
                                                                                                            html: 'Ha ocurrido un error!!', title: 'Notificación'
                                                                                                        });
                                                                                                    }
                                                                                                });
                                                                                            });

                                                                                       " />
                                                                    </Listeners>
                                                                    <ToolTips>
                                                                        <ext:ToolTip runat="server" Title="Add" Html="Add Selected Rows" />
                                                                    </ToolTips>
                                                                </ext:Button>
                                                                <ext:Button runat="server" Icon="ResultsetPrevious" StyleSpec="margin-bottom:2px;">
                                                                    <Listeners>
                                                                        <Click Handler = " var records = GPVEHICULOIN.selModel.getSelections();
                                                                                           Ext.each(records, function (record) {
                                                                                                parametro.desvincularVehiculoAlUsuario(records[0].data.VEHI_ID, HID_USER.getValue(), {
                                                                                                    success: function (result) {
                                                                                                        Ext.net.Notification.show({
                                                                                                            html: 'vehiculo desautorizado exitosamente', title: 'Notificación'
                                                                                                        });
                                                                                                        GPVEHICULOIN.deleteSelected();
                                                                                                        GPVEHICULOAOUT.store.addSorted(records); 
                                                                                                    },
                                                                                                    failure: function (errorMsg) {
                                                                                                        Ext.net.Notification.show({
                                                                                                            html: 'Ha ocurrido un error!!', title: 'Notificación'
                                                                                                        });
                                                                                                    }

                                                                                                });
                                                                                            });
                                                                                       " />
                                                                    </Listeners>
                                                                    <ToolTips>
                                                                        <ext:ToolTip runat="server" Title="Remove" Html="Remove Selected Rows" />
                                                                    </ToolTips>
                                                                </ext:Button>
                                                            </Items>
                                                        </ext:Panel>
                                                    </Items>
                                                </ext:Panel>
                                            </ext:LayoutColumn>
                                            <ext:LayoutColumn>
                                                <ext:GridPanel ID="GPVEHICULOIN" runat="server" AutoExpandColumn="CVEHI_OBSERVACION" ColumnWidth="0.5" Title="Vehiculos Asignados" Icon="CarRed" EnableDragDrop="true" DDGroup="firstGridDDGroup">
                                                    <Store>
                                                        <ext:Store ID="SVEHICULOIN" runat="server">
                                                            <Reader>
                                                                <ext:JsonReader>
                                                                    <Fields>
                                                                        <ext:RecordField Name="VEHI_ID" />
                                                                        <ext:RecordField Name="VEHI_PLACA" />
                                                                        <ext:RecordField Name="VEHI_MARCA" />
                                                                        <ext:RecordField Name="VEHI_MODELO" />
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
                                                            <ext:Column ColumnID="CVEHI_MARCA" DataIndex="VEHI_MARCA" Header="Marca" />
                                                            <ext:Column ColumnID="CVEHI_MODELO" DataIndex="VEHI_MODELO" Header="Modelo" />
                                                            <ext:Column ColumnID="CVEHI_OBSERVACION" DataIndex="VEHI_OBSERVACION" Header="Observaciones" />
                                                        </Columns>
                                                    </ColumnModel>
                                                    <SelectionModel>
                                                        <ext:RowSelectionModel SingleSelect="true" />
                                                    </SelectionModel>
                                                </ext:GridPanel>
                                            </ext:LayoutColumn>
                                        </Columns>
                                    </ext:ColumnLayout>
                                </Items>
                            </ext:Panel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Viewport>

        </div>
        <ext:DropTarget runat="server" Target="={GPVEHICULOAOUT.view.scroller.dom}" Group="firstGridDDGroup">
            <NotifyDrop Fn="notifyDrop1" />
        </ext:DropTarget>

        <ext:DropTarget runat="server" Target="={GPVEHICULOIN.view.scroller.dom}" Group="secondGridDDGroup">
            <NotifyDrop Fn="notifyDrop2" />
        </ext:DropTarget>

    </form>
</body>
</html>
