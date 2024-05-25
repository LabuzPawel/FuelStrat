using FuelStrat.RecentSessions;
using FuelStrat.SharedMemory.Types.Enums;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Reflection;

namespace FuelStrat
{
    public partial class FuelStrat : Form
    {
        public class Car
        {
            public string CarName { get; set; }
            public string ClassName { get; set; }
        }

        public class Track
        {
            public string TrackName { get; set; }
            public string TrackLapTime { get; set; }
            public string TrackPitDuration { get; set; }
            public List<CarTrackFuel> CarTrackFuel { get; set; }
        }

        public class CarTrackFuel
        {
            public string CarName { get; set; }
            public string FuelPerLap { get; set; }
            public string TankCapacity { get; set; }
        }

        public class FixedSizeList<T>(int max_size)
        {
            private readonly int max_size = max_size;
            private readonly List<T> list = new(max_size);

            public void Add(T item)
            {
                list.Add(item);
                if (list.Count > max_size)
                {
                    list.RemoveAt(0);
                }
            }

            public T this[int index]
            {
                get => list[index];
                set => list[index] = value;
            }

            public IEnumerator<T> GetEnumerator()
            {
                return list.GetEnumerator();
            }
        }

        public class ListBoxMultiline : ListBox
        {
            // https://stackoverflow.com/a/60589434

            private readonly TextFormatFlags flags = TextFormatFlags.WordBreak |
                                    TextFormatFlags.PreserveGraphicsClipping |
                                    TextFormatFlags.LeftAndRightPadding |
                                    TextFormatFlags.ExpandTabs |
                                    TextFormatFlags.VerticalCenter;

            private static readonly string[] separator = ["nextLine"];

            public ListBoxMultiline()
            { this.DrawMode = DrawMode.OwnerDrawVariable; }

            private Size GetItemSize(Graphics g, string itemText)
            {
                var size = TextRenderer.MeasureText(g, itemText, Font, ClientSize, flags);
                size.Height = Math.Max(Math.Min(size.Height, 247), Font.Height + 8) + 8;
                return size;
            }

            protected override void OnMeasureItem(MeasureItemEventArgs e)
            {
                if (Items.Count == 0)
                    return;

                string itemText = GetItemText(Items[e.Index]);

                Size itemSize = GetItemSize(e.Graphics, itemText);

                e.ItemWidth = itemSize.Width;
                e.ItemHeight = itemSize.Height;

                base.OnMeasureItem(e);
            }

            protected override void OnDrawItem(DrawItemEventArgs e)
            {
                if (Items.Count == 0 || e.Index < 0)
                    return;

                string itemText = GetItemText(Items[e.Index]);

                string[] parts = itemText.Split(separator, StringSplitOptions.None);

                int offsetY = -1;

                Rectangle outerBounds = new(e.Bounds.X, e.Bounds.Y + offsetY, e.Bounds.Width, e.Bounds.Height - offsetY);
                Rectangle firstLineBounds = new(e.Bounds.X + 1, e.Bounds.Y + offsetY, e.Bounds.Width - 2, (e.Bounds.Height / 2) - offsetY);
                Rectangle secondLineBounds = new(e.Bounds.X + 1, e.Bounds.Y + (e.Bounds.Height / 2) - 3, e.Bounds.Width - 2, (e.Bounds.Height / 2) + 3);

                e.Graphics.DrawRectangle(Pens.Black, outerBounds);

                firstLineBounds.Inflate(-1, -1);
                secondLineBounds.Inflate(2, 2);

                TextRenderer.DrawText(e.Graphics, parts[0], Font, firstLineBounds, ForeColor, flags);

                if (parts.Length > 1)
                {
                    TextRenderer.DrawText(e.Graphics, parts[1], Font, secondLineBounds, ForeColor, flags);
                }

                // Handle selection
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    using (var selectionBrush = new SolidBrush(SystemColors.Highlight))
                    {
                        e.Graphics.FillRectangle(selectionBrush, e.Bounds);

                        Rectangle selectedTextBounds = e.Bounds;
                        selectedTextBounds.Y += (e.Bounds.Height - TextRenderer.MeasureText(e.Graphics, itemText, Font).Height) / 2;

                        TextRenderer.DrawText(e.Graphics, parts[0], Font, firstLineBounds, Color.White, flags);

                        if (parts.Length > 1)
                        {
                            TextRenderer.DrawText(e.Graphics, parts[1], Font, secondLineBounds, Color.White, flags);
                        }
                    }
                }
                else
                {
                    using (var selectionBrush = new SolidBrush(SystemColors.Menu))
                    {
                        e.Graphics.FillRectangle(selectionBrush, e.Bounds);

                        Rectangle selectedTextBounds = e.Bounds;
                        selectedTextBounds.Y += (e.Bounds.Height - TextRenderer.MeasureText(e.Graphics, itemText, Font).Height) / 2;

                        TextRenderer.DrawText(e.Graphics, parts[0], Font, firstLineBounds, Color.Black, flags);

                        if (parts.Length > 1)
                        {
                            TextRenderer.DrawText(e.Graphics, parts[1], Font, secondLineBounds, Color.Black, flags);
                        }
                    }
                }

                base.OnDrawItem(e);
            }
        }

        public class LastStateOfTheApp
        {
            public List<StintData> Stints { get; set; }
            public SavedStrategy Controls { get; set; }

            public LastStateOfTheApp(SavedStrategy controls, List<StintData> stints)
            {
                Stints = stints;
                Controls = controls;
            }
        }

        public ListBoxMultiline listBoxMultiline_recent_sessions = new();

        public struct Lap
        {
            public int completed_laps;
            public int lap_time;
            public double fuel;
            public string session_type;
            public string track_name;
            public string car_name;
            public int fuel_at_the_start;
            public bool invalid;
            public bool used;
            public bool outlier;
        }

        public struct Stint
        {
            public int count_number;
            public string track_name;
            public string car_name;
            public int stint_lenght;
            public double average_fuel_per_lap;
            public int average_lap_time;
            public DateTime date_time;
            public string session_type;
            public int fuel_at_the_start;
        }

        public class StintData
        {
            public List<Lap> ListOfLaps;
            public Stint Stint;

            public StintData(StintData stintData)
            {
                ListOfLaps = new List<Lap>(stintData.ListOfLaps);
                Stint = stintData.Stint;
            }

            public StintData()
            { }
        }

        public FixedSizeList<StintData> recent_stints = new(10);

        public struct SavedStrategy
        {
            public string saved_name;
            public int saved_car_class_index;
            public int saved_car_index;
            public int saved_track_index;
            public string saved_race_h;
            public string saved_race_min;
            public string saved_lap_min;
            public string saved_lap_secs;
            public string saved_fuel_per_lap;
            public int saved_formation_index;
            public int saved_number_of_pits;
            public int saved_pit_stop_option_index;
            public bool saved_checkbox_max_stint;
            public string saved_max_stint;
            public bool saved_max_stint_enabled;
        }

        public List<Track> all_tracks;
        public List<Car> all_cars;
        public List<string> car_classes;
        public List<int> fuelPerStint;
        public List<int> lapsPerStint;
        public List<NumericUpDown> dynamic_numericUpDowns = [];
        public List<int> new_list_of_laps_to_pit = [];
        public List<int> new_list_of_refuel = [];
        public List<Label> dynamic_labels = [];

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
        private Debouncer recalculate_debouncer = new(50);
        private Debouncer import_race_debouncer = new(300);
        private Debouncer import_stint_debouncer = new(300);
        private Debouncer auto_debouncer = new(300);
        private bool is_strat_ok = true;
        private bool is_recalculate_needed = true;
        private System.Threading.Timer telemetryTimer;
        private UpdateFromTelemetry updateFromTelemetry;
        private System.Threading.Timer telemetry_update_timer;
        public UpdateFromTelemetry.Sim_data sim_data = new();

        private string session_saved = "";
        private int previous_lap = -1;
        private List<Lap> laps_data = [];
        private bool invalid_lap = false;

