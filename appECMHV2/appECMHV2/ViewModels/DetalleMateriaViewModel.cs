namespace appECMHV2.ViewModels
{
    using Models;
    public class DetalleMateriaViewModel
    {

        #region Properties


        public Notas NotasLista
        {
            get;
            set;
        }
        #endregion


        #region Constructors
        public DetalleMateriaViewModel(Notas notas)
        {
            this.NotasLista = notas;
        }
        #endregion

    }
}
