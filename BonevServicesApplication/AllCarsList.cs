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
    public partial class AllCarsList : Form
    {
        private bool openAnotherForm = false;
        public AllCarsList()
        {
            InitializeComponent();

            this.Text = $"Bonev Services - Всички коли";

            Helpers.DefaultFormPosition(this);
            Helpers.ButtonDesign(button1);

            panel1.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            panel1.BackColor = Color.FromArgb(0, 0, 0, 0);
            panel1.Left = 0;
            panel1.Top = 0;
            panel1.Width = this.ClientSize.Width;
            panel1.Height = this.ClientSize.Height - button1.Height - 40;
            panel1.Padding = new Padding(50);

            flowLayoutPanel1.AutoSize = true;


            button1.Left = 30;

            button1.Top = this.ClientSize.Height - button1.Height - 20;

            button1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            button1.Width = this.ClientSize.Width / 8;



            flowLayoutPanel1.Dock = DockStyle.Top;
            flowLayoutPanel1.AutoSize = true;

            foreach (var item in TxtFileService.GetAllCars())
            {
                Button button = new Button();
                Helpers.ColorfulButtonDesign(button);
                button.Text = item;
                button.Dock = DockStyle.Top;
                button.Click += Button_Click;
                button.Font = new Font(button.Font.FontFamily, 22, button.Font.Style);
                button.Padding = new Padding(15);
                button.Width = panel1.Width / 4 - 35;
                button.Height = panel1.Height / 7 - 5;

                button.Tag = item;

                flowLayoutPanel1.Controls.Add(button);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        private void Button_Click(object sender, EventArgs e)
        {
            openAnotherForm = true;

            this.Close();

            if (sender is Button clickedButton && clickedButton.Tag is string name)
            {
                Car foundCar = new Car(name);
                CarFound f = new CarFound(foundCar);
                f.Show();
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
