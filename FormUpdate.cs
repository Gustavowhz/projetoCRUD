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
    public partial class FormUpdate : Form
    {
        private readonly int _userId;

        public FormUpdate(int userId)
        {
            if (userId <= 0)
            {
                MessageBox.Show("ID de usuário inválido!");
                this.Close();
                return;
            }

            InitializeComponent();
            _userId = userId;
            CarregarDadosUsuario();
            ConfigurarTela();
        }

        private void ConfigurarTela()
        {
            this.Text = "Editar Usuário";
            txtSenha.PasswordChar = '*'; // Para ocultar a senha
            btnSalvar.Text = "Salvar Alterações";
            btnVoltar.Text = "Cancelar";
        }

        private void CarregarDadosUsuario()
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    var command = new SQLiteCommand("SELECT Nome, Senha FROM Users WHERE Id = @Id", connection);
                    command.Parameters.AddWithValue("@Id", _userId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtNome.Text = reader["Nome"].ToString();
                            txtSenha.Text = reader["Senha"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Usuário não encontrado!");
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar dados: {ex.Message}");
                this.Close();
            }
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("O nome não pode estar vazio!");
                return;
            }

            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    var command = new SQLiteCommand(
                        "UPDATE Users SET Nome = @Nome, Senha = @Senha WHERE Id = @Id",
                        connection);

                    command.Parameters.AddWithValue("@Nome", txtNome.Text);
                    command.Parameters.AddWithValue("@Senha", txtSenha.Text);
                    command.Parameters.AddWithValue("@Id", _userId);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Dados atualizados com sucesso!");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Nenhum registro foi atualizado.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar: {ex.Message}");
            }
        }

        // --- ADICIONE ESTE MÉTODO ---
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}