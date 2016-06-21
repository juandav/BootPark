<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Vehiculo.aspx.cs" Inherits="Boot_Park.View.Private.BootPark.Asignacion.Vehiculo" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../../Content/css/desktop.css" rel="stylesheet" />
    <title>Vehiculo - Tag</title>
    <script type="text/javascript">

        var rfid = new WebSocket('ws://172.16.31.150:2020');

        rfid.onerror = function (error) {
            Ext.net.Notification.show({ iconCls: 'icon-information', hideDelay: 2000, html: 'El servicio del Lector RFID PT-3L01Z esta inactivo', title: 'Notificación' });
        };

        rfid.onmessage = function (evt) {
            var kyt = evt.data;
            eval(kyt);

            switch (kyt.type) {
                case "connect":
                    rfid.send('reset');
                    break;
                case "disconnect":
                    alert(':) desconectado');
                    break;
                case "tag":
                    console.log(kyt.payload.tag)
                    var _tag = String(kyt.payload.tag)
                    TFfindEtiquetaOut.setValue(_tag);
                    findEtiquetaINSocket(GPETIQUETAOUT.store, _tag);
                    stoprfid();
                    break;
            }
        }

        function playRfid() {
            BSTOP.show();
            BPLAYER.hide();
            rfid.send("reset")
            rfid.send("start")
        }
        function stoprfid() {
            rfid.send("pause");
            BSTOP.hide();
            BPLAYER.show();
        }

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
            Ext.each(ddSource.dragData.selections, function (record) {
                parametro.DesvincularTagalVehiculo(record.data.ETIQ_ID, HID_VEHI.getValue(), {
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
           
            Ext.each(ddSource.dragData.selections, function (record) {
                parametro.vincularTagAlVehiculo(record.data.ETIQ_ID, HID_VEHI.getValue(), '', {
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
        
        var findCar = function (Store, texto, e) {
            if (e.getKey() == 13) {
                var store = Store,
                    text = texto;
                store.clearFilter();
                if (Ext.isEmpty(text, false)) {
                    return;
                }
                var re = new RegExp(".*" + text + ".*", "i");
                store.filterBy(function (node) {
                    var RESUMEN = node.data.VEHI_MARCA + node.data.VEHI_PLACA;
                    var a = re.test(RESUMEN);
                    return a;
                });
            }
        };

        var findEtiquetaOUT = function (Store, texto, e) {
           
            if (e.getKey() == 13) {
                var store = Store,
                    text = texto;
                store.clearFilter();
                if (Ext.isEmpty(text, false)) {
                    return;
                }
                var re = new RegExp(".*" + text + ".*", "i");
                store.filterBy(function (node) {
                    var RESUMEN = node.data.ETIQ_ETIQUETA + node.data.ETIQ_DESCRIPCION;
                    var a = re.test(RESUMEN);
                    return a;
                });
            }
        };

        var findEtiquetaINSocket = function (Store, texto) {
                var store = Store,
                    text = texto;
                store.clearFilter();
                var re = new RegExp(".*" + text + ".*", "i");
                store.filterBy(function (node) {
                    var RESUMEN = node.data.ETIQ_ETIQUETA + node.data.ETIQ_DESCRIPCION;
                    var a = re.test(RESUMEN);
                    return a;
                });
        };

        var findEtiquetaIN = function (Store, texto, e) {
            if (e.getKey() == 13) {
                var store = Store,
                    text = texto;
                store.clearFilter();
                if (Ext.isEmpty(text, false)) {
                    return;
                }
                var re = new RegExp(".*" + text + ".*", "i");
                store.filterBy(function (node) {
                    var RESUMEN = node.data.ETIQ_ETIQUETA + node.data.ETIQ_DESCRIPCION;
                    var a = re.test(RESUMEN);
                    return a;
                });
            }
        };
       
    </script>
</head>
<body>

    <ext:ResourceManager runat="server" />
     <ext:Hidden ID="HID_VEHI" runat="server" />
    <form id="TAG" runat="server">
        <div>
            <ext:Viewport ID="VPPRINCIPAL" runat="server" Layout="border">
                <Items>
                    <ext:Panel ID="PVEHICULO" runat="server" Layout="Fit" Region="Center">
                        <Items>
                            <ext:GridPanel ID="GPVEHICULO" runat="server" Height="300" Collapsible="True" Split="True" AutoExpandColumn="CVEHI_OBSERVACION" Title="Vehiculos" Icon="Car">
                                <TopBar>
                                    <ext:Toolbar runat="server">
                                        <Items>
                                            <ext:TextField ID="TFfindCar" runat="server" EmptyText="Escriba la placa o marca del vehiculo para buscar" Width="400" EnableKeyEvents="true" Icon="Magnifier">
                                                <Listeners>
                                                    <KeyPress Handler="findCar(GPVEHICULO.store, TFfindCar.getValue(), Ext.EventObject);" />
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
                                                    <ext:RecordField Name="VEHI_ID" Type="String" />
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
                                                PETIQUETA.setTitle('Etiqueta para asignar al Vehiculo:  ' + record.data.VEHI_MARCA + ' ' + record.data.VEHI_MODELO + '(' + record.data.VEHI_PLACA + ')' );
                                                HID_VEHI.setValue(record.data.VEHI_ID);
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
                                    <ext:ColumnLayout runat="server" FitHeight="true">
                                        <Columns>
                                            <ext:LayoutColumn ColumnWidth="0.5">
                                                <ext:GridPanel ID="GPETIQUETAOUT" runat="server" AutoExpandColumn="CETIQ_DESCRIPCION" ColumnWidth="0.5" Title="Etiquetas Disponibles" Icon="TagBlueAdd" EnableDragDrop="true" DDGroup="secondGridDDGroup">
                                                     <TopBar>
                                                        <ext:Toolbar   runat="server">
                                                            <Items>
                                                                <ext:TextField ID="TFfindEtiquetaOut" runat="server" EmptyText="codigo o etiqueta para buscar" Width="400" EnableKeyEvents="true" Icon="Magnifier">
                                                                    <Listeners>
                                                                        <KeyPress Handler="findEtiquetaOUT(GPETIQUETAOUT.store, TFfindEtiquetaOut.getValue(), Ext.EventObject);" />
                                                                    </Listeners>
                                                                </ext:TextField> 
                                                                <ext:Button ID ="BPLAYER" runat="server" Icon="PlayBlue" Hidden="false">
                                                                    <Listeners>
                                                                        <Click Handler="playRfid();" />
                                                                    </Listeners>
                                                                </ext:Button>
                                                                <ext:Button ID ="BSTOP" runat="server" Icon="Stop" Hidden="true" >
                                                                    <Listeners>
                                                                        <Click Handler="stoprfid()" />
                                                                    </Listeners>
                                                                </ext:Button>
                                                            </Items>
                                                        </ext:Toolbar>
                                                    </TopBar>
                                                    <Store>
                                                        <ext:Store ID="SETIQUETAOUT" runat="server">
                                                            <Reader>
                                                                <ext:JsonReader>
                                                                    <Fields>
                                                                        <ext:RecordField Name="ETIQ_ID" Type="String" />
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
                                                            <ext:Column ColumnID="CETIQ_ID" DataIndex="ETIQ_ID" Header="Codigo" />
                                                            <ext:Column ColumnID="CETIQ_ETIQUETA" DataIndex="ETIQ_ETIQUETA" Header="Tag" />
                                                            <ext:Column ColumnID="CETIQ_DESCRIPCION" DataIndex="ETIQ_DESCRIPCION" Header="Descripción" />
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

                                                                        <Click Handler=" var records  = GPETIQUETAOUT.selModel.getSelections();
                                                                                            Ext.each(records, function (record) {
                                                                                               parametro.vincularTagAlVehiculo(record.data.ETIQ_ID, HID_VEHI.getValue(), '', {
                                                                                                    success: function (result) {
                                                                                                        Ext.net.Notification.show({
                                                                                                            html: 'Tag asignado existosamente', title: 'Notificación'
                                                                                                        });
                                                                                                        GPETIQUETAOUT.deleteSelected();
                                                                                                        GPETIQUETAIN.store.addSorted(records);  
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
                                                                        <Click Handler=" var records = GPETIQUETAIN.selModel.getSelections();
                                                                                           Ext.each(records, function (record) {
                                                                                                parametro.DesvincularTagalVehiculo(record.data.ETIQ_ID, HID_VEHI.getValue(), {
                                                                                                    success: function (result) {
                                                                                                        Ext.net.Notification.show({
                                                                                                            html: 'Tag desvinculado existosamente', title: 'Notificación'
                                                                                                        });
                                                                                                        GPETIQUETAIN.deleteSelected();
                                                                                                        GPETIQUETAOUT.store.addSorted(records); 
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
                                                <ext:GridPanel ID="GPETIQUETAIN" runat="server" AutoExpandColumn="CETIQ_DESCRIPCION" ColumnWidth="0.5" Title="Etiquetas Asignados" Icon="TagBlueEdit" EnableDragDrop="true" DDGroup="firstGridDDGroup">
                                                    <TopBar>
                                                        <ext:Toolbar runat="server">
                                                            <Items>
                                                                <ext:TextField ID="TFfindEtiquetaIN" runat="server" EmptyText="codigo o etiqueta para buscar" Width="400" EnableKeyEvents="true" Icon="Magnifier">
                                                                    <Listeners>
                                                                        <KeyPress Handler="findEtiquetaIN(GPETIQUETAIN.store, TFfindEtiquetaIN.getValue(), Ext.EventObject);" />
                                                                    </Listeners>
                                                                </ext:TextField>
                                                            </Items>
                                                        </ext:Toolbar>
                                                    </TopBar>
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
                                                            <ext:Column ColumnID="CETIQ_ID" DataIndex="ETIQ_ID" Header="Codigo" />
                                                            <ext:Column ColumnID="CETIQ_ETIQUETA" DataIndex="ETIQ_ETIQUETA" Header="Tag" />
                                                            <ext:Column ColumnID="CETIQ_DESCRIPCION" DataIndex="ETIQ_DESCRIPCION" Width="150" Header="Descripción" />
                                                        </Columns>
                                                    </ColumnModel>
                                                    <SelectionModel>
                                                        <ext:RowSelectionModel runat="server" SingleSelect="true" />
                                                    </SelectionModel>
                                                    <Listeners>
                                                        <AfterEdit Fn="afterEdit" />
                                                    </Listeners>
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
        <ext:DropTarget runat="server" Target="={GPETIQUETAOUT.view.scroller.dom}" Group="firstGridDDGroup">
            <NotifyDrop Fn="notifyDrop1" />
        </ext:DropTarget>

        <ext:DropTarget runat="server" Target="={GPETIQUETAIN.view.scroller.dom}" Group="secondGridDDGroup">
            <NotifyDrop Fn="notifyDrop2" />
        </ext:DropTarget>
    </form>
</body>
</html>


