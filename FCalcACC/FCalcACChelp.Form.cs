using System.Reflection;

namespace FCalcACC
{

    // this form is only for displaying 'help'

    public partial class FCalcACChelp : Form
    {
        public FCalcACChelp()
        {
            InitializeComponent();
            LoadHelpText(out string help_text);
            richTextBox_help.Text = help_text;
        }

        private void button_close_help_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void LoadHelpText(out string helpText)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("FCalcACC.help.txt"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    helpText = reader.ReadToEnd();
                }
            }
        }
    }
}