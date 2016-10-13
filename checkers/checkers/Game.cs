using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace checkers
{
    [Serializable]
    class Game
    {
  
        private ArrayList CurrentCoordinates;
        private desk Pole;
        const int PlayerNum=0;
        private int CurrentID=1;
        private Player[] MasPlayer = { new Player("", 1), new Player("", 2)};
        private CollectionForLok Lok;
        const int locationCenter = 0;
        const int locationLeft = 1;
        const int locationRight = 2;
        const int locationUp = 3;
        const int locationDown = 4;
        const int locationLeftUp = 5;
        const int locationRightUp = 6;
        const int locationLeftDown = 7;
        const int locationRightDown = 8;
        const int LocationOponentLeftUp = 1;
        const int LocationOponentLeftDown = 2;
        const int LocationOponentRidhtUp = 3;
        const int LocationOponentRightDown = 4;



        public Game()
        {
            CurrentCoordinates = new ArrayList();
            Pole = new desk();
            Lok =new CollectionForLok();
        }
        public Game(string name1,string name2)
        {
            CurrentCoordinates = new ArrayList();
            Pole = new desk();
            Lok = new CollectionForLok();
            MasPlayer[0].Name = name1;
            MasPlayer[1].Name = name2;
        }


        public void GAME()
        {
            StartOldGame();
            Console.Clear();
            Pole.SHOW();
            do
            {
                Console.WriteLine("Для входа в меню: m");
                Console.WriteLine("Для продолжения нажмите любую другую клавишу.");
                string Str = Console.ReadLine();
                if (String.Compare(Str, "m") == 0)
                {
                    Menu(2);
                    Console.Clear();
                    Pole.SHOW();
                }

                for (int j = 0; j < MasPlayer.Length; j++)
                {
                    CurrentID = MasPlayer[j].ID;
                    if (MasPlayer[j].ID == 1)
                    {
                        Console.Write("Ходит: " + MasPlayer[j].Name);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("(");
                        Console.Write((char)2);
                        Console.WriteLine(")");
                        Console.ResetColor();
                    }
                    if (MasPlayer[j].ID == 2)
                    {
                        Console.Write("Ходит: " + MasPlayer[j].Name);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("(");
                        Console.Write((char)1);
                        Console.WriteLine(")");
                        Console.ResetColor();
                    }

                    Input_coordinate();
                    if (MasPlayer[0].Current_Ccount == 0)
                    {
                        Console.WriteLine("Игрок " + MasPlayer[0].Name + " проиграл!");
                        return;
                    }
                    if (MasPlayer[0].Current_Ccount == 0)
                    {
                        Console.WriteLine("Игрок " + MasPlayer[1].Name + " проиграл!");
                        return;
                    }
                }

            } while (MasPlayer[0].Current_Ccount != 0 || MasPlayer[1].Current_Ccount != 0);
        }

        private void StartOldGame()
        {
            if (MasPlayer[0].Name.Length == 0)
            {
                Menu(1);
                StartGame();
            }
        }

        private void Menu(int Flag)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Начать новую игру: n");
            Console.WriteLine("Продолжить предыдущую игру: p");
            if (Flag!=1)
            {
                Console.WriteLine("Сохранить игру: s");
                Console.WriteLine("Показать Lok текущей игры: l");
            }
           

            string Str = Console.ReadLine();
           
            if (String.Compare(Str, "p") == 0)
            {
                DownloadGame();
                Console.WriteLine("предыдущ игрa");
                Console.WriteLine();
            }
            if (String.Compare(Str, "n") == 0)
            {
                Console.WriteLine("Новая игра!");
                Console.WriteLine();
            }
            if (Flag!=1)
            {
                if (String.Compare(Str, "l") == 0)
                {
                    Console.WriteLine("Lok");
                    Lok.ShowCollection();
                }
                if (String.Compare(Str, "s") == 0)
                {
                    SaveInFile();
                    Console.WriteLine("Игра сохранена!");
                    Console.WriteLine();
                }
            }
           
        }

        private void DownloadGame()
        {
            Deserialize Des = new Deserialize();
            Des.ReadFile();
        }

        private void StartGame()
        {
            Console.WriteLine("    Игра шашки.");
            for (int i = 0; i <2; i++)
            {
                Console.WriteLine("Введите имя "+(i+1)+ "-го игрока.");

                MasPlayer[i].Name = (Console.ReadLine());
                MasPlayer[i].ID = i + 1;
            }
        }


        private void Input_coordinate ()
        {
            string Koordinate;
            int f = 0;
            do
            {
                if(f>0)
                    Console.WriteLine("Неверный ход!");
                Console.WriteLine();
 
                Console.WriteLine("Введите координаты хода(В формате: a1-b2):");
                Koordinate = Console.ReadLine();
                f++;

            } while (!CheckInput(Koordinate));

            RedrawDesk();
        }

       
        private void SaveInFile()
        {
            FileStream FS = new FileStream("Game_Checkers.dat",FileMode.Create,FileAccess.ReadWrite);
            BinaryFormatter BinFormat = new BinaryFormatter();
            BinFormat.Serialize(FS, this);

            FS.Close();
        }



        private void RedrawDesk()//перерисовать поле
        {          
           
            int x=Convert.ToInt32(CurrentCoordinates[0]);
            int y = Convert.ToInt32(CurrentCoordinates[1]);
            int x1=Convert.ToInt32(CurrentCoordinates[2]);
            int y1= Convert.ToInt32(CurrentCoordinates[3]);
            Pole.DeskMas[x,y] = Pole.bleck_Cell;

            if (CurrentID == 1)
            {
                Pole.DeskMas[x1, y1] = Pole.blue_Checkers;
            }
            if (CurrentID==2)
            {
                Pole.DeskMas[x1, y1] = Pole.green_Checkers;
            }
            Console.Clear();
            Console.WriteLine();
            Pole.SHOW();
            MasPlayer[0].ShowCount();
            MasPlayer[1].ShowCount();
            Console.WriteLine();
            Console.WriteLine();
        }

        private bool CheckInput(string koordinate)//проверка ввода 
        {
            if (koordinate.Length>5||koordinate.Length==0)
            {
                return false;
            }

            if (CurrentCoordinates.Count > 0)
                CurrentCoordinates.Clear();
           
            try
            {
                int j = 1;
                for (int i = 1; i <= 2; i++)
                {                   
                    int n;
                    n = Convert.ToInt32(koordinate[j].ToString());
                    if (n > 8)
                    {
                        return false;
                    }

                    char ch;
                    ch = Convert.ToChar(koordinate[j-1].ToString());
                    if (ch < 97 || ch > 104)
                    {
                        return false;
                    }
                    j += 3;
                    //запись координат в массив                   
                    CurrentCoordinates.Add(n-1);//координата цифры
                    SaveLok(n - 1, (Convert.ToInt32(ch))-97);
                    n = Convert.ToInt32(ch);
                    CurrentCoordinates.Add(n - 97);//координата букв
                }
               
                if (koordinate[2]!='-')
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            if (!TestExistenceСhecker(CurrentCoordinates))//нет хода по белым клеткам и по местам с шашками
            {

                return false;
            }

            if (!TestMyCheckers(CurrentCoordinates))//Ходить только своей шашкой
            {

                return false;
            }

            if (!StepForward(CurrentCoordinates))//ход вперед 
            {

                return false;
            }

            if (!StepByStep(CurrentCoordinates))//ход по каждой клетке
            {

                return false;
            }
         
            return true;
        }

        private void SaveLok(int x, int y)
        {
            Lok.Add(x, y);

        }

      

        private void StepViaOponent(int X_Oponent,int Y_Oponent)// функц для зарисовки убитой фишки и добавления в счет!
        {
            Pole.DeskMas[X_Oponent, Y_Oponent] = Pole.bleck_Cell;

            if (CurrentID == 1)
            {               
                MasPlayer[1].Current_Ccount -= 1;
            }
            if (CurrentID == 2)
            {               
                MasPlayer[0].Current_Ccount -= 1;
            }
        }


        private bool StepByStep(ArrayList coord)//ход по каждой клетке
        {
            if (TestBesideOpponent(coord).Capacity >= 1 && Convert.ToInt32(TestBesideOpponent(coord)[0])!=0)//если рядом есть противник
            {
                //выбранная координата - ход через противника
                bool LeftDown=false, LeftUp = false, RightDown = false, RidhtUp = false;
                foreach (int item in TestBesideOpponent(coord))
                {
                    if (item == LocationOponentLeftDown)
                        LeftDown = true;
                    if (item == LocationOponentLeftUp)
                        LeftUp = true;
                    if (item == LocationOponentRightDown)
                        RightDown = true;
                    if (item == LocationOponentRidhtUp)
                        RidhtUp = true;
                }

                if ((LeftDown==true) &&
                    ((Convert.ToInt32(coord[2]) == (Convert.ToInt32(coord[0]) + 2)) &&
                     (Convert.ToInt32(coord[3]) == (Convert.ToInt32(coord[1]) - 2))))
                {
                    StepViaOponent((Convert.ToInt32(coord[0])+1), (Convert.ToInt32(coord[1])-1));// функцию для зарисовки убитой фишки и добавления в счет!
                    return true;
                }
               else if ((LeftUp==true) &&
                    ((Convert.ToInt32(coord[2]) == (Convert.ToInt32(coord[0]) - 2)) &&
                     (Convert.ToInt32(coord[3]) == (Convert.ToInt32(coord[1]) - 2))))
                {
                    StepViaOponent((Convert.ToInt32(coord[0]) - 1), (Convert.ToInt32(coord[1]) - 1));
   
                    return true;
                }
                else if ((RightDown==true) &&
                    ((Convert.ToInt32(coord[2]) == (Convert.ToInt32(coord[0]) + 2)) &&
                     (Convert.ToInt32(coord[3]) == (Convert.ToInt32(coord[1]) + 2))))
                {
                    StepViaOponent((Convert.ToInt32(coord[0]) + 1), (Convert.ToInt32(coord[1]) + 1));
              
                    return true;
                }
                else if ((RidhtUp==true) &&
                    ((Convert.ToInt32(coord[2]) == (Convert.ToInt32(coord[0]) - 2)) &&
                     (Convert.ToInt32(coord[3]) == (Convert.ToInt32(coord[1]) + 2))))
                {
                    StepViaOponent((Convert.ToInt32(coord[0]) - 1), (Convert.ToInt32(coord[1]) + 1));
              
                    return true;
                }
                         
                return TeatStepByStep(coord);//
            }
            else
            {
   
                return TeatStepByStep(coord);
            }
                
        }

        private static bool TeatStepByStep(ArrayList coord)
        {
            if ((Math.Abs(Convert.ToInt32(coord[2]) - Convert.ToInt32(coord[0])) > 1) &&
                            (Math.Abs(Convert.ToInt32(coord[3]) - Convert.ToInt32(coord[1])) > 1))
                return false;
            else
                return true;
        }


        private bool StepForward(ArrayList coord)//ход вперед 
        {
            int TestBesOpp = TestBesideOpponent(coord).Capacity;

            if (TestBesOpp >=1 && Convert.ToInt32(TestBesideOpponent(coord)[0])!=0)//для удара можно пойти назад????
            {
                return true;
            }
            if (CurrentID==2)
            {
                if (Convert.ToInt32(coord[0]) - Convert.ToInt32(coord[2]) < 0)
                {
          
                    return true;
                }
                else
                {
          
                    return false;
                }
                    
            }
            if (CurrentID == 1)
            {
                if (Convert.ToInt32(coord[0]) - Convert.ToInt32(coord[2]) < 0)
                {
          
                    return false;
                }
                else
                {
         
                    return true;
                }                   
            }
            return true;            
        }


        private bool TestMyCheckers(ArrayList coord)//Ходить только своей шашкой
        {
            if (CurrentID==1)
            {
                if ((Pole.DeskMas[Convert.ToInt32(coord[0]), Convert.ToInt32(coord[1])] == Pole.blue_Checkers))//1-я координата игрока с id=1 совпадает с blue_Checkers
                    return true;
                else
                return false;
            }
            if (CurrentID == 2)
            {
                if ((Pole.DeskMas[Convert.ToInt32(coord[0]), Convert.ToInt32(coord[1])] == Pole.green_Checkers))//1-я координата игрока с id=2 совпадает с green_Checkers
                    return true;
                else
                    return false;
            }
            return false;
        }


        private bool TestExistenceСhecker(ArrayList coord)//нет хода по белым клеткам и по местам с шашками
        {
            if ((Pole.DeskMas[Convert.ToInt32(coord[2]), Convert.ToInt32(coord[3])] == Pole.blue_Checkers)||
                    (Pole.DeskMas[Convert.ToInt32(coord[2]), Convert.ToInt32(coord[3])]==Pole.green_Checkers)||
                    (Pole.DeskMas[Convert.ToInt32(coord[2]), Convert.ToInt32(coord[3])]==Pole.white_Cell))//не ходи по белым клеткам
            {
                return false;
            }
            else
                return true;
        }


        private ArrayList TestBesideOpponent(ArrayList coord)//есть ли рядом противник?
        {
           int locatCh = LocationChecker(coord);/////
            ArrayList MasLocationOpponent = new ArrayList();
            if (CurrentID == 1)
            {
                if (locatCh == locationLeft)
                {
                    if (Pole.DeskMas[(Convert.ToInt32(coord[0]) + 1), (Convert.ToInt32(coord[1])) + 1] == Pole.green_Checkers)
                    {
                        MasLocationOpponent.Add(LocationOponentRightDown);
 
                    }
                    if (Pole.DeskMas[(Convert.ToInt32(coord[0]) - 1), (Convert.ToInt32(coord[1])) + 1] == Pole.green_Checkers)
                    {
                        MasLocationOpponent.Add(LocationOponentRidhtUp);
     
                    }                  
                }
                 if (locatCh  == locationRight)
                {
                    if (Pole.DeskMas[(Convert.ToInt32(coord[0]) + 1), (Convert.ToInt32(coord[1])) - 1] == Pole.green_Checkers)
                    {
                        MasLocationOpponent.Add(LocationOponentLeftDown);
     
                    }
                    if (Pole.DeskMas[(Convert.ToInt32(coord[0]) - 1), (Convert.ToInt32(coord[1])) - 1] == Pole.green_Checkers)
                    {
                        MasLocationOpponent.Add(LocationOponentLeftUp);
        
                    }
                }
                 if(locatCh  ==locationUp)
                {
                    if (Pole.DeskMas[(Convert.ToInt32(coord[0]) + 1), (Convert.ToInt32(coord[1])) - 1] == Pole.green_Checkers)
                    {
                        MasLocationOpponent.Add(LocationOponentLeftDown);
          
                    }
                    if (Pole.DeskMas[(Convert.ToInt32(coord[0]) + 1), (Convert.ToInt32(coord[1])) + 1] == Pole.green_Checkers)
                    {
                        MasLocationOpponent.Add(LocationOponentRightDown);
           
                    }
                }
                if (locatCh  ==locationDown)
                {
                    if (Pole.DeskMas[(Convert.ToInt32(coord[0]) - 1), (Convert.ToInt32(coord[1])) - 1] == Pole.green_Checkers)
                    {
                        MasLocationOpponent.Add(LocationOponentLeftUp);
                  
                    }
                    if (Pole.DeskMas[(Convert.ToInt32(coord[0]) - 1), (Convert.ToInt32(coord[1])) + 1] == Pole.green_Checkers)
                    {
                        MasLocationOpponent.Add(LocationOponentRidhtUp);
           
                    }
                }
                 if(locatCh==locationLeftUp)
                {
                    if ((Pole.DeskMas[(Convert.ToInt32(coord[0]) +1), (Convert.ToInt32(coord[1])) + 1] == Pole.green_Checkers))
                    {
                        MasLocationOpponent.Add(LocationOponentRightDown);
        
                    }
                }
                 if (locatCh == locationLeftDown)
                {
                    if ((Pole.DeskMas[(Convert.ToInt32(coord[0]) - 1), (Convert.ToInt32(coord[1])) + 1] == Pole.green_Checkers))
                    {
                        MasLocationOpponent.Add(LocationOponentRidhtUp);
              
                    }
                }
                 if (locatCh == locationRightUp)
                {
                    if ((Pole.DeskMas[(Convert.ToInt32(coord[0]) + 1), (Convert.ToInt32(coord[1])) - 1] == Pole.green_Checkers))
                    {
                        MasLocationOpponent.Add(LocationOponentLeftDown);
                    
                    }
                }
                 if (locatCh == locationRightDown)
                {
                    if ((Pole.DeskMas[(Convert.ToInt32(coord[0]) - 1), (Convert.ToInt32(coord[1])) - 1] == Pole.green_Checkers))
                    {
                        MasLocationOpponent.Add(LocationOponentLeftUp);
                      
                    }
                }
                 if(locatCh==0)
                {    
                    if ((Pole.DeskMas[(Convert.ToInt32(coord[0]) - 1), (Convert.ToInt32(coord[1])) - 1] == Pole.green_Checkers))
                    {
                        MasLocationOpponent.Add(LocationOponentLeftUp);
                       // return LocationOponentLeftUp;
                    }
                     if ((Pole.DeskMas[(Convert.ToInt32(coord[0]) - 1), (Convert.ToInt32(coord[1])) + 1] == Pole.green_Checkers))
                    {
                        MasLocationOpponent.Add(LocationOponentRidhtUp);
                       // return LocationOponentRidhtUp;
                    }
                     if ((Pole.DeskMas[(Convert.ToInt32(coord[0]) + 1), (Convert.ToInt32(coord[1])) - 1] == Pole.green_Checkers))
                    {
                        MasLocationOpponent.Add(LocationOponentLeftDown);
                       // return LocationOponentLeftDown;
                    }
                     if ((Pole.DeskMas[(Convert.ToInt32(coord[0]) + 1), (Convert.ToInt32(coord[1])) + 1] == Pole.green_Checkers))
                    {
                        MasLocationOpponent.Add(LocationOponentRightDown);
                        //return LocationOponentRightDown;
                    }
                    
                }

                if (MasLocationOpponent.Capacity == 0)
                {
                    MasLocationOpponent.Add(0);
                }
            }
            else
            {
                if (locatCh == locationLeft)
                {
                    if (Pole.DeskMas[(Convert.ToInt32(coord[0]) + 1), (Convert.ToInt32(coord[1])) + 1] == Pole.blue_Checkers)
                    {
                        MasLocationOpponent.Add(LocationOponentRightDown);
                       // return LocationOponentRightDown;
                    }
                    if (Pole.DeskMas[(Convert.ToInt32(coord[0]) - 1), (Convert.ToInt32(coord[1])) + 1] == Pole.blue_Checkers)
                    {
                        MasLocationOpponent.Add(LocationOponentRidhtUp);
                       // return LocationOponentRidhtUp;
                    }
                }
                 if (locatCh == locationRight)
                {
                    if (Pole.DeskMas[(Convert.ToInt32(coord[0]) + 1), (Convert.ToInt32(coord[1])) - 1] == Pole.blue_Checkers)
                    {
                        MasLocationOpponent.Add(LocationOponentLeftDown);
                        //return LocationOponentLeftDown;
                    }
                    if (Pole.DeskMas[(Convert.ToInt32(coord[0]) - 1), (Convert.ToInt32(coord[1])) - 1] == Pole.blue_Checkers)
                    {
                        MasLocationOpponent.Add(LocationOponentLeftUp);
                        //return LocationOponentLeftUp;
                    }
                }
                 if (locatCh == locationUp)
                {
                    if (Pole.DeskMas[(Convert.ToInt32(coord[0]) + 1), (Convert.ToInt32(coord[1])) - 1] == Pole.blue_Checkers)
                    {
                        MasLocationOpponent.Add(LocationOponentLeftDown);
                       // return LocationOponentLeftDown;
                    }
                    if (Pole.DeskMas[(Convert.ToInt32(coord[0]) + 1), (Convert.ToInt32(coord[1])) + 1] == Pole.blue_Checkers)
                    {
                        MasLocationOpponent.Add(LocationOponentRightDown);
                        //return LocationOponentRightDown;
                    }
                }
                 if (locatCh == locationDown)
                {
                    if (Pole.DeskMas[(Convert.ToInt32(coord[0]) - 1), (Convert.ToInt32(coord[1])) - 1] == Pole.blue_Checkers)
                    {
                        MasLocationOpponent.Add(LocationOponentLeftUp);
                       // return LocationOponentLeftUp;
                    }
                    if (Pole.DeskMas[(Convert.ToInt32(coord[0]) - 1), (Convert.ToInt32(coord[1])) + 1] == Pole.blue_Checkers)
                    {
                        MasLocationOpponent.Add(LocationOponentRidhtUp);
                       // return LocationOponentRidhtUp;
                    }
                }
                 if (locatCh == locationLeftUp)
                {
                    if ((Pole.DeskMas[(Convert.ToInt32(coord[0]) + 1), (Convert.ToInt32(coord[1])) + 1] == Pole.blue_Checkers))
                    {
                        MasLocationOpponent.Add(LocationOponentRidhtUp);
                        //return LocationOponentRidhtUp;
                    }
                }
                 if (locatCh == locationLeftDown)
                {
                    if ((Pole.DeskMas[(Convert.ToInt32(coord[0]) - 1), (Convert.ToInt32(coord[1])) + 1] == Pole.blue_Checkers))
                    {
                        MasLocationOpponent.Add(LocationOponentRidhtUp);
                        //return LocationOponentRidhtUp;
                    }
                }
                 if (locatCh == locationRightUp)
                {
                    if ((Pole.DeskMas[(Convert.ToInt32(coord[0]) + 1), (Convert.ToInt32(coord[1])) - 1] == Pole.blue_Checkers))
                    {
                        MasLocationOpponent.Add(LocationOponentLeftDown);
                       // return LocationOponentLeftDown;
                    }
                }
                 if (locatCh == locationRightDown)
                {
                    if ((Pole.DeskMas[(Convert.ToInt32(coord[0]) - 1), (Convert.ToInt32(coord[1])) - 1] == Pole.blue_Checkers))
                    {
                        MasLocationOpponent.Add(LocationOponentLeftUp);
                        //return LocationOponentLeftUp;
                    }
                }
                if(locatCh==0)
                {                   
                    if ((Pole.DeskMas[(Convert.ToInt32(coord[0]) - 1), (Convert.ToInt32(coord[1])) - 1] == Pole.blue_Checkers))
                    {
                        MasLocationOpponent.Add(LocationOponentLeftUp);
                       // return LocationOponentLeftUp;
                    }
                     if ((Pole.DeskMas[(Convert.ToInt32(coord[0]) - 1), (Convert.ToInt32(coord[1])) + 1] == Pole.blue_Checkers))
                    {
                        MasLocationOpponent.Add(LocationOponentRidhtUp);
                        //return LocationOponentRidhtUp;
                    }
                     if ((Pole.DeskMas[(Convert.ToInt32(coord[0]) + 1), (Convert.ToInt32(coord[1])) + 1] == Pole.blue_Checkers))
                    {
                        MasLocationOpponent.Add(LocationOponentRightDown);
                       // return LocationOponentRightDown;
                    }
                     if ((Pole.DeskMas[(Convert.ToInt32(coord[0]) + 1), (Convert.ToInt32(coord[1])) - 1] == Pole.blue_Checkers))
                    {
                        MasLocationOpponent.Add(LocationOponentLeftDown);
                        //return LocationOponentLeftDown;
                    }
                   
                }
                if (MasLocationOpponent.Capacity==0)
                {
                    MasLocationOpponent.Add(0);
                }
               
            }
            return MasLocationOpponent;
        }


        private int LocationChecker(ArrayList coord)
        {
            if (Convert.ToInt32(coord[0]) == 0 && Convert.ToInt32(coord[1]) == 0)
            {
                return locationLeftUp;
            }
            if (Convert.ToInt32(coord[0]) == 7 && Convert.ToInt32(coord[1]) == 7)
            {
                return locationRightDown;
            }
            if (Convert.ToInt32(coord[0]) == 0 && Convert.ToInt32(coord[1]) == 7)
            {
                return locationRightUp;
            }
            if (Convert.ToInt32(coord[0]) == 7 && Convert.ToInt32(coord[1]) == 0)
            {
                return locationLeftDown;
            }
            if (Convert.ToInt32(coord[0]) == 0)
            {
               
                return locationUp;
            }
            if (Convert.ToInt32(coord[0]) == 7)
            {
                
                return locationDown;
            }
            if (Convert.ToInt32(coord[1]) == 7)
            {
               
                return locationRight;
            }
            if (Convert.ToInt32(coord[1]) == 0)
            {
               
                return locationLeft;
            }
           
            return locationCenter;
        }

    }
}
