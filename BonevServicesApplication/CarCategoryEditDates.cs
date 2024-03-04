﻿using System;
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
    public partial class CarCategoryEditDates : Form
    {
        private bool openAnotherForm = false;
        private Car _foundCar;
        private KeyValuePair<string, List<string>> _mainCategory;
        private string _subCatName;
        public CarCategoryEditDates(Car foundCar, KeyValuePair<string, List<string>> mainCategory, string subCatName)
        {
            InitializeComponent();


            this._foundCar = foundCar;
            this._mainCategory = mainCategory;
            this._subCatName = subCatName;

            this.Text = $"Bonev Services - {_foundCar.Name} - {_subCatName} - Дати";
            this.label1.Text = $"{_foundCar.Name} - {_subCatName} - Дати";


            Helpers.DefaultFormPosition(this);
            Helpers.ButtonDesign(button1);
            Helpers.LabelDesign(label1);


            label1.Anchor = AnchorStyles.Top;
            label1.Width = this.ClientSize.Width;
            label1.Height = 75;


            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            label1.Top = (this.ClientSize.Height - label1.Height) / 10;

            panel1.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            panel1.BackColor = Color.FromArgb(0, 0, 0, 0);
            panel1.Left = 0;
            panel1.Top = label1.Top + label1.Height;
            panel1.Width = this.ClientSize.Width;
            panel1.Height = this.ClientSize.Height - button1.Height - panel1.Top - 40;
            panel1.Padding = new Padding(50);

            flowLayoutPanel1.AutoSize = true;


            button1.Left = 30;

            button1.Top = this.ClientSize.Height - button1.Height - 20;

            button1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            button1.Width = this.ClientSize.Width / 8;



            flowLayoutPanel1.Dock = DockStyle.Top; // Dock it to the top of the panel
            flowLayoutPanel1.AutoSize = true;

            foreach (var item in _foundCar.DatesFromCategory(subCatName))
            {
                int counter = 0;
                Button button = new Button();
                Helpers.ColorfulButtonDesign(button);
                button.Anchor = AnchorStyles.None;
                button.Text = item;
                button.Dock = DockStyle.Top;
                button.Click += Button_Click;
                button.Padding = new Padding(15);
                button.Width = flowLayoutPanel1.Width / 3 - 10;
                button.AutoSize = true;

                button.Tag = item;

                flowLayoutPanel1.Controls.Add(button);
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            openAnotherForm = true;

            this.Close();

            if (sender is Button clickedButton && clickedButton.Tag is string selectedDate)
            {
                CarCategoryEdit f = new CarCategoryEdit(_foundCar, _mainCategory, _subCatName, selectedDate);
                f.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openAnotherForm = true;

            this.Close();

            CarCategoryView f = new CarCategoryView(_foundCar, _mainCategory, _subCatName);
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
