using System.Windows.Forms;
using static FuelStrat.FuelStrat;

namespace FuelStrat.Tests
{
    [TestClass()]
    public class FuelStrattests
    {
        private FuelStrat? form;

        [TestInitialize]
        public void TestInitialize()
        {
            form = new FuelStrat();
        }

        [TestMethod("Car object from CARS.json")]
        public void LoadCarObjectList_LoadCarJson_ReturnsListOfCarObjects()
        {
            form.LoadCarTrackObjects();
            Assert.IsTrue(form.all_cars[0] is Car);
        }

        [TestMethod("Track object from TRACKS.json")]
        public void LoadTrackObjectList_LoadTrackJson_ReturnsListOfTrackObjects()
        {
            form.LoadCarTrackObjects();
            Assert.IsTrue(form.all_tracks[0] is Track);
            Assert.IsTrue(form.all_tracks[0].CarTrackFuel[0] is CarTrackFuel);
        }

        [TestMethod("Load car classes into comboBox")]
        public void LoadCarClasses_AddUniqueCarClasses_ComboBoxCorrectlyPopulated()
        {
            ComboBox comboBox_test = new();
            form.LoadCarTrackObjects();

            form.LoadCarClasses(comboBox_test);

            Assert.AreEqual(form.car_classes.Count, comboBox_test.Items.Count);
            foreach (var car_class in form.car_classes)
            {
                Assert.IsTrue(comboBox_test.Items.Contains(car_class));
            }
        }

        [TestMethod("Load cars based on selected car class")]
        public void LoadCars_AddCars_ComboBoxCorrectlyPopulated()
        {
            ComboBox comboBox_test_car = new();
            form.LoadCarTrackObjects();

            form.LoadCars(comboBox_test_car, "GT3");
            var gt3_cars = form.all_cars.Where(car => car.ClassName.Contains("GT3"));
            Assert.AreEqual(gt3_cars.Count(), comboBox_test_car.Items.Count);
            Assert.AreEqual(comboBox_test_car.Items[0], "Aston Martin V12 Vantage GT3 2013");

            form.LoadCars(comboBox_test_car, "GT4");
            var gt4_cars = form.all_cars.Where(car => car.ClassName.Contains("GT4"));
            Assert.AreEqual(gt4_cars.Count(), comboBox_test_car.Items.Count);
            Assert.AreEqual(comboBox_test_car.Items[0], "Alpine A110 GT4 2018");

            form.LoadCars(comboBox_test_car, "GT2");
            var gt2_cars = form.all_cars.Where(car => car.ClassName.Contains("GT2"));
            Assert.AreEqual(gt2_cars.Count(), comboBox_test_car.Items.Count);
            Assert.AreEqual(comboBox_test_car.Items[0], "Audi R8 LMS GT2 2021");

            form.LoadCars(comboBox_test_car, "GTC");
            var gtc_cars = form.all_cars.Where(car => car.ClassName.Contains("GTC"));
            Assert.AreEqual(gtc_cars.Count(), comboBox_test_car.Items.Count);
            Assert.AreEqual(comboBox_test_car.Items[0], "Ferrari 488 Challenge Evo 2020");

            form.LoadCars(comboBox_test_car, "TCX");
            var tcx_cars = form.all_cars.Where(car => car.ClassName.Contains("TCX"));
            Assert.AreEqual(tcx_cars.Count(), comboBox_test_car.Items.Count);
            Assert.AreEqual(comboBox_test_car.Items[0], "BMW M2 CS 2020");
        }

        [TestMethod("Load tracks into comboBox")]
        public void LoadTracks_AddTracks_ComboBoxCorrectlyPopulated()
        {
            ComboBox comboBox_test = new();
            form.LoadCarTrackObjects();
            form.LoadTracks(comboBox_test);

            Assert.AreEqual(form.all_tracks.Count, comboBox_test.Items.Count);
            Assert.AreEqual(comboBox_test.Items[0], "Barcelona");
        }

