namespace ImageDifference.Gui
{
    partial class winImageDifference
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bttnStoreImage = new System.Windows.Forms.Button();
            this.bttnFindDifferenceToImage = new System.Windows.Forms.Button();
            this.grpbxReplaceMode = new System.Windows.Forms.GroupBox();
            this.chkbxInvertPicture = new System.Windows.Forms.CheckBox();
            this.chkbxInvertArea = new System.Windows.Forms.CheckBox();
            this.grpbxReplaceMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // bttnStoreImage
            // 
            this.bttnStoreImage.Location = new System.Drawing.Point(12, 12);
            this.bttnStoreImage.Name = "bttnStoreImage";
            this.bttnStoreImage.Size = new System.Drawing.Size(260, 23);
            this.bttnStoreImage.TabIndex = 0;
            this.bttnStoreImage.Text = "Store Unedited Image";
            this.bttnStoreImage.UseVisualStyleBackColor = true;
            this.bttnStoreImage.Click += new System.EventHandler(this.bttnStoreImage_Click);
            // 
            // bttnFindDifferenceToImage
            // 
            this.bttnFindDifferenceToImage.BackColor = System.Drawing.SystemColors.ControlLight;
            this.bttnFindDifferenceToImage.Location = new System.Drawing.Point(12, 142);
            this.bttnFindDifferenceToImage.Name = "bttnFindDifferenceToImage";
            this.bttnFindDifferenceToImage.Size = new System.Drawing.Size(260, 23);
            this.bttnFindDifferenceToImage.TabIndex = 2;
            this.bttnFindDifferenceToImage.Text = "Find Difference To Current Image";
            this.bttnFindDifferenceToImage.UseVisualStyleBackColor = false;
            this.bttnFindDifferenceToImage.Click += new System.EventHandler(this.bttnFindDifference_Click);
            // 
            // grpbxReplaceMode
            // 
            this.grpbxReplaceMode.Controls.Add(this.chkbxInvertPicture);
            this.grpbxReplaceMode.Controls.Add(this.chkbxInvertArea);
            this.grpbxReplaceMode.Location = new System.Drawing.Point(12, 41);
            this.grpbxReplaceMode.Name = "grpbxReplaceMode";
            this.grpbxReplaceMode.Size = new System.Drawing.Size(260, 96);
            this.grpbxReplaceMode.TabIndex = 3;
            this.grpbxReplaceMode.TabStop = false;
            this.grpbxReplaceMode.Text = "Replace Options";
            // 
            // chkbxInvertPicture
            // 
            this.chkbxInvertPicture.AutoSize = true;
            this.chkbxInvertPicture.Location = new System.Drawing.Point(6, 42);
            this.chkbxInvertPicture.Name = "chkbxInvertPicture";
            this.chkbxInvertPicture.Size = new System.Drawing.Size(136, 17);
            this.chkbxInvertPicture.TabIndex = 1;
            this.chkbxInvertPicture.Text = "Invert Resulting Picture";
            this.chkbxInvertPicture.UseVisualStyleBackColor = true;
            this.chkbxInvertPicture.CheckedChanged += new System.EventHandler(this.chkbxInvertResultingPicture_CheckedChanged);
            // 
            // chkbxInvertArea
            // 
            this.chkbxInvertArea.AutoSize = true;
            this.chkbxInvertArea.Location = new System.Drawing.Point(6, 19);
            this.chkbxInvertArea.Name = "chkbxInvertArea";
            this.chkbxInvertArea.Size = new System.Drawing.Size(118, 17);
            this.chkbxInvertArea.TabIndex = 0;
            this.chkbxInvertArea.Text = "Invert Isolated Area";
            this.chkbxInvertArea.UseVisualStyleBackColor = true;
            this.chkbxInvertArea.CheckedChanged += new System.EventHandler(this.chkbxInvertArea_CheckedChanged);
            // 
            // winImageDifference
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 188);
            this.Controls.Add(this.grpbxReplaceMode);
            this.Controls.Add(this.bttnFindDifferenceToImage);
            this.Controls.Add(this.bttnStoreImage);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "winImageDifference";
            this.Text = "Image Difference";
            this.grpbxReplaceMode.ResumeLayout(false);
            this.grpbxReplaceMode.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bttnStoreImage;
        private System.Windows.Forms.Button bttnFindDifferenceToImage;
        private System.Windows.Forms.GroupBox grpbxReplaceMode;
        private System.Windows.Forms.CheckBox chkbxInvertArea;
        private System.Windows.Forms.CheckBox chkbxInvertPicture;
    }
}