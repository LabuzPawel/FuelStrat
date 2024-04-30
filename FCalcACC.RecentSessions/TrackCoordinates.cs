using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using FCalcACC.SharedMemory.Types;
using System.Numerics;

namespace FCalcACC.RecentSessions
{
    public class TrackStartCoords
    {
        public class Track_start_coords
        {
            public string track_name { get; set; }
            public Vector3 track_start { get; set; }

            public Track_start_coords(string trackName, Vector3 trackStart)
            {
                track_name = trackName;
                track_start = trackStart;
            }
        }

        public List<Track_start_coords> track_start_list = new();
        
        public TrackStartCoords()
        {
            track_start_list = new List<Track_start_coords>();

            track_start_list.Add(new Track_start_coords("Barcelona", new Vector3(329.62f, -18.54f, -133.99f)));
            track_start_list.Add(new Track_start_coords("Brands Hatch", new Vector3(-151.03f, -7.97f, -381.24f)));
            track_start_list.Add(new Track_start_coords("COTA", new Vector3(846.99f, 154.76f, 83.45f)));
            track_start_list.Add(new Track_start_coords("Donington", new Vector3(-293.67f, -1.03f, 81.28f)));
            track_start_list.Add(new Track_start_coords("Hungaroring", new Vector3(-130.53f, 24.85f, 532.48f)));
            track_start_list.Add(new Track_start_coords("Imola", new Vector3(-9.33f, -83.51f, -417.30f)));
            track_start_list.Add(new Track_start_coords("Indianapolis", new Vector3(-361.77f, 199.12f, 268.08f)));
            track_start_list.Add(new Track_start_coords("Kyalami", new Vector3(195.44f, -46.32f, 197.81f)));
            track_start_list.Add(new Track_start_coords("Laguna Seca", new Vector3(-256.66f, -11.99f, -296.36f)));
            track_start_list.Add(new Track_start_coords("Misano", new Vector3(105.52f, -1.66f, -58.04f)));
            track_start_list.Add(new Track_start_coords("Monza", new Vector3(-359.74f, -9.02f, 258.36f)));
            track_start_list.Add(new Track_start_coords("Mount Panorama", new Vector3(120.37f, 31.60f, -586.16f)));
            track_start_list.Add(new Track_start_coords("Nurburgring", new Vector3(165.25f, 61.77f, -374.68f)));
            track_start_list.Add(new Track_start_coords("Nordschleife", new Vector3(167.74f, 61.84f, -374.70f)));
            track_start_list.Add(new Track_start_coords("Oulton Park", new Vector3(269.19f, -59.63f, -352.73f)));
            track_start_list.Add(new Track_start_coords("Paul Ricard", new Vector3(1527.18f, 472.68f, -1645.07f)));
            track_start_list.Add(new Track_start_coords("Red Bull Ring", new Vector3(246.85f, 665.15f, 314.44f)));
            track_start_list.Add(new Track_start_coords("Silverstone", new Vector3(75.89f, 0.94f, -849.47f)));
            track_start_list.Add(new Track_start_coords("Snetterton", new Vector3(-156.00f, -47.63f, 169.24f)));
            track_start_list.Add(new Track_start_coords("Spa-Francorchamps", new Vector3(-901.81f, 4.57f, -1989.55f)));
            track_start_list.Add(new Track_start_coords("Suzuka", new Vector3(639.56f, -27.57f, -65.18f)));
            track_start_list.Add(new Track_start_coords("Valencia", new Vector3(-87.72f, 148.38f, 31.77f)));
            track_start_list.Add(new Track_start_coords("Watkins Glen", new Vector3(1110.52f, 448.13f, 559.26f)));
            track_start_list.Add(new Track_start_coords("Zandvoort", new Vector3(-236.25f, 2.69f, -36.47f)));
            track_start_list.Add(new Track_start_coords("Zolder", new Vector3(1341.75f, 83.35f, -1048.29f)));
        } 
    }
}
