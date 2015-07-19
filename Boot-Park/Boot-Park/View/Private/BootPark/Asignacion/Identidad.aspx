<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Identidad.aspx.cs" Inherits="Boot_Park.View.Private.BootPark.Asignacion.Identidad" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Identidad</title>
</head>
<body>
    <ext:ResourceManager runat="server" />
    <form id="FIDENTIDAD" runat="server">
        <div>
            <ext:Viewport ID="VPPRINCIPAL" runat="server" Layout="border">
                <Items>
                    <ext:Panel ID="PUSUARIO" runat="server" Layout="Fit" Region="Center">
                        <Items>
                            <ext:GridPanel ID="GPUSUARIO" runat="server" Height="300" Collapsible="True" Split="True" AutoExpandColumn="CAPELLIDO">
                                <Store>
                                    <ext:Store ID="SUSUARIO" runat="server">
                                        <Reader>
                                            <ext:JsonReader>
                                                <Fields>
                                                    <ext:RecordField Name="ID"/>
                                                    <ext:RecordField Name="IDENT"/>
                                                    <ext:RecordField Name="NOMBRE"/>
                                                    <ext:RecordField Name="APELLIDO"/>
                                                    <ext:RecordField Name="TIPO"/>
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn />
                                        <ext:Column ColumnID="CIDENT" DataIndex="IDENT" Header="Identificación"/>
                                        <ext:Column ColumnID="CNOMBRE" DataIndex="NOMBRE" Header="Nombre" Width="300"/>
                                        <ext:Column ColumnID="CAPELLIDO" DataIndex="APELLIDO" Header="Apellido"/>
                                        <ext:Column ColumnID="CTIPO" DataIndex="TIPO" Header="Tipo"/>
                                        <ext:CommandColumn Width="60">
                                            <Commands>
                                                <ext:GridCommand Icon="ApplicationViewDetail" CommandName="Detalle">
                                                    <ToolTip Text="Información adicional del usuario vinculado al CHAIRA."/>
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
                                                parametro.cargarEtiquetasIN('1');"/>
                                        </Listeners>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <Listeners>
                                    <Command Handler="WDETALLEUSUARIO.show();"/>
                                    <Expand Handler="PETIQUETA.collapse();"/>
                                </Listeners>
                            </ext:GridPanel>
                            <ext:Panel ID="PETIQUETA" runat="server" Layout="Column" Padding="5" Collapsible="true" Collapsed="false">
                                <Items>
                                    <ext:GridPanel ID="GPETIQUETAOUT" runat="server" AutoExpandColumn="CETIQ_OBSERVACION" ColumnWidth="0.5">
                                        <Store>
                                            <ext:Store ID="SETIQUETAOUT" runat="server">
                                                <Reader>
                                                    <ext:JsonReader>
                                                        <Fields>
                                                            <ext:RecordField Name="ETIQ_ID"/>
                                                            <ext:RecordField Name="ETIQ_TIPO"/>
                                                            <ext:RecordField Name="ETIQ_ETIQUETA"/>
                                                            <ext:RecordField Name="ETIQ_OBSERVACION"/>
                                                        </Fields>
                                                    </ext:JsonReader>
                                                </Reader>
                                            </ext:Store>
                                        </Store>
                                        <ColumnModel>
                                            <Columns>
                                                <ext:RowNumbererColumn/>
                                                <ext:Column ColumnID="CETIQ_ETIQUETA" DataIndex="ETIQ_ETIQUETA" Header="Carnet"/>
                                                <ext:Column ColumnID="CETIQ_OBSERVACION" DataIndex="ETIQ_OBSERVACION" Header="Observaciones"/>
                                            </Columns>
                                        </ColumnModel>
                                    </ext:GridPanel>
                                    <ext:GridPanel ID="GPETIQUETAIN" runat="server" AutoExpandColumn="CETIQ_OBSERVACION" ColumnWidth="0.5">
                                         <Store>
                                            <ext:Store ID="STIQUETAIN" runat="server">
                                                <Reader>
                                                    <ext:JsonReader>
                                                        <Fields>
                                                            <ext:RecordField Name="ETIQ_ID"/>
                                                            <ext:RecordField Name="ETIQ_TIPO"/>
                                                            <ext:RecordField Name="ETIQ_ETIQUETA"/>
                                                            <ext:RecordField Name="ETIQ_OBSERVACION"/>
                                                        </Fields>
                                                    </ext:JsonReader>
                                                </Reader>
                                            </ext:Store>
                                        </Store>
                                        <ColumnModel>
                                            <Columns>
                                                <ext:RowNumbererColumn/>
                                                <ext:Column ColumnID="CETIQ_ETIQUETA" DataIndex="ETIQ_ETIQUETA" Header="Carnet"/>
                                                <ext:Column ColumnID="CETIQ_OBSERVACION" DataIndex="ETIQ_OBSERVACION" Header="Observaciones"/>
                                            </Columns>
                                        </ColumnModel>
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
    </form>
</body>
</html>
