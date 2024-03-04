namespace BonevServicesApplication
{
    public partial class Form1 : Form
    {
        private bool openAnotherForm = false;
        public Form1()
        {
            InitializeComponent();
            Helpers.DefaultFormPosition(this);
            Helpers.ButtonDesign(button1);
            Helpers.ButtonDesign(button2);
            Helpers.ColorfulButtonDesign(button3);
            Helpers.SmallLabelDesign(label1);
            Helpers.TextBoxDesign(textBox1);
            this.Text = "Bonev Services - Начало";

            int defWidth = this.ClientSize.Width / 3 + 30;

            textBox1.Width = defWidth;
            textBox1.Height = 40;
            label1.Width = defWidth;
            label1.Height = 50;
            button1.Width = defWidth;
            button2.Width = defWidth;
            button3.Width = defWidth;


            button1.Left = (this.ClientSize.Width - button1.Width) / 2;
            button2.Left = (this.ClientSize.Width - button2.Width) / 2;
            button3.Left = (this.ClientSize.Width - button3.Width) / 2;
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            textBox1.Left = (this.ClientSize.Width - textBox1.Width) / 2;

            label1.Top = (this.ClientSize.Height - label1.Height) / 10;
            textBox1.Top = label1.Top + label1.Height + 15;
            button1.Top = textBox1.Top + textBox1.Height + 15;
            button2.Top = button1.Top + button1.Height + 260;
            button3.Top = button2.Top + button2.Height + 15;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

            AllCarsList f = new AllCarsList();
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Replace(" ", "");
            name = Helpers.ReplaceBulgarianWithEnglish(name.ToUpper());

            if (Helpers.CarCreated(name))
            {
                this.Hide();

                Car foundCar = new Car(name);

                CarFound f = new CarFound(foundCar);
                f.Show();
            }
            else
            {
                MessageBox.Show($"Кола с рег. номер: {name} не съществува!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();

            AddNewCar f = new AddNewCar();
            f.Show();
        }
    }
}