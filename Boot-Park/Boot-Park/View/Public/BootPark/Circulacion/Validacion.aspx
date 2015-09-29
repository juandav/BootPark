<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Validacion.aspx.cs" Inherits="Boot_Park.View.Public.BootPark.Circulacion.Validacion" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../../../../Content/js/BiometricDevice.js"></script>
    <title>Validación Biometrico-RFID</title>
    <script type="text/javascript">
        
        try {
            var obj = new ActiveXObject("BootParkBiom.PluginBiometrico");
            obj.ConectarConTerminal('192.168.1.201', '4370', 'Biometrico');
        }
        catch (e) {
            console.log('Incompatibilidad con ActiveX');
        }

        function ValidadoDato() {
            alert('ID usuario: ' +  obj.Usuario() + '   Tipo Validacion:' + obj.TipoValidacion());
    
        } 


        "use strict";
        // 1. Se crea la clase circulación
        //class Circulacion{
        //    constructor() {
                
        //        console.log("Metodo que iniciar la conexion de los dispositivos");

        //    }

        //    //1. Detectar la Huella en el dispositivo biometrico
        //    detectarHuella() {
        //        alert('entro');
        //    }

        //    //2. Validar la Huella en la base de datos
        //    validarHuella() {

        //    }//y devuelve el pege, junto a la lista de vehiculos

        //    // 3. Detectar el tag del vehiculo
        //    detectarTag() {

        //    } //Devuelve el tag leido del RFID

        //    // 4. Validar el tag del vehiculo con los traidos en el arreglo de vehiculos de la BD.
        //    validarTag() {

        //    }

        //    // 5. Verificar si el usuario tiene autorización con el vehiculo
        //    verificarAutorizacion() {

        //    }

        //    // 6. Verificar si la terminal de lectura biometrica es de entrada o salida.
        //    verificarTerminal() {

        //    }

        //    // 7. Registrar la circulacion del vehiculo de entrada o de salida segun sea el caso.
        //    registrarCirculacion() {

        //    }
        //}

        //var deteccion = new Circulacion();
        
    </script>
</head>
<body>
    <ext:ResourceManager runat="server" />
    <form id="FCIRCULACION" runat="server">
        <ext:Viewport ID="VPCIRCULACION" runat="server" Layout="border">
            <Items>
                <ext:Panel runat="server" Layout="Fit" Region="Center">
                    <Items>
                        <%-- 1. Primera vista de presentación para la validación --%>
                        <%--queda esperando que el usuario ingrese la huella.--%>
                        <ext:Panel ID="PPRESENTACION" runat="server" Collapsible="true" Layout="Fit" Title="Iniciando Validación" Icon="Application">
                            <Items>
                                <ext:Image runat="server" ImageUrl="../../../../Content/Images/desktop.jpg">
                                    <Listeners>
                                        <Click Handler="ValidadoDato();" />
                                    </Listeners>
                                    <LoadMask />
                                </ext:Image>
                            </Items>
                        </ext:Panel>
                        <%-- 2. Segunda vista para la información del que ingresa o sale de la institución  --%>
                        <ext:Panel ID="PINFORMACION" runat="server" Collapsible="true" Collapsed="true" Layout="Fit" Title="Info. Usuario" Padding="5" Icon="User">
                            <Items>
                            </Items>
                        </ext:Panel>
                        <%-- 3. Muestra al usuario las gracias por confiar en la institución, esta pantalla es netamente innecesaria, sirve solo para adornar--%>
                        <ext:Panel ID="PDESPEDIDA" runat="server" Collapsible="true" Collapsed="true" Layout="Fit" Title="BootPark" Icon="ApplicationDelete">
                            <Items>
                                <ext:Image runat="server" ImageUrl="../../../../Content/Images/desktop.jpg" />
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
