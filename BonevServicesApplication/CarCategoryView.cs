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
    public partial class CarCategoryView : Form
    {
        private bool openAnotherForm = false;
        private Car _foundCar;
        private KeyValuePair<string, List<string>> _mainCategory;
        private string _subCatName;
        public CarCategoryView(Car foundCar, KeyValuePair<string, List<string>> mainCategory, string subCatName)
        {
            InitializeComponent();

            this._foundCar = foundCar;
            this._mainCategory = mainCategory;
            this._subCatName = subCatName;

            this.Text = $"Bonev Services - {_foundCar.Name} - {_mainCategory.Key} - {_subCatName}";

            this.label1.Text = $"{foundCar.Name} - {foundCar.Description}";
            this.label2.Text = $"{_subCatName}";

            Helpers.DefaultFormPosition(this);
            Helpers.LabelDesign(label1);
            Helpers.LabelDesign(label2);
            Helpers.ButtonDesign(button1);
            Helpers.ColorfulButtonDesign(button2);
            Helpers.ColorfulButtonDesign(button3);



            label1.Anchor = AnchorStyles.Top;
            label1.Width = this.ClientSize.Width;
            label1.Height = 75;

            label2.Anchor = AnchorStyles.Top;
            label2.Width = this.ClientSize.Width;
            label2.Height = 75;


            button1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            button1.Width = this.ClientSize.Width / 8;
            button2.Anchor = AnchorStyles.Left;
            button2.Width = this.ClientSize.Width / 5;
            button3.Anchor = AnchorStyles.Right;
            button3.Width = this.ClientSize.Width / 5;


            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            label1.Top = (this.ClientSize.Height - label1.Height) / 10;
            label2.Left = (this.ClientSize.Width - label2.Width) / 2;
            label2.Top = label1.Top + label1.Height;

            label1.Padding = new Padding(0,0,0,0);
            label2.Padding = new Padding(0,0,0,0);

            button1.Left = 30;
            button1.Top = this.ClientSize.Height - button1.Height - 20;
            button2.Left = this.ClientSize.Width / 2 - button2.Width - button2.Width / 3;
            button2.Top = label2.Top + label2.Height + 50;
            button3.Left = this.ClientSize.Width / 2 + button2.Width / 3;
            button3.Top = label2.Top + label2.Height + 50;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            openAnotherForm = true;

            this.Close();

            CarSubCategories f = new CarSubCategories(_foundCar, _mainCategory);
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openAnotherForm = true;

            this.Close();

            CarCategoryAdd f = new CarCategoryAdd(_foundCar, _mainCategory, _subCatName);
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openAnotherForm = true;

            this.Close();

            CarCategoryEditDates f = new CarCategoryEditDates(_foundCar, _mainCategory, _subCatName);
            f.Show();

            //CarCategoryEdit f = new CarCategoryEdit(_foundCar, _mainCategory, _subCatName);
            //f.Show();
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
