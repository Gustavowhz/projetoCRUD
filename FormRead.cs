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
using static projetoCRUD.DataSimulator;

namespace projetoCRUD
{
    public partial class FormRead : Form
    {

        public int UsuarioSelecionadoId { get; private set; }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int userId = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells["Id"].Value);
                using (var formUpdate = new FormUpdate(userId))
                {
                    if (formUpdate.ShowDialog() == DialogResult.OK)
                    {
                        // Atualiza a lista apenas se a edição foi confirmada
                        CarregarDados();
                    }
                }
            }
        }

        // Adicione um botão "Selecionar" no seu FormRead
        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                UsuarioSelecionadoId = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["Id"].Value);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        public FormRead()
        {
            InitializeComponent();
            CarregarDados();
            ConfigurarTela();
        }

        private void ConfigurarTela()
        {
            this.Text = "Visualizar Usuários";
            this.StartPosition = FormStartPosition.CenterScreen;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void CarregarDados()
        {

            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SQLiteCommand("SELECT Id, Nome, Senha FROM Users", connection);
                var reader = command.ExecuteReader();

                var users = new List<User>();
                while (reader.Read())
                {
                    users.Add(new User
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nome = reader["Nome"].ToString(),
                        Senha = reader["Senha"].ToString()
                    });
                }

                dataGridView.DataSource = users;
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            FormCRUD formCRUD = new FormCRUD();
            formCRUD.Show();
            this.Hide();
        }
    }
}