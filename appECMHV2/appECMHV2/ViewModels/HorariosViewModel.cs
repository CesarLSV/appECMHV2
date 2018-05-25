namespace appECMHV2.ViewModels
{
    using Models;
    using Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System;
    using Xamarin.Forms;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using System.Linq;

    public  class HorariosViewModel : BaseViewModel
    {

        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes

        private ObservableCollection<MHorarios> horariosAlumno;
        private List<MHorarios> ListaHorarios;

        private bool isRunning;
        private bool isEnabled;
        private bool isRefreshing;
        private string filter;

        #endregion

        #region Properties
        public ObservableCollection<MHorarios> HorariosAlumno
        {
            get { return this.horariosAlumno; }
            set { SetValue(ref this.horariosAlumno, value); }
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
        public HorariosViewModel()
        {
            this.apiService = new ApiService();
            this.LoadHorariosAlumno();
            this.IsEnabled = true;

        }
        #endregion

        #region Methods
        private async void LoadHorariosAlumno()
        {
            // this.IsRunning = true;
            //this.IsEnabled = false;
            this.IsRefreshing = true;
            var conection = await this.apiService.CheckConnection();

            if (!conection.IsSuccess)
            {
                //this.IsRunning = false;
                //this.IsEnabled = true;
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    conection.Message,
                    "Accept");
                /* await Application.Current.MainPage.Navigation.PopAsync();*/
                await App.Navigator.PopAsync();
                return;
            }

            var response = await this.apiService.GetList<MHorarios>(
                "https://sigecmh.monicaherrera.edu.sv/apiECMH/",
                "api/data",
                "/horarios",
                "bearer",
                MainViewModel.GetInstance().Token);

            if (!response.IsSuccess)
            {
                //this.IsRunning = false;
                //this.IsEnabled = true;
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                /* await Application.Current.MainPage.Navigation.PopAsync();*/
                await App.Navigator.PopAsync();
                return;
            }

            this.ListaHorarios = (List<MHorarios>)response.Result;
            this.HorariosAlumno = new ObservableCollection<MHorarios>(this.ListaHorarios);
            this.IsRefreshing = false;

            // this.IsRunning = false;
            //this.IsEnabled = true;
        }
        #endregion


        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadHorariosAlumno);

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
                this.HorariosAlumno = new ObservableCollection<MHorarios>(this.ListaHorarios);
            }
            else
            {
                this.HorariosAlumno = new ObservableCollection<MHorarios>(
                    this.ListaHorarios.Where(n => n.EventLongName.ToLower().Contains(this.filter.ToLower()) ||
                                               n.Section.ToLower().Contains(this.filter.ToLower())));
            }
        }



        #endregion

    }
}
