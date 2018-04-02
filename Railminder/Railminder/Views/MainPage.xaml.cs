using System;
using Railminder.ViewModels;
using Xamarin.Forms;

namespace Railminder.Views
{
	public partial class MainPage : ContentPage
	{

		public MainPage(MainPageViewModel mainPageViewModel)
		{
			InitializeComponent();

		    BindingContext = mainPageViewModel;
		}

	    private void Check_OnClicked(object sender, EventArgs e)
	    {
	        var viewModel = (MainPageViewModel) BindingContext;
	        viewModel.CheckForUpcomingTrains();
        }
	}
}
