﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Validacion.aspx.cs" Inherits="Boot_Park.View.Public.BootPark.Circulacion.Validacion" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../../../../Content/js/BiometricDevice.js"></script>
    <title>Validación Biometrico-RFID</title>
    <script type="text/javascript">
        /*
            For Example:
            //user: El identificador del usuario en el biometrico.
            //type: Define si es Huella o Tarjeta
            DetectarUsuarioBiometrico(function(user, type){
                console.log(user + type);
            });
        */

        //var data = {ip: "192.168.1.201", port: "4370" }
        var _DetectarUsuarioBiometrico = function (data, callback) // 1. Consulta el Usuario en Biometrico
        {
            try {
                var obj = new ActiveXObject("BootParkBiom.PluginBiometrico");
                obj.ConectarConTerminal(data.ip, data.port, 'Biometrico');
            }
            catch (e) {
                console.log('Incompatibilidad con ActiveX');
                callback("Incompatibilidad con ActiveX", "", "");
            }

            callback("", obj.Usuario(), obj.TipoValidacion());
        }

        //var data = {ip: "192.168.1.201", port: "4370" }
        var _ValidarUsuario = function (data, callback) {
            _DetectarUsuarioBiometrico(data, function (err, user, type) {
                if (err != "")
                    callback(err, false);

                VALIDACION.ValidarUsuario(user, type, {
                    success: function (result) {
                        callback("", result);
                    }
                });
            });
        }

        var DetectarTag = function () { }
        var ValidarTag = function () { }
        var RegistrarCirculacion = function () { }

        function main() {

            var data = {
                ip: "192.168.1.201",
                port: "4370"
            }

            _ValidarUsuario(data, function (err, value) {

                if (value) {
                    Ext.Msg.alert('PENDIENTE', "Pendiente por desarrollar.");
                } else {
                    Ext.Msg.alert('Error', err||"El usuario no existe en CHAIRA");
                }
            });
        }
        
        main();

        //"use strict";
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
