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
using System.Drawing;

namespace Kyrsova
{
    public class FileOperation {

        private string _filePath;

        private long desireAmount;

        private int lowerBound;

        private int upperBound;
        private long maxAmount = 150000000;
        private int maxBound = 1000000;
        private int minBound = -1000000;

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
            Random rand = new Random();

            
            double step = (double)(UpperBound - LowerBound) / DesireAmount;

            using (StreamWriter writer = new StreamWriter(_filePath))
            {
                
                int previous = rand.Next(LowerBound, LowerBound + (int)step + 1);
                writer.WriteLine(previous);

                for (int i = 1; i < DesireAmount; i++)
                {
                   
                    int nextMin = previous + 1;
                    int nextMax = Math.Min(UpperBound, previous + (int)step + 1);

                    previous = rand.Next(nextMin, nextMax);
                    writer.WriteLine(previous);
                }
            }
        }
        public void GenOrderedDown()
        {
            Random rand = new Random();
            double step = (double)(UpperBound - LowerBound) / DesireAmount;

            using (StreamWriter writer = new StreamWriter(_filePath))
            {
                int previous = rand.Next(UpperBound - (int)step, UpperBound + 1);
                writer.WriteLine(previous);

                for (int i = 1; i < DesireAmount; i++)
                {
                    int nextMax = previous - 1;
                    int nextMin = Math.Max(LowerBound, previous - (int)step);

                    previous = rand.Next(nextMin, nextMax + 1);
                    writer.WriteLine(previous);
                }
            }
        }


    } 
}
