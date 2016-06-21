<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Identidad.aspx.cs" Inherits="Boot_Park.View.Private.BootPark.Asignacion.Identidad" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../../Content/css/desktop.css" rel="stylesheet" />
    <script src="../../../../Content/js/BiometricDevice.js"></script>
    <title>Identidad</title>

    <script type="text/javascript">
        var io = new WebSocket("ws://172.16.31.150:2012");

        io.onerror = function (error) {
            Ext.net.Notification.show({ iconCls: 'icon-information', hideDelay: 5000, html: 'El servicio del Lector ZK-F19 ID  esta inactivo', title: 'Notificación' });
        };

        function emit(event, data) {

            var msg = {
                type: event,
                payload: []
            };

            msg.payload.push(data)

            io.send(JSON.stringify(msg));
        }
      
        io.onmessage = function (evt) {
            var data;
            eval(evt.data);
            console.log(data);
            switch (data.type) {
                case "card":
                    break;
                case "user":
                    break;
                case "getfinger":
                    var r = data.payload[0];
                    parametro.registraHuellausuario(r.data, r.index, r.length, r.user);
                    break;
                default:

            }
            
        }


        var afterEdit = function (e) {
            var dataUser = GPUSUARIO.selModel.getSelections();
            parametro.modificarCarnetUsuario(e.record.data.ETIQ_ID, dataUser[0].data.ID, e.record.data.ETUS_MOTIVO, {
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

        var prepareCommand = function (grid, command, record, row) {
            var t = this;
            // debugger;
            try {
                if (command.items.items[record].command == 'footprint' && row.data.HUEL_ESTADO === 'EXISTE') {
                    command.hidden = true;
                    command.hideMode = 'display'; //you can try 'visibility' also     
                }
            } catch (e) { console.log(e); }
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
            var dataUser = GPUSUARIO.selModel.getSelections();
            Ext.each(ddSource.dragData.selections, function (record) {
                parametro.desvincularCarnetAlUsuario(record.data.ETIQ_ID, HID_USER.getValue(), {
                    success: function (result) {
                        addRow(SETIQUETAOUT, record, ddSource);
                    },
                    failure: function (errorMsg) {
                    }

                });
            });

            return true;
        };

        var notifyDrop2 = function (ddSource, e, data) {

            // Loop through the selections
            var dataUser = GPUSUARIO.selModel.getSelections();
            Ext.each(ddSource.dragData.selections, function (record) {
                parametro.vincularCarnetAlUsuario(record.data.ETIQ_ID, HID_USER.getValue(), {
                    success: function (result) {
                        emit('recordcard', { user: String(HID_USER.getValue()), name: String(HNOMBRE_USER.getValue()), card: String(record.data.ETIQ_ETIQUETA) })
                        addRow(STIQUETAIN, record, ddSource);
                    },
                    failure: function (errorMsg) {

                    }
                });
            });

            return true;
        };

        var findUser = function (Store, texto, e) {
            if (e.getKey() == 13) {
                var store = Store,
                    text = texto;
                store.clearFilter();
                if (Ext.isEmpty(text, false)) {
                    return;
                }
                var re = new RegExp(".*" + text + ".*", "i");
                store.filterBy(function (node) {
                    var RESUMEN = node.data.IDENT + node.data.NOMBRE + node.data.APELLIDO;
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
                    var RESUMEN = node.data.ETIQ_ID + node.data.ETIQ_ETIQUETA;
                    var a = re.test(RESUMEN);
                    return a;
                });
            }
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
                    var RESUMEN = node.data.ETIQ_ID + node.data.ETIQ_ETIQUETA;
                    var a = re.test(RESUMEN);
                    return a;
                });
            }
        };


    </script>
