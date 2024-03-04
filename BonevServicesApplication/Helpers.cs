using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonevServicesApplication
{
    public class Helpers
    {
        private static int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        private static int screenHeight = Screen.PrimaryScreen.Bounds.Height;

        public static int formWidth = screenWidth * 2 / 3;
        public static int formHeight = screenHeight * 2 / 3;


        public static int centerX = (screenWidth - formWidth) / 2;
        public static int centerY = (screenHeight - formHeight) / 2;


        public static void DefaultFormPosition(Form form)
        {
            form.StartPosition = FormStartPosition.Manual;

            form.Size = new System.Drawing.Size(formWidth, formHeight);
            form.Location = new System.Drawing.Point(centerX, centerY);

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + $"background.jpg"))
            {
                Image bckg = Image.FromFile("background.jpg");
                form.BackgroundImage = new Bitmap(bckg);
            }
            else
            {
                form.BackColor = Color.FromArgb(230, 230, 230);
            }

            form.BackgroundImageLayout = ImageLayout.Stretch;
        }

        public static void ButtonDesign(Button btn)
        {
            btn.Anchor = AnchorStyles.None;

            btn.BackColor = Color.FromArgb(255, 250, 245);
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = Color.FromArgb(70, 70, 70);
            btn.FlatAppearance.BorderSize = 2;

            btn.Font = new Font("Arial", 16);

            btn.AutoSize = false;
        }

        public static void ColorfulButtonDesign(Button btn)
        {
            btn.Anchor = AnchorStyles.None;

            btn.BackColor = Color.FromArgb(255, 236, 217);
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = Color.FromArgb(70, 70, 70);
            btn.FlatAppearance.BorderSize = 2;

            btn.Font = new Font("Arial", 18);

            btn.AutoSize = false;
        }

        public static void LabelDesign(Label lbl)
        {
            lbl.Anchor = AnchorStyles.None;

            lbl.BackColor = Color.FromArgb(150, 255, 250, 245);
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.BorderStyle = BorderStyle.None;
            lbl.Padding = new Padding(40,15,40,15);


            lbl.Font = new Font("Arial", 30);

            lbl.AutoSize = false;
        }

        public static void SmallLabelDesign(Label lbl)
        {
            lbl.Anchor = AnchorStyles.None;

            lbl.BackColor = Color.FromArgb(255, 250, 245);
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.BorderStyle = BorderStyle.None;
            lbl.Padding = new Padding(0, 10, 0, 10);

            lbl.Font = new Font("Arial", 20);

            lbl.AutoSize = false;
        }

        public static void TextBoxListDesign(TextBox tb)
        {
            tb.Anchor = AnchorStyles.None;
            tb.ReadOnly = true;

            tb.BackColor = Color.FromArgb(255, 250, 245);
            tb.BorderStyle = BorderStyle.FixedSingle;
            tb.Padding = new Padding(8, 8, 8, 8);

            tb.Font = new Font("Arial", 16);

            tb.AutoSize = false;
        }

        public static void TextBoxListDesignWrite(TextBox tb)
        {
            tb.Anchor = AnchorStyles.None;
            tb.ReadOnly = false;

            tb.BackColor = Color.FromArgb(255, 250, 245);
            tb.BorderStyle = BorderStyle.FixedSingle;
            tb.Padding = new Padding(8, 8, 8, 8);

            tb.Font = new Font("Arial", 18);

            tb.AutoSize = false;
        }

        public static void TextBoxDesign(TextBox tb)
        {
            tb.Anchor = AnchorStyles.None;

            tb.BackColor = Color.FromArgb(255, 250, 245);
            tb.BorderStyle = BorderStyle.FixedSingle;
            //tb.Padding = new Padding(10);

            tb.Font = new Font("Arial", 24);

            tb.AutoSize = false;
        }





        public static bool CarCreated(string name)
        {
            bool result = false;
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + $"Database\\{name}.txt"))
            {
                result = true;
            }

            return result;
        }

        public static string ReplaceBulgarianWithEnglish(string input)
        {
            StringBuilder result = new StringBuilder();

            foreach (char c in input.ToUpper())
            {
                if (char.IsLetter(c))
                {
                    // Check for Bulgarian Cyrillic letters and replace with English equivalents
                    switch (char.ToUpper(c))
                    {
                        case 'А':
                            result.Append("A");
                            break;
                        case 'В':
                            result.Append("B");
                            break;
                        case 'Е':
                            result.Append("E");
                            break;
                        case 'К':
                            result.Append("K");
                            break;
                        case 'М':
                            result.Append("M");
                            break;
                        case 'Н':
                            result.Append("H");
                            break;
                        case 'О':
                            result.Append("O");
                            break;
                        case 'Р':
                            result.Append("P");
                            break;
                        case 'С':
                            result.Append("C");
                            break;
                        case 'Т':
                            result.Append("T");
                            break;
                        case 'У':
                            result.Append("Y");
                            break;
                        case 'Х':
                            result.Append("X");
                            break;
                        default:
                            result.Append(c);
                            break;
                    }
                }
                else if (char.IsDigit(c))
                {
                    // Do not modify numbers
                    result.Append(c);
                }
                else
                {
                    // Replace other symbols with '@'
                    result.Append('@');
                }
            }

            return result.ToString();
        }

    }
}
