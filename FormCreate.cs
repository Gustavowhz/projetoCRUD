using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projetoCRUD
{
    public partial class FormCreate : Form
    {
        public FormCreate()
        {
            InitializeComponent();
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            // Configurações básicas do formulário
            this.Text = "Cadastrar Novo Usuário";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Configuração dos controles (você pode fazer isso no designer também)
            lblNome.Text = "Nome:";
            lblNome.Location = new System.Drawing.Point(20, 20);

            txtNome.Location = new System.Drawing.Point(120, 20);
            txtNome.Size = new System.Drawing.Size(200, 20);

            lblSenha.Text = "Senha:";
            lblSenha.Location = new System.Drawing.Point(20, 60);

            txtSenha.Location = new System.Drawing.Point(120, 60);
            txtSenha.Size = new System.Drawing.Size(200, 20);
            txtSenha.PasswordChar = '*';  // Para ocultar a senha

            btnSalvar.Text = "Salvar";
            btnSalvar.Location = new System.Drawing.Point(120, 100);

            btnVoltar.Text = "Voltar";
            btnVoltar.Location = new System.Drawing.Point(220, 100);
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SQLiteCommand(
                    "INSERT INTO Users (Nome, Senha) VALUES (@Nome, @Senha)",
                    connection);

                command.Parameters.AddWithValue("@Nome", txtNome.Text);
                command.Parameters.AddWithValue("@Senha", txtSenha.Text);
                command.ExecuteNonQuery();
            }

            MessageBox.Show("Usuário cadastrado no banco de dados!");
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Por favor, informe o nome!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                MessageBox.Show("Por favor, informe a senha!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSenha.Focus();
                return false;
            }

            return true;
        }

        private void LimparCampos()
        {
            txtNome.Clear();
            txtSenha.Clear();
            txtNome.Focus();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            FormCRUD formCRUD = new FormCRUD();
            formCRUD.Show();
            this.Hide();
        }
    }
}