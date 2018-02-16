namespace Gen_Con_Hotel_Watch
{
    partial class EmailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmailForm));
            this.labelEmailSMTP0 = new System.Windows.Forms.Label();
            this.comboBoxSMTP0 = new System.Windows.Forms.ComboBox();
            this.labelEmailFrom0 = new System.Windows.Forms.Label();
            this.textBoxEmailFrom0 = new System.Windows.Forms.TextBox();
            this.labelEmailPassword0 = new System.Windows.Forms.Label();
            this.maskedTextBoxPassword0 = new System.Windows.Forms.MaskedTextBox();
            this.textBoxEmailTo0 = new System.Windows.Forms.TextBox();
            this.labelEmailTo0 = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelEmailSMTP0
            // 
            this.labelEmailSMTP0.AutoSize = true;
            this.labelEmailSMTP0.Location = new System.Drawing.Point(9, 9);
            this.labelEmailSMTP0.Name = "labelEmailSMTP0";
            this.labelEmailSMTP0.Size = new System.Drawing.Size(40, 13);
            this.labelEmailSMTP0.TabIndex = 0;
            this.labelEmailSMTP0.Text = "SMTP:";
            // 
            // comboBoxSMTP0
            // 
            this.comboBoxSMTP0.FormattingEnabled = true;
            this.comboBoxSMTP0.Items.AddRange(new object[] {
            "smtp.gmail.com",
            "smtp.mail.yahoo.com",
            "smtp.live.com",
            "smtp.mail.me.com",
            "smtp.office365.com",
            "smtp.aol.com",
            "smtp.comcast.net",
            "smtp.verizon.net",
            "mail.twc.com"});
            this.comboBoxSMTP0.Location = new System.Drawing.Point(55, 6);
            this.comboBoxSMTP0.Name = "comboBoxSMTP0";
            this.comboBoxSMTP0.Size = new System.Drawing.Size(112, 21);
            this.comboBoxSMTP0.TabIndex = 1;
            this.comboBoxSMTP0.Text = "smtp.gmail.com";
            // 
            // labelEmailFrom0
            // 
            this.labelEmailFrom0.AutoSize = true;
            this.labelEmailFrom0.Location = new System.Drawing.Point(173, 9);
            this.labelEmailFrom0.Name = "labelEmailFrom0";
            this.labelEmailFrom0.Size = new System.Drawing.Size(61, 13);
            this.labelEmailFrom0.TabIndex = 2;
            this.labelEmailFrom0.Text = "From Email:";
            // 
            // textBoxEmailFrom0
            // 
            this.textBoxEmailFrom0.Location = new System.Drawing.Point(240, 6);
            this.textBoxEmailFrom0.Name = "textBoxEmailFrom0";
            this.textBoxEmailFrom0.Size = new System.Drawing.Size(150, 20);
            this.textBoxEmailFrom0.TabIndex = 3;
            // 
            // labelEmailPassword0
            // 
            this.labelEmailPassword0.AutoSize = true;
            this.labelEmailPassword0.Location = new System.Drawing.Point(396, 9);
            this.labelEmailPassword0.Name = "labelEmailPassword0";
            this.labelEmailPassword0.Size = new System.Drawing.Size(84, 13);
            this.labelEmailPassword0.TabIndex = 4;
            this.labelEmailPassword0.Text = "Email Password:";
            // 
            // maskedTextBoxPassword0
            // 
            this.maskedTextBoxPassword0.Location = new System.Drawing.Point(486, 6);
            this.maskedTextBoxPassword0.Name = "maskedTextBoxPassword0";
            this.maskedTextBoxPassword0.PasswordChar = '*';
            this.maskedTextBoxPassword0.Size = new System.Drawing.Size(100, 20);
            this.maskedTextBoxPassword0.TabIndex = 5;
            // 
            // textBoxEmailTo0
            // 
            this.textBoxEmailTo0.Location = new System.Drawing.Point(649, 6);
            this.textBoxEmailTo0.Name = "textBoxEmailTo0";
            this.textBoxEmailTo0.Size = new System.Drawing.Size(150, 20);
            this.textBoxEmailTo0.TabIndex = 7;
            // 
            // labelEmailTo0
            // 
            this.labelEmailTo0.AutoSize = true;
            this.labelEmailTo0.Location = new System.Drawing.Point(592, 9);
            this.labelEmailTo0.Name = "labelEmailTo0";
            this.labelEmailTo0.Size = new System.Drawing.Size(51, 13);
            this.labelEmailTo0.TabIndex = 6;
            this.labelEmailTo0.Text = "To Email:";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAdd.Location = new System.Drawing.Point(12, 37);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 8;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRemove.Enabled = false;
            this.buttonRemove.Location = new System.Drawing.Point(93, 37);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 8;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.ButtonRemove_Click);
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSubmit.Location = new System.Drawing.Point(724, 37);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(75, 23);
            this.buttonSubmit.TabIndex = 17;
            this.buttonSubmit.Text = "Submit";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            this.buttonSubmit.Click += new System.EventHandler(this.ButtonSubmit_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(643, 37);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 17;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // buttonTest
            // 
            this.buttonTest.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonTest.Location = new System.Drawing.Point(376, 37);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(75, 23);
            this.buttonTest.TabIndex = 18;
            this.buttonTest.Text = "Test Email";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.ButtonTest_Click);
            // 
            // FormEmail
            // 
            this.AcceptButton = this.buttonSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(811, 72);
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSubmit);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.textBoxEmailTo0);
            this.Controls.Add(this.labelEmailTo0);
            this.Controls.Add(this.maskedTextBoxPassword0);
            this.Controls.Add(this.labelEmailPassword0);
            this.Controls.Add(this.textBoxEmailFrom0);
            this.Controls.Add(this.labelEmailFrom0);
            this.Controls.Add(this.comboBoxSMTP0);
            this.Controls.Add(this.labelEmailSMTP0);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(827, 110);
            this.Name = "FormEmail";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Email Information";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormEmail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelEmailSMTP0;
        private System.Windows.Forms.ComboBox comboBoxSMTP0;
        private System.Windows.Forms.Label labelEmailFrom0;
        private System.Windows.Forms.TextBox textBoxEmailFrom0;
        private System.Windows.Forms.Label labelEmailPassword0;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxPassword0;
        private System.Windows.Forms.TextBox textBoxEmailTo0;
        private System.Windows.Forms.Label labelEmailTo0;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonSubmit;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonTest;
    }
}