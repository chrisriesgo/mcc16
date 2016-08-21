using Xamarin.Forms;

namespace CoolFeatures
{
	public partial class CoolFeaturesPage : ContentPage
	{
		public CoolFeaturesPage()
		{
			InitializeComponent();
			BindingContext = new FeatureViewModel();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			MessagingCenter.Subscribe<FeatureViewModel, string>(this, "ShowFirstTimeAlert", (vm, feature) =>
			{
				Application.Current.MainPage.DisplayAlert(feature, "This is your first time accessing this feature.", "OK");
			});
		}

		protected override void OnDisappearing()
		{
			MessagingCenter.Unsubscribe<FeatureViewModel, string>(this, "ShowFirstTimeAlert");
			base.OnDisappearing();
		}
	}
}