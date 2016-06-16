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
        //-----------------------------------------------------------------DROP VEHICULOS------------------------------------------------
        var notifyDrop1 = function (ddSource, e, data) {
            // Loop through the selections
            Ext.each(ddSource.dragData.selections, function (record) {
                parametro.desvincularVehiculoAlUsuario(record.data.VEHI_ID, HID_USER.getValue(), {
                    success: function (result) {
                        if (result == false) {
                            Ext.net.Notification.show({
                                html: 'Existe usuarios terceros vinculados a este vehiculo', title: 'Notificación'
                            });
                        } else {
                            Ext.net.Notification.show({
                                html: 'vehiculo desautorizado exitosamente', title: 'Notificación'
                            });
                            addRow(SVEHICULOOUT, record, ddSource);
                        }
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

        //-----------------------------------------------------------------DROP USUARIOS------------------------------------------------

        var addRowUser = function (store, record, ddSource) {
            // Search for duplicates
            var foundItem = store.findExact('ID', record.data.ID);

            // if not found
            if (foundItem == -1) {
                //Remove Record from the source
                ddSource.grid.store.remove(record);

                store.add(record);

                // Call a sort dynamically
                store.sort('ID', 'ASC');
            }
            return foundItem;
        };

        var notifyDropUser1 = function (ddSource, e, data) {
            debugger;
            // Loop through the selections

            Ext.each(ddSource.dragData.selections, function (record) {
                console.log(record);
                parametro.desvincularVehiculoAlParticular(HIDVEHICULO.getValue(), record.data.ID, {
                    success: function (result) {
                        Ext.net.Notification.show({
                            html: 'vehiculo desautorizado exitosamente', title: 'Notificación'
                        });
                      
                        addRowUser(SUSEROUT, record, ddSource);
                    },
                    failure: function (errorMsg) {
                        Ext.net.Notification.show({
                            html: 'Este vehiculo esta vinculado con particulares!!', title: 'Notificación'
                        });
                    }

                });
            });

            return true;
        };

        var notifyDropUser2 = function (ddSource, e, data) {
            // Loop through the selections
            debugger;
            Ext.each(ddSource.dragData.selections, function (record) {
                console.log(record);
                parametro.vincularVehiculoAlParticular(HIDVEHICULO.getValue(), record.data.ID, {
                    success: function (result) {
                        Ext.net.Notification.show({
                            html: 'vehiculo autorizado exitosamente', title: 'Notificación'
                        });
                        
                        addRowUser(SUSERIN, record, ddSource);
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

        //-----------------------------------------------------FILTRO PROPIETARIO--------------------------------------------------------

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

        //-----------------------------------------------------FILTROS POR VEHICULOS--------------------------------------------------------
        
        var findVehiculoOut = function (Store, texto, e) {
            if (e.getKey() == 13) {
                var store = Store,
                    text = texto;
                store.clearFilter();
                if (Ext.isEmpty(text, false)) {
                    return;
                }
                var re = new RegExp(".*" + text + ".*", "i");
                store.filterBy(function (node) {
                    var RESUMEN = node.data.VEHI_PLACA + node.data.VEHI_MODELO + node.data.VEHI_MARCA;
                    var a = re.test(RESUMEN);
                    return a;
                });
            }
        };
        var findVehiculoIn = function (Store, texto, e) {
            if (e.getKey() == 13) {
                var store = Store,
                    text = texto;
                store.clearFilter();
                if (Ext.isEmpty(text, false)) {
                    return;
                }
                var re = new RegExp(".*" + text + ".*", "i");
                store.filterBy(function (node) {
                    var RESUMEN = node.data.VEHI_PLACA + node.data.VEHI_MODELO + node.data.VEHI_MARCA;
                    var a = re.test(RESUMEN);
                    return a;
                });
            }
        };

        //-----------------------------------------------------FILTROS POR PARTICULAR--------------------------------------------------------

        var findUserOut = function (Store, texto, e) {
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
        var findUserIn = function (Store, texto, e) {
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
        }

    </script>
</head>
<body>
    <ext:ResourceManager runat="server" />
    <ext:Hidden ID="HID_USER" runat="server" />
    <ext:Hidden ID="HID_USERINF" runat="server" />
    <ext:Hidden ID="HTIPO" runat="server" />
    <ext:Hidden ID="HIDVEHICULO" runat="server" />
    <form id="FVEHICULO" runat="server">
        <div>
            <ext:Viewport ID="VPPRINCIPAL" runat="server" Layout="border">
                <Items>
                    <ext:Panel ID="PUSUARIO" runat="server" Layout="Fit" Region="Center">
                        <Items>
                            <ext:GridPanel ID="GPUSUARIO" runat="server" Height="300" Collapsible="True" Split="True"  Title="Usuarios" Icon="User">
                                <TopBar>
                                    <ext:Toolbar runat="server">
                                        <Items>
                                            <ext:TextField ID="TFfindUser" runat="server" EmptyText="identificacion, nombre o apellido para buscar" Width="400" EnableKeyEvents="true" Icon="Magnifier">
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
                                        <ext:Column ColumnID="CAPELLIDO" DataIndex="APELLIDO" Header="Apellido" Width="300" />
                                        <ext:Column ColumnID="CTIPO" DataIndex="TIPOUSUARIO" Header="Tipo" Width="250" />
                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:RowSelectionModel SingleSelect="true">
                                        <Listeners>
                                            <RowSelect Handler="
                                                GPUSUARIO.collapse();
                                                PAUTORIZACION.collapse();
                                                PVEHICULO.expand();
                                                HID_USER.setValue(record.data.ID);
                                                HID_USERINF.setValue(record.data.NOMBRE +' '+ record.data.APELLIDO);
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
                                <BottomBar>
                                    <ext:PagingToolbar runat="server" PageSize="20" />
                                </BottomBar>
                            </ext:GridPanel>
                            <ext:Panel ID="PVEHICULO" runat="server" Layout="Column" Padding="5" Collapsible="true" Collapsed="false" Title="Vehiculos" Icon="Car">
                                <Items>
                                    <ext:ColumnLayout runat="server" FitHeight="true">
                                        <Columns>
                                            <ext:LayoutColumn ColumnWidth="0.5">
                                                <ext:GridPanel ID="GPVEHICULOAOUT" runat="server" AutoExpandColumn="CVEHI_OBSERVACION" ColumnWidth="0.5" Title="Vehiculos Disponibles" Icon="CarAdd" EnableDragDrop="true" DDGroup="secondGridDDGroup">
                                                    <TopBar>
                                                        <ext:Toolbar runat="server">
                                                            <Items>
                                                                <ext:TextField ID="TFfindVehiculoOut" runat="server" EmptyText="Placa, modelo o marca para buscar" Width="400" EnableKeyEvents="true" Icon="Magnifier">
                                                                    <Listeners>
                                                                        <KeyPress Handler="findVehiculoOUT(GPVEHICULOAOUT.store, TFfindVehiculoOut.getValue(), Ext.EventObject);" />
                                                                    </Listeners>
                                                                </ext:TextField>
                                                            </Items>
                                                        </ext:Toolbar>
                                                    </TopBar>
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
                                                    <BottomBar>
                                                        <ext:PagingToolbar runat="server" PageSize="25" />
                                                    </BottomBar>
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

                                                                        <Click Handler=" var records  = GPVEHICULOAOUT.selModel.getSelections();
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
                                                                        <Click Handler=" var records = GPVEHICULOIN.selModel.getSelections();
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
                                                    <TopBar>
                                                        <ext:Toolbar runat="server">
                                                            <Items>
                                                                <ext:TextField ID="TFfindVehiculoIn" runat="server" EmptyText="Placa, modelo o marca para buscar" Width="400" EnableKeyEvents="true" Icon="Magnifier">
                                                                    <Listeners>
                                                                        <KeyPress Handler="findVehiculoIn(GPVEHICULOIN.store, TFfindVehiculoIn.getValue(), Ext.EventObject);" />
                                                                    </Listeners>
                                                                </ext:TextField>
                                                            </Items>
                                                        </ext:Toolbar>
                                                    </TopBar>
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
                                                            <ext:CommandColumn Width="100">
                                                                <Commands>
                                                                    <ext:GridCommand Icon="UserSuit" CommandName="permiso" Text="Permiso">
                                                                    </ext:GridCommand>
                                                                </Commands>
                                                            </ext:CommandColumn>
                                                        </Columns>
                                                    </ColumnModel>
                                                    <SelectionModel>
                                                        <ext:RowSelectionModel SingleSelect="true" />
                                                    </SelectionModel>
                                                    <Listeners>
                                                        <Command Handler="HIDVEHICULO.setValue(record.data.VEHI_ID); PVEHICULO.collapse(); PAUTORIZACION.setTitle(HID_USERINF.getValue() + ' AUTORIZA EL  VEHICULO ' + record.data.VEHI_PLACA + '(' + record.data.VEHI_MARCA + ')' ); parametro.cargarusuarioOut(record.data.VEHI_ID); parametro.cargarusuarioIn(record.data.VEHI_ID);PAUTORIZACION.expand();" />
                                                    </Listeners>
                                                    <BottomBar>
                                                        <ext:PagingToolbar runat="server" PageSize="15" />
                                                    </BottomBar>

                                                </ext:GridPanel>
                                            </ext:LayoutColumn>
                                        </Columns>
                                    </ext:ColumnLayout>
                                </Items>
                            </ext:Panel>
                            <ext:Panel ID="PAUTORIZACION" runat="server" Layout="Column" Padding="5" Collapsible="true" Collapsed="true" Title="AUTORIZACION TERCEROS" Icon="UserAdd">
                                <Items>
                                    <ext:ColumnLayout runat="server" FitHeight="false">
                                        <Columns>
                                            <ext:LayoutColumn ColumnWidth="0.5">
                                                <ext:GridPanel ID="GUSUARIOOUT" runat="server" Title="Usuarios Disponibles" Icon="UserAdd" EnableDragDrop="true" DDGroup="firstGridDUserGroup" Height="300"> 
                                                    <TopBar>
                                                        <ext:Toolbar runat="server">
                                                            <Items>
                                                                <ext:TextField ID="TFINDUSEPRIN" runat="server" EmptyText="identificacion, nombre o apellido para buscar" Width="400" EnableKeyEvents="true">
                                                                    <Listeners>
                                                                        <KeyPress Handler="findUserOut(GUSUARIOOUT.store, TFINDUSEPRIN.getValue(), Ext.EventObject);" />
                                                                    </Listeners>
                                                                </ext:TextField>
                                                            </Items>
                                                        </ext:Toolbar>
                                                    </TopBar>
                                                    <Store>
                                                        <ext:Store ID="SUSEROUT" runat="server">
                                                            <Reader>
                                                                <ext:JsonReader>
                                                                    <Fields>
                                                                        <ext:RecordField Name="ID" />
                                                                        <ext:RecordField Name="IDENT" />
                                                                        <ext:RecordField Name="NOMBRE" />
                                                                        <ext:RecordField Name="APELLIDO" />
                                                                        <ext:RecordField Name="TIPO" />
                                                                    </Fields>
                                                                </ext:JsonReader>
                                                            </Reader>
                                                        </ext:Store>
                                                    </Store>
                                                    <ColumnModel>
                                                        <Columns>
                                                            <ext:RowNumbererColumn />
                                                            <ext:Column ColumnID="CIDENT" DataIndex="IDENT" Header="Identificación" />
                                                            <ext:Column ColumnID="CNOMBRE" DataIndex="NOMBRE" Header="Nombre" Width="130" />
                                                            <ext:Column ColumnID="CAPELLIDO" DataIndex="APELLIDO" Header="Apellido" Width="150" />
                                                            <ext:Column ColumnID="CTIPO" DataIndex="TIPO" Header="Tipo" Width="150" />
                                                        </Columns>
                                                    </ColumnModel>
                                                    <SelectionModel>
                                                        <ext:RowSelectionModel SingleSelect="true">
                                                        </ext:RowSelectionModel>
                                                    </SelectionModel>
                                                    <Listeners>
                                                    </Listeners>
                                                    <BottomBar>
                                                        <ext:PagingToolbar runat="server" PageSize="15" />
                                                    </BottomBar>
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

                                                                         <Click Handler = " var records  = GUSUARIOOUT.selModel.getSelections();
                                                                                            Ext.each(records, function (record) {
                                                                                                parametro.vincularVehiculoAlParticular(HIDVEHICULO.getValue(), records[0].data.ID, {
                                                                                                    success: function (result) {
                                                                                                        Ext.net.Notification.show({
                                                                                                            html: 'vehiculo autorizado exitosamente', title: 'Notificación'
                                                                                                        });
                                                                                                        GUSUARIOOUT.deleteSelected();
                                                                                                        GUSUARIOIN.store.addSorted(records);  
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
                                                                            <Click Handler = " var records = GUSUARIOIN.selModel.getSelections();
                                                                                           Ext.each(records, function (record) {
                                                                                                parametro.desvincularVehiculoAlParticular(HIDVEHICULO.getValue(),records[0].data.ID, {
                                                                                                    success: function (result) {
                                                                                                        Ext.net.Notification.show({
                                                                                                            html: 'vehiculo desautorizado exitosamente', title: 'Notificación'
                                                                                                        });
                                                                                                        GUSUARIOIN.deleteSelected();
                                                                                                        GUSUARIOOUT.store.addSorted(records); 
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
                                                <ext:GridPanel ID="GUSUARIOIN" runat="server" ColumnWidth="0.5" Title="Usuarios Autorizados" Icon="User" EnableDragDrop="true" DDGroup="secondGridDUserGroup" Height="300">

                                                    <TopBar>
                                                        <ext:Toolbar ID="Toolbar1" runat="server">
                                                            <Items>
                                                                <ext:TextField ID="TFINDUSUARIOIN" runat="server" EmptyText="identificacion, nombre o apellido para buscar" Width="400" EnableKeyEvents="true">
                                                                    <Listeners>
                                                                        <KeyPress Handler="findUserIn(GUSUARIOIN.store, TFINDUSUARIOIN.getValue(), Ext.EventObject);" />
                                                                    </Listeners>
                                                                </ext:TextField>
                                                            </Items>
                                                        </ext:Toolbar>
                                                    </TopBar>
                                                    <Store>
                                                        <ext:Store ID="SUSERIN" runat="server" >
                                                            <Reader>
                                                                <ext:JsonReader>
                                                                    <Fields>
                                                                        <ext:RecordField Name="ID" />
                                                                        <ext:RecordField Name="IDENT" />
                                                                        <ext:RecordField Name="NOMBRE" />
                                                                        <ext:RecordField Name="APELLIDO" />
                                                                        <ext:RecordField Name="TIPO" />
                                                                    </Fields>
                                                                </ext:JsonReader>
                                                            </Reader>
                                                        </ext:Store>
                                                    </Store>
                                                    <ColumnModel>
                                                        <Columns>
                                                            <ext:RowNumbererColumn />
                                                            <ext:Column ColumnID="CIDENT" DataIndex="IDENT" Header="Identificación" />
                                                            <ext:Column ColumnID="CNOMBRE" DataIndex="NOMBRE" Header="Nombre" Width="130" />
                                                            <ext:Column ColumnID="CAPELLIDO" DataIndex="APELLIDO" Header="Apellido" Width="150" />
                                                            <ext:Column ColumnID="CTIPO" DataIndex="TIPO" Header="Tipo" Width="150" />

                                                        </Columns>
                                                    </ColumnModel>
                                                    <SelectionModel>
                                                        <ext:RowSelectionModel SingleSelect="true">
                                                        </ext:RowSelectionModel>
                                                    </SelectionModel>
                                                     <BottomBar>
                                                        <ext:PagingToolbar runat="server" PageSize="25" Visible="true" />
                                                    </BottomBar>
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
          <ext:DropTarget ID="DUSEROUT"  runat="server" Target="={GUSUARIOOUT.view.scroller.dom}" Group="secondGridDUserGroup">
            <NotifyDrop Fn="notifyDropUser1" />
        </ext:DropTarget>

        <ext:DropTarget ID="DUSERIN" runat="server" Target="={GUSUARIOIN.view.scroller.dom}" Group="firstGridDUserGroup">
            <NotifyDrop Fn="notifyDropUser2" />
        </ext:DropTarget>
        <ext:DropTarget runat="server" Target="={GPVEHICULOAOUT.view.scroller.dom}" Group="firstGridDDGroup">
            <NotifyDrop Fn="notifyDrop1" />
        </ext:DropTarget>

        <ext:DropTarget runat="server" Target="={GPVEHICULOIN.view.scroller.dom}" Group="secondGridDDGroup">
            <NotifyDrop Fn="notifyDrop2" />
        </ext:DropTarget>
    </form>
</body>
</html>
