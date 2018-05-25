namespace appECMHV2.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;


    public class NotasViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion


        #region Attributes
        private ObservableCollection<Notas> notas;
        private bool isRefreshing;
        private string filter;
        private List<Notas> NotasLista;
        #endregion


        #region Properties
        public ObservableCollection<Notas> Notas
        {
            get { return this.notas; }
            set { SetValue(ref this.notas, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }


        public string Filter
        {
            get { return this.filter; }
            set
            {

                SetValue(ref this.filter, value);
                this.Search();
            }
        }
        #endregion

        #region Constructors
        public NotasViewModel()
        {
            this.apiService = new ApiService();
            this.LoadNotas();

        }

        #endregion

        #region Methods
        private async void LoadNotas()
        {
            this.IsRefreshing = true;
            var conection = await this.apiService.CheckConnection();

            if (!conection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    conection.Message,
                    "Accept");
                /*await Application.Current.MainPage.Navigation.PopAsync();*/
                await App.Navigator.PopAsync();
                return;
            }

            var response = await this.apiService.GetList<Notas>(
                "https://sigecmh.monicaherrera.edu.sv/apiECMH/",
                "api/data",
                "/notas",
                "bearer",
                MainViewModel.GetInstance().Token);

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                /* await Application.Current.MainPage.Navigation.PopAsync();*/
                await App.Navigator.PopAsync();
                return;
            }

            this.NotasLista = (List<Notas>)response.Result;
            this.Notas = new ObservableCollection<Notas>(this.NotasLista);
            this.IsRefreshing = false;
        }
        #endregion


        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadNotas);

            }

        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }

        private void Search()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Notas = new ObservableCollection<Notas>(this.NotasLista);
            }
            else
            {
                this.Notas = new ObservableCollection<Notas>(
                    this.NotasLista.Where(n => n.EventLongName.ToLower().Contains(this.filter.ToLower()) ||
                                               n.Section.ToLower().Contains(this.filter.ToLower())));
            }
        }



        #endregion

    }
}
