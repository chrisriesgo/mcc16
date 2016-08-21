using System;
using Plugin.FeatureTracking.Abstractions;

namespace Plugin.FeatureTracking
{
	public class CrossFeatureTracking
	{
		public static void Log(string message)
		{
#if !PORTABLE
			Console.WriteLine($"Plugin.FeatureTracking: {message}");
#endif
		}

		static Lazy<IFeatureTracking> TTS = new Lazy<IFeatureTracking>(() => CreateFeatureTracking(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

		public static IFeatureTracking Current
		{
			get
			{
				var ret = TTS.Value;
				if(ret == null)
				{
					throw NotImplementedInReferenceAssembly();
				}
				return ret;
			}
		}

		static IFeatureTracking CreateFeatureTracking()
		{
#if PORTABLE
        return null;
#else
			return new FeatureTracking();
#endif
		}

		internal static Exception NotImplementedInReferenceAssembly()
		{
			return new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the Xam.Plugins.Vibrate NuGet package from your main application project in order to reference the platform-specific implementation.");
		}
	}
}