</head>
<body>

    <ext:ResourceManager runat="server" />
    <ext:Hidden ID="HID_USER" runat="server" />
    <ext:Hidden ID="HNOMBRE_USER" runat="server" />
    <form id="FIDENTIDAD" runat="server">
        <div>
            <ext:Viewport ID="VPPRINCIPAL" runat="server" Layout="border">
                <Items>
                    <ext:Panel ID="PUSUARIO" runat="server" Layout="Fit" Region="Center">
                        <Items>
                            <ext:GridPanel ID="GPUSUARIO" runat="server" Height="300" Collapsible="True" Split="True" AutoExpandColumn="CAPELLIDO" Title="Usuarios" Icon="User">
                                <TopBar>
                                    <ext:Toolbar runat="server">
                                        <Items>
                                            <ext:TextField ID="TFfindUser" runat="server" EmptyText="Cedula, nombre o apellido para buscar" Width="400" EnableKeyEvents="true" Icon="Magnifier">
                                                <Listeners>
                                                    <KeyPress Handler="findUser(GPUSUARIO.store, TFfindUser.getValue(), Ext.EventObject);" />
                                                </Listeners>
                                            </ext:TextField>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>

                                <Store>
                                    <ext:Store ID="SUSUARIO" runat="server">
                                        <Reader>
                                            <ext:JsonReader>
                                                <Fields>
                                                    <ext:RecordField Name="ID" />
                                                    <ext:RecordField Name="IDENT" />
                                                    <ext:RecordField Name="NOMBRE" />
                                                    <ext:RecordField Name="APELLIDO" />
                                                    <ext:RecordField Name="TIPO" />
                                                    <ext:RecordField Name="HUEL_ESTADO" />
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
                                        <ext:Column ColumnID="CTIPO" DataIndex="TIPO" Header="Tipo" />
                                        <ext:CommandColumn Width="65" DataIndex="HUEL_ESTADO" Header="Huella">
                                            <Commands>
                                                <ext:GridCommand IconCls=".shortcut-icon-enrollment-Footprint icon-enrollment-Footprint" CommandName="enrollmentFootprint">
                                                    <ToolTip Text="incripcion de la Huella" />
                                                </ext:GridCommand>
                                                <ext:CommandSeparator />
                                                <ext:GridCommand IconCls="shortcut-icon-footprint icon-footprint" CommandName="footprint">
                                                    <ToolTip Text="Registrar la huella vinculado al Chaira." />
                                                </ext:GridCommand>
                                            </Commands>
                                          
                                        </ext:CommandColumn>

                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:RowSelectionModel SingleSelect="true">
                                        <Listeners>
                                            <RowSelect Handler="
                                                GPUSUARIO.collapse();
                                                PETIQUETA.expand();
                                                HID_USER.setValue(record.data.ID);
                                                HNOMBRE_USER.setValue(record.data.NOMBRE);
                                                PETIQUETA.setTitle('Carnet para asignar a: '+ record.data.NOMBRE + ' ' + record.data.APELLIDO + '(' + record.data.TIPO + ') ' );
                                                parametro.cargarEtiquetasOUT();
                                                parametro.cargarEtiquetasIN(record.data.ID);" />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <Listeners>
                                    <Command Handler="

                                        if(command=='enrollmentFootprint'){
                                           emit('recordfinger', { user:  String(record.data.ID)  });
                                           console.log(record.data.ID);
                                        } 

                                        if(command=='footprint'){
                                            emit('getfinger', { user:  String(record.data.ID)  });
                                            console.log(record.data.ID);
                                        }
                                        " />
                                                        
                                    <Expand Handler="PETIQUETA.collapse();" />
                                </Listeners>

                            </ext:GridPanel>
                            <ext:Panel ID="PETIQUETA" runat="server" Layout="Column" Padding="5" Collapsible="true" Collapsed="false" Title="Carnets" Icon="Cart">
                                <Items>
                                    <ext:ColumnLayout ID="ColumnLayout1" runat="server" FitHeight="true">
                                        <Columns>
                                            <ext:LayoutColumn ColumnWidth="0.5">
                                                <ext:GridPanel ID="GPETIQUETAOUT" runat="server" AutoExpandColumn="CETIQ_DESCRIPCION" ColumnWidth="0.5" Title="Carnets Disponibles" Icon="CartAdd" EnableDragDrop="true" DDGroup="secondGridDDGroup">
                                                    <TopBar>
                                                        <ext:Toolbar runat="server">
                                                            <Items>
                                                                <ext:TextField ID="TFfindEtiquetaOut" runat="server" EmptyText="codigo o etiqueta para buscar" Width="400" EnableKeyEvents="true" Icon="Magnifier">
                                                                    <Listeners>
                                                                        <KeyPress Handler="findEtiquetaOUT(GPETIQUETAOUT.store, TFfindEtiquetaOut.getValue(), Ext.EventObject);" />
                                                                    </Listeners>
                                                                </ext:TextField>
                                                            </Items>
                                                        </ext:Toolbar>
                                                    </TopBar>
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
                                                            <ext:Column ColumnID="CETIQ_ETIQUETA" DataIndex="ETIQ_ETIQUETA" Header="Carnet" />
                                                            <ext:Column ColumnID="CETIQ_DESCRIPCION" DataIndex="ETIQ_DESCRIPCION" Header="Descripción" />
                                                        </Columns>
                                                    </ColumnModel>
                                                    <SelectionModel>
                                                        <ext:RowSelectionModel SingleSelect="true" />
                                                    </SelectionModel>
                                                </ext:GridPanel>
                                            </ext:LayoutColumn>

                                            <ext:LayoutColumn>
                                                <ext:Panel ID="Panel1"
                                                    runat="server"
                                                    Width="35"
                                                    BodyStyle="background-color: transparent;"
                                                    Border="false"
                                                    Layout="Anchor">
                                                    <Items>
                                                        <ext:Panel ID="Panel2" runat="server" Border="false" BodyStyle="background-color: transparent;" AnchorVertical="40%" />
                                                        <ext:Panel ID="Panel3" runat="server" Border="false" BodyStyle="background-color: transparent;" Padding="5">
                                                            <Items>
                                                                <ext:Button ID="Button1" runat="server" Icon="ResultsetNext" StyleSpec="margin-bottom:2px;">
                                                                    <Listeners>

                                                                        <Click Handler=" var records  = GPETIQUETAOUT.selModel.getSelections();
                                                                                             Ext.each(records, function (record) {
                                                                                                parametro.vincularCarnetAlUsuario(record.data.ETIQ_ID, HID_USER.getValue(), {
                                                                                                    success: function (result) {
                                                                                                          
                                                                                                        emit('cardin', { user: String(HID_USER.getValue()), name: String(HNOMBRE_USER.getValue()), card: String(record.data.ETIQ_ETIQUETA) })

                                                                                                        GPETIQUETAOUT.deleteSelected();
                                                                                                        GPETIQUETAIN.store.addSorted(record);  
                                                                                                         Ext.net.Notification.show({
                                                                                                                 html: 'Tarjeta Vinculada exitosamente', title: 'Notificación'
                                                                                                         });
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
                                                                        <ext:ToolTip ID="ToolTip1" runat="server" Title="Add" Html="Add Selected Rows" />
                                                                    </ToolTips>
                                                                </ext:Button>
                                                                <ext:Button ID="Button2" runat="server" Icon="ResultsetPrevious" StyleSpec="margin-bottom:2px;">
                                                                    <Listeners>
                                                                        <Click Handler=" var records = GPETIQUETAIN.selModel.getSelections();
                                                                                            Ext.each(records, function (record) {
                                                                                                parametro.desvincularCarnetAlUsuario(record.data.ETIQ_ID, HID_USER.getValue(), {
                                                                                                    success: function (result) {
                                                                                                        Ext.net.Notification.show({
                                                                                                            html: 'Carnet desvinculado exitosamente', title: 'Notificación'
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
                                                                        <ext:ToolTip ID="ToolTip2" runat="server" Title="Remove" Html="Remove Selected Rows" />
                                                                    </ToolTips>
                                                                </ext:Button>
                                                            </Items>
                                                        </ext:Panel>
                                                    </Items>
                                                </ext:Panel>
                                            </ext:LayoutColumn>
                                            <ext:LayoutColumn>
                                                <ext:GridPanel ID="GPETIQUETAIN" runat="server" AutoExpandColumn="CETUS_MOTIVO" ColumnWidth="0.5" Title="Carnets Asignados" Icon="CartFull" EnableDragDrop="true" DDGroup="firstGridDDGroup">
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
                                                                        <ext:RecordField Name="ETUS_MOTIVO" />
                                                                    </Fields>
                                                                </ext:JsonReader>
                                                            </Reader>
                                                        </ext:Store>
                                                    </Store>
                                                    <ColumnModel>
                                                        <Columns>
                                                            <ext:RowNumbererColumn />
                                                            <ext:Column ColumnID="CETIQ_ETIQUETA" DataIndex="ETIQ_ETIQUETA" Header="Carnet" />
                                                            <ext:Column ColumnID="CETIQ_DESCRIPCION" DataIndex="ETIQ_DESCRIPCION" Header="Descripción" />
                                                            <ext:Column ColumnID="CETUS_MOTIVO" DataIndex="ETUS_MOTIVO" Header="Motivo">
                                                                <Editor>
                                                                    <ext:TextField runat="server" />
                                                                </Editor>
                                                            </ext:Column>
                                                        </Columns>
                                                    </ColumnModel>
                                                    <SelectionModel>
                                                        <ext:RowSelectionModel SingleSelect="true" />
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
