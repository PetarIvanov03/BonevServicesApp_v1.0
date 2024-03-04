using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BonevServicesApplication
{
    public partial class CarRepairsByCategories : Form
    {
        private bool openAnotherForm = false;
        private Car _foundCar;
        public CarRepairsByCategories(Car foundCar)
        {
            InitializeComponent();

            this._foundCar = foundCar;

            this.Text = $"Bonev Services - {_foundCar.Name} - Ремонти по категории";
            this.label1.Text = $"{_foundCar.Name} - Ремонти по категории";

            Helpers.DefaultFormPosition(this);
            Helpers.ButtonDesign(button1);
            Helpers.LabelDesign(label1);
            Helpers.TextBoxListDesign(textBox1);



            label1.Anchor = AnchorStyles.Top;
            label1.Width = this.ClientSize.Width;
            label1.Height = 75;

            textBox1.AutoSize = false;
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            textBox1.Width = this.ClientSize.Width * 2 / 3;
            textBox1.Height = this.ClientSize.Height * 2 / 3 - 30;
            textBox1.Padding = new Padding(15);
            button1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            button1.Width = this.ClientSize.Width / 8;

            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            label1.Top = (this.ClientSize.Height - label1.Height) / 10;

            textBox1.Left = (this.ClientSize.Width - textBox1.Width) / 2;
            textBox1.Top = label1.Top + label1.Height + 30;


            button1.Left = 30;
            button1.Top = this.ClientSize.Height - button1.Height - 20;

            string labelText = null;
            foreach (var item in _foundCar.RepairsByCategory)
            {
                string newRecord = item.Key;
                newRecord += Environment.NewLine;
                foreach (var row in item.Value)
                {
                    newRecord += "       ";
                    newRecord += row;
                    newRecord += Environment.NewLine;
                }
                newRecord += Environment.NewLine;
                labelText += newRecord;
            }
            textBox1.WordWrap = false;
            textBox1.Multiline = true;
            textBox1.ScrollBars = ScrollBars.Both;
            textBox1.Text = labelText;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openAnotherForm = true;

            this.Close();

            CarFound f = new CarFound(_foundCar);
            f.Show();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.UserClosing && !openAnotherForm)
            {
                DialogResult result = MessageBox.Show(Program.ConfMessage, Program.ConfLabel, MessageBoxButtons.YesNo);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    Application.Exit();
                }
            }
            else
            {
                openAnotherForm = false;
            }
        }
    }
}
