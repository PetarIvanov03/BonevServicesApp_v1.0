using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace BonevServicesApplication
{
    public class Car
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public string DateCreated { get; set; }
        public Dictionary<string, List<string>> RepairsByDate { get; set; }
        public Dictionary<string, List<string>> RepairsByCategory { get; set; }
        public Dictionary<string, List<string>> CategoriesSearch { get; set; }

        public Car(string name)
        {
            TxtFileService carFile = new TxtFileService(name);
            this.Name = carFile.Name;
            this.Description = carFile.Description;
            this.DateCreated = carFile.DateCreated;
            this.CategoriesSearch = carFile.GetCategories();
            this.RepairsByDate = GetRepairsByDate();
            this.RepairsByCategory = GetRepairsByCategory();
            this.Notes = carFile.GetNotes();
        }

        public static void Create(string name, string description)
        {
            TxtFileService addCar = new TxtFileService();
            addCar.Create(name, description);
        }
        public void NewRepair(string partName, string content)
        {
            content = content.Replace(Environment.NewLine, " ");
            string readyContent = TxtFileService.dateDevider + $"({DateTime.Now.ToString(TxtFileService.format)})" + TxtFileService.devider + content;
            List<string> list = this.CategoriesSearch[partName];
            list.Add(readyContent);
            this.CategoriesSearch[partName] = list;
        }
        public void EditCategory(string partName, string content)
        {
            this.CategoriesSearch[partName] = new List<string>(content.
                Split(new string[] { "\r" },
                StringSplitOptions.None));
        }

        public void EditNotes(string content)
        {
            this.Notes = content;
        }

        public void PrintAllCategories()
        {
            foreach (var category in this.CategoriesSearch)
            {
                foreach (var item in category.Value)
                {
                    Console.WriteLine(item);
                }
            }
        }

        public List<string> DatesFromCategory(string category)
        {
            List<string> result = new List<string>();


            for (int i = 1; i < CategoriesSearch[category].Count; i++)
            {
                string row = CategoriesSearch[category][i];
                string date = TxtFileService.GetDateFromRow(row);

                result.Add(date);
            }



            return result;
        }

        public string EditCatergoryFromDates(string category, string date)
        {
            string result = "";
            List<string> dates = CategoriesSearch[category];

            for (int i = 1; i < CategoriesSearch[category].Count; i++)
            {
                if (CategoriesSearch[category][i].Contains(date))
                {
                    result = TxtFileService.GetRepairFromRow(CategoriesSearch[category][i]);
                }
            }
            return result.TrimEnd('\r', '\n');
        }


        public void EditCategoryByDate(string category, string date, string content)
        {
            List<string> list = this.CategoriesSearch[category];
            for (int i = 1; i < this.CategoriesSearch[category].Count; i++)
            {
                string row = this.CategoriesSearch[category][i];
                string compare = $"-+date+-({date})<-->";
                if (row.Contains(compare))

                {
                    content = content.TrimEnd('\r', '\n');
                    this.CategoriesSearch[category][i] = $"-+date+-({date})<-->{content}";
                    break;

                }
            }
        }


        public void Save()
        {
            TxtFileService fileToSave = new TxtFileService(this.Name);
            fileToSave.EditFullFile(this.Name, this.Description, this.Notes, this.CategoriesSearch);
        }




        private Dictionary<string, List<string>> GetRepairsByCategory()
        {
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();

            foreach (var item in this.CategoriesSearch)
            {
                if (item.Value.Count > 1)
                {
                    for (int i = 1; i < item.Value.Count; i++)
                    {
                        string row = item.Value[i];
                        string repair = TxtFileService.GetDateFromRow(row) + " - " + TxtFileService.GetRepairFromRow(row);

                        if (!result.ContainsKey(item.Key))
                        {
                            List<string> list = new List<string>() { repair };
                            result.Add(item.Key, list);
                        }
                        else
                        {
                            result[item.Key].Add(repair);
                        }
                    }
                }
            }

            return result;
        }

        private Dictionary<string, List<string>> GetRepairsByDate()
        {
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();

            foreach (var item in this.CategoriesSearch)
            {
                if (item.Value.Count > 1)
                {
                    for (int i = 1; i < item.Value.Count; i++)
                    {
                        string row = item.Value[i];
                        string date = TxtFileService.GetDateFromRow(row);
                        string repair = item.Key + " - " + TxtFileService.GetRepairFromRow(row);

                        if (!result.ContainsKey(date))
                        {
                            List<string> list = new List<string>() { repair };
                            result.Add(date, list);
                        }
                        else
                        {
                            result[date].Add(repair);
                        }
                    }
                }
            }

            return result;
        }
    }
}
