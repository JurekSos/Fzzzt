using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;


namespace Fzzzt_ {
    public partial class Form1 : Form {

        //The main deck of cards in the game
        Deck deck;
        //A deck for the cards on the conveyor that haven't been revealed yet
        Deck conveyor;
        //Objects representing the players
        Player player1;
        Player player2;

        public Form1() {
            InitializeComponent();
            initDeck();
            initConveyor();
            initPlayers();

            //Give the players their initial cards
            for(int i = 0; i < 4; ++i) {
                player1.addToHand(deck.drawCard());
            }
            for (int i = 0; i < 4; ++i) {
                player2.addToHand(deck.drawCard());
            }

            //Shuffle the deck before starting
            deck.shuffle();

            test();
            while (deck.Cards.Count > 8) {
                setupAuction();

                doAuctionRound(); //TODO

                //allow players to add up to 1 robot on each of their productions (auction cleanup)
            }
            //Do final allocating of robot cards to production cards

            //Make players decide on material distribution

            //
        }


        private void test() {
            var a = deck.Cards;
            var b = player1.Hand.Cards;
            var c = player2.Hand.Cards;

            listBoxConveyor.Items.Add("Card Type".PadRight(20) + "Power".PadRight(10) + "Score".PadRight(10) + "Nut".PadRight(10) + "Bolt".PadRight(10) + "Gear".PadRight(10) + "Oil".PadRight(10));
            listBoxConveyor.Items.Add("");

            listBoxPlayer1Hand.Items.Add("Card Type".PadRight(20) + "Power".PadRight(10) + "Score".PadRight(10) + "Nut".PadRight(10) + "Bolt".PadRight(10) + "Gear".PadRight(10) + "Oil".PadRight(10));
            listBoxPlayer1Hand.Items.Add("");

            listBoxPlayer2Hand.Items.Add("Card Type".PadRight(20) + "Power".PadRight(10) + "Score".PadRight(10) + "Nut".PadRight(10) + "Bolt".PadRight(10) + "Gear".PadRight(10) + "Oil".PadRight(10));
            listBoxPlayer2Hand.Items.Add("");

            foreach (Card C in a) {
                listBoxConveyor.Items.Add(C);
            }
            foreach (Card C in b) {
                listBoxPlayer1Hand.Items.Add(C);
            }
            foreach (Card C in c) {
                listBoxPlayer2Hand.Items.Add(C);
            }
        }

        /// <summary>
        /// Sets up the auction stage of the game.
        /// </summary>
        private void setupAuction() {
            for(int i = 0; i < 8; ++i) {
                conveyor.addCard(deck.drawCard());
            }

            //Set up the listbox
            listBoxConveyor.Items.Add("Cards remaining: 8");
            listBoxConveyor.Items.Add("Card Type".PadRight(20) + "Power".PadRight(10) + "Score".PadRight(10) + "Nut".PadRight(10) + "Bolt".PadRight(10) + "Gear".PadRight(10) + "Oil".PadRight(10));
            listBoxConveyor.Items.Add("");

            //Take the first card from the conveyor
            Card temp = conveyor.drawCard();
            //Add the card to the listbox
            listBoxConveyor.Items.Add(temp);
            //Check the card's conveyor belt number and add more cards to the listbox if necessary
            int loopMax = temp.ConveyorBeltNumber - 1;
            for(int i = 0; i < loopMax; ++i) {
                listBoxConveyor.Items.Add(conveyor.drawCard());
            }
        }

        private void doAuctionRound() { //TODO
            //p1 places bid
            //p2 places bid
            //compare and return cards as necessary
            //repeat until out of cards
        }

        /// <summary>
        /// Initialises the main deck with all the cards for a 2 player game.
        /// </summary>
        private void initDeck() {
            deck = new Deck();

            initPlayerCards();
            initRobotCards();
            initProductionCards();
            initFzzztCards();
        }

        /// <summary>
        /// Initialises the deck that stores the cards on the conveyor that haven't been revealed yet
        /// </summary>
        private void initConveyor() {
            conveyor = new Deck();
        }

        /// <summary>
        /// Initialises the players
        /// </summary>
        private void initPlayers() {
            player1 = new Player();
            player2 = new Player();
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
