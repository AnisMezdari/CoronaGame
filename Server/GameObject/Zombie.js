const Position = require("./Position");
const Rotation = require("./Rotation");

class Zombie {
  constructor (name,index, position , rotation){
    this.name = name;
    this.position = position;
    this.rotation = rotation;
    this.index = index;
  }

}
module.exports = Zombie
