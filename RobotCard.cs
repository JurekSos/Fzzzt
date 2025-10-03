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

        /// <summary>
        /// An array representing what materials the robot card provides.
        /// Each index represents nuts, bolts, gears, and oil, respectively.
        /// </summary>
        public bool[] Materials {
            get { return this._materialsGiven; }
        }

        /// <summary>
        /// Returns a bool representing if the robot card provides the nut material.
        /// </summary>
        /// <returns></returns>
        public bool givesNut() {
            return this._materialsGiven[0];
        }

        /// <summary>
        /// Returns a bool representing if the robot card provides the bolt material.
        /// </summary>
        /// <returns></returns>
        public bool givesBolt() {
            return this._materialsGiven[1];
        }

        /// <summary>
        /// Returns a bool representing if the robot card provides the gear material.
        /// </summary>
        /// <returns></returns>
        public bool givesGear() {
            return this._materialsGiven[2];
        }

        /// <summary>
        /// Returns a bool representing if the robot card provides the oil material.
        /// </summary>
        /// <returns></returns>
        public bool givesOil() {
            return this._materialsGiven[3];
        }

        public override string ToString() {
            string ret = "Robot".PadRight(20) + base.ToString();

            foreach (bool m in this._materialsGiven) {
                ret += (m ? "1" : "0").PadRight(10);
            }

            return ret;
        }
    }
}
