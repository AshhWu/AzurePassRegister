namespace WindowsFormsApplication2
{
    partial class Form
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
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.infoText = new System.Windows.Forms.TextBox();
            this.refreshButtom = new System.Windows.Forms.Button();
            this.url = new System.Windows.Forms.TextBox();
            this.GoButtom = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // webBrowser
            // 
            this.webBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser.Location = new System.Drawing.Point(0, 53);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(1057, 684);
            this.webBrowser.TabIndex = 0;
            this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
            // 
            // infoText
            // 
            this.infoText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.infoText.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.infoText.Location = new System.Drawing.Point(0, 26);
            this.infoText.Name = "infoText";
            this.infoText.Size = new System.Drawing.Size(1057, 25);
            this.infoText.TabIndex = 1;
            this.infoText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.infoText.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // refreshButtom
            // 
            this.refreshButtom.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.refreshButtom.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.refreshButtom.Font = new System.Drawing.Font("PMingLiU", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.refreshButtom.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.refreshButtom.Location = new System.Drawing.Point(982, 1);
            this.refreshButtom.Name = "refreshButtom";
            this.refreshButtom.Size = new System.Drawing.Size(75, 25);
            this.refreshButtom.TabIndex = 2;
            this.refreshButtom.Text = "refresh";
            this.refreshButtom.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.refreshButtom.UseVisualStyleBackColor = false;
            this.refreshButtom.Click += new System.EventHandler(this.refresh_Click);
            // 
            // url
            // 
            this.url.Font = new System.Drawing.Font("PMingLiU", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.url.Location = new System.Drawing.Point(0, 1);
            this.url.Name = "url";
            this.url.Size = new System.Drawing.Size(1057, 25);
            this.url.TabIndex = 4;
            this.url.TextChanged += new System.EventHandler(this.url_TextChanged);
            // 
            // GoButtom
            // 
            this.GoButtom.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.GoButtom.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.GoButtom.Location = new System.Drawing.Point(929, 1);
            this.GoButtom.Name = "GoButtom";
            this.GoButtom.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.GoButtom.Size = new System.Drawing.Size(54, 25);
            this.GoButtom.TabIndex = 5;
            this.GoButtom.Text = "Go";
            this.GoButtom.UseVisualStyleBackColor = false;
            this.GoButtom.Click += new System.EventHandler(this.GoButtom_Click);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 733);
            this.Controls.Add(this.GoButtom);
            this.Controls.Add(this.refreshButtom);
            this.Controls.Add(this.infoText);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.url);
            this.Name = "Form";
            this.Text = "Azure Pass Register";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.TextBox infoText;
        private System.Windows.Forms.Button refreshButtom;
        private System.Windows.Forms.TextBox url;
        private System.Windows.Forms.Button GoButtom;
    }
}

