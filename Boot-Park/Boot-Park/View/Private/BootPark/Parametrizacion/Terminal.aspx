<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Terminal.aspx.cs" Inherits="Boot_Park.View.Private.BootPark.Parametrizacion.Terminal" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Terminal</title>
    <script type="text/javascript">
        var afterEdit = function (e) {
            parametro.modificarTerminal(e.record.data.TERM_ID, e.record.data.TERM_PUERTO, e.record.data.TERM_IP, e.record.data.TERM_TIPO);
        };
    </script>
</head>
<body>
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <form id="FTERMINAL" runat="server">
        <div>
            <ext:Viewport ID="VPPRESENTACION" runat="server" Layout="border">
                <Items>
                    <ext:Panel ID="PPRESENTACION" runat="server" Layout="Fit" Region="Center" Padding="5">
                        <Items>
                            <ext:GridPanel ID="GPTERMINAL" runat="server" AutoExpandColumn="TERM_IP">
                                <Store>
                                    <ext:Store ID="STERMINAL" runat="server">
                                        <Reader>
                                            <ext:JsonReader>
                                                <Fields>
                                                    <ext:RecordField Name="TERM_ID" />
                                                    <ext:RecordField Name="TERM_PUERTO" />
                                                    <ext:RecordField Name="TERM_IP" />
                                                    <ext:RecordField Name="TERM_TIPO" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn />
                                        <ext:Column ColumnID="CTERM_TIPO" DataIndex="TERM_TIPO" Header="Tipo">
                                            <Editor>
                                                <ext:TextField runat="server" />
                                            </Editor>
                                        </ext:Column>
                                        <ext:Column ColumnID="CTERM_IP" DataIndex="TERM_IP" Header="Ip">
                                            <Editor>
                                                <ext:TextField runat="server" />
                                            </Editor>
                                        </ext:Column>
                                        <ext:Column ColumnID="CTERM_PUERTO" DataIndex="TERM_PUERTO" Header="Puerto">
                                            <Editor>
                                                <ext:TextField runat="server" />
                                            </Editor>
                                        </ext:Column>

                                        <ext:CommandColumn Width="60">
                                            <Commands>
                                                <ext:GridCommand Icon="Delete" CommandName="del">
                                                    <ToolTip Text="Eliminar Terminal" />
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
                                            Ext.Msg.confirm('Confirmación', 'Estas seguro de eliminar el Terminal?', 
                                            function(btn) {
                                                if (btn === 'yes') {
                                                    parametro.eliminarTerminal(record.data.TERM_ID);
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
                                    <ext:Button runat="server" Icon="Add" Text="Nuevo Terminal">
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

            <ext:Window ID="WREGISTRO" runat="server" Draggable="false" Resizable="false" Height="230" Width="350" Icon="DriveAdd" Title="Nueva Terminal" Hidden="true" Modal="true">
                <Items>
                    <ext:FormPanel runat="server" ID="FREGISTRO" Frame="true" Padding="10" LabelAlign="Top">
                        <Items>
                            <ext:ComboBox ID="CTIPO" FieldLabel="Tipo" runat="server" Width="300" EmptyText="Tipo de terminal" ForceSelection="true" AllowBlank="false">
                                <Items>
                                    <ext:ListItem Text="Biometrico" Value="Biometrico" />
                                    <ext:ListItem Text="Rfid" Value="Rfid" />
                                </Items>
                            </ext:ComboBox>
                            <ext:TextField ID="TTERM_IP" FieldLabel="Ip" runat="server" Width="300" EmptyText="Dirección del dispositivo" AllowBlank="false"  />
                            <ext:TextField ID="TTERM_PUERTO" FieldLabel="Puerto" runat="server" Width="300" EmptyText="Puerto de comunicación" AllowBlank="false" />

                        </Items>
                    </ext:FormPanel>
                </Items>
                <BottomBar>
                    <ext:Toolbar runat="server">
                        <Items>
                            <ext:ToolbarFill />
                            <ext:Button runat="server" Icon="Add" Text="Guardar" FormBind="true">
                                <Listeners>
                                    <Click Handler="if(#{FREGISTRO}.getForm().isValid()) {parametro.crearTerminal(TTERM_PUERTO.getValue(), TTERM_IP.getValue(), CTIPO.getValue());}else{ return false;}  " />
                                </Listeners>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </BottomBar>
                <Listeners>
                    <BeforeHide Handler="FTERMINAL.reset();" />
                </Listeners>
            </ext:Window>
        </div>

    </form>
</body>
</html>