        private readonly string documents_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "FuelStrat");

        private readonly string DECIMAL_SEPARATOR = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        private readonly double ONE_L_PIT_TIME = 3.6;
        private readonly double ONE_L_MORE = 0.2;
        public readonly int DEFAULT_TIME_IN_PITS = 57;

        public List<string> PIT_OPTIONS =
        [
            "Refuel + tires",
            "Fixed refuel only",
            "Tires only",
            "Refuel only",
            "1L refuel",
            "No pit stops"
        ];

        private class Debouncer
        {
            // https://stackoverflow.com/a/47933557

            private List<CancellationTokenSource> StepperCancelTokens = [];
            private int MillisecondsToWait;
            private readonly object _lockThis = new object(); // Use a locking object to prevent the debouncer
                                                              // to trigger again while the func is still running

            public Debouncer(int millisecondsToWait = 300)
            {
                this.MillisecondsToWait = millisecondsToWait;
            }

            public void Debouce(Action func)
            {
                CancelAllStepperTokens(); // Cancel all api requests;
                var newTokenSrc = new CancellationTokenSource();
                lock (_lockThis)
                {
                    StepperCancelTokens.Add(newTokenSrc);
                }
                Task.Delay(MillisecondsToWait, newTokenSrc.Token).ContinueWith(task => // Create new request
                {
                    if (!newTokenSrc.IsCancellationRequested) // if it hasn't been cancelled
                    {
                        CancelAllStepperTokens(); // Cancel any that remain (there shouldn't be any)
                        StepperCancelTokens = new List<CancellationTokenSource>(); // set to new list
                        lock (_lockThis)
                        {
                            func(); // run
                        }
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }

            private void CancelAllStepperTokens()
            {
                foreach (var token in StepperCancelTokens)
                {
                    if (!token.IsCancellationRequested)
                    {
                        token.Cancel();
                    }
                }
            }
        }

        public void LoadCarTrackObjects()
        {
            // Load Cars from embedded json's
            string cars_resourse_name = "FuelStrat.json_resources.CARS.json";
            string tracks_resourse_name = "FuelStrat.json_resources.TRACKS.json";
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(cars_resourse_name))
            using (StreamReader reader = new(stream))
            {
                string cars_embedded = reader.ReadToEnd();
                all_cars = JsonConvert.DeserializeObject<List<Car>>(cars_embedded);
            }

            string data_path = Path.Combine(documents_path, "FuelStrat_data.json");

            // Load Track objects, if no data file then load from embedded
            if (File.Exists(data_path) == false)
            {
                using Stream stream = assembly.GetManifestResourceStream(tracks_resourse_name);
                using StreamReader reader = new(stream);
                string tracks_embedded = reader.ReadToEnd();
                all_tracks = JsonConvert.DeserializeObject<List<Track>>(tracks_embedded);
            }
            else
            {
                try
                {
                    string json_string_tracks = File.ReadAllText(data_path);
                    all_tracks = JsonConvert.DeserializeObject<List<Track>>(json_string_tracks);
                }
                catch (Exception ex)
                {
                    // catch exception when data file is corrupted or unreadable

                    MessageBox.Show("Error reading FuelStrat_data.json:\n" + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    DialogResult result = MessageBox.Show("Would you like to reset the FuelStrat_data.json?\n\n" +
                        "Choosing 'No' will exit application.",
                        "Reset data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        File.Delete(data_path);
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
                // works only if there is a specific car AND track selected
                return;
            }

            string tracks_resourse_name = "FuelStrat.json_resources.TRACKS.json";
            Assembly assembly = Assembly.GetExecutingAssembly();

            try
            {
                using Stream stream = assembly.GetManifestResourceStream(tracks_resourse_name);
                using StreamReader reader = new(stream);
                string tracks_embedded = reader.ReadToEnd();
                List<Track> temp_tracks = JsonConvert.DeserializeObject<List<Track>>(tracks_embedded);

                // Find the specific track in the list
                Track default_track = temp_tracks.Find(t => t.TrackName == comboBoxTrack.Text);
                CarTrackFuel default_car_track_fuel = default_track.CarTrackFuel.Find(
                    c => c.CarName == comboBoxCar.Text);
                foreach (var track in all_tracks)
                {
                    if (track.TrackName.Equals(comboBox_track.Text))
                    {
                        track.TrackLapTime = default_track.TrackLapTime;

                        foreach (var car_fuel in track.CarTrackFuel)
                        {
                            if (car_fuel.CarName.Equals(comboBox_car.Text))
                            {
                                car_fuel.FuelPerLap = default_car_track_fuel.FuelPerLap;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //  Handle exceptions
                MessageBox.Show("Error reading FuelStrat_data.json:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                DialogResult result = MessageBox.Show("Would you like to reset the FuelStrat_data.json?\n\n" +
                    "Choosing 'No' will exit application.",
                    "Reset data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    File.Delete(Path.Combine(documents_path, "FuelStrat_data.json"));
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
            car_classes = all_cars.Select(car => car.ClassName).Distinct().ToList();

            foreach (var car_class in car_classes)
            {
                comboBoxClass.Items.Add(car_class);
            }
        }

        public void LoadCars(ComboBox comboBoxCar, string car_class)
        {
            comboBoxCar.ResetText();
            comboBoxCar.Items.Clear();

            var cars_within_a_class = all_cars.Where(car => car.ClassName.Contains(car_class));

            foreach (var car in cars_within_a_class)
            {
                comboBoxCar.Items.Add(car.CarName);
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
                comboBoxTrack.Items.Add(track.TrackName);
            }
        }

        public int GetTankCapacity(string carName, string trackName)
        {
            if (updateFromTelemetry != null && sim_data.car_name != "?")
            {
                return sim_data.tank_capacity;
            }

            if (carName == "CAR" || trackName == "TRACK" || carName == "" || trackName == "")
            {
                return 99999;
            }

            var selected_track = all_tracks.FirstOrDefault(track => track.TrackName.Equals(comboBox_track.Text));
            var selected_trackCarFuel = selected_track.CarTrackFuel.FirstOrDefault(
                carTrackFuel => carTrackFuel.CarName.Equals(comboBox_car.Text));

            return int.Parse(selected_trackCarFuel.TankCapacity);
        }

        public static void LoadPitOptions(ComboBox comboBoxPit, List<string> pitOptions)
        {
            foreach (var pit_option in pitOptions)
            {
                comboBoxPit.Items.Add(pit_option);
            }
        }

        private void ComboBox_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Load cars from selected class when comboBox_class changes
            LoadCars(comboBox_car, comboBox_class.Text);
        }

        // Restrictions to key presses in textBoxes, numericUpDowns
        private void TextBox_race_h_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void TextBox_race_min_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void TextBox_lap_time_min_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void TextBox_lap_time_sec_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',';
        }

        private void TextBox_fuel_per_lap_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',';
        }

        private void NumericUpDown_pits_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void TextBox_max_stint_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        public void CalculateTimeLostInPits(int number_of_pits, ComboBox comboBoxPitOptions, string selected_track)
        {
            if (number_of_pits > 0 && comboBoxPitOptions.SelectedIndex == -1)
            {
                // change to default pit option (tires and fuel) if nothing is selected AND
                // number of pits is different than 0
                comboBoxPitOptions.SelectedIndex = 0;
            }

            if (selected_track == "TRACK")
            {
                // calculate time lost in pits when track ISNT selected

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
                // calculate time lost in pits when track IS selected

                var selected_track_object = all_tracks.FirstOrDefault(track => track.TrackName.Contains(selected_track));
                int.TryParse(selected_track_object.TrackPitDuration, out time_in_pits);

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
            // funtion used when 'refuel only' is selected in pit options
            // time lost depends of liters during refuel

            number_of_pits = (int)numericUpDownPits.Value;

            // this statement is needed to skip this function in a situation when number of pits is unknown (0) and
            // will change at the start of CalculatePitStops
            if (number_of_pits == 0)
            {
                return;
            }

            int time_in_pits;
            if (selected_track != "TRACK")
            {
                // when track IS selected
                var selected_track_object = all_tracks.FirstOrDefault(track => track.TrackName.Contains(selected_track));
                int.TryParse(selected_track_object.TrackPitDuration, out time_in_pits);
            }
            else
            {
                // when track ISNT selected
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
            // calculates overall race duration based on how many laps with a given lap time
            // will be before end of the race, time lost in pits is added at the start

            int.TryParse(TextBoxRaceH.Text, out int h_race);
            int.TryParse(textBoxRaceMin.Text, out int min_race);

            if (min_race > 59)
            {
                // if someone iserts more than 59 mins then set 59
                min_race = 59;
                textBoxRaceMin.Text = min_race.ToString();
            }

            if (h_race > 24 || h_race == 24 && min_race != 0)
            {
                // if someone iserts more than 24h then set 24 for hours and 0 for mins
                h_race = 24;
                TextBoxRaceH.Text = h_race.ToString();
                min_race = 0;
                textBoxRaceMin.Text = min_race.ToString();
            }

            // make sure that it doesnt matter if decimal separator is . or ,
            // when more than one separator -> deletes all decimal separators except the first one
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
                // if someone iserts more than 59mins then set 59
                min_laptime = 59;
                textBoxLapMin.Text = min_laptime.ToString();
            }
            if (sec_laptime > 59.99999)
            {
                // if someone iserts more than 59.999secs then set 59.999
                sec_laptime = 59.999f;
                textBoxLapSec.Text = sec_laptime.ToString();
            }

            // fill label in results with user lap time
            int lap_time_secs_floor = (int)Math.Floor(sec_laptime);
            int lap_time_secs_rest = (int)Math.Round((sec_laptime - lap_time_secs_floor) * 1000);
            string formatted_lap_time = string.Format("{0:D1}:{1:D2}.{2:000}",
                textBoxLapMin.Text, lap_time_secs_floor, lap_time_secs_rest);
            labelLapTimeResult2.Text = formatted_lap_time;

            race_duration_secs = (h_race * 3600) + (min_race * 60);
            lap_time_secs = (min_laptime * 60) + sec_laptime;

            // overall race duration starts with time lost in pits
            overall_race_duration = time_lost_in_pits;

            number_of_laps = 0;

            // add lap times until threshold of race duration is crossed
            while (overall_race_duration < race_duration_secs)
            {
                overall_race_duration += lap_time_secs;
                number_of_laps++;
            }

            // fill label in results with number of laps
            labelLapsResult.Text = number_of_laps.ToString();

            // fill label in results with overall race duration
            TimeSpan time_interval = TimeSpan.FromSeconds(overall_race_duration);
            string formatted_overall_duration = string.Format("{0:D2}:{1:D2}:{2:D2}",
                (int)time_interval.TotalHours, time_interval.Minutes, time_interval.Seconds);

            labelOverallResult.Text = formatted_overall_duration;
        }

        public void CalculateLapTimePlusMinus(Label labelPlus1LapTimeResult, Label labelMinus1LapTimeResult)
        {
            // calculates lap times that are needed for the race to be one lap longer and shorter

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
            // calculates fuel for the race based on number of laps and formation lap

            // make sure that it doesnt matter if decimal separator is . or ,
            // when more than one separator deletes all decimal separators except the first one
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

            // taking into account that formation lap can use a bit more fuel while
            // heating up tires with brake dragging
            if (full_formation_lap)
            {
                brake_dragging_full = fuel_per_lap + (fuel_per_lap * 0.1);
            }
            else
            {
                brake_dragging_short = fuel_per_lap * 0.25;
            }
            formation_lap_fuel = brake_dragging_full + brake_dragging_short;

            // fill labels in results with fuel amounts
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
            out List<int> lapsPerStint)
        {
            // calculate pit stop strategy based on number of pit stops, pit options, max stint duration, tank capacity etc.

            dynamic_numericUpDowns.Clear();
            dynamic_labels.Clear();

            // clear pit stop panel from previous calculation
            panelPitStopStrategy.Controls.Clear();

            // groupBox for the start of the race
            Size groupBox_size = new(385, 83);
            GroupBox groupBox_start = new()
            {
                Text = "Stint 1 - Start of the race",
                BackColor = Color.Gainsboro,
                Font = labelFuelRaceResult.Font,
                Location = new Point(40, 26),
                Size = groupBox_size
            };
            panelPitStopStrategy.Controls.Add(groupBox_start);

            // tableLayout with results for groupBox above
            TableLayoutPanel tableLayoutPanel_start = new()
            {
                Size = new Size(350, 32),
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset,
                ColumnCount = 2,
                Location = new Point(15, 35)
            };
            tableLayoutPanel_start.ColumnStyles.Clear();
            tableLayoutPanel_start.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            tableLayoutPanel_start.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            tableLayoutPanel_start.RowCount = 1;

            groupBox_start.Controls.Add(tableLayoutPanel_start);

            // labels that are inside tableLayout above
            Label label_fuel_for_start = new()
            {
                Text = "Fuel for the start",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(130, 17)
            };

            Label label_fuel_start_result = new()
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(label_fuel_start_result);
            dynamic_labels.Add(label_fuel_start_result);

            tableLayoutPanel_start.Controls.Add(label_fuel_for_start, 0, 0);
            tableLayoutPanel_start.Controls.Add(label_fuel_start_result, 1, 0);

            number_of_pits = (int)numericUpDownPits.Value;

            // fuelPerStint, lapsPerStint, labelFuelStartResultText are for testing purposes
            fuelPerStint = [];
            lapsPerStint = [];
            labelFuelStartResultText = "";

            int tank_capacity;

            if (comboBox_car.Text == "CAR" || comboBox_track.Text == "TRACK")
            {
                // tank capacity is unknown when no car or track is selected but it is require for calculation
                // set big amount of tank_capacity so tank isnt a limiting factor
                tank_capacity = 999999;
            }
            else
            {
                tank_capacity = GetTankCapacity(comboBox_car.Text, comboBox_track.Text);
            }

            // if number of pit stops is 0 but pit option and/or max stint duration selected then
            // calculate number of pit stops needed (usefull if we dont know how many pits will fit into a longer race)
            // and recalculate based on new information about number of pit stops
            // '1L refuel' and 'tires only' pit options will get a logical in this case 1 pit stop
            if (comboBoxPitOptions.SelectedIndex != -1 && checkBox_max_stint.Checked == false &&
                numericUpDownPits.Value == 0)
            {
                if (comboBoxPitOptions.Text == "1L refuel" || comboBoxPitOptions.Text == "Tires only")
                {
                    number_of_pits = 1;
                    numericUpDownPits.Value = number_of_pits;
                    Button_calculate_Click(button_calculate, EventArgs.Empty);
                    return;
                }
                else if (comboBox_car.Text != "CAR" && comboBox_track.Text != "TRACK")
                {
                    number_of_pits = fuel_for_race_round_up / tank_capacity;

                    // even if result above is 0 (tank capacity is more than enough for whole race)
                    // but pit option is selected then add one pit stop
                    if (number_of_pits == 0)
                    {
                        number_of_pits++;
                    }

                    numericUpDownPits.Value = number_of_pits;
                    Button_calculate_Click(button_calculate, EventArgs.Empty);
                    return;
                }
            }
            else if (comboBoxPitOptions.SelectedIndex != -1 && checkBox_max_stint.Checked == true &&
                numericUpDownPits.Value == 0)
            {
                // checking if number of pits will be limited by max stint duration or fuel tank
                int number_of_pits_stint = race_duration_secs / (int.Parse(textBox_max_stint.Text) * 60);
                int number_of_pits_tank = number_of_laps / (int)Math.Ceiling(tank_capacity / fuel_per_lap);
                number_of_pits = Math.Max(number_of_pits_tank, number_of_pits_stint);

                numericUpDownPits.Value = number_of_pits;
                Button_calculate_Click(button_calculate, EventArgs.Empty);
                return;
            }

            // if a sum of max stint duration with a set number of pit stops isnt enough for a race
            // recalculate number of pit stops
            if ((number_of_pits + 1) * (int.Parse(textBox_max_stint.Text) * 60) < race_duration_secs &&
                checkBox_max_stint.Checked == true)
            {
                number_of_pits = race_duration_secs / (int.Parse(textBox_max_stint.Text) * 60);
                numericUpDownPits.Value = number_of_pits;
                Button_calculate_Click(button_calculate, EventArgs.Empty);
                return;
            }

            if (number_of_pits == 0)
            {
                // if number of pit stops is 0, then fill fuel label for the start of the race
                // in first groupBox with fuel for whole race

                label_fuel_start_result.Text = labelFuelRaceResult.Text;
                labelFuelStartResultText = label_fuel_start_result.Text;
                fuelPerStint.Add(fuel_for_race_round_up);

                // warning if tank capacity is lower than fuel require for the race
                if (fuel_for_race_round_up > tank_capacity)
                {
                    GroupBox groupBox_warning = new()
                    {
                        Location = new Point(40, 124),
                        Size = new Size(385, 130),
                        Font = labelFuelRaceResult.Font,
                        ForeColor = Color.Red,
                        BackColor = groupBox_start.BackColor,
                        Text = "WARNING"
                    };
                    panelPitStopStrategy.Controls.Add(groupBox_warning);

                    Label label_warning = new()
                    {
                        Text = "Fuel needed for this race (" + fuel_for_race_round_up.ToString() +
                        " L) is greater than fuel tank capacity (" + tank_capacity.ToString() + " L). " +
                        "\n\nConsider adding a pit stop with refuel or " +
                        "fuel saving (" + (fuel_for_race_round_up - tank_capacity).ToString() + " L) during the race.",
                        Location = new Point(16, 30),
                        Size = new Size(350, 90),
                        ForeColor = Color.Black
                    };
                    groupBox_warning.Controls.Add(label_warning);
                }
            }
            else
            {
                // set some variables before going into a pit stop loop
                // 'stint' is a period between pit stops

                double current_laps = 0.0;
                int stints_left = number_of_pits;

                // number_of_laps_remaining holds a number of laps left after each stint
                double laps_per_stint = (double)number_of_laps / (number_of_pits + 1);
                double number_of_laps_remaining = number_of_laps;

                // variable that will set location of grouBox below the previous one
                int y_groupBox = 120;

                // calculate fuel for the first stint and substract it from fuel for the whole race
                // fuel_remaining will now holds a fuel that still needs to be add in next pit stop(s)
                int fuel_first_stint = (int)Math.Ceiling((laps_per_stint * fuel_per_lap) + formation_lap_fuel);
                fuel_first_stint = Math.Min(fuel_first_stint, tank_capacity);

                double fuel_remaining = fuel_for_race_round_up - fuel_first_stint;

                labelFuelStartResultText = fuel_first_stint.ToString() + " L";
                fuelPerStint.Add(fuel_first_stint);

                // stint's limiting factor is either tank capacity or max stint duration
                if (checkBox_max_stint.Checked == true && textBox_max_stint.Text != "0")
                {
                    int laps_per_stint_tank = (int)(fuel_first_stint / fuel_per_lap);
                    int laps_per_stint_max = (int)((int.Parse(textBox_max_stint.Text)) * 60 / lap_time_secs);
                    int laps_per_stint_limit = Math.Min(laps_per_stint_max, laps_per_stint_tank);
                    laps_per_stint = Math.Min(laps_per_stint_limit, laps_per_stint);
                }

                // variables that will be used when when 1L strategy isnt optimal
                List<int> fuel_1L_adjusted = [];
                int full_tank_count = fuel_for_race_round_up / tank_capacity;
                int full_tank_sum = 0;

                for (int i = full_tank_count; i > 0; i--)
                {
                    fuel_1L_adjusted.Add(tank_capacity);
                    full_tank_sum += tank_capacity;
                }

                if (number_of_pits < full_tank_count && is_strat_ok == false)
                {
                    number_of_pits += full_tank_count - 1;
                    numericUpDownPits.Value = number_of_pits;
                }

                if (comboBoxPitOptions.Text == "1L refuel")
                {
                    laps_per_stint = (double)number_of_laps / (number_of_pits + 1);
                    number_of_laps_remaining = number_of_laps;
                }

                int pit_stops_left = number_of_pits - full_tank_count;
                int fuel_1L_adjusted_remaining = fuel_for_race_round_up - full_tank_sum -
                    (pit_stops_left);

                fuel_1L_adjusted.Add(fuel_1L_adjusted_remaining);

                for (int i = pit_stops_left; i > 0; i--)
                {
                    fuel_1L_adjusted.Add(1);
                }

                // stint counter starts from 2 because 1st stint is already calculated by default in first groupBox
                int stint = 2;
                for (int i = number_of_pits; i > 0; i--)
                {
                    // names for each groupBox changes with each stint
                    string name_for_groupbox = "groupBox_stint" + stint.ToString();
                    string name_for_refuel_label = "label_refuel_stint" + stint.ToString();
                    string name_for_refuel_result_label = "label_refuel_stint" + stint.ToString() + "_result";
                    string name_for_table = "tableLayoutPanel_stint" + stint.ToString();

                    if (comboBoxPitOptions.Text == "Tires only")
                    {
                        // 'tires only' means no refuel so fuel for the start is the same as for the whole race
                        // each loop iteration, groupBoxes are only fill with information about pit timing

                        label_fuel_start_result.Text = label_fuel_race_result.Text;

                        GroupBox groupBox_temp = new()
                        {
                            Name = name_for_groupbox,
                            Location = new Point(40, y_groupBox),
                            Font = labelFuelRaceResult.Font,
                            Size = groupBox_size,
                            BackColor = groupBox_start.BackColor,
                            Text = "Stint " + stint
                        };
                        panelPitStopStrategy.Controls.Add(groupBox_temp);

                        TableLayoutPanel table_temp = new()
                        {
                            Name = name_for_table,
                            Location = new Point(16, 30),
                            CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset
                        };
                        table_temp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
                        table_temp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));

                        NumericUpDown numericUpDown_laps_temp = new()
                        {
                            Name = "numericUpDown_laps_stint" + stint.ToString(),
                            Dock = DockStyle.Fill,
                            TextAlign = HorizontalAlignment.Center,
                            Maximum = 999
                        };
                        this.Controls.Add(numericUpDown_laps_temp);
                        dynamic_numericUpDowns.Add(numericUpDown_laps_temp);

                        Label label_refuel_temp = new()
                        {
                            Dock = DockStyle.Fill,
                            TextAlign = ContentAlignment.MiddleCenter,
                            Name = name_for_refuel_label
                        };

                        double current_part = Math.Min(number_of_laps_remaining, laps_per_stint);
                        current_laps += current_part;
                        label_refuel_temp.Text = "Tires change after";
                        number_of_laps_remaining -= laps_per_stint;
                        fuelPerStint.Add(0);
                        lapsPerStint.Add((int)Math.Ceiling(current_laps));

                        numericUpDown_laps_temp.Value = ((int)Math.Ceiling(current_laps));

                        table_temp.Controls.Add(label_refuel_temp, 0, 0);
                        table_temp.Controls.Add(numericUpDown_laps_temp, 1, 0);
                        table_temp.Size = tableLayoutPanel_start.Size;

                        groupBox_temp.Controls.Add(table_temp);
                        stint++;
                        stints_left--;
                        y_groupBox += 100;

                        // on last loop iteration when all stint groupBoxes are done,
                        // show a warning if fuel tank capacity is lower than fuel for the whole race
                        if (fuel_for_race_round_up > tank_capacity && i == 1)
                        {
                            GroupBox groupBox_warning = new()
                            {
                                Location = new Point(40, y_groupBox),
                                Size = new Size(385, 130),
                                Font = labelFuelRaceResult.Font,
                                ForeColor = Color.Red,
                                BackColor = groupBox_start.BackColor,
                                Text = "WARNING"
                            };
                            panelPitStopStrategy.Controls.Add(groupBox_warning);

                            Label label_warning = new()
                            {
                                Text = "Fuel needed for this race (" + fuel_for_race_round_up.ToString() +
                                " L) is greater than fuel tank capacity (" + tank_capacity.ToString() + " L). " +
                                "\n\nConsider changing a pit stop option with refuel or " +
                                "fuel saving (" + (fuel_for_race_round_up - tank_capacity).ToString() + " L) during the race.",
                                Location = new Point(16, 30),
                                Size = new Size(350, 90),
                                ForeColor = Color.Black
                            };
                            groupBox_warning.Controls.Add(label_warning);
                        }
                    }
                    else if (comboBoxPitOptions.Text == "1L refuel")
                    {
                        // '1L refuel' pit options will attempt to set only 1L of refuel in each pit stop
                        // 'is_strat_ok' is used to switch to adjusted strategy if 1L per pit stop isnt enough of a refuel
                        is_strat_ok = true;

                        // warning that strategy needs to be adjusted
                        if (fuel_for_race_round_up > (tank_capacity + number_of_pits))
                        {
                            groupBox_start.Size = new Size(385, 123);
                            Label label_tank = new()
                            {
                                Text = "Exceeded fuel tank capacity of " + tank_capacity.ToString() +
                                " L for 1L strategy. Adjusted with higher refuel.",
                                Size = new Size(350, 50),
                                Location = new Point(16, 74)
                            };
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
                                // standard '1L refuel' strategy, 1L refuel each pit stop
                                // number of pit stops (liters) are substrated from a fuel for the whole race

                                label_fuel_start_result.Text = (fuel_for_race_round_up - number_of_pits).ToString() + " L";

                                GroupBox groupBox_temp = new()
                                {
                                    Name = name_for_groupbox,
                                    Location = new Point(40, y_groupBox),
                                    Size = new Size(385, 103),
                                    Font = labelFuelRaceResult.Font,
                                    BackColor = groupBox_start.BackColor,
                                    Text = "Stint " + stint
                                };
                                panelPitStopStrategy.Controls.Add(groupBox_temp);

                                TableLayoutPanel table_temp = new()
                                {
                                    Name = name_for_table,
                                    Location = new Point(16, 30),
                                    CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset
                                };
                                table_temp.ColumnStyles.Clear();
                                table_temp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
                                table_temp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));

                                Label label_refuel_temp = new()
                                {
                                    Name = name_for_refuel_label,
                                    Dock = DockStyle.Fill,
                                    TextAlign = ContentAlignment.MiddleCenter,
                                    Text = "Refuel for Stint " + stint.ToString()
                                };

                                NumericUpDown numericUpDown_laps_temp = new()
                                {
                                    Name = "numericUpDown_laps_stint" + stint.ToString(),
                                    Dock = DockStyle.Fill,
                                    TextAlign = HorizontalAlignment.Center,
                                    Maximum = 999
                                };
                                this.Controls.Add(numericUpDown_laps_temp);
                                dynamic_numericUpDowns.Add(numericUpDown_laps_temp);

                                double current_part = Math.Min(number_of_laps_remaining, laps_per_stint);
                                current_laps += current_part;
                                numericUpDown_laps_temp.Value = ((int)Math.Ceiling(current_laps));

                                number_of_laps_remaining -= laps_per_stint;
                                lapsPerStint.Add((int)Math.Ceiling(current_laps));

                                Label label_refuel_result_temp = new()
                                {
                                    Text = "1 L",
                                    Name = name_for_refuel_result_label,
                                    Dock = DockStyle.Fill,
                                    TextAlign = ContentAlignment.MiddleCenter
                                };
                                fuelPerStint.Add(1);

                                Label label_laps_temp = new()
                                {
                                    Name = "label_laps_stint" + stint.ToString(),
                                    Dock = DockStyle.Fill,
                                    TextAlign = ContentAlignment.MiddleCenter,
                                    Text = "Pit after lap"
                                };

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
                                // adjusted '1L refuel' strategy where more fuel is needed than with a standard approach
                                // this version will attempt to have a pit stop with 1L
                                // example: 1st stint = 120L, 2nd stint = 56L, 3rd stint = 1L
                                // laps from each stint are being substracted from number_of_laps_remaining

                                label_fuel_start_result.Text = tank_capacity.ToString() + " L";

                                GroupBox groupBox_temp2 = new()
                                {
                                    Name = name_for_groupbox,
                                    Location = new Point(40, y_groupBox),
                                    Size = new Size(385, 103),
                                    Font = labelFuelRaceResult.Font,
                                    BackColor = groupBox_start.BackColor,
                                    Text = "Stint " + stint
                                };
                                panelPitStopStrategy.Controls.Add(groupBox_temp2);

                                TableLayoutPanel table_temp2 = new()
                                {
                                    Name = name_for_table,
                                    Location = new Point(16, 30),
                                    CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset
                                };
                                table_temp2.ColumnStyles.Clear();
                                table_temp2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
                                table_temp2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));

                                Label label_laps_temp2 = new()
                                {
                                    Name = name_for_refuel_label,
                                    Dock = DockStyle.Fill,
                                    TextAlign = ContentAlignment.MiddleCenter,
                                    Text = "Pit after lap"
                                };

                                Label label_refuel_temp2 = new()
                                {
                                    Name = name_for_refuel_label,
                                    Dock = DockStyle.Fill,
                                    TextAlign = ContentAlignment.MiddleCenter,
                                    Text = "Refuel for Stint " + stint.ToString()
                                };

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

                                NumericUpDown numericUpDown_laps_temp2 = new()
                                {
                                    Name = "numericUpDown_laps_stint" + stint.ToString(),
                                    Dock = DockStyle.Fill,
                                    TextAlign = HorizontalAlignment.Center,
                                    Maximum = 9999
                                };
                                this.Controls.Add(numericUpDown_laps_temp2);
                                dynamic_numericUpDowns.Add(numericUpDown_laps_temp2);

                                double current_part_laps = Math.Min(number_of_laps_remaining, laps_per_stint);
                                current_laps += current_part_laps;
                                number_of_laps_remaining -= laps_per_stint;
                                lapsPerStint.Add((int)Math.Ceiling(current_laps));

                                numericUpDown_laps_temp2.Value = ((int)Math.Ceiling(current_laps));

                                // numericUpDowns have to be locked when dealing with full tank so it makes sense
                                // for this 1L strategy
                                if (fuel_for_this_stint == tank_capacity || fuelPerStint[stint - 2] == tank_capacity)
                                {
                                    numericUpDown_laps_temp2.Value = (int)(tank_capacity / fuel_per_lap) * (stint - 1);
                                    numericUpDown_laps_temp2.Enabled = false;
                                }

                                if (fuelPerStint[stint - 2] == tank_capacity)
                                {
                                    numericUpDown_laps_temp2.Enabled = false;
                                }

                                Label label_refuel_result_temp2 = new()
                                {
                                    Name = name_for_refuel_result_label,
                                    Text = fuel_for_this_stint.ToString() + " L",
                                    Dock = DockStyle.Fill,
                                    TextAlign = ContentAlignment.MiddleCenter
                                };

                                table_temp2.Controls.Add(label_refuel_temp2, 0, 1);
                                table_temp2.Controls.Add(label_refuel_result_temp2, 1, 1);
                                table_temp2.Controls.Add(label_laps_temp2, 0, 0);
                                table_temp2.Controls.Add(numericUpDown_laps_temp2, 1, 0);
                                table_temp2.Size = new Size(350, 65);

                                groupBox_temp2.Controls.Add(table_temp2);
                                y_groupBox += 100;
                                stint++;

                                // recalculate everything on last loop is needed because time lost in pits
                                // will change with this adjusted strategy
                                if (i == 1 && is_recalculate_needed == true)
                                {
                                    time_lost_in_pits += more_time_in_pits;
                                    CalculateRaceDuration(textBox_race_h, textBox_race_min, textBox_lap_time_min, textBox_lap_time_sec,
                                        label_overall_result, label_laps_result, label_lap_time_result2);
                                    CalculateLapTimePlusMinus(label_plus1_lap_time_result, label_minus1_lap_time_result);
                                    CalculateFuel(textBox_fuel_per_lap, listBox_formation, label_fuel_race_result,
                                        label_plus1_fuel_result, label_minus1_fuel_result);
                                    is_recalculate_needed = false;    // change to false so it wont be endless loop
                                    CalculatePitStops(panel_pit_stop_strategy, label_fuel_race_result, numericUpDown_pits, comboBox_pit_options,
                                        out string labelFuelStartResultTextForTesting, out List<int> fuelPerStintForTesting,
                                        out List<int> lapsPitStintForTesting);
                                    is_recalculate_needed = true;     // change back to true for next calculations
                                    SaveData();
                                }
                                break;
                        }

                        // warning that for this number of pit stops with full tanks, its still not enough fuel
                        // for the whole race
                        if (i == 1 && pit_stops_left < 0)
                        {
                            GroupBox groupBox_warning = new()
                            {
                                Name = name_for_groupbox + "_warning",
                                Location = new Point(40, y_groupBox),
                                Size = new Size(385, 135),
                                Font = labelFuelRaceResult.Font,
                                ForeColor = Color.Red,
                                BackColor = groupBox_start.BackColor,
                                Text = "WARNING"
                            };
                            panelPitStopStrategy.Controls.Add(groupBox_warning);

                            Label label_warning = new()
                            {
                                Name = name_for_refuel_result_label + "_warning",
                                Text = "Fuel needed for this race (" + fuel_for_race_round_up.ToString() +
                                " L) is greater than sum of full tank pit stops (" + full_tank_sum.ToString() + " L). " +
                                "\n\nConsider changing pit option to refuel and increase number of pit stops or " +
                                "fuel saving (" + (fuel_for_race_round_up - full_tank_sum).ToString() + " L) during the race.",
                                Location = new Point(16, 30),
                                Size = new Size(350, 90),
                                ForeColor = Color.Black
                            };
                            groupBox_warning.Controls.Add(label_warning);
                        }
                    }
                    else
                    {
                        // 'fixed refuel only', 'refuel only' and 'refuel + tires' share the same pit strategy formula
                        // fuel from each stint is being substracted from fuel_remaining
                        // laps from each stint are being substracted from number_of_laps_remaining

                        label_fuel_start_result.Text = fuel_first_stint.ToString() + " L";

                        GroupBox groupBox_temp = new()
                        {
                            Name = name_for_groupbox,
                            Location = new Point(40, y_groupBox),
                            Size = new Size(385, 103),
                            Font = labelFuelRaceResult.Font,
                            BackColor = groupBox_start.BackColor,
                            Text = "Stint " + stint
                        };
                        panelPitStopStrategy.Controls.Add(groupBox_temp);

                        TableLayoutPanel table_temp = new()
                        {
                            Name = name_for_table,
                            Location = new Point(16, 30),
                            CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset
                        };
                        table_temp.ColumnStyles.Clear();
                        table_temp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
                        table_temp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));

                        NumericUpDown numericUpDown_laps_temp = new()
                        {
                            Name = "numericUpDown_laps_stint" + stint.ToString(),
                            Dock = DockStyle.Fill,
                            TextAlign = HorizontalAlignment.Center,
                            Maximum = 99999
                        };
                        this.Controls.Add(numericUpDown_laps_temp);
                        dynamic_numericUpDowns.Add(numericUpDown_laps_temp);

                        Label label_refuel_temp = new()
                        {
                            Name = name_for_refuel_label,
                            Dock = DockStyle.Fill,
                            TextAlign = ContentAlignment.MiddleCenter,
                            Text = "Refuel for Stint " + stint.ToString()
                        };

                        double current_part_laps = Math.Min(number_of_laps_remaining, laps_per_stint);
                        current_laps += current_part_laps;

                        numericUpDown_laps_temp.Value = ((int)Math.Ceiling(current_laps));

                        number_of_laps_remaining -= laps_per_stint;
                        lapsPerStint.Add((int)Math.Ceiling(current_laps));

                        double current_part_fuel = Math.Min(fuel_remaining,
                            Math.Min((fuel_remaining / stints_left), tank_capacity));

                        int fuel_for_this_stint = (int)Math.Ceiling(current_part_fuel);
                        fuel_remaining -= fuel_for_this_stint;
                        stints_left--;
                        fuelPerStint.Add(fuel_for_this_stint);

                        Label label_laps_temp = new()
                        {
                            Name = "label_laps_stint" + stint.ToString(),
                            Dock = DockStyle.Fill,
                            TextAlign = ContentAlignment.MiddleCenter,
                            Text = "Pit after lap"
                        };

                        Label label_refuel_result_temp = new()
                        {
                            Name = name_for_refuel_result_label,
                            Text = fuel_for_this_stint.ToString() + " L",
                            Dock = DockStyle.Fill,
                            TextAlign = ContentAlignment.MiddleCenter
                        };
                        this.Controls.Add(label_refuel_result_temp);
                        dynamic_labels.Add(label_refuel_result_temp);

                        table_temp.Controls.Add(label_refuel_temp, 0, 1);
                        table_temp.Controls.Add(label_refuel_result_temp, 1, 1);
                        table_temp.Controls.Add(label_laps_temp, 0, 0);
                        table_temp.Controls.Add(numericUpDown_laps_temp, 1, 0);
                        table_temp.Size = new Size(350, 65);

                        groupBox_temp.Controls.Add(table_temp);
                        y_groupBox += 112;
                        stint++;

                        // warning that for this number of pit stops with full tanks, its still not enough fuel
                        // for the whole race
                        if (i == 1 && pit_stops_left < 0 && full_tank_sum < fuel_for_race_round_up)
                        {
                            GroupBox groupBox_warning = new()
                            {
                                Name = name_for_groupbox + "_warning",
                                Location = new Point(40, y_groupBox),
                                Size = new Size(385, 135),
                                Font = labelFuelRaceResult.Font,
                                ForeColor = Color.Red,
                                BackColor = groupBox_start.BackColor,
                                Text = "WARNING"
                            };
                            panelPitStopStrategy.Controls.Add(groupBox_warning);

                            Label label_warning = new()
                            {
                                Name = name_for_refuel_result_label + "_warning",
                                Text = "Fuel needed for this race (" + fuel_for_race_round_up.ToString() +
                                " L) is greater than sum of full tank pit stops (" + full_tank_sum.ToString() + " L). " +
                                "\n\nConsider increasing number of pit stops or " +
                                "fuel saving (" + (fuel_for_race_round_up - full_tank_sum).ToString() + " L) during the race.",
                                Location = new Point(16, 30),
                                Size = new Size(350, 90),
                                ForeColor = Color.Black
                            };
                            groupBox_warning.Controls.Add(label_warning);
                        }
                    }
                }
            }
            foreach (NumericUpDown numericUpDown in dynamic_numericUpDowns)
            {
                int numeric_value = Convert.ToInt32((numericUpDown).Value);
                new_list_of_laps_to_pit.Add(numeric_value);
            };

            PitLimits();    // set max and min limits on numericUpDowns

            new_list_of_laps_to_pit.Clear();
        }

        private void Recalculate()
        {
            // recalculate fuel when moving pit stops up and down in pit stop panel

            if (comboBox_pit_options.Text == "Tires only" || comboBox_pit_options.Text == "1L refuel")
            {
                // Tires only and 1L refuel dont need a recalculation, only PitLimits

                PitLimits();
                return;
            }
            else
            {
                int fuel_first_stint = (int)Math.Ceiling((new_list_of_laps_to_pit[0] * fuel_per_lap) + formation_lap_fuel);
                double fuel_remaining = fuel_for_race_round_up - fuel_first_stint;
                double rest_from_prev_stint = fuel_first_stint - (int)Math.Ceiling((new_list_of_laps_to_pit[0] * fuel_per_lap) +
                    formation_lap_fuel);
                dynamic_labels[0].Text = fuel_first_stint.ToString() + " L";

                int sum_of_laps_from_prev_iterations = 0;
                int current_laps;

                for (int i = 1; i < dynamic_labels.Count; i++)
                {
                    if (i != dynamic_labels.Count - 1)
                    {
                        current_laps = new_list_of_laps_to_pit[i] - new_list_of_laps_to_pit[i - 1];
                    }
                    else
                    {
                        current_laps = number_of_laps - sum_of_laps_from_prev_iterations;
                    }

                    sum_of_laps_from_prev_iterations += (int)current_laps;

                    int tank_capacity = GetTankCapacity(comboBox_car.Text, comboBox_track.Text);

                    double current_part_fuel = Math.Min(fuel_remaining, (
                                    Math.Min((current_laps * fuel_per_lap) - rest_from_prev_stint, tank_capacity)));
                    rest_from_prev_stint = (int)Math.Ceiling(current_part_fuel) - current_part_fuel;

                    int fuel_for_this_stint = (int)Math.Ceiling(current_part_fuel);
                    fuel_remaining -= fuel_for_this_stint;

                    dynamic_labels[i].Text = fuel_for_this_stint.ToString() + " L";
                }
            }

            PitLimits();    // set NEW max and min limits on numericUpDowns

            new_list_of_laps_to_pit.Clear();
        }

        private void PitLimits()
        {
            // numericUpDowns need min and max limit to prevent moving the pit stop beyond practical feasibility

            if (number_of_pits == 0)
            {
                return;
            }

            // set a minimum and maximum for NumericUpDowns

            int max_stint_limit = 99999;    // big number so it wont be a limit if there is no maximum stint timer
            int tank_capacity = 99999;      // big number so it wont be a limit if there is no car and track selected
            List<int> tank_capacity_limit = [];

            if (comboBox_car.Text != "CAR" && comboBox_track.Text != "TRACK")
            {
                if (comboBox_pit_options.Text != "Tires only")
                {
                    tank_capacity = GetTankCapacity(comboBox_car.Text, comboBox_track.Text);
                }

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

            // first pit stop cant be earlier than after lap 1 (min) and later than next pit stop (max)
            // fuel tank capacity and/or max stint timer might also be a limiting factor

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

            // another pit stop thresholds are previous pit timing and next one or 2nd to last lap
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
            // load data into comboBoxes if track and optionally car are selected

            if (comboBox_track.Text != "TRACK")
            {
                string selected_car = comboBox_car.Text;
                string selected_track = comboBox_track.Text;
                var track = all_tracks.Where(track => track.TrackName.Equals(selected_track)).First();

                float.TryParse(track.TrackLapTime.Replace(".", DECIMAL_SEPARATOR).Replace(",", DECIMAL_SEPARATOR),
                    out float lap_time);
                int lap_time_mins = (int)(lap_time / 60);
                float lap_time_secs = lap_time % 60;
                textBox_lap_time_min.Text = lap_time_mins.ToString();
                textBox_lap_time_sec.Text = lap_time_secs.ToString("0.000").Replace(",", ".");

                if (comboBox_car.Text != "CAR")
                {
                    CarTrackFuel car_fuel = track.CarTrackFuel.Find(car => car.CarName == selected_car);
                    textBox_fuel_per_lap.Text = car_fuel.FuelPerLap;
                }
            }
        }

        private void SaveData()
        {
            // saves data to 'FuelStrat_data.json' (fuel per lap, lap times)

            foreach (var track in all_tracks)
            {
                if (track.TrackName.Equals(comboBox_track.Text))
                {
                    track.TrackLapTime = lap_time_secs.ToString();

                    foreach (var car_fuel in track.CarTrackFuel)
                    {
                        if (car_fuel.CarName.Equals(comboBox_car.Text))
                        {
                            car_fuel.FuelPerLap = textBox_fuel_per_lap.Text;
                        }
                    }
                }
            }

            string json_data_save = JsonConvert.SerializeObject(all_tracks, Formatting.Indented);
            File.WriteAllText(Path.Combine(documents_path, "FuelStrat_data.json"), json_data_save);
        }

        public FuelStrat()
        {
            InitializeComponent();

            Font consolas_font = listBox_formation.Font;
            listBoxMultiline_recent_sessions.Location = new Point(21, 12);
            listBoxMultiline_recent_sessions.Size = new Size(993, 90);
            listBoxMultiline_recent_sessions.DrawMode = DrawMode.OwnerDrawVariable;
            listBoxMultiline_recent_sessions.Font = consolas_font;
            listBoxMultiline_recent_sessions.ScrollAlwaysVisible = true;
            listBoxMultiline_recent_sessions.SelectedIndexChanged += ListBox_recent_sessions_SelectedIndexChanged;
            this.panel_telemetry.Controls.Add(listBoxMultiline_recent_sessions);

            if (!Directory.Exists(documents_path))
            {
                // create a FuelStrat folder in documents

                Directory.CreateDirectory(documents_path);
            }
        }

        public void Form_Load(object sender, EventArgs e)
        {
            // actions taken on applications start

            LoadCarTrackObjects();
            LoadCarClasses(comboBox_class);
            LoadTracks(comboBox_track);
            LoadPitOptions(comboBox_pit_options, PIT_OPTIONS);
        }

        public static void ChangeControlColor(Control control, Color color)
        {
            // textBox will flash in a certain color (for warnings or automatic fill of Control objects)

            //Color original_color = control.BackColor;

            control.BackColor = color;

            System.Windows.Forms.Timer timer = new()
            {
                Interval = 1000
            };
            timer.Tick += (sender, e) =>
            {
                //control.BackColor = original_color;

                control.BackColor = Color.White;

                timer.Dispose();
            };
            timer.Start();
        }

        private static bool OnlyZeros(string text)
        {
            // check if string consists only characters from the list below (for button_calculate_Click action)

            List<char> chars = ['0', '.', ','];

            foreach (char c in text)
            {
                if (!chars.Contains(c))
                {
                    return true;
                }
            }
            return false;
        }

        private void Button_calculate_Click(object sender, EventArgs e)
        {
            if (comboBox_track.Text == "Nordschleife" && listBox_formation.SelectedIndex == -1)
            {
                // change formation lap automatically to short when Nordschleife is selected
                // this track is usually set up with short formation lap

                listBox_formation.SelectedIndex = 1;
            }
            else if (listBox_formation.SelectedIndex == -1)
            {
                listBox_formation.SelectedIndex = 0;
            }

            // if either lap time, fuel per lap or race duration is set to 0
            // prevent further calculations and flash textBox(es) with red color

            bool only_zeros = false;

            if (!OnlyZeros(textBox_fuel_per_lap.Text))
            {
                ChangeControlColor(textBox_fuel_per_lap, Color.Red);
                only_zeros = true;
            }

            if (!OnlyZeros(textBox_lap_time_min.Text + textBox_lap_time_sec.Text))
            {
                ChangeControlColor(textBox_lap_time_min, Color.Red);
                ChangeControlColor(textBox_lap_time_sec, Color.Red);
                only_zeros = true;
            }

            if (!OnlyZeros(textBox_race_h.Text + textBox_race_min.Text))
            {
                ChangeControlColor(textBox_race_h, Color.Red);
                ChangeControlColor(textBox_race_min, Color.Red);
                only_zeros = true;
            }

            if (only_zeros)
            {
                return;
            }

            if (comboBox_pit_options.Text == "Refuel only")
            {
                // for 'refuel only' pit option, time lost in pits is unknown because it relies on how much fuel
                // needs to be add during pit stop based on fuel for whole race and number of pit stops
                // fuel for whole race is unknown until CalculateFuel
                // in this case first iteration gets a default pit duration
                // next iteration gets more accurate information thanks to RefuelTimeLost
                // there are 3 iteration to make sure that end result is as close as possible to ideal one

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
                // standard procedure when 'only refuel' ISNT selected

                CalculateTimeLostInPits((int)numericUpDown_pits.Value, comboBox_pit_options, comboBox_track.Text);
                CalculateRaceDuration(textBox_race_h, textBox_race_min, textBox_lap_time_min, textBox_lap_time_sec,
                    label_overall_result, label_laps_result, label_lap_time_result2);
                CalculateLapTimePlusMinus(label_plus1_lap_time_result, label_minus1_lap_time_result);
                CalculateFuel(textBox_fuel_per_lap, listBox_formation, label_fuel_race_result,
                    label_plus1_fuel_result, label_minus1_fuel_result);
            }
            CalculatePitStops(panel_pit_stop_strategy, label_fuel_race_result, numericUpDown_pits, comboBox_pit_options,
                out string labelFuelStartResultTextForTesting, out List<int> fuelPerStint,
                out List<int> lapsPerStint);
            SaveData();
        }

        private void ComboBox_car_SelectedIndexChanged(object sender, EventArgs e)
        {
            // load new data when selected car changes

            LoadData();
        }

        private void ComboBox_track_SelectedIndexChanged(object sender, EventArgs e)
        {
            // load new data when selected track changes

            LoadData();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // menu->exit

            Application.Exit();
        }

        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // help button, showing new window

            FuelStratHelp fuelStratHelp = new();
            fuelStratHelp.Show();
        }

        private void CheckBox_max_stint_Click(object sender, EventArgs e)
        {
            // checkBox max stint enables and disables textBox

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

        private void GitHubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // github button, opnes github website of this project in a browser

            string github_url = "https://github.com/LabuzPawel/FuelStrat";
            Process.Start(new ProcessStartInfo
            {
                FileName = github_url,
                UseShellExecute = true
            });
        }

        private void Form_Shown(object sender, EventArgs e)
        {
            InitializeTelemetryTimer();

            try
            {
                if (File.Exists(Path.Combine(documents_path, "FuelStrat_last_state.json")))
                {
                    // Load last state of the app if file exists

                    string saved_json = File.ReadAllText(Path.Combine(documents_path, "FuelStrat_last_state.json"));
                    LastStateOfTheApp last_state =
                    JsonConvert.DeserializeObject<LastStateOfTheApp>(saved_json);

                    comboBox_class.SelectedIndex = last_state.Controls.saved_car_class_index;
                    comboBox_car.SelectedIndex = last_state.Controls.saved_car_index;
                    comboBox_track.SelectedIndex = last_state.Controls.saved_track_index;
                    textBox_race_h.Text = last_state.Controls.saved_race_h;
                    textBox_race_min.Text = last_state.Controls.saved_race_min;
                    textBox_lap_time_min.Text = last_state.Controls.saved_lap_min;
                    textBox_lap_time_sec.Text = last_state.Controls.saved_lap_secs;
                    textBox_fuel_per_lap.Text = last_state.Controls.saved_fuel_per_lap;
                    listBox_formation.SelectedIndex = last_state.Controls.saved_formation_index;
                    numericUpDown_pits.Value = last_state.Controls.saved_number_of_pits;
                    comboBox_pit_options.SelectedIndex = last_state.Controls.saved_pit_stop_option_index;
                    checkBox_max_stint.Checked = last_state.Controls.saved_checkbox_max_stint;
                    textBox_max_stint.Text = last_state.Controls.saved_max_stint;
                    textBox_max_stint.Enabled = last_state.Controls.saved_max_stint_enabled;

                    foreach (var stint in last_state.Stints)
                    {
                        recent_stints.Add(stint);
                    }

                    foreach (var stint in recent_stints)
                    {
                        string to_listbox =
                            "Session: " + stint.Stint.session_type + " | " +
                            "Car: " + stint.Stint.car_name + " | " +
                            "Track: " + stint.Stint.track_name + " | " +
                            "Date: " + stint.Stint.date_time + "nextLine" +
                            " Laps: " + stint.Stint.stint_lenght + " | " +
                            "Avg lap time: " + LapTimeSecsFormatted(stint.Stint.average_lap_time) + " | " +
                            "Avg fuel per lap: " + Math.Round(stint.Stint.average_fuel_per_lap, 2).ToString().
                            Replace(",", ".") + " | " +
                            "Fuel at the start: " + stint.Stint.fuel_at_the_start + " L";

                        listBoxMultiline_recent_sessions.Items.Add(to_listbox);
                    }
                }
            }
            catch
            {
                // abort if file is corrupted or cant be deserialized

                return;
            }
        }

        private void NumericUpDown_pit_strat_changes(object sender, EventArgs e)
        {
            // this event needs a debounce, multiple fast clicks and changes overloads CPU

            recalculate_debouncer.Debouce(() =>
            {
                new_list_of_laps_to_pit.Clear();
                foreach (NumericUpDown numericUpDown in dynamic_numericUpDowns)
                {
                    int numeric_value = Convert.ToInt32((numericUpDown).Value);
                    new_list_of_laps_to_pit.Add(numeric_value);
                };
                Recalculate();
                new_list_of_laps_to_pit.Clear();
            });
        }

        private void ComboBox_pit_options_SelectedIndexChanged(object sender, EventArgs e)
        {
            // if 'no pit stops' selected -> reset pit stops panel

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

        private void ResetAllDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // menu->reset data->reset current cat/track, option to reset data just for the selected car and track combination

            DialogResult result = MessageBox.Show("Would you like to reset the 'FuelStrat_data.json'?",
                        "Reset data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                File.Delete(Path.Combine(documents_path, "FuelStrat_data.json"));
                LoadCarTrackObjects();
            }
        }

        private void ResetCurrentCartrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // menu->reset data->reset all data, option to reset FuelStrat_data.json

            ResetSpecficCarTrack(comboBox_car, comboBox_track);
            LoadData();     // load reseted data and fill textBoxes with it
            CalculateRaceDuration(textBox_race_h, textBox_race_min, textBox_lap_time_min, textBox_lap_time_sec,
                    label_overall_result, label_laps_result, label_lap_time_result2);   // SaveData needs lap_time_secs
            SaveData();
        }

        private void InitializeTelemetryTimer()
        {
            // start a timer that will check if ACC is running

            telemetryTimer = new System.Threading.Timer(
                async _ => await GameStatusCheckAsync(), null, 3000, 5000);
        }

        private async Task GameStatusCheckAsync()
        {
            bool isACCRunning = await Task.Run(() => CheckGameStatus());

            if (isACCRunning)
            {
                UpdateGameStatus(true);
            }
            else
            {
                UpdateGameStatus(false);
            }
        }

        public bool CheckGameStatus()
        {
            // check processes if ACC exe is running
            // if users checks 'Telemetry disabled' then this will result in false

            if (telemetryDisabledToolStripMenuItem.Checked)
            {
                return false;
            }
            else
            {
                Process[] processes = Process.GetProcessesByName("AC2-Win64-Shipping");
                return processes.Length > 0;
            }
        }

        private void UpdateGameStatusMenuItem(bool isRunning)
        {
            // update ACC status icon

            if (isRunning)
            {
                ToolStripMenuItem_game_status.Text = " ACC ON ";
                ToolStripMenuItem_game_status.Image = Properties.Resources.greenFlag;
            }
            else
            {
                ToolStripMenuItem_game_status.Text = " ACC OFF";
                ToolStripMenuItem_game_status.Image = Properties.Resources.redFlag;

                // if there were some recorded laps and ACC was shut down, complete a stint with AddToListBox

                ConvertLapsIntoStintAndAddToListBox();
                previous_lap = -1;
            }
        }

        private void ToolStripMenuItem_game_status_TextChanged(object sender, EventArgs e)
        {
            // actions to be taken when ACC status changes

            if (ToolStripMenuItem_game_status.Text == " ACC ON ")
            {
                // start reading telemetry on a timer when ACC is ON

                updateFromTelemetry = new UpdateFromTelemetry();

                updateFromTelemetry.StartReading();

                telemetry_update_timer = new System.Threading.Timer(
                    _ => UpdateRecentSessions(updateFromTelemetry),
                    null,
                    TimeSpan.Zero,
                    TimeSpan.FromSeconds(3));
            }
            else if (ToolStripMenuItem_game_status.Text == " ACC OFF")
            {
                // if ACC is shut down dispose telemetry timer and stop reading

                telemetry_update_timer?.Dispose();

                updateFromTelemetry?.StopReading();

                button_auto.Enabled = false;
                button_import_race.Enabled = false;
                previous_lap = -1;
            }
        }

        private void UpdateGameStatus(bool isRunning)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((System.Windows.Forms.MethodInvoker)(() =>
                UpdateGameStatusMenuItem(isRunning)));
            }
            else
            {
                UpdateGameStatusMenuItem(isRunning);
            }
        }

        public static string LapTimeSecsFormatted(int lapTimeInMillisecs)
        {
            // telemtry sends lap time in milliseconds, method to format into a string M:SS.msmsms

            TimeSpan timeSpan = TimeSpan.FromMilliseconds(lapTimeInMillisecs);

            string lap_time_to_string = $"{timeSpan.Minutes}:{timeSpan.Seconds:00}." +
                $"{timeSpan.Milliseconds:000}";

            return lap_time_to_string;
        }

        public static bool IsFormationLapFull(string trackName, List<Vector3> carsCoords, string session)
        {
            // there is no information about type of formation lap (Full or Short) from telemetry
            // to get this info this method is going through all cars coordinates,
            // measure distance to a coordinates where first car in full formation lap should be,
            // finds the closest car and if distance is less than 20 units
            // that means there is a Full formation lap

            Vector3 current_track_coords;
            Vector3 car_coords;
            float closest_distance = 9999999.0f;
            TrackStartCoords trackStartCoords = new();

            if (carsCoords.Count == 0 || session != "Race")
            {
                return false;
            }

            var current_track = trackStartCoords.track_start_list.FirstOrDefault(track =>
                track.track_name.Equals(trackName));

            current_track_coords = new Vector3(
                current_track.track_start.X,
                current_track.track_start.Y,
                current_track.track_start.Z);

            for (int i = 0; i < carsCoords.Count; i++)
            {
                Vector3 car = carsCoords[i];

                float distance = Vector3.Distance(car, current_track_coords);

                if (distance < closest_distance)
                {
                    closest_distance = distance;
                }
            }

            if (closest_distance < 20.0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateRecentSessions(UpdateFromTelemetry updateFromTelemetry)
        {
            // method that is gathering all telemetry data

            if (updateFromTelemetry != null)
            {
                sim_data = updateFromTelemetry.GetNewData();
            }
            else
            {
                button_auto.Enabled = false;
                button_import_race.Enabled = false;
                return;
            }

            if (this.InvokeRequired)
            {
                this.Invoke(new System.Windows.Forms.MethodInvoker(delegate
                {
                    UpdateRecentSessions(updateFromTelemetry);
                }));
                return;
            }

            if (previous_lap == -1)
            {
                // if completed laps counter is in default -1 state, check completed laps
                // necessary if app was started during session where some laps were already done

                previous_lap = sim_data.completed_laps;
            }

            int last_lap_time = updateFromTelemetry.GetLapTime();

            if (sim_data.completed_laps > previous_lap && sim_data.completed_laps != 0)
            {
                // create a lap struct

                Lap current_lap = new()
                {
                    completed_laps = sim_data.completed_laps,
                    lap_time = last_lap_time,
                    fuel = sim_data.fuel,
                    session_type = sim_data.session_type,
                    track_name = sim_data.track_name,
                    car_name = sim_data.car_name,
                    fuel_at_the_start = (int)(sim_data.fuel_now + sim_data.fuel_used + 0.1),
                    invalid = invalid_lap,
                    used = true,
                    outlier = false
                };

                if (ignoreInvalidLapsToolStripMenuItem.Checked && current_lap.invalid)
                {
                    // if 'ignored invalid laps' checked then set this lap to not be used for stint

                    current_lap.used = false;
                }

                if (current_lap.lap_time != 0 && previous_lap != sim_data.completed_laps)
                {
                    // if this is a new lap then add it to the list

                    laps_data.Add(current_lap);
                }

                previous_lap = sim_data.completed_laps;
            }

            invalid_lap = sim_data.invalid_lap;

            if (sim_data.session_type == "Race")
            {
                // enable buttons when session is Race

                button_import_race.Enabled = true;
                button_auto.Enabled = true;
            }
            else
            {
                // disable buttons when session isnt Race

                button_import_race.Enabled = false;
                button_auto.Enabled = false;
            }

            if (sim_data.game_status == GameStatus.Off)
            {
                // if user quits to a main menu and there are some laps recorded, then complete a stint
                // and add it with AddToListBox

                ConvertLapsIntoStintAndAddToListBox();
                previous_lap = -1;
                return;
            }

            if (session_saved != sim_data.session_type)
            {
                // if session type changes and there are some laps recorded, then complete a stint
                // and add it with AddToListBox

                ConvertLapsIntoStintAndAddToListBox();
                previous_lap = -1;
                session_saved = sim_data.session_type;
                return;
            }

            if (sim_data.session_type == "Hotstint" && sim_data.distance_traveled < 3000.0 ||
                sim_data.session_type == "Hotlap" && sim_data.distance_traveled < 3000.0)
            {
                // those 2 session works in a different way and reset into a position on track rather than pit lane

                ConvertLapsIntoStintAndAddToListBox();
                previous_lap = -1;
                return;
            }

            ConvertLapsIntoStintAndAddToListBox();
        }

        private void ConvertLapsIntoStintAndAddToListBox()
        {
            if (laps_data.Count == 0)
            {
                return;
            }

            if (sim_data.session_type == "Race" && sim_data.is_pitting == false)
            {
                // for race situation convert laps into stint when performing a pit stop

                return;
            }

            if (sim_data.is_in_pits == true ||
                ToolStripMenuItem_game_status.Text == " ACC OFF" ||
                sim_data.game_status == GameStatus.Off ||
                session_saved != sim_data.session_type ||
                sim_data.session_type == "Hotstint" && sim_data.distance_traveled < 1000.0 ||
                sim_data.session_type == "Hotlap" && sim_data.distance_traveled < 1000.0)
            {
                checkBox_lap_time.Enabled = true;

                // get average fuel per lap and average lap time

                double sum_fuel = 0.0;
                int sum_lap_times = 0;
                int laps_count = 0;
                double average_fuel;
                int average_lap_time;

                foreach (var lap in laps_data)
                {
                    if (lap.used == true)
                    {
                        sum_fuel += lap.fuel;
                        sum_lap_times += lap.lap_time;
                        laps_count++;
                    }
                }

                try
                {
                    // possible division by 0 if all lap.used == false

                    average_fuel = sum_fuel / laps_count;
                    average_lap_time = sum_lap_times / laps_count;
                }
                catch
                {
                    // ignore 'used' property

                    foreach (var lap in laps_data)
                    {
                        sum_fuel += lap.fuel;
                        sum_lap_times += lap.lap_time;
                        laps_count++;
                    }

                    average_fuel = sum_fuel / laps_count;
                    average_lap_time = sum_lap_times / laps_count;
                }

                // create a stint struct

                Stint recent_stint = new()
                {
                    average_fuel_per_lap = Math.Round(average_fuel, 2),
                    average_lap_time = average_lap_time,
                    stint_lenght = laps_data.Count,
                    track_name = laps_data[0].track_name,
                    car_name = laps_data[0].car_name,
                    session_type = laps_data[0].session_type,
                    date_time = DateTime.Now,
                    fuel_at_the_start = laps_data[0].fuel_at_the_start
                };

                // add to class

                StintData stint_data = new()
                {
                    Stint = recent_stint,
                    ListOfLaps = new List<Lap>(laps_data)
                };
                recent_stints.Add(stint_data);

                // clear recorded laps that were converted into a stint
                // clear listBox because new list is going to be added

                laps_data.Clear();
                listBoxMultiline_recent_sessions.Items.Clear();

                foreach (var stint in recent_stints)
                {
                    string to_listbox =
                        "Session: " + stint.Stint.session_type + " | " +
                        "Car: " + stint.Stint.car_name + " | " +
                        "Track: " + stint.Stint.track_name + " | " +
                        "Date: " + stint.Stint.date_time + "nextLine" +
                        " Laps: " + stint.Stint.stint_lenght + " | " +
                        "Avg lap time: " + LapTimeSecsFormatted(stint.Stint.average_lap_time) + " | " +
                        "Avg fuel per lap: " + Math.Round(stint.Stint.average_fuel_per_lap, 2).ToString().
                        Replace(",", ".") + " | " +
                        "Fuel at the start: " + stint.Stint.fuel_at_the_start + " L";

                    listBoxMultiline_recent_sessions.Items.Add(to_listbox);
                }
            }
        }

        private void ListBox_recent_sessions_SelectedIndexChanged(object sender, EventArgs e)
        {
            // enable and disable Import stint button, this button shouldnt be active unless
            // user select something from a listBox

            if (listBoxMultiline_recent_sessions.Items.Count > 0)
            {
                if (listBoxMultiline_recent_sessions.SelectedIndex != -1)
                {
                    button_import_stint.Enabled = true;
                    button_open_stint.Enabled = true;
                }
            }
            else
            {
                button_import_stint.Enabled = false;
                button_open_stint.Enabled = false;
            }
        }

        private void Button_import_stint_Click(object sender, EventArgs e)
        {
            // this button click is importing data from selected stint in a listBox

            import_stint_debouncer.Debouce(() =>
            {
                int selected_stint = listBoxMultiline_recent_sessions.SelectedIndex;

                comboBox_track.SelectedItem = recent_stints[selected_stint].Stint.track_name;
                ChangeControlColor(comboBox_track, Color.LightGreen);

                var selected_car = all_cars.Where(car => car.CarName == recent_stints[selected_stint].Stint.car_name);
                comboBox_class.SelectedItem = selected_car.FirstOrDefault().ClassName;
                ChangeControlColor(comboBox_class, Color.LightGreen);

                comboBox_car.SelectedItem = selected_car.FirstOrDefault().CarName;
                ChangeControlColor(comboBox_car, Color.LightGreen);

                textBox_fuel_per_lap.Text = recent_stints[selected_stint].Stint.average_fuel_per_lap.ToString().Replace(",", ".");
                ChangeControlColor(textBox_fuel_per_lap, Color.LightGreen);

                if (checkBox_lap_time.Checked)
                {
                    TimeSpan timeSpan = TimeSpan.FromMilliseconds(recent_stints[selected_stint].Stint.average_lap_time);
                    textBox_lap_time_min.Text = timeSpan.Minutes.ToString();
                    textBox_lap_time_sec.Text = $"{timeSpan.Seconds:00}.{timeSpan.Milliseconds:000}";
                    ChangeControlColor(textBox_lap_time_min, Color.LightGreen);
                    ChangeControlColor(textBox_lap_time_sec, Color.LightGreen);
                }
            });
        }

        private void Button_import_race_Click(object sender, EventArgs e)
        {
            // this button click is importing race information from the telemetry

            import_race_debouncer.Debouce(() =>
            {
                UpdateRecentSessions(updateFromTelemetry);

                int secs = (int)sim_data.race_duration / 1000;
                int hours = secs / 3600;
                textBox_race_h.Text = hours.ToString();
                textBox_race_min.Text = ((secs - hours * 3600) / 60).ToString();
                ChangeControlColor(textBox_race_h, Color.LightGreen);
                ChangeControlColor(textBox_race_min, Color.LightGreen);

                if (IsFormationLapFull(sim_data.track_name, sim_data.cars_coordinates, sim_data.session_type) == true)
                {
                    listBox_formation.SelectedIndex = 0;
                }
                else
                {
                    listBox_formation.SelectedIndex = 1;
                }
                ChangeControlColor(listBox_formation, Color.LightGreen);

                numericUpDown_pits.Value = sim_data.missing_pit_stops;
                ChangeControlColor(numericUpDown_pits, Color.LightGreen);

                if (sim_data.stint_time < sim_data.race_duration && sim_data.stint_time > 0)
                {
                    checkBox_max_stint.Checked = true;
                    textBox_max_stint.Text = (sim_data.stint_time / 60000).ToString();
                    ChangeControlColor(checkBox_max_stint, Color.LightGreen);
                    ChangeControlColor(textBox_max_stint, Color.LightGreen);
                }

                comboBox_track.SelectedItem = sim_data.track_name;
                ChangeControlColor(comboBox_track, Color.LightGreen);

                var selected_car = all_cars.Where(car => car.CarName == sim_data.car_name);
                comboBox_class.SelectedItem = selected_car.FirstOrDefault().ClassName;
                ChangeControlColor(comboBox_class, Color.LightGreen);

                comboBox_car.SelectedItem = selected_car.FirstOrDefault().CarName;
                ChangeControlColor(comboBox_car, Color.LightGreen);
            });
        }

        private void Button_save_load_Click(object sender, EventArgs e)
        {
            // when user clicks on save / load button a struct with current settings from
            // input panel is being created and used if user wants to save

            SavedStrategy current_strat = new()
            {
                saved_name = "",
                saved_car_class_index = comboBox_class.SelectedIndex,
                saved_car_index = comboBox_car.SelectedIndex,
                saved_track_index = comboBox_track.SelectedIndex,
                saved_race_h = textBox_race_h.Text,
                saved_race_min = textBox_race_min.Text,
                saved_lap_min = textBox_lap_time_min.Text,
                saved_lap_secs = textBox_lap_time_sec.Text,
                saved_fuel_per_lap = textBox_fuel_per_lap.Text,
                saved_formation_index = listBox_formation.SelectedIndex,
                saved_number_of_pits = (int)numericUpDown_pits.Value,
                saved_pit_stop_option_index = comboBox_pit_options.SelectedIndex,
                saved_checkbox_max_stint = checkBox_max_stint.Checked,
                saved_max_stint = textBox_max_stint.Text,
                saved_max_stint_enabled = textBox_max_stint.Enabled
            };

            // new form pops out for saving and loading

            SaveLoad save_load_form = new(current_strat);

            save_load_form.LoadButtonClicked += LoadStrat_LoadButtonClicked;

            try
            {
                save_load_form.ShowDialog();
            }
            catch
            {
                return;
            }
        }

        private void Button_auto_Click_1(object sender, EventArgs e)
        {
            // auto button is filling all input panel and perform calculations

            auto_debouncer.Debouce(() =>
            {
                if (sim_data.missing_pit_stops > 0)
                {
                    // if there are mandatory pit stops, new form pops out where user can select
                    // a desired pit stop option

                    Pit_option pit_option_form = new(sim_data.missing_pit_stops);

                    pit_option_form.ButtonClicked += PitOptionForm_ButtonClicked;

                    pit_option_form.ShowDialog();
                }
                ChangeControlColor(comboBox_pit_options, Color.LightGreen);

                int secs = (int)sim_data.race_duration / 1000;
                int hours = secs / 3600;
                textBox_race_h.Text = hours.ToString();
                textBox_race_min.Text = ((secs - hours * 3600) / 60).ToString();
                ChangeControlColor(textBox_race_h, Color.LightGreen);
                ChangeControlColor(textBox_race_min, Color.LightGreen);

                int formation_index;
                if (IsFormationLapFull(sim_data.track_name, sim_data.cars_coordinates, sim_data.session_type) == true)
                {
                    formation_index = 0;
                }
                else
                {
                    formation_index = 1;
                }
                listBox_formation.SelectedIndex = formation_index;
                ChangeControlColor(listBox_formation, Color.LightGreen);

                numericUpDown_pits.Value = sim_data.missing_pit_stops;
                ChangeControlColor(numericUpDown_pits, Color.LightGreen);

                if (sim_data.stint_time < sim_data.race_duration && sim_data.stint_time > 0)
                {
                    checkBox_max_stint.Checked = true;
                    textBox_max_stint.Enabled = true;
                    textBox_max_stint.Text = (sim_data.stint_time / 60000).ToString();
                    ChangeControlColor(checkBox_max_stint, Color.LightGreen);
                    ChangeControlColor(textBox_max_stint, Color.LightGreen);
                }

                if (listBoxMultiline_recent_sessions.Items.Count != 0)
                {
                    // if there are stints in listBox that are matching current car and track
                    // search for the longest stint and add use this data

                    List<Stint> filtered_stints = [];

                    foreach (var stint in recent_stints)
                    {
                        if (stint.Stint.track_name == sim_data.track_name && stint.Stint.car_name == sim_data.car_name)
                        {
                            filtered_stints.Add(stint.Stint);
                        }
                    }

                    if (filtered_stints.Count > 0)
                    {
                        Stint longest_stint = new();
                        int stint_lenght = 0;

                        foreach (var stint in filtered_stints)
                        {
                            if (stint.stint_lenght > stint_lenght)
                            {
                                longest_stint = stint;
                                stint_lenght = stint.stint_lenght;
                            }
                        }

                        comboBox_track.SelectedItem = longest_stint.track_name;
                        ChangeControlColor(comboBox_track, Color.LightGreen);

                        var selected_car2 = all_cars.Where(car => car.CarName == longest_stint.car_name);
                        comboBox_class.SelectedItem = selected_car2.FirstOrDefault().ClassName;
                        ChangeControlColor(comboBox_class, Color.LightGreen);

                        comboBox_car.SelectedItem = selected_car2.FirstOrDefault().CarName;
                        ChangeControlColor(comboBox_car, Color.LightGreen);

                        textBox_fuel_per_lap.Text = longest_stint.average_fuel_per_lap.ToString().Replace(",", ".");
                        ChangeControlColor(textBox_fuel_per_lap, Color.LightGreen);

                        if (checkBox_lap_time.Checked)
                        {
                            TimeSpan timeSpan = TimeSpan.FromMilliseconds(longest_stint.average_lap_time);
                            textBox_lap_time_min.Text = timeSpan.Minutes.ToString();
                            textBox_lap_time_sec.Text = $"{timeSpan.Seconds:00}.{timeSpan.Milliseconds:000}";
                            ChangeControlColor(textBox_lap_time_min, Color.LightGreen);
                            ChangeControlColor(textBox_lap_time_sec, Color.LightGreen);
                            ChangeControlColor(textBox_lap_time_min, Color.LightGreen);
                            ChangeControlColor(textBox_lap_time_sec, Color.LightGreen);
                        }
                    }

                    comboBox_track.SelectedItem = sim_data.track_name;
                    ChangeControlColor(comboBox_track, Color.LightGreen);

                    var selected_car = all_cars.Where(car => car.CarName == sim_data.car_name);
                    comboBox_class.SelectedItem = selected_car.FirstOrDefault().ClassName;
                    ChangeControlColor(comboBox_class, Color.LightGreen);

                    comboBox_car.SelectedItem = selected_car.FirstOrDefault().CarName;
                    ChangeControlColor(comboBox_car, Color.LightGreen);
                }
                else
                {
                    // if there are no matching car and track in a listBox (or if its empty)
                    // fill car, track from telemetry which automatically fill
                    // fuel per lap and lap time with last used data

                    comboBox_track.SelectedItem = sim_data.track_name;
                    ChangeControlColor(comboBox_track, Color.LightGreen);

                    var selected_car = all_cars.Where(car => car.CarName == sim_data.car_name);
                    comboBox_class.SelectedItem = selected_car.FirstOrDefault().ClassName;
                    ChangeControlColor(comboBox_class, Color.LightGreen);

                    comboBox_car.SelectedItem = selected_car.FirstOrDefault().CarName;
                    ChangeControlColor(comboBox_car, Color.LightGreen);
                }

                button_calculate.PerformClick();
            });
        }

        private void PitOptionForm_ButtonClicked(object sender, int pitOption)
        {
            // select pit option based on what user selects in Pit.Form

            comboBox_pit_options.SelectedIndex = pitOption;
        }

        private void NewEditedStint_ButtonClicked(object sender, FuelStrat.StintData outStint)
        {
            // Load stint edited in Stint.Form and replace the old one

            recent_stints[listBoxMultiline_recent_sessions.SelectedIndex] = outStint;
            int listBox_index = listBoxMultiline_recent_sessions.SelectedIndex;
            listBoxMultiline_recent_sessions.Items.Clear();

            foreach (var stint in recent_stints)
            {
                string to_listbox =
                    "Session: " + stint.Stint.session_type + " | " +
                    "Car: " + stint.Stint.car_name + " | " +
                    "Track: " + stint.Stint.track_name + " | " +
                    "Date: " + stint.Stint.date_time + "nextLine" +
                    " Laps: " + stint.Stint.stint_lenght + " | " +
                    "Avg lap time: " + LapTimeSecsFormatted(stint.Stint.average_lap_time) + " | " +
                    "Fuel at the start: " + stint.Stint.fuel_at_the_start + " L | " +
                    "Avg fuel per lap: " + Math.Round(stint.Stint.average_fuel_per_lap, 2).ToString().
                    Replace(",", ".");

                listBoxMultiline_recent_sessions.Items.Add(to_listbox);
                listBoxMultiline_recent_sessions.SelectedIndex = listBox_index;
            }
        }

        private void LoadStrat_LoadButtonClicked(object sender, int slot)
        {
            // fill all controls in Input panel with selected save to load

            try
            {
                string saved_json = File.ReadAllText(Path.Combine(documents_path, "FuelStrat_saved_strats.json"));
                List<SavedStrategy> saved_strat_list =
                JsonConvert.DeserializeObject<List<FuelStrat.SavedStrategy>>(saved_json);

                SavedStrategy strat_to_load = saved_strat_list[slot];

                comboBox_class.SelectedIndex = strat_to_load.saved_car_class_index;
                comboBox_car.SelectedIndex = strat_to_load.saved_car_index;
                comboBox_track.SelectedIndex = strat_to_load.saved_track_index;
                textBox_race_h.Text = strat_to_load.saved_race_h;
                textBox_race_min.Text = strat_to_load.saved_race_min;
                textBox_lap_time_min.Text = strat_to_load.saved_lap_min;
                textBox_lap_time_sec.Text = strat_to_load.saved_lap_secs;
                textBox_fuel_per_lap.Text = strat_to_load.saved_fuel_per_lap;
                listBox_formation.SelectedIndex = strat_to_load.saved_formation_index;
                numericUpDown_pits.Value = strat_to_load.saved_number_of_pits;
                comboBox_pit_options.SelectedIndex = strat_to_load.saved_pit_stop_option_index;
                checkBox_max_stint.Checked = strat_to_load.saved_checkbox_max_stint;
                textBox_max_stint.Text = strat_to_load.saved_max_stint;
                textBox_max_stint.Enabled = strat_to_load.saved_max_stint_enabled;
            }
            catch (Exception ex)
            {
                // catch exception when data file is corrupted or unreadable

                MessageBox.Show("Error reading FuelStrat_saved_strats.json:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                DialogResult result = MessageBox.Show("Would you like to reset the " +
                    "FuelStrat_saved_strats.json?\n\nChoosing 'Yes' will delete all saved strategies.\n\n" +
                    "Choosing 'No' will abort current load.",
                    "Reset data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    File.Delete(Path.Combine(documents_path, "FuelStrat_saved_strats.json"));
                    List<FuelStrat.SavedStrategy> default_saved_strat_list;

                    Assembly assembly = Assembly.GetExecutingAssembly();
                    using (Stream stream = assembly.GetManifestResourceStream("FuelStrat.json_resources.FuelStrat_saved_strats.json"))
                    using (StreamReader reader = new(stream))
                    {
                        string defualt_saved = reader.ReadToEnd();
                        default_saved_strat_list = JsonConvert.DeserializeObject<List<FuelStrat.SavedStrategy>>(defualt_saved);
                    }
                    string default_save_json = JsonConvert.SerializeObject(default_saved_strat_list, Formatting.Indented);
                    File.WriteAllText(Path.Combine(documents_path, "FuelStrat_saved_strats.json"), default_save_json);
                }
                else if (result == DialogResult.No)
                {
                    return;
                }
            }
        }

        private void IgnoreInvalidLaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // change a checked status for 'Ignore invalid laps' in menu

            if (ignoreInvalidLapsToolStripMenuItem.Checked)
            {
                ignoreInvalidLapsToolStripMenuItem.Checked = false;
            }
            else
            {
                ignoreInvalidLapsToolStripMenuItem.Checked = true;
            }
        }

        private void TelemetryDisabledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // change a checked status for 'Telemetry disabled' in menu

            if (telemetryDisabledToolStripMenuItem.Checked)
            {
                telemetryDisabledToolStripMenuItem.Checked = false;
            }
            else
            {
                telemetryDisabledToolStripMenuItem.Checked = true;
            }
        }

        private void button_open_stint_Click(object sender, EventArgs e)
        {
            // open selected stint form listBox and pass it to the Stint.Form

            int selected_stint_index = listBoxMultiline_recent_sessions.SelectedIndex;
            StintData selected_stint = recent_stints[selected_stint_index];

            StintForm stint_form = new(selected_stint);
            stint_form.ButtonClicked += NewEditedStint_ButtonClicked;
            stint_form.ShowDialog();
        }

        private void FuelStrat_FormClosing(object sender, FormClosingEventArgs e)
        {
            // create a last state of the app and save it into a json file

            SavedStrategy controls = new()
            {
                saved_name = "",
                saved_car_class_index = comboBox_class.SelectedIndex,
                saved_car_index = comboBox_car.SelectedIndex,
                saved_track_index = comboBox_track.SelectedIndex,
                saved_race_h = textBox_race_h.Text,
                saved_race_min = textBox_race_min.Text,
                saved_lap_min = textBox_lap_time_min.Text,
                saved_lap_secs = textBox_lap_time_sec.Text,
                saved_fuel_per_lap = textBox_fuel_per_lap.Text,
                saved_formation_index = listBox_formation.SelectedIndex,
                saved_number_of_pits = (int)numericUpDown_pits.Value,
                saved_pit_stop_option_index = comboBox_pit_options.SelectedIndex,
                saved_checkbox_max_stint = checkBox_max_stint.Checked,
                saved_max_stint = textBox_max_stint.Text,
                saved_max_stint_enabled = textBox_max_stint.Enabled
            };

            List<StintData> stints = new();

            foreach (var stint in recent_stints)
            {
                stints.Add(stint);
            }

            LastStateOfTheApp last_state = new(controls, stints);

            string new_json = JsonConvert.SerializeObject(last_state, Formatting.Indented);
            File.WriteAllText(Path.Combine(documents_path, "FuelStrat_last_state.json"), new_json);
        }
    }
}