﻿
namespace appECMHV2.Views
{

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPage : MasterDetailPage
	{
        public MasterPage()
        {
            InitializeComponent();
            App.Navigator = Navigator;

            App.Master = this;

        }
    }
}