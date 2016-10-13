using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers
{
    [Serializable]
    class CollectionForLok
    {
        public List<int> Keys = new List<int>();
        public List<int> Values = new List<int>();
      

        public void Add(int t1, int t2)
        {
            Keys.Add(t1);
            Values.Add(t2);
        }

        public void ShowCollection()
        {
            if (Keys.Count==0)
            {
                Console.WriteLine("Пусто.");
            }
            for (int i = 0,j=1; i < Keys.Count; i++,j++)
            {
                Console.Write((j)+". ");

                Console.Write(Convert.ToChar(Values[i]+97));
                Console.Write(Keys[i]+1);

                if (i+1< Keys.Count)
                {
                    Console.Write("-");
                    Console.Write(Convert.ToChar(Values[i+1] + 97));
                    Console.Write(Keys[i+1] + 1);
                    Console.WriteLine();
                    i++;
                }
                
            }
        }

        public void Clear()
        {
            Keys.Clear();
            Values.Clear();
        }
    }
}
