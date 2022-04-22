using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace WindowsFormsApplication2
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void ButtonLogIn_Click(object sender, EventArgs e)
        {
            using (UserContext db = new UserContext())
            {
                foreach (User user in db.Users)
                {

                    if (textBoxLog.Text == user.Login && this.GetHashString(textBoxPass.Text) == user.Password)
                    {
                        UserForm userForm = new UserForm();
                        this.Hide();
                        MessageBox.Show("Вы вошли под учетной записью " + user.Login);
                        userForm.Show();
                        return;
                    }
                }
                MessageBox.Show("Логин или пароль указан неверно!");
            }

        }
        private string GetHashString(string s)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(s);

            MD5CryptoServiceProvider CSP = new
            MD5CryptoServiceProvider();
            byte[] byteHash = CSP.ComputeHash(bytes);
            string hash = "";
            foreach (byte b in byteHash)
            {
                hash += string.Format("{0:x2}", b);
            }
            return hash;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Registration registration = new Registration();
            this.Hide();
            registration.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            RecoverAcc recoveracc = new RecoverAcc();
            this.Hide();
            recoveracc.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (textBoxPass.UseSystemPasswordChar == true)
            {
                textBoxPass.UseSystemPasswordChar = false;
            }
            else
            {
                textBoxPass.UseSystemPasswordChar = true;
            }
        }

        private void Authorization_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
