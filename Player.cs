using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fzzzt_ {
    internal class Player {
        private Deck _hand = new Deck();
        private Deck _discardPile = new Deck();
        private Deck _productionUnits = new Deck();

        private int _score = 0;
        private int _tiebreakerValue = 0;

        /// <summary>
        /// Returns a deck representing the player's hand.
        /// </summary>
        public Deck Hand {
            get { return this._hand; }
        }

        /// <summary>
        /// Returns a deck representing the player's discard pile.
        /// </summary>
        public Deck DiscardPile {
            get { return this._discardPile; }
        }

        /// <summary>
        /// Returns a deck representing the player's collection of production units.
        /// </summary>
        public Deck ProductionUnits {
            get { return this._productionUnits; }
        }

        /// <summary>
        /// The player's score
        /// </summary>
        public int Score {
            get { return this._score; }
            set { this._score = value; }
        }

        /// <summary>
        /// The number of robot cards the player has purchased, used to settle tiebreakers.
        /// </summary>
        public int TiebreakerScore {
            get { return this._tiebreakerValue; }
            set { this._tiebreakerValue = value; }
        }

        /// <summary>
        /// Adds a card to the player's hand.
        /// </summary>
        /// <param name="card"></param>
        public void addToHand(Card card) {
            this._hand.addCard(card);
        }

        /// <summary>
        /// Adds a card to the player's dicard pile.
        /// </summary>
        /// <param name="card"></param>
        public void addToDiscard(Card card) {
            this._discardPile.addCard(card);
        }

        /// <summary>
        /// Adds a production unit card to the player's collection.
        /// </summary>
        /// <param name="card"></param>
        public void addToProduction(ProductionUnitCard card) {  
            this._productionUnits.addCard(card);
        }

        /// <summary>
        /// Shuffles the player's discard pile and then draws up to 6 cards into the player's hand. 
        /// </summary>
        public void drawHand() {
            //ensure the player's hand is empty before drawing any cards.
            if (this.Hand.Cards.Count > 0) {
                MessageBox.Show("Error: Player's hand must be empty before drawing any new cards");
                return;
            }

            this._discardPile.shuffle();

            int count = 0;
            while(count < 6 && this._discardPile.Count > 0) {
                this._hand.addCard(this._discardPile.drawCard());
                ++count;
            }
        }
    }
}
