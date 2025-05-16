using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projetoCRUD
{
    internal static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => {
                MessageBox.Show($"Erro não tratado: {e.ExceptionObject}");
            };

            Application.ThreadException += (sender, e) => {
                MessageBox.Show($"Erro na thread: {e.Exception.Message}");
            };

            DatabaseHelper.InicializarBancoDeDados();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormInicio());
        }
    }
}