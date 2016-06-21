<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HistorialControl.aspx.cs" Inherits="Boot_Park.View.Private.BootPark.Report.View.HistorialControl" %>

<%@ Import Namespace="System.Collections.Generic"%>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lista Vehiculos Parqueadero</title>
    
    <style type="text/css">
        body  {
            font : normal 11px tahoma, arial, helvetica, sans-serif;
        }
        
        #customers-ct table { width : 100% !important; }
        
        #customers-ct th {
            background : #F0F4F5 url(/extjs/resources/images/default/toolbar/bg-gif/ext.axd) repeat-x scroll left top;
            font-weight : bold;
            padding : 8px 5px;
        }
       
        #customers-ct td {
            padding : 5px;
            border-bottom : 1px solid silver;
        }
        
        #customers-ct .letter-row {
            padding-top : 15px;
            border : none;
        }
        
        #customers-ct .letter-row h2 { font-size : 2em; }
        
        #customers-ct .header { padding : 10px 0px 10px 5px; }
        
        #customers-ct .header p { font-size : 2em; }

        #customers-ct .header a { margin-bottom : 10px; }
        
        .cust-name-over {
            cursor : pointer;
            background-color : #efefef;
        }
        
        #customers-ct .letter-row div {
            border-bottom : 2px solid #99bbe8;
            cursor : pointer;
            background : transparent url(/extjs/resources/images/default/grid/group-expand-sprite-gif/ext.axd) no-repeat 0px -42px;
            margin-bottom : 5px;
        }

        #customers-ct .letter-row div h2  { padding-left : 18px; }

        #customers-ct .letter-row div.collapsed { background-position : 0px 8px; }
        
        #customers-ct tr.collapsed { display : none; }
        
        .customer-label {
            font-weight : bold;
            font-size : 12px;
            padding : 5px 0px 5px 28px;            
            width : 150px;
        }
    </style>
    
    <script type="text/javascript">
        var viewClick = function (dv, e) {
            var group = e.getTarget("h2.letter-selector");
            
            if (group) {
                Ext.fly(group).up("div").toggleClass("collapsed");
                Ext.select("#customers-ct tr.l-" + group.innerHTML).toggleClass("collapsed");
            }
        };
        var nodeClick = function (view, index, node, e) {
            var nd = Ext.fly(node).first("td");

            DataViewContextMenu.node = {
                id: nd.getAttributeNS("", "VEHICULO"),
                contact: nd.dom.innerHTML
            };

            DataViewContextMenu.showAt(e.getXY());
        };
       
    </script>
</head>
<body>
    <ext:ResourceManager runat="server" />
    
      <ext:Store ID="dsReport" runat="server">
        <Reader>
            <ext:JsonReader>
                <Fields>
                    <ext:RecordField Name="Letter" />
                    <ext:RecordField Name="Customers" IsComplex="true" />
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>
    
    <ext:Toolbar runat="server">
        <Items>
            <ext:Button runat="server" Text="Print" Icon="Printer" OnClientClick="window.print();" />
        </Items>
    </ext:Toolbar>
    
    <ext:DataView   runat="server" 
        StoreID="dsReport"
        
        SingleSelect="true"
        ItemSelector="tr.customer-record" 
        OverClass="cust-name-over"
        EmptyText="No customers to display">
        <Template ID="Template1" runat="server">
            <Html>
				<div id="customers-ct">
					<div class="header">
						<p>Listado Vehiculos Registrados</p>                                                                        
					</div>
					<table>
						<tr>
							<th>Tipo</th>
							<th>Usuario</th>
                            <th>Fecha</th>
							<th>Hora</th>
                            <th>Tipo</th>
                            <th>Usuario</th>
                            <th>Fecha</th>
							<th>Hora</th>
                            <th>Duración</th>
						</tr>
					
						<tpl for=".">
								<tr>
									<td class="letter-row" colspan="8">
										<div><h2 class="letter-selector">{Letter}</h2></div>
									</td>
								</tr>
								<tpl for="Customers">
									<tr class="customer-record l-{parent.Letter}">
										<td>&nbsp;{ENTRADA}</td>
                                        <td>&nbsp;{USUARIOENTRADA}</td>
										<td>&nbsp;{FECHAINI}</td>
										<td>&nbsp;{HORAINI}</td>
										<td>&nbsp;{SALIDA}</td>
                                        <td>&nbsp;{USUARIOSALIDA}</td>
										<td>&nbsp;{FECHASAL}</td>
										<td>&nbsp;{HORAFIN}</td>
                                        <td>&nbsp;{TIEMPO}</td>
									</tr>
								</tpl>
						</tpl>                    
					</table>
				</div>
			</Html>
        </Template>
        <Listeners>
            <ContainerClick Fn="viewClick" />
        </Listeners>
    </ext:DataView>
</body>
</html>