        [TestMethod("Load pit options into comboBox")]
        public void LoadPitOptions_AddPitOptions_ComboBoxCorrectlyPopulated()
        {
            ComboBox comboBox_test = new();
            LoadPitOptions(comboBox_test, form.PIT_OPTIONS);

            Assert.AreEqual(form.PIT_OPTIONS.Count, comboBox_test.Items.Count);
            foreach (var option in form.PIT_OPTIONS)
            {
                Assert.IsTrue(comboBox_test.Items.Contains(option));
            }
        }

        [TestMethod("Time lost in pits (No Track, 2 pits)")]
        public void CalculateTimeLostInPits_NoTrack2Pits()
        {
            ComboBox comboBoxPitOptionsTest = new();
            LoadPitOptions(comboBoxPitOptionsTest, form.PIT_OPTIONS);

            for (int i = 0; i < 5; i++)
            {
                comboBoxPitOptionsTest.SelectedIndex = i;

                form.CalculateTimeLostInPits(2, comboBoxPitOptionsTest, "TRACK");

                if (comboBoxPitOptionsTest.Text == "Tires only")
                {
                    Assert.AreEqual(form.time_lost_in_pits, form.DEFAULT_TIME_IN_PITS * 2);
                }
                else if (comboBoxPitOptionsTest.Text == "Fixed refuel only")
                {
                    Assert.AreEqual(form.time_lost_in_pits, (form.DEFAULT_TIME_IN_PITS - 5) * 2);
                }
                else if (comboBoxPitOptionsTest.Text == "1L refuel")
                {
                    Assert.AreEqual(form.time_lost_in_pits, (form.DEFAULT_TIME_IN_PITS - 26.4) * 2);
                }
                else if (comboBoxPitOptionsTest.Text == "Refuel + tires")
                {
                    Assert.AreEqual(form.time_lost_in_pits, form.DEFAULT_TIME_IN_PITS * 2);
                }
            }
        }

        [TestMethod("Time lost in pits (Selected Track, 2 pits)")]
        public void CalculateTimeLostInPits_SelectedTrack2Pits()
        {
            ComboBox comboBoxPitOptionsTest = new();
            LoadPitOptions(comboBoxPitOptionsTest, form.PIT_OPTIONS);
            form.LoadCarTrackObjects();

            for (int i = 0; i < 5; i++)
            {
                comboBoxPitOptionsTest.SelectedIndex = i;

                form.CalculateTimeLostInPits(2, comboBoxPitOptionsTest, "Barcelona");

                if (comboBoxPitOptionsTest.Text == "Tires only")
                {
                    Assert.AreEqual(form.time_lost_in_pits, form.time_in_pits * 2);
                }
                else if (comboBoxPitOptionsTest.Text == "Fixed refuel only")
                {
                    Assert.AreEqual(form.time_lost_in_pits, (form.time_in_pits - 5) * 2);
                }
                else if (comboBoxPitOptionsTest.Text == "1L refuel")
                {
                    Assert.AreEqual(form.time_lost_in_pits, (form.time_in_pits - 26.4) * 2);
                }
                else if (comboBoxPitOptionsTest.Text == "Refuel + tires")
                {
                    Assert.AreEqual(form.time_lost_in_pits, form.time_in_pits * 2);
                }
            }
        }

        [TestMethod("Calculating time lost in pits with only refuel #1 (No track selected, 1 pit)")]
        public void RefuelTimeLost_SampleData1()
        {
            NumericUpDown numericUpDownPitsTest = new() { Value = 1 };
            form.time_lost_in_pits = 0;
            form.LoadCarTrackObjects();
            form.fuel_for_race_round_up = 104;
            form.fuel_per_lap = 4;
            form.formation_lap_fuel = 4;
            form.number_of_laps = 25;

            form.RefuelTimeLost("TRACK", numericUpDownPitsTest);

            Assert.AreEqual(Math.Round(form.time_lost_in_pits, 2), 40.4);
        }

