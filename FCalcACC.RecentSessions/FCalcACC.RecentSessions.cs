using FCalcACC.SharedMemory;
using FCalcACC;
using FCalcACC.SharedMemory.Types.Enums;
using System.Runtime.CompilerServices;
using System.Reflection.PortableExecutable;
using System.Timers;

namespace FCalcACC.RecentSessions
{
    public class UpdateFromTelemetry
    {
        double fuel;
        int completed_laps;
        double lap_time;
        bool is_in_pits;
        string track_name = "?";
        string car_name = "?";
        GameStatus game_status;
        TelemetryReader reader = new TelemetryReader();
        AcSessionType session_type;

        public struct recent_lap
        {
            public int completed_laps;
            public double lap_time;
            public double fuel;
            public DateTime date_time;
            public string session_type;
            public string track_name;
            public string car_name;
        }

        public void StartReading()
        {
            reader.Start();
        }

        public void StopReading()
        {
            reader.Stop();
            reader.Dispose();
        }

        public GameStatus GetGameStatus()
        {
            reader.GraphicUpdated += graphics =>
            {
                game_status = graphics.Status;
            };

            return game_status;
        }

        public int GetCompletedLaps()
        {
            reader.GraphicUpdated += graphics =>
            {
                completed_laps = graphics.CompletedLaps;
            };

            return completed_laps;
        }

        public double GetLapTime()
        {
            reader.GraphicUpdated += graphics =>
            {
                lap_time = graphics.ILastTime;
            };

            double lap_time_formatted = Math.Round(((double)(lap_time / 1000) + 
                (double)(lap_time % 1000) / 1000.0), 3);

            return lap_time_formatted;
        }

        public double GetFuel()
        {
            reader.GraphicUpdated += graphics =>
            {
                fuel = Math.Round(graphics.FuelXLap, 2);
            };

            return fuel;
        }

        public string GetSessionType()
        {
            reader.GraphicUpdated += graphics =>
            {
                session_type = graphics.Session;
            };

            if (Maps.session_type_map.ContainsKey(session_type))
            {
                return Maps.session_type_map[session_type];
            }
            else
            {
                return "?";
            }
        }

        public bool IsInPits()
        {
            reader.GraphicUpdated += graphics =>
            {
                is_in_pits = graphics.IsInPitLane;
            };

            return is_in_pits;
        }

        public string GetTrack()
        {
            reader.StaticInfosUpdated += statics =>
            {
                track_name = statics.Track;
            };

            if (Maps.track_map.ContainsKey(track_name))
            {
                return Maps.track_map[track_name];
            }
            else
            {
                return "?";
            };
        }

        public string GetCarName()
        {
            reader.StaticInfosUpdated += statics =>
            {
                car_name = statics.CarModel;
            };

            if (Maps.car_model_map.ContainsKey(car_name))
            {
                return Maps.car_model_map[car_name];
            }
            else
            {
                return "?";
            }
        }

        public recent_lap CreateStruct()
        {
            recent_lap recent_Session = new recent_lap();
            recent_Session.completed_laps = GetCompletedLaps();
            recent_Session.lap_time = GetLapTime();
            recent_Session.fuel = GetFuel();
            recent_Session.date_time = DateTime.Now;
            recent_Session.session_type = GetSessionType();
            recent_Session.track_name = GetTrack();
            recent_Session.car_name = GetCarName();

            return recent_Session;
        }
    }
}
