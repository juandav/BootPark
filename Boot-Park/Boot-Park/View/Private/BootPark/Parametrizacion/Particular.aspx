<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Particular.aspx.cs" Inherits="Boot_Park.View.Private.BootPark.Parametrizacion.Particular" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Particular</title>
    <script type="text/javascript">
        var afterEdit = function (e) {
            parametro.modificarParticular(e.record.data.PART_ID, e.record.data.PART_IDENTIFICACION, e.record.data.PART_NOMBRE, e.record.data.PART_APELLIDO);
        };
    </script>
</head>
<body>
    <ext:ResourceManager runat="server" />
    <form id="FPARTICULAR" runat="server">
        <div>
            <ext:Viewport ID="VPPRESENTACION" runat="server" Layout="border">
                <Items>
                    <ext:Panel ID="PPRESENTACION" runat="server" Layout="Fit" Region="Center" Padding="5">
                        <Items>
                            <ext:GridPanel ID="GPPARTICULAR" runat="server" AutoExpandColumn="PART_APELLIDO">
                                <Store>
                                    <ext:Store ID="SPARTICULAR" runat="server">
                                        <Reader>
                                            <ext:JsonReader>
                                                <Fields>
                                                    <ext:RecordField Name="PART_ID" />
                                                    <ext:RecordField Name="PART_IDENTIFICACION" />
                                                    <ext:RecordField Name="PART_NOMBRE" />
                                                    <ext:RecordField Name="PART_APELLIDO" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn />
                                        <ext:Column ColumnID="CPART_IDENTIFICACION" DataIndex="PART_IDENTIFICACION" Header="Identificación">
                                            <Editor>
                                                <ext:SpinnerField runat="server" />
                                            </Editor>
                                        </ext:Column>
                                        <ext:Column ColumnID="CPART_NOMBRE" DataIndex="PART_NOMBRE" Header="Nombre" Width="300">
                                            <Editor>
                                                <ext:TextField runat="server" />
                                            </Editor>
                                        </ext:Column>
                                        <ext:Column ColumnID="CPART_APELLIDO" DataIndex="PART_APELLIDO" Header="Apellido">
                                            <Editor>
                                                <ext:TextField runat="server" />
                                            </Editor>
                                        </ext:Column>
                                        <ext:CommandColumn Width="60">
                                            <Commands>
                                                <ext:GridCommand Icon="Delete" CommandName="del">
                                                    <ToolTip Text="Eliminar particular" />
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
                                            Ext.Msg.confirm('Confirmación', 'Estas seguro de eliminar el particular?', 
                                            function(btn) {
                                                if (btn === 'yes') {
                                                    parametro.eliminarParticular(record.data.PART_ID);
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
                                    <ext:Button runat="server" Icon="Add" Text="Nuevo Particular">
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

            <ext:Window ID="WREGISTRO" runat="server" Draggable="false" Height="400" Width="400" Icon="User" Title="Nuevo Particular" Hidden="true" Padding="10">
                <Items>
                    <ext:SpinnerField ID="SPIDENTIFICACION" runat="server" FieldLabel="Identificación" />
                    <ext:TextField ID="TFNOMBRE" runat="server" FieldLabel="Nombres" />
                    <ext:TextField ID="TFAPELLIDO" runat="server" FieldLabel="Apellidos" />
                </Items>
                <BottomBar>
                    <ext:Toolbar runat="server">
                        <Items>
                            <ext:ToolbarFill />
                            <ext:Button runat="server" Icon="Add" Text="Guardar">
                                <Listeners>
                                    <Click Handler="parametro.crearParticular(SPIDENTIFICACION.getValue(), TFNOMBRE.getValue(), TFAPELLIDO.getValue());" />
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
