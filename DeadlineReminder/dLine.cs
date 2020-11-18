using System;

namespace DeadlineReminder
{
    class dLine
    {
        public string name { get; set; }
        public DateTime date { get; set; }
        public dLine (string name, DateTime date)
        {
            this.name = name;
            this.date = date;
        }
    }
}
