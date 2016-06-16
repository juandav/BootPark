<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Boot_Park.Login" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>
        Login
    </title>
    <style type="text/css">
        .back {
            background-image: url('Content/images/login.jpg');
            background-repeat: no-repeat;
            background-size: cover;
            background-position: center;
            height: 450px;
        }
    </style>
    <script>
        'use strict'

        let ClearLocalStorage = () => {

            localStorage.removeItem('accountSession')
        }

        let RowSelect = (evt, index, account) => {

            this.data = account.data
            localStorage.setItem('accountSession', JSON.stringify(this.data)) // para entender la data JSON.parse(this.data)
        }

        let KeyPressEvent = (store, data, component) => {

            var re = new RegExp(".*" + data + ".*", "i")
            store.filterBy((node) => {

                let filter = node.data.IDENTIFICACION
                return re.test(filter)
            });
        }

        let Login = () => {
            let creds = JSON.parse(localStorage.getItem('accountSession'))

            if (creds !== null) {
                parametro.direccionarDestop();
                console.log(creds)
            } else {

                Ext.Msg.alert('Warning', 'No ha seleccionado cuenta, para iniciar');
            }
            
        }

        //Main
        ClearLocalStorage()

    </script>
    <meta charset="utf-8"/>
</head>
<body class="back">
    <ext:ResourceManager runat="server" />
    <form runat="server">
        <ext:Window 
            runat="server" 
            ID="wlogin" 
            Width="600" 
            Height="420" 
            Draggable="false" 
            Closable="false" 
            Resizable="false"
            Title="Login"
            Icon="User"
            Padding="5"
            >

            <Items>
                <ext:GridPanel ID="faccount" runat="server" Height="350" AutoExpandColumn="NOMBRE">
                    <Store>
                        <ext:Store runat="server" ID="accountStore">
                            <Reader>
                                <ext:JsonReader>
                                    <Fields>
                                        <ext:RecordField Name="PEGE_ID"/>
                                        <ext:RecordField Name="IDENTIFICACION"/>
                                        <ext:RecordField Name="NOMBRE"/>
                                        <ext:RecordField Name="APELLIDO"/>
                                        <ext:RecordField Name="TIPOUSUARIO"/>
                                    </Fields>
                                </ext:JsonReader>
                            </Reader>
                        </ext:Store>
                    </Store>
                    <ColumnModel>
                        <Columns>
                            <ext:RowNumbererColumn />
                            <ext:Column DataIndex="IDENTIFICACION" ColumnID="identificacion" Header="C.C." />
                            <ext:Column DataIndex="NOMBRE" ColumnID="nombre" Header="Nombre" />
                            <ext:Column DataIndex="APELLIDO" ColumnID="apellido" Header="Apellido" />
                            <ext:Column DataIndex="TIPOUSUARIO" ColumnID="tipo" Header="Función" />
                        </Columns>
                    </ColumnModel>
                    <BottomBar>
                        <ext:PagingToolbar runat="server" PageIndex="1" PageSize="14">
                            <Items>
                                <ext:TextField ID="findId" runat="server" EmptyText="Busqueda por cedula" Width="200" EnableKeyEvents="true" Icon="Magnifier">
                                    <Listeners>
                                        <KeyPress Handler="KeyPressEvent(faccount.store, findId.getValue(), Ext.EventObject)"/>
                                    </Listeners>
                                </ext:TextField>
                            </Items>
                        </ext:PagingToolbar>
                    </BottomBar>
                    <SelectionModel>
                        <ext:RowSelectionModel SingleSelect="true">
                           <Listeners>
                               <RowSelect Fn="RowSelect"/>
                           </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
            <BottomBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:ToolbarFill />
                        <ext:Button runat="server" Text="Login" Icon="Tick">
                            <Listeners>
                                <Click Fn="Login"/>
                            </Listeners>
                        </ext:Button>
                    </Items>
                </ext:Toolbar>
            </BottomBar>
        </ext:Window>
    </form>
</body>
</html>
