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
    public partial class FormCRUD : Form
    {
        public FormCRUD()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            new FormCreate().Show();
            this.Hide();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            new FormRead().Show();
            this.Hide();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // DEBUG: Verifique se este método está sendo chamado
            MessageBox.Show("Método btnUpdate_Click foi acionado", "DEBUG",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Abre diretamente o FormUpdate com um ID de teste (remova depois)
            int testUserId = 1; // Substitua por lógica real depois
            FormUpdate formUpdate = new FormUpdate(testUserId);
            formUpdate.ShowDialog();

            // Fecha o FormCRUD se necessário
            this.Hide();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            new FormDelete().ShowDialog();
        }

        private void btnVoltarInicio_Click(object sender, EventArgs e)
        {
            new FormInicio().Show();
            this.Close(); // Fecha o FormCRUD completamente
        }
    }
}