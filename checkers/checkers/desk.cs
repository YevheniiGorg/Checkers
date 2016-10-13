using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers
{
    [Serializable]
    class desk
    {
        const int White_Cell = 0;
        const int Green_Checkers =1;
        const int Blue_Checkers = 2;
        const int Bleck_Cell = 3;

       public int[,] DeskMas = {
                          {Green_Checkers ,White_Cell,Green_Checkers ,White_Cell,Green_Checkers ,White_Cell,Green_Checkers ,White_Cell},
                          {White_Cell,Green_Checkers ,White_Cell,Green_Checkers ,White_Cell,Green_Checkers ,White_Cell,Green_Checkers },
                          {Green_Checkers ,White_Cell,Green_Checkers ,White_Cell,Green_Checkers ,White_Cell,Green_Checkers ,White_Cell},
                          {White_Cell,Bleck_Cell,White_Cell,Bleck_Cell,White_Cell,Bleck_Cell,White_Cell,Bleck_Cell},
                          {Bleck_Cell,White_Cell,Bleck_Cell,White_Cell,Bleck_Cell,White_Cell,Bleck_Cell,White_Cell},
                          {White_Cell,Blue_Checkers,White_Cell,Blue_Checkers,White_Cell,Blue_Checkers,White_Cell,Blue_Checkers},
                          {Blue_Checkers,White_Cell,Blue_Checkers,White_Cell,Blue_Checkers,White_Cell,Blue_Checkers,White_Cell},
                          {White_Cell,Blue_Checkers,White_Cell,Blue_Checkers,White_Cell,Blue_Checkers,White_Cell,Blue_Checkers}
                          };

        public int white_Cell
        {
            get { return White_Cell; }
        }

        public int green_Checkers
        {
            get { return Green_Checkers ; }
        }

        public int blue_Checkers
        {
            get { return Blue_Checkers; }
        }

        public int bleck_Cell
        {
            get { return Bleck_Cell; }
        }


        public void SHOW()
        {
            ShowDesk(DeskMas);
        }


        private static void ShowDesk(int[,] desk)
        {
            Console.WriteLine("   a b c d e f g h");
            Console.WriteLine("   ----------------");

            for (int i = 0; i < desk.GetLength(0); i++)
            {
                Console.Write(" "+(i + 1)+"|");
                for (int j = 0; j < desk.GetLength(1); j++)
                {

                    if (desk[i, j] == Green_Checkers )
                    {
                        Console.ForegroundColor = ConsoleColor.Green; // устанавливаем цвет                       
                        Console.Write((char)desk[i, j] + " ");
                        Console.ResetColor(); // сбрасываем в стандартный
                    }
                    if (desk[i, j] == Blue_Checkers)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write((char)desk[i, j] + " ");
                        Console.ResetColor();
                    }
                    if (desk[i, j] ==White_Cell )
                    {

                        Console.Write((char)9608);
                        Console.Write((char)9608);
                    }
                    if (desk[i, j] == Bleck_Cell)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(desk[i, j] + " ");
                        Console.ResetColor();
                    }                
                }
                Console.Write("|");
                Console.WriteLine(i+1);
            }
            Console.WriteLine("   ----------------");
            Console.WriteLine("   a b c d e f g h");
            Console.WriteLine();
         
        }
    }
}
