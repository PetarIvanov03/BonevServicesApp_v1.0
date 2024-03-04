using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;

namespace BonevServicesApplication
{
    public class TxtFileService
    {
        private Dictionary<string, string> startMarkers = new Dictionary<string, string>();
        private Dictionary<string, string> endMarkers = new Dictionary<string, string>();
        public List<string> markers = new List<string>();
        private void FillMarkers()
        {
            foreach (var marker in Categories.autoChasti)
            {
                foreach (var item in marker.Value)
                {
                    this.markers.Add(item);
                    this.startMarkers.Add(item, $"-----------{item}-----------");
                    this.endMarkers.Add(item, $"+++++++++++{item}+++++++++++");
                }
            }
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string DateCreated { get; set; }
        public string sPath { get; set; }
        public List<string> Notes { get; set; }
        public List<string> Content { get; set; }

        public static string format = "dd-MMMM-yyyy HH:mm:ss";
        public static string devider = "<-->";
        public static string notesStartMarker = "<----<notes>---->";
        public static string notesEndMarker = "<++++<notes>++++>";
        public static string dateDevider = "-+date+-";



        public TxtFileService(string name)
        {
            this.sPath = AppDomain.CurrentDomain.BaseDirectory + $"Database\\{name}.txt";
            this.Content = new List<string>(File.ReadAllText(this.sPath)
                .Split(new string[] { "\r" }, StringSplitOptions.None));
            FillMarkers();

            this.Name = GetName();
            this.Notes = ReadNotes();
            this.Description = GetDescription();
            this.DateCreated = GetDateCreated();
        }

        public TxtFileService()
        {
            this.DateCreated = DateTime.Now.ToString(format);
        }
        ///
        /// <summary>
        /// PRIVATE METHODS
        /// </summary>
        /// <returns></returns>
        /// 
        private string GetName()
        {
            return this.Content[0].Split(new string[] { devider }, StringSplitOptions.None)[1];
        }
        private string GetDescription()
        {
            return this.Content[1].Split(new string[] { devider }, StringSplitOptions.None)[1];
        }
        private string GetDateCreated()
        {
            return this.Content[2].Split(new string[] { devider }, StringSplitOptions.None)[1];
        }
        private List<string> ReadNotes()
        {
            List<string> result = new List<string>();
            int startLine = 0;
            int endLine = 0;

            for (int i = 0; i < this.Content.Count; i++)
            {
                if (this.Content[i] == notesStartMarker)
                {
                    startLine = i;
                }
                else if (this.Content[i] == notesEndMarker)
                {
                    endLine = i;
                }
            }

            for (int n = startLine + 1; n < endLine; n++)
            {
                result.Add(this.Content[n]);
            }

            return result;
        }
        private List<string> ReadSegment(string partName)
        {
            List<string> result = new List<string>();
            string startMarker = startMarkers[partName];
            string endMarker = endMarkers[partName];
            int startLine = 0;
            int endLine = 0;

            for (int i = 0; i < this.Content.Count; i++)
            {
                if (this.Content[i] == startMarker)
                {
                    startLine = i;
                }
                else if (this.Content[i] == endMarker)
                {
                    endLine = i;
                }
            }

            for (int n = startLine + 1; n < endLine; n++)
            {
                result.Add(this.Content[n]);
            }

            return result;
        }
        private List<string> ConsolidateFileContent(string name, string description, string notes, Dictionary<string, List<string>> categories)
        {
            List<string> result = new List<string>();
            result.Add($"Name{devider}{name}");
            result.Add($"Description{devider}{description}");
            result.Add($"DateCreated{devider}{this.DateCreated}");

            result.Add(notesStartMarker);

            List<string> notesList = new List<string>(notes.Split(new string[] { "\r" }, StringSplitOptions.None));

            foreach (var item in notesList)
            {
                result.Add(item);
            }

            result.Add(notesEndMarker);


            foreach (var item in categories)
            {
                result.Add(startMarkers[item.Key]);
                foreach (var row in item.Value)
                {
                    result.Add(row);
                }
                result.Add(endMarkers[item.Key]);
            }

            return result;
        }
        ///
        /// <summary>
        /// PUBLIC METHODS - CREATE, GETDATES, GETCATEGORIES, UPDATE?
        /// </summary>
        /// <returns></returns>
        /// 


        public static List<string> GetAllCars()
        {
            List<string> result = new List<string> ();
            string folderPath = AppDomain.CurrentDomain.BaseDirectory + $"Database";

            if (Directory.Exists(folderPath))
            {
                string[] txtFiles = Directory.GetFiles(folderPath, "*.txt");

                foreach (string filePath in txtFiles)
                {
                    string fileName = Path.GetFileName(filePath).Replace(".txt", "");
                    result.Add(fileName);
                }
            }
            else
            {
                result.Add("Something went wrong!");
            }


            return result;
        }


        public void Create(string name, string description)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + $"Database\\{name}.txt";

            description = description.Replace(Environment.NewLine, " ");
            name = name.Replace(" ", "");

            FillMarkers();

            if (!File.Exists(path))
            {
                string result = $"Name{devider}{name}" + "\r";
                result += $"Description{devider}{description}" + "\r";
                result += $"DateCreated{devider}{this.DateCreated}" + "\r";

                result += notesStartMarker + "\r";

                result += notesEndMarker + "\r";

                foreach (var item in markers)
                {
                    result += startMarkers[item] + "\r";
                    result += "Created: (" + this.DateCreated + ") " + "\r";
                    result += endMarkers[item] + "\r";
                }
                result = result.ToString().TrimEnd('\r', '\n');

                File.WriteAllText(path, result);
            }
        }


        public static string GetDateFromRow(string row)
        {
            string date;

            int dateStartIndex = row.IndexOf('(');
            int dateEndIndex = row.IndexOf(')');
            int dateLenght = dateEndIndex - dateStartIndex - 1;

            date = row.Substring(dateStartIndex + 1, dateLenght);

            return date;
        }

        public static string GetRepairFromRow(string row)
        {
            string repair;

            string startTag = "<-->";
            int repairStartIndex = row.IndexOf(startTag);

            repair = row.Substring(repairStartIndex + startTag.Length);

            return repair;
        }



        public Dictionary<string, List<string>> GetCategories()
        {
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();

            foreach (var item in this.markers)
            {
                string cat = item;
                List<string> value = ReadSegment(item);
                result.Add(cat, value);
            }

            return result;
        }




        public List<string> GetByDate(string dateInFromat)
        {
            List<string> resultLines = new List<string>();

            foreach (var item in this.Content)
            {
                if (item.Contains(dateInFromat))
                {
                    resultLines.Add(item);
                }
            }

            return resultLines;
        }


        public string GetNotes()
        {
            string result = "";
            foreach (var item in this.Notes)
            {
                result += item + "\r";
            }

            return result;
        }



        private void SaveChanges()
        {
            string contents = null;
            for (int i = 0; i < this.Content.Count; i++)
            {
                contents += this.Content[i];
                if (i != this.Content.Count - 1)
                {
                    contents += "\r";
                }
            }
            try
            {
                File.WriteAllText(this.sPath, contents);
                Console.WriteLine("Changes saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving changes: {ex.Message}");
            }
        }
        public void EditFullFile(string name, string description, string notes, Dictionary<string, List<string>> categories)
        {
            this.Content = ConsolidateFileContent(name, description, notes, categories);

            SaveChanges();
        }
    }
}
