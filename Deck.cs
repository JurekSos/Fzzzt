using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fzzzt_ {
    internal class Deck {
        private List<Card> _cards = new List<Card>();

        /// <summary>
        /// Adds a card to the deck
        /// </summary>
        /// <param name="card"></param>
        public void addCard(Card card) {
            this._cards.Add(card);
        }

        /// <summary>
        /// Returns a read only collection of the cards in the deck
        /// </summary>
        public ReadOnlyCollection<Card> Cards {
            get { return new ReadOnlyCollection<Card>(this._cards); }
        }

        /// <summary>
        /// Returns a card at the given position, by default 0, then removes that card from the deck.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public Card drawCard(int pos = 0) {
            Card ret = this._cards[pos];
            this._cards.RemoveAt(pos);
            return ret;
        }

        /// <summary>
        /// Shuffles the deck
        /// </summary>
        public void shuffle() {
            List<Card> shuffledDeck = new List<Card>();

            int numCards = this._cards.Count;

            Random rand = new Random();

            for(int i = 0; i < numCards; ++i) {
                shuffledDeck.Add(this.drawCard(rand.Next(numCards - i)));
            }

            this._cards = shuffledDeck;
        }
    }
}
