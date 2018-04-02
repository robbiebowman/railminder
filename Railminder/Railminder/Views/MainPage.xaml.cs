using System;
using Railminder.Models;
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

	    private void Button_OnClicked(object sender, EventArgs e)
	    {
	        var button = (Button) sender;
	        var train = (TrainInfo) button.BindingContext;
	        var viewModel = (MainPageViewModel)BindingContext;
	        viewModel.ScheduleNotification(train);
        }
	}
}
