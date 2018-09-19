namespace appECMHV2.ViewModels
{
    
    using Com.OneSignal;
    using Com.OneSignal.Abstractions;
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Services;
    using System;
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
              
        public string IdPlayer { get; set; }
        public string IdDevice { get; set; }
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
            // Se registra si ya hay una sesion iniciada. 
            //OneSignal.Current.StartInit("28ee355b-c341-4d30-a51e-e404526823dc")
            //  .EndInit();
            

            OneSignal.Current.IdsAvailable(new Com.OneSignal.Abstractions.IdsAvailableCallback((PlayerID, pushToken) =>
            {
                //IdPlayer = $"Player ID de este device:\n{PlayerID}";
                IdPlayer = PlayerID;

            }));

            
        //public abstract void GetTags(TagsReceived inTagsReceivedDelegate);

        var respuesta = await apiService.PostRegistrarPlayerID<MHorarios>(
               "https://sigecmh.monicaherrera.edu.sv/apiECMH/api/data/playerID",
               "bearer",
               MainViewModel.GetInstance().Token,
               this.IdPlayer,
               this.IdDevice);


            //this.IsRefreshing = false;

            //  ShowPlayerIdHandler();
            //this.Notas = new ObservableCollection<Notas>(this.NotasLista);
            //this.IsRefreshing = false;

            this.IsRunning = false;
            this.IsEnabled = true;
        }

        

        private void TagsReceived(Dictionary<string, object> tags)
        {
            foreach (var tag in tags)
                Console.WriteLine(tag.Key + ":" + tag.Value);
        }


        //private static void TagsReceived(Dictionary<string, object> tags)
        //{
        //    foreach (var tag in tags)
        //      Console.WriteLine(tag.Key + ":" + tag.Value);
        //}


        //Llamado desde un boton 
        // <Button x:Name="showPlayerIdButton" Text="Mostrar Player Id" VerticalOptions="Center" HorizontalOptions="Center" Clicked="ShowPlayerIdHandler" />
        private async void ShowPlayerIdHandler()
        {
          

            OneSignal.Current.IdsAvailable(new Com.OneSignal.Abstractions.IdsAvailableCallback((Device, pushToken) =>
            {


                //IdPlayer = $"Player ID de este device:\n{PlayerID}";
                IdDevice = Device;

            }));



           


           


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
