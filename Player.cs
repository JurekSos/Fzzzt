using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fzzzt_ {
    internal class Player {
        private Deck _hand = new Deck();
        private Deck _discardPile = new Deck();
        private Deck _productionUnits = new Deck();


        public void addToHand(Card card) {
            this._hand.addCard(card);
        }

        public void addToDiscard(Card card) {
            this._discardPile.addCard(card);
        }

        public void addToProduction(ProductionUnitCard card) {
            this._productionUnits.addCard(card);
        }

        public void drawHand() {
            this._discardPile.shuffle();

            int count = 0;
            while(count < 6 && this._discardPile.Cards.Count > 0) {
                this._hand.addCard(this._discardPile.drawCard());
                ++count;
            }
        }
    }
}
