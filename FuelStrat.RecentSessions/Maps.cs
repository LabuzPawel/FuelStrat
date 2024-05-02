using FuelStrat.SharedMemory.Types.Enums;

namespace FuelStrat.RecentSessions
{
    public class Maps
    {
        public static Dictionary<string, string> car_model_map = new()
        {
            { "amr_v12_vantage_gt3", "Aston Martin V12 Vantage GT3 2013" },
            { "amr_v8_vantage_gt3", "Aston Martin V8 Vantage GT3 2019" },
            { "audi_r8_lms", "Audi R8 LMS GT3 2015" },
            { "audi_r8_lms_evo", "Audi R8 LMS Evo GT3 2019" },
            { "audi_r8_lms_evo_ii", "Audi R8 LMS Evo II GT3 2022" },
            { "bentley_continental_gt3_2016", "Bentley Continental GT3 2015" },
            { "bentley_continental_gt3_2018", "Bentley Continental GT3 2018" },
            { "bmw_m6_gt3", "BMW M6 GT3 2017" },
            { "bmw_m4_gt3", "BMW M4 GT3 2022" },
            { "jaguar_g3", "Emil Frey Jaguar GT3 2012" },
            { "ferrari_488_gt3", "Ferrari 488 GT3 2018" },
            { "ferrari_488_gt3_evo", "Ferrari 488 GT3 Evo 2020" },
            { "ferrari_296_gt3", "Ferrari 296 GT3 2023" },
            { "honda_nsx_gt3", "Honda NSX GT3 2017" },
            { "honda_nsx_gt3_evo", "Honda NSX Evo GT3 2019" },
            { "lamborghini_huracan_gt3", "Lamborghini Huracan GT3 2015" },
            { "lamborghini_huracan_gt3_evo", "Lamborghini Huracan Evo GT3 2019" },
            { "lamborghini_huracan_gt3_evo2", "Lamborghini Huracan Evo2 GT3 2023" },
            { "lexus_rc_f_gt3", "Lexus RC F GT3 2016" },
            { "mclaren_650s_gt3", "McLaren 650S GT3 2015" },
            { "mclaren_720s_gt3", "McLaren 720S GT3 2019" },
            { "mclaren_720s_gt3_evo", "McLaren 720S GT3 Evo 2023" },
            { "mercedes_amg_gt3", "Mercedes AMG GT3 2015" },
            { "mercedes_amg_gt3_evo", "Mercedes AMG GT3 2020" },
            { "nissan_gt_r_gt3_2017", "Nissan GTR Nismo GT3 2015" },
            { "nissan_gt_r_gt3_2018", "Nissan GTR Nismo GT3 2018" },
            { "porsche_991_gt3_r", "Porsche 911 GT3 R 2018" },
            { "porsche_991ii_gt3_r", "Porsche 911 II GT3 R 2019" },
            { "porsche_992_gt3_r", "Porsche 992 GT3 R 2023" },
            { "lamborghini_gallardo_rex", "Reiter Engineering R-EX GT3 2017" },
            { "alpine_a110_gt4", "Alpine A110 GT4 2018" },
            { "amr_v8_vantage_gt4", "Aston Martin V8 Vantage GT4 2018" },
            { "audi_r8_gt4", "Audi R8 LMS GT4 2018" },
            { "bmw_m4_gt4", "BMW M4 GT4 2018" },
            { "chevrolet_camaro_gt4r", "Chevrolet Camaro R GT4 2017" },
            { "ginetta_g55_gt4", "Ginetta G55 GT4 2012" },
            { "ktm_xbow_gt4", "KTM X-Bow GT4 2016" },
            { "maserati_mc_gt4", "Maserati Granturismo MC GT4 2016" },
            { "mclaren_570s_gt4", "Mclaren 570S GT4 2016" },
            { "mercedes_amg_gt4", "Mercedes AMG GT4 2016" },
            { "porsche_718_cayman_gt4_mr", "Porsche 718 Cayman GT4 Clubsport 2019" },
            { "audi_r8_lms_gt2", "Audi R8 LMS GT2 2021" },
            { "ktm_xbow_gt2", "KTM X-Bow GT2 2021" },
            { "maserati_mc20_gt2", "Maserati MC20 GT2 2023" },
            { "mercedes_amg_gt2", "Mercedes AMG GT2 2023" },
            { "porsche_935", "Porsche 935 GT2 2019" },
            { "porsche_991_gt2_rs_mr", "Porsche 991 II GT2 RS CS Evo 2023" },
            { "ferrari_488_challenge_evo", "Ferrari 488 Challenge Evo 2020" },
            { "lamborghini_huracan_st", "Lamborghini Huracan Super Trofeo 2015" },
            { "lamborghini_huracan_st_evo2", "Lamborghini Huracan Super Trofeo Evo2 2021" },
            { "porsche_991ii_gt3_cup", "Porsche 911 II GT3 Cup 2017" },
            { "porsche_992_gt3_cup", "Porsche 911 GT3 Cup (992) 2021" },
            { "bmw_m2_cs_racing", "BMW M2 CS 2020" }
        };

        public static Dictionary<string, string> track_map = new()
        {
            { "Barcelona", "Barcelona"},
            { "brands_hatch", "Brands Hatch" },
            { "cota", "COTA" },
            { "donington", "Donington" },
            { "Hungaroring", "Hungaroring" },
            { "Imola", "Imola" },
            { "indianapolis", "Indianapolis" },
            { "Kyalami", "Kyalami" },
            { "Laguna_Seca", "Laguna Seca" },
            { "misano", "Misano" },
            { "monza", "Monza" },
            { "mount_panorama", "Mount Panorama" },
            { "nurburgring", "Nurburgring" },
            { "nurburgring_24h", "Nordschleife" },
            { "oulton_park", "Oulton Park" },
            { "Paul_Ricard", "Paul Ricard" },
            { "red_bull_ring", "Red Bull Ring" },
            { "Silverstone", "Silverstone" },
            { "snetterton", "Snetterton" },
            { "Spa", "Spa-Francorchamps" },
            { "Suzuka", "Suzuka" },
            { "Valencia", "Valencia" },
            { "watkins_glen", "Watkins Glen" },
            { "Zandvoort", "Zandvoort" },
            { "Zolder", "Zolder" }
        };

        public static Dictionary<AcSessionType, string> session_type_map = new()
        {
            { AcSessionType.Practice, "Practice" },
            { AcSessionType.Race, "Race" },
            { AcSessionType.Qualify, "Qualify" },
            { AcSessionType.Hotlap, "Hotlap" },
            { AcSessionType.Hotstint, "Hotstint" },
            { AcSessionType.TimeAttack, "TimeAttack" },
            { AcSessionType.Unknown, "Unknown" },
            { AcSessionType.Drag, "Drag" },
            { AcSessionType.HotstintSuperpole, "HotstintSuperpole" }
        };

        public static Dictionary<string, string> GetCarMap()
        {
            return car_model_map;
        }

        public static Dictionary<string, string> GetTrackMap()
        {
            return track_map;
        }

        public static Dictionary<AcSessionType, string> GetSessionMap()
        {
            return session_type_map;
        }
    }
    
}

