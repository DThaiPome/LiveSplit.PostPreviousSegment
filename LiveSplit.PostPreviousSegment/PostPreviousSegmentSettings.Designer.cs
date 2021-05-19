
namespace LiveSplit.PostPreviousSegment
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
            // PostPreviousSegmentSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.baseApiUrlLabel);
            this.Controls.Add(this.baseApiUrlInput);
            this.Name = "PostPreviousSegmentSettings";
            this.Size = new System.Drawing.Size(422, 32);
            this.Load += new System.EventHandler(this.PostPreviousSegmentSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox baseApiUrlInput;
        private System.Windows.Forms.Label baseApiUrlLabel;
    }
}