        [TestMethod("Calculating time lost in pits with only refuel #2 (Track selected, 4 pits)")]
        public void RefuelTimeLost_SampleData2()
        {
            NumericUpDown numericUpDownPitsTest = new() { Value = 4 };
            form.time_lost_in_pits = 0;
            form.LoadCarTrackObjects();
            form.fuel_for_race_round_up = 104;
            form.fuel_per_lap = 4;
            form.formation_lap_fuel = 4;
            form.number_of_laps = 25;

            form.RefuelTimeLost("Barcelona", numericUpDownPitsTest);

            Assert.AreEqual(Math.Round(form.time_lost_in_pits, 2), 165.6);
        }

        [TestMethod("Calculating laps and overall race duration #1")]
        public void CalculateRaceDuration_SampleData1()
        {
            form.time_lost_in_pits = form.DEFAULT_TIME_IN_PITS;
            TextBox textBoxRaceHTest = new() { Text = "1" };
            TextBox textBoxRaceMinTest = new() { Text = "0" };
            TextBox textBoxLapMinTest = new() { Text = "1" };
            TextBox textBoxLapSecTest = new() { Text = "45,5" };
            Label labelLapsResultTest = new();
            Label labelLapTimeResult2 = new();
            Label labelOverallResultTest = new();

            form.CalculateRaceDuration(textBoxRaceHTest, textBoxRaceMinTest,
                textBoxLapMinTest, textBoxLapSecTest, labelOverallResultTest, labelLapsResultTest, labelLapTimeResult2);

            Assert.AreEqual(form.number_of_laps, 34);
            Assert.AreEqual(Math.Round(form.overall_race_duration, 3), 3644.000);
        }

        [TestMethod("Calculating laps and overall race duration #2")]
        public void CalculateRaceDuration_SampleData2()
        {
            form.time_lost_in_pits = form.DEFAULT_TIME_IN_PITS * 2;
            TextBox textBoxRaceHTest = new() { Text = "0" };
            TextBox textBoxRaceMinTest = new() { Text = "45" };
            TextBox textBoxLapMinTest = new() { Text = "2" };
            TextBox textBoxLapSecTest = new() { Text = "33,33,," };
            Label labelLapsResultTest = new();
            Label labelLapTimeResult2 = new();
            Label labelOverallResultTest = new();

            form.CalculateRaceDuration(textBoxRaceHTest, textBoxRaceMinTest,
                textBoxLapMinTest, textBoxLapSecTest, labelOverallResultTest, labelLapsResultTest, labelLapTimeResult2);

            Assert.AreEqual(form.number_of_laps, 17);
            Assert.AreEqual(Math.Round(form.overall_race_duration, 3), 2720.610);
        }

        [TestMethod("Calculating Lap times for +1 and -1 lap #1")]
        public void CalculateLapTimnePlusMinus_SampleData1()
        {
            form.number_of_laps = 29;
            form.lap_time_secs = 126.035f;
            form.overall_race_duration = 3655.015;
            form.race_duration_secs = 3600;
            form.time_lost_in_pits = 0;
            Label labelPlus1LapTimeResultTest = new();
            Label labelMinus1LapTimeResultTest = new();

            form.CalculateLapTimePlusMinus(labelPlus1LapTimeResultTest, labelMinus1LapTimeResultTest);

            Assert.AreEqual(labelPlus1LapTimeResultTest.Text, "2:04.138");
            Assert.AreEqual(labelMinus1LapTimeResultTest.Text, "2:08.571");
        }

        [TestMethod("Calculating Lap times for +1 and -1 lap #2")]
        public void CalculateLapTimnePlusMinus_SampleData2()
        {
            form.number_of_laps = 72;
            form.lap_time_secs = 100.015f;
            form.overall_race_duration = 7258.111;
            form.race_duration_secs = 7200;
            form.time_lost_in_pits = 57;
            Label labelPlus1LapTimeResultTest = new();
            Label labelMinus1LapTimeResultTest = new();

            form.CalculateLapTimePlusMinus(labelPlus1LapTimeResultTest, labelMinus1LapTimeResultTest);

            Assert.AreEqual(labelPlus1LapTimeResultTest.Text, "1:39.208");
            Assert.AreEqual(labelMinus1LapTimeResultTest.Text, "1:40.606");
        }

