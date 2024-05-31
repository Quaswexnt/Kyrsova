using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Kyrsova
{
    public class MultiWay
    {
        private int fileAmountMult = 10;
        private string finalPath;

        private int diskAccessCount = 0;
        public int DiskAccessCount { get { return diskAccessCount; } }

        public int ReadAmount { get { return fileAmountMult; } }

        public string[] CreateHelpFile(string helpDirectoryPath)
        {
            string[] filePaths = new string[ReadAmount];


            for (int i = 0; i < ReadAmount; i++)
            {
                string fileName = $"helpFile{i + 1}.txt";
                string _filePath = Path.Combine(helpDirectoryPath, fileName);
                using (FileStream fileStream = File.Create(_filePath)) { diskAccessCount++; }
                

                filePaths[i] = _filePath;
               

            }


            return filePaths;
        }

        public void DivideFile(string filename, string[] helpFilePaths)
        {
            try
            {
                using (FileStream fileStream = File.OpenRead(filename))
                using (StreamReader reader = new StreamReader(fileStream))
                {
                   
                    FileStream[] fileStreams = new FileStream[helpFilePaths.Length];
                    for (int i = 0; i < helpFilePaths.Length; i++)
                    {
                        fileStreams[i] = File.OpenWrite(helpFilePaths[i]);
                        
                    }

                    string line;
                    int fileIndex = 0;

                    
                    while ((line = reader.ReadLine()) != null)
                    {
                        
                        byte[] data = Encoding.UTF8.GetBytes(line + Environment.NewLine);
                        fileStreams[fileIndex].Write(data, 0, data.Length);

                        fileIndex = (fileIndex + 1) % helpFilePaths.Length;
                        diskAccessCount++;
                    }

                    
                    foreach (var stream in fileStreams)
                    {
                        stream.Close();  
                    }
                }

                
                if (File.Exists(filename))
                {
                    

                    File.Delete(filename);
                    diskAccessCount++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при разделении файла: {ex.Message}");
            }
        }



        public void SortHelpFiles(string[] helpFilePaths, bool ascending)
        {
            foreach (string filePath in helpFilePaths)
            {
                List<int> numbers = new List<int>();

                using (StreamReader reader = new StreamReader(filePath))
                {
                    diskAccessCount++;
                    string line;
                    
                    while ((line = reader.ReadLine()) != null)
                    {
                        
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            continue;
                        }

                        if (int.TryParse(line, out int number))
                        {
                            numbers.Add(number);
                        }

                       
                    }
                    
                }
                

                if (ascending)
                {
                    numbers.Sort();
                }
                else
                {
                    numbers.Sort();
                    numbers.Reverse();
                }

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (int number in numbers)
                    {
                        writer.WriteLine(number);
                        

                    }
                    
                }
                diskAccessCount++;

            }
        }

        public void RealMergeHelpFile(string[] helpFilePath, bool ascending)
        {
            int countFile1 = 0;
            int countFile2 = 1;

            while (countFile2 < helpFilePath.Length)
            {
                if (helpFilePath[countFile1] != null && helpFilePath[countFile2] != null)
                {
                    string mergedFileName = $"MyFile.txt";
                    string mergedFilePath = Path.Combine(Path.GetDirectoryName(helpFilePath[countFile1]), mergedFileName);

                    if (File.Exists(mergedFilePath))
                    {
                        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(mergedFileName);
                        string fileExtension = Path.GetExtension(mergedFileName);
                        mergedFileName = $"{fileNameWithoutExtension}_{DateTime.Now.Ticks}{fileExtension}";
                        mergedFilePath = Path.Combine(Path.GetDirectoryName(helpFilePath[countFile1]), mergedFileName);
                    }

                    List<int> mergedFile1 = File.ReadAllLines(helpFilePath[countFile1]).Select(int.Parse).ToList();
                    List<int> mergedFile2 = File.ReadAllLines(helpFilePath[countFile2]).Select(int.Parse).ToList();
                    diskAccessCount += 2;



                    List<int> resultMerge = new List<int>();
                    int i = 0;
                    int j = 0;
                    while (i < mergedFile1.Count && j < mergedFile2.Count)
                    {
                        if (ascending ? (mergedFile1[i] <= mergedFile2[j]) : (mergedFile1[i] >= mergedFile2[j]))
                        {
                            resultMerge.Add(mergedFile1[i]);
                            i++;
                        }
                        else
                        {
                            resultMerge.Add(mergedFile2[j]);
                            j++;
                        }
                    }

                    while (i < mergedFile1.Count)
                    {
                        resultMerge.Add(mergedFile1[i]);
                        i++;
                    }

                    while (j < mergedFile2.Count)
                    {
                        resultMerge.Add(mergedFile2[j]);
                        j++;
                    }

                    File.Delete(helpFilePath[countFile1]);
                    
                    File.Delete(helpFilePath[countFile2]);
                    diskAccessCount += 2;



                    using (StreamWriter writer = new StreamWriter(mergedFilePath))
                    {
                        foreach (int value in resultMerge)
                        {
                            writer.WriteLine(value);
                            


                        }
                        

                    }
                    


                }

                countFile1 += 2;
                countFile2 += 2;
            }
        }
    }
}