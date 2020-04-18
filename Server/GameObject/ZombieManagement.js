const Position = require("./Position");
const Rotation = require("./Rotation");
const Zombie = require("./Zombie");

var zombieList = [];

module.exports = {

  instantiateZombie : function (socket){
    socket.on("instantiateZombie", function(data){
      console.log("instantiate zombie");
      var x = data.x;
      var y = data.y;
      var z = data.z;
      var xr = data.rx;
      var yr = data.ry;
      var zr = data.rz;
      var newPostion = new Position(x,y,z);
      var newRotation = new Rotation(xr,yr,zr);
      var currentZombie = new Zombie("Zombie" ,data.index, newPostion, newRotation);
      zombieList[data.index] = currentZombie;
      socket.broadcast.emit("instantiateZombieEmit", {zombie : currentZombie });

      sharePositionZombie(socket);
    });
  }
};


function sharePositionZombie (socket){
  socket.on('positionZombie', function(data ){
    //console.log("partage position zombie " +  data.index);
    var currentZombie = zombieList[data.index];
    currentZombie.position.x = data.x;
    currentZombie.position.y = data.y;
    currentZombie.position.z = data.z;
    currentZombie.rotation.x = data.rx;
    currentZombie.rotation.y = data.ry;
    currentZombie.rotation.z = data.rz;
    socket.broadcast.emit("positionZombieEmit" , {zombie : currentZombie});
  });
}

function instantiateExit(socket){
  socket.on("")
}
