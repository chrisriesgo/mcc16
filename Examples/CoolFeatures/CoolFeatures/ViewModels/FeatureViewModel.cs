using Xamarin.Forms;
using System.Windows.Input;
using Plugin.FeatureTracking;
using System.ComponentModel;

namespace CoolFeatures
{
	public class FeatureViewModel : INotifyPropertyChanged
	{
		const string FEATURE_NAME = "CoolFeature";

		private ICommand showFeatureCommand;
		public ICommand ShowFeatureCommand
		{
			get
			{
				if(showFeatureCommand == null)
				{
					showFeatureCommand = new Command(ShowFeature);
				}
				return showFeatureCommand;
			}
		}
		private ICommand removeAllFeaturesCommand;
		/// <summary>
		/// Executing this command will clear all tracked features on the device.
		/// </summary>
		public ICommand RemoveAllFeaturesCommand 
		{
			get
			{
				if(removeAllFeaturesCommand == null)
				{
					//LOOK: 3. Executing this command will clear all tracked features on the device.
					removeAllFeaturesCommand = new Command(CrossFeatureTracking.Current.RemoveAll);
				}
				return removeAllFeaturesCommand;
			}
		}

		private void ShowFeature()
		{
			CrossFeatureTracking.Current.OnFirstLaunch(FEATURE_NAME, () =>
			{
				//LOOK: 1. If it's the first time for you to use this feature on the device, 
				//	you will receive an alert dialog.
				MessagingCenter.Send(this, "ShowFirstTimeAlert", FEATURE_NAME);
			});

			//LOOK: 2. Look at your DEBUG application output.
			//		If this is the first time using the named Feature, 
			//		you will see a "Plugin.FeatureTracking:" output message indicating that the 
			//		feature is now being tracked on the platform you executed the app on.
			CrossFeatureTracking.Current.Track(FEATURE_NAME);
		}

		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string propertyName)
		{
			if(PropertyChanged != null)
				PropertyChanged(this,
					new PropertyChangedEventArgs(propertyName));
		}
		#endregion
	}
}
