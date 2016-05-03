<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Etiqueta.aspx.cs" Inherits="Boot_Park.View.Private.BootPark.Parametrizacion.Etiqueta" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Etiqueta</title>
    <script src="../../../../Content/js/BiometricDevice.js"></script>
    <script type="text/javascript">
  
        var socket = new WebSocket('ws://127.0.0.1:2012');
        var rfid = new WebSocket('ws://127.0.0.1:2020');

        function emit(event) {

            var msg = {
                type: event,
                payload: []
            };
            socket.send(JSON.stringify(msg));
        }

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
                    // console.log(kyt.payload.tag)
                    TFETIQ_ETIQUETA.setValue(String(kyt.payload.tag));
                    break;
            }
        }

        var entry = false;
        socket.onmessage = function (evt) {
            var kuo = evt.data;
            eval(kuo);

            if(entry){
                switch (kuo.type) {
                    case "regcard":
                        TFETIQ_ETIQUETA.setValue(String(kuo.payload.card));
                        entry = false;
                        break;
                }
            }

        };

        var afterEdit = function (e) {
            parametro.modificarEtiqueta(e.record.data.ETIQ_ID, e.record.data.ETIQ_TIPO, e.record.data.ETIQ_ETIQUETA, e.record.data.ETIQ_DESCRIPCION, e.record.data.ETIQ_OBSERVACION, e.record.data.ETIQ_ESTADO);
        };

        var focus = function (e) {
            TFETIQ_ETIQUETA.setValue("");
            if (CBETIQ_TIPO.getValue() === "TAG") {
                BDISCONECT.hide();
                TFETIQ_ETIQUETA.enable()
            } else {
                TFETIQ_ETIQUETA.disable()
                entry = true
                BDISCONECT.show();
            }
        };

        var blur = function (e) {
            if (CBETIQ_TIPO.getValue() === "TAG") {
                rfid.send("single");
            } 
        };
        var findCarnet = function (Store, texto, e) {
            if (e.getKey() == 13) {
                var store = Store,
                    text = texto;
                store.clearFilter();
                if (Ext.isEmpty(text, false)) {
                    return;
                }
                var re = new RegExp(".*" + text + ".*", "i");
                store.filterBy(function (node) {
                    var RESUMEN = node.data.ETIQ_ID + node.data.ETIQ_ETIQUETA + node.data.ETIQ_TIPO + node.data.ETIQ_ESTADO;
                    var a = re.test(RESUMEN);
                    return a;
                });
            }
        };
    </script>