        [TestMethod("Calculating fuel for the race #1 (33 laps, full formation, 3,55 fpr)")]
        public void CalculateFuel_SampleData1()
        {
            TextBox textBoxFuelPerLapTest = new() { Text = "3,55" };
            ListBox listBoxformationLapTest = new() { Text = "Full" };
            Label labelFuelRaceResultTest = new();
            Label labelPlus1FuelResultTest = new();
            Label labelMinus1FuelResultTest = new();
            form.number_of_laps = 33;

            form.CalculateFuel(textBoxFuelPerLapTest, listBoxformationLapTest, labelFuelRaceResultTest,
                labelPlus1FuelResultTest, labelMinus1FuelResultTest);

            Assert.AreEqual(labelFuelRaceResultTest.Text, "122 L");
            Assert.AreEqual(labelPlus1FuelResultTest.Text, "125 L");
            Assert.AreEqual(labelMinus1FuelResultTest.Text, "118 L");
        }

        [TestMethod("Calculating fuel for the race #2 (16 laps, short formation, 3.1 fpr)")]
        public void CalculateFuel_SampleData2()
        {
            TextBox textBoxFuelPerLapTest = new() { Text = "3.,1" };
            ListBox listBoxformationLapTest = new() { Text = "Short" };
            Label labelFuelRaceResultTest = new();
            Label labelPlus1FuelResultTest = new();
            Label labelMinus1FuelResultTest = new();
            form.number_of_laps = 16;

            form.CalculateFuel(textBoxFuelPerLapTest, listBoxformationLapTest, labelFuelRaceResultTest,
                labelPlus1FuelResultTest, labelMinus1FuelResultTest);

            Assert.AreEqual(labelFuelRaceResultTest.Text, "51 L");
            Assert.AreEqual(labelPlus1FuelResultTest.Text, "54 L");
            Assert.AreEqual(labelMinus1FuelResultTest.Text, "48 L");
        }

        [TestMethod("Calculating pit strategy #1 (0 pits)")]
        public void CalculatePitStops_SampleData1()
        {
            Panel panelPitStopStrategyTest = new();
            Label labelFuelRaceResultTest = new() { Text = "56 L" };
            ComboBox comboBoxPitOptionsTest = new();
            NumericUpDown numericUpDownPitsTest = new() { Value = 0 };
            LoadPitOptions(comboBoxPitOptionsTest, form.PIT_OPTIONS);
            form.number_of_laps = 10;
            form.fuel_for_race_round_up = 56;
            form.fuel_per_lap = 5;
            form.formation_lap_fuel = 5.1;

            form.CalculatePitStops(panelPitStopStrategyTest, labelFuelRaceResultTest, numericUpDownPitsTest, comboBoxPitOptionsTest,
                out string labelFuelStartResultTextTest, out List<int> fuelPerStintTest, out List<int> lapsPitStintTest);

            Assert.AreEqual(fuelPerStintTest[0].ToString() + " L", labelFuelStartResultTextTest);
            Assert.AreEqual(fuelPerStintTest.Count, 1);
        }

