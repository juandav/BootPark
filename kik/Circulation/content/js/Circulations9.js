var kyts = new WebSocket('ws://172.16.31.150:2020') // RFID
var kuos = new WebSocket('ws://172.16.31.150:2012') // Biometrico
var socket = io.connect('http://127.0.0.1:2016')

var count=0;
kyts.onopen = function (event) {
    kyts.send("connect")
};

kyts.onmessage = function (evt) {

    setTimeout(function () {
    

    }, 5000);
    var kyt
    eval(evt.data)
    console.log(kyt.payload.tag);
    if (kyt.type == 'tag') {
        if (count == '0') {
            kyts.send("pause")
          
            App.direct.CargarVehiculo(kyt.payload.tag, localStorage.getItem("user"), {
                success: function (res) {
                    if (res) {
                    
                        App.direct.RegistarCiculacion(kyt.payload.tag, localStorage.getItem("user"), {
                            success: function (res) {
                                if (res) {
                                   

                                    socket.emit('click')
                                    setTimeout(function () { socket.emit('click') }, 3000)

                                    App.direct.QueTipoEs({

                                        success: function (data) {

                                            Ext.net.Notification.show({
                                                title: 'Notificación', html: data
                                            });
                                        } // -> esta es la data que me responde del metodo C# QueTipoEs()
                                    })
                                    
                                } else {
                                   
                                    Ext.net.Notification.show({
                                        title: 'Advertencia', html: 'No hemos podido registrar su solicitud en bootpark'
                                    });
                                    App.LESTADO.setText('Esperando Usuario....');
                                }
                               
                            }
                        })
                        
                       
                    } else {
                        Ext.net.Notification.show({
                            title: 'Advertencia', html: 'No esta autorizado para sacar el vehiculo'
                        });
                        App.LESTADO.setText('Esperando Usuario....');
                    }
                }
            })
        }
       
        count = count + 1;
    }
   
}

function callsocket() {
    socket.emit('click');
}

kuos.onmessage = function (evt) {
    var kuo
    eval(evt.data)

    if (kuo.type == 'finger' || kuo.type == 'card') {
        App.direct.CargarUsuario(kuo.payload.user, {
            success: function (res) {
                if (res==true) {
                    count = 0;
                    localStorage.setItem("user", kuo.payload.user);
                    kyts.send("connect")
                    kyts.send("reset")
                    //kyts.send("pause")
                    kyts.send("start")
                    //kyts.send("single")
                   
                } else {
                    Ext.net.Notification.show({
                        html: 'El usuario no se encuentra registrado en bootpark, por favor arregle las inconsistencias.', title: 'Advertencia'
                    });
                }
            }
        })
    }
}

function cerrarpuerta() {
    socket.emit('click');
}