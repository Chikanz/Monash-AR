namespace Mapbox.Unity.Location
{
    using Mapbox.Utils;
    using System;
	using UnityEngine;

	public abstract class AbstractLocationProvider : MonoBehaviour, ILocationProvider
	{
		public event Action<Location> OnLocationUpdated = delegate { };

		protected void SendLocation(Location location)
		{
			OnLocationUpdated(location);
		}

        public void ForceLocationButton()
        {
            Location L;
            L.Heading = 0;
            L.LatitudeLongitude = new Vector2d(-37.8767, 145.0440);
            L.Accuracy = 5;
            L.Timestamp = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            L.IsLocationUpdated = true;
            L.IsHeadingUpdated = true;

            SendLocation(L);
        }
    }
}
