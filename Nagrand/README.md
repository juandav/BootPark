# Nagrand

## ¿Que es Nagrand?
Es un servicio de windows, que gestiona un servdior sockect para el manejo e interacción de eventos del dispositivo biometrico ZK-F19 ID.

## CLIENTE

1. Crear unae estancia de conexion mediante el protocolo WebSocket, que puentea la comunicación entre el cliente y el servicio windows que corre del lado del servidor.
```js
var socket = new WebSocket('ws://127.0.0.1:2012')
```

2. Se crea el receptor de mensajes en el cliente. donde "evt" es el mensaje que emite el servidor.
```js
socket.onmessage = function (evt) {
      console.log(evt.data);
}
```

## SERVIDOR

1. Se dejan los siguientes atributos como estaticos en el servicio de windows.
```cs
// Ip del dispositivo biometrico
private const string _IP_BIOMETRICO = "192.168.1.201";
// Puerto del socket en que escucha conexiones
private const string _PORT_SOCKET = "2012";
```
2. La ip del socket se toma por defecto como "127.0.0.1" ó "localhost"
