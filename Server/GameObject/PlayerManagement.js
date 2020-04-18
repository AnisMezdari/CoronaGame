
module.exports = {

	sharePosition : function(socket,playerList){
    socket.on('position', function(data ){
			var currentPlayer = playerList[data.index];
			currentPlayer.position.x = data.x;
			currentPlayer.position.y = data.y;
			currentPlayer.position.z = data.z;
			currentPlayer.rotation.x = data.rx;
			currentPlayer.rotation.y = data.ry;
			currentPlayer.rotation.z = data.rz;
      socket.broadcast.emit("positionEmit" , {player : currentPlayer});
    });
	},
};
