using System;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Plugin.LocalNotifications;
using Xamarin.Forms;

namespace MCCPlugins
{
	public class App : Application
	{
		public App()
		{
			MainPage = new NavigationPage(new HomePage());
		}

		public class HomePage : ContentPage
		{
			Button button;

			public HomePage()
			{
				Title = "MCC Plugins";
				button = new Button { Text = "Tap Me" };
				button.Clicked += Button_Clicked;

				this.Content = new ContentView
				{
					Content = new StackLayout
					{
						VerticalOptions = LayoutOptions.Center,
						Children = { button }
					}
				};
			}

			protected override void OnAppearing()
			{
				base.OnAppearing();
				// LOOK:1. Handle the ConnectivityChanged event
				CrossConnectivity.Current.ConnectivityChanged += ConnectivityChanged;
				button.IsEnabled = CrossConnectivity.Current.IsConnected;
			}

			void Button_Clicked(object sender, EventArgs e)
			{
				// LOOK:3. Show a native device notification when tapped
				CrossLocalNotifications.Current.Show(
					"MCC Plugins",
					"You tapped the button",
					0, DateTime.Now.AddSeconds(5));
			}

			void ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
			{
				// LOOK:2. Set the button's IsEnabled property when connectivity changes
				button.IsEnabled = e.IsConnected;
			}
		}
	}
}