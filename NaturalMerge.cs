using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsova
{ 
    internal class NaturalMerge
    {
        private int helpAmountNatural = 2;
        private int diskAccessCount = 0;
        public int DiskAccessCount { get { return diskAccessCount; } }

        public int ReadTwoAmount { get { return helpAmountNatural; } }

        public string[] CreateTwoHelpFile(string helpDirectoryPath)
        {
            string[] filePaths = new string[ReadTwoAmount];

            
            for (int i = 0; i < ReadTwoAmount; i++)
            {
                 string fileName = $"helpFile{i + 1}.txt";
                 string _filePath = Path.Combine(helpDirectoryPath, fileName);
                 using (FileStream fileStream = File.Create(_filePath)) { }
                 diskAccessCount++;



                filePaths[i] = _filePath;
            }
            
            
            return filePaths;

        }
        public void GetDividedFiles(string mainFile, string[] helpFilePath, bool descending)
        {
            using (StreamWriter writer1 = new StreamWriter(helpFilePath[0]))
            using (StreamWriter writer2 = new StreamWriter(helpFilePath[1]))
            using (StreamReader reader = new StreamReader(mainFile))
            {
               
                bool addToFirstArray = false;
                int? previousNumber = null;

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    diskAccessCount++;


                    if (int.TryParse(line, out int currentNumber))
                    {
                        if (previousNumber.HasValue)
                        {
                            if ((descending && previousNumber.Value <= currentNumber) || (!descending && previousNumber.Value >= currentNumber))
                            {
                                if (addToFirstArray)
                                {
                                    writer1.WriteLine(previousNumber.Value);
                                    diskAccessCount++;
                                }
                                else
                                {
                                    writer2.WriteLine(previousNumber.Value);
                                    diskAccessCount++;
                                }
                                
                            }
                            else
                            {
                                if (addToFirstArray)
                                {
                                    writer1.WriteLine(previousNumber.Value);
                                    addToFirstArray = false;
                                    
                                }
                                else
                                {
                                    writer2.WriteLine(previousNumber.Value);
                                    addToFirstArray = true;
                                    
                                }
                               
                            }
                        }
                        previousNumber = currentNumber;
                    }
                }

                if (previousNumber.HasValue)
                {
                    if (addToFirstArray)
                    {
                        writer1.WriteLine(previousNumber.Value);
                        
                    }
                    else
                    {
                        writer2.WriteLine(previousNumber.Value);
                        
                    }
                    
                }
            }

            File.WriteAllText(mainFile, string.Empty);
            diskAccessCount++;


        }

        public void GetMergedSeries(string[] helpFilePath, string mainFile, bool descending)
        {
            List<int> mergedFile1 = File.ReadAllLines(helpFilePath[0]).Select(int.Parse).ToList();
            List<int> mergedFile2 = File.ReadAllLines(helpFilePath[1]).Select(int.Parse).ToList();
            diskAccessCount += 2;

            List<int> resMerged = new List<int>();

            int i = 0, j = 0;

            if (mergedFile1.Count == 0 || mergedFile2.Count == 0)
            {
                IsSorted(true, helpFilePath);
            }

            while (i < mergedFile1.Count && j < mergedFile2.Count)
            {
                List<int> helpMerged1 = new List<int>();

                while (i < mergedFile1.Count - 1 && (descending ? mergedFile1[i] <= mergedFile1[i + 1] : mergedFile1[i] >= mergedFile1[i + 1]))
                {
                    helpMerged1.Add(mergedFile1[i]);
                    i++;
                }
                helpMerged1.Add(mergedFile1[i]);

                List<int> helpMerged2 = new List<int>();
                while (j < mergedFile2.Count - 1 && (descending ? mergedFile2[j] <= mergedFile2[j + 1] : mergedFile2[j] >= mergedFile2[j + 1]))
                {
                    helpMerged2.Add(mergedFile2[j]);
                    j++;
                }
                helpMerged2.Add(mergedFile2[j]);

                resMerged.AddRange(MergeSeries(helpMerged1, helpMerged2, descending));
                i++;
                j++;
            }

            while (i < mergedFile1.Count)
            {
                resMerged.Add(mergedFile1[i]);
                i++;
            }

            while (j < mergedFile2.Count)
            {
                resMerged.Add(mergedFile2[j]);
                j++;
            }

            using (StreamWriter writer = new StreamWriter(mainFile))
            {
                foreach (int item in resMerged)
                {
                    writer.WriteLine(item);
                    diskAccessCount++;


                }
            }
        }

        public List<int> MergeSeries(List<int> arr1, List<int> arr2, bool descending)
        {
            List<int> result = new List<int>();
            int i = 0, j = 0;

            while (i < arr1.Count && j < arr2.Count)
            {
                if ((descending && arr1[i] <= arr2[j]) || (!descending && arr1[i] >= arr2[j]))
                {
                    result.Add(arr1[i]);
                    i++;
                }
                else
                {
                    result.Add(arr2[j]);
                    j++;
                }
            }

            while (i < arr1.Count)
            {
                result.Add(arr1[i]);
                i++;
            }

            while (j < arr2.Count)
            {
                result.Add(arr2[j]);
                j++;
            }

            return result;
        }

        public  void IsSorted(bool n, string[] path)
        {
            if (n == true)
            {
                File.Delete(path[0]);
                File.Delete(path[1]);
                diskAccessCount += 2;

            }
        }
    }


}

      