        [TestMethod("Calculating pit strategy #2 (3 pit, tires only)")]
        public void CalculatePitStops_SampleData2()
        {
            Panel panelPitStopStrategyTest = new();
            Label labelFuelRaceResultTest = new();
            ComboBox comboBoxPitOptionsTest = new();
            NumericUpDown numericUpDownPitsTest = new() { Value = 3 };
            LoadPitOptions(comboBoxPitOptionsTest, form.PIT_OPTIONS);
            comboBoxPitOptionsTest.SelectedIndex = 2;
            form.number_of_laps = 20;
            form.fuel_for_race_round_up = 84;
            form.fuel_per_lap = 4;
            form.formation_lap_fuel = 4;

            form.CalculatePitStops(panelPitStopStrategyTest, labelFuelRaceResultTest, numericUpDownPitsTest, comboBoxPitOptionsTest,
                out string labelFuelStartResultTextTest, out List<int> fuelPerStintTest, out List<int> lapsPitStintTest);

            Assert.AreEqual(fuelPerStintTest[0].ToString() + " L", labelFuelStartResultTextTest);
            Assert.AreEqual(fuelPerStintTest.Count, 4);
            for (int i = 1; i < fuelPerStintTest.Count; i++)
            {
                Assert.IsTrue(fuelPerStintTest[i] == 0);
            }
            int pit_after = 5;
            for (int i = 0; i < lapsPitStintTest.Count; i++)
            {
                Assert.IsTrue(lapsPitStintTest[i] == pit_after);
                pit_after += 5;
            }
        }

        [TestMethod("Calculating pit strategy #3 (1 pit, 1L)")]
        public void CalculatePitStops_SampleData3()
        {
            Panel panelPitStopStrategyTest = new();
            Label labelFuelRaceResultTest = new();
            ComboBox comboBoxPitOptionsTest = new();
            NumericUpDown numericUpDownPitsTest = new() { Value = 1 };
            LoadPitOptions(comboBoxPitOptionsTest, form.PIT_OPTIONS);
            comboBoxPitOptionsTest.SelectedIndex = 4;
            form.number_of_laps = 10;
            form.fuel_for_race_round_up = 44;
            form.fuel_per_lap = 4;
            form.formation_lap_fuel = 4;

            form.CalculatePitStops(panelPitStopStrategyTest, labelFuelRaceResultTest, numericUpDownPitsTest, comboBoxPitOptionsTest,
                out string labelFuelStartResultTextTest, out List<int> fuelPerStintTest, out List<int> lapsPitStintTest);

            Assert.AreEqual(fuelPerStintTest[0].ToString() + " L", labelFuelStartResultTextTest);
            Assert.AreEqual(fuelPerStintTest.Count, 2);
            for (int i = 1; i < fuelPerStintTest.Count; i++)
            {
                Assert.IsTrue(fuelPerStintTest[i] == 1);
            }
            int pit_after = 5;
            for (int i = 0; i < lapsPitStintTest.Count; i++)
            {
                Assert.IsTrue(lapsPitStintTest[i] == pit_after);
            }
        }

        [TestMethod("Calculating pit strategy #4 (5 pit, refuel+tires)")]
        public void CalculatePitStops_SampleData4()
        {
            Panel panelPitStopStrategyTest = new();
            Label labelFuelRaceResultTest = new();
            ComboBox comboBoxPitOptionsTest = new();
            NumericUpDown numericUpDownPitsTest = new() { Value = 5 };
            LoadPitOptions(comboBoxPitOptionsTest, form.PIT_OPTIONS);
            comboBoxPitOptionsTest.SelectedIndex = 3;
            form.number_of_laps = 30;
            form.fuel_for_race_round_up = 124;
            form.fuel_per_lap = 4;
            form.formation_lap_fuel = 4;

            form.CalculatePitStops(panelPitStopStrategyTest, labelFuelRaceResultTest, numericUpDownPitsTest, comboBoxPitOptionsTest,
                out string labelFuelStartResultTextTest, out List<int> fuelPerStintTest, out List<int> lapsPitStintTest);

            Assert.AreEqual(fuelPerStintTest[0].ToString() + " L", labelFuelStartResultTextTest);
            Assert.AreEqual(fuelPerStintTest.Count, 6);
            for (int i = 1; i < fuelPerStintTest.Count; i++)
            {
                Assert.IsTrue(fuelPerStintTest[i] == 20);
            }
            int pit_after = 5;
            for (int i = 0; i < lapsPitStintTest.Count; i++)
            {
                Assert.IsTrue(lapsPitStintTest[i] == pit_after);
                pit_after += 5;
            }
        }
    }
}