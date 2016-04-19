var app = require('http').createServer(),
     io = require('socket.io').listen(app),
     five = require('johnny-five');

app.listen(2016);

var board = new five.Board();

board.on("ready", function() {
  var led = new five.Led(13);

  io.sockets.on('connection', function (socket) {
    socket.on('click', function () {
      led.toggle();
    });
  });
});
