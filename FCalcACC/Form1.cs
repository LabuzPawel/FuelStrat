using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace FCalcACC
{
    public partial class Form1 : Form
    {
        public class Car
        {
            public string car_name { get; set; }
            public string class_name { get; set; }
        }

        public class Track
        {
            public string track_name { get; set; }
            public string track_lap_time { get; set; }
            public string track_pit_duration { get; set; }
            public List<CarTrackFuel> car_track_fuel { get; set; }
        }

        public class CarTrackFuel
        {
            public string car_name { get; set; }
            public string fuel_per_lap { get; set; }
            public string tank_capacity { get; set; }
        }

        public int GetTankCapacity(string carName, string trackName)
        {
            var selected_track = all_tracks.FirstOrDefault(track => track.track_name.Equals(comboBox_track.Text));
            var selected_trackCarFuel = selected_track.car_track_fuel.FirstOrDefault(
                carTrackFuel => carTrackFuel.car_name.Equals(comboBox_car.Text));
            return int.Parse(selected_trackCarFuel.tank_capacity);
        }

        public List<Track> all_tracks;
        public List<Car> all_cars;
        public List<string> car_classes;

        public double time_lost_in_pits;
        public int time_in_pits;
        public int number_of_laps;
        public double overall_race_duration;
        public int race_duration_secs;
        public float lap_time_secs;
        public int fuel_for_race_round_up;
        public float fuel_per_lap;
        public int number_of_pits;
        public double formation_lap_fuel;
        private double fuel_for_race;

        private string DECIMAL_SEPARATOR = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        private double ONE_L_PIT_TIME = 3.6;
        private double ONE_L_MORE = 0.2;
        public int DEFAULT_TIME_IN_PITS = 57;

        public List<string> PIT_OPTIONS = new List<string>()
        {
            "Refuel only",
            "Fixed refuel only",
            "Tires only",
            "Refuel + tires",
            "1L refuel"
        };

        public void LoadCarTrackObjects()
        {
            string cars_resourse_name = "FCalcACC.car_track_data.CARS.json";
            string tracks_resourse_name = "FCalcACC.car_track_data.TRACKS.json";
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(cars_resourse_name))
            using (StreamReader reader = new StreamReader(stream))
            {
                string cars_embedded = reader.ReadToEnd();
                all_cars = JsonConvert.DeserializeObject<List<Car>>(cars_embedded);
            }
            if (File.Exists("FCalcACC_data.json") == false)
            {
                using (Stream stream = assembly.GetManifestResourceStream(tracks_resourse_name))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string tracks_embedded = reader.ReadToEnd();
                    all_tracks = JsonConvert.DeserializeObject<List<Track>>(tracks_embedded);
                }
            }
            else
            {
                try
                {
                    string json_string_tracks = File.ReadAllText("FCalcACC_data.json");
                    all_tracks = JsonConvert.DeserializeObject<List<Track>>(json_string_tracks);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error reading FCalcACC_data.json:\n" + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    DialogResult result = MessageBox.Show("Would you like to reset the FCalcACC_data.json?\n\n" +
                        "Choosing 'No' will exit application.",
                        "Reset data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        File.Delete("FCalcACC_data.json");
                        LoadCarTrackObjects();
                    }
                    else if (result == DialogResult.No)
                    {
                        Application.Exit();
                    }
                }
            }
        }

        public void LoadCarClasses(ComboBox comboBoxClass)
        {
            car_classes = all_cars.Select(car => car.class_name).Distinct().ToList();

            foreach (var car_class in car_classes)
            {
                comboBoxClass.Items.Add(car_class);
            }
        }

        public void LoadCars(ComboBox comboBoxCar, string car_class)
        {
            comboBoxCar.ResetText();
            comboBoxCar.Items.Clear();

            var cars_within_a_class = all_cars.Where(car => car.class_name.Contains(car_class));

            foreach (var car in cars_within_a_class)
            {
                comboBoxCar.Items.Add(car.car_name);
            }
            comboBoxCar.Text = "CAR";

            if (car_class == "TCX")
            {
                comboBoxCar.SelectedIndex = 0;
            }
        }

        public void LoadTracks(ComboBox comboBoxTrack)
        {
            foreach (var track in all_tracks)
            {
                comboBoxTrack.Items.Add(track.track_name);
            }
        }

        public void LoadPitOptions(ComboBox comboBoxPit, List<string> pitOptions)
        {
            foreach (var pit_option in pitOptions)
            {
                comboBoxPit.Items.Add(pit_option);
            }
        }

        private void comboBox_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCars(comboBox_car, comboBox_class.Text);
        }

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

        public void CalculateTimeLostInPits(int number_of_pits, ComboBox comboBoxPitOptions, string selected_track)
        {
            if (number_of_pits > 0 && comboBoxPitOptions.SelectedIndex == -1)
            {
                comboBoxPitOptions.SelectedIndex = 3;
            }

            if (selected_track == "TRACK")
            {
                switch (comboBoxPitOptions.Text)
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
                var selected_track_object = all_tracks.FirstOrDefault(track => track.track_name.Contains(selected_track));
                int.TryParse(selected_track_object.track_pit_duration, out time_in_pits);

                switch (comboBoxPitOptions.Text)
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

        public void RefuelTimeLost(string selected_track, NumericUpDown numericUpDownPits)
        {
            number_of_pits = (int)numericUpDownPits.Value;

            int time_in_pits;
            if (selected_track != "TRACK")
            {
                var selected_track_object = all_tracks.FirstOrDefault(track => track.track_name.Contains(selected_track));
                int.TryParse(selected_track_object.track_pit_duration, out time_in_pits);
            }
            else
            {
                time_in_pits = DEFAULT_TIME_IN_PITS;
            }

            double laps_per_stint = number_of_laps / (double)(number_of_pits + 1);
            double fuel_for_stint = laps_per_stint * fuel_per_lap;
            int fuel_for_stint_round_up = (int)Math.Ceiling(fuel_for_stint);
            int refuel = fuel_for_race_round_up - (int)formation_lap_fuel - fuel_for_stint_round_up;
            refuel = (int)Math.Ceiling((double)refuel / number_of_pits);

            time_in_pits -= 30;
            time_lost_in_pits = time_in_pits + ONE_L_PIT_TIME;
            refuel--;
            while (refuel > 0)
            {
                time_lost_in_pits += ONE_L_MORE;
                refuel--;
            }
            time_lost_in_pits *= number_of_pits;
        }

        public void CalculateRaceDuration(TextBox TextBoxRaceH, TextBox textBoxRaceMin, TextBox textBoxLapMin,
            TextBox textBoxLapSec, Label labelOverallResult, Label labelLapsResult, Label labelLapTimeResult2)
        {
            int.TryParse(TextBoxRaceH.Text, out int h_race);
            int.TryParse(textBoxRaceMin.Text, out int min_race);
            if (min_race > 59)
            {
                min_race = 59;
                textBoxRaceMin.Text = min_race.ToString();
            }

            string lap_time_sec_normalize = textBoxLapSec.Text.Replace(".", DECIMAL_SEPARATOR).Replace(",", DECIMAL_SEPARATOR);
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
            }

            float.TryParse(lap_time_sec_normalize, out float sec_laptime);
            int.TryParse(textBoxLapMin.Text, out int min_laptime);
            if (min_laptime > 59)
            {
                min_laptime = 59;
                textBoxLapMin.Text = min_laptime.ToString();
            }
            if (sec_laptime > 59.99999)
            {
                sec_laptime = 59.999f;
                textBoxLapSec.Text = sec_laptime.ToString();
            }

            int lap_time_secs_floor = (int)Math.Floor(sec_laptime);
            int lap_time_secs_rest = (int)Math.Round((sec_laptime - lap_time_secs_floor) * 1000);
            string formatted_lap_time = string.Format("{0:D1}:{1:D2}.{2:000}",
                textBoxLapMin.Text, lap_time_secs_floor, lap_time_secs_rest);
            labelLapTimeResult2.Text = formatted_lap_time;

            race_duration_secs = (h_race * 3600) + (min_race * 60);
            lap_time_secs = (min_laptime * 60) + sec_laptime;

            overall_race_duration = time_lost_in_pits;

            number_of_laps = 0;

            while (overall_race_duration < race_duration_secs)
            {
                overall_race_duration += lap_time_secs;
                number_of_laps++;
            }
            labelLapsResult.Text = number_of_laps.ToString();

            TimeSpan time_interval = TimeSpan.FromSeconds(overall_race_duration);
            string formatted_overall_duration = string.Format("{0:D2}:{1:D2}:{2:D2}",
                (int)time_interval.TotalHours, time_interval.Minutes, time_interval.Seconds);

            labelOverallResult.Text = formatted_overall_duration;
        }

        public void CalculateLapTimePlusMinus(Label labelPlus1LapTimeResult, Label labelMinus1LapTimeResult)
        {
            double plus1_lap_time_secs = lap_time_secs - ((overall_race_duration - race_duration_secs) / number_of_laps);
            int plus1_lap_time_floor = (int)Math.Floor(plus1_lap_time_secs);
            int plus1_lap_time_rest = (int)Math.Round((plus1_lap_time_secs - plus1_lap_time_floor) * 1000);
            TimeSpan time_interval_plus = TimeSpan.FromSeconds(plus1_lap_time_floor);
            string formatted_plus1_lap_time = string.Format("{0:D1}:{1:D2}.{2:000}",
                (int)time_interval_plus.TotalMinutes, (int)time_interval_plus.Seconds, (int)plus1_lap_time_rest);
            labelPlus1LapTimeResult.Text = formatted_plus1_lap_time;

            double minus1_lap_time_secs = (race_duration_secs - time_lost_in_pits) / (number_of_laps - 1);
            int minus1_lap_time_floor = (int)Math.Floor(minus1_lap_time_secs);
            int minus1_lap_time_rest = (int)Math.Round((minus1_lap_time_secs - minus1_lap_time_floor) * 1000);
            TimeSpan time_interval_minus = TimeSpan.FromSeconds(minus1_lap_time_floor);
            string formatted_minus1_lap_time = string.Format("{0:D1}:{1:D2}.{2:000}",
                (int)time_interval_minus.TotalMinutes, (int)time_interval_minus.Seconds, (int)minus1_lap_time_rest);
            labelMinus1LapTimeResult.Text = formatted_minus1_lap_time;
        }

        public void CalculateFuel(TextBox textBoxFuelPerLap, ListBox listBoxformationLap, Label labelFuelRaceResult,
            Label labelPlus1FuelResult, Label labelMinus1FuelResult)
        {
            string fuel_per_lap_normalize = textBoxFuelPerLap.Text.Replace(".", DECIMAL_SEPARATOR).Replace(",", DECIMAL_SEPARATOR);
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
                fuel_per_lap_normalize = fuel_per_lap_normalize.Replace(DECIMAL_SEPARATOR, string.Empty)
                    .Insert(fuel_per_lap_normalize.IndexOf(DECIMAL_SEPARATOR[0]), DECIMAL_SEPARATOR);
                textBoxFuelPerLap.Text = fuel_per_lap_normalize;
            }
            float.TryParse(fuel_per_lap_normalize, out fuel_per_lap);

            bool full_formation_lap = listBoxformationLap.Text == "Full";
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

            fuel_for_race = (number_of_laps * (fuel_per_lap + 0.01)) + formation_lap_fuel;
            fuel_for_race_round_up = (int)Math.Ceiling(fuel_for_race);
            labelFuelRaceResult.Text = fuel_for_race_round_up.ToString() + " L";

            double fuel_plus1 = fuel_for_race + (fuel_per_lap + 0.01);
            int fuel_plus1_round_up = (int)Math.Ceiling(fuel_plus1);
            labelPlus1FuelResult.Text = fuel_plus1_round_up.ToString() + " L";

            double fuel_minus1 = fuel_for_race - (fuel_per_lap + 0.01);
            int fuel_minus1_round_up = (int)Math.Ceiling(fuel_minus1);
            labelMinus1FuelResult.Text = fuel_minus1_round_up.ToString() + " L";
        }

        public void CalculatePitStops(Panel panelPitStopStrategy, Label labelFuelRaceResult, NumericUpDown numericUpDownPits,
            ComboBox comboBoxPitOptions, out string labelFuelStartResultText, out List<int> fuelPerStint,
            out List<int> lapsPitStint)
        {
            panelPitStopStrategy.Controls.Clear();

            GroupBox groupBox_start = new GroupBox();
            groupBox_start.Text = "Stint 1 - Start of the race";
            groupBox_start.BackColor = Color.Gainsboro;
            groupBox_start.Font = groupBox_car_track.Font;
            groupBox_start.Location = new Point(16, 26);
            Size groupBox_size = new Size(385, 83);
            groupBox_start.Size = groupBox_size;
            panelPitStopStrategy.Controls.Add(groupBox_start);

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

            number_of_pits = (int)numericUpDownPits.Value;

            fuelPerStint = new List<int>();
            lapsPitStint = new List<int>();
            labelFuelStartResultText = "";

            int tank_capacity;

            if (comboBox_car.Text == "CAR")
            {
                tank_capacity = 999999;
            }
            else
            {
                tank_capacity = GetTankCapacity(comboBox_car.Text, comboBox_track.Text);
            }

            if (comboBoxPitOptions.Text != "" && checkBox_max_stint.Checked == false &&
                number_of_pits == 0)
            {
                if (comboBoxPitOptions.Text == "1L refuel" || comboBoxPitOptions.Text == "Tires only")
                {
                    number_of_pits = 1;
                    numericUpDownPits.Value = number_of_pits;
                    button_calculate_Click(button_calculate, EventArgs.Empty);
                    return;
                }
                else if (comboBox_car.Text != "CAR")
                {
                    number_of_pits = fuel_for_race_round_up / tank_capacity;
                    numericUpDownPits.Value = number_of_pits;
                    button_calculate_Click(button_calculate, EventArgs.Empty);
                    return;
                }
            }
            else if (comboBoxPitOptions.SelectedIndex != -1 && checkBox_max_stint.Checked == true &&
                number_of_pits == 0)
            {
                number_of_pits = race_duration_secs / (int.Parse(textBox_max_stint.Text) * 60);
                numericUpDownPits.Value = number_of_pits;
                button_calculate_Click(button_calculate, EventArgs.Empty);
                return;
            }

            if ((number_of_pits + 1) * (int.Parse(textBox_max_stint.Text) * 60) < race_duration_secs &&
                checkBox_max_stint.Checked == true)
            {
                number_of_pits = race_duration_secs / (int.Parse(textBox_max_stint.Text) * 60);
                numericUpDownPits.Value = number_of_pits;
                button_calculate_Click(button_calculate, EventArgs.Empty);
                return;
            }

            if (number_of_pits == 0)
            {
                label_fuel_start_result.Text = labelFuelRaceResult.Text;
                labelFuelStartResultText = label_fuel_start_result.Text;
                fuelPerStint.Add(fuel_for_race_round_up);

                if (fuel_for_race_round_up > tank_capacity)
                {
                    label_fuel_start_result.ForeColor = Color.Red;
                    groupBox_start.Size = new Size(385, 103);
                    Label label_tank = new Label();
                    label_tank.Text = "Exceed fuel tank capacity of " + tank_capacity.ToString() + " L";
                    label_tank.Size = new Size(350, 30);
                    label_tank.Location = new Point(16, 74);
                    groupBox_start.Controls.Add(label_tank);
                }
            }
            else
            {
                double current_laps = 0.0;
                int stints_left = number_of_pits;

                double laps_per_stint = (double)number_of_laps / (number_of_pits + 1);
                double number_of_laps_remaining = number_of_laps;

                int y_groupBox = 120;

                int fuel_first_stint = (int)Math.Ceiling((laps_per_stint * fuel_per_lap) + formation_lap_fuel);
                fuel_first_stint = Math.Min(fuel_first_stint, tank_capacity);
                double fuel_remaining = fuel_for_race_round_up - fuel_first_stint;
                labelFuelStartResultText = fuel_first_stint.ToString() + " L";
                fuelPerStint.Add(fuel_first_stint);

                if (fuel_first_stint > tank_capacity)
                {
                    int laps_per_stint_tank = (int)(fuel_first_stint / fuel_per_lap);
                    laps_per_stint = laps_per_stint_tank;
                }

                if (checkBox_max_stint.Checked == true && textBox_max_stint.Text != "0")
                {
                    int laps_per_stint_max = (int)((int.Parse(textBox_max_stint.Text)) * 60 / lap_time_secs);
                    laps_per_stint = laps_per_stint_max;
                }

                List<int> fuel_1L_adjusted = new List<int>();
                int full_tank_count = fuel_for_race_round_up / tank_capacity;
                int full_tank_sum = 0;

                for (int i = full_tank_count; i > 0; i--)
                {
                    fuel_1L_adjusted.Add(tank_capacity);
                    full_tank_sum += tank_capacity;
                }

                int pit_stops_left = number_of_pits - full_tank_count;
                int fuel_1L_adjusted_remaining = fuel_for_race_round_up - full_tank_sum -
                    (pit_stops_left);

                fuel_1L_adjusted.Add(fuel_1L_adjusted_remaining);

                for (int i = pit_stops_left; i > 0; i--)
                {
                    fuel_1L_adjusted.Add(1);
                }

                int stint = 2;
                for (int i = number_of_pits; i > 0; i--)
                {
                    string name_for_groupbox = "groupBox_stint" + stint.ToString();
                    string name_for_refuel_label = "label_refuel_stint" + stint.ToString();
                    string name_for_refuel_result_label = "label_refuel_stint" + stint.ToString() + "_result";
                    string name_for_table = "tableLayoutPanel_stint" + stint.ToString();

                    if (comboBoxPitOptions.Text == "Tires only")
                    {
                        label_fuel_start_result.Text = label_fuel_race_result.Text;

                        if (fuel_for_race_round_up > tank_capacity)
                        {
                            label_fuel_start_result.ForeColor = Color.Red;
                            groupBox_start.Size = new Size(385, 103);
                            Label label_tank = new Label();
                            label_tank.Text = "Exceed fuel tank capacity of " + tank_capacity.ToString() + " L";
                            label_tank.Size = new Size(350, 30);
                            label_tank.Location = new Point(16, 74);
                            groupBox_start.Controls.Add(label_tank);

                            if (i == number_of_pits)
                            {
                                y_groupBox += 25;
                            }
                        }

                        GroupBox groupBox_temp = new GroupBox();
                        groupBox_temp.Name = name_for_groupbox;
                        groupBox_temp.Location = new Point(16, y_groupBox);
                        groupBox_temp.Font = groupBox_car_track.Font;
                        groupBox_temp.Size = groupBox_size;
                        groupBox_temp.BackColor = groupBox_start.BackColor;
                        groupBox_temp.Text = "Stint " + stint;
                        panelPitStopStrategy.Controls.Add(groupBox_temp);

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

                        double current_part = Math.Min(number_of_laps_remaining, laps_per_stint);
                        current_laps += current_part;
                        label_refuel_temp.Text = "Tires change after";
                        number_of_laps_remaining -= laps_per_stint;
                        fuelPerStint.Add(0);
                        lapsPitStint.Add((int)Math.Ceiling(current_laps));

                        Label label_refuel_result_temp = new Label();
                        label_refuel_result_temp.Name = name_for_refuel_result_label;
                        label_refuel_result_temp.Text = ((int)Math.Ceiling(current_laps)).ToString() + " laps";
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
                    else if (comboBoxPitOptions.Text == "1L refuel")
                    {
                        bool is_strat_ok = true;

                        if (fuel_for_race_round_up > tank_capacity)
                        {
                            groupBox_start.Size = new Size(385, 123);
                            Label label_tank = new Label();
                            label_tank.Text = "Exceed fuel tank capacity of " + tank_capacity.ToString() + " L for 1L strategy. " +
                                "Adjusted with higher refuel.";
                            label_tank.Size = new Size(350, 50);
                            label_tank.Location = new Point(16, 74);
                            groupBox_start.Controls.Add(label_tank);

                            if (i == number_of_pits)
                            {
                                y_groupBox += 45;
                            }

                            is_strat_ok = false;
                        }

                        switch (is_strat_ok)
                        {
                            case true:
                                label_fuel_start_result.Text = (fuel_for_race_round_up - number_of_pits).ToString() + " L";

                                GroupBox groupBox_temp = new GroupBox();
                                groupBox_temp.Name = name_for_groupbox;
                                groupBox_temp.Location = new Point(16, y_groupBox);
                                groupBox_temp.Size = groupBox_size;
                                groupBox_temp.Font = groupBox_car_track.Font;
                                groupBox_temp.BackColor = groupBox_start.BackColor;
                                groupBox_temp.Text = "Stint " + stint;
                                panelPitStopStrategy.Controls.Add(groupBox_temp);

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

                                double current_part = Math.Min(number_of_laps_remaining, laps_per_stint);
                                current_laps += current_part;
                                label_refuel_temp.Text = "Refuel after " + ((int)Math.Ceiling(current_laps)) + " laps";
                                number_of_laps_remaining -= laps_per_stint;
                                lapsPitStint.Add((int)Math.Ceiling(current_laps));

                                Label label_refuel_result_temp = new Label() { Text = "1 L" };
                                label_refuel_result_temp.Name = name_for_refuel_result_label;
                                label_refuel_result_temp.Dock = DockStyle.Fill;
                                label_refuel_result_temp.TextAlign = ContentAlignment.MiddleCenter;
                                fuelPerStint.Add(1);

                                table_temp.Controls.Add(label_refuel_temp, 0, 0);
                                table_temp.Controls.Add(label_refuel_result_temp, 1, 0);
                                table_temp.Size = tableLayoutPanel_start.Size;

                                groupBox_temp.Controls.Add(table_temp);
                                stint++;
                                y_groupBox += 100;
                                break;

                            case false:
                                label_fuel_start_result.Text = tank_capacity.ToString() + " L";

                                GroupBox groupBox_temp2 = new GroupBox();
                                groupBox_temp2.Name = name_for_groupbox;
                                groupBox_temp2.Location = new Point(16, y_groupBox);
                                groupBox_temp2.Size = groupBox_size;
                                groupBox_temp2.Font = groupBox_car_track.Font;
                                groupBox_temp2.BackColor = groupBox_start.BackColor;
                                groupBox_temp2.Text = "Stint " + stint;
                                panelPitStopStrategy.Controls.Add(groupBox_temp2);

                                TableLayoutPanel table_temp2 = new TableLayoutPanel();
                                table_temp2.Name = name_for_table;
                                table_temp2.Location = new Point(16, 30);
                                table_temp2.ColumnStyles.Clear();
                                table_temp2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
                                table_temp2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
                                table_temp2.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;

                                Label label_refuel_temp2 = new Label();
                                label_refuel_temp2.Name = name_for_refuel_label;
                                label_refuel_temp2.Dock = DockStyle.Fill;
                                label_refuel_temp2.TextAlign = ContentAlignment.MiddleCenter;

                                double current_part_laps = Math.Min(number_of_laps_remaining, laps_per_stint);
                                current_laps += current_part_laps;
                                label_refuel_temp2.Text = "Refuel after " + ((int)Math.Ceiling(current_laps)) + " laps";
                                number_of_laps_remaining -= laps_per_stint;
                                lapsPitStint.Add((int)Math.Ceiling(current_laps));

                                int fuel_for_this_stint = fuel_1L_adjusted[stint - 1];
                                stints_left--;
                                fuelPerStint.Add(fuel_for_this_stint);

                                Label label_refuel_result_temp2 = new Label();
                                label_refuel_result_temp2.Name = name_for_refuel_result_label;
                                label_refuel_result_temp2.Text = fuel_for_this_stint.ToString() + " L";
                                label_refuel_result_temp2.Dock = DockStyle.Fill;
                                label_refuel_result_temp2.TextAlign = ContentAlignment.MiddleCenter;

                                table_temp2.Controls.Add(label_refuel_temp2, 0, 0);
                                table_temp2.Controls.Add(label_refuel_result_temp2, 1, 0);
                                table_temp2.Size = tableLayoutPanel_start.Size;

                                groupBox_temp2.Controls.Add(table_temp2);
                                y_groupBox += 100;
                                stint++;
                                break;
                        }

                        if (i == 1 && pit_stops_left < 0)
                        {
                            GroupBox groupBox_warning = new GroupBox();
                            groupBox_warning.Name = name_for_groupbox + "_warning";
                            groupBox_warning.Location = new Point(16, y_groupBox);
                            groupBox_warning.Size = new Size(385, 135);
                            groupBox_warning.Font = groupBox_car_track.Font;
                            groupBox_warning.ForeColor = Color.Red;
                            groupBox_warning.BackColor = groupBox_start.BackColor;
                            groupBox_warning.Text = "WARNING";
                            panelPitStopStrategy.Controls.Add(groupBox_warning);

                            Label label_warning = new Label();
                            label_warning.Name = name_for_refuel_result_label + "_warning";
                            label_warning.Text = "Fuel needed for this race (" + fuel_for_race_round_up.ToString() +
                                " L) is greater than sum of full tank pit stops (" + full_tank_sum.ToString() + " L). " +
                                "\n\nConsider changing pit option to refuel and increase number of pit stops or " +
                                "fuel saving (" + (fuel_for_race_round_up - full_tank_sum).ToString() + " L) during the race.";
                            label_warning.Location = new Point(16, 30);
                            label_warning.Size = new Size(350, 90);
                            label_warning.ForeColor = Color.Black;
                            groupBox_warning.Controls.Add(label_warning);
                        }
                    }
                    else
                    {
                        label_fuel_start_result.Text = fuel_first_stint.ToString() + " L";

                        GroupBox groupBox_temp = new GroupBox();
                        groupBox_temp.Name = name_for_groupbox;
                        groupBox_temp.Location = new Point(16, y_groupBox);
                        groupBox_temp.Size = groupBox_size;
                        groupBox_temp.Font = groupBox_car_track.Font;
                        groupBox_temp.BackColor = groupBox_start.BackColor;
                        groupBox_temp.Text = "Stint " + stint;
                        panelPitStopStrategy.Controls.Add(groupBox_temp);

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

                        double current_part_laps = Math.Min(number_of_laps_remaining, laps_per_stint);
                        current_laps += current_part_laps;
                        label_refuel_temp.Text = "Refuel after " + ((int)Math.Ceiling(current_laps)) + " laps";
                        number_of_laps_remaining -= laps_per_stint;
                        lapsPitStint.Add((int)Math.Ceiling(current_laps));

                        double current_part_fuel = Math.Min(fuel_remaining,
                            Math.Min((fuel_remaining / stints_left), tank_capacity));
                        int fuel_for_this_stint = (int)Math.Ceiling(current_part_fuel);
                        fuel_remaining -= fuel_for_this_stint;
                        stints_left--;
                        fuelPerStint.Add(fuel_for_this_stint);

                        Label label_refuel_result_temp = new Label();
                        label_refuel_result_temp.Name = name_for_refuel_result_label;
                        label_refuel_result_temp.Text = fuel_for_this_stint.ToString() + " L";
                        label_refuel_result_temp.Dock = DockStyle.Fill;
                        label_refuel_result_temp.TextAlign = ContentAlignment.MiddleCenter;

                        table_temp.Controls.Add(label_refuel_temp, 0, 0);
                        table_temp.Controls.Add(label_refuel_result_temp, 1, 0);
                        table_temp.Size = tableLayoutPanel_start.Size;

                        groupBox_temp.Controls.Add(table_temp);
                        y_groupBox += 100;
                        stint++;

                        if (i == 1 && pit_stops_left < 0)
                        {
                            GroupBox groupBox_warning = new GroupBox();
                            groupBox_warning.Name = name_for_groupbox + "_warning";
                            groupBox_warning.Location = new Point(16, y_groupBox);
                            groupBox_warning.Size = new Size(385, 135);
                            groupBox_warning.Font = groupBox_car_track.Font;
                            groupBox_warning.ForeColor = Color.Red;
                            groupBox_warning.BackColor = groupBox_start.BackColor;
                            groupBox_warning.Text = "WARNING";
                            panelPitStopStrategy.Controls.Add(groupBox_warning);

                            Label label_warning = new Label();
                            label_warning.Name = name_for_refuel_result_label + "_warning";
                            label_warning.Text = "Fuel needed for this race (" + fuel_for_race_round_up.ToString() +
                                " L) is greater than sum of full tank pit stops (" + full_tank_sum.ToString() + " L). " +
                                "\n\nConsider increase number of pit stops or " +
                                "fuel saving (" + (fuel_for_race_round_up - full_tank_sum).ToString() + " L) during the race.";
                            label_warning.Location = new Point(16, 30);
                            label_warning.Size = new Size(350, 90);
                            label_warning.ForeColor = Color.Black;
                            groupBox_warning.Controls.Add(label_warning);
                        }
                    }
                }
            }
        }

        private void LoadData()
        {
            if (comboBox_track.Text != "TRACK")
            {
                string selected_car = comboBox_car.Text;
                string selected_track = comboBox_track.Text;
                var track = all_tracks.Where(track => track.track_name.Equals(selected_track)).First();

                float.TryParse(track.track_lap_time.Replace(".", DECIMAL_SEPARATOR).Replace(",", DECIMAL_SEPARATOR),
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

            string json_data_save = JsonConvert.SerializeObject(all_tracks, Formatting.Indented);
            File.WriteAllText("FCalcACC_data.json", json_data_save);
        }

        public Form1()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            LoadCarTrackObjects();
            if (File.Exists("FCalcACC_data.json") == false)
            {
                SaveData();
                MessageBox.Show("'FCalcACC_data.json' created in application directory");
            }
            LoadCarClasses(comboBox_class);
            LoadTracks(comboBox_track);
            LoadPitOptions(comboBox_pit_options, PIT_OPTIONS);

            listBox_formation.SelectedIndex = 0;
        }

        private void button_calculate_Click(object sender, EventArgs e)
        {
            if (comboBox_pit_options.Text == "Refuel only")
            {
                time_lost_in_pits = 57;
                for (int i = 3; i > 0; i--)
                {
                    CalculateRaceDuration(textBox_race_h, textBox_race_min, textBox_lap_time_min, textBox_lap_time_sec,
                        label_overall_result, label_laps_result, label_lap_time_result2);
                    CalculateLapTimePlusMinus(label_plus1_lap_time_result, label_minus1_lap_time_result);
                    CalculateFuel(textBox_fuel_per_lap, listBox_formation, label_fuel_race_result,
                    label_plus1_fuel_result, label_minus1_fuel_result);
                    RefuelTimeLost(comboBox_track.Text, numericUpDown_pits);
                }
            }
            else
            {
                CalculateTimeLostInPits((int)numericUpDown_pits.Value, comboBox_pit_options, comboBox_track.Text);
                CalculateRaceDuration(textBox_race_h, textBox_race_min, textBox_lap_time_min, textBox_lap_time_sec,
                    label_overall_result, label_laps_result, label_lap_time_result2);
                CalculateLapTimePlusMinus(label_plus1_lap_time_result, label_minus1_lap_time_result);
                CalculateFuel(textBox_fuel_per_lap, listBox_formation, label_fuel_race_result,
                    label_plus1_fuel_result, label_minus1_fuel_result);
            }
            CalculatePitStops(panel_pit_stop_strategy, label_fuel_race_result, numericUpDown_pits, comboBox_pit_options,
                out string labelFuelStartResultTextForTesting, out List<int> fuelPerStintForTesting,
                out List<int> lapsPitStintForTesting);
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void checkBox_max_stint_Click(object sender, EventArgs e)
        {
            if (checkBox_max_stint.Checked == true)
            {
                textBox_max_stint.Enabled = true;
            }
            else if (checkBox_max_stint.Checked == false)
            {
                textBox_max_stint.Enabled = false;
                textBox_max_stint.Text = "0";
            }
        }

        private void gitHubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string github_url = "https://github.com/LabuzPawel/FCalcACC";
            Process.Start(new ProcessStartInfo
            {
                FileName = github_url,
                UseShellExecute = true
            });
        }

        private void resetDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Would you like to reset the FCalcACC_data.json?",
                        "Reset data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                File.Delete("FCalcACC_data.json");
                LoadCarTrackObjects();
            }
        }
    }
}