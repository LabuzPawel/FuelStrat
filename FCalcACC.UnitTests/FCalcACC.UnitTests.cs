using Microsoft.VisualStudio.TestTools.UnitTesting;
using FCalcACC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FCalcACC.Form1;
using System.DirectoryServices.ActiveDirectory;

namespace FCalcACC.Tests
{
    [TestClass()]
    public class Form1Tests
    {
        private Form1 form;

        [TestInitialize]
        public void TestInitialize()
        {
            form = new Form1();
        }

        [TestMethod("Car object from CAR.json")]
        public void LoadCarObjectList_LoadCarJson_ReturnsListOfCarObjects()
        {
            form.LoadCarObjectsList();
            Assert.IsTrue(form.all_cars[0] is Car);
        }

        [TestMethod("Track object from TRACK.json")]
        public void LoadTrackObjectList_LoadTrackJson_ReturnsListOfTrackObjects()
        {
            form.LoadTrackObjectsList();
            Assert.IsTrue(form.all_tracks[0] is Track);
            Assert.IsTrue(form.all_tracks[0].car_track_fuel[0] is CarTrackFuel);
        }

        [TestMethod("Load car classes into comboBox")]
        public void LoadCarClasses_AddUniqueCarClasses_ComboBoxCorrectlyPopulated()
        {
            ComboBox comboBox_test = new ComboBox();
            form.LoadCarObjectsList();

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
            ComboBox comboBox_test_car = new ComboBox();
            form.LoadCarObjectsList();

            form.LoadCars(comboBox_test_car, "GT3");
            var gt3_cars = form.all_cars.Where(car => car.class_name.Contains("GT3"));
            Assert.AreEqual(gt3_cars.Count(), comboBox_test_car.Items.Count);
            Assert.AreEqual(comboBox_test_car.Items[0], "Aston Martin V12 Vantage GT3 2013");

            form.LoadCars(comboBox_test_car, "GT4");
            var gt4_cars = form.all_cars.Where(car => car.class_name.Contains("GT4"));
            Assert.AreEqual(gt4_cars.Count(), comboBox_test_car.Items.Count);
            Assert.AreEqual(comboBox_test_car.Items[0], "Alpine A110 GT4 2018");
            
            form.LoadCars(comboBox_test_car, "GT2");
            var gt2_cars = form.all_cars.Where(car => car.class_name.Contains("GT2"));
            Assert.AreEqual(gt2_cars.Count(), comboBox_test_car.Items.Count);
            Assert.AreEqual(comboBox_test_car.Items[0], "Audi R8 LMS GT2");
            
            form.LoadCars(comboBox_test_car, "GTC");
            var gtc_cars = form.all_cars.Where(car => car.class_name.Contains("GTC"));
            Assert.AreEqual(gtc_cars.Count(), comboBox_test_car.Items.Count);
            Assert.AreEqual(comboBox_test_car.Items[0], "Ferrari 488 Challenge Evo 2020");
            
            form.LoadCars(comboBox_test_car, "TCX");
            var tcx_cars = form.all_cars.Where(car => car.class_name.Contains("TCX"));
            Assert.AreEqual(tcx_cars.Count(), comboBox_test_car.Items.Count);
            Assert.AreEqual(comboBox_test_car.Items[0], "BMW M2 CS 2020");
        }

        [TestMethod("Load tracks into comboBox")]
        public void LoadTracks_AddTracks_ComboBoxCorrectlyPopulated()
        {
            ComboBox comboBox_test = new ComboBox();
            form.LoadTrackObjectsList();
            form.LoadTracks(comboBox_test);

            Assert.AreEqual(form.all_tracks.Count, comboBox_test.Items.Count);
            Assert.AreEqual(comboBox_test.Items[0], "Barcelona");
        }

        [TestMethod("Load pit options into comboBox")]
        public void LoadPitOptions_AddPitOptions_ComboBoxCorrectlyPopulated()
        {
            ComboBox comboBox_test = new ComboBox();
            form.LoadPitOptions(comboBox_test);

            Assert.AreEqual(form.PIT_OPTIONS.Count, comboBox_test.Items.Count);
            foreach (var option in form.PIT_OPTIONS)
            {
                Assert.IsTrue(comboBox_test.Items.Contains(option));
            }
        }

        [TestMethod("Time lost in pits (No Track, 2 pits)")]
        public void CalculateTimeLostInPits_NoTrack2Pits()
        {
            List<string> test_options = form.PIT_OPTIONS;
            test_options.Remove("Refuel only");

            foreach (string test in test_options)
            {
                form.CalculateTimeLostInPits(2, test, "TRACK");
                
                if (test == "Tires only")
                {
                    Assert.AreEqual(form.time_lost_in_pits, form.DEFAULT_TIME_IN_PITS * 2);
                }
                else if (test == "Fixed refuel only")
                {
                    Assert.AreEqual(form.time_lost_in_pits, (form.DEFAULT_TIME_IN_PITS - 5) * 2);
                }
                else if (test == "1L refuel")
                {
                    Assert.AreEqual(form.time_lost_in_pits, (form.DEFAULT_TIME_IN_PITS - 26.4) * 2);
                }
                else if (test == "Refuel + tires")
                {
                    Assert.AreEqual(form.time_lost_in_pits, form.DEFAULT_TIME_IN_PITS * 2);
                }
            }
        }

