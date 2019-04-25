using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandboxConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            DrawTest();
        }
        static void DrawTest()
        {
            Console.WriteLine();
            Console.WriteLine("01234567890123456789");

            // Test 1: draw 2 x 2 square at 1,1
            Draw.RectangleFromCursor(1, 0, 2, 2);
            Console.WriteLine(" <--Cursor after test 1 was here.");

            // Test 2: draw a yellow 2 x 2 square at 4,2
            Draw.RectangleFromTop(4, 2, 2, 2, ConsoleColor.DarkYellow);
            Console.WriteLine(" <--Cursor after test 2 was here.");

            // Test 3: draw a red 2 x 2 square below
            Draw.RectangleFromCursor(1, 3, 2, 2, keepOriginalCursorLocation: true, color: ConsoleColor.Red);
            Console.WriteLine(" <--Cursor after test 3 was here.");

            // Test 4: draw a green 2 x 2 square below
            Draw.Rectangle(4, 2, 2, 2, Draw.DrawKind.BelowCursorButKeepCursorLocation, color: ConsoleColor.Green);
            Console.WriteLine(" <--Cursor after test 4 was here.");

            // Test 5: draw a double-boarder cyan rectangle around everything
            Draw.RectangleFromTop(0, 0, 10, 15, ConsoleColor.Cyan, useDoubleLines: true);
            Console.WriteLine(" <--Cursor after test 5 was here.");
            Console.ReadLine();
        }
    }
}
