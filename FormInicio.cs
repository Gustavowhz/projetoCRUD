using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projetoCRUD
{
    public partial class FormInicio : Form
    {
        public FormInicio()
        {
            InitializeComponent();
            //this.BackgroundImage = Image.FromFile("background.jpg"); // Adicione uma imagem profissional
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.Show();
            this.Hide();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
