﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace IoTHubBuddyClient
{
    class LocationProvider
    {
        private GeolocationAccessStatus accessStatus;
        public LocationProvider()
        {
            this.Initialize();
        }
        /// <summary>
        /// request access if user has not yet given permission to use location
        /// </summary>
        private async void Initialize()
        {
            try
            {
                accessStatus = await Geolocator.RequestAccessAsync();
            } catch (Exception e)
            {
                accessStatus = GeolocationAccessStatus.Denied;
            }
            
        }
        /// <summary>
        /// get location if access is permitted
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetLocation()
        {
            string coords = "";
            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:
                    Geolocator gl = new Geolocator();
                    gl.DesiredAccuracy = PositionAccuracy.High;
                    Geoposition pos = await gl.GetGeopositionAsync();
                    coords = pos.Coordinate.Latitude.ToString() + ", " + pos.Coordinate.Longitude.ToString();
                    break;
                default:
                    coords = "Access to location is denied. Please enable location in your device settings.";
                    break;
            }
            return coords;
        }
    }
}
