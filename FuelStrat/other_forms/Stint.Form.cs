using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FuelStrat
{
    public partial class StintForm : Form
    {
        FuelStrat.StintData stint = new();

        CustomListBox customListBox = new();

        int button_invalid_count = 0;
        int button_outliers_count = 0;

        public class CustomListBox : ListBox
        {
            private int avgLapTime;

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
                        string[] textParts = textToSplit.Split(new string[] { "time:", " Fuel:" }, StringSplitOptions.None);
                        if (int.TryParse(textParts[1], out int lapTime))
                        {
                            if (lapTime < bestLapTime)
                            {
                                bestLapTime = lapTime;
                            }
                        }
                    }
                }

                var item = this.Items[e.Index].ToString();
                string[] textPartsItem = item.Split(new string[] { "time:", " Fuel:" }, StringSplitOptions.None);
                int lapTimeItem = int.Parse(textPartsItem[1]);

                Color textColor;
                if (item.Contains("Invalid:True"))
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

        public StintForm(FuelStrat.StintData selected_stint)
        {
            InitializeComponent();
            this.stint = selected_stint;
            IsLapOutlier(stint);

            customListBox.Location = new Point(15, 45);
            customListBox.Size = new Size(440, 200);
            customListBox.ScrollAlwaysVisible = true;
            customListBox.SelectedIndex = -1;
            customListBox.SelectedIndexChanged += customListBox_selected_index_changed;
            this.Controls.Add(customListBox);

            foreach (var lap in stint.ListOfLaps)
            {
                customListBox.Items.Add("Lap:" + lap.completed_laps + " Lap time:" + lap.lap_time + " Fuel:" + lap.fuel +
                    " Invalid:" + lap.invalid + "    Used:" + lap.used + " Outlier:" + lap.outlier);
            }
        }

        public void customListBox_selected_index_changed(object sender, EventArgs e)
        {
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
                customListBox.Items.Add("Lap:" + lap.completed_laps + " Lap time:" + lap.lap_time + " Fuel:" + lap.fuel +
                    " Invalid:" + lap.invalid + "    Used:" + lap.used + " Outlier:" + lap.outlier);
            }
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_ignore_invalid_Click(object sender, EventArgs e)
        {
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
                customListBox.Items.Add("Lap:" + lap.completed_laps + " Lap time:" + lap.lap_time + " Fuel:" + lap.fuel +
                    " Invalid:" + lap.invalid + "    Used:" + lap.used + " Outlier:" + lap.outlier);
            }

            button_invalid_count++;
        }

        private void button_ignore_outliers_Click(object sender, EventArgs e)
        {
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
                customListBox.Items.Add("Lap:" + lap.completed_laps + " Lap time:" + lap.lap_time + " Fuel:" + lap.fuel +
                    " Invalid:" + lap.invalid + "    Used:" + lap.used + " Outlier:" + lap.outlier);
            }

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
                if (avg_lap_time / lap.lap_time < 0.95)
                {
                    lap.outlier = true;
                    stint.ListOfLaps[i] = lap;
                }
            }
        }
    }
}
