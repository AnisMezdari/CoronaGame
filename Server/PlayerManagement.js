

module.exports = {

	sharePosition : function(socket,playerList){
    socket.on('position', function(data ){
			var currentPlayer = playerList[data.index];
			currentPlayer.position.x = data.x;
			currentPlayer.position.y = data.y;
			currentPlayer.position.z = data.z;
			console.log(currentPlayer);
      socket.broadcast.emit("positionEmit" , {player : currentPlayer});
    });
	},
};
