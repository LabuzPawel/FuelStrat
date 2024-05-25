namespace FuelStrat
{
    public partial class StintForm : Form
    {
        public delegate void ButtonClickedEventHandler(object sender, FuelStrat.StintData outStint);

        public event ButtonClickedEventHandler ButtonClicked;

        FuelStrat.StintData stint = new();
        FuelStrat.StintData stint_copy = new();

        CustomListBox customListBox = new();

        int button_invalid_count = 0;
        int button_outliers_count = 0;

        public class CustomListBox : ListBox
        {
            // this is custom version of a ListBox that will change color of items

            public CustomListBox()
            {
                this.DrawMode = DrawMode.OwnerDrawFixed;
                this.MeasureItem += CustomListBox_MeasureItem;
            }

            private void CustomListBox_MeasureItem(object sender, MeasureItemEventArgs e)
            {
                e.ItemHeight = 20;
            }

            protected override void OnDrawItem(DrawItemEventArgs e)
            {
                base.OnDrawItem(e);

                int bestLapTime = 99999999;

                if (e.Index < 0)
                {
                    return;
                }

                if (this.Items.Count > 0)
                {
                    foreach (var i in this.Items)
                    {
                        string textToSplit = i.ToString();
                        string[] textParts = textToSplit.Split(new string[] { "time: ", " | Fuel" }, StringSplitOptions.None);
                        if (int.TryParse(textParts[1], out int lapTime))
                        {
                            if (lapTime < bestLapTime && i.ToString().Contains("Invalid: False"))
                            {
                                bestLapTime = lapTime;
                            }
                        }
                    }
                }

                var item = this.Items[e.Index].ToString();
                string[] textPartsItem = item.Split(new string[] { "time: ", " | Fuel" }, StringSplitOptions.None);
                int lapTimeItem = int.Parse(textPartsItem[1]);

                Color textColor;
                if (item.Contains("Invalid: True"))
                {
                    textColor = Color.DarkOrange;

                    if (item.Contains("Used:False"))
                    {
                        textColor = Color.Red;
                    }
                }
                else if (item.Contains("Outlier:True"))
                {
                    textColor = Color.Chocolate;

                    if (item.Contains("Used:False"))
                    {
                        textColor = Color.Red;
                    }
                }
                else if (item.Contains("Used:False"))
                {
                    textColor = Color.Red;
                }
                else if (lapTimeItem == bestLapTime)
                {
                    textColor = Color.Magenta;
                }
                else
                {
                    textColor = Color.Black;
                }

                TimeSpan lapTimeSpan = TimeSpan.FromMilliseconds(lapTimeItem);
                string laptimeFormatted = lapTimeSpan.ToString(@"m\:ss\.fff");

                string displayText = item.Replace(textPartsItem[1], laptimeFormatted);

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    e.Graphics.FillRectangle(Brushes.White, e.Bounds);
                }
                else
                {
                    e.DrawBackground();
                }

                using (Brush textBrush = new SolidBrush(textColor))
                {
                    e.Graphics.DrawString(displayText, e.Font, textBrush, e.Bounds);
                }

                e.DrawFocusRectangle();
            }
        }

        public StintForm(FuelStrat.StintData selectedStint)
        {
            InitializeComponent();
            this.stint = selectedStint;
            this.stint_copy = new(stint);   // a copy for revert button

            IsLapOutlier(stint);

            customListBox.Location = new Point(15, 75);
            customListBox.Size = new Size(520, 200);
            customListBox.ScrollAlwaysVisible = true;
            customListBox.SelectedIndex = -1;
            customListBox.SelectedIndexChanged += customListBox_selected_index_changed;
            this.Controls.Add(customListBox);

            foreach (var lap in stint.ListOfLaps)
            {
                customListBox.Items.Add("Lap: " + lap.completed_laps + " | Lap time: " + lap.lap_time +
                    " | Fuel: " + lap.fuel.ToString().Replace(",", ".") +
                    " | Invalid: " + lap.invalid + "    Used:" + lap.used + " Outlier:" + lap.outlier);
            }

            RecalculateStint(stint);
        }

        public void customListBox_selected_index_changed(object sender, EventArgs e)
        {
            // when clicking on an item, toggle 'used' property that indicates if this lap should be
            // taken into stint calculation

            if (customListBox.SelectedIndex == -1)
            {
                return;
            }
            if (customListBox.Text.Contains("Used:True"))
            {
                customListBox.Text.Replace("Used:True", "Used:False");
                FuelStrat.Lap lap_used = stint.ListOfLaps[customListBox.SelectedIndex];
                lap_used.used = false;
                stint.ListOfLaps[customListBox.SelectedIndex] = lap_used;
            }
            else
            {
                customListBox.Text.Replace("Used:False", "Used:True");
                FuelStrat.Lap lap_used = stint.ListOfLaps[customListBox.SelectedIndex];
                lap_used.used = true;
                stint.ListOfLaps[customListBox.SelectedIndex] = lap_used;
            }

            customListBox.Items.Clear();
            foreach (var lap in stint.ListOfLaps)
            {
                customListBox.Items.Add("Lap: " + lap.completed_laps + " | Lap time: " + lap.lap_time +
                    " | Fuel: " + lap.fuel.ToString().Replace(",", ".") +
                    " | Invalid: " + lap.invalid + "    Used:" + lap.used + " Outlier:" + lap.outlier);
            }

            RecalculateStint(stint);
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            RecalculateStint(stint);

            ButtonClicked?.Invoke(this, stint);
            this.Close();
        }

        private void RecalculateStint(FuelStrat.StintData stint)
        {
            // recalculation of stint when one or more laps changed 'used' property

            double sum_fuel = 0.0;
            int sum_lap_times = 0;
            int laps_count = 0;
            double average_fuel;
            int average_lap_time;

            foreach (var lap in stint.ListOfLaps)
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

                foreach (var lap in stint.ListOfLaps)
                {
                    sum_fuel += lap.fuel;
                    sum_lap_times += lap.lap_time;
                    laps_count++;
                }

                average_fuel = sum_fuel / laps_count;
                average_lap_time = sum_lap_times / laps_count;
            }

            stint.Stint.average_lap_time = average_lap_time;
            stint.Stint.average_fuel_per_lap = Math.Round(average_fuel, 2);

            label_avg_lap.Text = "Avg lap time: " + FuelStrat.LapTimeSecsFormatted(average_lap_time);
            label_avg_fuel.Text = "Avg fuel per lap: " + Math.Round(average_fuel, 2).ToString().Replace(",", ".");
        }

        private void button_ignore_invalid_Click(object sender, EventArgs e)
        {
            // this button works as toggle on/off
            // if lap is invalid then change its 'used' property

            if (button_invalid_count % 2 != 0 && button_invalid_count != 0)
            {
                for (int i = 0; i < stint.ListOfLaps.Count; i++)
                {
                    if (stint.ListOfLaps[i].invalid)
                    {
                        FuelStrat.Lap lap = stint.ListOfLaps[i];
                        lap.used = true;
                        stint.ListOfLaps[i] = lap;
                    }
                }
            }
            else
            {
                for (int i = 0; i < stint.ListOfLaps.Count; i++)
                {
                    if (stint.ListOfLaps[i].invalid)
                    {
                        FuelStrat.Lap lap = stint.ListOfLaps[i];
                        lap.used = false;
                        stint.ListOfLaps[i] = lap;
                    }
                }
            }

            customListBox.Items.Clear();
            foreach (var lap in stint.ListOfLaps)
            {
                customListBox.Items.Add("Lap: " + lap.completed_laps + " | Lap time: " + lap.lap_time +
                    " | Fuel: " + lap.fuel.ToString().Replace(",", ".") +
                    " | Invalid: " + lap.invalid + "    Used:" + lap.used + " Outlier:" + lap.outlier);
            }

            RecalculateStint(stint);

            button_invalid_count++;
        }

        private void button_ignore_outliers_Click(object sender, EventArgs e)
        {
            // this button works as toggle on/off
            // if lap is an outlier (more than 105% of avg lap time) then change its 'used' property

            if (button_outliers_count % 2 != 0 && button_outliers_count != 0)
            {
                for (int i = 0; i < stint.ListOfLaps.Count; i++)
                {
                    if (stint.ListOfLaps[i].outlier)
                    {
                        FuelStrat.Lap lap = stint.ListOfLaps[i];
                        lap.used = true;
                        stint.ListOfLaps[i] = lap;
                    }
                }
            }
            else
            {
                for (int i = 0; i < stint.ListOfLaps.Count; i++)
                {
                    if (stint.ListOfLaps[i].outlier)
                    {
                        FuelStrat.Lap lap = stint.ListOfLaps[i];
                        lap.used = false;
                        stint.ListOfLaps[i] = lap;
                    }
                }
            }

            customListBox.Items.Clear();
            foreach (var lap in stint.ListOfLaps)
            {
                customListBox.Items.Add("Lap: " + lap.completed_laps + " | Lap time: " + lap.lap_time +
                    " | Fuel: " + lap.fuel.ToString().Replace(",", ".") +
                    " | Invalid: " + lap.invalid + "    Used:" + lap.used + " Outlier:" + lap.outlier);
            }

            RecalculateStint(stint);

            button_outliers_count++;
        }

        private void IsLapOutlier(FuelStrat.StintData stint)
        {
            int sum_lap_times = 0;

            foreach (var lap in stint.ListOfLaps)
            {
                sum_lap_times += lap.lap_time;
            }

            double avg_lap_time = sum_lap_times / stint.ListOfLaps.Count;

            for (int i = 0; i < stint.ListOfLaps.Count; i++)
            {
                var lap = stint.ListOfLaps[i];
                if (lap.lap_time / avg_lap_time > 1.05)
                {
                    lap.outlier = true;
                    stint.ListOfLaps[i] = lap;
                }
            }
        }

        private void button_revert_Click(object sender, EventArgs e)
        {
            // reverts all changes made after opening this form

            stint = new(stint_copy);
            IsLapOutlier(stint);
            RecalculateStint(stint);

            customListBox.Items.Clear();
            foreach (var lap in stint.ListOfLaps)
            {
                customListBox.Items.Add("Lap: " + lap.completed_laps + " | Lap time: " + lap.lap_time +
                    " | Fuel: " + lap.fuel.ToString().Replace(",", ".") +
                    " | Invalid: " + lap.invalid + "    Used:" + lap.used + " Outlier:" + lap.outlier);
            }
        }
    }
}