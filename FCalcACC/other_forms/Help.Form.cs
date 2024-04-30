using System.Reflection;

namespace FCalcACC
{

    // this form is only for displaying 'help'

    public partial class FCalcACChelp : Form
    {
        public FCalcACChelp()
        {
            InitializeComponent();
        }

        private void button_close_help_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}