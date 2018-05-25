namespace appECMHV2.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Views;
    using Xamarin.Forms;

    public   class AlumnoViewModel : BaseViewModel
    {

        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        //  private ObservableCollection<LoggedUser> loggedUser;

        private ObservableCollection<LoggedUser> loggedUser;
        private List<LoggedUser> ListaLoggedUser;

        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public ObservableCollection<LoggedUser> LoggedUser
        {
            get { return this.loggedUser; }
            set { SetValue(ref this.loggedUser, value); }
        }



        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }


        #endregion





        #region Constructors
        public AlumnoViewModel()
        {
            this.apiService = new ApiService();
            this.LoadUserLogged();
            this.IsEnabled = true;

        }



        #endregion


        #region Methods
        private async void LoadUserLogged()
        {
            this.IsRunning = true;
            this.IsEnabled = false;
            // this.IsRefreshing = true;
            var conection = await this.apiService.CheckConnection();

            if (!conection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                //this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    conection.Message,
                    "Accept");
                /*await Application.Current.MainPage.Navigation.PopAsync();*/
                await App.Navigator.PopAsync();
                return;
            }

            var response = await this.apiService.GetList<LoggedUser>(
                "https://sigecmh.monicaherrera.edu.sv/apiECMH/",
                "api/data",
                "/authenticate",
                "bearer",
                MainViewModel.GetInstance().Token);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                //this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                /*await Application.Current.MainPage.Navigation.PopAsync();*/
                await App.Navigator.PopAsync();

                return;
            }

            this.ListaLoggedUser = (List<LoggedUser>)response.Result;
            this.LoggedUser = new ObservableCollection<LoggedUser>(this.ListaLoggedUser);
            //this.IsRefreshing = false;

            //this.Notas = new ObservableCollection<Notas>(this.NotasLista);
            //this.IsRefreshing = false;

            this.IsRunning = false;
            this.IsEnabled = true;
        }
        #endregion

        #region Command

        public ICommand NotasCommand
        {
            get
            {
                return new RelayCommand(Notas);

            }

        }

        private async void Notas()
        {

            MainViewModel.GetInstance().Notas = new NotasViewModel();
            await App.Navigator.PushAsync(new NotasPage());
            /*
            MainViewModel.GetInstance().Notas = new NotasViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new NotasPage());
            */


        }


        public ICommand BibliotecaCommand
        {
            get
            {

                return new RelayCommand(Biblioteca);

            }

        }

        private async void Biblioteca()
        {
            MainViewModel.GetInstance().Biblioteca = new BibliotecaViewModel();
            await App.Navigator.PushAsync(new BibliotecaPage());

            /* MainViewModel.GetInstance().Biblioteca = new BibliotecaViewModel();
             await Application.Current.MainPage.Navigation.PushAsync(new BibliotecaPage());
             */
        }



        public ICommand HorariosCommand
        {
            get
            {

                return new RelayCommand(Horarios);

            }

        }

        private async void Horarios()
        {

            MainViewModel.GetInstance().Horarios = new HorariosViewModel();
            await App.Navigator.PushAsync(new HorariosPage());

            /*
            MainViewModel.GetInstance().Horarios = new HorariosViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new HorariosPage());
            */
        }

        #endregion



    }
}
