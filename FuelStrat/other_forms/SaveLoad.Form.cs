using Newtonsoft.Json;
using System.Reflection;

namespace FuelStrat
{
    public partial class SaveLoad : Form
    {
        private List<FuelStrat.SavedStrategy> default_saved_strat_list;
        private List<FuelStrat.SavedStrategy> saved_strat_list;
        private FuelStrat.SavedStrategy current_strat;

        private readonly string documents_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "FuelStrat");

        public delegate void LoadButtonClickedEventHandler(object sender, int slot);

        public event LoadButtonClickedEventHandler LoadButtonClicked;

        public SaveLoad(FuelStrat.SavedStrategy currentStrat)
        {
            InitializeComponent();
            this.current_strat = currentStrat;
            CreateOrLoadSavedJson();
        }

        public void listBox_save_load_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Load button active only when user selects a valid save

            if (listBox_save_load.SelectedIndex != -1 &&
                listBox_save_load.Text.Contains(" EMPTY        ") != true)
            {
                button_load.Enabled = true;
            }
            else
            {
                button_load.Enabled = false;
            }

            // Save button active only when user selects a slot and there is text in 'save name' textBox or
            // selects a slot that is already occupy by some save in attempt to replace it

            if (listBox_save_load.SelectedIndex != -1 && textBox_save.Text != "save name" &&
                textBox_save.Text != "")
            {
                button_save.Enabled = true;
            }
            else if (listBox_save_load.SelectedIndex != -1 &&
                listBox_save_load.Text.Contains(" EMPTY        ") != true)
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

        public void CreateOrLoadSavedJson()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("FuelStrat.json_resources.FuelStrat_saved_strats.json"))
            using (StreamReader reader = new StreamReader(stream))
            {
                string defualt_saved = reader.ReadToEnd();
                default_saved_strat_list = JsonConvert.DeserializeObject<List<FuelStrat.SavedStrategy>>(defualt_saved);
            }

            if (File.Exists(Path.Combine(documents_path, "FuelStrat_saved_strats.json")) == false)
            {
                string default_save_json = JsonConvert.SerializeObject(default_saved_strat_list, Formatting.Indented);
                File.WriteAllText(Path.Combine(documents_path, "FuelStrat_saved_strats.json"), default_save_json);

                foreach (var strat in default_saved_strat_list)
                {
                    listBox_save_load.Items.Add(strat.saved_name);
                }
            }
            else
            {
                try
                {
                    string saved_json = File.ReadAllText(Path.Combine(documents_path, "FuelStrat_saved_strats.json"));
                    saved_strat_list = JsonConvert.DeserializeObject<List<FuelStrat.SavedStrategy>>(saved_json);

                    foreach (var strat in saved_strat_list)
                    {
                        listBox_save_load.Items.Add(strat.saved_name);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error reading FuelStrat_saved_strats.json:\n" + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    DialogResult result = MessageBox.Show("Would you like to reset the " +
                    "FuelStrat_saved_strats.json?\n\nChoosing 'Yes' will delete all saved strategies.",
                    "Reset data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        File.Delete(Path.Combine(documents_path, "FuelStrat_saved_strats.json"));
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

            if (listBox_save_load.Text.Contains(" EMPTY        ") != true)
            {
                DialogResult result = MessageBox.Show("Do you want overwrite this save slot?", "Question",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    return;
                }
                else
                {
                    textBox_save.Text = listBox_save_load.Text.Replace((selected_slot + 1).ToString() + ". ", "");
                }
            }

            string save_name = (selected_slot + 1).ToString() + ". " + textBox_save.Text;
            current_strat.saved_name = save_name;
            saved_strat_list[selected_slot] = current_strat;

            string new_json = JsonConvert.SerializeObject(saved_strat_list, Formatting.Indented);
            File.WriteAllText(Path.Combine(documents_path, "FuelStrat_saved_strats.json"), new_json);

            listBox_save_load.Items.Clear();
            CreateOrLoadSavedJson();

            textBox_save.Text = "save name";
            textBox_save.Font = new Font(textBox_save.Font, FontStyle.Italic);
        }

        private void textBox_save_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (textBox_save.Text != "")
                {
                    button_save.PerformClick();
                }
            }
        }
    }
}