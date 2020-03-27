const Lobby = require("./Lobby");
const Player = require("./Player");

var listLobby = [];

module.exports = {

	Main : function(io){
		io.on('connection', function(socket){
			console.log("connected");
			connection(socket)
		});
	},
};

function connection(socket){

	socket.on('join', function(data ){
		console.log(data.namePlayer + " join " + data.nameServer + " server");
			join(socket,data.nameServer, data.namePlayer);
	});

	socket.on('create', function(data ){
		console.log(data.namePlayer + " create " + data.nameServer + " server");
		CreateLobby(socket, data.nameServer,data.namePlayer)
	});

}

function CreateLobby(socket,  nameServer, namePlayer){
  var newLobby = new Lobby(nameServer);
  var player = new Player(namePlayer);
  newLobby.playerList.push(player);
  listLobby.push(newLobby);
	console.log(listLobby);
}

function join(socket, nameServer, namePlayer){
  var indexServ = findLobbyByName(nameServer);
  var lobbyObj = listLobby[indexServ];
  var player = new Player(namePlayer);
  lobbyObj.playerList.push(player);
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
