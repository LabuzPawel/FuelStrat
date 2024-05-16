namespace FuelStrat
{
    partial class FuelStrat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FuelStrat));
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
            splitContainer_input_result = new SplitContainer();
            groupBox_pit = new GroupBox();
            checkBox_max_stint = new CheckBox();
            comboBox_pit_options = new ComboBox();
            textBox_max_stint = new TextBox();
            label_pits_count = new Label();
            label_max_stint_min = new Label();
            numericUpDown_pits = new NumericUpDown();
            label_max_stint = new Label();
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
            tableLayoutPanel_duration_laps_result = new TableLayoutPanel();
            label_overall_duration = new Label();
            label_laps_result = new Label();
            label_laps_number = new Label();
            label_overall_result = new Label();
            table_fuel_results = new TableLayoutPanel();
            label_lap_time_restult = new Label();
            label_minus1_fuel_result = new Label();
            label_plus1_fuel_result = new Label();
            label_fuel_for_plus1 = new Label();
            label_plus1_lap_time_result = new Label();
            label_lap_time_plus1 = new Label();
            label_fuel_race_result = new Label();
            label_lap_time_minus1 = new Label();
            label_minus1_lap_time_result = new Label();
            label_fuel_for_minus1 = new Label();
            label_lap_time_result2 = new Label();
            label_fuel_for_the_race = new Label();
            label_input_data = new Label();
            label_results = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            menuStrip_game_status = new MenuStrip();
            toolStripMenuItem_menu = new ToolStripMenuItem();
            ignoreInvalidLapsToolStripMenuItem = new ToolStripMenuItem();
            resetDataToolStripMenuItem = new ToolStripMenuItem();
            resetAllDataToolStripMenuItem = new ToolStripMenuItem();
            resetCurrentCartrackToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            ToolStripMenuItem_help = new ToolStripMenuItem();
            ToolStripMenuItem_github = new ToolStripMenuItem();
            toolStripMenuItem_empty = new ToolStripMenuItem();
            ToolStripMenuItem_game_status = new ToolStripMenuItem();
            button_save_load = new Button();
            button_import_race = new Button();
            button_auto = new Button();
            button_import_stint = new Button();
            checkBox_lap_time = new CheckBox();
            panel_telemetry = new Panel();
            label_telemetry = new Label();
            telemetryDisabledToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)splitContainer_input_result).BeginInit();
            splitContainer_input_result.Panel1.SuspendLayout();
            splitContainer_input_result.Panel2.SuspendLayout();
            splitContainer_input_result.SuspendLayout();
            groupBox_pit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_pits).BeginInit();
            groupBox_variables.SuspendLayout();
            groupBox_car_track.SuspendLayout();
            tableLayoutPanel_duration_laps_result.SuspendLayout();
            table_fuel_results.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            menuStrip_game_status.SuspendLayout();
            panel_telemetry.SuspendLayout();
            SuspendLayout();
            // 
            // listBox_formation
            // 
            listBox_formation.Font = new Font("Consolas", 11.25F);
            listBox_formation.FormattingEnabled = true;
            listBox_formation.Items.AddRange(new object[] { "Full", "Short" });
            listBox_formation.Location = new Point(235, 139);
            listBox_formation.Name = "listBox_formation";
            listBox_formation.Size = new Size(205, 40);
            listBox_formation.TabIndex = 20;
            // 
            // label_fuel_L
            // 
            label_fuel_L.AutoSize = true;
            label_fuel_L.Font = new Font("Consolas", 11.25F);
            label_fuel_L.Location = new Point(47, 154);
            label_fuel_L.Name = "label_fuel_L";
            label_fuel_L.Size = new Size(64, 18);
            label_fuel_L.TabIndex = 19;
            label_fuel_L.Text = "L / Lap";
            // 
            // textBox_fuel_per_lap
            // 
            textBox_fuel_per_lap.Font = new Font("Consolas", 11.25F);
            textBox_fuel_per_lap.Location = new Point(118, 150);
            textBox_fuel_per_lap.MaxLength = 4;
            textBox_fuel_per_lap.Name = "textBox_fuel_per_lap";
            textBox_fuel_per_lap.Size = new Size(62, 25);
            textBox_fuel_per_lap.TabIndex = 18;
            textBox_fuel_per_lap.Text = "0.0";
            textBox_fuel_per_lap.TextAlign = HorizontalAlignment.Center;
            textBox_fuel_per_lap.KeyPress += TextBox_fuel_per_lap_KeyPress;
            // 
            // label_lap_time_sec
            // 
            label_lap_time_sec.AutoSize = true;
            label_lap_time_sec.Font = new Font("Consolas", 11.25F);
            label_lap_time_sec.Location = new Point(326, 72);
            label_lap_time_sec.Name = "label_lap_time_sec";
            label_lap_time_sec.Size = new Size(32, 18);
            label_lap_time_sec.TabIndex = 17;
            label_lap_time_sec.Text = "Sec";
            // 
            // label_lap_time_min
            // 
            label_lap_time_min.AutoSize = true;
            label_lap_time_min.Font = new Font("Consolas", 11.25F);
            label_lap_time_min.Location = new Point(235, 72);
            label_lap_time_min.Name = "label_lap_time_min";
            label_lap_time_min.Size = new Size(32, 18);
            label_lap_time_min.TabIndex = 16;
            label_lap_time_min.Text = "Min";
            // 
            // textBox_lap_time_sec
            // 
            textBox_lap_time_sec.Font = new Font("Consolas", 11.25F);
            textBox_lap_time_sec.Location = new Point(358, 69);
            textBox_lap_time_sec.MaxLength = 6;
            textBox_lap_time_sec.Name = "textBox_lap_time_sec";
            textBox_lap_time_sec.Size = new Size(83, 25);
            textBox_lap_time_sec.TabIndex = 15;
            textBox_lap_time_sec.Text = "0.000";
            textBox_lap_time_sec.TextAlign = HorizontalAlignment.Center;
            textBox_lap_time_sec.KeyPress += TextBox_lap_time_sec_KeyPress;
            // 
            // textBox_lap_time_min
            // 
            textBox_lap_time_min.Font = new Font("Consolas", 11.25F);
            textBox_lap_time_min.Location = new Point(265, 69);
            textBox_lap_time_min.MaxLength = 1;
            textBox_lap_time_min.Name = "textBox_lap_time_min";
            textBox_lap_time_min.Size = new Size(52, 25);
            textBox_lap_time_min.TabIndex = 14;
            textBox_lap_time_min.Text = "0";
            textBox_lap_time_min.TextAlign = HorizontalAlignment.Center;
            textBox_lap_time_min.KeyPress += TextBox_lap_time_min_KeyPress;
            // 
            // label_race_min
            // 
            label_race_min.AutoSize = true;
            label_race_min.Font = new Font("Consolas", 11.25F);
            label_race_min.Location = new Point(118, 72);
            label_race_min.Name = "label_race_min";
            label_race_min.Size = new Size(32, 18);
            label_race_min.TabIndex = 13;
            label_race_min.Text = "Min";
            // 
            // label_race_h
            // 
            label_race_h.AutoSize = true;
            label_race_h.Font = new Font("Consolas", 11.25F);
            label_race_h.Location = new Point(11, 72);
            label_race_h.Name = "label_race_h";
            label_race_h.Size = new Size(16, 18);
            label_race_h.TabIndex = 12;
            label_race_h.Text = "H";
            // 
            // textBox_race_min
            // 
            textBox_race_min.Font = new Font("Consolas", 11.25F);
            textBox_race_min.Location = new Point(154, 69);
            textBox_race_min.MaxLength = 2;
            textBox_race_min.Name = "textBox_race_min";
            textBox_race_min.Size = new Size(62, 25);
            textBox_race_min.TabIndex = 11;
            textBox_race_min.Text = "0";
            textBox_race_min.TextAlign = HorizontalAlignment.Center;
            textBox_race_min.KeyPress += TextBox_race_min_KeyPress;
            // 
            // textBox_race_h
            // 
            textBox_race_h.Font = new Font("Consolas", 11.25F);
            textBox_race_h.Location = new Point(31, 69);
            textBox_race_h.MaxLength = 2;
            textBox_race_h.Name = "textBox_race_h";
            textBox_race_h.Size = new Size(62, 25);
            textBox_race_h.TabIndex = 10;
            textBox_race_h.Text = "0";
            textBox_race_h.TextAlign = HorizontalAlignment.Center;
            textBox_race_h.KeyPress += TextBox_race_h_KeyPress;
            // 
            // label_formation
            // 
            label_formation.BackColor = Color.Silver;
            label_formation.BorderStyle = BorderStyle.FixedSingle;
            label_formation.Font = new Font("Consolas", 11.25F);
            label_formation.Location = new Point(235, 109);
            label_formation.Name = "label_formation";
            label_formation.Size = new Size(205, 22);
            label_formation.TabIndex = 9;
            label_formation.Text = "Formation lap";
            label_formation.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_fuel_per_lap
            // 
            label_fuel_per_lap.BackColor = Color.Silver;
            label_fuel_per_lap.BorderStyle = BorderStyle.FixedSingle;
            label_fuel_per_lap.Font = new Font("Consolas", 11.25F);
            label_fuel_per_lap.Location = new Point(11, 109);
            label_fuel_per_lap.Name = "label_fuel_per_lap";
            label_fuel_per_lap.Size = new Size(205, 22);
            label_fuel_per_lap.TabIndex = 8;
            label_fuel_per_lap.Text = "Fuel per lap";
            label_fuel_per_lap.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_lap_time
            // 
            label_lap_time.BackColor = Color.Silver;
            label_lap_time.BorderStyle = BorderStyle.FixedSingle;
            label_lap_time.Font = new Font("Consolas", 11.25F);
            label_lap_time.Location = new Point(235, 33);
            label_lap_time.Name = "label_lap_time";
            label_lap_time.Size = new Size(205, 22);
            label_lap_time.TabIndex = 7;
            label_lap_time.Text = "Lap time";
            label_lap_time.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_race_duration
            // 
            label_race_duration.BackColor = Color.Silver;
            label_race_duration.BorderStyle = BorderStyle.FixedSingle;
            label_race_duration.Font = new Font("Consolas", 11.25F);
            label_race_duration.Location = new Point(11, 33);
            label_race_duration.Name = "label_race_duration";
            label_race_duration.Size = new Size(205, 22);
            label_race_duration.TabIndex = 6;
            label_race_duration.Text = "Race Duration";
            label_race_duration.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // splitContainer_input_result
            // 
            splitContainer_input_result.BorderStyle = BorderStyle.FixedSingle;
            splitContainer_input_result.IsSplitterFixed = true;
            splitContainer_input_result.Location = new Point(14, 280);
            splitContainer_input_result.Name = "splitContainer_input_result";
            // 
            // splitContainer_input_result.Panel1
            // 
            splitContainer_input_result.Panel1.Controls.Add(groupBox_pit);
            splitContainer_input_result.Panel1.Controls.Add(groupBox_variables);
            splitContainer_input_result.Panel1.Controls.Add(groupBox_car_track);
            splitContainer_input_result.Panel1.Controls.Add(button_calculate);
            // 
            // splitContainer_input_result.Panel2
            // 
            splitContainer_input_result.Panel2.Controls.Add(panel_pit_stop_strategy);
            splitContainer_input_result.Panel2.Controls.Add(label_pit_stops);
            splitContainer_input_result.Panel2.Controls.Add(tableLayoutPanel_duration_laps_result);
            splitContainer_input_result.Panel2.Controls.Add(table_fuel_results);
            splitContainer_input_result.Size = new Size(1040, 593);
            splitContainer_input_result.SplitterDistance = 497;
            splitContainer_input_result.SplitterWidth = 5;
            splitContainer_input_result.TabIndex = 8;
            // 
            // groupBox_pit
            // 
            groupBox_pit.BackColor = Color.Gainsboro;
            groupBox_pit.Controls.Add(checkBox_max_stint);
            groupBox_pit.Controls.Add(comboBox_pit_options);
            groupBox_pit.Controls.Add(textBox_max_stint);
            groupBox_pit.Controls.Add(label_pits_count);
            groupBox_pit.Controls.Add(label_max_stint_min);
            groupBox_pit.Controls.Add(numericUpDown_pits);
            groupBox_pit.Controls.Add(label_max_stint);
            groupBox_pit.Controls.Add(label_pits_options);
            groupBox_pit.Font = new Font("Consolas", 11.25F, FontStyle.Bold);
            groupBox_pit.Location = new Point(21, 411);
            groupBox_pit.Name = "groupBox_pit";
            groupBox_pit.Size = new Size(454, 128);
            groupBox_pit.TabIndex = 13;
            groupBox_pit.TabStop = false;
            groupBox_pit.Text = "Pit stops";
            // 
            // checkBox_max_stint
            // 
            checkBox_max_stint.AutoSize = true;
            checkBox_max_stint.Location = new Point(290, 85);
            checkBox_max_stint.Name = "checkBox_max_stint";
            checkBox_max_stint.Size = new Size(15, 14);
            checkBox_max_stint.TabIndex = 0;
            checkBox_max_stint.UseVisualStyleBackColor = true;
            checkBox_max_stint.Click += CheckBox_max_stint_Click;
            // 
            // comboBox_pit_options
            // 
            comboBox_pit_options.Font = new Font("Consolas", 11.25F);
            comboBox_pit_options.FormattingEnabled = true;
            comboBox_pit_options.Location = new Point(103, 80);
            comboBox_pit_options.Name = "comboBox_pit_options";
            comboBox_pit_options.Size = new Size(177, 26);
            comboBox_pit_options.TabIndex = 12;
            // 
            // textBox_max_stint
            // 
            textBox_max_stint.Enabled = false;
            textBox_max_stint.Font = new Font("Consolas", 11.25F);
            textBox_max_stint.Location = new Point(311, 80);
            textBox_max_stint.MaxLength = 2;
            textBox_max_stint.Name = "textBox_max_stint";
            textBox_max_stint.Size = new Size(130, 25);
            textBox_max_stint.TabIndex = 17;
            textBox_max_stint.Text = "0";
            textBox_max_stint.TextAlign = HorizontalAlignment.Center;
            textBox_max_stint.KeyPress += TextBox_max_stint_KeyPress;
            // 
            // label_pits_count
            // 
            label_pits_count.BackColor = Color.Silver;
            label_pits_count.BorderStyle = BorderStyle.FixedSingle;
            label_pits_count.Font = new Font("Consolas", 11.25F);
            label_pits_count.Location = new Point(11, 31);
            label_pits_count.Name = "label_pits_count";
            label_pits_count.Size = new Size(84, 42);
            label_pits_count.TabIndex = 10;
            label_pits_count.Text = "Number of pit stops";
            label_pits_count.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_max_stint_min
            // 
            label_max_stint_min.AutoSize = true;
            label_max_stint_min.Font = new Font("Consolas", 11.25F);
            label_max_stint_min.Location = new Point(311, 61);
            label_max_stint_min.Name = "label_max_stint_min";
            label_max_stint_min.Size = new Size(32, 18);
            label_max_stint_min.TabIndex = 18;
            label_max_stint_min.Text = "Min";
            // 
            // numericUpDown_pits
            // 
            numericUpDown_pits.Font = new Font("Consolas", 11.25F);
            numericUpDown_pits.Location = new Point(11, 82);
            numericUpDown_pits.Name = "numericUpDown_pits";
            numericUpDown_pits.Size = new Size(85, 25);
            numericUpDown_pits.TabIndex = 11;
            numericUpDown_pits.TextAlign = HorizontalAlignment.Center;
            numericUpDown_pits.KeyPress += NumericUpDown_pits_KeyPress;
            // 
            // label_max_stint
            // 
            label_max_stint.BackColor = Color.Silver;
            label_max_stint.BorderStyle = BorderStyle.FixedSingle;
            label_max_stint.Font = new Font("Consolas", 11.25F);
            label_max_stint.Location = new Point(287, 31);
            label_max_stint.Name = "label_max_stint";
            label_max_stint.Size = new Size(154, 22);
            label_max_stint.TabIndex = 19;
            label_max_stint.Text = "Max stint duration";
            label_max_stint.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_pits_options
            // 
            label_pits_options.BackColor = Color.Silver;
            label_pits_options.BorderStyle = BorderStyle.FixedSingle;
            label_pits_options.Font = new Font("Consolas", 11.25F);
            label_pits_options.Location = new Point(103, 31);
            label_pits_options.Name = "label_pits_options";
            label_pits_options.Size = new Size(177, 42);
            label_pits_options.TabIndex = 10;
            label_pits_options.Text = " Pit stops  options";
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
            groupBox_variables.Font = new Font("Consolas", 11.25F, FontStyle.Bold);
            groupBox_variables.Location = new Point(21, 199);
            groupBox_variables.Name = "groupBox_variables";
            groupBox_variables.Size = new Size(454, 194);
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
            groupBox_car_track.Font = new Font("Consolas", 11.25F, FontStyle.Bold);
            groupBox_car_track.Location = new Point(21, 15);
            groupBox_car_track.Name = "groupBox_car_track";
            groupBox_car_track.Size = new Size(454, 164);
            groupBox_car_track.TabIndex = 11;
            groupBox_car_track.TabStop = false;
            groupBox_car_track.Text = "Car and track";
            // 
            // label_choose_track
            // 
            label_choose_track.BackColor = Color.Silver;
            label_choose_track.BorderStyle = BorderStyle.FixedSingle;
            label_choose_track.Font = new Font("Consolas", 11.25F);
            label_choose_track.Location = new Point(11, 89);
            label_choose_track.Name = "label_choose_track";
            label_choose_track.Size = new Size(429, 22);
            label_choose_track.TabIndex = 4;
            label_choose_track.Text = "Choose the track";
            label_choose_track.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // comboBox_class
            // 
            comboBox_class.Font = new Font("Consolas", 11.25F);
            comboBox_class.FormattingEnabled = true;
            comboBox_class.ImeMode = ImeMode.NoControl;
            comboBox_class.Location = new Point(11, 57);
            comboBox_class.Name = "comboBox_class";
            comboBox_class.Size = new Size(82, 26);
            comboBox_class.TabIndex = 2;
            comboBox_class.Text = "CLASS";
            comboBox_class.SelectedIndexChanged += ComboBox_class_SelectedIndexChanged;
            // 
            // comboBox_track
            // 
            comboBox_track.Font = new Font("Consolas", 11.25F);
            comboBox_track.FormattingEnabled = true;
            comboBox_track.Location = new Point(11, 114);
            comboBox_track.Name = "comboBox_track";
            comboBox_track.Size = new Size(429, 26);
            comboBox_track.TabIndex = 5;
            comboBox_track.Text = "TRACK";
            comboBox_track.SelectedIndexChanged += ComboBox_track_SelectedIndexChanged;
            // 
            // label_choose_class
            // 
            label_choose_class.BackColor = Color.Silver;
            label_choose_class.BorderStyle = BorderStyle.FixedSingle;
            label_choose_class.Font = new Font("Consolas", 11.25F);
            label_choose_class.Location = new Point(11, 30);
            label_choose_class.Name = "label_choose_class";
            label_choose_class.Size = new Size(82, 22);
            label_choose_class.TabIndex = 0;
            label_choose_class.Text = "Car class";
            label_choose_class.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_choose_car
            // 
            label_choose_car.BackColor = Color.Silver;
            label_choose_car.BorderStyle = BorderStyle.FixedSingle;
            label_choose_car.Font = new Font("Consolas", 11.25F);
            label_choose_car.Location = new Point(105, 30);
            label_choose_car.Name = "label_choose_car";
            label_choose_car.Size = new Size(336, 22);
            label_choose_car.TabIndex = 1;
            label_choose_car.Text = "Choose the car";
            label_choose_car.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // comboBox_car
            // 
            comboBox_car.Font = new Font("Consolas", 11.25F);
            comboBox_car.FormattingEnabled = true;
            comboBox_car.Location = new Point(105, 55);
            comboBox_car.Name = "comboBox_car";
            comboBox_car.Size = new Size(335, 26);
            comboBox_car.TabIndex = 3;
            comboBox_car.Text = "CAR";
            comboBox_car.SelectedIndexChanged += ComboBox_car_SelectedIndexChanged;
            // 
            // button_calculate
            // 
            button_calculate.BackColor = SystemColors.ActiveCaption;
            button_calculate.Font = new Font("Consolas", 11.25F);
            button_calculate.Location = new Point(21, 545);
            button_calculate.Name = "button_calculate";
            button_calculate.Size = new Size(454, 33);
            button_calculate.TabIndex = 10;
            button_calculate.Text = "Calculate";
            button_calculate.UseVisualStyleBackColor = false;
            button_calculate.Click += Button_calculate_Click;
            // 
            // panel_pit_stop_strategy
            // 
            panel_pit_stop_strategy.AutoScroll = true;
            panel_pit_stop_strategy.BorderStyle = BorderStyle.FixedSingle;
            panel_pit_stop_strategy.Location = new Point(16, 221);
            panel_pit_stop_strategy.Name = "panel_pit_stop_strategy";
            panel_pit_stop_strategy.Size = new Size(497, 357);
            panel_pit_stop_strategy.TabIndex = 8;
            // 
            // label_pit_stops
            // 
            label_pit_stops.BackColor = Color.Silver;
            label_pit_stops.BorderStyle = BorderStyle.FixedSingle;
            label_pit_stops.Font = new Font("Consolas", 11.25F);
            label_pit_stops.Location = new Point(16, 196);
            label_pit_stops.Name = "label_pit_stops";
            label_pit_stops.Size = new Size(497, 22);
            label_pit_stops.TabIndex = 7;
            label_pit_stops.Text = "Pit Stop Strategy";
            label_pit_stops.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel_duration_laps_result
            // 
            tableLayoutPanel_duration_laps_result.BackColor = Color.Gainsboro;
            tableLayoutPanel_duration_laps_result.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;
            tableLayoutPanel_duration_laps_result.ColumnCount = 4;
            tableLayoutPanel_duration_laps_result.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68.49728F));
            tableLayoutPanel_duration_laps_result.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 31.50272F));
            tableLayoutPanel_duration_laps_result.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 151F));
            tableLayoutPanel_duration_laps_result.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
            tableLayoutPanel_duration_laps_result.Controls.Add(label_overall_duration, 0, 0);
            tableLayoutPanel_duration_laps_result.Controls.Add(label_laps_result, 3, 0);
            tableLayoutPanel_duration_laps_result.Controls.Add(label_laps_number, 2, 0);
            tableLayoutPanel_duration_laps_result.Controls.Add(label_overall_result, 1, 0);
            tableLayoutPanel_duration_laps_result.Font = new Font("Consolas", 9.75F, FontStyle.Bold);
            tableLayoutPanel_duration_laps_result.Location = new Point(16, 15);
            tableLayoutPanel_duration_laps_result.Name = "tableLayoutPanel_duration_laps_result";
            tableLayoutPanel_duration_laps_result.RowCount = 1;
            tableLayoutPanel_duration_laps_result.RowStyles.Add(new RowStyle(SizeType.Absolute, 39F));
            tableLayoutPanel_duration_laps_result.Size = new Size(497, 43);
            tableLayoutPanel_duration_laps_result.TabIndex = 1;
            // 
            // label_overall_duration
            // 
            label_overall_duration.AutoSize = true;
            label_overall_duration.Dock = DockStyle.Fill;
            label_overall_duration.Font = new Font("Consolas", 11.25F);
            label_overall_duration.Location = new Point(5, 2);
            label_overall_duration.Name = "label_overall_duration";
            label_overall_duration.Size = new Size(176, 39);
            label_overall_duration.TabIndex = 2;
            label_overall_duration.Text = "Overall race duration";
            label_overall_duration.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_laps_result
            // 
            label_laps_result.AutoSize = true;
            label_laps_result.Dock = DockStyle.Fill;
            label_laps_result.Font = new Font("Consolas", 11.25F);
            label_laps_result.Location = new Point(427, 2);
            label_laps_result.Name = "label_laps_result";
            label_laps_result.Size = new Size(65, 39);
            label_laps_result.TabIndex = 4;
            label_laps_result.Text = "0";
            label_laps_result.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_laps_number
            // 
            label_laps_number.AutoSize = true;
            label_laps_number.Dock = DockStyle.Fill;
            label_laps_number.Font = new Font("Consolas", 11.25F);
            label_laps_number.Location = new Point(274, 2);
            label_laps_number.Name = "label_laps_number";
            label_laps_number.Size = new Size(145, 39);
            label_laps_number.TabIndex = 3;
            label_laps_number.Text = "Number of laps";
            label_laps_number.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_overall_result
            // 
            label_overall_result.AutoSize = true;
            label_overall_result.Dock = DockStyle.Fill;
            label_overall_result.Font = new Font("Consolas", 11.25F);
            label_overall_result.Location = new Point(189, 2);
            label_overall_result.Name = "label_overall_result";
            label_overall_result.Size = new Size(77, 39);
            label_overall_result.TabIndex = 3;
            label_overall_result.Text = "00:00:00";
            label_overall_result.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // table_fuel_results
            // 
            table_fuel_results.BackColor = Color.Gainsboro;
            table_fuel_results.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;
            table_fuel_results.ColumnCount = 4;
            table_fuel_results.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68.5F));
            table_fuel_results.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 31.5F));
            table_fuel_results.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 151F));
            table_fuel_results.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
            table_fuel_results.Controls.Add(label_lap_time_restult, 0, 0);
            table_fuel_results.Controls.Add(label_minus1_fuel_result, 3, 2);
            table_fuel_results.Controls.Add(label_plus1_fuel_result, 3, 1);
            table_fuel_results.Controls.Add(label_fuel_for_plus1, 2, 1);
            table_fuel_results.Controls.Add(label_plus1_lap_time_result, 1, 1);
            table_fuel_results.Controls.Add(label_lap_time_plus1, 0, 1);
            table_fuel_results.Controls.Add(label_fuel_race_result, 3, 0);
            table_fuel_results.Controls.Add(label_lap_time_minus1, 0, 2);
            table_fuel_results.Controls.Add(label_minus1_lap_time_result, 1, 2);
            table_fuel_results.Controls.Add(label_fuel_for_minus1, 2, 2);
            table_fuel_results.Controls.Add(label_lap_time_result2, 1, 0);
            table_fuel_results.Controls.Add(label_fuel_for_the_race, 2, 0);
            table_fuel_results.Font = new Font("Consolas", 9.75F, FontStyle.Bold);
            table_fuel_results.Location = new Point(16, 72);
            table_fuel_results.Name = "table_fuel_results";
            table_fuel_results.RowCount = 3;
            table_fuel_results.RowStyles.Add(new RowStyle(SizeType.Absolute, 31F));
            table_fuel_results.RowStyles.Add(new RowStyle(SizeType.Absolute, 31F));
            table_fuel_results.RowStyles.Add(new RowStyle(SizeType.Absolute, 31F));
            table_fuel_results.Size = new Size(497, 107);
            table_fuel_results.TabIndex = 0;
            // 
            // label_lap_time_restult
            // 
            label_lap_time_restult.AutoSize = true;
            label_lap_time_restult.Dock = DockStyle.Fill;
            label_lap_time_restult.Font = new Font("Consolas", 11.25F);
            label_lap_time_restult.Location = new Point(5, 2);
            label_lap_time_restult.Name = "label_lap_time_restult";
            label_lap_time_restult.Size = new Size(176, 31);
            label_lap_time_restult.TabIndex = 7;
            label_lap_time_restult.Text = "Lap time";
            label_lap_time_restult.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_minus1_fuel_result
            // 
            label_minus1_fuel_result.AutoSize = true;
            label_minus1_fuel_result.Dock = DockStyle.Fill;
            label_minus1_fuel_result.Font = new Font("Consolas", 11.25F);
            label_minus1_fuel_result.Location = new Point(427, 68);
            label_minus1_fuel_result.Name = "label_minus1_fuel_result";
            label_minus1_fuel_result.Size = new Size(65, 37);
            label_minus1_fuel_result.TabIndex = 6;
            label_minus1_fuel_result.Text = "0 L";
            label_minus1_fuel_result.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_plus1_fuel_result
            // 
            label_plus1_fuel_result.AutoSize = true;
            label_plus1_fuel_result.Dock = DockStyle.Fill;
            label_plus1_fuel_result.Font = new Font("Consolas", 11.25F);
            label_plus1_fuel_result.Location = new Point(427, 35);
            label_plus1_fuel_result.Name = "label_plus1_fuel_result";
            label_plus1_fuel_result.Size = new Size(65, 31);
            label_plus1_fuel_result.TabIndex = 6;
            label_plus1_fuel_result.Text = "0 L";
            label_plus1_fuel_result.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_fuel_for_plus1
            // 
            label_fuel_for_plus1.AutoSize = true;
            label_fuel_for_plus1.Dock = DockStyle.Fill;
            label_fuel_for_plus1.Font = new Font("Consolas", 11.25F);
            label_fuel_for_plus1.Location = new Point(274, 35);
            label_fuel_for_plus1.Name = "label_fuel_for_plus1";
            label_fuel_for_plus1.Size = new Size(145, 31);
            label_fuel_for_plus1.TabIndex = 10;
            label_fuel_for_plus1.Text = "Fuel for +1 lap";
            label_fuel_for_plus1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_plus1_lap_time_result
            // 
            label_plus1_lap_time_result.AutoSize = true;
            label_plus1_lap_time_result.Dock = DockStyle.Fill;
            label_plus1_lap_time_result.Font = new Font("Consolas", 11.25F);
            label_plus1_lap_time_result.Location = new Point(189, 35);
            label_plus1_lap_time_result.Name = "label_plus1_lap_time_result";
            label_plus1_lap_time_result.Size = new Size(77, 31);
            label_plus1_lap_time_result.TabIndex = 8;
            label_plus1_lap_time_result.Text = "0:00.000";
            label_plus1_lap_time_result.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_lap_time_plus1
            // 
            label_lap_time_plus1.AutoSize = true;
            label_lap_time_plus1.Dock = DockStyle.Fill;
            label_lap_time_plus1.Font = new Font("Consolas", 11.25F);
            label_lap_time_plus1.Location = new Point(5, 35);
            label_lap_time_plus1.Name = "label_lap_time_plus1";
            label_lap_time_plus1.Size = new Size(176, 31);
            label_lap_time_plus1.TabIndex = 6;
            label_lap_time_plus1.Text = "Lap time for +1 lap";
            label_lap_time_plus1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_fuel_race_result
            // 
            label_fuel_race_result.AutoSize = true;
            label_fuel_race_result.BackColor = Color.Gainsboro;
            label_fuel_race_result.Dock = DockStyle.Fill;
            label_fuel_race_result.Font = new Font("Consolas", 11.25F);
            label_fuel_race_result.Location = new Point(427, 2);
            label_fuel_race_result.Name = "label_fuel_race_result";
            label_fuel_race_result.Size = new Size(65, 31);
            label_fuel_race_result.TabIndex = 5;
            label_fuel_race_result.Text = "0 L";
            label_fuel_race_result.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_lap_time_minus1
            // 
            label_lap_time_minus1.AutoSize = true;
            label_lap_time_minus1.Dock = DockStyle.Fill;
            label_lap_time_minus1.Font = new Font("Consolas", 11.25F);
            label_lap_time_minus1.Location = new Point(5, 68);
            label_lap_time_minus1.Name = "label_lap_time_minus1";
            label_lap_time_minus1.Size = new Size(176, 37);
            label_lap_time_minus1.TabIndex = 7;
            label_lap_time_minus1.Text = "Lap time for -1 lap";
            label_lap_time_minus1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_minus1_lap_time_result
            // 
            label_minus1_lap_time_result.AutoSize = true;
            label_minus1_lap_time_result.Dock = DockStyle.Fill;
            label_minus1_lap_time_result.Font = new Font("Consolas", 11.25F);
            label_minus1_lap_time_result.Location = new Point(189, 68);
            label_minus1_lap_time_result.Name = "label_minus1_lap_time_result";
            label_minus1_lap_time_result.Size = new Size(77, 37);
            label_minus1_lap_time_result.TabIndex = 9;
            label_minus1_lap_time_result.Text = "0:00.000";
            label_minus1_lap_time_result.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_fuel_for_minus1
            // 
            label_fuel_for_minus1.AutoSize = true;
            label_fuel_for_minus1.Dock = DockStyle.Fill;
            label_fuel_for_minus1.Font = new Font("Consolas", 11.25F);
            label_fuel_for_minus1.Location = new Point(274, 68);
            label_fuel_for_minus1.Name = "label_fuel_for_minus1";
            label_fuel_for_minus1.Size = new Size(145, 37);
            label_fuel_for_minus1.TabIndex = 11;
            label_fuel_for_minus1.Text = "Fuel for -1 lap";
            label_fuel_for_minus1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_lap_time_result2
            // 
            label_lap_time_result2.AutoSize = true;
            label_lap_time_result2.Dock = DockStyle.Fill;
            label_lap_time_result2.Font = new Font("Consolas", 11.25F);
            label_lap_time_result2.Location = new Point(189, 2);
            label_lap_time_result2.Name = "label_lap_time_result2";
            label_lap_time_result2.Size = new Size(77, 31);
            label_lap_time_result2.TabIndex = 9;
            label_lap_time_result2.Text = "0:00.000";
            label_lap_time_result2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_fuel_for_the_race
            // 
            label_fuel_for_the_race.AutoSize = true;
            label_fuel_for_the_race.Dock = DockStyle.Fill;
            label_fuel_for_the_race.Font = new Font("Consolas", 11.25F);
            label_fuel_for_the_race.Location = new Point(274, 2);
            label_fuel_for_the_race.Name = "label_fuel_for_the_race";
            label_fuel_for_the_race.Size = new Size(145, 31);
            label_fuel_for_the_race.TabIndex = 4;
            label_fuel_for_the_race.Text = "Fuel for the race";
            label_fuel_for_the_race.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_input_data
            // 
            label_input_data.BackColor = SystemColors.ActiveBorder;
            label_input_data.BorderStyle = BorderStyle.FixedSingle;
            label_input_data.Font = new Font("Consolas", 11.25F);
            label_input_data.Location = new Point(14, 255);
            label_input_data.Name = "label_input_data";
            label_input_data.Size = new Size(497, 22);
            label_input_data.TabIndex = 9;
            label_input_data.Text = "Input data";
            label_input_data.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_results
            // 
            label_results.BackColor = SystemColors.ActiveBorder;
            label_results.BorderStyle = BorderStyle.FixedSingle;
            label_results.Font = new Font("Consolas", 11.25F);
            label_results.Location = new Point(516, 255);
            label_results.Name = "label_results";
            label_results.Size = new Size(538, 22);
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
            // menuStrip_game_status
            // 
            menuStrip_game_status.Items.AddRange(new ToolStripItem[] { toolStripMenuItem_menu, ToolStripMenuItem_help, ToolStripMenuItem_github, toolStripMenuItem_empty, ToolStripMenuItem_game_status });
            menuStrip_game_status.Location = new Point(0, 0);
            menuStrip_game_status.Name = "menuStrip_game_status";
            menuStrip_game_status.Padding = new Padding(7, 2, 0, 2);
            menuStrip_game_status.Size = new Size(1064, 26);
            menuStrip_game_status.TabIndex = 12;
            menuStrip_game_status.Text = "ACC OFF";
            // 
            // toolStripMenuItem_menu
            // 
            toolStripMenuItem_menu.BackColor = Color.Gainsboro;
            toolStripMenuItem_menu.DropDownItems.AddRange(new ToolStripItem[] { ignoreInvalidLapsToolStripMenuItem, telemetryDisabledToolStripMenuItem, resetDataToolStripMenuItem, exitToolStripMenuItem });
            toolStripMenuItem_menu.Font = new Font("Consolas", 11.25F);
            toolStripMenuItem_menu.Name = "toolStripMenuItem_menu";
            toolStripMenuItem_menu.Size = new Size(52, 22);
            toolStripMenuItem_menu.Text = "Menu";
            // 
            // ignoreInvalidLapsToolStripMenuItem
            // 
            ignoreInvalidLapsToolStripMenuItem.Name = "ignoreInvalidLapsToolStripMenuItem";
            ignoreInvalidLapsToolStripMenuItem.RightToLeft = RightToLeft.No;
            ignoreInvalidLapsToolStripMenuItem.Size = new Size(228, 22);
            ignoreInvalidLapsToolStripMenuItem.Text = "Ignore invalid laps";
            ignoreInvalidLapsToolStripMenuItem.Click += ignoreInvalidLaToolStripMenuItem_Click;
            // 
            // resetDataToolStripMenuItem
            // 
            resetDataToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { resetAllDataToolStripMenuItem, resetCurrentCartrackToolStripMenuItem });
            resetDataToolStripMenuItem.Name = "resetDataToolStripMenuItem";
            resetDataToolStripMenuItem.Size = new Size(228, 22);
            resetDataToolStripMenuItem.Text = "Reset data";
            // 
            // resetAllDataToolStripMenuItem
            // 
            resetAllDataToolStripMenuItem.Name = "resetAllDataToolStripMenuItem";
            resetAllDataToolStripMenuItem.Size = new Size(260, 22);
            resetAllDataToolStripMenuItem.Text = "Reset all data";
            resetAllDataToolStripMenuItem.Click += ResetAllDataToolStripMenuItem_Click;
            // 
            // resetCurrentCartrackToolStripMenuItem
            // 
            resetCurrentCartrackToolStripMenuItem.Name = "resetCurrentCartrackToolStripMenuItem";
            resetCurrentCartrackToolStripMenuItem.Size = new Size(260, 22);
            resetCurrentCartrackToolStripMenuItem.Text = "Reset current car/track";
            resetCurrentCartrackToolStripMenuItem.Click += ResetCurrentCartrackToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(228, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            // 
            // ToolStripMenuItem_help
            // 
            ToolStripMenuItem_help.BackColor = Color.Gainsboro;
            ToolStripMenuItem_help.Font = new Font("Consolas", 11.25F);
            ToolStripMenuItem_help.Name = "ToolStripMenuItem_help";
            ToolStripMenuItem_help.Size = new Size(52, 22);
            ToolStripMenuItem_help.Text = "Help";
            ToolStripMenuItem_help.Click += HelpToolStripMenuItem_Click;
            // 
            // ToolStripMenuItem_github
            // 
            ToolStripMenuItem_github.BackColor = Color.Gainsboro;
            ToolStripMenuItem_github.Font = new Font("Consolas", 11.25F);
            ToolStripMenuItem_github.Name = "ToolStripMenuItem_github";
            ToolStripMenuItem_github.Size = new Size(68, 22);
            ToolStripMenuItem_github.Text = "GitHub";
            ToolStripMenuItem_github.Click += GitHubToolStripMenuItem_Click;
            // 
            // toolStripMenuItem_empty
            // 
            toolStripMenuItem_empty.Enabled = false;
            toolStripMenuItem_empty.Name = "toolStripMenuItem_empty";
            toolStripMenuItem_empty.Size = new Size(24, 22);
            toolStripMenuItem_empty.Text = " ";
            // 
            // ToolStripMenuItem_game_status
            // 
            ToolStripMenuItem_game_status.BackColor = Color.Gainsboro;
            ToolStripMenuItem_game_status.Font = new Font("Consolas", 11.25F);
            ToolStripMenuItem_game_status.Name = "ToolStripMenuItem_game_status";
            ToolStripMenuItem_game_status.Size = new Size(100, 22);
            ToolStripMenuItem_game_status.Text = "ACC STATUS";
            ToolStripMenuItem_game_status.TextChanged += ToolStripMenuItem_game_status_TextChanged;
            // 
            // button_save_load
            // 
            button_save_load.Font = new Font("Consolas", 11.25F);
            button_save_load.Location = new Point(763, 114);
            button_save_load.Name = "button_save_load";
            button_save_load.Size = new Size(252, 53);
            button_save_load.TabIndex = 18;
            button_save_load.Text = "Save / Load strategy";
            button_save_load.UseVisualStyleBackColor = true;
            button_save_load.Click += Button_save_load_Click;
            // 
            // button_import_race
            // 
            button_import_race.Enabled = false;
            button_import_race.Font = new Font("Consolas", 11.25F);
            button_import_race.Location = new Point(501, 114);
            button_import_race.Name = "button_import_race";
            button_import_race.Size = new Size(251, 53);
            button_import_race.TabIndex = 20;
            button_import_race.Text = "Import current race";
            button_import_race.UseVisualStyleBackColor = true;
            button_import_race.Click += Button_import_race_Click;
            // 
            // button_auto
            // 
            button_auto.Enabled = false;
            button_auto.Font = new Font("Consolas", 11.25F);
            button_auto.Location = new Point(21, 114);
            button_auto.Name = "button_auto";
            button_auto.Size = new Size(173, 53);
            button_auto.TabIndex = 21;
            button_auto.Text = "AUTO";
            button_auto.UseVisualStyleBackColor = true;
            button_auto.Click += Button_auto_Click_1;
            // 
            // button_import_stint
            // 
            button_import_stint.Enabled = false;
            button_import_stint.Font = new Font("Consolas", 11.25F);
            button_import_stint.Location = new Point(200, 114);
            button_import_stint.Name = "button_import_stint";
            button_import_stint.Size = new Size(173, 53);
            button_import_stint.TabIndex = 22;
            button_import_stint.Text = "Import stint data";
            button_import_stint.UseVisualStyleBackColor = true;
            button_import_stint.Click += Button_import_stint_Click;
            // 
            // checkBox_lap_time
            // 
            checkBox_lap_time.Enabled = false;
            checkBox_lap_time.Font = new Font("Consolas", 11.25F);
            checkBox_lap_time.Location = new Point(379, 114);
            checkBox_lap_time.Name = "checkBox_lap_time";
            checkBox_lap_time.Size = new Size(115, 52);
            checkBox_lap_time.TabIndex = 23;
            checkBox_lap_time.Text = "Take my avg lap time";
            checkBox_lap_time.TextAlign = ContentAlignment.MiddleCenter;
            checkBox_lap_time.UseVisualStyleBackColor = true;
            // 
            // panel_telemetry
            // 
            panel_telemetry.BorderStyle = BorderStyle.FixedSingle;
            panel_telemetry.Controls.Add(button_save_load);
            panel_telemetry.Controls.Add(checkBox_lap_time);
            panel_telemetry.Controls.Add(button_import_race);
            panel_telemetry.Controls.Add(button_auto);
            panel_telemetry.Controls.Add(button_import_stint);
            panel_telemetry.Font = new Font("Consolas", 9.75F, FontStyle.Bold);
            panel_telemetry.Location = new Point(14, 69);
            panel_telemetry.Name = "panel_telemetry";
            panel_telemetry.Size = new Size(1039, 178);
            panel_telemetry.TabIndex = 25;
            // 
            // label_telemetry
            // 
            label_telemetry.BackColor = SystemColors.ActiveBorder;
            label_telemetry.BorderStyle = BorderStyle.FixedSingle;
            label_telemetry.Font = new Font("Consolas", 11.25F);
            label_telemetry.Location = new Point(14, 44);
            label_telemetry.Name = "label_telemetry";
            label_telemetry.Size = new Size(1039, 22);
            label_telemetry.TabIndex = 10;
            label_telemetry.Text = "Recent stints";
            label_telemetry.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // telemetryDisabledToolStripMenuItem
            // 
            telemetryDisabledToolStripMenuItem.Name = "telemetryDisabledToolStripMenuItem";
            telemetryDisabledToolStripMenuItem.Size = new Size(228, 22);
            telemetryDisabledToolStripMenuItem.Text = "Telemetry disabled";
            telemetryDisabledToolStripMenuItem.Click += telemetryDisabledToolStripMenuItem_Click;
            // 
            // FuelStrat
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1064, 885);
            Controls.Add(label_telemetry);
            Controls.Add(panel_telemetry);
            Controls.Add(label_results);
            Controls.Add(label_input_data);
            Controls.Add(splitContainer_input_result);
            Controls.Add(menuStrip_game_status);
            Font = new Font("Consolas", 11.25F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FuelStrat";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FuelStrat - Fuel and strategy for ACC";
            Load += Form_Load;
            Shown += Form_Shown;
            splitContainer_input_result.Panel1.ResumeLayout(false);
            splitContainer_input_result.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer_input_result).EndInit();
            splitContainer_input_result.ResumeLayout(false);
            groupBox_pit.ResumeLayout(false);
            groupBox_pit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_pits).EndInit();
            groupBox_variables.ResumeLayout(false);
            groupBox_variables.PerformLayout();
            groupBox_car_track.ResumeLayout(false);
            tableLayoutPanel_duration_laps_result.ResumeLayout(false);
            tableLayoutPanel_duration_laps_result.PerformLayout();
            table_fuel_results.ResumeLayout(false);
            table_fuel_results.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            menuStrip_game_status.ResumeLayout(false);
            menuStrip_game_status.PerformLayout();
            panel_telemetry.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label_lap_time;
        private Label label_race_duration;
        private Label label_fuel_per_lap;
        private Label label_formation;
        private SplitContainer splitContainer_input_result;
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
        private TableLayoutPanel tableLayoutPanel_duration_laps_result;
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
        private Label label_lap_time_restult;
        private Label label_lap_time_result2;
        private MenuStrip menuStrip1;
        private MenuStrip menuStrip_game_status;
        private ToolStripMenuItem toolStripMenuItem_menu;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem ToolStripMenuItem_help;
        private TextBox textBox_max_stint;
        private Label label_max_stint_min;
        private Label label_max_stint;
        private CheckBox checkBox_max_stint;
        private ToolStripMenuItem ToolStripMenuItem_github;
        private ToolStripMenuItem resetDataToolStripMenuItem;
        private ToolStripMenuItem resetAllDataToolStripMenuItem;
        private ToolStripMenuItem resetCurrentCartrackToolStripMenuItem;
        private Button button_save_load;
        private Button button_import_race;
        private ToolStripMenuItem ToolStripMenuItem_game_status;
        private ToolStripMenuItem toolStripMenuItem_empty;
        private Button button_auto;
        private Button button_import_stint;
        private CheckBox checkBox_lap_time;
        private Panel panel_telemetry;
        private Label label_telemetry;
        private ToolStripMenuItem ignoreInvalidLapsToolStripMenuItem;
        private ToolStripMenuItem telemetryDisabledToolStripMenuItem;
    }
}
