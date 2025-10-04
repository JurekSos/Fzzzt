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

        const string CARD_LAYOUT_KEY = "Card Type           Power     Score     Nuts      Bolts     Gears     Oil       ";
        const string BID_STRENGTH_STRING = "Current bid strength: ";

        //The main deck of cards in the game
        Deck deck;
        //A deck for the cards on the conveyor that haven't been revealed yet
        Deck conveyor;
        //Objects representing the players
        Player player1;
        Player player2;
        //Logic for determining which player's turn it is, if any.
        bool isPlayer1Turn;
        bool isPlayer2Turn;
        //For determining which player is the chief mechanic
        bool player1IsChief;
        bool player2IsChief;
        //For storing the bid of the player who went first that round
        List<Card> chiefBid;

        public Form1() {
            InitializeComponent();
            initDeck();
            initConveyor();
            initPlayers();
            initLogic();
        }

        private void buttonStart_Click(object sender, EventArgs e) {
            buttonStart.Enabled = false;
            buttonStart.Visible = false;

            //Show all the controls
            showControls();


            //Give the players their initial cards
            for (int i = 0; i < 4; ++i) {
                player1.addToHand(deck.drawCard());
            }
            for (int i = 0; i < 4; ++i) {
                player2.addToHand(deck.drawCard());
            }

            //Shuffle the deck before starting
            deck.shuffle();

            //while (deck.Cards.Count > 8) {
                setupAuction();

                startAuctionRound();

                //compare and return cards as necessary
                //repeat until out of cards

                //allow players to add up to 1 robot on each of their productions (auction cleanup)
            //}
            //Do final allocating of robot cards to production cards

            //Make players decide on material distribution

            //
        }

        private void showControls() {
            //show all labels
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;

            //Show all buttons
            buttonPlayer1AddCardToBid.Visible = true;
            buttonPlayer1RetrieveBid.Visible = true;
            buttonPlayer1Confirm.Visible = true;
            buttonPlayer2AddCardToBid.Visible = true;
            buttonPlayer2RetrieveBid.Visible = true;
            buttonPlayer2Confirm.Visible = true;
            buttonProductionAddCard.Visible = true;
            buttonProductionRetrieveCard.Visible = true;
            buttonProductionConfirm.Visible = true;

            //Show all listboxes
            listBoxConveyor.Visible = true;
            listBoxPlayer1Hand.Visible = true;
            listBoxPlayer2Hand.Visible = true;
            listBoxPlayer1Bid.Visible = true;
            listBoxPlayer2Bid.Visible = true;
            listBoxProductionCard.Visible = true;
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

            //Set up the listboxes
            listBoxConveyor.Items.Add("Cards remaining: 8");
            listBoxConveyor.Items.Add(CARD_LAYOUT_KEY);
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

            //Set the selected card to be the first one show on the conveyor
            listBoxConveyor.SelectedIndex = 3;
        }

        /// <summary>
        /// Starts an auction round by setting player 1 to make their turn.
        /// </summary>
        /// <returns></returns>
        private void startAuctionRound() {
            player1IsChief = true;
            player2IsChief = false;
            //p1 places bid
            MessageBox.Show("Player 1, please place your bid by selecting cards to bid, and then confirming.");

            buttonPlayer1AddCardToBid.Enabled = true;
            buttonPlayer1RetrieveBid.Enabled = true;
            buttonPlayer1Confirm.Enabled = true;

            listBoxPlayer1Hand.Enabled = true;
            listBoxPlayer1Bid.Enabled = true;

            refreshListBoxPlayer1Hand();

            listBoxPlayer1Bid.Items.Add(BID_STRENGTH_STRING + "0");
            listBoxPlayer1Bid.Items.Add(CARD_LAYOUT_KEY);
            listBoxPlayer1Bid.Items.Add("");

            isPlayer1Turn = true;
            isPlayer2Turn = false;
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
        /// Initialises game logic.
        /// </summary>
        private void initLogic() {
            isPlayer1Turn = false;
            isPlayer2Turn = false;

            player1IsChief = false;
            player2IsChief = false;

            chiefBid = new List<Card>();
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

        /// <summary>
        /// Checks that player 1 has selected at least one card to bid with, and ends their turn if so.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPlayer1Confirm_Click(object sender, EventArgs e) {
            if(listBoxPlayer1Bid.Items.Count == 3) {
                MessageBox.Show("You must bid with at least one card.");
                return;
            } else {
                chiefBid = new List<Card>();
                for(int i = 3; i < listBoxPlayer1Bid.Items.Count; ++i) {
                    chiefBid.Add((Card)listBoxPlayer1Bid.Items[i]);
                }

                listBoxPlayer1Bid.Items.Clear();
                listBoxPlayer1Bid.Items.Add("Cards bid: " + chiefBid.Count);
                listBoxPlayer1Bid.SelectedIndex = -1;
                listBoxPlayer1Bid.Enabled = false;

                listBoxPlayer1Hand.Items.Clear();
                listBoxPlayer1Hand.SelectedIndex = -1;
                listBoxPlayer1Hand.Enabled = false;

                buttonPlayer1AddCardToBid.Enabled = false;
                buttonPlayer1RetrieveBid.Enabled = false;
                buttonPlayer1Confirm.Enabled = false;

                if (player1IsChief) {
                    MessageBox.Show("Player 2, please place your bid by selecting cards to bid, and then confirming.");

                    listBoxPlayer2Hand.Enabled = true;
                    listBoxPlayer2Bid.Enabled = true;

                    buttonPlayer2AddCardToBid.Enabled = true;
                    buttonPlayer2RetrieveBid.Enabled = true;
                    buttonPlayer2Confirm.Enabled = true;

                    refreshListBoxPlayer2Hand();

                    listBoxPlayer2Bid.Items.Add(BID_STRENGTH_STRING + "0");
                    listBoxPlayer2Bid.Items.Add(CARD_LAYOUT_KEY);
                    listBoxPlayer2Bid.Items.Add("");

                    isPlayer2Turn = true;
                    isPlayer1Turn = false;
                } else {
                    //TODO - if player 2 went first. i.e. end of round now
                }
            }
        }

        /// <summary>
        /// Stops the user from changing the selected index without disabling the control (and making it look bad).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxConveyor_SelectedIndexChanged(object sender, EventArgs e) {
            listBoxConveyor.SelectedIndex = 3;
        }

        /// <summary>
        /// Adds a card from player 1's hand to their bid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPlayer1AddCardToBid_Click(object sender, EventArgs e) {
            if(listBoxPlayer1Hand.SelectedIndex < 3) {
                MessageBox.Show("You must select one of your cards to bid.");
                return;
            } else {
                int index = listBoxPlayer1Hand.SelectedIndex - 3;
                Card transfer = player1.Hand.drawCard(index);
                listBoxPlayer1Bid.Items.Add(transfer);

                refreshPlayer1BidStrength();

                refreshListBoxPlayer1Hand();
            }
        }

        /// <summary>
        /// Retrieves a card from player 1's bid to their hand.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPlayer1RetrieveBid_Click(object sender, EventArgs e) {
            if(listBoxPlayer1Bid.SelectedIndex < 3) {
                MessageBox.Show("You must select a card to return to your hand.");
                return;
            } else {
                int index = listBoxPlayer1Bid.SelectedIndex;
                Card transfer = (Card)listBoxPlayer1Bid.Items[index];
                listBoxPlayer1Bid.Items.RemoveAt(index);
                player1.addToHand(transfer);

                refreshPlayer1BidStrength();

                refreshListBoxPlayer1Hand();
            }
        }

        /// <summary>
        /// Refreshes the contents of the listbox for player 1's hand.
        /// </summary>
        private void refreshListBoxPlayer1Hand() {
            listBoxPlayer1Hand.Items.Clear();

            listBoxPlayer1Hand.Items.Add("Cards in hand: " + player1.Hand.Cards.Count);
            listBoxPlayer1Hand.Items.Add(CARD_LAYOUT_KEY);
            listBoxPlayer1Hand.Items.Add("");
            foreach (Card c in player1.Hand.Cards) {
                listBoxPlayer1Hand.Items.Add(c);
            }
        }

        private void refreshListBoxPlayer2Hand() {
            listBoxPlayer2Hand.Items.Clear();

            listBoxPlayer2Hand.Items.Add("Cards in hand: " + player1.Hand.Cards.Count);
            listBoxPlayer2Hand.Items.Add(CARD_LAYOUT_KEY);
            listBoxPlayer2Hand.Items.Add("");
            foreach (Card c in player2.Hand.Cards) {
                listBoxPlayer2Hand.Items.Add(c);
            }
        }

        /// <summary>
        /// Updates the bid strength displayed in player 1's bid listbox.
        /// </summary>
        private void refreshPlayer1BidStrength() {
            int bidStrength = 0;
            for (int i = 3; i < listBoxPlayer1Bid.Items.Count; ++i) {
                Card currentCard = (Card)listBoxPlayer1Bid.Items[i];
                bidStrength += currentCard.BidPower;
            }

            listBoxPlayer1Bid.Items[0] = BID_STRENGTH_STRING + bidStrength;
        }
    }
}