        [TestMethod("Time lost in pits (Selected Track, 2 pits)")]
        public void CalculateTimeLostInPits_SelectedTrack2Pits()
        {
            List<string> test_options = form.PIT_OPTIONS;
            test_options.Remove("Refuel only");

            form.LoadTrackObjectsList();
            
            foreach (string test in test_options)
            {
                form.CalculateTimeLostInPits(2, test, "Barcelona");

                if (test == "Tires only")
                {
                    Assert.AreEqual(form.time_lost_in_pits, form.time_in_pits * 2);
                }
                else if (test == "Fixed refuel only")
                {
                    Assert.AreEqual(form.time_lost_in_pits, (form.time_in_pits - 5) * 2);
                }
                else if (test == "1L refuel")
                {
                    Assert.AreEqual(form.time_lost_in_pits, (form.time_in_pits - 26.4) * 2);
                }
                else if (test == "Refuel + tires")
                {
                    Assert.AreEqual(form.time_lost_in_pits, form.time_in_pits * 2);
                }
            }
        }

        // RefuelTimeLost

        [TestMethod("Calculating laps and overall race duration #1")]
        public void CalculateRaceDuration_SampleData1()
        {
            form.time_lost_in_pits = form.DEFAULT_TIME_IN_PITS;
            TextBox textBoxRaceHTest = new TextBox() { Text = "1" };
            TextBox textBoxRaceMinTest = new TextBox() { Text = "0" };
            TextBox textBoxLapMinTest = new TextBox() { Text = "1" };
            TextBox textBoxLapSecTest = new TextBox() { Text = "45,5" };
            Label labelLapsResultTest = new Label();
            Label labelLapTimeResult2 = new Label();
            Label labelOverallResultTest = new Label();

            form.CalculateRaceDuration(textBoxRaceHTest, textBoxRaceMinTest,
                textBoxLapMinTest, textBoxLapSecTest, labelOverallResultTest, labelLapsResultTest, labelLapTimeResult2);

            Assert.AreEqual(form.number_of_laps, 34);
            Assert.AreEqual(Math.Round(form.overall_race_duration, 3), 3644.000);
        }

        [TestMethod("Calculating laps and overall race duration #2")]
        public void CalculateRaceDuration_SampleData2()
        {
            form.time_lost_in_pits = form.DEFAULT_TIME_IN_PITS * 2;
            TextBox textBoxRaceHTest = new TextBox() { Text = "0" };
            TextBox textBoxRaceMinTest = new TextBox() { Text = "45" };
            TextBox textBoxLapMinTest = new TextBox() { Text = "2" };
            TextBox textBoxLapSecTest = new TextBox() { Text = "33,33,," };
            Label labelLapsResultTest = new Label();
            Label labelLapTimeResult2 = new Label();
            Label labelOverallResultTest = new Label();

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
            Label labelPlus1LapTimeResultTest = new Label();
            Label labelMinus1LapTimeResultTest = new Label();

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
            Label labelPlus1LapTimeResultTest = new Label();
            Label labelMinus1LapTimeResultTest = new Label();

            form.CalculateLapTimePlusMinus(labelPlus1LapTimeResultTest, labelMinus1LapTimeResultTest);

            Assert.AreEqual(labelPlus1LapTimeResultTest.Text, "1:39.208");
            Assert.AreEqual(labelMinus1LapTimeResultTest.Text, "1:40.606");
        }

        [TestMethod("Calculating fuel for the race #1")]
        public void CalculateFuel_SampleData1()
        {
            TextBox textBoxFuelPerLapTest = new TextBox() { Text = "3,55" };
            ListBox listBoxformationLapTest = new ListBox() { Text = "Full" };
            Label labelFuelRaceResultTest = new Label();
            Label labelPlus1FuelResultTest = new Label();
            Label labelMinus1FuelResultTest = new Label();
            form.number_of_laps = 33;

            form.CalculateFuel(textBoxFuelPerLapTest, listBoxformationLapTest, labelFuelRaceResultTest,
                labelPlus1FuelResultTest, labelMinus1FuelResultTest);

            Assert.AreEqual(labelFuelRaceResultTest.Text, "122 L");
            Assert.AreEqual(labelPlus1FuelResultTest.Text, "125 L");
            Assert.AreEqual(labelMinus1FuelResultTest.Text, "118 L");
        }

        [TestMethod("Calculating fuel for the race #2")]
        public void CalculateFuel_SampleData2()
        {
            TextBox textBoxFuelPerLapTest = new TextBox() { Text = "3.,1" };
            ListBox listBoxformationLapTest = new ListBox() { Text = "Short" };
            Label labelFuelRaceResultTest = new Label();
            Label labelPlus1FuelResultTest = new Label();
            Label labelMinus1FuelResultTest = new Label();
            form.number_of_laps = 16;

            form.CalculateFuel(textBoxFuelPerLapTest, listBoxformationLapTest, labelFuelRaceResultTest,
                labelPlus1FuelResultTest, labelMinus1FuelResultTest);

            Assert.AreEqual(labelFuelRaceResultTest.Text, "51 L");
            Assert.AreEqual(labelPlus1FuelResultTest.Text, "54 L");
            Assert.AreEqual(labelMinus1FuelResultTest.Text, "48 L");
        }
    }
}
