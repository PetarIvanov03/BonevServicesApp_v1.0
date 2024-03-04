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
    public partial class AddNewCar : Form
    {
        private bool openAnotherForm = false;
        public AddNewCar()
        {
            InitializeComponent();

            this.Text = $"Bonev Services - Нова кола";

            Helpers.DefaultFormPosition(this);
            Helpers.SmallLabelDesign(label1);
            Helpers.SmallLabelDesign(label2);
            Helpers.TextBoxDesign(textBox1);
            Helpers.TextBoxDesign(textBox2);
            Helpers.ColorfulButtonDesign(button1);
            Helpers.ButtonDesign(button2);

            int defWidth = this.ClientSize.Width / 3;
            label1.Width = defWidth;
            label2.Width = defWidth;
            label1.Height = 50;
            label2.Height = 50;

            textBox1.Width = defWidth;
            textBox1.Height = 40;
            textBox2.Width = defWidth;
            textBox2.Height = 40;

            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            textBox1.Left = (this.ClientSize.Width - textBox1.Width) / 2;
            label2.Left = (this.ClientSize.Width - label2.Width) / 2;
            textBox2.Left = (this.ClientSize.Width - textBox2.Width) / 2;
            button1.Left = (this.ClientSize.Width - button1.Width) / 2;

            button2.Left = 30;

            label1.Top = (this.ClientSize.Height - label1.Height) / 10;
            textBox1.Top = label1.Top + label1.Height;
            label2.Top = textBox1.Top + textBox1.Height + 30;
            textBox2.Top = label2.Top + label2.Height;
            button1.Top = textBox2.Top + textBox2.Height + 30;

            button2.Top = this.ClientSize.Height - button2.Height - 20;



            button2.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            button2.Width = this.ClientSize.Width / 8;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Replace(" ", "");
            name = Helpers.ReplaceBulgarianWithEnglish(name.ToUpper());

            string description = textBox2.Text;

            if (name != "")
            {
                if (description != "")
                {
                    if (Helpers.CarCreated(name))
                    {
                        MessageBox.Show($"Вече съществува кола с рег. номер: {name}!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        Car.Create(name, description);
                        Application.Restart();
                    }
                }
                else
                {
                    MessageBox.Show($"Полетo за описание е празно!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show($"Полетo за име е празно!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
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
