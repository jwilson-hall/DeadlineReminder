using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.IO;

namespace DeadlineReminder
{
    class Program
    {
        static void Main(string[] args)
        {
            string csvDeadlinesFile = "DeadLines.csv";
            List<dLine> dLines = new List<dLine>();
            if (!File.Exists(csvDeadlinesFile))
            {
                File.Create(csvDeadlinesFile);
            }
            string[] DeadLines;
            bool dLinesTrue = false;
            try
            {
                DeadLines = File.ReadAllLines(csvDeadlinesFile);
                if (DeadLines.Length>2)
                {
                    for (int i = 2; i < DeadLines.Length; i++)
                    {
                        string[] listItems = DeadLines[i].Split(',');
                        dLines.Add(new dLine(listItems[0], Convert.ToDateTime(listItems[1])));
                    }
                    dLinesTrue = true;
                }
                else
                {
                    Console.WriteLine("Deadline file does not contain any deadlines");
                    Console.ReadLine();
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Deadline file is open in another program");
                Console.ReadLine();
            }
            while (dLinesTrue == true)
            {
                for (int i = 0; i < dLines.Count; i++)
                {
                    DateTime today = DateTime.Today;
                    DateTime Next = new DateTime(today.Year, dLines[i].date.Month, dLines[i].date.Day);
                    if (Next < today)
                        Next = Next.AddYears(1);
                    int daysuntilDLine = (Next - today).Days;
                    int daystill = 100;
                    ConsoleColor highLightColour = ConsoleColor.Red;
                    try
                    {
                        daystill = Properties.Settings.Default.DaysTill;
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        highLightColour = Properties.Settings.Default.HighlightColour;
                    }
                    catch (Exception)
                    {
                    }
                    if (daysuntilDLine <= daystill && daysuntilDLine >= 0)
                    {
                        Console.Write((Next - today).Days + " Days until ");
                        Console.ForegroundColor = highLightColour;
                        Console.Write(dLines[i].name);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" due");
                        Console.WriteLine();
                    }
                }
                Console.ReadLine();
                Console.Clear();
            }
            
        }
    }
}
