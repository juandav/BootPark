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
                    var RESUMEN = node.data.PART_IDENTIFICACION + node.data.PART_NOMBRE + node.data.PART_APELLIDO;
                    var a = re.test(RESUMEN);
                    return a;
                });
            }
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
                                 <TopBar>
                                    <ext:Toolbar runat="server">
                                        <Items>
                                            <ext:TextField ID="TFfindUser" runat="server" EmptyText="Cedula, nombre o apellido para buscar" Width="400" EnableKeyEvents="true" Icon="Magnifier">
                                                <Listeners>
                                                    <KeyPress Handler="findUser(GPPARTICULAR.store, TFfindUser.getValue(), Ext.EventObject);" />
                                                </Listeners>
                                            </ext:TextField>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
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

            <ext:Window ID="WREGISTRO" runat="server" Draggable="false" Height="235" Width="350" Icon="User" Title="Nuevo Particular" Hidden="true" >
                <Items>
                    <ext:FormPanel runat="server" ID="FREGISTRO" Frame="true" Padding="10" LabelAlign="Top">
                        <Items>
                            <ext:TextField ID="SPIDENTIFICACION" runat="server" Width="300" FieldLabel="Identificación"  MaskRe="/[0-9]/" AllowBlank="false" />
                            <ext:TextField ID="TFNOMBRE" runat="server"  Width="300" FieldLabel="Nombres" AllowBlank="false" />
                            <ext:TextField ID="TFAPELLIDO" runat="server"  Width="300" FieldLabel="Apellidos" AllowBlank="false" />
                        </Items>
                    </ext:FormPanel>
                </Items>
                <BottomBar>
                    <ext:Toolbar runat="server">
                        <Items>
                            <ext:ToolbarFill />
                            <ext:Button runat="server" Icon="Add" Text="Guardar" FormBind="true">
                                <Listeners>
                                    <Click Handler="if(#{FREGISTRO}.getForm().isValid()) { parametro.crearParticular(SPIDENTIFICACION.getValue(), TFNOMBRE.getValue(), TFAPELLIDO.getValue()); }else{ return false;} " />
                                </Listeners>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </BottomBar>
                <Listeners>
                    <BeforeHide Handler="FREGISTRO.reset();" />
                </Listeners>
            </ext:Window>
        </div>
    </form>
     <script src="https://code.jquery.com/jquery-2.2.1.js" ></script>
	<script src="https://cdnjs.cloudflare.com/ajax/libs/scannerdetection/1.2.0/jquery.scannerdetection.min.js"></script>
	<script src="../../../../Content/js/NUIP_Parser.js"></script>
</body>
</html>
