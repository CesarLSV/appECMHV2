
namespace appECMHV2.ViewModels
{

    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Services;
    using System.Windows.Input;
    using Views;
    using Xamarin.Forms;



    public class LoginViewModel : BaseViewModel
    {


        // #region Events
        // public event PropertyChangedEventHandler PropertyChanged;
        // #endregion


        #region Services
        private ApiService apiService;

        #endregion

        #region Attributes
        private string clave;
        private string usuario;
        private bool isRunning;
        private bool isEnabled;


        #endregion

        #region Properties

        public string Usuario
        {
            get { return this.usuario; }
            set { SetValue(ref this.usuario, value); }
        }

        public string Clave
        {

            get { return this.clave; }
            set { SetValue(ref this.clave, value); }
            //get
            //{
            //    return clave;
            //}
            //set
            //{
            //    if(this.clave != value)
            //    {
            //        this.clave = value;
            //        PropertyChanged?.Invoke(
            //            this, new PropertyChangedEventArgs(nameof(this.Clave)));

            //    }
            //}
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public bool IsRemembered
        {
            get;
            set;
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }



        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.IsRemembered = true;
            this.IsEnabled = true;
            // this.IsRunning = true;

            this.Usuario = "adriana.diaz";
            this.Clave = "Qumu0378";


        }


        #endregion


        #region Commands

        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);

            }

        }



        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Usuario))
            {
                await Application.Current.MainPage.DisplayAlert(
                   Languages.Error,
                    Languages.uservalidator,
                    Languages.Accept);
                return;

            }

            //this.isRunning = true;
            //this.isEnabled = false;


            if (string.IsNullOrEmpty(this.Clave))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.passwordvalidator,
                    Languages.Accept);
                return;

            }

            /*
            if (this.Usuario != "adriana.diaz" || this.Clave != "Qumu0378")
            {
                this.isRunning = false;
                this.isEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Usuario o clave incorrectos!!!.",
                    "Accept");
                this.Clave = string.Empty;
                return;
            }
            */


            this.IsRunning = true;
            this.IsEnabled = false;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                   Languages.Error,
                   connection.Message,
                   Languages.Accept);
                return;
            }

            //await Application.Current.MainPage.DisplayAlert(
            //        "OK",
            //        "Login Correcto.",
            //        "Accept");
            //return;s


            var token = await this.apiService.GetToken("https://sigecmh.monicaherrera.edu.sv/apiECMH/",
                                                       this.usuario,
                                                       this.clave);

            if (token == null)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                   "Error",
                   "An error occurred when validating credentials, try again",
                   "Accept");
                return;
            }


            if (string.IsNullOrEmpty(token.AccessToken))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                   "Error",
                   token.ErrorDescription,
                   "Accept");
                this.Clave = string.Empty;
                return;
            }



            //MainViewModel.GetInstance().Token = token;
            MainViewModel.GetInstance().Token = token.AccessToken;
            MainViewModel.GetInstance().TokenType = token.TokenType;

            if (this.IsRemembered == true)
            {
                Settings.Token = token.AccessToken;
                Settings.TokenType = token.AccessToken;
            }



            MainViewModel.GetInstance().Alumno = new AlumnoViewModel();
            // await Application.Current.MainPage.Navigation.PushAsync(new AlumnoPage());
            Application.Current.MainPage = new MasterPage();

            //this.IsRunning = true;
            //this.IsEnabled = false;



            this.Usuario = string.Empty;
            this.Clave = string.Empty;

            this.IsRunning = false;
            this.IsEnabled = true;

        }




        #endregion


    }
}
