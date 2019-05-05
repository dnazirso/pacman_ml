using System.Collections.Generic;
using System.IO;

namespace board_libs
{
    public class Board
    {
        public int DotsLeft = 0;

        public List<List<char>> Grid { get; private set; }

        public Board(string pathToFile)
        {
            if (File.Exists(pathToFile))
            {
                CreateBoard(pathToFile);
            }
        }

        private void CreateBoard(string pathToFile)
        {
            Grid = new List<List<char>>();
            using (StreamReader Sr = File.OpenText(pathToFile))
            {
                string s;
                while ((s = Sr.ReadLine()) != null)
                {
                    var list = new List<char>();
                    foreach (char c in s)
                    {
                        switch (c)
                        {
                            case '.': DotsLeft++; break;
                        }
                        list.Add(c);
                    }
                    Grid.Add(list);
                }
            }
        }
    }
}
