namespace FuelStrat
{
    partial class StintForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label_best = new Label();
            label_invalid = new Label();
            label_outlier = new Label();
            label_ignored = new Label();
            button_ignore_invalid = new Button();
            button_ignore_outliers = new Button();
            button_close = new Button();
            label_avg_lap = new Label();
            label_avg_fuel = new Label();
            button_revert = new Button();
            SuspendLayout();
            // 
            // label_best
            // 
            label_best.BackColor = Color.Transparent;
            label_best.BorderStyle = BorderStyle.FixedSingle;
            label_best.ForeColor = Color.Magenta;
            label_best.Location = new Point(15, 279);
            label_best.Name = "label_best";
            label_best.Size = new Size(114, 25);
            label_best.TabIndex = 1;
            label_best.Text = "Best lap time";
            label_best.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_invalid
            // 
            label_invalid.BackColor = Color.Transparent;
            label_invalid.BorderStyle = BorderStyle.FixedSingle;
            label_invalid.Font = new Font("Consolas", 11.25F);
            label_invalid.ForeColor = Color.DarkOrange;
            label_invalid.Location = new Point(187, 279);
            label_invalid.Name = "label_invalid";
            label_invalid.Size = new Size(98, 25);
            label_invalid.TabIndex = 2;
            label_invalid.Text = "Invalid lap";
            label_invalid.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_outlier
            // 
            label_outlier.BackColor = Color.Transparent;
            label_outlier.BorderStyle = BorderStyle.FixedSingle;
            label_outlier.Font = new Font("Consolas", 11.25F);
            label_outlier.ForeColor = Color.Chocolate;
            label_outlier.Location = new Point(344, 279);
            label_outlier.Name = "label_outlier";
            label_outlier.Size = new Size(66, 25);
            label_outlier.TabIndex = 3;
            label_outlier.Text = "Outlier";
            label_outlier.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_ignored
            // 
            label_ignored.BackColor = Color.Transparent;
            label_ignored.BorderStyle = BorderStyle.FixedSingle;
            label_ignored.Font = new Font("Consolas", 11.25F);
            label_ignored.ForeColor = Color.Red;
            label_ignored.Location = new Point(469, 279);
            label_ignored.Name = "label_ignored";
            label_ignored.Size = new Size(66, 25);
            label_ignored.TabIndex = 4;
            label_ignored.Text = "Ignored";
            label_ignored.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button_ignore_invalid
            // 
            button_ignore_invalid.Location = new Point(15, 12);
            button_ignore_invalid.Name = "button_ignore_invalid";
            button_ignore_invalid.Size = new Size(170, 25);
            button_ignore_invalid.TabIndex = 6;
            button_ignore_invalid.Text = "Ignore invalid laps";
            button_ignore_invalid.UseVisualStyleBackColor = true;
            button_ignore_invalid.Click += button_ignore_invalid_Click;
            // 
            // button_ignore_outliers
            // 
            button_ignore_outliers.Location = new Point(190, 12);
            button_ignore_outliers.Name = "button_ignore_outliers";
            button_ignore_outliers.Size = new Size(170, 25);
            button_ignore_outliers.TabIndex = 7;
            button_ignore_outliers.Text = "Ignore outliers";
            button_ignore_outliers.UseVisualStyleBackColor = true;
            button_ignore_outliers.Click += button_ignore_outliers_Click;
            // 
            // button_close
            // 
            button_close.BackColor = SystemColors.ActiveCaption;
            button_close.Location = new Point(15, 310);
            button_close.Name = "button_close";
            button_close.Size = new Size(520, 25);
            button_close.TabIndex = 8;
            button_close.Text = "Close and save this stint";
            button_close.UseVisualStyleBackColor = false;
            button_close.Click += button_close_Click;
            // 
            // label_avg_lap
            // 
            label_avg_lap.BackColor = Color.Transparent;
            label_avg_lap.BorderStyle = BorderStyle.FixedSingle;
            label_avg_lap.ForeColor = Color.Black;
            label_avg_lap.Location = new Point(15, 42);
            label_avg_lap.Name = "label_avg_lap";
            label_avg_lap.Size = new Size(255, 25);
            label_avg_lap.TabIndex = 9;
            label_avg_lap.Text = "Avg lap time: ";
            label_avg_lap.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_avg_fuel
            // 
            label_avg_fuel.BackColor = Color.Transparent;
            label_avg_fuel.BorderStyle = BorderStyle.FixedSingle;
            label_avg_fuel.ForeColor = Color.Black;
            label_avg_fuel.Location = new Point(280, 42);
            label_avg_fuel.Name = "label_avg_fuel";
            label_avg_fuel.Size = new Size(255, 25);
            label_avg_fuel.TabIndex = 10;
            label_avg_fuel.Text = "Avg fuel per lap: ";
            label_avg_fuel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button_revert
            // 
            button_revert.Location = new Point(365, 12);
            button_revert.Name = "button_revert";
            button_revert.Size = new Size(170, 25);
            button_revert.TabIndex = 11;
            button_revert.Text = "Revert all changes";
            button_revert.UseVisualStyleBackColor = true;
            button_revert.Click += button_revert_Click;
            // 
            // StintForm
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(553, 340);
            Controls.Add(button_revert);
            Controls.Add(label_avg_fuel);
            Controls.Add(label_avg_lap);
            Controls.Add(button_close);
            Controls.Add(button_ignore_outliers);
            Controls.Add(button_ignore_invalid);
            Controls.Add(label_ignored);
            Controls.Add(label_outlier);
            Controls.Add(label_invalid);
            Controls.Add(label_best);
            Font = new Font("Consolas", 11.25F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "StintForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Stint";
            ResumeLayout(false);
        }

        #endregion
        private Label label_best;
        private Label label_invalid;
        private Label label_outlier;
        private Label label_ignored;
        private Button button_ignore_invalid;
        private Button button_ignore_outliers;
        private Button button_close;
        private Label label_avg_lap;
        private Label label_avg_fuel;
        private Button button_revert;
    }
}