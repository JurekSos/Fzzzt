using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Fzzzt_ {
    public partial class Form1 : Form {
        const string CARD_LAYOUT_KEY = "Card Type           Power     Score     Nuts      Bolts     Gears     Oil       ";
        const string BID_STRENGTH_STRING = "Current bid strength: ";

        //The same listbox as the conveyor, but alised for a different purpose in the endgame stage.
        ListBox listBoxProductionSets;

        //The main deck of cards in the game
        Deck deck;
        //A deck for the cards on the conveyor that haven't been revealed yet
        Deck conveyor;
        //Objects representing the players
        Player player1;
        Player player2;
        //Logic for determining which player's turn it is, if any.
        bool isPlayer1Turn;
        //For controlling the change of the production card listbox
        bool canChangeProdIndex;
        int prodIndex;
        //For checking if all aucitons are over.
        bool auctionsEnded;
        bool choosingMaterials;
        //For storing the bid of the player who went first that round
        List<Card> chiefBid;


        Action<int> showPlayerWonBid = (int x) => MessageBox.Show("Player " + x + " has won the bid.");
        Action<int> showPlayerBidInstruction = (int x) => MessageBox.Show("Player " + x + ", please place your bid by selecting cards to bid, and then confirming." +
                                                                          "\nIf you are out of cards, then just click the confirm button.");
        Action<int> showAuctionCleanupInstruction = (int x) => MessageBox.Show("Player " + x + ", please select up to one card to add to the highlighted production card.");


        public Form1() {
            InitializeComponent();
            initDeck();
            initConveyor();
            initPlayers();
            initLogic();

            listBoxProductionSets = listBoxConveyor;
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
            setUpAuction();

            //allow players to add up to 1 robot on each of their productions (auction cleanup)
            //}
            //Do final allocating of robot cards to production cards

            //Make players decide on material distribution

            //
        }

        private void showControls() {
            //show all labels
            label1.Visible = true;
            labelConveyorBelt.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            label8.Visible = true;

            //Show all buttons
            buttonPlayer1AddCardToBid.Visible = true;
            buttonPlayer1RetrieveBid.Visible = true;
            buttonPlayer1Confirm.Visible = true;
            buttonPlayer2AddCardToBid.Visible = true;
            buttonPlayer2RetrieveBid.Visible = true;
            buttonPlayer2Confirm.Visible = true;
            buttonAddProd.Visible = true;
            buttonNoAddProd.Visible = true;
            buttonProductionMaterialConfirm.Visible = true;

            //Show the radio buttons
            radioButtonNut.Visible = true;
            radioButtonBolt.Visible = true;
            radioButtonGear.Visible = true;
            radioButtonOil.Visible = true;

            //Show all listboxes
            listBoxConveyor.Visible = true;
            listBoxPlayer1Hand.Visible = true;
            listBoxPlayer2Hand.Visible = true;
            listBoxPlayer1Bid.Visible = true;
            listBoxPlayer2Bid.Visible = true;
            listBoxProductionCard.Visible = true;
        }

        /// <summary>
        /// Sets up the auction stage of the game.
        /// </summary>
        private void setUpAuction() {
            labelConveyorBelt.Visible = true;
            labelProdSets.Visible = false;

            listBoxConveyor.Items.Clear();

            for (int i = 0; i < 8; ++i) {
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
            for (int i = 0; i < loopMax; ++i) {
                listBoxConveyor.Items.Add(conveyor.drawCard());
            }

            //Set the selected card to be the first one show on the conveyor
            listBoxConveyor.SelectedIndex = 3;

            if (player1.Hand.Count == 0 && player2.Hand.Count == 0) {
                player1.drawHand();
                player2.drawHand();
            }

            startAuctionRound();
        }

        /// <summary>
        /// Starts an auction round by setting player 1 to make their turn.
        /// </summary>
        /// <returns></returns>
        private void startAuctionRound() {
            player1.IsChief = true;
            player2.IsChief = false;
            //p1 places bid
            showPlayerBidInstruction(1);

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

            player1.IsChief = false;
            player2.IsChief = false;

            canChangeProdIndex = false;
            prodIndex = -1;

            auctionsEnded = false;
            choosingMaterials = false;

            chiefBid = new List<Card>();
        }

        /// <summary>
        /// Initialises the players' cards and adds them to the main deck
        /// </summary>
        private void initPlayerCards() {
            this.deck.addCard(new RobotCard(1, 1, 4, new bool[] { false, true, true, false }));
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
            if (listBoxPlayer1Bid.Items.Count == 3) {
                if (player1.Hand.Count != 0) {
                    MessageBox.Show("You must bid with at least one card.");
                    return;
                }
            }
            if (player1.IsChief) {
                chiefBid = new List<Card>();
                for (int i = 3; i < listBoxPlayer1Bid.Items.Count; ++i) {
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

                showPlayerBidInstruction(2);

                listBoxPlayer2Hand.Enabled = true;
                listBoxPlayer2Bid.Enabled = true;

                buttonPlayer2AddCardToBid.Enabled = true;
                buttonPlayer2RetrieveBid.Enabled = true;
                buttonPlayer2Confirm.Enabled = true;

                refreshListBoxPlayer2Hand();

                listBoxPlayer2Bid.Items.Add(BID_STRENGTH_STRING + "0");
                listBoxPlayer2Bid.Items.Add(CARD_LAYOUT_KEY);
                listBoxPlayer2Bid.Items.Add("");

                isPlayer1Turn = false;
            } else {
                List<Card> tempP1BidCards = new List<Card>();

                for (int i = 3; i < listBoxPlayer1Bid.Items.Count; ++i) {
                    tempP1BidCards.Add((Card)listBoxPlayer1Bid.Items[i]);
                }

                //Clear player 1's listboxes here to avoid revealing information to to other player.
                listBoxPlayer1Hand.Items.Clear();
                listBoxPlayer1Bid.Items.Clear();

                //Clear as the information is no longer relevant.
                listBoxPlayer2Bid.Items.Clear();

                int player1BidStrength = 0;
                int player2BidStrength = 0;

                foreach (Card c in chiefBid) {
                    player2BidStrength += c.BidPower;
                }
                foreach (Card c in tempP1BidCards) {
                    player1BidStrength += c.BidPower;
                }

                Card boughtCard = (Card)listBoxConveyor.Items[listBoxConveyor.SelectedIndex];
                listBoxConveyor.Items.RemoveAt(listBoxConveyor.SelectedIndex);

                if (player2BidStrength >= player1BidStrength && chiefBid.Count > 0) {
                    //Player 2 wins the bid
                    showPlayerWonBid(2);

                    foreach (Card c in chiefBid) {
                        player2.addToDiscard(c);
                    }
                    foreach (Card c in tempP1BidCards) {
                        player1.addToHand(c);
                    }

                    if (boughtCard is ProductionUnitCard) {
                        player2.ProductionUnits.addCard(boughtCard);
                    } else {
                        player2.addToDiscard(boughtCard);
                        if(boughtCard is RobotCard) {
                            player2.TiebreakerScore++;
                        }
                    }

                } else {
                    //Player 1 wins the bid
                    showPlayerWonBid(1);

                    foreach (Card c in tempP1BidCards) {
                        player1.addToDiscard(c);
                    }
                    foreach (Card c in chiefBid) {
                        player2.addToHand(c);
                    }

                    if (boughtCard is ProductionUnitCard) {
                        player1.ProductionUnits.addCard(boughtCard);
                    } else {
                        player1.addToDiscard(boughtCard);
                        if(boughtCard is RobotCard) {
                            player1.TiebreakerScore++;
                        }
                    }
                }

                player1.IsChief = true;
                player2.IsChief = false;

                resetAuction();
            }

        }

        /// <summary>
        /// Stops the user from changing the selected index without disabling the control (and making it look bad).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxConveyor_SelectedIndexChanged(object sender, EventArgs e) {
            //First check is if the conveyor belt is being displayed, the second check is for when production sets are being displayed and can be selected.
            if (listBoxConveyor.Items.Count > 3 && labelConveyorBelt.Enabled) {
                listBoxConveyor.SelectedIndex = 3;
            } else if (listBoxConveyor.Items.Count > 4 && choosingMaterials) {
                listBoxConveyor.SelectedIndex = 4;
            } else {
                listBoxConveyor.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Adds a card from player 1's hand to their bid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPlayer1AddCardToBid_Click(object sender, EventArgs e) {
            if (listBoxPlayer1Hand.SelectedIndex < 3) {
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
            if (listBoxPlayer1Bid.SelectedIndex < 3) {
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
        /// Adds a card from player 1's hand to their bid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPlayer2AddCardToBid_Click(object sender, EventArgs e) {
            if (listBoxPlayer2Hand.SelectedIndex < 3) {
                MessageBox.Show("You must select one of your cards to bid.");
                return;
            } else {
                int index = listBoxPlayer2Hand.SelectedIndex - 3;
                Card transfer = player2.Hand.drawCard(index);
                listBoxPlayer2Bid.Items.Add(transfer);

                refreshPlayer2BidStrength();

                refreshListBoxPlayer2Hand();
            }
        }

        /// <summary>
        /// Retrieves a card from player 2's bid to their hand.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPlayer2RetrieveBid_Click(object sender, EventArgs e) {
            if (listBoxPlayer2Bid.SelectedIndex < 3) {
                MessageBox.Show("You must select a card to return to your hand.");
                return;
            } else {
                int index = listBoxPlayer2Bid.SelectedIndex;
                Card transfer = (Card)listBoxPlayer2Bid.Items[index];
                listBoxPlayer2Bid.Items.RemoveAt(index);
                player2.addToHand(transfer);

                refreshPlayer2BidStrength();

                refreshListBoxPlayer2Hand();
            }
        }

        /// <summary>
        /// Checks that player 2 has selected at least one card to bid with, and ends their turn if so.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPlayer2Confirm_Click(object sender, EventArgs e) {
            if (listBoxPlayer2Bid.Items.Count == 3) {
                if (player2.Hand.Count != 0) {
                    MessageBox.Show("You must bid with at least one card.");
                    return;
                }
            }
            if (player2.IsChief) {
                chiefBid = new List<Card>();
                for (int i = 3; i < listBoxPlayer2Bid.Items.Count; ++i) {
                    chiefBid.Add((Card)listBoxPlayer2Bid.Items[i]);
                }

                listBoxPlayer2Bid.Items.Clear();
                listBoxPlayer2Bid.Items.Add("Cards bid: " + chiefBid.Count);
                listBoxPlayer2Bid.SelectedIndex = -1;
                listBoxPlayer2Bid.Enabled = false;

                listBoxPlayer2Hand.Items.Clear();
                listBoxPlayer2Hand.SelectedIndex = -1;
                listBoxPlayer2Hand.Enabled = false;

                buttonPlayer2AddCardToBid.Enabled = false;
                buttonPlayer2RetrieveBid.Enabled = false;
                buttonPlayer2Confirm.Enabled = false;

                showPlayerBidInstruction(1);

                listBoxPlayer1Hand.Enabled = true;
                listBoxPlayer1Bid.Enabled = true;

                buttonPlayer1AddCardToBid.Enabled = true;
                buttonPlayer1RetrieveBid.Enabled = true;
                buttonPlayer1Confirm.Enabled = true;

                refreshListBoxPlayer1Hand();

                listBoxPlayer1Bid.Items.Add(BID_STRENGTH_STRING + "0");
                listBoxPlayer1Bid.Items.Add(CARD_LAYOUT_KEY);
                listBoxPlayer1Bid.Items.Add("");

                isPlayer1Turn = true;
            } else {
                List<Card> tempP2BidCards = new List<Card>();

                for (int i = 3; i < listBoxPlayer2Bid.Items.Count; ++i) {
                    tempP2BidCards.Add((Card)listBoxPlayer2Bid.Items[i]);
                }

                //Clear player 2's listboxes here to avoid revealing information to to other player.
                listBoxPlayer2Hand.Items.Clear();
                listBoxPlayer2Bid.Items.Clear();

                //Clear as the information is no longer relevant.
                listBoxPlayer1Bid.Items.Clear();

                int player1BidStrength = 0;
                int player2BidStrength = 0;

                foreach (Card c in chiefBid) {
                    player1BidStrength += c.BidPower;
                }
                foreach (Card c in tempP2BidCards) {
                    player2BidStrength += c.BidPower;
                }

                Card boughtCard = (Card)listBoxConveyor.Items[listBoxConveyor.SelectedIndex];
                listBoxConveyor.Items.RemoveAt(listBoxConveyor.SelectedIndex);

                if (player1BidStrength >= player2BidStrength && chiefBid.Count > 0) {
                    //Player 1 wins the bid
                    showPlayerWonBid(1);

                    foreach (Card c in chiefBid) {
                        player1.addToDiscard(c);
                    }
                    foreach (Card c in tempP2BidCards) {
                        player2.addToHand(c);
                    }

                    if (boughtCard is ProductionUnitCard) {
                        player1.ProductionUnits.addCard(boughtCard);
                    } else {
                        player1.addToDiscard(boughtCard);
                        if(boughtCard is RobotCard) {
                            player1.TiebreakerScore++;
                        }
                    }

                } else {
                    //Player 2 wins the bid
                    showPlayerWonBid(2);

                    foreach (Card c in tempP2BidCards) {
                        player2.addToDiscard(c);
                    }
                    foreach (Card c in chiefBid) {
                        player1.addToHand(c);
                    }

                    if (boughtCard is ProductionUnitCard) {
                        player2.ProductionUnits.addCard(boughtCard);
                    } else {
                        player2.addToDiscard(boughtCard);
                        if (boughtCard is RobotCard) {
                            player2.TiebreakerScore++;
                        }
                    }
                }

                player1.IsChief = false;
                player2.IsChief = true;

                resetAuction();
            }

        }

        /// <summary>
        /// Resets the auction after one round of bidding has been completed.
        /// </summary>
        private void resetAuction() {
            if (listBoxConveyor.Items.Count == 3) {
                //The revealed cards have all been auctioned
                if (conveyor.Count > 0) {
                    //There are remaining cards to be auctioned

                    //Take the first card from the conveyor
                    Card temp = conveyor.drawCard();
                    //Add the card to the listbox
                    listBoxConveyor.Items.Add(temp);
                    //Check the card's conveyor belt number and add more cards to the listbox if necessary
                    int loopMax = temp.ConveyorBeltNumber - 1;
                    int count = 0;
                    while (count < loopMax && conveyor.Count > 0) {
                        listBoxConveyor.Items.Add(conveyor.drawCard());
                        count++;
                    }
                } else {
                    //end auction round and do cleanup
                    auctionCleanup();
                    return;
                }
            }
            //Update the information in the listbox to reflect the remaining number of cards.
            listBoxConveyor.Items[0] = "Cards remaining: " + (conveyor.Count + listBoxConveyor.Items.Count - 3);
            //Reselect the current card to be auctioned in the listbox.
            listBoxConveyor.SelectedIndex = 3;

            if (player1.Hand.Count == 0 && player2.Hand.Count == 0) {
                player1.drawHand();
                player2.drawHand();
            }

            if (player1.IsChief) {
                showPlayerBidInstruction(1);
                refreshListBoxPlayer1Hand();

                listBoxPlayer1Bid.Items.Add(BID_STRENGTH_STRING + "0");
                listBoxPlayer1Bid.Items.Add(CARD_LAYOUT_KEY);
                listBoxPlayer1Bid.Items.Add("");

                isPlayer1Turn = true;
            } else {
                showPlayerBidInstruction(2);
                refreshListBoxPlayer2Hand();

                listBoxPlayer2Bid.Items.Add(BID_STRENGTH_STRING + "0");
                listBoxPlayer2Bid.Items.Add(CARD_LAYOUT_KEY);
                listBoxPlayer2Bid.Items.Add("");

                isPlayer1Turn = false;
            }
        }

        /// <summary>
        /// Starts the cleanup stage after auctions are completed, allowing players to place robots onto their production cards.
        /// </summary>
        private void auctionCleanup() { 
            buttonPlayer1AddCardToBid.Enabled = false;
            buttonPlayer1RetrieveBid.Enabled = false;
            buttonPlayer1Confirm.Enabled = false;

            //Not disabled because it is needded for player 1 to add their cards.
            //NOT DONE - listBoxPlayer1Hand.Enabled = false;
            listBoxPlayer1Bid.Enabled = false;

            buttonPlayer2AddCardToBid.Enabled = false;
            buttonPlayer2RetrieveBid.Enabled = false;
            buttonPlayer2Confirm.Enabled = false;

            listBoxPlayer2Hand.Enabled = false;
            listBoxPlayer2Bid.Enabled = false;

            if (player1.ProductionUnits.Count > 0) {
                isPlayer1Turn = true;

                showAuctionCleanupInstruction(1);
            } else {
                isPlayer1Turn = false;

                MessageBox.Show("Player 1, you have no production cards.");

                if (player2.ProductionUnits.Count > 0) {
                    showAuctionCleanupInstruction(2);

                    listBoxPlayer1Hand.Enabled = false;
                    listBoxPlayer2Hand.Enabled = true;
                } else {
                    MessageBox.Show("Player 2, you have no production cards.");

                    buttonNoAddProd.Enabled = false;
                    buttonAddProd.Enabled = false;

                    listBoxProductionCard.Enabled = false;
                    listBoxPlayer2Hand.Enabled = false;

                    if (deck.Count >= 8) {
                        setUpAuction();
                    } else {
                        beginScoringPhase();
                    }
                    return;
                }
            }

            buttonAddProd.Enabled = true;
            buttonNoAddProd.Enabled = true;

            showProductionInformation();
        }

        /// <summary>
        /// Refreshes the contents of the listbox for player 1's hand.
        /// </summary>
        private void refreshListBoxPlayer1Hand() {
            listBoxPlayer1Hand.Items.Clear();

            listBoxPlayer1Hand.Items.Add("Cards in hand: " + player1.Hand.Count);
            listBoxPlayer1Hand.Items.Add(CARD_LAYOUT_KEY);
            listBoxPlayer1Hand.Items.Add("");
            foreach (Card c in player1.Hand.Cards) {
                listBoxPlayer1Hand.Items.Add(c);
            }
        }

        /// <summary>
        /// Refreshes the contents of the listbox for player 2's hand.
        /// </summary>
        private void refreshListBoxPlayer2Hand() {
            listBoxPlayer2Hand.Items.Clear();

            listBoxPlayer2Hand.Items.Add("Cards in hand: " + player2.Hand.Count);
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

        /// <summary>
        /// Updates the bid strength displayed in player 2's bid listbox.
        /// </summary>
        private void refreshPlayer2BidStrength() {
            int bidStrength = 0;
            for (int i = 3; i < listBoxPlayer2Bid.Items.Count; ++i) {
                Card currentCard = (Card)listBoxPlayer2Bid.Items[i];
                bidStrength += currentCard.BidPower;
            }

            listBoxPlayer2Bid.Items[0] = BID_STRENGTH_STRING + bidStrength;
        }

        /// <summary>
        /// Calls the methods which put all the information necessary in the listboxes for when adding robots to production units.
        /// </summary>
        private void showProductionInformation() {
            setUpListBoxProductionCard();

            putAllPlayerCardsInHand();
            if (isPlayer1Turn) {
                refreshListBoxPlayer1Hand();
            } else {
                refreshListBoxPlayer2Hand();
            }

            labelConveyorBelt.Visible = false;
            labelProdSets.Visible = true;
            refreshProductionRobotInformation();
        }

        private void setUpListBoxProductionCard() {
            listBoxProductionCard.Enabled = true;

            listBoxProductionCard.Items.Add(CARD_LAYOUT_KEY);
            listBoxProductionCard.Items.Add("");

            ReadOnlyCollection<Card> cards;

            if (isPlayer1Turn) {
                cards = player1.ProductionUnits.Cards;
            } else {
                cards = player2.ProductionUnits.Cards;
            }

            foreach (Card c in cards) {
                listBoxProductionCard.Items.Add(c);
            }

            changeProdIndex(2);
        }

        /// <summary>
        /// Moves all the player's non production cards into their hand.
        /// </summary>
        private void putAllPlayerCardsInHand() {
            Player currentPlayer;
            if (isPlayer1Turn) {
                currentPlayer = player1;
            } else {
                currentPlayer = player2;
            }
            while (currentPlayer.DiscardPile.Count > 0) {
                currentPlayer.addToHand(currentPlayer.DiscardPile.drawCard());
            }
        }

        private void putAllplayerCardsInDiscard() {
            Player currentPlayer;
            if (isPlayer1Turn) {
                currentPlayer = player1;
            } else {
                currentPlayer = player2;
            }
            while (currentPlayer.Hand.Count > 0) {
                currentPlayer.addToDiscard(currentPlayer.Hand.drawCard());
            }
        }

        /// <summary>
        /// Puts the information about the robots currently on the selected production card into the listbox.
        /// </summary>
        private void refreshProductionRobotInformation() {
            listBoxProductionSets.Items.Clear();

            ProductionUnitCard selectedProdUnit = (ProductionUnitCard)listBoxProductionCard.Items[listBoxProductionCard.SelectedIndex];

            listBoxProductionSets.Items.Add("Current production unit has " + selectedProdUnit.NutsProvided + " nuts, " + selectedProdUnit.BoltsProvided + " bolts, " + selectedProdUnit.GearsProvided + " gears, and " + selectedProdUnit.OilProvided + " oil.");
            listBoxProductionSets.Items.Add("Cards on current production unit:");
            listBoxProductionSets.Items.Add(CARD_LAYOUT_KEY);
            listBoxProductionSets.Items.Add("");


            foreach(RobotCard r in selectedProdUnit.Robots) {
                listBoxProductionSets.Items.Add(r);
            }
        }

        /// <summary>
        /// Adds a valid selected robot card from the current player's deck to the selected production unit caard.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddProd_Click(object sender, EventArgs e) {
            if (auctionsEnded) {
                //User can select the index, so have to ensure that a valid production unit was selected.

                if(listBoxProductionCard.SelectedIndex < 2) {
                    MessageBox.Show("Please select a production unit on which to place the card.");
                    return;
                }
            }

            int ind = 0;
            Card temp;
            RobotCard candidate;
            ProductionUnitCard selectedProdUnit = (ProductionUnitCard)listBoxProductionCard.Items[listBoxProductionCard.SelectedIndex];
            bool materialMatch = false;

            if (isPlayer1Turn) {
                ind = listBoxPlayer1Hand.SelectedIndex;
                if (ind >= 3) {
                    temp = (Card)listBoxPlayer1Hand.Items[ind];
                    if(temp is RobotCard) {
                        candidate = (RobotCard)temp;

                        //Check that the robot provides at least one material that the production unit needs.
                        for(int i = 0; i < 4; ++i) {
                            if (candidate.Materials[i] && selectedProdUnit.MaterialsNeeded[i]) {
                                materialMatch = true;
                                break;
                            }
                        }

                        if (materialMatch) {
                            //Add the robot to the production unit.
                            selectedProdUnit.addRobot(candidate);

                            //Remove the card from the player's hand.
                            player1.Hand.drawCard(ind - 3);

                            //Update the hand listbox
                            refreshListBoxPlayer1Hand();

                            if (!auctionsEnded) {
                                //Move onto the next index.
                                buttonNoAddProd_Click(sender, e);
                            } else {
                                refreshProductionRobotInformation();
                            }
                        } else {
                            MessageBox.Show("Please select a card which provides one of the necessary materials.");
                            return;
                        }
                    } else {
                        MessageBox.Show("Please select a robot card to add.");
                        return;
                    }
                } else {
                    MessageBox.Show("Please select a card in your hand to add.");
                    return;
                }
            } else {
                ind = listBoxPlayer2Hand.SelectedIndex;
                if (ind >= 3) {
                    temp = (Card)listBoxPlayer2Hand.Items[ind];
                    if (temp is RobotCard) {
                        candidate = (RobotCard)temp;

                        //Check that the robot provides at least one material that the production unit needs.
                        for (int i = 0; i < 4; ++i) {
                            if (candidate.Materials[i] && selectedProdUnit.MaterialsNeeded[i]) {
                                materialMatch = true;
                                break;
                            }
                        }

                        if (materialMatch) {
                            //Add the robot to the production unit.
                            selectedProdUnit.addRobot(candidate);

                            //Remove the card from the player's hand.
                            player2.Hand.drawCard(ind - 3);

                            //Update the hand listbox
                            refreshListBoxPlayer2Hand();

                            if (!auctionsEnded) {
                                //Move onto the next index.
                                buttonNoAddProd_Click(sender, e);
                            } else {
                                refreshProductionRobotInformation();
                            }
                        } else {
                            MessageBox.Show("Please select a card which provides one of the necessary materials.");
                            return;
                        }
                    } else {
                        MessageBox.Show("Please select a robot card to add.");
                        return;
                    }
                } else {
                    MessageBox.Show("Please select a card in your hand to add.");
                    return;
                }
            }
        }

        /// <summary>
        /// Does not add a card to the selected production unit card, and moves on.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNoAddProd_Click(object sender, EventArgs e) {
            int currentIndex = listBoxProductionCard.SelectedIndex;
            //Check if end of listbox has been reached;
            if(currentIndex < listBoxProductionCard.Items.Count - 1) {
                MessageBox.Show("Done. Now repeat for the next card");
                changeProdIndex(currentIndex + 1);
            } else {
                changeProdIndex(-1);
                listBoxProductionCard.Items.Clear();

                listBoxPlayer1Hand.Items.Clear();

                if (isPlayer1Turn) {
                    putAllplayerCardsInDiscard();

                    if (player2.ProductionUnits.Count > 0) {
                        isPlayer1Turn = false;

                        showAuctionCleanupInstruction(2);

                        listBoxPlayer2Hand.Enabled = true;

                        showProductionInformation();
                        return;
                    } else {
                        MessageBox.Show("Player 2, you have no production cards.");
                    }
                }
                //Player 2 has completed and we start another auction round.

                putAllplayerCardsInDiscard();

                listBoxPlayer2Hand.Items.Clear();

                buttonNoAddProd.Enabled = false;
                buttonAddProd.Enabled = false;

                listBoxProductionCard.Enabled = false;
                listBoxPlayer2Hand.Enabled = false;

                if (deck.Count >= 8) {
                    setUpAuction();
                } else {
                    beginScoringPhase();
                }
                return;
            }
        }

        /// <summary>
        /// Prevents the index of the listbox changing when it shouldn't.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxProductionCard_SelectedIndexChanged(object sender, EventArgs e) {
            if (canChangeProdIndex) {
                prodIndex = listBoxProductionCard.SelectedIndex;
                if (prodIndex > 1) {
                    refreshProductionRobotInformation();    
                }
            } else {
                listBoxProductionCard.SelectedIndex = prodIndex;
            }
        }

        /// <summary>
        /// Changes the selected index for the production card listbox.
        /// </summary>
        /// <param name="i"></param>
        private void changeProdIndex(int i) {
            canChangeProdIndex = true;
            listBoxProductionCard.SelectedIndex = i;
            canChangeProdIndex = false;
        }

        /// <summary>
        /// Counts how many robot cards the player is still holding
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private int countRobotCardsHeld(Player player) {
            int count = 0;
            foreach(Card c in player.Hand.Cards) {
                if(c is RobotCard) {
                    ++count;
                }
            }
            return count;
        }

        private void end() {
            if (player1.Score == player2.Score) {
                if (player1.TiebreakerScore == player2.TiebreakerScore) {
                    MessageBox.Show("It's a tie!");
                } else if (player1.TiebreakerScore < player2.TiebreakerScore) {
                    MessageBox.Show("Player 1 has won!");
                } else {
                    MessageBox.Show("Player 2 has won!");
                }
            } else if (player1.Score > player2.Score) {
                MessageBox.Show("Player 1 has won!");
            } else {
                MessageBox.Show("Player 2 has won!");
            }
            Application.Exit();
        }

        /// <summary>
        /// Begins the scoring phase at the end of the game, where players place all their robots on production cards, assign their materials, and tally up their total score.
        /// </summary>
        private void beginScoringPhase() {
            auctionsEnded = true;

            if (player1.ProductionUnits.Count > 0) {
                MessageBox.Show("Player 1, please place each of your robot cards onto a production unit." +
                                "\nYou can now select specific production unit cards to place each card on." +
                                "\nOnce you are done, click confirm.");

                isPlayer1Turn = true;

                listBoxPlayer1Hand.Enabled = true;

                showProductionInformation();
            } else if (player2.ProductionUnits.Count > 0) {
                MessageBox.Show("Player 1, you have no production units.");
                MessageBox.Show("Player 2, please place each of your robot cards onto a production unit." +
                                "\nYou can now select specific production unit cards to place each card on." +
                                "\nOnce you are done, click confirm.");

                isPlayer1Turn = false;

                listBoxPlayer2Hand.Enabled = true;

                showProductionInformation();
            } else {
                MessageBox.Show("Player 2, you have no production units.");
                tallyPoints();
                return;
            }

            labelConveyorBelt.Visible = false;
            labelProdSets.Visible = true;

            listBoxProductionSets.Enabled = true;
            listBoxProductionCard.Enabled = true;

            buttonAddProd.Enabled = true;
            buttonProductionMaterialConfirm.Enabled = true;

            //Lets the user now change the index themselves.
            canChangeProdIndex = true;
            
        }


        private void tallyPoints() {
            foreach (Card c in player1.DiscardPile.Cards) {
                player1.Score += c.Score;
            }
            foreach (Card c in player1.Hand.Cards) {
                player1.Score += c.Score;
            }
            foreach (ProductionUnitCard p in player1.ProductionUnits.Cards) {
                foreach(Card c in p.Robots) {
                    player1.Score += c.Score;
                }
                //Count sets
                int sets = p.countSets();

                if(sets == 0) {
                    player1.Score -= p.Score;
                } else {
                    player1.Score += p.Score * sets;
                }
            }

            foreach (Card c in player2.DiscardPile.Cards) {
                player2.Score += c.Score;
            }
            foreach (Card c in player2.Hand.Cards) {
                player2.Score += c.Score;
            }
            foreach (ProductionUnitCard p in player2.ProductionUnits.Cards) {
                foreach (Card c in p.Robots) {
                    player2.Score += c.Score;
                }
                //Count sets
                int sets = p.countSets();

                if (sets == 0) {
                    player2.Score -= p.Score;
                } else {
                    player2.Score += p.Score * sets;
                }
            }

            end();
        }

        private void buttonProductionMaterialConfirm_Click(object sender, EventArgs e) {
            if (choosingMaterials) {
                ProductionUnitCard currentProdUnit = (ProductionUnitCard)listBoxProductionCard.SelectedItem;
                if (radioButtonNut.Checked) {
                    currentProdUnit.addNut();
                } else if (radioButtonBolt.Checked) {
                    currentProdUnit.addBolt();
                } else if (radioButtonGear.Checked) {
                    currentProdUnit.addGear();
                } else if (radioButtonOil.Checked) {
                    currentProdUnit.addOil();
                } else {
                    MessageBox.Show("Please select one of the materials to add to the production card.");
                    return;
                }

                int remainingRobots = listBoxProductionSets.Items.Count - 4;
                int currentUnitIndex = listBoxProductionCard.SelectedIndex;

                if(remainingRobots > 0) {
                    //Remove the current robot from the listbox, moving onto the next one
                    listBoxProductionSets.Items.RemoveAt(4);
                    refreshProductionRobotInformation();
                    //Doesn't return
                } else {
                    if(currentUnitIndex < listBoxProductionCard.Items.Count - 1) {
                        changeProdIndex(currentUnitIndex + 1);
                        //Doesn't return
                    } else {
                        //All materials assigned for all production units
                        if (isPlayer1Turn) {
                            //Change to player 2 turn
                            if (player2.ProductionUnits.Count > 0) {
                                MessageBox.Show("Player 2, please select the material that will be provided by each robot card to each production card." +
                                                "\nOnce you are done, click confirm.");
                                isPlayer1Turn = false;

                                setUpListBoxProductionCard();
                                refreshProductionRobotInformation();
                                //Doesn't return
                            } else {
                                MessageBox.Show("Player 2, you have no production cards.");
                                tallyPoints();
                                return;
                            }
                        } else {
                            //End the game
                            tallyPoints();
                            return;
                        }
                    }
                }
                //Update the radio buttons to reflect the new robot card

                RobotCard newRobot = (RobotCard)listBoxProductionSets.Items[listBoxProductionSets.SelectedIndex];
                ProductionUnitCard newProdUnit = (ProductionUnitCard)listBoxProductionCard.Items[listBoxProductionCard.SelectedIndex];

                if (newRobot.givesNut() && newProdUnit.needsNut()) {
                    radioButtonNut.Enabled = true;
                } else {
                    radioButtonNut.Enabled = false;
                }

                if (newRobot.givesBolt() && newProdUnit.needsBolt()) {
                    radioButtonBolt.Enabled = true;
                }else {
                    radioButtonBolt.Enabled = false;
                }

                if (newRobot.givesGear() && newProdUnit.needsGear()) {
                    radioButtonGear.Enabled = true;
                } else {
                    radioButtonGear.Enabled = false;
                }

                if (newRobot.givesOil() && newProdUnit.needsOil()) {
                    radioButtonOil.Enabled = true;
                } else{
                    radioButtonOil.Enabled = false;
                }

            } else {
                if (isPlayer1Turn) {
                    isPlayer1Turn = false;

                    listBoxPlayer1Hand.Items.Clear();

                    MessageBox.Show("Player 2, please place each of your robot cards onto a production unit." +
                                    "\nYou can now select specific production unit cards to place each card on." +
                                    "\nOnce you are done, click confirm.");

                    listBoxPlayer1Hand.Enabled = false;
                    listBoxPlayer2Hand.Enabled = true;

                    showProductionInformation();
                } else {
                    isPlayer1Turn = true;

                    listBoxPlayer2Hand.Items.Clear();
                    listBoxPlayer2Hand.Enabled = false;

                    setUpMaterialAssignment();
                }
            }
        }

        private void setUpMaterialAssignment() {
            choosingMaterials = true;
            canChangeProdIndex = false;

            if (player1.ProductionUnits.Count > 0) {
                MessageBox.Show("Player 1, please select the material that will be provided by each robot card to each production card." +
                                "\nOnce you are done, click confirm.");
            } else {
                MessageBox.Show("Player 1, you have no production cards.");
                if (player2.ProductionUnits.Count > 0) {
                    MessageBox.Show("Player 2, please select the material that will be provided by each robot card to each production card." +
                                    "\nOnce you are done, click confirm.");
                    isPlayer1Turn = false;
                } else {
                    MessageBox.Show("Player 2, you have no production cards.");
                    tallyPoints();
                    return;
                }
            }

            setUpListBoxProductionCard();
            refreshProductionRobotInformation();

            RobotCard firstRobot = (RobotCard)listBoxProductionSets.Items[listBoxProductionSets.SelectedIndex];
            ProductionUnitCard firstProdUnit = (ProductionUnitCard)listBoxProductionCard.Items[listBoxProductionCard.SelectedIndex];

            if(firstRobot.givesNut() && firstProdUnit.needsNut()) {
                radioButtonNut.Enabled = true;
            }
            if (firstRobot.givesBolt() && firstProdUnit.needsBolt()) {
                radioButtonBolt.Enabled = true;
            }
            if (firstRobot.givesGear() && firstProdUnit.needsGear()) {
                radioButtonGear.Enabled = true;
            }
            if (firstRobot.givesOil() && firstProdUnit.needsOil()) {
                radioButtonOil.Enabled = true;
            }
        }
    }
}