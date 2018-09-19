
namespace appECMHV2.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Microcharts;
    using SkiaSharp.Views.Forms;
    using Xamarin.Forms;

    public class InicioViewModel: BaseViewModel
    {
        #region Attributes

        
        private Chart chart;
        /*private string texto;*/
        #endregion

        #region Properties
        public Chart Chart
        {
            get { return this.chart; }
            set { SetValue(ref this.chart, value); }
        }

        /*
        public string Texto
        {
            get;
            set;
        }
        */
        #endregion

        #region Constructor
        public InicioViewModel()
        {
           // this.Chart = chart;
            this.Graficar();
           // this.Texto = "xyz";
        }
        #endregion

        #region Methods

        
        public void Graficar()
        {
            
            IList<Microcharts.Entry> firstSerie = new List<Microcharts.Entry>();

      

            int PorcentajeTotal = 100;
            int Avance = 45;
            int Restante = PorcentajeTotal - Avance;

            Microcharts.Entry newValue = new Microcharts.Entry(Avance)
            {
                Label = $"{Avance} %",
                Color = Color.Green.ToSKColor(),
                //ValueLabel = 50.ToString(),
                TextColor = Color.Blue.ToSKColor()
            };
            firstSerie.Add(newValue);

            Microcharts.Entry newValue2 = new Microcharts.Entry(Restante)
            {
                //Label = $"Value number {50}",
                Color = Color.LightGray.ToSKColor(),
                //ValueLabel = 50.ToString(),
                TextColor = Color.Blue.ToSKColor()
            };

            firstSerie.Add(newValue2);

            Chart = new DonutChart()
            {
                Entries = firstSerie,
                LabelTextSize = 24,
                Margin = 20
            };
            /*
            Views.InicioPage p = new Views.InicioPage();

            p.chartView1.Chart = new Microcharts.DonutChart()
            {
                Entries = firstSerie,
                LabelTextSize = 24,
                Margin = 20
            };
            */
        }
        #endregion
    }



}
