using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBotManager
{
    class SaveTarget
    {
        //public static int[] TargetID = { 0, 2, 52, 45698 };
        public static int[] TargetID = new int[10000];

        public void SearchAndAdd(int Id)
        {           
            int Size = TargetID.Length;
            for (int i = 0; i < Size; i++)
            {
                if(TargetID[i] == 0)
                {
                    TargetID[i] = Id;
                    break;
                }
            }
        }

        public void SaveArray()
        {
            string path = @"D:\\MyTest.txt";
            string[] IDText = new string[10000];
            if (!File.Exists(path))
            {
                for(int i = 0; i < TargetID.Length; i++)
                {
                    IDText[i] = TargetID[i].ToString();
                }
                File.WriteAllLines(path, IDText, Encoding.UTF8);
            }

            //string appendText = "This is extra text" + Environment.NewLine;
            //File.AppendAllText(path, appendText, Encoding.UTF8);

            string[] readText = File.ReadAllLines(path, Encoding.UTF8);
            foreach (string s in readText)
            {
                
            }
        }
    }
}
