var kyts = new WebSocket('ws://127.0.0.1:2020') // RFID
var kuos = new WebSocket('ws://127.0.0.1:2012') // Biometrico
var count=0;
kyts.onopen = function (event) {
    kyts.send("connect")
};

kyts.onmessage = function (evt) {
    var kyt
    eval(evt.data)
    if (kyt.type == 'tag') {
        if (count == '0') {
            App.direct.CargarVehiculo(kyt.payload.tag, localStorage.getItem("user"), {
                success: function (res) {
                    App.PVEHICULO.expand();
                    if (res) {
                        App.direct.RegistarCiculación(kyt.payload.tag, localStorage.getItem("user"), {
                            success: function (res) {
                                if (res) {
                                    console.log(kyt.payload.tag)
                                    App.USERID.collapse();
                                    Ext.net.Notification.show({
                                        title: 'Notificación', html: "Solicitud Confirmada"
                                    });
                                } else {
                                    Ext.net.Notification.show({
                                        title: 'Advertencia', html: 'No hemos podido registrar su solicitud en bootpark'
                                    });
                                }
                            }
                        })
                    } else {
                        Ext.net.Notification.show({
                            title: 'Advertencia', html: 'El usuario no esta autorizado para sacar el vehiculo'
                        });
                    }
                }
            })
        }
        count = count + 1;
    }
   
}

kuos.onmessage = function (evt) {
    var kuo
    eval(evt.data)

    if (kuo.type == 'finger' || kuo.type == 'card') {
        App.direct.CargarUsuario(kuo.payload.user, {
            success: function (res) {
                if (res) {
                    count = 0;
                    App.USERID.expand();
                    localStorage.setItem("user", kuo.payload.user);
                    kyts.send("connect")
                    kyts.send("reset")
                    kyts.send("single")
                   
                } else {
                    Ext.net.Notification.show({
                        html: 'El usuario no se encuentra registrado en bootpark, por favor arregle las inconsistencias.', title: 'Advertencia'
                    });
                }
            }
        })
    }
}