namespace appECMHV2.Models
{
    using ViewModels;
    using GalaSoft.MvvmLight.Command;
    using Newtonsoft.Json;
    using System.Windows.Input;
    using Views;
    using Xamarin.Forms;

    public class Notas
    {
        [JsonProperty(PropertyName = "PeopleID")]
        public string PeopleID { get; set; }

        [JsonProperty(PropertyName = "LegalName")]
        public string LegalName { get; set; }

        [JsonProperty("AcademicYear")]
        public string AcademicYear { get; set; }

        [JsonProperty("AcademicTerm")]
        public string AcademicTerm { get; set; }

        [JsonProperty("AcademicSession")]
        public string AcademicSession { get; set; }

        [JsonProperty("EventID")]
        public string EventID { get; set; }

        [JsonProperty("Section")]
        public string Section { get; set; }

        [JsonProperty("EventLongName")]
        public string EventLongName { get; set; }

        [JsonProperty("Carrera")]
        public string Carrera { get; set; }

        [JsonProperty("PrimerParcial")]
        public string PrimerParcial { get; set; }

        [JsonProperty("SegundoParcial")]
        public string SegundoParcial { get; set; }

        [JsonProperty("EvaluacionFinal")]
        public string EvaluacionFinal { get; set; }

        [JsonProperty("Promedio")]
        public string Promedio { get; set; }

        #region Command

        public ICommand SelectLandCommand
        {
            get
            {
                return new RelayCommand(SelectMateria);
            }

        }

        private async void SelectMateria()
        {

            MainViewModel.GetInstance().DetalleMateria = new DetalleMateriaViewModel(this);
            await App.Navigator.PushAsync(new DetalleMateriaPage());

            /* MainViewModel.GetInstance().DetalleMateria = new DetalleMateriaViewModel(this);
             await Application.Current.MainPage.Navigation.PushAsync(new DetalleMateriaPage());
             */
        }

        #endregion


    }
}
