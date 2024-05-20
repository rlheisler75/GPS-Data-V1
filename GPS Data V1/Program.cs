using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace GeoLocationApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await GetGeolocationAsync();
           
            

           
        }

        private static async Task GetGeolocationAsync()
        {
            var geolocator = new Geolocator { DesiredAccuracyInMeters = 10 };

            // Check if the location access is allowed
            var accessStatus = await Geolocator.RequestAccessAsync();
            if (accessStatus != GeolocationAccessStatus.Allowed)
            {
                Console.WriteLine("Access to location is denied.");
                return;
            }

            while (true)
            {
                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();

                // Get the position
                Geoposition pos = await geolocator.GetGeopositionAsync();

                stopwatch.Stop();
                
                Geocoordinate coord = pos.Coordinate;

                // Display the position source
                PositionSource positionSource = coord.PositionSource;

                Console.WriteLine($"Latitude: {coord.Point.Position.Latitude}");
                Console.WriteLine($"Longitude: {coord.Point.Position.Longitude}");
                Console.WriteLine($"Speed MPH: {coord.Speed * 2.23694}");
                Console.WriteLine($"Accuracy in Feet: {coord.Accuracy / 3.280839895}");
                Console.WriteLine($"Position Source: {positionSource}");
                Console.WriteLine("The current date and time: {0:MM/dd/yy H:mm:ss zzz}", DateTime.Now);
                Console.WriteLine();
                // Wait for a specific interval before checking again
                await Task.Delay(3000); // Delay for 3 seconds

                // Get the elapsed time as a TimeSpan value.
                TimeSpan ts = stopwatch.Elapsed;

                // Format and display the TimeSpan value.
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                Console.WriteLine("RunTime " + elapsedTime);
                Console.WriteLine();
                Console.WriteLine();

            }
            
        }
    }
}
