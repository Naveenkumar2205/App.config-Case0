using System;
using System.Windows.Forms;
using System.Configuration;

namespace ConfigurationFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        { }
        private void ReadConfigButton_Click_1(object sender, EventArgs e)
        {
            string key0 = ConfigurationManager.AppSettings["key0"];
            string key1 = ConfigurationManager.AppSettings["key1"];
           
            MessageBox.Show($"Key0: {key0}\nKey1: {key1}", "Configuration Parameters");
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string currentValue0 = configuration.AppSettings.Settings["key0"].Value;
            string currentValue1 = configuration.AppSettings.Settings["key1"].Value;
           
            string newValue0 = InputBox("Enter new key0:", "Input", currentValue0);
            string newValue1 = InputBox("Enter new key1:", "Input", currentValue1);
           
            configuration.AppSettings.Settings["key0"].Value = newValue0;
            configuration.AppSettings.Settings["key1"].Value = newValue1;
            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            MessageBox.Show("Value Updated");
        }
        private string InputBox(string prompt, string title, string defaultValue = "")
        {
            using (Form inputForm = new Form())
            {
                Label label = new Label() { Left = 20, Top = 20, Text = prompt };
                TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 200, Text = defaultValue };
                Button okButton = new Button() { Text = "OK", Left = 230, Top = 50, DialogResult = DialogResult.OK };
                Button cancelButton = new Button() { Text = "Cancel", Left = 230, Top = 75, DialogResult = DialogResult.Cancel };

                inputForm.Text = title;
                inputForm.AcceptButton = okButton;
                inputForm.CancelButton = cancelButton;

                inputForm.Controls.Add(label);
                inputForm.Controls.Add(textBox);
                inputForm.Controls.Add(okButton);
                inputForm.Controls.Add(cancelButton);

                return inputForm.ShowDialog() == DialogResult.OK ? textBox.Text : defaultValue;
            }
        }
    }
}

