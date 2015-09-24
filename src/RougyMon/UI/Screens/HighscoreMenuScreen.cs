using GameStateManagementSample;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Highscore = System.Tuple<string, System.TimeSpan>;

namespace RougyMon.UI.Screens
{
    class HighscoreMenuScreen : MenuScreen
    {
        public HighscoreMenuScreen()
             : base("Highscore")
        {
            List<Highscore> highscores = new List<Highscore>();
            FileInfo highscoreFile = new FileInfo("Highscore.txt");
            using (Stream filestream = highscoreFile.Open(FileMode.OpenOrCreate, FileAccess.Read))
            using (StreamReader reader = new StreamReader(filestream))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] temp = line.Split(';');

                    string name = temp[0];
                    TimeSpan time = TimeSpan.Parse(temp[1]);

                    highscores.Add(Tuple.Create(name, time));
                }
            }

            highscores.Sort(sortTuple);

            int count = Math.Min(10, highscores.Count);

            for (int i = 0; i < count; i++)
            {
                string name = highscores[i].Item1;
                TimeSpan time = highscores[i].Item2;
                MenuEntries.Add(new MenuEntry(string.Format("{0}: {1:g}", name, time)));
            }

            MenuEntry back = new MenuEntry("Back");

            back.Selected += OnCancel;

            MenuEntries.Add(back);
        }

        int sortTuple(Highscore x, Highscore y)
        {
            return y.Item2.CompareTo(x.Item2);

        }
    }
}
