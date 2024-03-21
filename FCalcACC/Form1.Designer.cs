namespace FCalcACC
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            listBox_formation = new ListBox();
            label_fuel_L = new Label();
            textBox_fuel_per_lap = new TextBox();
            label_lap_time_sec = new Label();
            label_lap_time_min = new Label();
            textBox_lap_time_sec = new TextBox();
            textBox_lap_time_min = new TextBox();
            label_race_min = new Label();
            label_race_h = new Label();
            textBox_race_min = new TextBox();
            textBox_race_h = new TextBox();
            label_formation = new Label();
            label_fuel_per_lap = new Label();
            label_lap_time = new Label();
            label_race_duration = new Label();
            splitContainer1 = new SplitContainer();
            groupBox_pit = new GroupBox();
            comboBox_pit_options = new ComboBox();
            label_pits_count = new Label();
            numericUpDown_pits = new NumericUpDown();
            label_pits_options = new Label();
            groupBox_variables = new GroupBox();
            groupBox_car_track = new GroupBox();
            label_choose_track = new Label();
            comboBox_class = new ComboBox();
            comboBox_track = new ComboBox();
            label_choose_class = new Label();
            label_choose_car = new Label();
            comboBox_car = new ComboBox();
            button_calculate = new Button();
            panel_pit_stop_strategy = new Panel();
            label_pit_stops = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            label_laps_result = new Label();
            label_laps_number = new Label();
            table_fuel_results = new TableLayoutPanel();
            label_minus1_fuel_result = new Label();
            label_plus1_fuel_result = new Label();
            label_fuel_for_plus1 = new Label();
            label_plus1_lap_time_result = new Label();
            label_lap_time_plus1 = new Label();
            label_overall_duration = new Label();
            label_overall_result = new Label();
            label_fuel_for_the_race = new Label();
            label_fuel_race_result = new Label();
            label_lap_time_minus1 = new Label();
            label_minus1_lap_time_result = new Label();
            label_fuel_for_minus1 = new Label();
            label_input_data = new Label();
            label_results = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            tableLayoutPanel2 = new TableLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            groupBox_pit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_pits).BeginInit();
            groupBox_variables.SuspendLayout();
            groupBox_car_track.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            table_fuel_results.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // listBox_formation
            // 
            listBox_formation.FormattingEnabled = true;
            listBox_formation.ItemHeight = 15;
            listBox_formation.Location = new Point(215, 144);
            listBox_formation.Name = "listBox_formation";
            listBox_formation.Size = new Size(150, 34);
            listBox_formation.TabIndex = 20;
            // 
            // label_fuel_L
            // 
            label_fuel_L.AutoSize = true;
            label_fuel_L.Location = new Point(56, 137);
            label_fuel_L.Name = "label_fuel_L";
            label_fuel_L.Size = new Size(56, 15);
            label_fuel_L.TabIndex = 19;
            label_fuel_L.Text = "L / Lap";
            // 
            // textBox_fuel_per_lap
            // 
            textBox_fuel_per_lap.Location = new Point(56, 155);
            textBox_fuel_per_lap.MaxLength = 4;
            textBox_fuel_per_lap.Name = "textBox_fuel_per_lap";
            textBox_fuel_per_lap.Size = new Size(56, 23);
            textBox_fuel_per_lap.TabIndex = 18;
            textBox_fuel_per_lap.Text = "0.0";
            textBox_fuel_per_lap.TextAlign = HorizontalAlignment.Center;
            textBox_fuel_per_lap.KeyPress += textBox_fuel_per_lap_KeyPress;
            // 
            // label_lap_time_sec
            // 
            label_lap_time_sec.AutoSize = true;
            label_lap_time_sec.Location = new Point(287, 52);
            label_lap_time_sec.Name = "label_lap_time_sec";
            label_lap_time_sec.Size = new Size(28, 15);
            label_lap_time_sec.TabIndex = 17;
            label_lap_time_sec.Text = "Sec";
            // 
            // label_lap_time_min
            // 
            label_lap_time_min.AutoSize = true;
            label_lap_time_min.Location = new Point(215, 52);
            label_lap_time_min.Name = "label_lap_time_min";
            label_lap_time_min.Size = new Size(28, 15);
            label_lap_time_min.TabIndex = 16;
            label_lap_time_min.Text = "Min";
            // 
            // textBox_lap_time_sec
            // 
            textBox_lap_time_sec.Location = new Point(287, 70);
            textBox_lap_time_sec.MaxLength = 6;
            textBox_lap_time_sec.Name = "textBox_lap_time_sec";
            textBox_lap_time_sec.Size = new Size(78, 23);
            textBox_lap_time_sec.TabIndex = 15;
            textBox_lap_time_sec.Text = "0.000";
            textBox_lap_time_sec.TextAlign = HorizontalAlignment.Center;
            textBox_lap_time_sec.KeyPress += textBox_lap_time_sec_KeyPress;
            // 
            // textBox_lap_time_min
            // 
            textBox_lap_time_min.Location = new Point(215, 70);
            textBox_lap_time_min.MaxLength = 1;
            textBox_lap_time_min.Name = "textBox_lap_time_min";
            textBox_lap_time_min.Size = new Size(50, 23);
            textBox_lap_time_min.TabIndex = 14;
            textBox_lap_time_min.Text = "0";
            textBox_lap_time_min.TextAlign = HorizontalAlignment.Center;
            textBox_lap_time_min.KeyPress += textBox_lap_time_min_KeyPress;
            // 
            // label_race_min
            // 
            label_race_min.AutoSize = true;
            label_race_min.Location = new Point(102, 52);
            label_race_min.Name = "label_race_min";
            label_race_min.Size = new Size(28, 15);
            label_race_min.TabIndex = 13;
            label_race_min.Text = "Min";
            // 
            // label_race_h
            // 
            label_race_h.AutoSize = true;
            label_race_h.Location = new Point(10, 52);
            label_race_h.Name = "label_race_h";
            label_race_h.Size = new Size(14, 15);
            label_race_h.TabIndex = 12;
            label_race_h.Text = "H";
            // 
            // textBox_race_min
            // 
            textBox_race_min.Location = new Point(105, 70);
            textBox_race_min.MaxLength = 2;
            textBox_race_min.Name = "textBox_race_min";
            textBox_race_min.Size = new Size(55, 23);
            textBox_race_min.TabIndex = 11;
            textBox_race_min.Text = "0";
            textBox_race_min.TextAlign = HorizontalAlignment.Center;
            textBox_race_min.KeyPress += textBox_race_min_KeyPress;
            // 
            // textBox_race_h
            // 
            textBox_race_h.Location = new Point(10, 70);
            textBox_race_h.MaxLength = 2;
            textBox_race_h.Name = "textBox_race_h";
            textBox_race_h.Size = new Size(55, 23);
            textBox_race_h.TabIndex = 10;
            textBox_race_h.Text = "0";
            textBox_race_h.TextAlign = HorizontalAlignment.Center;
            textBox_race_h.KeyPress += textBox_race_h_KeyPress;
            // 
            // label_formation
            // 
            label_formation.BackColor = Color.Silver;
            label_formation.BorderStyle = BorderStyle.FixedSingle;
            label_formation.Location = new Point(215, 120);
            label_formation.Name = "label_formation";
            label_formation.Size = new Size(150, 17);
            label_formation.TabIndex = 9;
            label_formation.Text = "Formation lap";
            label_formation.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_fuel_per_lap
            // 
            label_fuel_per_lap.BackColor = Color.Silver;
            label_fuel_per_lap.BorderStyle = BorderStyle.FixedSingle;
            label_fuel_per_lap.Location = new Point(10, 120);
            label_fuel_per_lap.Name = "label_fuel_per_lap";
            label_fuel_per_lap.Size = new Size(150, 17);
            label_fuel_per_lap.TabIndex = 8;
            label_fuel_per_lap.Text = "Fuel per lap";
            label_fuel_per_lap.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_lap_time
            // 
            label_lap_time.BackColor = Color.Silver;
            label_lap_time.BorderStyle = BorderStyle.FixedSingle;
            label_lap_time.Location = new Point(215, 33);
            label_lap_time.Name = "label_lap_time";
            label_lap_time.Size = new Size(150, 19);
            label_lap_time.TabIndex = 7;
            label_lap_time.Text = "Lap time";
            label_lap_time.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_race_duration
            // 
            label_race_duration.BackColor = Color.Silver;
            label_race_duration.BorderStyle = BorderStyle.FixedSingle;
            label_race_duration.Location = new Point(10, 33);
            label_race_duration.Name = "label_race_duration";
            label_race_duration.Size = new Size(150, 19);
            label_race_duration.TabIndex = 6;
            label_race_duration.Text = "Race Duration";
            label_race_duration.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // splitContainer1
            // 
            splitContainer1.BorderStyle = BorderStyle.FixedSingle;
            splitContainer1.Location = new Point(12, 38);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(groupBox_pit);
            splitContainer1.Panel1.Controls.Add(groupBox_variables);
            splitContainer1.Panel1.Controls.Add(groupBox_car_track);
            splitContainer1.Panel1.Controls.Add(button_calculate);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(panel_pit_stop_strategy);
            splitContainer1.Panel2.Controls.Add(label_pit_stops);
            splitContainer1.Panel2.Controls.Add(tableLayoutPanel1);
            splitContainer1.Panel2.Controls.Add(table_fuel_results);
            splitContainer1.Size = new Size(880, 551);
            splitContainer1.SplitterDistance = 410;
            splitContainer1.TabIndex = 8;
            // 
            // groupBox_pit
            // 
            groupBox_pit.BackColor = Color.Gainsboro;
            groupBox_pit.Controls.Add(comboBox_pit_options);
            groupBox_pit.Controls.Add(label_pits_count);
            groupBox_pit.Controls.Add(numericUpDown_pits);
            groupBox_pit.Controls.Add(label_pits_options);
            groupBox_pit.Font = new Font("Consolas", 9.75F, FontStyle.Bold);
            groupBox_pit.Location = new Point(18, 403);
            groupBox_pit.Name = "groupBox_pit";
            groupBox_pit.Size = new Size(371, 94);
            groupBox_pit.TabIndex = 13;
            groupBox_pit.TabStop = false;
            groupBox_pit.Text = "Pit stop";
            // 
            // comboBox_pit_options
            // 
            comboBox_pit_options.FormattingEnabled = true;
            comboBox_pit_options.Location = new Point(215, 53);
            comboBox_pit_options.Name = "comboBox_pit_options";
            comboBox_pit_options.Size = new Size(150, 23);
            comboBox_pit_options.TabIndex = 12;
            // 
            // label_pits_count
            // 
            label_pits_count.BackColor = Color.Silver;
            label_pits_count.BorderStyle = BorderStyle.FixedSingle;
            label_pits_count.Location = new Point(10, 29);
            label_pits_count.Name = "label_pits_count";
            label_pits_count.Size = new Size(150, 17);
            label_pits_count.TabIndex = 10;
            label_pits_count.Text = "Number of pit stops";
            label_pits_count.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // numericUpDown_pits
            // 
            numericUpDown_pits.Location = new Point(10, 53);
            numericUpDown_pits.Name = "numericUpDown_pits";
            numericUpDown_pits.Size = new Size(150, 23);
            numericUpDown_pits.TabIndex = 11;
            numericUpDown_pits.TextAlign = HorizontalAlignment.Center;
            numericUpDown_pits.KeyPress += numericUpDown_pits_KeyPress;
            // 
            // label_pits_options
            // 
            label_pits_options.BackColor = Color.Silver;
            label_pits_options.BorderStyle = BorderStyle.FixedSingle;
            label_pits_options.Location = new Point(215, 27);
            label_pits_options.Name = "label_pits_options";
            label_pits_options.Size = new Size(150, 19);
            label_pits_options.TabIndex = 10;
            label_pits_options.Text = "Pit stops options";
            label_pits_options.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBox_variables
            // 
            groupBox_variables.BackColor = Color.Gainsboro;
            groupBox_variables.Controls.Add(listBox_formation);
            groupBox_variables.Controls.Add(label_race_duration);
            groupBox_variables.Controls.Add(textBox_race_h);
            groupBox_variables.Controls.Add(label_fuel_L);
            groupBox_variables.Controls.Add(label_race_h);
            groupBox_variables.Controls.Add(label_formation);
            groupBox_variables.Controls.Add(textBox_race_min);
            groupBox_variables.Controls.Add(textBox_fuel_per_lap);
            groupBox_variables.Controls.Add(label_race_min);
            groupBox_variables.Controls.Add(label_lap_time);
            groupBox_variables.Controls.Add(label_lap_time_sec);
            groupBox_variables.Controls.Add(label_fuel_per_lap);
            groupBox_variables.Controls.Add(textBox_lap_time_min);
            groupBox_variables.Controls.Add(label_lap_time_min);
            groupBox_variables.Controls.Add(textBox_lap_time_sec);
            groupBox_variables.Font = new Font("Consolas", 9.75F, FontStyle.Bold);
            groupBox_variables.Location = new Point(18, 188);
            groupBox_variables.Name = "groupBox_variables";
            groupBox_variables.Size = new Size(371, 195);
            groupBox_variables.TabIndex = 12;
            groupBox_variables.TabStop = false;
            groupBox_variables.Text = "Variables";
            // 
            // groupBox_car_track
            // 
            groupBox_car_track.BackColor = Color.Gainsboro;
            groupBox_car_track.BackgroundImageLayout = ImageLayout.None;
            groupBox_car_track.Controls.Add(label_choose_track);
            groupBox_car_track.Controls.Add(comboBox_class);
            groupBox_car_track.Controls.Add(comboBox_track);
            groupBox_car_track.Controls.Add(label_choose_class);
            groupBox_car_track.Controls.Add(label_choose_car);
            groupBox_car_track.Controls.Add(comboBox_car);
            groupBox_car_track.FlatStyle = FlatStyle.Popup;
            groupBox_car_track.Font = new Font("Consolas", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 238);
            groupBox_car_track.Location = new Point(18, 14);
            groupBox_car_track.Name = "groupBox_car_track";
            groupBox_car_track.Size = new Size(371, 155);
            groupBox_car_track.TabIndex = 11;
            groupBox_car_track.TabStop = false;
            groupBox_car_track.Text = "Car and track";
            // 
            // label_choose_track
            // 
            label_choose_track.BackColor = Color.Silver;
            label_choose_track.BorderStyle = BorderStyle.FixedSingle;
            label_choose_track.Location = new Point(5, 87);
            label_choose_track.Name = "label_choose_track";
            label_choose_track.Size = new Size(360, 19);
            label_choose_track.TabIndex = 4;
            label_choose_track.Text = "Choose the track";
            label_choose_track.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // comboBox_class
            // 
            comboBox_class.FormattingEnabled = true;
            comboBox_class.Location = new Point(5, 56);
            comboBox_class.Name = "comboBox_class";
            comboBox_class.Size = new Size(72, 23);
            comboBox_class.TabIndex = 2;
            comboBox_class.Text = "CLASS";
            comboBox_class.SelectedIndexChanged += comboBox_class_SelectedIndexChanged;
            // 
            // comboBox_track
            // 
            comboBox_track.FormattingEnabled = true;
            comboBox_track.Location = new Point(5, 110);
            comboBox_track.Name = "comboBox_track";
            comboBox_track.Size = new Size(360, 23);
            comboBox_track.TabIndex = 5;
            comboBox_track.Text = "TRACK";
            comboBox_track.SelectedIndexChanged += comboBox_track_SelectedIndexChanged;
            // 
            // label_choose_class
            // 
            label_choose_class.AutoSize = true;
            label_choose_class.BackColor = Color.Silver;
            label_choose_class.BorderStyle = BorderStyle.FixedSingle;
            label_choose_class.Location = new Point(5, 34);
            label_choose_class.Name = "label_choose_class";
            label_choose_class.Size = new Size(72, 17);
            label_choose_class.TabIndex = 0;
            label_choose_class.Text = "Car class";
            label_choose_class.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_choose_car
            // 
            label_choose_car.BackColor = Color.Silver;
            label_choose_car.BorderStyle = BorderStyle.FixedSingle;
            label_choose_car.Location = new Point(87, 33);
            label_choose_car.Name = "label_choose_car";
            label_choose_car.Size = new Size(278, 17);
            label_choose_car.TabIndex = 1;
            label_choose_car.Text = "Choose the car";
            label_choose_car.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // comboBox_car
            // 
            comboBox_car.FormattingEnabled = true;
            comboBox_car.Location = new Point(87, 54);
            comboBox_car.Name = "comboBox_car";
            comboBox_car.Size = new Size(278, 23);
            comboBox_car.TabIndex = 3;
            comboBox_car.Text = "CAR";
            comboBox_car.SelectedIndexChanged += comboBox_car_SelectedIndexChanged;
            // 
            // button_calculate
            // 
            button_calculate.BackColor = SystemColors.ActiveCaption;
            button_calculate.Font = new Font("Consolas", 9.75F, FontStyle.Bold);
            button_calculate.Location = new Point(18, 503);
            button_calculate.Name = "button_calculate";
            button_calculate.Size = new Size(371, 31);
            button_calculate.TabIndex = 10;
            button_calculate.Text = "Calculate";
            button_calculate.UseVisualStyleBackColor = false;
            button_calculate.Click += button_calculate_Click;
            // 
            // panel_pit_stop_strategy
            // 
            panel_pit_stop_strategy.AutoScroll = true;
            panel_pit_stop_strategy.BorderStyle = BorderStyle.FixedSingle;
            panel_pit_stop_strategy.Location = new Point(15, 188);
            panel_pit_stop_strategy.Name = "panel_pit_stop_strategy";
            panel_pit_stop_strategy.Size = new Size(435, 346);
            panel_pit_stop_strategy.TabIndex = 8;
            // 
            // label_pit_stops
            // 
            label_pit_stops.BackColor = Color.Silver;
            label_pit_stops.BorderStyle = BorderStyle.FixedSingle;
            label_pit_stops.Font = new Font("Consolas", 9.75F, FontStyle.Bold);
            label_pit_stops.Location = new Point(15, 165);
            label_pit_stops.Name = "label_pit_stops";
            label_pit_stops.Size = new Size(435, 17);
            label_pit_stops.TabIndex = 7;
            label_pit_stops.Text = "Pit Stop Strategy";
            label_pit_stops.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.Gainsboro;
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68.7763748F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 31.223629F));
            tableLayoutPanel1.Controls.Add(label_laps_result, 0, 0);
            tableLayoutPanel1.Controls.Add(label_laps_number, 0, 0);
            tableLayoutPanel1.Font = new Font("Consolas", 9.75F, FontStyle.Bold);
            tableLayoutPanel1.Location = new Point(15, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(239, 34);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // label_laps_result
            // 
            label_laps_result.AutoSize = true;
            label_laps_result.Dock = DockStyle.Fill;
            label_laps_result.Location = new Point(167, 2);
            label_laps_result.Name = "label_laps_result";
            label_laps_result.Size = new Size(67, 30);
            label_laps_result.TabIndex = 4;
            label_laps_result.Text = "0 laps";
            label_laps_result.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_laps_number
            // 
            label_laps_number.AutoSize = true;
            label_laps_number.Dock = DockStyle.Fill;
            label_laps_number.Location = new Point(5, 2);
            label_laps_number.Name = "label_laps_number";
            label_laps_number.Size = new Size(154, 30);
            label_laps_number.TabIndex = 3;
            label_laps_number.Text = "Number of laps";
            label_laps_number.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // table_fuel_results
            // 
            table_fuel_results.BackColor = Color.Gainsboro;
            table_fuel_results.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;
            table_fuel_results.ColumnCount = 4;
            table_fuel_results.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68.7763748F));
            table_fuel_results.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 31.223629F));
            table_fuel_results.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 136F));
            table_fuel_results.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 55F));
            table_fuel_results.Controls.Add(label_minus1_fuel_result, 3, 2);
            table_fuel_results.Controls.Add(label_plus1_fuel_result, 3, 1);
            table_fuel_results.Controls.Add(label_fuel_for_plus1, 2, 1);
            table_fuel_results.Controls.Add(label_plus1_lap_time_result, 1, 1);
            table_fuel_results.Controls.Add(label_lap_time_plus1, 0, 1);
            table_fuel_results.Controls.Add(label_overall_duration, 0, 0);
            table_fuel_results.Controls.Add(label_overall_result, 1, 0);
            table_fuel_results.Controls.Add(label_fuel_for_the_race, 2, 0);
            table_fuel_results.Controls.Add(label_fuel_race_result, 3, 0);
            table_fuel_results.Controls.Add(label_lap_time_minus1, 0, 2);
            table_fuel_results.Controls.Add(label_minus1_lap_time_result, 1, 2);
            table_fuel_results.Controls.Add(label_fuel_for_minus1, 2, 2);
            table_fuel_results.Font = new Font("Consolas", 9.75F, FontStyle.Bold);
            table_fuel_results.Location = new Point(15, 47);
            table_fuel_results.Name = "table_fuel_results";
            table_fuel_results.RowCount = 3;
            table_fuel_results.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            table_fuel_results.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            table_fuel_results.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            table_fuel_results.Size = new Size(435, 101);
            table_fuel_results.TabIndex = 0;
            // 
            // label_minus1_fuel_result
            // 
            label_minus1_fuel_result.AutoSize = true;
            label_minus1_fuel_result.Dock = DockStyle.Fill;
            label_minus1_fuel_result.Location = new Point(380, 66);
            label_minus1_fuel_result.Name = "label_minus1_fuel_result";
            label_minus1_fuel_result.Size = new Size(50, 33);
            label_minus1_fuel_result.TabIndex = 6;
            label_minus1_fuel_result.Text = "0 L";
            label_minus1_fuel_result.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_plus1_fuel_result
            // 
            label_plus1_fuel_result.AutoSize = true;
            label_plus1_fuel_result.Dock = DockStyle.Fill;
            label_plus1_fuel_result.Location = new Point(380, 34);
            label_plus1_fuel_result.Name = "label_plus1_fuel_result";
            label_plus1_fuel_result.Size = new Size(50, 30);
            label_plus1_fuel_result.TabIndex = 6;
            label_plus1_fuel_result.Text = "0 L";
            label_plus1_fuel_result.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_fuel_for_plus1
            // 
            label_fuel_for_plus1.AutoSize = true;
            label_fuel_for_plus1.Dock = DockStyle.Fill;
            label_fuel_for_plus1.Location = new Point(242, 34);
            label_fuel_for_plus1.Name = "label_fuel_for_plus1";
            label_fuel_for_plus1.Size = new Size(130, 30);
            label_fuel_for_plus1.TabIndex = 10;
            label_fuel_for_plus1.Text = "Fuel for +1 lap";
            label_fuel_for_plus1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_plus1_lap_time_result
            // 
            label_plus1_lap_time_result.AutoSize = true;
            label_plus1_lap_time_result.Dock = DockStyle.Fill;
            label_plus1_lap_time_result.Location = new Point(167, 34);
            label_plus1_lap_time_result.Name = "label_plus1_lap_time_result";
            label_plus1_lap_time_result.Size = new Size(67, 30);
            label_plus1_lap_time_result.TabIndex = 8;
            label_plus1_lap_time_result.Text = "0:00.000";
            label_plus1_lap_time_result.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_lap_time_plus1
            // 
            label_lap_time_plus1.AutoSize = true;
            label_lap_time_plus1.Dock = DockStyle.Fill;
            label_lap_time_plus1.Location = new Point(5, 34);
            label_lap_time_plus1.Name = "label_lap_time_plus1";
            label_lap_time_plus1.Size = new Size(154, 30);
            label_lap_time_plus1.TabIndex = 6;
            label_lap_time_plus1.Text = "Lap time for +1 lap";
            label_lap_time_plus1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_overall_duration
            // 
            label_overall_duration.AutoSize = true;
            label_overall_duration.Dock = DockStyle.Fill;
            label_overall_duration.Location = new Point(5, 2);
            label_overall_duration.Name = "label_overall_duration";
            label_overall_duration.Size = new Size(154, 30);
            label_overall_duration.TabIndex = 2;
            label_overall_duration.Text = "Overall race duration";
            label_overall_duration.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_overall_result
            // 
            label_overall_result.AutoSize = true;
            label_overall_result.Dock = DockStyle.Fill;
            label_overall_result.Location = new Point(167, 2);
            label_overall_result.Name = "label_overall_result";
            label_overall_result.Size = new Size(67, 30);
            label_overall_result.TabIndex = 3;
            label_overall_result.Text = "00:00:00";
            label_overall_result.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_fuel_for_the_race
            // 
            label_fuel_for_the_race.AutoSize = true;
            label_fuel_for_the_race.Dock = DockStyle.Fill;
            label_fuel_for_the_race.Location = new Point(242, 2);
            label_fuel_for_the_race.Name = "label_fuel_for_the_race";
            label_fuel_for_the_race.Size = new Size(130, 30);
            label_fuel_for_the_race.TabIndex = 4;
            label_fuel_for_the_race.Text = "Fuel for the race";
            label_fuel_for_the_race.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_fuel_race_result
            // 
            label_fuel_race_result.AutoSize = true;
            label_fuel_race_result.BackColor = Color.Gainsboro;
            label_fuel_race_result.Dock = DockStyle.Fill;
            label_fuel_race_result.Location = new Point(380, 2);
            label_fuel_race_result.Name = "label_fuel_race_result";
            label_fuel_race_result.Size = new Size(50, 30);
            label_fuel_race_result.TabIndex = 5;
            label_fuel_race_result.Text = "0 L";
            label_fuel_race_result.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_lap_time_minus1
            // 
            label_lap_time_minus1.AutoSize = true;
            label_lap_time_minus1.Dock = DockStyle.Fill;
            label_lap_time_minus1.Location = new Point(5, 66);
            label_lap_time_minus1.Name = "label_lap_time_minus1";
            label_lap_time_minus1.Size = new Size(154, 33);
            label_lap_time_minus1.TabIndex = 7;
            label_lap_time_minus1.Text = "Lap time for -1 lap";
            label_lap_time_minus1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_minus1_lap_time_result
            // 
            label_minus1_lap_time_result.AutoSize = true;
            label_minus1_lap_time_result.Dock = DockStyle.Fill;
            label_minus1_lap_time_result.Location = new Point(167, 66);
            label_minus1_lap_time_result.Name = "label_minus1_lap_time_result";
            label_minus1_lap_time_result.Size = new Size(67, 33);
            label_minus1_lap_time_result.TabIndex = 9;
            label_minus1_lap_time_result.Text = "0:00.000";
            label_minus1_lap_time_result.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_fuel_for_minus1
            // 
            label_fuel_for_minus1.AutoSize = true;
            label_fuel_for_minus1.Dock = DockStyle.Fill;
            label_fuel_for_minus1.Location = new Point(242, 66);
            label_fuel_for_minus1.Name = "label_fuel_for_minus1";
            label_fuel_for_minus1.Size = new Size(130, 33);
            label_fuel_for_minus1.TabIndex = 11;
            label_fuel_for_minus1.Text = "Fuel for -1 lap";
            label_fuel_for_minus1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_input_data
            // 
            label_input_data.BackColor = SystemColors.ActiveBorder;
            label_input_data.BorderStyle = BorderStyle.FixedSingle;
            label_input_data.Font = new Font("Consolas", 9.75F, FontStyle.Bold);
            label_input_data.Location = new Point(12, 16);
            label_input_data.Name = "label_input_data";
            label_input_data.Size = new Size(410, 19);
            label_input_data.TabIndex = 9;
            label_input_data.Text = "Input data";
            label_input_data.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_results
            // 
            label_results.BackColor = SystemColors.ActiveBorder;
            label_results.BorderStyle = BorderStyle.FixedSingle;
            label_results.Font = new Font("Consolas", 9.75F, FontStyle.Bold);
            label_results.Location = new Point(426, 16);
            label_results.Name = "label_results";
            label_results.Size = new Size(466, 19);
            label_results.TabIndex = 10;
            label_results.Text = "Results";
            label_results.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67.94258F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32.057415F));
            tableLayoutPanel2.Controls.Add(label1, 0, 0);
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.Size = new Size(200, 100);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(5, 2);
            label1.Name = "label1";
            label1.Size = new Size(43, 17);
            label1.TabIndex = 4;
            label1.Text = "0 laps";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(5, 2);
            label2.Name = "label2";
            label2.Size = new Size(100, 17);
            label2.TabIndex = 3;
            label2.Text = "Number of laps";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(901, 601);
            Controls.Add(label_results);
            Controls.Add(label_input_data);
            Controls.Add(splitContainer1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Form1";
            Text = "FCalcACC - Fuel and strategy for ACC";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            groupBox_pit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDown_pits).EndInit();
            groupBox_variables.ResumeLayout(false);
            groupBox_variables.PerformLayout();
            groupBox_car_track.ResumeLayout(false);
            groupBox_car_track.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            table_fuel_results.ResumeLayout(false);
            table_fuel_results.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label label_lap_time;
        private Label label_race_duration;
        private Label label_fuel_per_lap;
        private Label label_formation;
        private SplitContainer splitContainer1;
        private Label label_input_data;
        private Label label_pits_count;
        private Label label_results;
        private Label label_pits_options;
        private Button button_calculate;
        private NumericUpDown numericUpDown_pits;
        private TextBox textBox_race_min;
        private TextBox textBox_race_h;
        private ComboBox comboBox_pit_options;
        private Label label_fuel_L;
        private TextBox textBox_fuel_per_lap;
        private Label label_lap_time_sec;
        private Label label_lap_time_min;
        private TextBox textBox_lap_time_sec;
        private TextBox textBox_lap_time_min;
        private Label label_race_min;
        private Label label_race_h;
        private ListBox listBox_formation;
        private TableLayoutPanel table_fuel_results;
        private Label label_number_of_laps;
        private Label label_fuel_for_the_race;
        private Label label_overall_result;
        private Label label_overall_duration;
        private Label label_fuel_race_result;
        private Label label_plus1_lap_time_result;
        private Label label_lap_time_plus1;
        private Label label_minus1_lap_time_result;
        private Label label_minus1_fuel_result;
        private Label label_plus1_fuel_result;
        private Label label_fuel_for_plus1;
        private Label label_fuel_for_minus1;
        private TableLayoutPanel tableLayoutPanel1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Label label_laps_result;
        private Label label_laps_number;
        private Panel panel_pit_stop_strategy;
        private Label label_pit_stops;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label1;
        private Label label2;
        private Label label_choose_class;
        private Label label_choose_car;
        private ComboBox comboBox_class;
        private ComboBox comboBox_car;
        private Label label_choose_track;
        private ComboBox comboBox_track;
        private GroupBox groupBox_car_track;
        private GroupBox groupBox_variables;
        private GroupBox groupBox_pit;
        private Label label_lap_time_minus1;
    }
}
