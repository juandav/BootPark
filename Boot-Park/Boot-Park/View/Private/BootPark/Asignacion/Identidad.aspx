<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Identidad.aspx.cs" Inherits="Boot_Park.View.Private.BootPark.Asignacion.Identidad" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>
<%--<embed type="application/x-my-extension" id="pluginId">--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<%--    <script>
  var plugin = document.getElementById("pluginId");
  var result = plugin.myPluginMethod();  // call a method in your plugin
  console.log("my plugin returned: " + result);
</script>--%>
<head runat="server">
    <link href="../../../../Content/css/desktop.css" rel="stylesheet" />
    <script src="../../../../Content/js/BiometricDevice.js"></script>
    <title>Identidad</title>

    <script type="text/javascript">
        try {
            var obj = new ActiveXObject("BootParkBiom.PluginBiometrico");
            obj.ConectarConTerminal('192.168.1.201', '4370', 'Biometrico');
        }
        catch (e) {
            console.log('Incompatibilidad con ActiveX');
        }
        function registrarHuella(idusuario) {
            var r = obj.RecuperarHuella(idusuario);
            if (typeof r === 'undefined') {
                Ext.net.Notification.show({
                    html: 'Huella no registrada en el dispositivo!!',
                    title: 'Notificación'
                });
            } else {
                parametro.registraHuellausuario(r, idusuario);
            }
        }

        var afterEdit = function (e) {
            var dataUser = GPUSUARIO.selModel.getSelections();
            parametro.modificarCarnetUsuario(e.record.data.ETIQ_ID, dataUser[0].data.ID,e.record.data.ETUS_MOTIVO);
        };

        var prepareCommand = function (grid, command, record, row) {
            if (command.command == 'footprint' && record.get("HUEL_ESTADO") == 'EXISTE') {
                command.hidden = true;
                command.hideMode = 'display'; //you can try 'visibility' also     
            }

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
                parametro.desvincularCarnetAlUsuario(record.data.ETIQ_ID, dataUser[0].data.ID, '', {
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
                parametro.vincularCarnetAlUsuario(record.data.ETIQ_ID, dataUser[0].data.ID, {
                    success: function (result) {
                        obj.RegistrarCarnet(record.data.ETIQ_ETIQUETA, dataUser[0].data.NOMBRE, dataUser[0].data.ID);
                        addRow(STIQUETAIN, record, ddSource);
                    },
                    failure: function (errorMsg) {
                        
                    }
                });
            });

            return true;
        };

    </script>
</head>
<body>

    <ext:ResourceManager runat="server" />
    <form id="FIDENTIDAD" runat="server">
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
                                        <ext:CommandColumn Width="60" DataIndex="HUEL_ESTADO" Header="Huella">
                                            <Commands>
                                                 <ext:GridCommand IconCls=".shortcut-icon-enrollment-Footprint icon-enrollment-Footprint" CommandName="enrollmentFootprint">
                                                    <ToolTip Text="incripcion de la Huella" />
                                                </ext:GridCommand>
                                                 <ext:CommandSeparator />
                                                    <ext:GridCommand IconCls="shortcut-icon-footprint icon-footprint" CommandName="footprint">
                                                    <ToolTip Text="Registrar la huella vinculado al Chaira." />
                                                </ext:GridCommand>
                                            </Commands>
                                            <PrepareToolbar Fn="prepareCommand" />
                                        </ext:CommandColumn>
                                        <ext:CommandColumn Width="60">
                                            <Commands>
                                                <ext:GridCommand Icon="ApplicationViewDetail" CommandName="Detalle">
                                                    <ToolTip Text="Información adicional del usuario vinculado al CHAIRA." />
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
                                                parametro.cargarEtiquetasOUT();
                                                parametro.cargarEtiquetasIN(record.data.ID);" />
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <Listeners>
                                          <Command Handler="if(command=='Detalle'){WDETALLEUSUARIO.show();}if(command=='enrollmentFootprint'){ obj.CapturarHuella(record.data.ID);} if(command=='footprint'){registrarHuella(record.data.ID);}" />          
                                    <Expand Handler="PETIQUETA.collapse();" />
                                </Listeners>

                            </ext:GridPanel>
                            <ext:Panel ID="PETIQUETA" runat="server" Layout="Column" Padding="5" Collapsible="true" Collapsed="false" Title="Carnets" Icon="Cart">
                                <Items>
                                    <ext:GridPanel ID="GPETIQUETAOUT" runat="server" AutoExpandColumn="CETIQ_DESCRIPCION" ColumnWidth="0.5" Title="Carnets Disponibles" Icon="CartAdd" EnableDragDrop="true" DDGroup="secondGridDDGroup">
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
                                    <ext:GridPanel ID="GPETIQUETAIN" runat="server" AutoExpandColumn="CETUS_MOTIVO" ColumnWidth="0.5" Title="Carnets Asignados" Icon="CartFull" EnableDragDrop="true" DDGroup="firstGridDDGroup">
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
                                                <ext:Column ColumnID="CETUS_MOTIVO" DataIndex="ETUS_MOTIVO" Header="Motivo">
                                                    <Editor>
                                                        <ext:TextField runat="server"/>
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
                                </Items>
                            </ext:Panel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Viewport>

            <ext:Window ID="WDETALLEUSUARIO" runat="server" Modal="true" Hidden="true" Icon="User" Title="Información del Usuario" Width="800" Height="400">
                <Items>
                </Items>
            </ext:Window>
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
