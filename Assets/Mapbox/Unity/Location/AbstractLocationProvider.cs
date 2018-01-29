namespace Mapbox.Unity.Location
{
    using Mapbox.Utils;
    using System;
	using UnityEngine;

	public abstract class AbstractLocationProvider : MonoBehaviour, ILocationProvider
	{
        private Vector2d[] ForcedLocationUpdate =
        {
            new Vector2d(-37.8765, 145.0442),
            new Vector2d(-37.8768, 145.0440),
        };

        int index;

		public event Action<Location> OnLocationUpdated = delegate { };

		protected void SendLocation(Location location)
		{
			OnLocationUpdated(location);
		}

        public void ForceLocationButton()
        {
            if (index > ForcedLocationUpdate.Length - 1) return;
            Location L;
            L.Heading = 0;
            L.LatitudeLongitude = ForcedLocationUpdate[index];
            index++;
            L.Accuracy = 5;
            L.Timestamp = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            L.IsLocationUpdated = true;
            L.IsHeadingUpdated = true;

            SendLocation(L);
        }
    }
}
