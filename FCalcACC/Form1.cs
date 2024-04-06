using Newtonsoft.Json;
using System.Diagnostics;
using System.DirectoryServices;
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

            if (carName == "CAR" || trackName == "TRACK")
            {
                return 99999;
            }

            return int.Parse(selected_trackCarFuel.tank_capacity);
        }

        public List<Track> all_tracks;
        public List<Car> all_cars;
        public List<string> car_classes;
        public List<int> fuelPerStint;
        public List<int> lapsPerStint;
        public List<NumericUpDown> dynamic_numericUpDowns = new List<NumericUpDown>();
        public List<int> new_list_of_laps_to_pit = new List<int>();
        public List<int> new_list_of_refuel = new List<int>();
        public List<Label> dynamic_labels = new List<Label>();

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

        private bool is_recalculate_needed = true;
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
            "1L refuel",
            "No pit stops"
        };

        public void LoadCarTrackObjects()
        {
            //Load Cars from embedded json's
            string cars_resourse_name = "FCalcACC.car_track_data.CARS.json";
            string tracks_resourse_name = "FCalcACC.car_track_data.TRACKS.json";
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(cars_resourse_name))
            using (StreamReader reader = new StreamReader(stream))
            {
                string cars_embedded = reader.ReadToEnd();
                all_cars = JsonConvert.DeserializeObject<List<Car>>(cars_embedded);
            }

            //Load Track objects, if no data file then load from embedded
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
                    //catch exception when data file is corrupted or unreadable

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
        public void ResetSpecficCarTrack(ComboBox comboBoxCar, ComboBox comboBoxTrack)
        {
            if (comboBoxCar.Text == "CAR" || comboBoxTrack.Text == "TRACK")
            {
                return;
            }

            string tracks_resourse_name = "FCalcACC.car_track_data.TRACKS.json";
            Assembly assembly = Assembly.GetExecutingAssembly();

            try
            {
                using (Stream stream = assembly.GetManifestResourceStream(tracks_resourse_name))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string tracks_embedded = reader.ReadToEnd();
                    List<Track> temp_tracks = JsonConvert.DeserializeObject<List<Track>>(tracks_embedded);

                    //Find the specific track in the list
                    Track default_track = temp_tracks.Find(t => t.track_name == comboBoxTrack.Text);
                    CarTrackFuel default_car_track_fuel = default_track.car_track_fuel.Find(
                        c => c.car_name == comboBoxCar.Text);
                    foreach (var track in all_tracks)
                    {
                        if (track.track_name.Equals(comboBox_track.Text))
                        {
                            track.track_lap_time = default_track.track_lap_time;
                            
                            foreach (var car_fuel in track.car_track_fuel)
                            {
                                if (car_fuel.car_name.Equals(comboBox_car.Text))
                                {
                                    car_fuel.fuel_per_lap = default_car_track_fuel.fuel_per_lap;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
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
            //Load cars from selected class when comboBox_class changes
            LoadCars(comboBox_car, comboBox_class.Text);
        }

        //Restrictions to key presses in textBoxes, numericUpDowns
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

        private void textBox_max_stint_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        public void CalculateTimeLostInPits(int number_of_pits, ComboBox comboBoxPitOptions, string selected_track)
        {
            if (number_of_pits > 0 && comboBoxPitOptions.SelectedIndex == -1)
            {
                //change to default pit option (tires and fuel) if nothing is selected AND
                //number of pits is different than 0
                comboBoxPitOptions.SelectedIndex = 3;
            }

            if (selected_track == "TRACK")
            {
                //calculate time lost in pits when track ISNT selected

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
                //calculate time lost in pits when track IS selected

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
            //funtion used when 'refuel only' is selected in pit options
            //time lost depends of liters during refuel

            number_of_pits = (int)numericUpDownPits.Value;

            //this statement is needed to skip this function in a situation when number of pits is unknown (0) and
            //will change at the start of CalculatePitStops
            if (number_of_pits == 0)
            {
                return;
            }

            int time_in_pits;
            if (selected_track != "TRACK")
            {
                //when track IS selected
                var selected_track_object = all_tracks.FirstOrDefault(track => track.track_name.Contains(selected_track));
                int.TryParse(selected_track_object.track_pit_duration, out time_in_pits);
            }
            else
            {
                //when track ISNT selected
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
            //calculates overall race duration based on how many laps with a given lap time
            //will be before end of the race, time lost in pits is added at the start

            int.TryParse(TextBoxRaceH.Text, out int h_race);
            int.TryParse(textBoxRaceMin.Text, out int min_race);
            if (min_race > 59)
            {
                //if someone iserts more than 59mins then set 59
                min_race = 59;
                textBoxRaceMin.Text = min_race.ToString();
            }

            //make sure that it doesnt matter if decimal separator is . or ,
            //when more than one separator deletes all decimal separators except the first one
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
                //if someone iserts more than 59mins then set 59
                min_laptime = 59;
                textBoxLapMin.Text = min_laptime.ToString();
            }
            if (sec_laptime > 59.99999)
            {
                //if someone iserts more than 59.999secs then set 59.999
                sec_laptime = 59.999f;
                textBoxLapSec.Text = sec_laptime.ToString();
            }

            //fill label in results with user lap time
            int lap_time_secs_floor = (int)Math.Floor(sec_laptime);
            int lap_time_secs_rest = (int)Math.Round((sec_laptime - lap_time_secs_floor) * 1000);
            string formatted_lap_time = string.Format("{0:D1}:{1:D2}.{2:000}",
                textBoxLapMin.Text, lap_time_secs_floor, lap_time_secs_rest);
            labelLapTimeResult2.Text = formatted_lap_time;

            race_duration_secs = (h_race * 3600) + (min_race * 60);
            lap_time_secs = (min_laptime * 60) + sec_laptime;

            //overall race duration starts with time lost in pits
            overall_race_duration = time_lost_in_pits;

            number_of_laps = 0;

            //add lap times until threshold of race duration is crossed
            while (overall_race_duration < race_duration_secs)
            {
                overall_race_duration += lap_time_secs;
                number_of_laps++;
            }

            //fill label in results with number of laps
            labelLapsResult.Text = number_of_laps.ToString();

            //fill label in results with overall race duration
            TimeSpan time_interval = TimeSpan.FromSeconds(overall_race_duration);
            string formatted_overall_duration = string.Format("{0:D2}:{1:D2}:{2:D2}",
                (int)time_interval.TotalHours, time_interval.Minutes, time_interval.Seconds);

            labelOverallResult.Text = formatted_overall_duration;
        }

        public void CalculateLapTimePlusMinus(Label labelPlus1LapTimeResult, Label labelMinus1LapTimeResult)
        {
            //calculates lap times that are needed for the race to be one lap longer and shorter

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
            //calculates fuel for the race based on number of laps and formation lap

            //make sure that it doesnt matter if decimal separator is . or ,
            //when more than one separator deletes all decimal separators except the first one
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

            //taking into account that formation lap can use a bit more fuel while
            //heating up tires with brake dragging
            if (full_formation_lap)
            {
                brake_dragging_full = fuel_per_lap + (fuel_per_lap * 0.1);
            }
            else
            {
                brake_dragging_short = fuel_per_lap * 0.25;
            }
            formation_lap_fuel = brake_dragging_full + brake_dragging_short;

            //fill labels in results with fuel amounts
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
            out List<int> lapsPerStint, List<int> newListOfLapsToPit)
        {
            //calculate pit stop strategy based on number of pit stops, pit options, max stint duration, tank capacity etc.

            //clear pit stop panel from previous calculation
            panelPitStopStrategy.Controls.Clear();

            //groupBox for the start of the race
            GroupBox groupBox_start = new GroupBox();
            groupBox_start.Text = "Stint 1 - Start of the race";
            groupBox_start.BackColor = Color.Gainsboro;
            groupBox_start.Font = groupBox_car_track.Font;
            groupBox_start.Location = new Point(16, 26);
            Size groupBox_size = new Size(385, 83);
            groupBox_start.Size = groupBox_size;
            panelPitStopStrategy.Controls.Add(groupBox_start);

            //tableLayout with results for groupBox above
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

            //labels that are inside tableLayout above
            Label label_fuel_for_start = new Label();
            label_fuel_for_start.Text = "Fuel for the start";
            label_fuel_for_start.Dock = DockStyle.Fill;
            label_fuel_for_start.TextAlign = ContentAlignment.MiddleCenter;
            label_fuel_for_start.Size = new Size(130, 17);

            Label label_fuel_start_result = new Label();
            label_fuel_start_result.Dock = DockStyle.Fill;
            label_fuel_start_result.TextAlign = ContentAlignment.MiddleCenter;

            tableLayoutPanel_start.Controls.Add(label_fuel_for_start, 0, 0);
            tableLayoutPanel_start.Controls.Add(label_fuel_start_result, 1, 0);

            number_of_pits = (int)numericUpDownPits.Value;

            //fuelPerStint, lapsPerStint, labelFuelStartResultText are for testing purposes right now
            //lists might be used in future for recalculating pit stop strategy
            fuelPerStint = new List<int>();
            lapsPerStint = new List<int>();
            labelFuelStartResultText = "";

            int tank_capacity;

            if (comboBox_car.Text == "CAR" || comboBox_track.Text == "TRACK")
            {
                //tank capacity is unknown when no car or track is selected but it is require for calculation
                //set big amount of tank_capacity so tank isnt a limiting factor
                tank_capacity = 999999;
            }
            else
            {
                tank_capacity = GetTankCapacity(comboBox_car.Text, comboBox_track.Text);
            }

            //if number of pit stops is 0 but pit option and/or max stint duration selected then
            //calculate number of pit stops needed (usefull if we dont know how many pits will fit into a longer race)
            //and recalculate based on new information about number of pit stops
            //'1L refuel' and 'tires only' pit options will get a logical in this case 1 pit stop
            if (comboBoxPitOptions.SelectedIndex != -1 && checkBox_max_stint.Checked == false &&
                numericUpDownPits.Value == 0)
            {
                if (comboBoxPitOptions.Text == "1L refuel" || comboBoxPitOptions.Text == "Tires only")
                {
                    number_of_pits = 1;
                    numericUpDownPits.Value = number_of_pits;
                    button_calculate_Click(button_calculate, EventArgs.Empty);
                    return;
                }
                else if (comboBox_car.Text != "CAR" && comboBox_track.Text != "TRACK")
                {
                    number_of_pits = fuel_for_race_round_up / tank_capacity;

                    //even if result above is 0 (tank capacity is more than enough for whole race)
                    //but pit option is selected then add one pit stop
                    if (number_of_pits == 0)
                    {
                        number_of_pits++;
                    }

                    numericUpDownPits.Value = number_of_pits;
                    button_calculate_Click(button_calculate, EventArgs.Empty);
                    return;
                }
            }
            else if (comboBoxPitOptions.SelectedIndex != -1 && checkBox_max_stint.Checked == true &&
                numericUpDownPits.Value == 0)
            {
                //checking if number of pits will be limited by max stint duration or fuel tank
                int number_of_pits_stint = race_duration_secs / (int.Parse(textBox_max_stint.Text) * 60);
                int number_of_pits_tank = number_of_laps / (int)Math.Ceiling(tank_capacity / fuel_per_lap);
                number_of_pits = Math.Max(number_of_pits_tank, number_of_pits_stint);

                numericUpDownPits.Value = number_of_pits;
                button_calculate_Click(button_calculate, EventArgs.Empty);
                return;
            }

            //if a sum of max stint duration with a set number of pit stops isnt enough for a race
            //recalculate number of pit stops
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
                //if number of pit stops is 0, then fill fuel label for the start of the race
                //in first groupBox with fuel for whole race

                label_fuel_start_result.Text = labelFuelRaceResult.Text;
                labelFuelStartResultText = label_fuel_start_result.Text;
                fuelPerStint.Add(fuel_for_race_round_up);

                //warning if tank capacity is lower than fuel require for the race
                if (fuel_for_race_round_up > tank_capacity)
                {
                    GroupBox groupBox_warning = new GroupBox();
                    groupBox_warning.Location = new Point(16, 124);
                    groupBox_warning.Size = new Size(385, 130);
                    groupBox_warning.Font = groupBox_car_track.Font;
                    groupBox_warning.ForeColor = Color.Red;
                    groupBox_warning.BackColor = groupBox_start.BackColor;
                    groupBox_warning.Text = "WARNING";
                    panelPitStopStrategy.Controls.Add(groupBox_warning);

                    Label label_warning = new Label();
                    label_warning.Text = "Fuel needed for this race (" + fuel_for_race_round_up.ToString() +
                        " L) is greater than tank capacity (" + tank_capacity.ToString() + " L). " +
                        "\n\nConsider adding a pit stop with refuel or " +
                        "fuel saving (" + (fuel_for_race_round_up - tank_capacity).ToString() + " L) during the race.";
                    label_warning.Location = new Point(16, 30);
                    label_warning.Size = new Size(350, 90);
                    label_warning.ForeColor = Color.Black;
                    groupBox_warning.Controls.Add(label_warning);
                }
            }
            else
            {
                //set some variables before going into a pit stop loop
                //'stint' is a number of laps between pit stops

                dynamic_numericUpDowns.Clear();

                double current_laps = 0.0;
                int stints_left = number_of_pits;

                //number_of_laps_remaining holds a number of laps left after each stint
                double laps_per_stint = (double)number_of_laps / (number_of_pits + 1);
                double number_of_laps_remaining = number_of_laps;

                //variable that will set location of grouBox below the previous one
                int y_groupBox = 120;

                //calculate fuel for the first stint and substract it from fuel for the whole race
                //fuel_remaining will now holds a fuel that still needs to be add in next pit stop(s)
                int fuel_first_stint = (int)Math.Ceiling((laps_per_stint * fuel_per_lap) + formation_lap_fuel);
                fuel_first_stint = Math.Min(fuel_first_stint, tank_capacity);

                double fuel_remaining = fuel_for_race_round_up - fuel_first_stint;
                int sum_of_laps_from_prev_iterations = 0;
                double rest_from_prev_stint = 0;

                if (newListOfLapsToPit.Count > 0)
                {
                    //this handles situation when user changes pit stop timing

                    fuel_first_stint = (int)Math.Ceiling((newListOfLapsToPit[0] * fuel_per_lap) + formation_lap_fuel);
                    fuel_remaining = fuel_for_race_round_up - fuel_first_stint;
                    rest_from_prev_stint = fuel_first_stint - (int)Math.Ceiling((newListOfLapsToPit[0] * fuel_per_lap) +
                        formation_lap_fuel);
                }

                labelFuelStartResultText = fuel_first_stint.ToString() + " L";
                fuelPerStint.Add(fuel_first_stint);

                //stint's limiting factor is either tank capacity or max stint duration
                if (checkBox_max_stint.Checked == true && textBox_max_stint.Text != "0")
                {
                    int laps_per_stint_tank = (int)(fuel_first_stint / fuel_per_lap);
                    int laps_per_stint_max = (int)((int.Parse(textBox_max_stint.Text)) * 60 / lap_time_secs);
                    laps_per_stint = Math.Min(laps_per_stint_max, laps_per_stint_tank);
                }

                //variables that will be used when when 1L strategy isnt optimal
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

                //stint counter starts from 2 because 1st stint is already calculated by default in first groupBox
                int stint = 2;
                for (int i = number_of_pits; i > 0; i--)
                {
                    //names for each groupBox changes with each stint
                    string name_for_groupbox = "groupBox_stint" + stint.ToString();
                    string name_for_refuel_label = "label_refuel_stint" + stint.ToString();
                    string name_for_refuel_result_label = "label_refuel_stint" + stint.ToString() + "_result";
                    string name_for_table = "tableLayoutPanel_stint" + stint.ToString();

                    if (comboBoxPitOptions.Text == "Tires only")
                    {
                        //'tires only' means no refuel so fuel for the start is the same as for whole race
                        //each loop iteration, groupBoxes are only fill with information about pit timing

                        label_fuel_start_result.Text = label_fuel_race_result.Text;

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

                        NumericUpDown numericUpDown_laps_temp = new NumericUpDown();
                        numericUpDown_laps_temp.Name = "numericUpDown_laps_stint" + stint.ToString();
                        numericUpDown_laps_temp.Dock = DockStyle.Fill;
                        numericUpDown_laps_temp.TextAlign = HorizontalAlignment.Center;
                        numericUpDown_laps_temp.Maximum = 999;
                        this.Controls.Add(numericUpDown_laps_temp);
                        dynamic_numericUpDowns.Add(numericUpDown_laps_temp);

                        Label label_refuel_temp = new Label();
                        label_refuel_temp.Dock = DockStyle.Fill;
                        label_refuel_temp.TextAlign = ContentAlignment.MiddleCenter;
                        label_refuel_temp.Name = name_for_refuel_label;

                        double current_part = Math.Min(number_of_laps_remaining, laps_per_stint);
                        current_laps += current_part;
                        label_refuel_temp.Text = "Tires change after";
                        number_of_laps_remaining -= laps_per_stint;
                        fuelPerStint.Add(0);
                        lapsPerStint.Add((int)Math.Ceiling(current_laps));

                        if (newListOfLapsToPit.Count > 0)
                        {
                            //this handles situation when user changes pit stop timing

                            if (stints_left > 1)
                            {
                                current_laps = newListOfLapsToPit[stint - 1] - newListOfLapsToPit[stint - 2];
                            }
                            else
                            {
                                current_laps = newListOfLapsToPit[stint - 2] - sum_of_laps_from_prev_iterations;
                            }

                            sum_of_laps_from_prev_iterations += (int)current_laps;
                            numericUpDown_laps_temp.Value = newListOfLapsToPit[stint - 2];
                        }
                        else
                        {
                            numericUpDown_laps_temp.Value = ((int)Math.Ceiling(current_laps));
                        }

                        table_temp.Controls.Add(label_refuel_temp, 0, 0);
                        table_temp.Controls.Add(numericUpDown_laps_temp, 1, 0);
                        table_temp.Size = tableLayoutPanel_start.Size;

                        groupBox_temp.Controls.Add(table_temp);
                        stint++;
                        stints_left--;
                        y_groupBox += 100;

                        //on last loop iteration when all stint groupBoxes are done,
                        //show a warning if tank capacity is lower than fuel for whole race
                        if (fuel_for_race_round_up > tank_capacity && i == 1)
                        {
                            GroupBox groupBox_warning = new GroupBox();
                            groupBox_warning.Location = new Point(16, y_groupBox);
                            groupBox_warning.Size = new Size(385, 130);
                            groupBox_warning.Font = groupBox_car_track.Font;
                            groupBox_warning.ForeColor = Color.Red;
                            groupBox_warning.BackColor = groupBox_start.BackColor;
                            groupBox_warning.Text = "WARNING";
                            panelPitStopStrategy.Controls.Add(groupBox_warning);

                            Label label_warning = new Label();
                            label_warning.Text = "Fuel needed for this race (" + fuel_for_race_round_up.ToString() +
                                " L) is greater than tank capacity (" + tank_capacity.ToString() + " L). " +
                                "\n\nConsider changing a pit stop option with refuel or " +
                                "fuel saving (" + (fuel_for_race_round_up - tank_capacity).ToString() + " L) during the race.";
                            label_warning.Location = new Point(16, 30);
                            label_warning.Size = new Size(350, 90);
                            label_warning.ForeColor = Color.Black;
                            groupBox_warning.Controls.Add(label_warning);
                        }
                    }
                    else if (comboBoxPitOptions.Text == "1L refuel")
                    {
                        //'1L refuel' pit options will attempt to set only 1L of refuel in each pit stop
                        //'is_strat_ok' is used to switch to adjusted strategy if 1L per pit stop isnt enough of a refuel

                        bool is_strat_ok = true;

                        //warning that strategy needs to be adjusted
                        if (fuel_for_race_round_up > (tank_capacity + number_of_pits))
                        {
                            groupBox_start.Size = new Size(385, 123);
                            Label label_tank = new Label();
                            label_tank.Text = "Exceeded fuel tank capacity of " + tank_capacity.ToString() + " L for 1L strategy. " +
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
                                //standard '1L refuel' strategy, 1L refuel each pit stop
                                //number of pit stops (liters) are substrated from a fuel for a whole race

                                label_fuel_start_result.Text = (fuel_for_race_round_up - number_of_pits).ToString() + " L";

                                GroupBox groupBox_temp = new GroupBox();
                                groupBox_temp.Name = name_for_groupbox;
                                groupBox_temp.Location = new Point(16, y_groupBox);
                                groupBox_temp.Size = new Size(385, 103);
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
                                label_refuel_temp.Text = "Refuel for Stint " + stint.ToString();

                                NumericUpDown numericUpDown_laps_temp = new NumericUpDown();
                                numericUpDown_laps_temp.Name = "numericUpDown_laps_stint" + stint.ToString();
                                numericUpDown_laps_temp.Dock = DockStyle.Fill;
                                numericUpDown_laps_temp.TextAlign = HorizontalAlignment.Center;
                                numericUpDown_laps_temp.Maximum = 999;
                                this.Controls.Add(numericUpDown_laps_temp);
                                dynamic_numericUpDowns.Add(numericUpDown_laps_temp);

                                double current_part = Math.Min(number_of_laps_remaining, laps_per_stint);
                                current_laps += current_part;

                                if (newListOfLapsToPit.Count > 0)
                                {
                                    //this handles situation when user changes pit stop timing

                                    if (stints_left > 1)
                                    {
                                        current_laps = newListOfLapsToPit[stint - 1] - newListOfLapsToPit[stint - 2];
                                    }
                                    else
                                    {
                                        current_laps = newListOfLapsToPit[stint - 2] - sum_of_laps_from_prev_iterations;
                                    }

                                    sum_of_laps_from_prev_iterations += (int)current_laps;
                                    numericUpDown_laps_temp.Value = newListOfLapsToPit[stint - 2];
                                }
                                else
                                {
                                    numericUpDown_laps_temp.Value = ((int)Math.Ceiling(current_laps));
                                }

                                number_of_laps_remaining -= laps_per_stint;
                                lapsPerStint.Add((int)Math.Ceiling(current_laps));

                                Label label_refuel_result_temp = new Label() { Text = "1 L" };
                                label_refuel_result_temp.Name = name_for_refuel_result_label;
                                label_refuel_result_temp.Dock = DockStyle.Fill;
                                label_refuel_result_temp.TextAlign = ContentAlignment.MiddleCenter;
                                fuelPerStint.Add(1);

                                Label label_laps_temp = new Label();
                                label_laps_temp.Name = "label_laps_stint" + stint.ToString();
                                label_laps_temp.Dock = DockStyle.Fill;
                                label_laps_temp.TextAlign = ContentAlignment.MiddleCenter;
                                label_laps_temp.Text = "Pit after lap";

                                table_temp.Controls.Add(label_refuel_temp, 0, 1);
                                table_temp.Controls.Add(label_refuel_result_temp, 1, 1);
                                table_temp.Controls.Add(label_laps_temp, 0, 0);
                                table_temp.Controls.Add(numericUpDown_laps_temp, 1, 0);
                                table_temp.Size = new Size(350, 65);

                                groupBox_temp.Controls.Add(table_temp);
                                stint++;
                                stints_left--;
                                y_groupBox += 130;
                                break;

                            case false:
                                //adjusted '1L refuel' strategy where more fuel is needed than with a standard approach
                                //this version will attempt to have a pit stop with 1L
                                //example: 1st stint = 120L, 2nd stint = 56L, 3rd stint = 1L
                                //laps from each stint are being substracted from number_of_laps_remaining

                                label_fuel_start_result.Text = tank_capacity.ToString() + " L";

                                GroupBox groupBox_temp2 = new GroupBox();
                                groupBox_temp2.Name = name_for_groupbox;
                                groupBox_temp2.Location = new Point(16, y_groupBox);
                                groupBox_temp2.Size = new Size(385, 103);
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

                                Label label_laps_temp2 = new Label();
                                label_laps_temp2.Name = name_for_refuel_label;
                                label_laps_temp2.Dock = DockStyle.Fill;
                                label_laps_temp2.TextAlign = ContentAlignment.MiddleCenter;
                                label_laps_temp2.Text = "Pit after lap";

                                Label label_refuel_temp2 = new Label();
                                label_refuel_temp2.Name = name_for_refuel_label;
                                label_refuel_temp2.Dock = DockStyle.Fill;
                                label_refuel_temp2.TextAlign = ContentAlignment.MiddleCenter;
                                label_refuel_temp2.Text = "Refuel for Stint " + stint.ToString();

                                if (stint == 2)
                                {
                                    fuelPerStint.Clear();
                                    fuelPerStint.Add(tank_capacity);
                                }

                                int fuel_for_this_stint = fuel_1L_adjusted[stint - 1];
                                stints_left--;
                                fuelPerStint.Add(fuel_for_this_stint);
                                double more_time_in_pits = 0.0;
                                if (fuel_for_this_stint > 1)
                                {
                                    more_time_in_pits += (fuel_for_this_stint - 1) * ONE_L_MORE;
                                }

                                NumericUpDown numericUpDown_laps_temp2 = new NumericUpDown();
                                numericUpDown_laps_temp2.Name = "numericUpDown_laps_stint" + stint.ToString();
                                numericUpDown_laps_temp2.Dock = DockStyle.Fill;
                                numericUpDown_laps_temp2.TextAlign = HorizontalAlignment.Center;
                                numericUpDown_laps_temp2.Maximum = 999;
                                this.Controls.Add(numericUpDown_laps_temp2);
                                dynamic_numericUpDowns.Add(numericUpDown_laps_temp2);

                                double current_part_laps = Math.Min(number_of_laps_remaining, laps_per_stint);
                                current_laps += current_part_laps;
                                label_refuel_temp2.Text = "Refuel after ";
                                number_of_laps_remaining -= laps_per_stint;
                                lapsPerStint.Add((int)Math.Ceiling(current_laps));

                                if (newListOfLapsToPit.Count > 0)
                                {
                                    //this handles situation when user changes pit stop timing

                                    if (stints_left > 1)
                                    {
                                        current_laps = newListOfLapsToPit[stint - 1] - newListOfLapsToPit[stint - 2];
                                    }
                                    else
                                    {
                                        current_laps = newListOfLapsToPit[stint - 2] - sum_of_laps_from_prev_iterations;
                                    }

                                    sum_of_laps_from_prev_iterations += (int)current_laps;
                                    numericUpDown_laps_temp2.Value = newListOfLapsToPit[stint - 2];

                                    if (fuel_for_this_stint == tank_capacity && fuelPerStint[stint - 2] == tank_capacity)
                                    {
                                        numericUpDown_laps_temp2.Value = (int)(tank_capacity / fuel_per_lap) * (stint - 1);
                                        numericUpDown_laps_temp2.Enabled = false;
                                    }
                                }
                                else
                                {
                                    numericUpDown_laps_temp2.Value = ((int)Math.Ceiling(current_laps));

                                    if (fuel_for_this_stint == tank_capacity || fuelPerStint[stint - 2] == tank_capacity)
                                    {
                                        numericUpDown_laps_temp2.Value = (int)(tank_capacity / fuel_per_lap) * (stint - 1);
                                        numericUpDown_laps_temp2.Enabled = false;
                                    }
                                }

                                if (fuelPerStint[stint - 2] == tank_capacity)
                                {
                                    numericUpDown_laps_temp2.Enabled = false;
                                }

                                Label label_refuel_result_temp2 = new Label();
                                label_refuel_result_temp2.Name = name_for_refuel_result_label;
                                label_refuel_result_temp2.Text = fuel_for_this_stint.ToString() + " L";
                                label_refuel_result_temp2.Dock = DockStyle.Fill;
                                label_refuel_result_temp2.TextAlign = ContentAlignment.MiddleCenter;

                                table_temp2.Controls.Add(label_refuel_temp2, 0, 1);
                                table_temp2.Controls.Add(label_refuel_result_temp2, 1, 1);
                                table_temp2.Controls.Add(label_laps_temp2, 0, 0);
                                table_temp2.Controls.Add(numericUpDown_laps_temp2, 1, 0);
                                table_temp2.Size = new Size(350, 65);

                                groupBox_temp2.Controls.Add(table_temp2);
                                y_groupBox += 100;
                                stint++;

                                //recalculate everything on last loop is needed because time lost in pits
                                //will change with this adjusted strategy
                                if (i == 1 && is_recalculate_needed == true)
                                {
                                    time_lost_in_pits += more_time_in_pits;
                                    CalculateRaceDuration(textBox_race_h, textBox_race_min, textBox_lap_time_min, textBox_lap_time_sec,
                                        label_overall_result, label_laps_result, label_lap_time_result2);
                                    CalculateLapTimePlusMinus(label_plus1_lap_time_result, label_minus1_lap_time_result);
                                    CalculateFuel(textBox_fuel_per_lap, listBox_formation, label_fuel_race_result,
                                        label_plus1_fuel_result, label_minus1_fuel_result);
                                    is_recalculate_needed = false;    //change to false so it wont be endless loop
                                    CalculatePitStops(panel_pit_stop_strategy, label_fuel_race_result, numericUpDown_pits, comboBox_pit_options,
                                        out string labelFuelStartResultTextForTesting, out List<int> fuelPerStintForTesting,
                                        out List<int> lapsPitStintForTesting, new_list_of_laps_to_pit);
                                    is_recalculate_needed = true;     //change back to true for next calculations
                                    SaveData();
                                }
                                break;
                        }

                        //warning that for this number of pit stops with full tanks, its still not enough fuel
                        //for the whole race
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
                        //'fixed refuel only', 'refuel only' and 'refuel + tires' share the same pit strategy formula
                        //fuel from each stint is being substracted from fuel_remaining
                        //laps from each stint are being substracted from number_of_laps_remaining

                        label_fuel_start_result.Text = fuel_first_stint.ToString() + " L";

                        GroupBox groupBox_temp = new GroupBox();
                        groupBox_temp.Name = name_for_groupbox;
                        groupBox_temp.Location = new Point(16, y_groupBox);
                        groupBox_temp.Size = new Size(385, 103);
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

                        NumericUpDown numericUpDown_laps_temp = new NumericUpDown();
                        numericUpDown_laps_temp.Name = "numericUpDown_laps_stint" + stint.ToString();
                        numericUpDown_laps_temp.Dock = DockStyle.Fill;
                        numericUpDown_laps_temp.TextAlign = HorizontalAlignment.Center;
                        numericUpDown_laps_temp.Maximum = 999;
                        this.Controls.Add(numericUpDown_laps_temp);
                        dynamic_numericUpDowns.Add(numericUpDown_laps_temp);

                        Label label_refuel_temp = new Label();
                        label_refuel_temp.Name = name_for_refuel_label;
                        label_refuel_temp.Dock = DockStyle.Fill;
                        label_refuel_temp.TextAlign = ContentAlignment.MiddleCenter;
                        label_refuel_temp.Text = "Refuel for Stint " + stint.ToString();

                        double current_part_laps = Math.Min(number_of_laps_remaining, laps_per_stint);
                        current_laps += current_part_laps;

                        if (newListOfLapsToPit.Count > 0)
                        {
                            //this handles situation when user changes pit stop timing

                            if (stints_left > 1)
                            {
                                current_laps = newListOfLapsToPit[stint - 1] - newListOfLapsToPit[stint - 2];
                            }
                            else
                            {
                                current_laps = newListOfLapsToPit[stint - 2] - sum_of_laps_from_prev_iterations;
                            }

                            sum_of_laps_from_prev_iterations += (int)current_laps;
                            numericUpDown_laps_temp.Value = newListOfLapsToPit[stint - 2];
                        }
                        else
                        {
                            numericUpDown_laps_temp.Value = ((int)Math.Ceiling(current_laps));
                        }

                        number_of_laps_remaining -= laps_per_stint;
                        lapsPerStint.Add((int)Math.Ceiling(current_laps));

                        double current_part_fuel = Math.Min(fuel_remaining,
                            Math.Min((fuel_remaining / stints_left), tank_capacity));

                        if (newListOfLapsToPit.Count > 0)
                        {
                            //this handles situation when user changes pit stop timing

                            current_part_fuel = Math.Min(fuel_remaining,
                                Math.Min((current_laps * fuel_per_lap - rest_from_prev_stint), tank_capacity));
                            rest_from_prev_stint = (int)Math.Ceiling(current_part_fuel) - current_part_fuel;
                        }

                        int fuel_for_this_stint = (int)Math.Ceiling(current_part_fuel);
                        fuel_remaining -= fuel_for_this_stint;
                        stints_left--;
                        fuelPerStint.Add(fuel_for_this_stint);

                        Label label_laps_temp = new Label();
                        label_laps_temp.Name = "label_laps_stint" + stint.ToString();
                        label_laps_temp.Dock = DockStyle.Fill;
                        label_laps_temp.TextAlign = ContentAlignment.MiddleCenter;
                        label_laps_temp.Text = "Pit after lap";

                        Label label_refuel_result_temp = new Label();
                        label_refuel_result_temp.Name = name_for_refuel_result_label;
                        label_refuel_result_temp.Text = fuel_for_this_stint.ToString() + " L";
                        label_refuel_result_temp.Dock = DockStyle.Fill;
                        label_refuel_result_temp.TextAlign = ContentAlignment.MiddleCenter;

                        table_temp.Controls.Add(label_refuel_temp, 0, 1);
                        table_temp.Controls.Add(label_refuel_result_temp, 1, 1);
                        table_temp.Controls.Add(label_laps_temp, 0, 0);
                        table_temp.Controls.Add(numericUpDown_laps_temp, 1, 0);
                        table_temp.Size = new Size(350, 65);

                        groupBox_temp.Controls.Add(table_temp);
                        y_groupBox += 112;
                        stint++;

                        //warning that for this number of pit stops with full tanks, its still not enough fuel
                        //for the whole race
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
                                "\n\nConsider increasing number of pit stops or " +
                                "fuel saving (" + (fuel_for_race_round_up - full_tank_sum).ToString() + " L) during the race.";
                            label_warning.Location = new Point(16, 30);
                            label_warning.Size = new Size(350, 90);
                            label_warning.ForeColor = Color.Black;
                            groupBox_warning.Controls.Add(label_warning);
                        }
                    }
                }
            }
            PitLimits();
        }

        private void PitLimits()
        {
            if (number_of_pits == 0)
            {
                return;
            }

            //set a minimum and maximum for NumericUpDown

            int max_stint_limit = 99999;
            List<int> tank_capacity_limit = new List<int>();

            if (comboBox_car.Text != "CAR" && comboBox_track.Text != "TRACK")
            {
                int tank_capacity = GetTankCapacity(comboBox_car.Text, comboBox_track.Text);

                tank_capacity_limit.Add((int)((tank_capacity - formation_lap_fuel) / fuel_per_lap));

                int sum_of_prev_stints = 0;

                for (int i = 1; i < dynamic_numericUpDowns.Count; i++)
                {
                    tank_capacity_limit.Add(Math.Min(
                        (int)(tank_capacity / fuel_per_lap),
                        (int)(tank_capacity - (fuel_per_lap * (sum_of_prev_stints - (int)dynamic_numericUpDowns[i].Value))
                        )));
                }
            }
            else
            {
                for (int i = 0; i < dynamic_numericUpDowns.Count; i++)
                {
                    tank_capacity_limit.Add(99999);
                }
            }

            if (checkBox_max_stint.Checked && textBox_max_stint.Text != "0")
            {
                int.TryParse(textBox_max_stint.Text, out int max_stint);
                max_stint_limit = (int)((max_stint * 60) / lap_time_secs);
            }

            //first pit stop cant be earlier than after lap 1 (min) and later than next pit stop or
            //2nd to last lap (max)

            if (dynamic_numericUpDowns.Count > 1)
            {
                if (Math.Min(tank_capacity_limit[0], max_stint_limit) <
                    dynamic_numericUpDowns[1].Value - dynamic_numericUpDowns[0].Value)
                {
                    dynamic_numericUpDowns[0].Minimum = dynamic_numericUpDowns[1].Value -
                        Math.Min(tank_capacity_limit[0], max_stint_limit);
                }
                else
                {
                    dynamic_numericUpDowns[0].Minimum = 1;
                }
            }
            else
            {
                dynamic_numericUpDowns[0].Minimum = 1;
            }

            if (dynamic_numericUpDowns.Count == 1)
            {
                dynamic_numericUpDowns[0].Maximum = Math.Min(number_of_laps - 1,
                    Math.Min(tank_capacity_limit[0], max_stint_limit));
            }
            else
            {
                dynamic_numericUpDowns[0].Maximum = Math.Min(dynamic_numericUpDowns[1].Value - 1,
                    Math.Min(tank_capacity_limit[0], max_stint_limit));
            }

            //another pit stop thresholds are previous pit timing and next or 2nd to last lap
            if (new_list_of_laps_to_pit.Count > 1)
            {
                for (int i = 1; i < new_list_of_laps_to_pit.Count; i++)
                {
                    if (i < new_list_of_laps_to_pit.Count - 1)
                    {
                        if (Math.Min(tank_capacity_limit[i], max_stint_limit) >
                            dynamic_numericUpDowns[i + 1].Value - dynamic_numericUpDowns[i].Value)
                        {
                            dynamic_numericUpDowns[i].Minimum = dynamic_numericUpDowns[i - 1].Value + 1;
                        }
                        else
                        {
                            dynamic_numericUpDowns[i].Minimum = dynamic_numericUpDowns[i + 1].Value -
                                Math.Min(tank_capacity_limit[0], max_stint_limit);
                        }

                        if (Math.Min(tank_capacity_limit[0], max_stint_limit) >
                            dynamic_numericUpDowns[i].Value - dynamic_numericUpDowns[i - 1].Value)
                        {
                            dynamic_numericUpDowns[i].Maximum = dynamic_numericUpDowns[i + 1].Value - 1;
                        }
                        else
                        {
                            dynamic_numericUpDowns[i].Maximum = dynamic_numericUpDowns[i - 1].Value +
                                Math.Min(tank_capacity_limit[0], max_stint_limit);
                        }
                    }
                    else
                    {
                        dynamic_numericUpDowns[i].Minimum = Math.Max(number_of_laps - Math.Min(tank_capacity_limit[0], max_stint_limit),
                            dynamic_numericUpDowns[i - 1].Value + 1);

                        if (Math.Min(tank_capacity_limit[0], max_stint_limit) >
                            dynamic_numericUpDowns[i].Value - dynamic_numericUpDowns[i - 1].Value)
                        {
                            dynamic_numericUpDowns[i].Maximum = number_of_laps - 1;
                        }
                        else
                        {
                            dynamic_numericUpDowns[i].Maximum = dynamic_numericUpDowns[i - 1].Value +
                                Math.Min(tank_capacity_limit[0], max_stint_limit);
                        }
                    }
                }
            }

            foreach (NumericUpDown numericUpDown in dynamic_numericUpDowns)
            {
                numericUpDown.ValueChanged += new EventHandler(NumericUpDown_pit_strat_changes);
            }
        }

        private void LoadData()
        {
            //load data into comboBoxes if track and optionally car are selected

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
                textBox_lap_time_sec.Text = lap_time_secs.ToString("0.000").Replace(",", ".");

                if (comboBox_car.Text != "CAR")
                {
                    CarTrackFuel car_fuel = track.car_track_fuel.Find(car => car.car_name == selected_car);
                    textBox_fuel_per_lap.Text = car_fuel.fuel_per_lap;
                }
            }
        }

        private void SaveData()
        {
            //saves data to 'FCalcACC_data.json' (fuel per lap, lap times)

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
            //actions taken on applications start

            LoadCarTrackObjects();
            LoadCarClasses(comboBox_class);
            LoadTracks(comboBox_track);
            LoadPitOptions(comboBox_pit_options, PIT_OPTIONS);

            //default formation lap is 'Full'
            listBox_formation.SelectedIndex = 0;
        }

        private void button_calculate_Click(object sender, EventArgs e)
        {
            if (comboBox_pit_options.Text == "Refuel only")
            {
                //for 'refuel only' pit option, time lost in pits is unknown because it relies on how much fuel
                //needs to be add during pit stop based on fuel for whole race and number of pit stops
                //fuel for whole race is unknown until CalculateFuel
                //in this case first iteration gets a default pit duration
                //next iteration gets more accurate information thanks to RefuelTimeLost
                //there are 3 iteration to make sure that end result is as close as possible to ideal one

                time_lost_in_pits = 57 * number_of_pits;
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
                //standard procedure when 'only refuel' ISNT selected

                CalculateTimeLostInPits((int)numericUpDown_pits.Value, comboBox_pit_options, comboBox_track.Text);
                CalculateRaceDuration(textBox_race_h, textBox_race_min, textBox_lap_time_min, textBox_lap_time_sec,
                    label_overall_result, label_laps_result, label_lap_time_result2);
                CalculateLapTimePlusMinus(label_plus1_lap_time_result, label_minus1_lap_time_result);
                CalculateFuel(textBox_fuel_per_lap, listBox_formation, label_fuel_race_result,
                    label_plus1_fuel_result, label_minus1_fuel_result);
            }
            CalculatePitStops(panel_pit_stop_strategy, label_fuel_race_result, numericUpDown_pits, comboBox_pit_options,
                out string labelFuelStartResultTextForTesting, out List<int> fuelPerStint,
                out List<int> lapsPerStint, new_list_of_laps_to_pit);
            SaveData();
        }

        private void comboBox_car_SelectedIndexChanged(object sender, EventArgs e)
        {
            //load new data when selected car changes

            LoadData();
        }

        private void comboBox_track_SelectedIndexChanged(object sender, EventArgs e)
        {
            //load new data when selected track changes

            LoadData();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //menu->exit

            Application.Exit();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //help button, showing new window

            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void checkBox_max_stint_Click(object sender, EventArgs e)
        {
            //checkBox max stint enables and disables textBox

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
            //github button, opnes github website of this project in a browser

            string github_url = "https://github.com/LabuzPawel/FCalcACC";
            Process.Start(new ProcessStartInfo
            {
                FileName = github_url,
                UseShellExecute = true
            });
        }

        private void resetDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //menu->reset data, option to reset FCalcACC_data.json

            DialogResult result = MessageBox.Show("Would you like to reset the 'FCalcACC_data.json'?",
                        "Reset data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                File.Delete("FCalcACC_data.json");
                LoadCarTrackObjects();
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            //dialog pop up after Form is shown informing that the file was created

            if (File.Exists("FCalcACC_data.json") == false)
            {
                SaveData();
                MessageBox.Show("'FCalcACC_data.json' created in application directory");
            }
        }

        private void NumericUpDown_pit_strat_changes(object sender, EventArgs e)
        {
            //get updated list of laps when user wants to pit
            new_list_of_laps_to_pit.Clear();

            foreach (NumericUpDown numericUpDown in dynamic_numericUpDowns)
            {
                int numeric_value = Convert.ToInt32((numericUpDown).Value);
                new_list_of_laps_to_pit.Add(numeric_value);
            }

            CalculatePitStops(panel_pit_stop_strategy, label_fuel_race_result, numericUpDown_pits, comboBox_pit_options,
                out string labelFuelStartResultTextForTesting, out List<int> fuelPerStint,
                out List<int> lapsPerStint, new_list_of_laps_to_pit);

            new_list_of_laps_to_pit.Clear();
        }

        private void comboBox_pit_options_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if 'no pit stops' selected -> reset pit stops panel

            if (comboBox_pit_options.SelectedIndex == 5)
            {
                comboBox_pit_options.SelectedIndex = -1;
                numericUpDown_pits.Value = 0;
                textBox_max_stint.Text = "0";
                if (checkBox_max_stint.Checked)
                {
                    checkBox_max_stint.Checked = false;
                    textBox_max_stint.Enabled = false;
                }
            }
        }

        private void resetAllDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.Delete("FCalcACC_data.json");
            LoadCarTrackObjects();
        }

        private void resetCurrentCartrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetSpecficCarTrack(comboBox_car, comboBox_track);
            LoadData();
            CalculateRaceDuration(textBox_race_h, textBox_race_min, textBox_lap_time_min, textBox_lap_time_sec,
                    label_overall_result, label_laps_result, label_lap_time_result2);
            SaveData();
        }
    }
}