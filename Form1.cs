using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fzzzt_ {
    public partial class Form1 : Form {

        //The main deck of cards in the game
        Deck deck;

        public Form1() {
            InitializeComponent();
            initDeck();
        }

        /// <summary>
        /// Initialises the main deck with all the cards for a 2 player game.
        /// </summary>
        private void initDeck() {
            initPlayerCards();
            initRobotCards();
            initProductionCards();
            initFzzztCards();
        }

        /// <summary>
        /// Initialises the players' cards and adds them to the main deck
        /// </summary>
        private void initPlayerCards() {
            this.deck.addCard(new RobotCard(1, 1, 4, new bool[]{ false, true, true, false }));
            this.deck.addCard(new RobotCard(2, 2, 4, new bool[] { false, false, false, true }));
            this.deck.addCard(new RobotCard(3, 2, 2, new bool[] { true, false, false, false }));
            this.deck.addCard(new MechanicCard());
            this.deck.addCard(new RobotCard(1, 1, 4, new bool[] { false, true, false, true }));
            this.deck.addCard(new RobotCard(2, 2, 4, new bool[] { true, false, false, false }));
            this.deck.addCard(new RobotCard(3, 2, 2, new bool[] { false, false, true, false }));
            this.deck.addCard(new MechanicCard());
        }

        /// <summary>
        /// Initialises all the robot cards that won't immediately be given to players and adds them to the main deck
        /// </summary>
        private void initRobotCards() {
            //1 power cards
            this.deck.addCard(new RobotCard(1, 1, 4, new bool[] { true, false, true, false }));
            this.deck.addCard(new RobotCard(1, 1, 4, new bool[] { true, false, false, true }));
            this.deck.addCard(new RobotCard(1, 1, 4, new bool[] { true, false, false, false }));
            this.deck.addCard(new RobotCard(1, 1, 4, new bool[] { true, true, true, false }));
            this.deck.addCard(new RobotCard(1, 1, 4, new bool[] { true, true, false, false }));
            this.deck.addCard(new RobotCard(1, 1, 4, new bool[] { false, true, false, false }));
            this.deck.addCard(new RobotCard(1, 1, 4, new bool[] { false, false, false, true }));
            this.deck.addCard(new RobotCard(1, 1, 4, new bool[] { false, false, true, false }));
            this.deck.addCard(new RobotCard(1, 1, 4, new bool[] { false, false, true, true }));
            this.deck.addCard(new RobotCard(1, 1, 4, new bool[] { true, true, true, false }));

            //2 power cards
            this.deck.addCard(new RobotCard(2, 2, 4, new bool[] { false, true, false, false }));
            this.deck.addCard(new RobotCard(2, 2, 4, new bool[] { false, false, true, false }));
            this.deck.addCard(new RobotCard(2, 2, 4, new bool[] { true, false, false, false }));
            this.deck.addCard(new RobotCard(2, 2, 4, new bool[] { false, true, false, false }));
            this.deck.addCard(new RobotCard(2, 2, 4, new bool[] { false, false, true, false }));
            this.deck.addCard(new RobotCard(2, 2, 4, new bool[] { false, false, false, true }));

            //3 power cards
            this.deck.addCard(new RobotCard(3, 2, 2, new bool[] { false, false, false, true }));
            this.deck.addCard(new RobotCard(3, 2, 2, new bool[] { false, true, false, false }));
            this.deck.addCard(new RobotCard(3, 2, 2, new bool[] { true, false, false, false }));
            this.deck.addCard(new RobotCard(3, 2, 2, new bool[] { false, true, false, false }));
            this.deck.addCard(new RobotCard(3, 2, 2, new bool[] { false, false, true, false }));
            this.deck.addCard(new RobotCard(3, 2, 2, new bool[] { false, false, false, true }));

            //4 power cards
            this.deck.addCard(new RobotCard(4, 3, 2, new bool[] { true, false, false, false }));
            this.deck.addCard(new RobotCard(4, 3, 2, new bool[] { false, true, false, false }));
            this.deck.addCard(new RobotCard(4, 3, 2, new bool[] { false, false, true, false }));
            this.deck.addCard(new RobotCard(4, 3, 2, new bool[] { false, false, false, true }));

            //5 power cards
            this.deck.addCard(new RobotCard(5, 3, 1, new bool[] { true, false, false, false }));
            this.deck.addCard(new RobotCard(5, 3, 1, new bool[] { false, true, false, false }));
            this.deck.addCard(new RobotCard(5, 3, 1, new bool[] { false, false, true, false }));
            this.deck.addCard(new RobotCard(5, 3, 1, new bool[] { false, false, false, true }));

            //Robot upgrade cards
            this.deck.addCard(new RobotCard(0, 0, 8, new bool[] { true, true, true, true }));
            this.deck.addCard(new RobotCard(0, 0, 8, new bool[] { true, true, true, true }));
        }
        
        /// <summary>
        /// Initialises all the production cards and adds them to the main deck
        /// </summary>
        private void initProductionCards() {
            this.deck.addCard(new ProductionUnitCard(0, 13, 3, new bool[] { true, true, true, true }));
            this.deck.addCard(new ProductionUnitCard(0, 9, 3, new bool[] { true, true, true, false }));
            this.deck.addCard(new ProductionUnitCard(0, 9, 3, new bool[] { true, true, true, false }));
            this.deck.addCard(new ProductionUnitCard(0, 5, 3, new bool[] { true, false, true, false }));
            this.deck.addCard(new ProductionUnitCard(0, 5, 3, new bool[] { false, true, true, false }));
            this.deck.addCard(new ProductionUnitCard(0, 6, 3, new bool[] { true, false, false, true }));
            this.deck.addCard(new ProductionUnitCard(0, 6, 3, new bool[] { false, true, false, true }));
            this.deck.addCard(new ProductionUnitCard(0, 5, 3, new bool[] { true, true, false, false }));
            this.deck.addCard(new ProductionUnitCard(0, 6, 3, new bool[] { false, false, true, true }));
            this.deck.addCard(new ProductionUnitCard(0, 3, 3, new bool[] { false, false, false, true }));
        }

        /// <summary>
        /// Initialises all the fzzzt cards and adds them to the main deck
        /// </summary>
        private void initFzzztCards() {
            this.deck.addCard(new FzzztCard());
            this.deck.addCard(new FzzztCard());
            this.deck.addCard(new FzzztCard());
            this.deck.addCard(new FzzztCard());
        }
    }
}
