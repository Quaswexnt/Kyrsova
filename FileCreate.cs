using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Enumeration;
using System.Runtime.CompilerServices;
using System.Diagnostics.Tracing;
using System.Security.Cryptography;

namespace Kyrsova
{
    public class FileOperation {

        private string _filePath;

        private long desireAmount;

        private int lowerBound;

        private int upperBound;
        private long maxAmount = 150000000;
        private int maxBound = 10000;
        private int minBound = -10000;

        public int MaxBound { get => maxBound; }
        public int MinBound { get => minBound; }
        public double MaxAmount { get => maxAmount; }


        public string GetFilePath { get { return _filePath; } set { _filePath = value; } }

        public long DesireAmount
        {
            get { return desireAmount; }
            set
            {

                desireAmount = value;
            }
        }

        public int LowerBound
        {
            get { return lowerBound; }
            set
            {

                lowerBound = value;
            }
        }

        public int UpperBound
        {
            get { return upperBound; }
            set
            {
                upperBound = value;
            }
        }

        public void CreateFile()
        {
            FileStream file = File.Create(_filePath);
            file.Close();
        }

        public void GenArray()
        {

            FileStream file = File.Create(_filePath);
            file.Close();
            Random random = new Random();
            using (StreamWriter writter = new StreamWriter(_filePath))
            {
                for (int i = 0; i < desireAmount; i++)
                {
                    int randomNumber = random.Next(lowerBound, upperBound);
                    writter.WriteLine(randomNumber);

                }
            }

        }
        public void GenOrderedUp()
        {
            List<int> array = new List<int>((int)DesireAmount);


            Random random = new Random();
            for (int i = 0; i < DesireAmount; i++)
            {
                array.Add(random.Next(LowerBound, UpperBound));
            }
            array.Sort();

            using (StreamWriter writter = new StreamWriter(_filePath))
            {
                for (int i = 0; i < array.Count; i++)
                {

                    writter.WriteLine(array[i]);

                }
            }

        }
        public void GenOrderedDown()
        {
            List<int> array = new List<int>((int)DesireAmount);


            Random random = new Random();
            for (int i = 0; i < DesireAmount; i++)
            {
                array.Add(random.Next(LowerBound, UpperBound));
            }
            array.Sort();
            array.Reverse();
            using (StreamWriter writter = new StreamWriter(_filePath))
            {
                for (int i = 0; i < array.Count; i++)
                {

                    writter.WriteLine(array[i]);

                }
            }

        }


    } 
}
