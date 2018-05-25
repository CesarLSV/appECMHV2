namespace appECMHV2.ViewModels
{
    using Helpers;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Views;
    using Xamarin.Forms;
    public class MenuItemViewModel
    {
        #region Properties
        public string Icon { get; set; }
        public string Title { get; set; }
        public string PageName { get; set; }
        #endregion


        #region Commands

        public ICommand NavigateCommand
        {
            get
            {
                return new RelayCommand(Navigate);
            }

        }

        private void Navigate()
        {

            App.Master.IsPresented = false;

            if (this.PageName == "LoginPage")
            {

                Settings.Token = string.Empty;
                Settings.TokenType = string.Empty;

                MainViewModel.GetInstance().Token = string.Empty;
                MainViewModel.GetInstance().Token = string.Empty;

                /* Application.Current.MainPage = new LoginPage();*/
                Application.Current.MainPage = new NavigationPage(new LoginPage());

            }


            if (this.PageName == "NotasPage")
            {

                /* Application.Current.MainPage = new LoginPage();*/
                MainViewModel.GetInstance().Notas = new NotasViewModel();
                App.Navigator.PushAsync(new NotasPage());

                //await App.Navigator.PushAsync(new NotasPage());

            }

            if (this.PageName == "HorariosPage")
            {

                /* Application.Current.MainPage = new LoginPage();*/
                MainViewModel.GetInstance().Horarios = new HorariosViewModel();
                App.Navigator.PushAsync(new HorariosPage());

                //await App.Navigator.PushAsync(new NotasPage());

            }


            if (this.PageName == "BibliotecaPage")
            {

                /* Application.Current.MainPage = new LoginPage();*/
                MainViewModel.GetInstance().Biblioteca = new BibliotecaViewModel();
                App.Navigator.PushAsync(new BibliotecaPage());

                //await App.Navigator.PushAsync(new NotasPage());

            }





        }
        #endregion

    }
}
