<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Etiqueta.aspx.cs" Inherits="Boot_Park.View.Private.BootPark.Parametrizacion.Etiqueta" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Etiqueta</title>
    <script type="text/javascript">
        var obj = new ActiveXObject("BootParkBiom.PluginBiometrico");

        var afterEdit = function (e) {
            parametro.modificarEtiqueta(e.record.data.ETIQ_ID, e.record.data.ETIQ_TIPO, e.record.data.ETIQ_ETIQUETA, e.record.data.ETIQ_DESCRIPCION, e.record.data.ETIQ_OBSERVACION, e.record.data.ETIQ_ESTADO);
        };

        function ConectarBiometrico() {
            obj.ConectarConTerminal('192.168.1.201', '4370', 'Biometrico');
        }

        function ConectarRFID() {
           
        }

        var focus = function (e) {
            if (CBETIQ_TIPO.getValue() === "CARNET") {
                ConectarBiometrico();
            }
            else {
                ConectarRFID();
            }
        };

        var blur = function (e) {
            if (CBETIQ_TIPO.getValue() === "CARNET") {
                TFETIQ_ETIQUETA.setValue(obj.Tarjeta());
            }
            else if (CBETIQ_TIPO.getValue() === "TAG") {
                TFETIQ_ETIQUETA.setValue("000000000");
            } else {
                alert("CAMPO VACIO");
            }
        };
    </script>
</head>
<body>
    <ext:ResourceManager runat="server" />
    <form id="FETIQUETA" runat="server">
        <div>

            <ext:Viewport ID="VPPRESENTACION" runat="server" Layout="border">
                <Items>
                    <ext:Panel ID="PPRESENTACION" runat="server" Layout="Fit" Region="Center" Padding="5">
                        <Items>
                            <ext:GridPanel ID="GPETIQUETA" runat="server" AutoExpandColumn="CETIQ_DESCRIPCION">
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
                                        <ext:Column ColumnID="CETIQ_TIPO" DataIndex="ETIQ_TIPO" Header="Tipo">
                                            <Editor>
                                                <ext:TextField runat="server" />
                                            </Editor>
                                        </ext:Column>
                                        <ext:Column ColumnID="CETIQ_ETIQUETA" DataIndex="ETIQ_ETIQUETA" Header="Etiqueta">
                                            <Editor>
                                                <ext:TextField runat="server" />
                                            </Editor>
                                        </ext:Column>
                                        <ext:Column ColumnID="CETIQ_DESCRIPCION" DataIndex="ETIQ_DESCRIPCION" Header="Descripción">
                                            <Editor>
                                                <ext:TextField runat="server" />
                                            </Editor>
                                        </ext:Column>
                                        <ext:Column ColumnID="CETIQ_OBSERVACION" DataIndex="ETIQ_OBSERVACION" Header="Observación">
                                            <Editor>
                                                <ext:TextField runat="server" />
                                            </Editor>
                                        </ext:Column>
                                        <ext:Column ColumnID="CETIQ_ESTADO" DataIndex="ETIQ_ESTADO" Header="Estado">
                                            <Editor>
                                                <ext:TextField runat="server" />
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

            <ext:Window ID="WREGISTRO" runat="server" Draggable="false" Resizable="false" Height="400" Width="340" Icon="User" Title="Nueva Etiqueta" Hidden="true" Modal="true" Padding="10" LabelAlign="Top">
                <Items>
                    <ext:ComboBox ID="CBETIQ_TIPO" FieldLabel="Tipo" runat="server" Width="300" EmptyText="Tipo de la etiqueta">
                        <Items>
                            <ext:ListItem Text="Carnet" Value="CARNET" />
                            <ext:ListItem Text="Tag" Value="TAG" />
                        </Items>
                    </ext:ComboBox>
                    <ext:TextField ID="TFETIQ_ETIQUETA" FieldLabel="Etiqueta" runat="server" Width="300" EmptyText="Codigo de la etiqueta">
                        <Listeners>
                            <Focus Fn="focus" />
                            <Blur Fn="blur" />
                        </Listeners>
                    </ext:TextField>
                    <ext:TextArea ID="TFETIQ_DESCRIPCION" FieldLabel="Descripción" runat="server" Width="300" EmptyText="Descripcion de la etiqueta" />
                    <ext:TextArea ID="TFETIQ_OBSERVACION" FieldLabel="Observación" runat="server" Width="300" EmptyText="Observaciones de la etiqueta" />
                    <ext:ComboBox ID="CBESTADO" FieldLabel="Estado" runat="server" Width="300" EmptyText="Estado de la etiqueta">
                        <Items>
                            <ext:ListItem Text="Disponible" Value="DISPONIBLE" />
                            <ext:ListItem Text="Inactivo" Value="INACTIVO" />
                        </Items>
                    </ext:ComboBox>
                </Items>
                <BottomBar>
                    <ext:Toolbar runat="server">
                        <Items>
                            <ext:ToolbarFill />
                            <ext:Button runat="server" Icon="Add" Text="Guardar">
                                <Listeners>
                                    <Click Handler="parametro.crearEtiqueta(CBETIQ_TIPO.getValue(), TFETIQ_ETIQUETA.getValue(), TFETIQ_DESCRIPCION.getValue(), TFETIQ_OBSERVACION.getValue(), CBESTADO.getValue());" />
                                </Listeners>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </BottomBar>
            </ext:Window>
        </div>
    </form>
</body>
</html>
