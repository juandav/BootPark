/*Crea las ventanas que se despliegas desde menu Desktop*/
var crearVentanaWindow = function (escritorio, url, titulo, ancho, largo) {
    var desk = escritorio.getDesktop();
    var w = desk.createWindow({
        title: titulo,
        width: ancho,
        height: largo,
        autoLoad: {
            url: url,
            mode: "iframe",
            showMask: true
        }
    });

    w.center();
    w.show();
};