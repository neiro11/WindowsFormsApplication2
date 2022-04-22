using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace WindowsFormsApplication2
{
    public partial class RecoverAcc : Form
    {
        public RecoverAcc()
        {
            InitializeComponent();
        }
        private User _user;
        private void buttonSendPassword_Click(object sender, EventArgs e)
        {
            try
            {
                MailAddress from = new MailAddress("ildarado4@mail.ru", "ildar sitdikov");
                MailAddress to = new MailAddress(textBoxEmail.Text);
                MailMessage m = new MailMessage(from, to);
                m.Subject = "Пароль для входа в аккаунт";
                
                using (UserContext db = new UserContext())
                {
                    foreach (User user in db.Users)
                    {
                        if (textBoxEmail.Text == user.Email)
                        {
                            _user = user;
                            m.Body = "<h1>Пароль: " + user.Password + "</h1>";
                        }
                    }
                }
                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.inbox.ru", 587);
                smtp.Credentials = new NetworkCredential("ildarado4@mail.ru", "X8Smm1nGbfGTKkc0xnc4");
                smtp.EnableSsl = true;
                smtp.Send(m);
                textBoxEmail.ReadOnly = true;
                MessageBox.Show("Письмо отправлено");
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Authorization auth = new Authorization();
            auth.Show();
        }

        private void RecoverAcc_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
