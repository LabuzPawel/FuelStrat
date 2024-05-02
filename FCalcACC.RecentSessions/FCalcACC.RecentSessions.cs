using FCalcACC.SharedMemory;
using FCalcACC;
using FCalcACC.SharedMemory.Types.Enums;
using System.Runtime.CompilerServices;
using System.Reflection.PortableExecutable;
using System.Timers;
using FCalcACC.SharedMemory.Types;
using System.Numerics;
using System.Drawing;

namespace FCalcACC.RecentSessions
{
    public class UpdateFromTelemetry
    {
        double lap_time;
        string track_name = "?";
        string car_name = "?";
        int stint_time;
        List<Vector3> cars_coords = new(); 
        TelemetryReader reader = new TelemetryReader();
        AcSessionType session_type;

        public struct Sim_data
        {
            public int completed_laps;
            public double lap_time;
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
                sim_data.track_name = Maps.track_map[track_name];

                car_name = statics.CarModel;
                sim_data.car_name = Maps.car_model_map[car_name];

                sim_data.pit_window_start = statics.PitWindowStart;

                sim_data.tank_capacity = (int)statics.MaxFuel;
            };

            reader.GraphicUpdated += graphics =>
            {
                sim_data.is_in_pits = graphics.IsInPitLane;

                sim_data.completed_laps = graphics.CompletedLaps;

                lap_time = graphics.ILastTime;
                double lap_time_formatted = Math.Round(((double)(lap_time / 1000) +
                (double)(lap_time % 1000) / 1000.0), 3);
                sim_data.lap_time = lap_time_formatted;

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
