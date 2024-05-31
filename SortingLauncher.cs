using Kyrsova;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsova
{
    internal class SortingLauncher
    {

        public string finalPath;
        public int complexity;


        public void PolyPhaseSort(bool acsending)
        {
            PolyPhase poly = new PolyPhase();
            string[] pathHelp;
            pathHelp = poly.CreateHelpFile(@"C:\Users\nnhf2\Desktop\Курсова робота\Файлы\");
            poly.MergeSeriesUntilOneRemains(@"C:\Users\nnhf2\Desktop\Курсова робота\Файлы\myFile.txt", pathHelp,acsending);
            string[] localDirectory = Directory.GetFiles(@"C:\Users\nnhf2\Desktop\Курсова робота\Файлы\");
            finalPath = localDirectory[0];
            complexity++;

            complexity = poly.DiskAccessCount;

        }
        public void MultiWaySort(bool acsending)
        {
            MultiWay multiway = new MultiWay();
            string[] pathHelp;
            pathHelp = multiway.CreateHelpFile(@"C:\Users\nnhf2\Desktop\Курсова робота\Файлы\");
            multiway.DivideFile(@"C:\Users\nnhf2\Desktop\Курсова робота\Файлы\myFile.txt", pathHelp);

            string[] localDirectory = Directory.GetFiles(@"C:\Users\nnhf2\Desktop\Курсова робота\Файлы\");

            while (localDirectory.Length > 1)
            {
                string[] helpReloadPaths = new string[localDirectory.Length];
                Array.Copy(localDirectory, helpReloadPaths, localDirectory.Length);

                multiway.SortHelpFiles(helpReloadPaths, acsending );
                multiway.RealMergeHelpFile(helpReloadPaths, acsending);
                
                localDirectory = Directory.GetFiles(@"C:\Users\nnhf2\Desktop\Курсова робота\Файлы\");
                complexity++;
            }
            finalPath = localDirectory[0];

            complexity = multiway.DiskAccessCount;
        }





        public void NaturalSort(bool acsending)
        {

            NaturalMerge natural = new NaturalMerge();
            string[] pathHelp;
            pathHelp = natural.CreateTwoHelpFile(@"C:\Users\nnhf2\Desktop\Курсова робота\Файлы\");

            string[] localDirectory = Directory.GetFiles(@"C:\Users\nnhf2\Desktop\Курсова робота\Файлы\");

            while (localDirectory.Length > 1)
            {
                string[] helpReloadPaths = new string[localDirectory.Length];
                Array.Copy(localDirectory, helpReloadPaths, localDirectory.Length);

                natural.GetDividedFiles(@"C:\Users\nnhf2\Desktop\Курсова робота\Файлы\myFile.txt", pathHelp, acsending);
                natural.GetMergedSeries(pathHelp, @"C:\Users\nnhf2\Desktop\Курсова робота\Файлы\myFile.txt",acsending);
                
                localDirectory = Directory.GetFiles(@"C:\Users\nnhf2\Desktop\Курсова робота\Файлы\");
                complexity++;
                
            }
            complexity = natural.DiskAccessCount;
           

        }
    }
}

            

        
    

        