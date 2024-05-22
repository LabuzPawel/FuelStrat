using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FuelStrat
{
    public partial class StintForm : Form
    {
        FuelStrat.StintData stint = new();

        CustomListBox customListBox = new();

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

                if (e.Index < 0)
                {
                    return;
                }

                if (this.Items.Count > 0)
                {
                    int sumLapTime = 0;
                    foreach (var i in this.Items)
                    {
                        string textToSplit = i.ToString();
                        string[] textParts = textToSplit.Split(new string[] { "time:", " Fuel:" }, StringSplitOptions.None);
                        if (int.TryParse(textParts[1], out int lapTime))
                        {
                            sumLapTime += lapTime;
                        }
                    }
                    avgLapTime = sumLapTime / this.Items.Count;
                }

                var item = this.Items[e.Index].ToString();
                string[] textPartsItem = item.Split(new string[] { "time:", " Fuel:" }, StringSplitOptions.None);
                int lapTimeItem = int.Parse(textPartsItem[1]);

                Color textColor;
                if (item.Contains("Invalid:True"))
                {
                    textColor = Color.Orange;

                    if (item.Contains("Used:False"))
                    {
                        textColor = Color.Red;
                    }
                }
                else if ((double)avgLapTime / lapTimeItem < 0.95)
                {
                    textColor = Color.Yellow;

                    if (item.Contains("Used:False"))
                    {
                        textColor = Color.Red;
                    }
                }
                else if (item.Contains("Used:False"))
                {
                    textColor = Color.Red;
                }
                else
                {
                    textColor = Color.Black;
                }

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
                    e.Graphics.DrawString(item, e.Font, textBrush, e.Bounds);
                }

                e.DrawFocusRectangle();
            }
        }

        public StintForm(FuelStrat.StintData selected_stint)
        {
            InitializeComponent();
            this.stint = selected_stint;

            customListBox.Location = new Point(100, 100);
            customListBox.Size = new Size(500, 300);
            customListBox.ScrollAlwaysVisible = true;
            customListBox.SelectedIndexChanged += customListBox_selected_index_changed;
            this.Controls.Add(customListBox);

            foreach (var lap in stint.ListOfLaps)
            {
                customListBox.Items.Add("Lap:" + lap.completed_laps + " Lap time:" + lap.lap_time + " Fuel:" + lap.fuel + 
                    " Invalid:" + lap.invalid + " Used:" + lap.used);
            }
        }
        
        public void customListBox_selected_index_changed(object sender, EventArgs e)
        {
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
                    " Invalid:" + lap.invalid + " Used:" + lap.used);
            }
        }
    }
}
