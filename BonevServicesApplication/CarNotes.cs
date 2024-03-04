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
    public partial class CarNotes : Form
    {
        private bool openAnotherForm = false;
        private Car _foundCar;
        public CarNotes(Car foundCar)
        {
            InitializeComponent();

            this._foundCar = foundCar;

            this.Text = $"Bonev Services - {_foundCar.Name} - Забележки";
            this.label1.Text = $"{_foundCar.Name} - Забележки";

            Helpers.DefaultFormPosition(this);
            Helpers.LabelDesign(label1);
            Helpers.TextBoxListDesignWrite(textBox1);
            Helpers.ColorfulButtonDesign(button2);
            Helpers.ButtonDesign(button1);



            label1.Anchor = AnchorStyles.Top;
            label1.Width = this.ClientSize.Width;
            label1.Height = 75;

            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            textBox1.Width = this.ClientSize.Width * 2 / 3;
            textBox1.Height = this.ClientSize.Height * 2 / 3 - 30;
            button1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            button1.Width = this.ClientSize.Width / 8;
            button2.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            button2.Width = this.ClientSize.Width / 8;

            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            textBox1.Left = (this.ClientSize.Width - textBox1.Width) / 2;

            button1.Left = 30;
            button2.Left = (this.ClientSize.Width - button2.Width) - 30;


            label1.Top = (this.ClientSize.Height - label1.Height) / 10;
            textBox1.Top = label1.Top + label1.Height + 30;

            button2.Top = this.ClientSize.Height - button2.Height - 20;
            button1.Top = this.ClientSize.Height - button1.Height - 20;

            textBox1.Text = _foundCar.Notes.TrimEnd('\r', '\n');
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openAnotherForm = true;

            this.Close();

            CarFound f = new CarFound(_foundCar);
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string notes = this.textBox1.Text;

            if (textBox1.Text != "")
            {
                DialogResult result = MessageBox.Show("Желаете ли да запазите промените?", "Внимание!", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    openAnotherForm = true;

                    _foundCar.EditNotes(notes);
                    _foundCar.Save();

                    this.Close();

                    Car newCar = new Car(_foundCar.Name);
                    CarFound f = new CarFound(newCar);
                    f.Show();
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Полетo за забележки е празно \n Желаете ли да запазите промените?", "Внимание!", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    openAnotherForm = true;

                    _foundCar.EditNotes(notes);
                    _foundCar.Save();

                    this.Close();

                    Car newCar = new Car(_foundCar.Name);
                    CarFound f = new CarFound(newCar);
                    f.Show();
                }
            }
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
