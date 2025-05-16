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
    public partial class FormDelete : Form
    {
        public FormDelete()
        {
            InitializeComponent();
            CarregarUsuarios();
            ConfigurarTela();
        }

        private void ConfigurarTela()
        {
            this.Text = "Excluir Usuário";
            listUsuarios.DisplayMember = "Nome";
            listUsuarios.ValueMember = "Id";
        }

        private void CarregarUsuarios()
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    var command = new SQLiteCommand("SELECT Id, Nome FROM Users", connection);

                    // Cria uma lista de objetos anônimos
                    var users = new List<dynamic>();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new
                            {
                                Id = reader.GetInt32(0),
                                Nome = reader.GetString(1)
                            });
                        }
                    }

                    listUsuarios.DataSource = users;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar usuários: {ex.Message}");
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (listUsuarios.SelectedItem == null)
            {
                MessageBox.Show("Selecione um usuário primeiro!");
                return;
            }

            // Obtém o ID do usuário selecionado
            dynamic selectedItem = listUsuarios.SelectedItem;
            int userId = selectedItem.Id;
            string nomeUsuario = selectedItem.Nome;

            var confirmacao = MessageBox.Show(
                $"Tem certeza que deseja excluir o usuário {nomeUsuario}?",
                "Confirmação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacao == DialogResult.Yes)
            {
                try
                {
                    using (var connection = DatabaseHelper.GetConnection())
                    {
                        connection.Open();
                        var command = new SQLiteCommand(
                            "DELETE FROM Users WHERE Id = @Id",
                            connection);
                        command.Parameters.AddWithValue("@Id", userId);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Usuário excluído com sucesso!");
                            CarregarUsuarios(); // Recarrega a lista
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao excluir: {ex.Message}");
                }
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}