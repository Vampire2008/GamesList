namespace GamesList
{
    partial class InformationWindows
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InformationWindows));
            this.NameI = new System.Windows.Forms.Label();
            this.icon = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DateOpen = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DateClose = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Description = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.icon)).BeginInit();
            this.SuspendLayout();
            // 
            // NameI
            // 
            this.NameI.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NameI.Location = new System.Drawing.Point(12, 21);
            this.NameI.Name = "NameI";
            this.NameI.Size = new System.Drawing.Size(195, 38);
            this.NameI.TabIndex = 0;
            this.NameI.Text = "Name";
            // 
            // icon
            // 
            this.icon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.icon.Location = new System.Drawing.Point(213, 12);
            this.icon.Name = "icon";
            this.icon.Size = new System.Drawing.Size(240, 205);
            this.icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.icon.TabIndex = 1;
            this.icon.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = " Дата открытия:";
            // 
            // DateOpen
            // 
            this.DateOpen.AutoSize = true;
            this.DateOpen.Location = new System.Drawing.Point(108, 59);
            this.DateOpen.Name = "DateOpen";
            this.DateOpen.Size = new System.Drawing.Size(56, 13);
            this.DateOpen.TabIndex = 3;
            this.DateOpen.Text = "DateOpen";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Дата закрытия:";
            // 
            // DateClose
            // 
            this.DateClose.AutoSize = true;
            this.DateClose.Location = new System.Drawing.Point(108, 83);
            this.DateClose.Name = "DateClose";
            this.DateClose.Size = new System.Drawing.Size(56, 13);
            this.DateClose.TabIndex = 5;
            this.DateClose.Text = "DateClose";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Описание:";
            // 
            // Description
            // 
            this.Description.Location = new System.Drawing.Point(13, 139);
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(194, 78);
            this.Description.TabIndex = 7;
            this.Description.Text = "Description";
            // 
            // InformationWindows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 235);
            this.Controls.Add(this.Description);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DateClose);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DateOpen);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.icon);
            this.Controls.Add(this.NameI);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InformationWindows";
            this.Text = "InformationWindows";
            ((System.ComponentModel.ISupportInitialize)(this.icon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label NameI;
        private System.Windows.Forms.PictureBox icon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label DateOpen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label DateClose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Description;
    }
}