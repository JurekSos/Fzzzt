using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fzzzt_ {
    internal class Card {
        //The value in a bid the card has
        protected int _bidPower;
        //The score the card provides
        protected int _score;
        //The protectedonveyor belt number, as described in the game rules
        protected int _conveyorBeltNumber;

        public Card(int power, int score, int conveyorBeltNumber) {
            this._bidPower = power;
            this._score = score;
            this._conveyorBeltNumber = conveyorBeltNumber;
        }

        /// <summary>
        /// The value in a bid the card has
        /// </summary>
        public int BidPower {
            get {  return this._bidPower; }
        }

        /// <summary>
        /// The score the card provides
        /// </summary>
        public int Score {
            get { return this._score; }
        }

        /// <summary>
        /// The conveyor belt number. Dictates how many more cards to flip over in auctions.
        /// </summary>
        public int ConveyorBeltNumber {
            get { return this._conveyorBeltNumber; }
        }

        public override string ToString() {
            return this.BidPower.ToString().PadRight(5) + this.Score.ToString().PadRight(5);
        }
    }
}
