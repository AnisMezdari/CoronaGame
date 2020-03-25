var express = require('express');
var session = require('express-session');
var app = express();
const PORT = process.env.PORT || 7777;

var session = require("express-session")({
    secret: "my-secret",
    resave: true,
    saveUninitialized: true
});
var sharedsession = require("express-socket.io-session");
app.use(session);


var io = require('socket.io')({
	transports: ['websocket'],
});

io.use(sharedsession(session, {
    autoSave:true
}));
io.attach(PORT);

var tools = require("./LobbyManagement");
tools.Main(io);


console.log("sa tourne oklm avec le corona virus en plus");
console.log("PORT : " + PORT);
