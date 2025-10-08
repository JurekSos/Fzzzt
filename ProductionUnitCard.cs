using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design.Serialization;
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

        //For Storing the robot cards that were placed on the production unit
        private List<RobotCard> _robots = new List<RobotCard>();

        public ProductionUnitCard(int power, int score, int conveyorBeltNumber, bool[] materialsNeeded) : base(power, score, conveyorBeltNumber) {
            this._materialsNeeded = materialsNeeded;
        }

        /// <summary>
        /// An array representing what materials the production unit requires.
        /// Each index represents nuts, bolts, gears, and oil, respectively.
        /// </summary>
        public bool[] MaterialsNeeded {
            get { return this._materialsNeeded; }
        }

        /// <summary>
        /// A read only collection of the robot cards assigneed to this production unit.
        /// </summary>
        public ReadOnlyCollection<RobotCard> Robots {
            get { return new ReadOnlyCollection<RobotCard>(this._robots); }
        }

        /// <summary>
        /// Returns a bool representing if the production unit requires the nut material.
        /// </summary>
        /// <returns></returns>
        public bool givesNut() {
            return this._materialsNeeded[0];
        }

        /// <summary>
        /// Returns a bool representing if the production unit requires the bolt material.
        /// </summary>
        /// <returns></returns>
        public bool givesBolt() {
            return this._materialsNeeded[1];
        }

        /// <summary>
        /// Returns a bool representing if the production unit requires the gear material.
        /// </summary>
        /// <returns></returns>
        public bool givesGear() {
            return this._materialsNeeded[2];
        }

        /// <summary>
        /// Returns a bool representing if the production unit requires the oil material.
        /// </summary>
        /// <returns></returns>
        public bool givesOil() {
            return this._materialsNeeded[3];
        }

        public void addRobot(RobotCard card) {
            this._robots.Add(card);
        }

        public override string ToString() {
            string ret = "Production Unit".PadRight(20) + base.ToString();

            foreach(bool m in this._materialsNeeded) {
                ret += (m ? "1" : "0").PadRight(10);
            }

            return ret;
        }
    }
}
