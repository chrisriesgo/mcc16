using System;
using System.Collections.Generic;
using Plugin.FeatureTracking.Abstractions;

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
			CrossFeatureTracking.Log($"Checking Feature ({feature}) for first launch on Android");
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
				CrossFeatureTracking.Log($"Feature ({feature}) now being tracked on Android");
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

