namespace Fzzzt_ {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.listBoxConveyor = new System.Windows.Forms.ListBox();
            this.listBoxPlayer1Hand = new System.Windows.Forms.ListBox();
            this.listBoxPlayer2Hand = new System.Windows.Forms.ListBox();
            this.listBoxPlayer1Bid = new System.Windows.Forms.ListBox();
            this.listBoxPlayer2Bid = new System.Windows.Forms.ListBox();
            this.buttonPlayer2AddCardToBid = new System.Windows.Forms.Button();
            this.buttonPlayer2RetrieveBid = new System.Windows.Forms.Button();
            this.buttonPlayer2Confirm = new System.Windows.Forms.Button();
            this.buttonPlayer1AddCardToBid = new System.Windows.Forms.Button();
            this.buttonPlayer1RetrieveBid = new System.Windows.Forms.Button();
            this.buttonPlayer1Confirm = new System.Windows.Forms.Button();
            this.listBoxProductionCard = new System.Windows.Forms.ListBox();
            this.buttonProductionAddCard = new System.Windows.Forms.Button();
            this.buttonProductionRetrieveCard = new System.Windows.Forms.Button();
            this.buttonProductionConfirm = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listBoxConveyor
            // 
            this.listBoxConveyor.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxConveyor.FormattingEnabled = true;
            this.listBoxConveyor.ItemHeight = 18;
            this.listBoxConveyor.Location = new System.Drawing.Point(12, 240);
            this.listBoxConveyor.Name = "listBoxConveyor";
            this.listBoxConveyor.Size = new System.Drawing.Size(774, 400);
            this.listBoxConveyor.TabIndex = 0;
            this.listBoxConveyor.Visible = false;
            this.listBoxConveyor.SelectedIndexChanged += new System.EventHandler(this.listBoxConveyor_SelectedIndexChanged);
            // 
            // listBoxPlayer1Hand
            // 
            this.listBoxPlayer1Hand.Enabled = false;
            this.listBoxPlayer1Hand.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxPlayer1Hand.FormattingEnabled = true;
            this.listBoxPlayer1Hand.ItemHeight = 18;
            this.listBoxPlayer1Hand.Location = new System.Drawing.Point(12, 665);
            this.listBoxPlayer1Hand.Name = "listBoxPlayer1Hand";
            this.listBoxPlayer1Hand.Size = new System.Drawing.Size(774, 184);
            this.listBoxPlayer1Hand.TabIndex = 1;
            this.listBoxPlayer1Hand.Visible = false;
            // 
            // listBoxPlayer2Hand
            // 
            this.listBoxPlayer2Hand.Enabled = false;
            this.listBoxPlayer2Hand.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxPlayer2Hand.FormattingEnabled = true;
            this.listBoxPlayer2Hand.ItemHeight = 18;
            this.listBoxPlayer2Hand.Location = new System.Drawing.Point(12, 32);
            this.listBoxPlayer2Hand.Name = "listBoxPlayer2Hand";
            this.listBoxPlayer2Hand.Size = new System.Drawing.Size(774, 184);
            this.listBoxPlayer2Hand.TabIndex = 2;
            this.listBoxPlayer2Hand.Visible = false;
            // 
            // listBoxPlayer1Bid
            // 
            this.listBoxPlayer1Bid.Enabled = false;
            this.listBoxPlayer1Bid.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxPlayer1Bid.FormattingEnabled = true;
            this.listBoxPlayer1Bid.ItemHeight = 18;
            this.listBoxPlayer1Bid.Location = new System.Drawing.Point(798, 665);
            this.listBoxPlayer1Bid.Name = "listBoxPlayer1Bid";
            this.listBoxPlayer1Bid.Size = new System.Drawing.Size(774, 184);
            this.listBoxPlayer1Bid.TabIndex = 4;
            this.listBoxPlayer1Bid.Visible = false;
            // 
            // listBoxPlayer2Bid
            // 
            this.listBoxPlayer2Bid.Enabled = false;
            this.listBoxPlayer2Bid.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxPlayer2Bid.FormattingEnabled = true;
            this.listBoxPlayer2Bid.ItemHeight = 18;
            this.listBoxPlayer2Bid.Location = new System.Drawing.Point(798, 32);
            this.listBoxPlayer2Bid.Name = "listBoxPlayer2Bid";
            this.listBoxPlayer2Bid.Size = new System.Drawing.Size(774, 184);
            this.listBoxPlayer2Bid.TabIndex = 5;
            this.listBoxPlayer2Bid.Visible = false;
            // 
            // buttonPlayer2AddCardToBid
            // 
            this.buttonPlayer2AddCardToBid.Enabled = false;
            this.buttonPlayer2AddCardToBid.Location = new System.Drawing.Point(798, 240);
            this.buttonPlayer2AddCardToBid.Name = "buttonPlayer2AddCardToBid";
            this.buttonPlayer2AddCardToBid.Size = new System.Drawing.Size(150, 40);
            this.buttonPlayer2AddCardToBid.TabIndex = 6;
            this.buttonPlayer2AddCardToBid.Text = "Add Selected Card";
            this.buttonPlayer2AddCardToBid.UseVisualStyleBackColor = true;
            this.buttonPlayer2AddCardToBid.Visible = false;
            this.buttonPlayer2AddCardToBid.Click += new System.EventHandler(this.buttonPlayer2AddCardToBid_Click);
            // 
            // buttonPlayer2RetrieveBid
            // 
            this.buttonPlayer2RetrieveBid.Enabled = false;
            this.buttonPlayer2RetrieveBid.Location = new System.Drawing.Point(1110, 240);
            this.buttonPlayer2RetrieveBid.Name = "buttonPlayer2RetrieveBid";
            this.buttonPlayer2RetrieveBid.Size = new System.Drawing.Size(150, 40);
            this.buttonPlayer2RetrieveBid.TabIndex = 7;
            this.buttonPlayer2RetrieveBid.Text = "Retrieve Selected Card";
            this.buttonPlayer2RetrieveBid.UseVisualStyleBackColor = true;
            this.buttonPlayer2RetrieveBid.Visible = false;
            this.buttonPlayer2RetrieveBid.Click += new System.EventHandler(this.buttonPlayer2RetrieveBid_Click);
            // 
            // buttonPlayer2Confirm
            // 
            this.buttonPlayer2Confirm.Enabled = false;
            this.buttonPlayer2Confirm.Location = new System.Drawing.Point(1422, 240);
            this.buttonPlayer2Confirm.Name = "buttonPlayer2Confirm";
            this.buttonPlayer2Confirm.Size = new System.Drawing.Size(150, 40);
            this.buttonPlayer2Confirm.TabIndex = 9;
            this.buttonPlayer2Confirm.Text = "Confirm";
            this.buttonPlayer2Confirm.UseVisualStyleBackColor = true;
            this.buttonPlayer2Confirm.Visible = false;
            this.buttonPlayer2Confirm.Click += new System.EventHandler(this.buttonPlayer2Confirm_Click);
            // 
            // buttonPlayer1AddCardToBid
            // 
            this.buttonPlayer1AddCardToBid.Enabled = false;
            this.buttonPlayer1AddCardToBid.Location = new System.Drawing.Point(798, 600);
            this.buttonPlayer1AddCardToBid.Name = "buttonPlayer1AddCardToBid";
            this.buttonPlayer1AddCardToBid.Size = new System.Drawing.Size(150, 40);
            this.buttonPlayer1AddCardToBid.TabIndex = 10;
            this.buttonPlayer1AddCardToBid.Text = "Add Selected Card";
            this.buttonPlayer1AddCardToBid.UseVisualStyleBackColor = true;
            this.buttonPlayer1AddCardToBid.Visible = false;
            this.buttonPlayer1AddCardToBid.Click += new System.EventHandler(this.buttonPlayer1AddCardToBid_Click);
            // 
            // buttonPlayer1RetrieveBid
            // 
            this.buttonPlayer1RetrieveBid.Enabled = false;
            this.buttonPlayer1RetrieveBid.Location = new System.Drawing.Point(1110, 600);
            this.buttonPlayer1RetrieveBid.Name = "buttonPlayer1RetrieveBid";
            this.buttonPlayer1RetrieveBid.Size = new System.Drawing.Size(150, 40);
            this.buttonPlayer1RetrieveBid.TabIndex = 11;
            this.buttonPlayer1RetrieveBid.Text = "Retrieve Selected Card";
            this.buttonPlayer1RetrieveBid.UseVisualStyleBackColor = true;
            this.buttonPlayer1RetrieveBid.Visible = false;
            this.buttonPlayer1RetrieveBid.Click += new System.EventHandler(this.buttonPlayer1RetrieveBid_Click);
            // 
            // buttonPlayer1Confirm
            // 
            this.buttonPlayer1Confirm.Enabled = false;
            this.buttonPlayer1Confirm.Location = new System.Drawing.Point(1422, 600);
            this.buttonPlayer1Confirm.Name = "buttonPlayer1Confirm";
            this.buttonPlayer1Confirm.Size = new System.Drawing.Size(150, 40);
            this.buttonPlayer1Confirm.TabIndex = 13;
            this.buttonPlayer1Confirm.Text = "Confirm";
            this.buttonPlayer1Confirm.UseVisualStyleBackColor = true;
            this.buttonPlayer1Confirm.Visible = false;
            this.buttonPlayer1Confirm.Click += new System.EventHandler(this.buttonPlayer1Confirm_Click);
            // 
            // listBoxProductionCard
            // 
            this.listBoxProductionCard.Enabled = false;
            this.listBoxProductionCard.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxProductionCard.FormattingEnabled = true;
            this.listBoxProductionCard.ItemHeight = 18;
            this.listBoxProductionCard.Location = new System.Drawing.Point(798, 319);
            this.listBoxProductionCard.Name = "listBoxProductionCard";
            this.listBoxProductionCard.Size = new System.Drawing.Size(774, 184);
            this.listBoxProductionCard.TabIndex = 14;
            this.listBoxProductionCard.Visible = false;
            // 
            // buttonProductionAddCard
            // 
            this.buttonProductionAddCard.Enabled = false;
            this.buttonProductionAddCard.Location = new System.Drawing.Point(798, 509);
            this.buttonProductionAddCard.Name = "buttonProductionAddCard";
            this.buttonProductionAddCard.Size = new System.Drawing.Size(150, 40);
            this.buttonProductionAddCard.TabIndex = 15;
            this.buttonProductionAddCard.Text = "Add Selected Card";
            this.buttonProductionAddCard.UseVisualStyleBackColor = true;
            this.buttonProductionAddCard.Visible = false;
            // 
            // buttonProductionRetrieveCard
            // 
            this.buttonProductionRetrieveCard.Enabled = false;
            this.buttonProductionRetrieveCard.Location = new System.Drawing.Point(1110, 509);
            this.buttonProductionRetrieveCard.Name = "buttonProductionRetrieveCard";
            this.buttonProductionRetrieveCard.Size = new System.Drawing.Size(150, 40);
            this.buttonProductionRetrieveCard.TabIndex = 16;
            this.buttonProductionRetrieveCard.Text = "Retrieve Selected Card";
            this.buttonProductionRetrieveCard.UseVisualStyleBackColor = true;
            this.buttonProductionRetrieveCard.Visible = false;
            // 
            // buttonProductionConfirm
            // 
            this.buttonProductionConfirm.Enabled = false;
            this.buttonProductionConfirm.Location = new System.Drawing.Point(1422, 509);
            this.buttonProductionConfirm.Name = "buttonProductionConfirm";
            this.buttonProductionConfirm.Size = new System.Drawing.Size(150, 40);
            this.buttonProductionConfirm.TabIndex = 17;
            this.buttonProductionConfirm.Text = "Confirm";
            this.buttonProductionConfirm.UseVisualStyleBackColor = true;
            this.buttonProductionConfirm.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 20);
            this.label1.TabIndex = 18;
            this.label1.Text = "Player 2 Hand";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(12, 219);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 20);
            this.label2.TabIndex = 19;
            this.label2.Text = "Conveyor Belt";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.Location = new System.Drawing.Point(12, 642);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 20);
            this.label3.TabIndex = 20;
            this.label3.Text = "Player 1 Hand";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label4.Location = new System.Drawing.Point(794, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.TabIndex = 21;
            this.label4.Text = "Player 2 Bids";
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label5.Location = new System.Drawing.Point(794, 643);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 20);
            this.label5.TabIndex = 22;
            this.label5.Text = "Player 1 Bids";
            this.label5.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label6.Location = new System.Drawing.Point(794, 296);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(160, 20);
            this.label6.TabIndex = 23;
            this.label6.Text = "Production Card Sets";
            this.label6.Visible = false;
            // 
            // buttonStart
            // 
            this.buttonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 42F);
            this.buttonStart.Location = new System.Drawing.Point(640, 350);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(320, 100);
            this.buttonStart.TabIndex = 24;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(795, 283);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(778, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = resources.GetString("label7.Text");
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label7.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(795, 568);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(778, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = resources.GetString("label8.Text");
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label8.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 861);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonProductionConfirm);
            this.Controls.Add(this.buttonProductionRetrieveCard);
            this.Controls.Add(this.buttonProductionAddCard);
            this.Controls.Add(this.listBoxProductionCard);
            this.Controls.Add(this.buttonPlayer1Confirm);
            this.Controls.Add(this.buttonPlayer1RetrieveBid);
            this.Controls.Add(this.buttonPlayer1AddCardToBid);
            this.Controls.Add(this.buttonPlayer2Confirm);
            this.Controls.Add(this.buttonPlayer2RetrieveBid);
            this.Controls.Add(this.buttonPlayer2AddCardToBid);
            this.Controls.Add(this.listBoxPlayer2Bid);
            this.Controls.Add(this.listBoxPlayer1Bid);
            this.Controls.Add(this.listBoxPlayer2Hand);
            this.Controls.Add(this.listBoxPlayer1Hand);
            this.Controls.Add(this.listBoxConveyor);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxConveyor;
        private System.Windows.Forms.ListBox listBoxPlayer1Hand;
        private System.Windows.Forms.ListBox listBoxPlayer2Hand;
        private System.Windows.Forms.ListBox listBoxPlayer1Bid;
        private System.Windows.Forms.ListBox listBoxPlayer2Bid;
        private System.Windows.Forms.Button buttonPlayer2AddCardToBid;
        private System.Windows.Forms.Button buttonPlayer2RetrieveBid;
        private System.Windows.Forms.Button buttonPlayer2Confirm;
        private System.Windows.Forms.Button buttonPlayer1AddCardToBid;
        private System.Windows.Forms.Button buttonPlayer1RetrieveBid;
        private System.Windows.Forms.Button buttonPlayer1Confirm;
        private System.Windows.Forms.ListBox listBoxProductionCard;
        private System.Windows.Forms.Button buttonProductionAddCard;
        private System.Windows.Forms.Button buttonProductionRetrieveCard;
        private System.Windows.Forms.Button buttonProductionConfirm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}

