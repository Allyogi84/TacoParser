﻿using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;
namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        // private static double latitude;
        //private static Point location1;
        const string csvPath = "TacoBell-US-AL.csv";
        //private const int V = 0;
        static void Main(string[] args)
        {
            // TODO:  Find the two Taco Bells that are the furthest from one another.
            // HINT:  You’ll need two nested forloops ---------------------------
            logger.LogInfo("Log initialized");
            // use File.ReadAllLines(path) to grab all the lines from your csv file
            // Log and error if you get 0 lines and a warning if you get 1 line
            var lines = File.ReadAllLines(csvPath);
            logger.LogInfo($"Lines: { lines[0]}");
            // Create a new instance of your TacoParser class
            var parser = new TacoParser();
            // Grab an IEnumerable of locations using the Select command: var locations = lines.Select(parser.Parse);
            var locations = lines.Select(parser.Parse).ToArray();
            // DON’T FORGET TO LOG YOUR STEPS
            // Now that your Parse method is completed, START BELOW ----------
            // DONE - Include the Geolocation toolbox, so you can compare locations: `using GeoCoordinatePortable;`
            //HINT NESTED LOOPS SECTION---------------------
            // TODO: Create two `ITrackable` variables with initial values of `null`.
            // These will be used to store your two taco bells that are the farthest from each other.
            ITrackable tacoBell1 = null;
            ITrackable tacoBell2 = null;
            // Create a `double` variable to store the distance
            double distance = 0;
            // Do a loop for your locations to grab each location as the origin (perhaps: `locA`)
            for (int i = 0; i < locations.Length; i++)
            {
                ITrackable locA = locations[i];
                //var corA = new GeoCoordinate();
                // Create a new corA Coordinate with your locA’s lat and long
                GeoCoordinate corA = new GeoCoordinate(locA.Location.Latitude, locA.Location.Longitude);
                // Point location = location1;
                // Now, do another loop on the locations with the scope of your first loop,
                //so you can grab the “destination” location (perhaps: `locB`)
                // Create a new Coordinate with your locB’s lat and long
                for (int j = 0; j < locations.Length; j++)
                {
                    ITrackable locB = locations[j];
                    GeoCoordinate corB = new GeoCoordinate(locB.Location.Latitude, locB.Location.Longitude);
                    // Now, compare the two using `.GetDistanceTo()`, which returns a double
                    // If the distance is greater than the currently saved distance,
                    if (corA.GetDistanceTo(corB) > distance)
                    {
                        //update the distance and the two `ITrackable` variables you set above
                        tacoBell1 = locA;
                        tacoBell2 = locB;
                        distance = Math.Round(corA.GetDistanceTo(corB) / 1609.344);
                    }
                }
            }
            // Once you’ve looped through everything, you’ve found the two Taco Bells farthest away from each other.
            logger.LogInfo($"{ tacoBell1.Name} and { tacoBell2.Name} has a distance of {distance} miles");
        }
    }
}