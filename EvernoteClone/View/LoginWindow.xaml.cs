using EvernoteClone.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EvernoteClone.View
{
    /// <summary>
    /// Lógica interna para LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        LoginViewModel LoginViewModel;
        public LoginWindow()
        {
            InitializeComponent();
            // criando link entre viewModel com codeBehind para executar um comando específico, nesse caso fechar janela login
            LoginViewModel = FindResource("viewModel") as LoginViewModel;
            LoginViewModel.Authenticated += LoginViewModel_Authenticated;
        }

        private void LoginViewModel_Authenticated(object sender, EventArgs e)
        {
            Close();
        }
    }
}
