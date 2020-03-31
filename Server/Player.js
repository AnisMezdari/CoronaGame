const Position = require("./Position");

class Player {

  constructor (name,index){
    this.name = name;
    this.position = new Position(0,0,0);
    this.index = index;
  }

}
module.exports = Player
