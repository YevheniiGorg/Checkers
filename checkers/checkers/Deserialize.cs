using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace checkers
{
    class Deserialize
    {

        public void ReadFile()
        {
            FileStream FS = new FileStream("Game_Checkers.dat", FileMode.Open, FileAccess.ReadWrite);
            BinaryFormatter BinFormat = new BinaryFormatter();
            Game G = new Game();
            G = (Game)BinFormat.Deserialize(FS);
            FS.Close();
            G.GAME();
        }
       
    }
}
