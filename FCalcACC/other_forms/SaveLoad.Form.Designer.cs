namespace FCalcACC
{
    partial class SaveLoad
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
            components = new System.ComponentModel.Container();
            label_save_load = new Label();
            listBox_save_load = new ListBox();
            button_save = new Button();
            button_load = new Button();
            textBox_save = new TextBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            button_close = new Button();
            SuspendLayout();
            // 
            // label_save_load
            // 
            label_save_load.AutoSize = true;
            label_save_load.Font = new Font("Consolas", 13.75F);
            label_save_load.Location = new Point(12, 9);
            label_save_load.Name = "label_save_load";
            label_save_load.Size = new Size(310, 22);
            label_save_load.TabIndex = 0;
            label_save_load.Text = "Select a slot to save or load:";
            // 
            // listBox_save_load
            // 
            listBox_save_load.Font = new Font("Consolas", 13.75F);
            listBox_save_load.FormattingEnabled = true;
            listBox_save_load.ItemHeight = 22;
            listBox_save_load.Location = new Point(12, 34);
            listBox_save_load.Name = "listBox_save_load";
            listBox_save_load.Size = new Size(426, 224);
            listBox_save_load.TabIndex = 1;
            listBox_save_load.SelectedIndexChanged += listBox_save_load_SelectedIndexChanged;
            // 
            // button_save
            // 
            button_save.Enabled = false;
            button_save.Font = new Font("Consolas", 9.75F, FontStyle.Bold);
            button_save.Location = new Point(133, 269);
            button_save.Name = "button_save";
            button_save.Size = new Size(115, 30);
            button_save.TabIndex = 2;
            button_save.Text = "Save";
            button_save.UseVisualStyleBackColor = true;
            button_save.Click += button_save_Click;
            // 
            // button_load
            // 
            button_load.Enabled = false;
            button_load.Font = new Font("Consolas", 9.75F, FontStyle.Bold);
            button_load.Location = new Point(12, 269);
            button_load.Name = "button_load";
            button_load.Size = new Size(115, 30);
            button_load.TabIndex = 3;
            button_load.Text = "Load";
            button_load.UseVisualStyleBackColor = true;
            button_load.Click += button_load_Click;
            // 
            // textBox_save
            // 
            textBox_save.Font = new Font("Consolas", 11.25F, FontStyle.Italic, GraphicsUnit.Point, 238);
            textBox_save.Location = new Point(253, 272);
            textBox_save.MaxLength = 39;
            textBox_save.Name = "textBox_save";
            textBox_save.Size = new Size(184, 25);
            textBox_save.TabIndex = 4;
            textBox_save.Text = "save name";
            textBox_save.TextAlign = HorizontalAlignment.Center;
            textBox_save.Click += textBox_save_Click;
            textBox_save.TextChanged += textBox_save_TextChanged;
            textBox_save.KeyDown += textBox_save_KeyDown;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // button_close
            // 
            button_close.Font = new Font("Consolas", 9.75F, FontStyle.Bold);
            button_close.Location = new Point(12, 305);
            button_close.Name = "button_close";
            button_close.Size = new Size(425, 30);
            button_close.TabIndex = 5;
            button_close.Text = "Close";
            button_close.UseVisualStyleBackColor = true;
            button_close.Click += button_close_Click;
            // 
            // SaveLoad
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(452, 344);
            Controls.Add(button_close);
            Controls.Add(textBox_save);
            Controls.Add(button_load);
            Controls.Add(button_save);
            Controls.Add(listBox_save_load);
            Controls.Add(label_save_load);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "SaveLoad";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Save/Load strategy";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label_save_load;
        private ListBox listBox_save_load;
        private Button button_save;
        private Button button_load;
        private TextBox textBox_save;
        private ContextMenuStrip contextMenuStrip1;
        private Button button_close;
    }
}