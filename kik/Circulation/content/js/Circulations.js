var kyts = new WebSocket('ws://127.0.0.1:2020') // RFID
var kuos = new WebSocket('ws://127.0.0.1:2012') // Biometrico

kyts.onopen = function (event) {
    kyts.send("connect")
};

kyts.onmessage = function (evt) {
    var kyt
    eval(evt.data)

    if (kyt.type == 'tag') {
        App.direct.CargarVehiculo(kyt.payload.tag, localStorage.getItem("user"), {
            success: function (res) {
                if (res) {
                    App.direct.RegistarCiculación(kyt.payload.tag, localStorage.getItem("user"), {
                        success: function (res) {
                            if (res) {
                                console.log(kyt.payload.tag)
                                Ext.Msg.alert('Notificación', "Solicitud Confirmada");
                            } else {
                                Ext.Msg.alert('Advertencia', 'No hemos podido registrar su solicitud en bootpark');
                            }
                        }
                    })
                } else {
                    Ext.Msg.alert('Advertencia', 'El usuario no esta autorizado a sacar el vehiculo');
                }
            }
        })
    } 
}

kuos.onmessage = function (evt) {
    var kuo
    eval(evt.data)
   
    if (kuo.type == 'finger' || kuo.type == 'card') {
        App.direct.CargarUsuario(kuo.payload.user, {
            success: function (res) {
                if (res) {
                    localStorage.setItem("user", kuo.payload.user);
                    kyts.send("connect")
                    kyts.send("reset")
                    kyts.send("single")
                } else {
                    Ext.Msg.alert('Advertencia', 'El usuario no se encuentra registrado en bootpark, por favor arregle las inconsistencias.');
                }
            }
        })
    }
}