
namespace appECMHV2.ViewModels
{
    using Helpers;
    using Models;
    using System.Collections.ObjectModel;
    
    public class MainViewModel
    {

        #region ViewModels
        public LoginViewModel Login
        {
            get;
            set;
        }

        public AlumnoViewModel Alumno
        {
            get;
            set;
        }

        public NotasViewModel Notas
        {
            get;
            set;
        }

        public DetalleMateriaViewModel DetalleMateria
        {
            get;
            set;
        }

        public BibliotecaViewModel Biblioteca
        {
            get;
            set;
        }


        public HorariosViewModel Horarios
        {
            get;
            set;
        }

        #endregion


        #region Properties

        /*public TokenResponse Token
        {
            get;
            set;
        }
        */

        public string Token { get; set; }

        public string TokenType { get; set; }


        public TokenResponse UserLog
        {
            get;
            set;
        }

        public ObservableCollection<MenuItemViewModel> Menus
        {
            get;
            set;
        }
        #endregion
        
        #region Constructor

        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
            this.LoadMenu();
        }
        
        #endregion


        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();

            }
            return instance;
        }

        #endregion
        

        #region Methods
        private void LoadMenu()
        {
            this.Menus = new ObservableCollection<MenuItemViewModel>();
            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "qualifications",
                PageName = "NotasPage",
                Title = Languages.myqualifications
            });

            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "schedule",
                PageName = "HorariosPage",
                Title = Languages.myschedules
            });


            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "library",
                PageName = "BibliotecaPage",
                Title = Languages.library
            });

            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "exit",
                PageName = "LoginPage",
                Title = Languages.exit
            });


        }

        #endregion


    }
}
