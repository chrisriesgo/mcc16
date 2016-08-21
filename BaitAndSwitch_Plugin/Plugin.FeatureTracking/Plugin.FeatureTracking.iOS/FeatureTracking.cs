using System;
using Plugin.FeatureTracking.Abstractions;
using System.Collections.Generic;
namespace Plugin.FeatureTracking
{
	public class FeatureTracking : IFeatureTracking
	{
		public FeatureTracking()
		{
			Features = new HashSet<string>();
		}

		public HashSet<string> Features { get; protected set; }

		public bool IsFirstLaunch(string feature)
		{
			CrossFeatureTracking.Log($"Checking Feature ({feature}) for first launch on iOS");
			return FirstLaunchForFeature(feature);
		}

		public void OnFirstLaunch(string feature, Action block)
		{
			if(FirstLaunchForFeature(feature)) block?.Invoke();
		}

		public void Track(string feature)
		{
			if(FirstLaunchForFeature(feature))
			{
				Features.Add(feature);
				CrossFeatureTracking.Log($"Feature ({feature}) now being tracked on iOS");
			}
		}

		public void Remove(string feature)
		{
			if(Features.Contains(feature)) Features.Remove(feature);
		}

		public void RemoveAll()
		{
			Features.Clear();
		}

		private bool FirstLaunchForFeature(string feature)
		{
			return !Features.Contains(feature);
		}
	}
}

