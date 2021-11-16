using System;
using System.Collections.Generic;
using System.Linq;

namespace CrappySudokuSolver
{
    class Program
    {

        static void Main(string[] args)
        {
            //var b = string.Empty.PadRight(81, ' ');
            var b = "    2   71 73 8        7 6      2 8 9 3    1 4   8  59  42 5 7 7 24     59       ";
            skrivbrett(b);
            prov(b, 0);
            //skrivbrett(b);
        }

        static bool prov(string b, int pos)
        {
            if (pos >= 81) {
                Console.ReadKey();    return true;
            }
            if (b[pos] != ' ')
            {
                return prov(b, pos + 1);
            }
            for (int i = 1; i <= 9; i++)
            {
                var b_NY = b.Substring(0, pos) + i.ToString() + b.Substring(pos + 1, 80 - pos);
                skrivbrett(b_NY);
                var cand = new List<string> { row(b_NY, pos), col(b_NY, pos), quad(b_NY, pos) };
                if (!cand.Any(c => c.Where(_ => _ != ' ').GroupBy(_ => _).Any(g => g.Count() > 1)))
                {
                    if( prov(b_NY, pos + 1)) return true;
                }
            }
            return false;
        }

        static void skrivbrett(string b)
        {
            if (b==null)
            {
                Console.WriteLine("INGEN LOSNING");
            }
            else
            {
                for (int i = 0; i < 81; i += 9)
                {
                    Console.WriteLine("|" + b.Substring(i, 9) + "|");
                }
            }
            Console.WriteLine("-----------");
            Console.ReadKey();
        }

        static string row(string b, int pos)
        {
            return b.Substring(pos / 9 * 9, 9);
        }

        static string col(string b, int pos)
        {
            var result = string.Empty;
            for (int i = pos % 9; i < 81; i+=9)
            {
                result += b[i];
            }
            return result;
        }

        static string quad(string b, int pos)
        {
            var y = pos / 9;
            var x = pos % 9;
            var yquad = y / 3;
            var xquad = x / 3;
            return b.Substring(yquad * 27 + xquad * 3, 3) + b.Substring(yquad * 27 + 9 + xquad * 3, 3) + b.Substring(yquad * 27 + 18 + xquad * 3, 3);
        }
    }
}