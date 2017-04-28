/*  A small windows form application to watch for vacancies in Gen Con's hotel block
 *  Copyright (C) 2017 Aaron Echols (aaronly@gmail.com)
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gen_Con_Hotel_Watch
{
    public partial class FormEmail : Form
    {
        int lastNumber;
        int shift = 26;
        int initialHeight;

        public static EmailInfo[] getEmail;
        FormSearch getForm = (FormSearch)Application.OpenForms["FormSearch"];

        public FormEmail(EmailInfo[] getEmail)
        {
            InitializeComponent();
        }

        private void getEmailInfo()
        {
            getEmail = null;
            Control[] comboBoxSMTPArray = findControl("comboBoxSMTP");
            Control[] textBoxEmailFromArray = findControl("textBoxEmailFrom");
            Control[] maskedTextBoxPasswordArray = findControl("maskedTextBoxPassword");
            Control[] textBoxEmailToArray = findControl("textBoxEmailTo");

            // Get the high number of email addresses possible and resize the array
            Control comboBoxSMTPlast = findNewest(comboBoxSMTPArray);
            int.TryParse(comboBoxSMTPlast.Name.Substring(comboBoxSMTPlast.Name.Length - 1), out lastNumber);
            Array.Resize(ref getEmail, lastNumber + 1);

            for (int i = 0; i < lastNumber + 1; i++)
            {
                string smtpString = comboBoxSMTPArray[i].Text;
                string fromString = textBoxEmailFromArray[i].Text;
                string pwordString = maskedTextBoxPasswordArray[i].Text;
                string toString = textBoxEmailToArray[i].Text;

                if ((fromString == "") | (pwordString == "") | (toString == ""))
                {
                    Array.Resize(ref getEmail, i);
                    return;
                }
                else
                {
                    EmailInfo thisEmail = new EmailInfo();
                    thisEmail.smtp = smtpString;
                    thisEmail.from = fromString;
                    thisEmail.pword = pwordString;
                    thisEmail.to = toString;
                    getEmail[i] = thisEmail;
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            getEmailInfo();
            getForm.emailInfo = getEmail;
            Dispose();
        }
        private void buttonTest_Click(object sender, EventArgs e)
        {
            getEmailInfo();

            string subject = "Test Email from Gen Con Hotel Watch";
            string body = "<html><body><ul>";
            body += "<li><b>Hotel 1</b><ul><li>3 blocks away<br></li></ul></li>";
            body += "<li><b>Hotel 2</b><ul><li>2 blocks away<br></li></ul></li>";
            body += "</ul></body></html>";

            getForm.sendEmail(getEmail, subject, body);
        }


        private Control[] findControl(string search)
        {
            Control[] found = new Control[0];
            int ctr = 1;
            foreach (Control item in Controls)
            {
                if (item.Name.Contains(search))
                {
                    Array.Resize(ref found, ctr);
                    found[ctr-1] = item;
                    ctr += 1;
                }
            }
            return found;
        }
        private Control findNewest(Control[] controls)
        {
            Control result = controls[0];
            if (controls.Length == 1)
                return result;

            for (int i = 1; i < controls.Length; i++)
            {
                string name1 = result.Name;
                string name2 = controls[i].Name;
                int pos1, pos2;
                if (!(int.TryParse(name1.Substring(name1.Length - 1), out pos1)))
                    return result;
                if (!(int.TryParse(name2.Substring(name2.Length - 1), out pos2)))
                    return result;

                if (pos1 < pos2)
                    result = controls[i];
            }

            return result;
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (Height < initialHeight + shift * 9)
            {
                if (Height == initialHeight)
                    buttonRemove.Enabled = true;

                Height = Height + shift;

                if (Height == initialHeight + shift * 9)
                    buttonAdd.Enabled = false;

            }
            else
                return;

            // labelEmailSMTP
            Control[] labelSMTPArray  = findControl("labelEmailSMTP");
            Control labelSMTPlast = findNewest(labelSMTPArray);
            int.TryParse(labelSMTPlast.Name.Substring(labelSMTPlast.Name.Length - 1), out lastNumber);
            Label labelEmailSMTP = new Label();
            labelEmailSMTP.Name = "labelEmailSMTP" + (lastNumber + 1).ToString();
            labelEmailSMTP.Size = new Size(labelSMTPlast.Width, labelSMTPlast.Height);
            labelEmailSMTP.Text = labelSMTPlast.Text;
            labelEmailSMTP.Top = labelSMTPlast.Top + shift;
            labelEmailSMTP.Left = labelSMTPlast.Left;
            Controls.Add(labelEmailSMTP);
            // comboBoxSMTP
            Control[] comboBoxSMTPArray = findControl("comboBoxSMTP");
            Control comboBoxSMTPlast = findNewest(comboBoxSMTPArray);
            int.TryParse(comboBoxSMTPlast.Name.Substring(comboBoxSMTPlast.Name.Length - 1), out lastNumber);
            ComboBox comboBoxSMTP = new ComboBox();
            comboBoxSMTP.Name = "comboBoxSMTP" + (lastNumber + 1).ToString();
            object[] contents = new object[((ComboBox)comboBoxSMTPlast).Items.Count];
            ((ComboBox)comboBoxSMTPlast).Items.CopyTo(contents, 0);
            comboBoxSMTP.Items.AddRange(contents);
            comboBoxSMTP.Size = new System.Drawing.Size(comboBoxSMTPlast.Width, comboBoxSMTPlast.Height);
            comboBoxSMTP.Top = comboBoxSMTPlast.Top + shift;
            comboBoxSMTP.Left = comboBoxSMTPlast.Left;
            comboBoxSMTP.Text = comboBoxSMTPlast.Text;
            Controls.Add(comboBoxSMTP);
            // labelEmailFrom
            Control[] labelEmailFromArray = findControl("labelEmailFrom");
            Control labelEmailFromlast = findNewest(labelEmailFromArray);
            int.TryParse(labelEmailFromlast.Name.Substring(labelEmailFromlast.Name.Length - 1), out lastNumber);
            Label labelEmailFrom = new Label();
            labelEmailFrom.Name = "labelEmailFrom" + (lastNumber + 1).ToString();
            labelEmailFrom.Size = new Size(labelEmailFromlast.Width, labelEmailFromlast.Height);
            labelEmailFrom.Text = labelEmailFromlast.Text;
            labelEmailFrom.Top = labelEmailFromlast.Top + shift;
            labelEmailFrom.Left = labelEmailFromlast.Left;
            Controls.Add(labelEmailFrom);
            // textBoxEmailFrom
            Control[] textBoxEmailFromArray = findControl("textBoxEmailFrom");
            Control textBoxEmailFromlast = findNewest(textBoxEmailFromArray);
            int.TryParse(textBoxEmailFromlast.Name.Substring(textBoxEmailFromlast.Name.Length - 1), out lastNumber);
            TextBox textBoxEmailFrom = new TextBox();
            textBoxEmailFrom.Name = "textBoxEmailFrom" + (lastNumber + 1).ToString();
            textBoxEmailFrom.Size = new Size(textBoxEmailFromlast.Width, textBoxEmailFromlast.Height);
            textBoxEmailFrom.Top = textBoxEmailFromlast.Top + shift;
            textBoxEmailFrom.Left = textBoxEmailFromlast.Left;
            Controls.Add(textBoxEmailFrom);
            // labelEmailPassword
            Control[] labelEmailPasswordArray = findControl("labelEmailPassword");
            Control labelEmailPasswordlast = findNewest(labelEmailPasswordArray);
            int.TryParse(labelEmailPasswordlast.Name.Substring(labelEmailPasswordlast.Name.Length - 1), out lastNumber);
            Label labelEmailPassword = new Label();
            labelEmailPassword.Name = "labelEmailPassword" + (lastNumber + 1).ToString();
            labelEmailPassword.Size = new Size(labelEmailPasswordlast.Width, labelEmailPasswordlast.Height);
            labelEmailPassword.Text = labelEmailPasswordlast.Text;
            labelEmailPassword.Top = labelEmailPasswordlast.Top + shift;
            labelEmailPassword.Left = labelEmailPasswordlast.Left;
            Controls.Add(labelEmailPassword);
            // maskedTextBoxPassword
            Control[] maskedTextBoxPasswordArray = findControl("maskedTextBoxPassword");
            Control maskedTextBoxPasswordlast = findNewest(maskedTextBoxPasswordArray);
            int.TryParse(maskedTextBoxPasswordlast.Name.Substring(maskedTextBoxPasswordlast.Name.Length - 1), out lastNumber);
            MaskedTextBox maskedTextBoxPassword = new MaskedTextBox();
            maskedTextBoxPassword.Name = "maskedTextBoxPassword" + (lastNumber + 1).ToString();
            maskedTextBoxPassword.Size = new Size(maskedTextBoxPasswordlast.Width, maskedTextBoxPasswordlast.Height);
            maskedTextBoxPassword.Top = maskedTextBoxPasswordlast.Top + shift;
            maskedTextBoxPassword.Left = maskedTextBoxPasswordlast.Left;
            maskedTextBoxPassword.PasswordChar = '*';
            Controls.Add(maskedTextBoxPassword);
            // labelEmailTo
            Control[] labelEmailToArray = findControl("labelEmailTo");
            Control labelEmailTolast = findNewest(labelEmailToArray);
            int.TryParse(labelEmailTolast.Name.Substring(labelEmailTolast.Name.Length - 1), out lastNumber);
            Label labelEmailTo = new Label();
            labelEmailTo.Name = "labelEmailTo" + (lastNumber + 1).ToString();
            labelEmailTo.Size = new Size(labelEmailTolast.Width, labelEmailTolast.Height);
            labelEmailTo.Text = labelEmailTolast.Text;
            labelEmailTo.Top = labelEmailTolast.Top + shift;
            labelEmailTo.Left = labelEmailTolast.Left;
            Controls.Add(labelEmailTo);
            // textBoxEmailTo
            Control[] textBoxEmailToArray = findControl("textBoxEmailTo");
            Control textBoxEmailTolast = findNewest(textBoxEmailToArray);
            int.TryParse(textBoxEmailTolast.Name.Substring(textBoxEmailTolast.Name.Length - 1), out lastNumber);
            TextBox textBoxEmailTo = new TextBox();
            textBoxEmailTo.Name = "textBoxEmailTo" + (lastNumber + 1).ToString();
            textBoxEmailTo.Size = new Size(textBoxEmailTolast.Width, textBoxEmailTolast.Height);
            textBoxEmailTo.Top = textBoxEmailTolast.Top + shift;
            textBoxEmailTo.Left = textBoxEmailTolast.Left;
            Controls.Add(textBoxEmailTo);
        }
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (!(Height == initialHeight))
            {
                if (Height == initialHeight + shift)
                    buttonRemove.Enabled = false;
                if (Height == initialHeight + shift * 9)
                    buttonAdd.Enabled = true;
                Height = Height - shift;
            }
            else
                return;

            // labelEmailSMTP
            Control[] labelSMTPArray = findControl("labelEmailSMTP");
            Control labelSMTPlast = findNewest(labelSMTPArray);
            labelSMTPlast.Dispose();
            // comboBoxSMTP
            Control[] comboBoxSMTPArray = findControl("comboBoxSMTP");
            Control comboBoxSMTPlast = findNewest(comboBoxSMTPArray);
            comboBoxSMTPlast.Dispose();
            // labelEmailFrom
            Control[] labelEmailFromArray = findControl("labelEmailFrom");
            Control labelEmailFromlast = findNewest(labelEmailFromArray);
            labelEmailFromlast.Dispose();
            // textBoxEmailFrom
            Control[] textBoxEmailFromArray = findControl("textBoxEmailFrom");
            Control textBoxEmailFromlast = findNewest(textBoxEmailFromArray);
            textBoxEmailFromlast.Dispose();
            // labelEmailPassword
            Control[] labelEmailPasswordArray = findControl("labelEmailPassword");
            Control labelEmailPasswordlast = findNewest(labelEmailPasswordArray);
            labelEmailPasswordlast.Dispose();
            // maskedTextBoxPassword
            Control[] maskedTextBoxPasswordArray = findControl("maskedTextBoxPassword");
            Control maskedTextBoxPasswordlast = findNewest(maskedTextBoxPasswordArray);
            maskedTextBoxPasswordlast.Dispose();
            // labelEmailTo
            Control[] labelEmailToArray = findControl("labelEmailTo");
            Control labelEmailTolast = findNewest(labelEmailToArray);
            labelEmailTolast.Dispose();
            // textBoxEmailTo
            Control[] textBoxEmailToArray = findControl("textBoxEmailTo");
            Control textBoxEmailTolast = findNewest(textBoxEmailToArray);
            textBoxEmailTolast.Dispose();
        }

        private void FormEmail_Load(object sender, EventArgs e)
        {
            initialHeight = Height;
            if (getEmail != null)
            {
                for (int i = 0; i < getEmail.Length; i++)
                {
                    if (i > 0)
                        buttonAdd.PerformClick();

                    Control[] comboBoxSMTPArray = findControl("comboBoxSMTP");
                    Control[] textBoxEmailFromArray = findControl("textBoxEmailFrom");
                    Control[] maskedTextBoxPasswordArray = findControl("maskedTextBoxPassword");
                    Control[] textBoxEmailToArray = findControl("textBoxEmailTo");

                    Control comboBoxSMTPlast = findNewest(comboBoxSMTPArray);
                    Control textBoxEmailFromlast = findNewest(textBoxEmailFromArray);
                    Control maskedTextBoxPasswordlast = findNewest(maskedTextBoxPasswordArray);
                    Control textBoxEmailTolast = findNewest(textBoxEmailToArray);

                    comboBoxSMTPlast.Text = getEmail[i].smtp;
                    textBoxEmailFromlast.Text = getEmail[i].from;
                    maskedTextBoxPasswordlast.Text = getEmail[i].pword;
                    textBoxEmailTolast.Text = getEmail[i].to;
                }
            }
        }
    }
}
