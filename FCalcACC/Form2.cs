namespace FCalcACC
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            string help_text =
                "This application was created to help with calculating fuel and pit strategy for Assetto Corsa Competizione (ACC).\r\n\r\n" +
                "ACC is a racing simulation where races are set up with certain duration and exact number of laps will change depending on many variables that this application takes into account.\r\n\r\n" +
                "Additional file \"CARS.json\" and \"TRACKS.json\" holds important data for this app to work. \"TRACKS.json\" is also a file where some of the data will be saved for future use after pressing \"Calculate\" button. \r\n" +
                "This includes \"Fuel per lap\" for every given track and car combination and track's lap time.\r\n" +
                "Every track also have unique time that is needed for full pit stop (tires change and refuel).\r\n\r\n" +
                "INPUT DATA\r\n\r\n" +
                "Car and track are not required for calculation.\r\n\r\n" +
                "Car class has to be selected to get a list of cars in next box.\r\n" +
                "Car needs to be selected to store a fuel per lap data.\r\n\r\n" +
                "If no track is being selected then time lost during pit stop is set to default. Default pit stop time is set to 57 seconds which is roughly a mediana from all tracks. Selecting track will give more accurate results.\r\n\r\n" +
                "Variables section is required for calculation. Formation lap is set to \"Full\" by default. Lap time should be an average time.\r\n\r\n" +
                "Pit stop section is optional. If no pit option is selected and number of pits is more than 0, then tires + refuel is selected as default.\r\n\r\n" +
                "RESULTS\r\n\r\n" +
                "Row \"Lap time for +1 lap\" shows lap time that is needed for the race to have one more lap. If this threshold is reached during the race, there will be one more lap compared to lap time set in Variables section (row above).\r\n" +
                "Row \"Lap time for -1 lap\" shows the opposite situation with race shorter by one lap.\r\n\r\n" +
                "Pit stop panel will show a recommended pit stop strategy for the race, considering the selected pit stop option and the number of pit stops.";
            richTextBox_help.Text = help_text;
        }

        private void button_close_help_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}