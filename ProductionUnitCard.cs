using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fzzzt_ {
    internal class ProductionUnitCard : Card{
        //For storing which materials the robot can provide, with each material at a different index.
        /*
        0 - Nut
        1 - Bolt
        2 - Gear
        4 - Oil
        */
        private bool[] _materialsNeeded = new bool[4];

        //For counting at the end how many of each material was provided to the production card
        private int[] _materialsProvided = new int[4];

        //For Storing the robooot cards that were placed on the production unit
        private List<RobotCard> _cards = new List<RobotCard>();

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

        public override string ToString() {
            string ret = "Production Unit".PadRight(20) + base.ToString();

            foreach(bool m in this._materialsNeeded) {
                ret += (m ? "1" : "0").PadRight(5);
            }

            return ret;
        }
    }
}
