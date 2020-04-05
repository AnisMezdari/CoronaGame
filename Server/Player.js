const Position = require("./Position");

class Player {

  constructor (name,index,isAdmin){
    this.name = name;
    this.position = new Position(0,0,0);
    this.index = index;
    this.isAdmin = isAdmin
  }

}
module.exports = Player
