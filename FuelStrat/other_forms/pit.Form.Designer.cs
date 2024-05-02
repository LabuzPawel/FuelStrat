namespace FuelStrat
{
    partial class Pit_option
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
            label_pit_option1 = new Label();
            label_pit_option2 = new Label();
            button_Refuel_Tires = new Button();
            button_tires = new Button();
            button_refuel = new Button();
            button_1L_refuel = new Button();
            button_fixed_refuel = new Button();
            SuspendLayout();
            // 
            // label_pit_option1
            // 
            label_pit_option1.Font = new Font("Consolas", 11.25F);
            label_pit_option1.Location = new Point(14, 11);
            label_pit_option1.Name = "label_pit_option1";
            label_pit_option1.Size = new Size(685, 26);
            label_pit_option1.TabIndex = 0;
            label_pit_option1.Text = "In this race there are 0 mandatory pit stops.";
            label_pit_option1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_pit_option2
            // 
            label_pit_option2.Font = new Font("Consolas", 11.25F);
            label_pit_option2.Location = new Point(14, 37);
            label_pit_option2.Name = "label_pit_option2";
            label_pit_option2.Size = new Size(685, 26);
            label_pit_option2.TabIndex = 1;
            label_pit_option2.Text = " Select your desired pit stop option:";
            label_pit_option2.TextAlign = ContentAlignment.TopCenter;
            // 
            // button_Refuel_Tires
            // 
            button_Refuel_Tires.Font = new Font("Consolas", 11.25F);
            button_Refuel_Tires.Location = new Point(14, 67);
            button_Refuel_Tires.Margin = new Padding(3, 4, 3, 4);
            button_Refuel_Tires.Name = "button_Refuel_Tires";
            button_Refuel_Tires.Size = new Size(131, 54);
            button_Refuel_Tires.TabIndex = 2;
            button_Refuel_Tires.Text = "Refuel + Tires";
            button_Refuel_Tires.UseVisualStyleBackColor = true;
            button_Refuel_Tires.Click += button_Refuel_Tires_Click;
            // 
            // button_tires
            // 
            button_tires.Font = new Font("Consolas", 11.25F);
            button_tires.Location = new Point(152, 67);
            button_tires.Margin = new Padding(3, 4, 3, 4);
            button_tires.Name = "button_tires";
            button_tires.Size = new Size(131, 54);
            button_tires.TabIndex = 3;
            button_tires.Text = "Tires only";
            button_tires.UseVisualStyleBackColor = true;
            button_tires.Click += button_tires_Click;
            // 
            // button_refuel
            // 
            button_refuel.Font = new Font("Consolas", 11.25F);
            button_refuel.Location = new Point(290, 67);
            button_refuel.Margin = new Padding(3, 4, 3, 4);
            button_refuel.Name = "button_refuel";
            button_refuel.Size = new Size(131, 54);
            button_refuel.TabIndex = 4;
            button_refuel.Text = "Refuel only";
            button_refuel.UseVisualStyleBackColor = true;
            button_refuel.Click += button_refuel_Click;
            // 
            // button_1L_refuel
            // 
            button_1L_refuel.Font = new Font("Consolas", 11.25F);
            button_1L_refuel.Location = new Point(567, 67);
            button_1L_refuel.Margin = new Padding(3, 4, 3, 4);
            button_1L_refuel.Name = "button_1L_refuel";
            button_1L_refuel.Size = new Size(131, 54);
            button_1L_refuel.TabIndex = 5;
            button_1L_refuel.Text = "1L refuel";
            button_1L_refuel.UseVisualStyleBackColor = true;
            button_1L_refuel.Click += button_1L_refuel_Click;
            // 
            // button_fixed_refuel
            // 
            button_fixed_refuel.Font = new Font("Consolas", 11.25F);
            button_fixed_refuel.Location = new Point(429, 67);
            button_fixed_refuel.Margin = new Padding(3, 4, 3, 4);
            button_fixed_refuel.Name = "button_fixed_refuel";
            button_fixed_refuel.Size = new Size(131, 54);
            button_fixed_refuel.TabIndex = 6;
            button_fixed_refuel.Text = "Fixed refuel";
            button_fixed_refuel.UseVisualStyleBackColor = true;
            button_fixed_refuel.Click += button_fixed_refuel_Click;
            // 
            // Pit_option
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(714, 138);
            Controls.Add(button_fixed_refuel);
            Controls.Add(button_1L_refuel);
            Controls.Add(button_refuel);
            Controls.Add(button_tires);
            Controls.Add(button_Refuel_Tires);
            Controls.Add(label_pit_option2);
            Controls.Add(label_pit_option1);
            Font = new Font("Consolas", 11.25F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            Name = "Pit_option";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Pit stop option";
            ResumeLayout(false);
        }

        #endregion

        private Label label_pit_option1;
        private Label label_pit_option2;
        private Button button_Refuel_Tires;
        private Button button_tires;
        private Button button_refuel;
        private Button button_1L_refuel;
        private Button button_fixed_refuel;
    }
}