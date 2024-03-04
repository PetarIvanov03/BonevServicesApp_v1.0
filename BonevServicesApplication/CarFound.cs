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
    public partial class CarFound : Form
    {
        private bool openAnotherForm = false;
        private Car _foundCar;
        public CarFound(Car foundCar)
        {
            InitializeComponent();
            this._foundCar = foundCar;

            this.Text = $"Bonev Services - {foundCar.Name} - {foundCar.Description}";

            this.label1.Text = $"{foundCar.Name} - {foundCar.Description}";

            Helpers.DefaultFormPosition(this);
            Helpers.LabelDesign(label1);
            Helpers.ColorfulButtonDesign(button1);
            Helpers.ColorfulButtonDesign(button2);
            Helpers.ColorfulButtonDesign(button3);
            Helpers.ButtonDesign(button4);
            Helpers.ButtonDesign(button5);



            label1.Anchor = AnchorStyles.Top;
            label1.Width = this.ClientSize.Width;
            label1.Height = 75;

            button1.Anchor = AnchorStyles.None;
            button1.Width = this.ClientSize.Width / 6;
            button1.Height = 80;
            button2.Anchor = AnchorStyles.None;
            button2.Width = this.ClientSize.Width / 6;
            button2.Height = 80;
            button3.Anchor = AnchorStyles.None;
            button3.Width = this.ClientSize.Width / 6;
            button3.Height = 80;
            button4.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            button4.Width = this.ClientSize.Width / 8;
            button5.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            button5.Width = this.ClientSize.Width / 8;


            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            label1.Top = (this.ClientSize.Height - label1.Height) / 10;

            int spacing = (this.ClientSize.Width / 2) / 4;

            button1.Left = spacing;
            button1.Top = label1.Top + label1.Height + 45;
            button2.Left = spacing + button1.Width + spacing;
            button2.Top = label1.Top + label1.Height + 45;
            button3.Left = spacing + button1.Width + spacing + button1.Width + spacing;
            button3.Top = label1.Top + label1.Height + 45;
            button4.Left = 30;
            button4.Top = this.ClientSize.Height - button4.Height - 20;
            button5.Left = this.ClientSize.Width - button4.Width - 30; 
            button5.Top = this.ClientSize.Height - button4.Height - 20;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openAnotherForm = true;

            this.Close();

            CarRepairsByCategories f = new CarRepairsByCategories(_foundCar);
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openAnotherForm = true;

            this.Close();

            CarRepairsByDate f = new CarRepairsByDate(_foundCar);
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openAnotherForm = true;

            this.Close();

            CarMainCategories f = new CarMainCategories(_foundCar);
            f.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openAnotherForm = true;

            this.Close();

            CarNotes f = new CarNotes(_foundCar);
            f.Show();
        }

        private void CarFound_Load(object sender, EventArgs e)
        {

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
