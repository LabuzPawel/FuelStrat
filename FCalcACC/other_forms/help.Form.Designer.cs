namespace FCalcACC
{
    partial class FCalcACChelp
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
            button_close_help = new Button();
            panel1 = new Panel();
            SuspendLayout();
            // 
            // button_close_help
            // 
            button_close_help.BackColor = SystemColors.ActiveCaption;
            button_close_help.Font = new Font("Consolas", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
            button_close_help.Location = new Point(12, 518);
            button_close_help.Name = "button_close_help";
            button_close_help.Size = new Size(760, 34);
            button_close_help.TabIndex = 1;
            button_close_help.Text = "CLOSE";
            button_close_help.UseVisualStyleBackColor = false;
            button_close_help.Click += button_close_help_Click;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(760, 500);
            panel1.TabIndex = 2;
            // 
            // FCalcACChelp
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(panel1);
            Controls.Add(button_close_help);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "FCalcACChelp";
            StartPosition = FormStartPosition.CenterParent;
            Text = "FCalcACC - HELP";
            ResumeLayout(false);
        }

        #endregion
        private Button button_close_help;
        private Panel panel1;
    }
}