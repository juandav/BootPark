<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Vehiculo.aspx.cs" Inherits="Boot_Park.View.Private.BootPark.Parametrizacion.Vehiculo" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vehiculo</title>
    <script type="text/javascript">
        var afterEdit = function (e) {
            parametro.modificarVehiculo(e.record.data.VEHI_ID, e.record.data.VEHI_OBSERVACION, e.record.data.VEHI_PLACA, e.record.data.VEHI_MODELO, e.record.data.VEHI_MARCA, e.record.data.VEHI_COLOR);
        };
      
    </script>
</head>
<body>
    <ext:ResourceManager runat="server" />
    <ext:Hidden ID="Hidden1" runat="server" />
    <form id="FVEHICULO" runat="server">
        <div>
            <ext:Viewport ID="VPPRESENTACION" runat="server" Layout="border">
                <Items>
                    <ext:Panel ID="PPRESENTACION" runat="server" Layout="Fit" Region="Center" Padding="5">
                        <Items>
                            <ext:GridPanel ID="GPVEHICULO" runat="server" AutoExpandColumn="CVEHI_OBSERVACION">
                                <Store>
                                    <ext:Store ID="SVEHICULO" runat="server">
                                        <Reader>
                                            <ext:JsonReader>
                                                <Fields>
                                                    <ext:RecordField Name="VEHI_ID" />
                                                    <ext:RecordField Name="VEHI_OBSERVACION" />
                                                    <ext:RecordField Name="VEHI_PLACA" />
                                                    <ext:RecordField Name="VEHI_MODELO" />
                                                    <ext:RecordField Name="VEHI_MARCA" />
                                                    <ext:RecordField Name="VEHI_COLOR" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn />
                                        <ext:Column ColumnID="CVEHI_PLACA" DataIndex="VEHI_PLACA" Header="Placa">
                                            <Editor>
                                                <ext:TextField runat="server" />
                                            </Editor>
                                        </ext:Column>
                                        <ext:Column ColumnID="CVEHI_MODELO" DataIndex="VEHI_MODELO" Header="Modelo">
                                            <Editor>
                                                <ext:SpinnerField runat="server" />
                                            </Editor>
                                        </ext:Column>
                                        <ext:Column ColumnID="CVEHI_MARCA" DataIndex="VEHI_MARCA" Header="Marca">
                                            <Editor>
                                                <ext:TextField runat="server" />
                                            </Editor>
                                        </ext:Column>
                                        <ext:Column ColumnID="CVEHI_COLOR" DataIndex="VEHI_COLOR" Header="Color">
                                            <Editor>
                                                <ext:TextField runat="server" />
                                            </Editor>
                                        </ext:Column>
                                        <ext:Column ColumnID="CVEHI_OBSERVACION" DataIndex="VEHI_OBSERVACION" Header="Observaciones">
                                            <Editor>
                                                <ext:TextField runat="server" />
                                            </Editor>
                                        </ext:Column>
                                        <ext:CommandColumn Width="60">
                                            <Commands>
                                                <ext:GridCommand Icon="Delete" CommandName="del">
                                                    <ToolTip Text="Eliminar vehiculo" />
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
                                            Ext.Msg.confirm('Confirmación', 'Estas seguro de eliminar el vehiculo?', 
                                            function(btn) {
                                                if (btn === 'yes') {
                                                    parametro.eliminarVehiculo(record.data.VEHI_ID);
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
                                    <ext:Button runat="server" Icon="Add" Text="Nuevo Vehiculo">
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

            <ext:Window ID="WREGISTRO" runat="server" Draggable="false" Height="400" Width="345" Icon="CarAdd" Title="Nuevo Vehiculo" Hidden="true" >
                <Items>
                    <ext:FormPanel runat="server" ID="FREGISTRO" Frame="true" Padding="10" LabelAlign="Top">
                        <Items>
                            <ext:TextField ID="TFVEHI_PLACA" runat="server" Width="300" FieldLabel="Placa" AllowBlank="false" />
                            <ext:SpinnerField ID="SFVEHI_MODELO" runat="server" Width="300" FieldLabel="Modelo" AllowBlank="false" />
                            <ext:ComboBox ID="TFVEHI_MARCA"  runat="server" Width="300" ValueField="MAR_MARCA"  DisplayField="MAR_MARCA" FieldLabel="Marca" AllowBlank="false">
                               <Store>
                                   <ext:Store ID="SMARCA" runat="server">
                                       <Reader>
                                           <ext:JsonReader>
                                               <Fields>
                                                   <ext:RecordField Name="MAR_ID" />
                                                   <ext:RecordField Name="MAR_MARCA" Type="String" />
                                               </Fields>
                                           </ext:JsonReader>
                                       </Reader>
                                   </ext:Store>
                               </Store>
                            </ext:ComboBox>
                            <ext:TextField ID="TFVEHI_COLOR" runat="server" Width="300" FieldLabel="Color" AllowBlank="false" />
                            <ext:TextArea ID="TAVEHI_OBSERVACION" runat="server" Width="300" FieldLabel="Observaciones" Height="90" />
                        </Items>
                    </ext:FormPanel>
                </Items>
                <BottomBar>
                    <ext:Toolbar runat="server">
                        <Items>
                            <ext:ToolbarFill />
                            <ext:Button runat="server" Icon="Add" Text="Guardar">
                                <Listeners>
                                    <Click Handler=" if(#{FREGISTRO}.getForm().isValid()) { parametro.crearVehiculo(TAVEHI_OBSERVACION.getValue(), TFVEHI_PLACA.getValue(), SFVEHI_MODELO.getValue(), TFVEHI_MARCA.getValue(), TFVEHI_COLOR.getValue()); }else{ return false;}" />
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
</body>
</html>
