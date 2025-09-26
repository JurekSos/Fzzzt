using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fzzzt_ {
    internal class ProductionUnitCard {
        //For storing which materials the robot can provide, with each material at a different index.
        /*
        0 - Nut
        1 - Bolt
        2 - Gear
        4 - Oil
        */
        private bool[] _materialsNeeded = new bool[4];

        public ProductionUnitCard(int power, int score, int conveyorBeltNumber, bool[] materialsNeeded) : base(power, score, conveyorBeltNumber) {
            this._materialsNeeded = materialsNeeded;
        }

        public bool[] MaterialsNeeded {
            get { return this._materialsNeeded; }
        }

        public bool givesNut() {
            return this._materialsNeeded[0];
        }

        public bool givesBolt() {
            return this._materialsNeeded[1];
        }

        public bool givesGear() {
            return this._materialsNeeded[2];
        }

        public bool givesOil() {
            return this._materialsNeeded[3];
        }

        public override string ToString() { //TODO - materials
            return "Production Unit".PadRight(20) + base.ToString();
        }
    }
}
