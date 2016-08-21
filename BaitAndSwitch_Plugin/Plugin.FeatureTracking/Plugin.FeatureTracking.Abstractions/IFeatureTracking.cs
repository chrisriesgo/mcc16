using System;
namespace Plugin.FeatureTracking.Abstractions
{
	public interface IFeatureTracking
	{
		void Track(string feature);
		bool IsFirstLaunch(string feature);
		void OnFirstLaunch(string feature, Action block);
		void Remove(string feature);
		void RemoveAll();
	}
}