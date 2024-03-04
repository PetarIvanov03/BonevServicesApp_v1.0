using System;
using System.Collections.Generic;
using System.Text;

namespace BonevServicesApplication
{
    public static class Categories
    {
        public static List<string> obsluzhvane = new List<string>
        {
            "Моторно масло", "Поленов филтър", "Маслен филтър", "Горивен филтър", "Въздушен филтър", "Обслужване - други"
        };

        public static List<string> dvigatel = new List<string>
        {
            "Изпускателна система", "Горивна система", "Ремъчно задвижване", "Смазочни материали", "Двигател - други"
        };

        public static List<string> shasiiZadvijvane = new List<string>
        {
            "Аксиално задвижване", "Ходова част", "Спирачна система", "Окачване", "Трансмисия",
            "Съединител", "Управление", "Задвижване на колелата", "Колела/гуми", "Шаси и задвижване - други"
        };

        public static List<string> aksesoariiDrugi = new List<string>
        {
            "Системи за комфорт", "Система за почистване на стъкла", "Системи за сигурност", "Аксесоари", "Аксесоари - други"
        };

        public static List<string> karoseriq = new List<string>
        {
            "Вътрешно обурудване", "Каросерия", "Заключваща система", "Каросерия - други"
        };

        public static List<string> otoplenieiOhlajdane = new List<string>
        {
            "Отоплителна система", "Охладителна система", "Климатик", "Отопление и охлаждане - други"
        };

        public static List<string> elktricheskaSistema = new List<string>
        {
            "Електрическа инсталация", "Системи за информация/комуникация", "Запалителна система", "Електрическа система - други"
        };

        public static List<string> drugi = new List<string>()
        {
            "Други ремонти"
        };

        // Добавяне на категориите в обща структура
        public static Dictionary<string, List<string>> autoChasti = new Dictionary<string, List<string>>
        {
            { "Обслужване", obsluzhvane },
            { "Двигател", dvigatel },
            { "Шаси и задвижване", shasiiZadvijvane },
            { "Аксесоари и други", aksesoariiDrugi },
            { "Каросерия", karoseriq },
            { "Отопление и охлаждане", otoplenieiOhlajdane },
            { "Електрическа система", elktricheskaSistema },
            { "Други", drugi }
        };

    }
}
