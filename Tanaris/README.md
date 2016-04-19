# Tanaris

Tanaris es una app socket al que se le emite la salida de la validación cruce dispositivo biometrico y RFID.

## Instalación
npm install

## ¿Como usar Tanaris?

```html
<!DOCTYPE html>
<html>
  <head>
    <meta charset="utf-8">
    <title>BPark</title>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="https://cdn.socket.io/socket.io-1.4.5.js"></script>
    <script>

      $(document).ready(function() {
        var socket = io.connect('http://localhost:2016');
        $('#button').click(function(e){
          socket.emit('click');
          e.preventDefault();
        });
      });

    </script>
  </head>
  <body>
    <button id="button" href="#">LED ON/OFF</button>
  </body>
</html>

```