</head>
<body>
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <form id="FETIQUETA" runat="server">
        <div>
            <ext:Viewport ID="VPPRESENTACION" runat="server" Layout="border">
                <Items>
                    <ext:Panel ID="PPRESENTACION" runat="server" Layout="Fit" Region="Center" Padding="5">
                        <Items>
                            <ext:GridPanel ID="GPETIQUETA" runat="server" AutoExpandColumn="CETIQ_DESCRIPCION">
                                  <TopBar>
                                    <ext:Toolbar runat="server">
                                        <Items>
                                            <ext:TextField ID="TFfindCarnet" runat="server" EmptyText="Codigo, tag o estado para buscar" Width="400" EnableKeyEvents="true" Icon="Magnifier">
                                                <Listeners>
                                                    <KeyPress Handler="findCarnet(GPETIQUETA.store, TFfindCarnet.getValue(), Ext.EventObject);" />
                                                </Listeners>
                                            </ext:TextField>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>

                                <Store>
                                    <ext:Store ID="SETIQUETA" runat="server">
                                        <Reader>
                                            <ext:JsonReader>
                                                <Fields>
                                                    <ext:RecordField Name="ETIQ_ID" />
                                                    <ext:RecordField Name="ETIQ_TIPO" />
                                                    <ext:RecordField Name="ETIQ_ETIQUETA" />
                                                    <ext:RecordField Name="ETIQ_DESCRIPCION" />
                                                    <ext:RecordField Name="ETIQ_OBSERVACION" />
                                                    <ext:RecordField Name="ETIQ_ESTADO" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn />
                                        <ext:Column ColumnID="CETIQ_ID" DataIndex="ETIQ_ID" Header="Codigo" Width="80" />
                                        <ext:Column ColumnID="CETIQ_TIPO" DataIndex="ETIQ_TIPO" Header="Tipo" Width="90">

                                            <Editor>
                                                <ext:TextField runat="server" />
                                            </Editor>
                                        </ext:Column>
                                        <ext:Column ColumnID="CETIQ_ETIQUETA" DataIndex="ETIQ_ETIQUETA" Header="Etiqueta" Width="180">
                                            <Editor>
                                                <ext:TextField runat="server" />
                                            </Editor>
                                        </ext:Column>
                                        <ext:Column ColumnID="CETIQ_DESCRIPCION" DataIndex="ETIQ_DESCRIPCION" Header="Descripción" Width="200">
                                            <Editor>
                                                <ext:TextField runat="server" />
                                            </Editor>
                                        </ext:Column>
                                        <ext:Column ColumnID="CETIQ_OBSERVACION" DataIndex="ETIQ_OBSERVACION" Header="Observación" Width="200">
                                            <Editor>
                                                <ext:TextField runat="server" />
                                            </Editor>
                                        </ext:Column>
                                        <ext:Column ColumnID="CETIQ_ESTADO" DataIndex="ETIQ_ESTADO" Header="Estado">
                                            <Editor>
                                                <ext:ComboBox runat="server">
                                                    <Items>
                                                        <ext:ListItem Text="DISPONIBLE" Value="DISPONIBLE" />
                                                        <ext:ListItem Text="EN USO" Value="ENUSO" />
                                                        <ext:ListItem Text="INACTIVO" Value="INACTIVO" />
                                                    </Items>
                                                </ext:ComboBox>
                                            </Editor>
                                        </ext:Column>
                                        <ext:CommandColumn Width="60">
                                            <Commands>
                                                <ext:GridCommand Icon="Delete" CommandName="del">
                                                    <ToolTip Text="Eliminar Etiqueta" />
                                                </ext:GridCommand>
                                            </Commands>
                                        </ext:CommandColumn>
                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" SingleSelect="true" />
                                </SelectionModel>
                                <Listeners>
                                    <Command Handler="
                                        if(command == 'del'){
                                            Ext.Msg.confirm('Confirmación', 'Estas seguro de eliminar la etiqueta?', 
                                            function(btn) {
                                                if (btn === 'yes') {
                                                    parametro.eliminarEtiqueta(record.data.ETIQ_ID, record.data.ETIQ_TIPO);
                                                } 
                                            });
                                        }                                      
                                     " />
                                    <AfterEdit Fn="afterEdit" />
                                </Listeners>
                              
                            </ext:GridPanel>
                        </Items>
                        <BottomBar>
                            <ext:Toolbar runat="server">
                                <Items>
                                    <ext:ToolbarFill />
                                    <ext:Button runat="server" Icon="Add" Text="Nueva Etiqueta">
                                        <Listeners>
                                            <Click Handler="WREGISTRO.show();" />
                                        </Listeners>
                                    </ext:Button>
                                </Items>
                            </ext:Toolbar>
                        </BottomBar>
                    </ext:Panel>
                </Items>
            </ext:Viewport>

            <ext:Window ID="WREGISTRO" runat="server" Draggable="false" Resizable="false" Height="420" Width="350" Icon="User" Title="Nueva Etiqueta" Hidden="true" Modal="true">
                <Items>
                    <ext:FormPanel runat="server" ID="FREGISTRO" Frame="true" Padding="10" LabelAlign="Top">
                        <Items>
                            <ext:ComboBox ID="CBETIQ_TIPO" FieldLabel="Tipo" runat="server" Width="300" EmptyText="Tipo de la etiqueta" ForceSelection="true" AllowBlank="false">
                                <Items>
                                    <ext:ListItem Text="Carnet" Value="CARNET" />
                                    <ext:ListItem Text="Tag" Value="TAG" />
                                </Items>
                                <Listeners>
                                    <Select Fn="focus" />
                                </Listeners>
                               
                            </ext:ComboBox>
                            <ext:TextField ID="TFETIQ_ETIQUETA" FieldLabel="Etiqueta" runat="server" Width="300" EmptyText="Codigo de la etiqueta" AllowBlank="false">
                                <Listeners>
                                    <Blur Fn="blur" />
                                </Listeners>
                            </ext:TextField>
                            <ext:TextArea ID="TFETIQ_DESCRIPCION" FieldLabel="Descripción" runat="server" Width="300" EmptyText="Descripcion de la etiqueta" />
                            <ext:TextArea ID="TFETIQ_OBSERVACION" FieldLabel="Observación" runat="server" Width="300" EmptyText="Observaciones de la etiqueta" />
                            <ext:ComboBox ID="CBESTADO" FieldLabel="Estado" runat="server" Width="300" EmptyText="Estado de la etiqueta" AllowBlank="false">
                                <Items>
                                    <ext:ListItem Text="Disponible" Value="DISPONIBLE" />
                                    <ext:ListItem Text="Inactivo" Value="INACTIVO" />
                                </Items>
                            </ext:ComboBox>
                        </Items>
                    </ext:FormPanel>
                </Items>
                <BottomBar>
                    <ext:Toolbar runat="server">
                        <Items>
                            <ext:Button runat="server" ID="BDISCONECT" Icon="Connect" Text="Reconectar" Hidden="true">
                                <Listeners>
                                    <Click Handler = "emit('connect'); " />
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarFill />
                            <ext:Button runat="server" Icon="Add" Text="Guardar" FormBind="true">
                                <Listeners>
                                    <Click Handler="if(#{FREGISTRO}.getForm().isValid()) { parametro.crearEtiqueta(CBETIQ_TIPO.getValue(), TFETIQ_ETIQUETA.getValue(), TFETIQ_DESCRIPCION.getValue(), TFETIQ_OBSERVACION.getValue(), CBESTADO.getValue()); }else{ return false;} " />
                                </Listeners>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </BottomBar>
                <Listeners>
                    <BeforeHide Handler="FREGISTRO.reset();" />
                </Listeners>
                <Listeners>
                    <Show Handler="rfid.send('connect');" />
                </Listeners>
                <Listeners>
                    <Close Handler="rfid.send('disconnect');" />
                </Listeners>
            </ext:Window>
        </div>

    </form>
</body>
</html>
