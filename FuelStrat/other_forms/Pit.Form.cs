using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FuelStrat
{
    public partial class Pit_option : Form
    {

        public delegate void ButtonClickedEventHandler (object sender, int pitOption);

        public event ButtonClickedEventHandler ButtonClicked;

        public Pit_option(int pitNumber)
        {
            InitializeComponent();
            LoadPitsNumber(pitNumber);
        }

        public void LoadPitsNumber(int pit_number)
        {
            if (pit_number == 1) 
            {
                label_pit_option1.Text = "In this race there is 1 mandatory pit stop.";
            }
            else
            {
                label_pit_option1.Text = "In this race there are " + pit_number + " mandatory pit stops.";
            }
        }

        private void button_Refuel_Tires_Click(object sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, 0);
            this.Close();
        }

        private void button_tires_Click(object sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, 2);
            this.Close();
        }

        private void button_refuel_Click(object sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, 3);
            this.Close();
        }

        private void button_1L_refuel_Click(object sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, 4);
            this.Close();
        }

        private void button_fixed_refuel_Click(object sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, 1);
            this.Close();
        }

    }
}
