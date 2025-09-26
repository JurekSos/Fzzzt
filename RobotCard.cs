using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fzzzt_ {
    internal class RobotCard : Card{
        //For storing which materials the robot can provide, with each material at a different index.
            /*
            0 - Nut
            1 - Bolt
            2 - Gear
            4 - Oil
            */
        private bool[] _materialsGiven = new bool[4];

        public RobotCard(int power, int score, int conveyorBeltNumber, bool[] materialsGiven) : base(power, score, conveyorBeltNumber) {
            this._materialsGiven = materialsGiven;
        }

        public bool[] Materials {
            get { return this._materialsGiven; }
        }

        public bool givesNut() {
            return this._materialsGiven[0];
        }

        public bool givesBolt() {
            return this._materialsGiven[1];
        }

        public bool givesGear() {
            return this._materialsGiven[2];
        }

        public bool givesOil() {
            return this._materialsGiven[3];
        }

        public override string ToString() { //TODO - materials
            return "Robot".PadRight(20) + base.ToString();
        }
    }
}
