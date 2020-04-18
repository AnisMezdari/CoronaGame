const Lobby = require("./Lobby");
const Player = require("../GameObject/Player");

var listLobby = [];

var playerManagement = require("../GameObject/PlayerManagement");
var zombieManagement = require("../GameObject/zombieManagement");


module.exports = {

	Main : function(io){
		io.on('connection', function(socket){
			console.log("connected");
			connection(socket);

		});
	},
};

function connection(socket){
	socket.on('join', function(data ){
		console.log(data.namePlayer + " join " + data.nameServer + " server");
		var player = join(socket,data.nameServer, data.namePlayer);
		socket.broadcast.emit("newPlayerJoin", {nameNewPlayer : data.namePlayer });
		console.log(player);
		socket.emit("player" , {playerObject : player });
	});

	socket.on('create', function(data ){
		console.log(data.namePlayer + " create " + data.nameServer + " server");
		CreateLobby(socket, data.nameServer,data.namePlayer)
	});

	socket.on("listLobby" , function(data){
		socket.emit("listLobby",{lobbyList : listLobby} );
	});

	socket.on("listPlayer" , function(data){
		var listPlayerJson = listPlayerByServerName(data.nameServer);
		console.log(listPlayerJson);
		socket.emit("listPlayer",{playerList : listPlayerJson} );
	});

	socket.on("startGame", function(data){
		zombieManagement.instantiateZombie(socket);
		socket.broadcast.emit("startGame");
	})
}

function CreateLobby(socket,  nameServer, namePlayer){
  var newLobby = new Lobby(nameServer);
  var player = new Player(namePlayer,0,true);
  newLobby.playerList.push(player);
  listLobby.push(newLobby);
	playerManagement.sharePosition(socket, newLobby.playerList);
	console.log(listLobby);
}

function join(socket, nameServer, namePlayer){
  var indexServ = findLobbyByName(nameServer);
  var lobbyObj = listLobby[indexServ];
  var player = new Player(namePlayer, lobbyObj.playerList.length,false);
  lobbyObj.playerList.push(player);
	playerManagement.sharePosition(socket, lobbyObj.playerList);
	return player;
}

function findLobbyByName(name){
	var i= 0;
	for(i = 0; i < listLobby.length ; i++){
		if(listLobby[i].name == name){
			return i;
		}
	}
	return 0;
}

function listPlayerByServerName(name){
	var i =0;
	for(i=0;i < listLobby.length;i++){
		if(listLobby[i].name == name) {
			return listLobby[i].playerList;
		}
	}
}
