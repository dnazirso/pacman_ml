using System.Collections.Generic;
using System.IO;

namespace board_libs
{
    public class Board
    {
        public int DotsLeft = 0;

        public List<List<int>> Grid { get; private set; }

        public Board(string pathToFile)
        {
            if (File.Exists(pathToFile))
            {
                CreateBoard(pathToFile);

                if (Grid.Count < 3 || Grid[0].Count < 3)
                {
                    CreateDefaultBoard();
                }
            }
            else
            {
                CreateDefaultBoard();
            }
        }

        private void CreateBoard(string pathToFile)
        {
            Grid = new List<List<int>>();
            using (StreamReader Sr = File.OpenText(pathToFile))
            {
                string s;
                while ((s = Sr.ReadLine()) != null)
                {
                    var list = new List<int>();
                    foreach (char c in s)
                    {
                        switch (c)
                        {
                            case 'x': list.Add(0); break;
                            case '+': list.Add(8); break;
                            default: list.Add(1); DotsLeft++; break;
                        }
                    }
                    Grid.Add(list);
                }
            }
        }

        private void CreateDefaultBoard()
        {
            var list = new List<int>(3);
            list.Add(1);
            list.Add(1);
            list.Add(1);
            Grid = new List<List<int>>(3);
            Grid.Add(new List<int>(list));
            Grid.Add(new List<int>(list));
            Grid.Add(new List<int>(list));
            Grid[1][1] = 0;
            DotsLeft = 8;
        }
    }

}
