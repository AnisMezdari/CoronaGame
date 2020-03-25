const Lobby = require("./Lobby");

var listLobby = []

module.exports = {

	Main : function(io){



		io.on('join', function(socket,data){
			join(socket,data.name);
		});
    io.on("create", function(socket,data){
      CreateLobby(socket,data.name);
    });

	},
};

function CreateLobby(socket,  nameServer, namePlayer){
  var newLobby = new Lobby(nameServer);
  var player = new Player(namePlayer);
  newLobby.playerList.push(player);
  listLobby.push(newLobby);
}

function join(socket, nameServer, namePlayer){

  var indexServ = listLobby.indexOf('nameServer');
  var lobby = listLobby[indexServ];
  var player = new Player(namePlayer);
  lobby.playerList.push(player);

}
