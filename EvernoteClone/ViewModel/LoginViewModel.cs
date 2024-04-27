using EvernoteClone.Model;
using EvernoteClone.ViewModel.Commands;
using EvernoteClone.ViewModel.Helpers;
using System;
using System.ComponentModel;
using System.Windows;

namespace EvernoteClone.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private bool isShowingRegister = true;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler Authenticated;
        public RegisterCommand RegisterCommand { get; set; }
        public LoginCommand LoginCommand { get; set; }
        public ShowRegisterCommand ShowRegisterCommand { get; set; }

        private User _user;
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                User = new User()
                {
                    Password = this.Password,
                    Name = this.Name,
                    Lastname = this.Lastname,
                    ConfirmPassword = this.ConfirmPassword

                };
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                User = new User()
                {
                    Password = _password,
                    Name = this.Name,
                    Lastname = this.Lastname,
                    ConfirmPassword = this.ConfirmPassword

                };
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                User = new User()
                {
                    Name = _name,
                    Password = this.Password,
                    Lastname = this.Lastname,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _lastName;
        public string Lastname
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                User = new User()
                {
                    Name = this.Name,
                    Lastname = _lastName,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged(nameof(Lastname));
            }
        }

        private string _confirmPassword;

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                User = new User()
                {
                    Name = this.Name,
                    Lastname = this.Lastname,
                    Password = this.Password,
                    ConfirmPassword = _confirmPassword
                };
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        private Visibility _loginVisibility;
        public Visibility LoginVisibility
        {
            get { return _loginVisibility; }
            set
            {
                _loginVisibility = value;
                OnPropertyChanged(nameof(LoginVisibility));
            }
        }

        private Visibility _registerVisibility;

        public Visibility RegisterVisibility
        {
            get { return _registerVisibility; }
            set
            {
                _registerVisibility = value;
                OnPropertyChanged(nameof(RegisterVisibility));
            }
        }


        public LoginViewModel()
        {
            LoginVisibility = Visibility.Visible;
            RegisterVisibility = Visibility.Collapsed;

            RegisterCommand = new RegisterCommand(this);
            LoginCommand = new LoginCommand(this);
            ShowRegisterCommand = new ShowRegisterCommand(this);

            User = new User();
        }

        public void SwitchViews()
        {
            isShowingRegister = !isShowingRegister;

            if (isShowingRegister)
            {
                RegisterVisibility = Visibility.Visible;
                LoginVisibility = Visibility.Collapsed;
            }
            else
            {
                RegisterVisibility = Visibility.Collapsed;
                LoginVisibility = Visibility.Visible;
            }
        }

        public void Login()
        {
            var isLoggedIn = LoginHelper.Login(User);
            if (isLoggedIn)
            {
                Authenticated?.Invoke(this, new EventArgs());
            }
        }

        public void Register()
        {
            var isRegistered = LoginHelper.Register(User);
            if (isRegistered)
            {
                Authenticated?.Invoke(this, new EventArgs());
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
