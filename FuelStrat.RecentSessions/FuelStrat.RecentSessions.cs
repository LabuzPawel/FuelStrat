using FuelStrat.SharedMemory;
using FuelStrat.SharedMemory.Types;
using FuelStrat.SharedMemory.Types.Enums;
using System.Numerics;

namespace FuelStrat.RecentSessions
{
    // uses SharedMemory to get data from Memory Mapped Files that stores telemetry
    // each update is being stored in a Sim_data struct (except lap time)

    public class UpdateFromTelemetry
    {
        //double lap_time;
        private int lap_time_millisecs;

        private string track_name = "?";
        private string car_name = "?";
        private int stint_time;
        private List<Vector3> cars_coords = new();
        private TelemetryReader reader = new TelemetryReader();
        private AcSessionType session_type;

        public struct Sim_data
        {
            public int completed_laps;

            //public double lap_time;
            public double fuel;
            public string session_type;
            public string track_name;
            public string car_name;
            public GameStatus game_status;
            public bool is_in_pits;
            public int missing_pit_stops;
            public float race_duration;
            public int stint_time;
            public int pit_window_start;
            public int active_drivers;
            public List<Vector3> cars_coordinates;
            public Vec3[] players_coords;
            public int tank_capacity;
            public float fuel_now;
            public float fuel_used;
            public float distance_traveled;
        };

        public void StartReading()
        {
            reader.Start();
        }

        public void StopReading()
        {
            //reader.Stop();
            reader.Dispose();
        }

        public int GetLapTime()
        {
            reader.GraphicUpdated += graphics =>
            {
                lap_time_millisecs = graphics.ILastTime;
            };
            return lap_time_millisecs;
        }

        public Sim_data GetNewData()
        {
            Sim_data sim_data = new();

            Thread.Sleep(10);

            reader.PhysicsUpdated += physics =>
            {
                sim_data.fuel_now = physics.Fuel;
            };

            reader.StaticInfosUpdated += statics =>
            {
                track_name = statics.Track;
                if (Maps.track_map.ContainsKey(track_name))
                {
                    sim_data.track_name = Maps.track_map[track_name];
                }
                else
                {
                    sim_data.track_name = "?";
                }

                car_name = statics.CarModel;
                if (Maps.car_model_map.ContainsKey(car_name))
                {
                    sim_data.car_name = Maps.car_model_map[car_name];
                }
                else
                {
                    sim_data.car_name = "?";
                }

                sim_data.pit_window_start = statics.PitWindowStart;

                sim_data.tank_capacity = (int)statics.MaxFuel;
            };

            reader.GraphicUpdated += graphics =>
            {
                sim_data.distance_traveled = graphics.DistanceTraveled;

                sim_data.is_in_pits = graphics.IsInPitLane;

                sim_data.completed_laps = graphics.CompletedLaps;

                // lap time inside this reader is broken, sends wrong data

                //lap_time = graphics.ILastTime;
                //double lap_time_formatted = Math.Round(((double)(lap_time / 1000) +
                //(double)(lap_time % 1000) / 1000.0), 3);
                //sim_data.lap_time = lap_time_formatted;

                sim_data.fuel = Math.Round(graphics.FuelXLap, 2);

                session_type = graphics.Session;
                sim_data.session_type = Maps.session_type_map[session_type];

                sim_data.missing_pit_stops = graphics.MissingMandatoryPits;

                sim_data.race_duration = graphics.SessionTimeLeft;

                stint_time = graphics.DriverStintTimeLeft;
                if (stint_time > 0)
                {
                    sim_data.stint_time = stint_time;
                }
                else
                {
                    sim_data.stint_time = 0;
                }

                sim_data.active_drivers = graphics.ActiveCars;

                Vector3 car_coords = new Vector3();
                cars_coords.Clear();

                for (int i = 0; i < sim_data.active_drivers; i++)
                {
                    Vec3 car_coords_ACC = graphics.CarCoordinates[i];

                    car_coords = new Vector3(
                    car_coords_ACC.X,
                    car_coords_ACC.Y,
                    car_coords_ACC.Z);

                    cars_coords.Add(car_coords);
                };
                sim_data.cars_coordinates = cars_coords;

                sim_data.game_status = graphics.Status;

                sim_data.fuel_used = graphics.UsedFuel;
            };
            StartReading();
            Thread.Sleep(20);
            return sim_data;
        }
    }
}