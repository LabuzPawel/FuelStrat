using System.Globalization;
using Newtonsoft.Json;

namespace FCalcACC
{
    public partial class Form1 : Form
    {
        class Car
        {
            public string car_name { get; set; }
            public string class_name { get; set; }
        }
        class Track
        {
            public string track_name { get; set; }
            public string track_lap_time { get; set; }
            public string track_pit_duration { get; set; }
            public List<CarTrackFuel> car_track_fuel { get; set; }
        }
        class CarTrackFuel
        {
            public string car_name { get; set; }
            public string fuel_per_lap { get; set; }
        }
        List<Track> all_tracks;
        List<Car> all_cars;

        double time_lost_in_pits;
        int number_of_laps;
        double overall_race_duration;
        int race_duration_secs;
        float lap_time_secs;
        int fuel_for_race_round_up;
        float fuel_per_lap;
        int number_of_pits;
        double formation_lap_fuel;
        double fuel_for_race;

        string DECIMAL_SEPARATOR = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        double ONE_L_PIT_TIME = 3.6;
        double ONE_L_MORE = 0.2;
        int DEFAULT_TIME_IN_PITS = 57;


        List<string> CAR_CLASSES = new List<string>()
        {
            "GT3",
            "GT4",
            "GT2",
            "GTC",
            "TCX"
        };

        List<string> PIT_OPTIONS = new List<string>()
        {
            "Refuel only",
            "Fixed refuel only",
            "Tires only",
            "Refuel + tires",
            "1L refuel"
        };

        void LoadCarClasses()
        {
            foreach (var car_class in CAR_CLASSES)
            {
                comboBox_class.Items.Add(car_class);
            }
        }

        void LoadPitOptions()
        {
            foreach (var pit_option in PIT_OPTIONS)
            {
                comboBox_pit_options.Items.Add(pit_option);
            }
        }

        void LoadCars()
        {
            comboBox_car.ResetText();
            comboBox_car.Items.Clear();
            string selected_car_class = comboBox_class.Text;

            string json_string_cars = File.ReadAllText("CARS.json");
            all_cars = JsonConvert.DeserializeObject<List<Car>>(json_string_cars);

            var cars_within_a_class = all_cars.Where(car => car.class_name.Contains(selected_car_class));

            foreach (var car in cars_within_a_class)
            {
                comboBox_car.Items.Add(car.car_name);
            }
            comboBox_car.Text = "CAR";

            if (selected_car_class == "TCX")
            {
                comboBox_car.SelectedIndex = 0;
            }

        }

        void LoadTracks()
        {
            string json_string_tracks = File.ReadAllText("TRACKS.json");
            all_tracks = JsonConvert.DeserializeObject<List<Track>>(json_string_tracks);

            foreach (var track in all_tracks)
            {
                comboBox_track.Items.Add(track.track_name);
            }
        }
        private void comboBox_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCars();
        }

        // TEXTBOX INPUT RESTRICTIONS

