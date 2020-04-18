const Position = require("./Position");
const Rotation = require("./Rotation");

class Player {

  constructor (name,index,isAdmin){
    this.name = name;
    this.position = new Position(0,0,0);
    this.rotation = new Rotation(0,0,0);
    this.index = index;
    this.isAdmin = isAdmin
  }

}
module.exports = Player
