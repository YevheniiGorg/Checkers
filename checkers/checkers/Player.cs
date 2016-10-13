using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers
{
    [Serializable]
    class Player
    {
        public string Name {get;set;}
        public int ID { get; set;}
        public int Current_Ccount { get; set;}

        public Player(string name, int id)
        {
           
            Name = name;

            ID = id;
            Current_Ccount = 12;
        }

        public void ShowCount()
        {
            Console.Write("Игрок "+Name+": "+Current_Ccount+" ");
        }
        
    }
}