        private void textBox_race_h_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox_race_min_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox_lap_time_min_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox_lap_time_sec_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',';
        }

        private void textBox_fuel_per_lap_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',';
        }
        private void numericUpDown_pits_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void CalculateTimeLostInPits()
        {
            number_of_pits = (int)numericUpDown_pits.Value;
            string pit_stop_option = comboBox_pit_options.Text;
            if (number_of_pits == 0)
            {
                comboBox_pit_options.ResetText();
            }
            if (comboBox_track.Text == "TRACK")
            {
                switch (pit_stop_option)
                {
                    case "Fixed refuel only":
                        time_lost_in_pits = DEFAULT_TIME_IN_PITS - 5;
                        break;
                    case "Tires only":
                        time_lost_in_pits = DEFAULT_TIME_IN_PITS;
                        break;
                    case "Refuel + tires":
                        time_lost_in_pits = DEFAULT_TIME_IN_PITS;
                        break;
                    case "1L refuel":
                        time_lost_in_pits = DEFAULT_TIME_IN_PITS - 26.4;
                        break;
                }
                time_lost_in_pits *= number_of_pits;
            }
            else
            {
                var selected_track = all_tracks.FirstOrDefault(track => track.track_name.Contains(comboBox_track.Text));
                int.TryParse(selected_track.track_pit_duration, out int time_in_pits);

                switch (pit_stop_option)
                {
                    case "Fixed refuel only":
                        time_lost_in_pits = time_in_pits - 5;
                        break;
                    case "Tires only":
                        time_lost_in_pits = time_in_pits;
                        break;
                    case "Refuel + tires":
                        time_lost_in_pits = time_in_pits;
                        break;
                    case "1L refuel":
                        time_lost_in_pits = time_in_pits - 26.4;
                        break;
                }
                time_lost_in_pits *= number_of_pits;

            }
        }
        private void RefuelTimeLost()
        {
            int time_in_pits;
            if (comboBox_track.Text != "TRACK")
            {
                var selected_track = all_tracks.FirstOrDefault(track => track.track_name.Contains(comboBox_track.Text));
                int.TryParse(selected_track.track_pit_duration, out time_in_pits);
            }
            else
            {
                time_in_pits = DEFAULT_TIME_IN_PITS;
            }
                
            int first_stint_laps = (int)Math.Ceiling(number_of_laps / 2.0);
            float fuel_first_stint = first_stint_laps * fuel_per_lap;
            int fuel_first_stint_round_up = (int)Math.Ceiling(fuel_first_stint);
            int refuel = fuel_for_race_round_up - fuel_first_stint_round_up;

            time_in_pits -= 30;
            time_lost_in_pits = time_in_pits + ONE_L_PIT_TIME;
            while (refuel > 0)
            {
                time_lost_in_pits += ONE_L_MORE;
                refuel--;
            }
            time_lost_in_pits *= number_of_pits;
        }

        private void CalculateRaceDuration()
        {
            int.TryParse(textBox_race_h.Text, out int h_race);
            int.TryParse(textBox_race_min.Text, out int min_race);
            if (min_race > 59)
            {
                min_race = 59;
                textBox_race_min.Text = min_race.ToString();
            }

            string lap_time_sec_normalize = textBox_lap_time_sec.Text.Replace(".", DECIMAL_SEPARATOR).Replace(",", DECIMAL_SEPARATOR);
            int decimal_separator_count = 0;
            foreach (char d in lap_time_sec_normalize)
            {
                if (d == DECIMAL_SEPARATOR[0])
                {
                    decimal_separator_count++;
                }
            }
            if (decimal_separator_count > 1)
            {
                lap_time_sec_normalize = lap_time_sec_normalize.Replace(DECIMAL_SEPARATOR, string.Empty).Replace(DECIMAL_SEPARATOR, "")
                    .Insert(lap_time_sec_normalize.IndexOf(DECIMAL_SEPARATOR[0]), DECIMAL_SEPARATOR);
                textBox_lap_time_sec.Text = lap_time_sec_normalize;
            }

            float.TryParse(lap_time_sec_normalize, out float sec_laptime);
            int.TryParse(textBox_lap_time_min.Text, out int min_laptime);
            if (min_laptime > 59)
            {
                min_laptime = 59;
                textBox_lap_time_min.Text = min_laptime.ToString();
            }
            if (sec_laptime > 59.99999)
            {
                sec_laptime = 59.999f;
                textBox_lap_time_sec.Text = sec_laptime.ToString();
            }

            race_duration_secs = (h_race * 3600) + (min_race * 60);
            lap_time_secs = (min_laptime * 60) + sec_laptime;

            overall_race_duration = time_lost_in_pits;

            number_of_laps = 0;

            while (overall_race_duration < race_duration_secs)
            {
                overall_race_duration += lap_time_secs;
                number_of_laps++;
            }

            TimeSpan time_interval = TimeSpan.FromSeconds(overall_race_duration);
            string formatted_overall_duration = string.Format("{0:D2}:{1:D2}:{2:D2}",
                (int)time_interval.TotalHours, time_interval.Minutes, time_interval.Seconds);

            label_overall_result.Text = formatted_overall_duration;
        }

        private void CalculateLapTimePlusMinus()
        {
            double plus1_lap_time_secs = lap_time_secs - ((overall_race_duration - race_duration_secs) / number_of_laps);
            int plus1_lap_time_floor = (int)Math.Floor(plus1_lap_time_secs);
            int plus1_lap_time_rest = (int)Math.Round((plus1_lap_time_secs - plus1_lap_time_floor) * 1000);
            TimeSpan time_interval_plus = TimeSpan.FromSeconds(plus1_lap_time_floor);
            string formatted_plus1_lap_time = string.Format("{0:D1}:{1:D2}.{2:000}",
                (int)time_interval_plus.TotalMinutes, (int)time_interval_plus.Seconds, (int)plus1_lap_time_rest);
            label_plus1_lap_time_result.Text = formatted_plus1_lap_time;

            double minus1_lap_time_secs = (race_duration_secs - time_lost_in_pits) / (number_of_laps - 1);
            int minus1_lap_time_floor = (int)Math.Floor(minus1_lap_time_secs);
            int minus1_lap_time_rest = (int)Math.Round((minus1_lap_time_secs - minus1_lap_time_floor) * 1000);
            TimeSpan time_interval_minus = TimeSpan.FromSeconds(minus1_lap_time_floor);
            string formatted_minus1_lap_time = string.Format("{0:D1}:{1:D2}.{2:000}",
                (int)time_interval_minus.TotalMinutes, (int)time_interval_minus.Seconds, (int)minus1_lap_time_rest);
            label_minus1_lap_time_result.Text = formatted_minus1_lap_time;
        }

        private void CalculateFuel()
        {
            string fuel_per_lap_normalize = textBox_fuel_per_lap.Text.Replace(".", DECIMAL_SEPARATOR).Replace(",", DECIMAL_SEPARATOR);
            float.TryParse(fuel_per_lap_normalize, out fuel_per_lap);
            int decimal_separator_count = 0;
            foreach (char d in fuel_per_lap_normalize)
            {
                if (d == DECIMAL_SEPARATOR[0])
                {
                    decimal_separator_count++;
                }
            }
            if (decimal_separator_count > 1)
            {
                fuel_per_lap_normalize = fuel_per_lap_normalize.Replace(DECIMAL_SEPARATOR, string.Empty).Replace(".", "").Insert(fuel_per_lap_normalize.IndexOf(DECIMAL_SEPARATOR[0]), ".");
                textBox_fuel_per_lap.Text = fuel_per_lap_normalize;
            }

            bool full_formation_lap = (listBox_formation.Text == "Full");
            double brake_dragging_full = 0;
            double brake_dragging_short = 0;

            if (full_formation_lap)
            {
                brake_dragging_full = fuel_per_lap + (fuel_per_lap * 0.1);
            }
            else
            {
                brake_dragging_short = fuel_per_lap * 0.25;
            }
            formation_lap_fuel = brake_dragging_full + brake_dragging_short;
            label_laps_result.Text = number_of_laps + " laps";

            fuel_for_race = number_of_laps * (fuel_per_lap + 0.01) + formation_lap_fuel;
            fuel_for_race_round_up = (int)Math.Ceiling(fuel_for_race);
            label_fuel_race_result.Text = fuel_for_race_round_up.ToString() + " L";

            double fuel_plus1 = fuel_for_race + (fuel_per_lap + 0.01);
            int fuel_plus1_round_up = (int)Math.Ceiling(fuel_plus1);
            label_plus1_fuel_result.Text = fuel_plus1_round_up.ToString() + " L";

            double fuel_minus1 = fuel_for_race - (fuel_per_lap + 0.01);
            int fuel_minus1_round_up = (int)Math.Ceiling(fuel_minus1);
            label_minus1_fuel_result.Text = fuel_minus1_round_up.ToString() + " L";
        }

        private void CalculatePitStops()
        {
            panel_pit_stop_strategy.Controls.Clear();

            GroupBox groupBox_start = new GroupBox();
            groupBox_start.Text = "Stint 1 - Start of the race";
            groupBox_start.BackColor = Color.Gainsboro;
            groupBox_start.Font = groupBox_car_track.Font;
            groupBox_start.Location = new Point(16, 26);
            groupBox_start.Size = new Size(385, 83);
            panel_pit_stop_strategy.Controls.Add(groupBox_start);

            TableLayoutPanel tableLayoutPanel_start = new TableLayoutPanel();
            tableLayoutPanel_start.Size = new Size(350, 30);
            tableLayoutPanel_start.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;
            tableLayoutPanel_start.ColumnCount = 2;
            tableLayoutPanel_start.ColumnStyles.Clear();
            tableLayoutPanel_start.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            tableLayoutPanel_start.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            tableLayoutPanel_start.RowCount = 1;
            tableLayoutPanel_start.Location = new Point(15, 35);
            groupBox_start.Controls.Add(tableLayoutPanel_start);

            Label label_fuel_for_start = new Label();
            label_fuel_for_start.Text = "Fuel for the start";
            label_fuel_for_start.Dock = DockStyle.Fill;
            label_fuel_for_start.TextAlign = ContentAlignment.MiddleCenter;
            label_fuel_for_start.Size = new Size(130, 17);
            Label label_fuel_start_result = new Label();
            label_fuel_start_result.Text = "0 L";
            label_fuel_start_result.Dock = DockStyle.Fill;
            label_fuel_start_result.TextAlign = ContentAlignment.MiddleCenter;

            tableLayoutPanel_start.Controls.Add(label_fuel_for_start, 0, 0);
            tableLayoutPanel_start.Controls.Add(label_fuel_start_result, 1, 0);

            number_of_pits = (int)numericUpDown_pits.Value;

            if (number_of_pits == 0)
            {
                label_fuel_start_result.Text = label_fuel_race_result.Text;
            }
            else
            {
                int laps_per_stint;
                int laps_first_stint;
                double laps_per_stint_check = number_of_laps / (number_of_pits + 1);
                if (laps_per_stint_check % 1 == 0)
                {
                    laps_per_stint = (int)laps_per_stint_check;
                    laps_first_stint = (int)laps_per_stint_check;
                }
                else
                {
                    laps_per_stint = (int)Math.Floor(laps_per_stint_check);
                    laps_first_stint = laps_per_stint + 1;
                }

                int y_groupBox = 120;

                int fuel_first_stint = (int)Math.Ceiling((laps_first_stint * fuel_per_lap) + formation_lap_fuel);
                int fuel_rest = fuel_for_race_round_up - fuel_first_stint;
                int fuel_per_stint = (int)Math.Ceiling(fuel_rest / (float)number_of_pits);

                int stint = 2;
                for (int i = number_of_pits; i > 0; i--)
                {
                    if ((fuel_for_race_round_up - fuel_first_stint - (fuel_per_stint * number_of_pits)) != 0)
                    {
                        fuel_per_stint -= 1;
                    }

                    string name_for_groupbox = "groupBox_stint" + stint.ToString();
                    string name_for_refuel_label = "label_refuel_stint" + stint.ToString();
                    string name_for_refuel_result_label = "label_refuel_stint" + stint.ToString() + "_result";
                    string name_for_table = "tableLayoutPanel_stint" + stint.ToString();

                    if (comboBox_pit_options.Text == "Tires only")
                    {
                        label_fuel_start_result.Text = label_fuel_race_result.Text;

                        GroupBox groupBox_temp = new GroupBox();
                        groupBox_temp.Name = name_for_groupbox;
                        groupBox_temp.Location = new Point(15, y_groupBox);
                        groupBox_temp.Font = groupBox_car_track.Font;
                        groupBox_temp.Size = groupBox_start.Size;
                        groupBox_temp.BackColor = groupBox_start.BackColor;
                        groupBox_temp.Text = "Stint " + stint;
                        panel_pit_stop_strategy.Controls.Add(groupBox_temp);

                        TableLayoutPanel table_temp = new TableLayoutPanel();
                        table_temp.Name = name_for_table;
                        table_temp.Location = new Point(16, 30);
                        table_temp.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;
                        table_temp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
                        table_temp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));

                        Label label_refuel_temp = new Label();
                        label_refuel_temp.Dock = DockStyle.Fill;
                        label_refuel_temp.TextAlign = ContentAlignment.MiddleCenter;
                        label_refuel_temp.Name = name_for_refuel_label;
                        if (stint == 2)
                        {
                            label_refuel_temp.Text = "Tires change after";
                            laps_per_stint = laps_first_stint;
                        }
                        else
                        {
                            label_refuel_temp.Text = "Tires change after";
                            laps_per_stint = laps_first_stint + (laps_per_stint * i);
                        }

                        Label label_refuel_result_temp = new Label();
                        label_refuel_result_temp.Name = name_for_refuel_result_label;
                        label_refuel_result_temp.Text = laps_per_stint.ToString() + " laps";
                        label_refuel_result_temp.Size = new Size(130, 17);
                        label_refuel_result_temp.Dock = DockStyle.Fill;
                        label_refuel_result_temp.TextAlign = ContentAlignment.MiddleCenter;

                        table_temp.Controls.Add(label_refuel_temp, 0, 0);
                        table_temp.Controls.Add(label_refuel_result_temp, 1, 0);
                        table_temp.Size = tableLayoutPanel_start.Size;

                        groupBox_temp.Controls.Add(table_temp);
                        stint++;
                        y_groupBox += 100;
                    }
                    else if (comboBox_pit_options.Text == "1L refuel")
                    {
                        
                        label_fuel_start_result.Text = (fuel_for_race_round_up - number_of_pits).ToString() + " L";

                        GroupBox groupBox_temp = new GroupBox();
                        groupBox_temp.Name = name_for_groupbox;
                        groupBox_temp.Location = new Point(15, y_groupBox);
                        groupBox_temp.Size = groupBox_start.Size;
                        groupBox_temp.Font = groupBox_car_track.Font;
                        groupBox_temp.BackColor = groupBox_start.BackColor;
                        groupBox_temp.Text = "Stint " + stint;
                        panel_pit_stop_strategy.Controls.Add(groupBox_temp);

                        TableLayoutPanel table_temp = new TableLayoutPanel();
                        table_temp.Name = name_for_table;
                        table_temp.Location = new Point(16, 30);
                        table_temp.ColumnStyles.Clear();
                        table_temp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
                        table_temp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
                        table_temp.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;

                        Label label_refuel_temp = new Label();
                        label_refuel_temp.Name = name_for_refuel_label;
                        label_refuel_temp.Dock = DockStyle.Fill;
                        label_refuel_temp.TextAlign = ContentAlignment.MiddleCenter;
                        if (stint == 1)
                        {
                            label_refuel_temp.Text = "Refuel after " + laps_first_stint.ToString() + " laps";
                            laps_per_stint += laps_first_stint;
                        }
                        else
                        {
                            label_refuel_temp.Text = "Refuel after " + laps_per_stint.ToString() + " laps";
                            laps_per_stint += laps_per_stint;
                        }

                        Label label_refuel_result_temp = new Label();
                        label_refuel_result_temp.Name = name_for_refuel_result_label;
                        label_refuel_result_temp.Text = "1 L";
                        label_refuel_result_temp.Dock = DockStyle.Fill;
                        label_refuel_result_temp.TextAlign = ContentAlignment.MiddleCenter;

                        table_temp.Controls.Add(label_refuel_temp, 0, 0);
                        table_temp.Controls.Add(label_refuel_result_temp, 1, 0);
                        table_temp.Size = tableLayoutPanel_start.Size;

                        groupBox_temp.Controls.Add(table_temp);
                        stint++;
                        y_groupBox += 100;
                    }
                    else
                    {
                        label_fuel_start_result.Text = fuel_first_stint.ToString() + " L";

                        GroupBox groupBox_temp = new GroupBox();
                        groupBox_temp.Name = name_for_groupbox;
                        groupBox_temp.Location = new Point(15, y_groupBox);
                        groupBox_temp.Size = groupBox_start.Size;
                        groupBox_temp.Font = groupBox_car_track.Font;
                        groupBox_temp.BackColor = groupBox_start.BackColor;
                        groupBox_temp.Text = "Stint " + stint;
                        panel_pit_stop_strategy.Controls.Add(groupBox_temp);

                        TableLayoutPanel table_temp = new TableLayoutPanel();
                        table_temp.Name = name_for_table;
                        table_temp.Location = new Point(16, 30);
                        table_temp.ColumnStyles.Clear();
                        table_temp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
                        table_temp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
                        table_temp.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;

                        Label label_refuel_temp = new Label();
                        label_refuel_temp.Name = name_for_refuel_label;
                        label_refuel_temp.Dock = DockStyle.Fill;
                        label_refuel_temp.TextAlign = ContentAlignment.MiddleCenter;

                        if (stint == 1)
                        {
                            label_refuel_temp.Text = "Refuel after " + laps_first_stint.ToString();
                            laps_per_stint += laps_first_stint;
                        }
                        else
                        {
                            label_refuel_temp.Text = "Refuel after " + laps_per_stint.ToString();
                            laps_per_stint += laps_per_stint;
                        }

                        Label label_refuel_result_temp = new Label();
                        label_refuel_result_temp.Name = name_for_refuel_result_label;
                        label_refuel_result_temp.Text = fuel_per_stint.ToString() + " L";
                        label_refuel_result_temp.Dock = DockStyle.Fill;
                        label_refuel_result_temp.TextAlign = ContentAlignment.MiddleCenter;

                        table_temp.Controls.Add(label_refuel_temp, 0, 0);
                        table_temp.Controls.Add(label_refuel_result_temp, 1, 0);
                        table_temp.Size = tableLayoutPanel_start.Size;

                        groupBox_temp.Controls.Add(table_temp);
                        stint++;
                        y_groupBox += 100;
                    }

                }
            }
        }

        private void LoadData()
        {
            if ((comboBox_track.Text != "TRACK"))
            {
                string selected_car = comboBox_car.Text;
                string selected_track = comboBox_track.Text;
                var track = all_tracks.Where(track => track.track_name.Equals(selected_track)).First();

                float.TryParse((track.track_lap_time.Replace(".", DECIMAL_SEPARATOR).Replace(",", DECIMAL_SEPARATOR)),
                    out float lap_time);
                int lap_time_mins = (int)(lap_time / 60);
                float lap_time_secs = lap_time % 60;
                textBox_lap_time_min.Text = lap_time_mins.ToString();
                textBox_lap_time_sec.Text = lap_time_secs.ToString("0.000");

                if (comboBox_car.Text != "CAR")
                {
                    CarTrackFuel car_fuel = track.car_track_fuel.Find(car => car.car_name == selected_car);
                    textBox_fuel_per_lap.Text = car_fuel.fuel_per_lap;
                }
            }
        }

        private void SaveData()
        {
            foreach (var track in all_tracks)
            {
                if (track.track_name.Equals(comboBox_track.Text))
                {
                    track.track_lap_time = lap_time_secs.ToString();

                    foreach (var car_fuel in track.car_track_fuel)
                    {
                        if (car_fuel.car_name.Equals(comboBox_car.Text))
                        {
                            car_fuel.fuel_per_lap = textBox_fuel_per_lap.Text;
                        }
                    }
                }
            }

            if (File.Exists("TRACKS.json"))
            {
                string json_tracks_save = JsonConvert.SerializeObject(all_tracks, Formatting.Indented);
                File.WriteAllText("TRACKS.json", json_tracks_save);
            }
        }


        public Form1()
        {
            InitializeComponent();
            LoadCarClasses();
            LoadTracks();
            LoadPitOptions();

            listBox_formation.Items.Add("Full");
            listBox_formation.Items.Add("Short");
            listBox_formation.SelectedIndex = 0;
        }

        private void button_calculate_Click(object sender, EventArgs e)
        {
            if (comboBox_pit_options.Text == "Refuel only")
            {
                time_lost_in_pits = 57;
                for (int i = 3; i > 0; i--)
                {
                    CalculateRaceDuration();
                    CalculateLapTimePlusMinus();
                    CalculateFuel();
                    RefuelTimeLost();
                }
            }
            else
            {
                CalculateTimeLostInPits();
                CalculateRaceDuration();
                CalculateLapTimePlusMinus();
                CalculateFuel();
            }
            CalculatePitStops();
            SaveData();
        }

        private void comboBox_car_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void comboBox_track_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

    }
}