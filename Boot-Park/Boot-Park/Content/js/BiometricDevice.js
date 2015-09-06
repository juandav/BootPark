

function ConectarBiometrico() {
    try {
        if (obj.ConectarConTerminal('192.168.1.201', '4370', 'Biometrico')) {
            Ext.net.Notification.show({
                html: 'Conectado correctamente',
                title: 'Notificación'
            });
        } else {
            Ext.net.Notification.show({
                html: 'No conectado!, Asegurece que el Dispositivo este conectado a la red TCP/IP',
                title: 'Notificación'
            });
        }
    } catch (e) {
        Ext.net.Notification.show({
            html: 'Browser Imcompatible con ActiveX',
            title: 'Notificación'
        });
    }
}
function CapturarHuellaDactilar(Identificacion) {
    try {
        alert('entro');
        if (obj.CapturarHuella()) {
            Ext.net.Notification.show({
                html: 'Captura Terminada.. ',
                title: 'Notificación'
            });
        }
    } catch (e) {
        Ext.net.Notification.show({
            html: 'Browser Imcompatible con ActiveX',
            title: 'Notificación'
        });
    }

}