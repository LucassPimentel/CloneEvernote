using EvernoteClone.ViewModel;
using System;
using System.Windows;

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
