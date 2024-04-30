using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FCalcACC
{
    public partial class SaveLoad : Form
    {

        List<FCalcACC.SavedStrategy> default_saved_strat_list;
        List<FCalcACC.SavedStrategy> saved_strat_list;
        FCalcACC.SavedStrategy current_strat;
        public delegate void LoadButtonClickedEventHandler (object sender, int slot);
        public event LoadButtonClickedEventHandler LoadButtonClicked;

        public SaveLoad(FCalcACC.SavedStrategy currentStrat)
        {
            InitializeComponent();
            this.current_strat = currentStrat;
            CreateOrLoadSavedJson();
        }

        public void listBox_save_load_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_save_load.SelectedIndex != -1 && listBox_save_load.Text.Contains(" EMPTY        ") != true)
            {
                button_load.Enabled = true;
            }
            else
            {
                button_load.Enabled = false;
            }

            if (listBox_save_load.SelectedIndex != -1 && textBox_save.Text != "save name" &&
                textBox_save.Text != "")
            {
                button_save.Enabled = true;
            }
        }

        private void textBox_save_TextChanged(object sender, EventArgs e)
        {
            if (textBox_save.Text != "")
            {
                button_save.Enabled = true;
            }
            else
            {
                button_save.Enabled = false;
            }
        }

        private void textBox_save_Click(object sender, EventArgs e)
        {
            textBox_save.Text = "";
            textBox_save.Font = new Font(textBox_save.Font, FontStyle.Regular);
        }

        private void CreateOrLoadSavedJson()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("FCalcACC.json_resources.FCalcACC_saved_strats.json"))
            using (StreamReader reader = new StreamReader(stream))
            {
                string defualt_saved = reader.ReadToEnd();
                default_saved_strat_list = JsonConvert.DeserializeObject<List<FCalcACC.SavedStrategy>>(defualt_saved);
            }

            if (File.Exists("FCalcACC_saved_strats.json") == false)
            {
                string default_save_json = JsonConvert.SerializeObject(default_saved_strat_list, Formatting.Indented);
                File.WriteAllText("FCalcACC_saved_strats.json", default_save_json);

                foreach (var strat in default_saved_strat_list)
                {
                    listBox_save_load.Items.Add(strat.saved_name);
                }
            }
            else
            {
                try
                {
                    string saved_json = File.ReadAllText("FCalcACC_saved_strats.json");
                    saved_strat_list = JsonConvert.DeserializeObject<List<FCalcACC.SavedStrategy>>(saved_json);

                    foreach (var strat in saved_strat_list)
                    {
                        listBox_save_load.Items.Add(strat.saved_name);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error reading FCalcACC_saved_strats.json:\n" + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    DialogResult result = MessageBox.Show("Would you like to reset the FCalcACC_saved_strats.json?\n\n" +
                        "Choosing 'No' will close Save/Load window.",
                        "Reset data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        File.Delete("FCalcACC_saved_strats.json");
                        CreateOrLoadSavedJson();
                    }
                    else if (result == DialogResult.No)
                    {
                        this.Close();
                    }
                }
            }
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_load_Click(object sender, EventArgs e)
        {
            int selected_slot = listBox_save_load.SelectedIndex;
            LoadButtonClicked?.Invoke(this, selected_slot);
            this.Close();
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            int selected_slot = listBox_save_load.SelectedIndex;
            string save_name = (selected_slot + 1).ToString() + ". " + textBox_save.Text;
            current_strat.saved_name = save_name;
            saved_strat_list[selected_slot] = current_strat;

            string new_json = JsonConvert.SerializeObject(saved_strat_list, Formatting.Indented);
            File.WriteAllText("FCalcACC_saved_strats.json", new_json);

            listBox_save_load.Items.Clear();
            CreateOrLoadSavedJson();
        }
    }
}
