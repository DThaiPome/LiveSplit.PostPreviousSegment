
namespace LiveSplit.UI.Components
{
    partial class PostPreviousSegmentSettings
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.baseApiUrlInput = new System.Windows.Forms.TextBox();
            this.baseApiUrlLabel = new System.Windows.Forms.Label();
            this.betComparisonLabel = new System.Windows.Forms.Label();
            this.splitComparisonLabel = new System.Windows.Forms.Label();
            this.betComparisonInput = new System.Windows.Forms.ComboBox();
            this.splitComparisonInput = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // baseApiUrlInput
            // 
            this.baseApiUrlInput.Location = new System.Drawing.Point(106, 3);
            this.baseApiUrlInput.Name = "baseApiUrlInput";
            this.baseApiUrlInput.Size = new System.Drawing.Size(313, 22);
            this.baseApiUrlInput.TabIndex = 0;
            this.baseApiUrlInput.TextChanged += new System.EventHandler(this.baseApiUrlChanged);
            // 
            // baseApiUrlLabel
            // 
            this.baseApiUrlLabel.AutoSize = true;
            this.baseApiUrlLabel.Location = new System.Drawing.Point(3, 6);
            this.baseApiUrlLabel.Name = "baseApiUrlLabel";
            this.baseApiUrlLabel.Size = new System.Drawing.Size(97, 17);
            this.baseApiUrlLabel.TabIndex = 1;
            this.baseApiUrlLabel.Text = "Base API URL";
            // 
            // betComparisonLabel
            // 
            this.betComparisonLabel.AutoSize = true;
            this.betComparisonLabel.Location = new System.Drawing.Point(3, 35);
            this.betComparisonLabel.Name = "betComparisonLabel";
            this.betComparisonLabel.Size = new System.Drawing.Size(108, 17);
            this.betComparisonLabel.TabIndex = 2;
            this.betComparisonLabel.Text = "Bet Comparison";
            // 
            // splitComparisonLabel
            // 
            this.splitComparisonLabel.AutoSize = true;
            this.splitComparisonLabel.Location = new System.Drawing.Point(3, 62);
            this.splitComparisonLabel.Name = "splitComparisonLabel";
            this.splitComparisonLabel.Size = new System.Drawing.Size(114, 17);
            this.splitComparisonLabel.TabIndex = 3;
            this.splitComparisonLabel.Text = "Split Comparison";
            // 
            // betComparisonInput
            // 
            this.betComparisonInput.FormattingEnabled = true;
            this.betComparisonInput.Items.AddRange(new object[] {
            "Current Comparison",
            "Game Time",
            "Real Time"});
            this.betComparisonInput.Location = new System.Drawing.Point(118, 32);
            this.betComparisonInput.Name = "betComparisonInput";
            this.betComparisonInput.Size = new System.Drawing.Size(301, 24);
            this.betComparisonInput.TabIndex = 4;
            // 
            // splitComparisonInput
            // 
            this.splitComparisonInput.FormattingEnabled = true;
            this.splitComparisonInput.Items.AddRange(new object[] {
            "Current Comparison",
            "Game Time",
            "Real Time"});
            this.splitComparisonInput.Location = new System.Drawing.Point(118, 63);
            this.splitComparisonInput.Name = "splitComparisonInput";
            this.splitComparisonInput.Size = new System.Drawing.Size(301, 24);
            this.splitComparisonInput.TabIndex = 5;
            this.splitComparisonInput.SelectionChangeCommitted += new System.EventHandler(this.splitComparisonInputChanged);
            // 
            // PostPreviousSegmentSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitComparisonInput);
            this.Controls.Add(this.betComparisonInput);
            this.Controls.Add(this.splitComparisonLabel);
            this.Controls.Add(this.betComparisonLabel);
            this.Controls.Add(this.baseApiUrlLabel);
            this.Controls.Add(this.baseApiUrlInput);
            this.Name = "PostPreviousSegmentSettings";
            this.Size = new System.Drawing.Size(422, 113);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox baseApiUrlInput;
        private System.Windows.Forms.Label baseApiUrlLabel;
        private System.Windows.Forms.Label betComparisonLabel;
        private System.Windows.Forms.Label splitComparisonLabel;
        private System.Windows.Forms.ComboBox betComparisonInput;
        private System.Windows.Forms.ComboBox splitComparisonInput;
    }
}
