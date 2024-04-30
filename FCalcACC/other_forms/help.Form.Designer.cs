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
            richTextBox_help = new RichTextBox();
            button_close_help = new Button();
            SuspendLayout();
            // 
            // richTextBox_help
            // 
            richTextBox_help.Font = new Font("Consolas", 12F);
            richTextBox_help.Location = new Point(12, 12);
            richTextBox_help.Name = "richTextBox_help";
            richTextBox_help.Size = new Size(760, 500);
            richTextBox_help.TabIndex = 0;
            richTextBox_help.Text = "";
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
            // FCalcACChelp
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(button_close_help);
            Controls.Add(richTextBox_help);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "FCalcACChelp";
            StartPosition = FormStartPosition.CenterParent;
            Text = "FCalcACC - HELP";
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox richTextBox_help;
        private Button button_close_help;

    }
}