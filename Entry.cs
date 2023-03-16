using System;

namespace XMusic
{
    public class Entry
    {
        public DateTime EntryDate;
        public string Name { get; set; }
        public string Tag { get; set; }
        public int Money { get; set; }
        /*{
            get => Money;
            set
            {
                if (value < 0)
                {
                    value *=(-1);
                }
            }
        }*/

        public bool IsIncome { get; set; }


        public Entry(DateTime entryDate, string entryName, string entryTag, int entryMoney, bool isIncome)
        {
            EntryDate = entryDate;
            Name = entryName;
            Tag = entryTag;
            Money = entryMoney;
            IsIncome = isIncome;
        }
    }
}
