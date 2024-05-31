
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.CodeDom.Compiler;
using static System.Windows.Forms.Design.AxImporter;


namespace Kyrsova
{
    public partial class Form1 : Form
    {

        FileOperation file = new FileOperation();
        SortingLauncher launcher = new SortingLauncher();
        private CheckBoxGroupManager checkBoxGroupManager;
        private CheckBoxGroupManager checkBoxGroupManager2;
        private bool isAscending = true;

        private int[] initialFilePaths;
        private int[] sortedFilePaths;

        public Form1()
        {
            InitializeComponent();
            InitializeCheckBoxGroupManager();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void InitializeCheckBoxGroupManager()
        {
            Action[] actions = new Action[]
            {
                () =>
                {
                    ClearDirectory(@"C:\Users\nnhf2\Desktop\������� ������\�����\",@"C:\Users\nnhf2\Desktop\������� ������\�����\tempFiles\");
                    file.GetFilePath = @"C:\Users\nnhf2\Desktop\������� ������\�����\myFile.txt";
                    file.CreateFile();
                    file.GenOrderedUp();
                    File.Copy(file.GetFilePath,@"C:\Users\nnhf2\Desktop\������� ������\�����\tempFiles\inputFile.txt");
                    initialFilePaths= File.ReadLines(file.GetFilePath)
                       .Take(300)
                       .Select(int.Parse)
                       .ToArray();

                },
                ()=>
                {
                    ClearDirectory(@"C:\Users\nnhf2\Desktop\������� ������\�����\", @"C:\Users\nnhf2\Desktop\������� ������\�����\tempFiles\");
                    file.GetFilePath = @"C:\Users\nnhf2\Desktop\������� ������\�����\myFile.txt";
                    file.CreateFile();
                    file.GenOrderedDown();
                    File.Copy(file.GetFilePath,@"C:\Users\nnhf2\Desktop\������� ������\�����\tempFiles\inputFile.txt");
                    initialFilePaths= File.ReadLines(file.GetFilePath)
                       .Take(300)
                       .Select(int.Parse)
                       .ToArray();
                },
                ()=>
                {

                    ClearDirectory(@"C:\Users\nnhf2\Desktop\������� ������\�����\", @"C:\Users\nnhf2\Desktop\������� ������\�����\tempFiles\");
                    file.GetFilePath = @"C:\Users\nnhf2\Desktop\������� ������\�����\myFile.txt";
                    file.CreateFile();
                    file.GenArray();
                    File.Copy(file.GetFilePath,@"C:\Users\nnhf2\Desktop\������� ������\�����\tempFiles\inputFile.txt");
                    initialFilePaths= File.ReadLines(file.GetFilePath)
                       .Take(300)
                       .Select(int.Parse)
                       .ToArray();

                }
            };

            checkBoxGroupManager = new CheckBoxGroupManager(actions, checkBox4, checkBox5, checkBox6);

            Action[] sortActions = new Action[]
                {
                    () =>
                    {   if(file.DesireAmount>0)
                        {
                        launcher.NaturalSort(isAscending);
                        File.Copy(file.GetFilePath,@"C:\Users\nnhf2\Desktop\������� ������\�����\tempFiles\outPutFile.txt");
                        sortedFilePaths = File.ReadLines(file.GetFilePath)
                                          .Take(300)
                                          .Select(int.Parse)
                                          .ToArray();
                        }
                        else
                        {
                            MessageBox.Show("����� ����� �� ������������ !");
                        }

                    },
                    ()=>
                    {
                        if(file.DesireAmount>0)
                        {
                            launcher.MultiWaySort(isAscending);
                            File.Copy(launcher.finalPath,@"C:\Users\nnhf2\Desktop\������� ������\�����\tempFiles\outPutFile.txt");
                            sortedFilePaths = File.ReadLines(launcher.finalPath)
                                              .Take(300)
                                              .Select(int.Parse)
                                              .ToArray();
                        }
                        else
                        {
                            MessageBox.Show("����� ����� �� ������������ !");
                        }
                    },
                    ()=>
                    {


                        if (file.DesireAmount > 0)
                        {
                            launcher.PolyPhaseSort(isAscending);
                            File.Copy(launcher.finalPath,@"C:\Users\nnhf2\Desktop\������� ������\�����\tempFiles\outPutFile.txt");
                            sortedFilePaths = File.ReadLines(launcher.finalPath)
                                              .Take(300)
                                              .Select(int.Parse)
                                              .ToArray();
                        }
                        else
                        {
                            MessageBox.Show("����� ����� �� ������������ !");
                        }
                    }
                };


            checkBoxGroupManager2 = new CheckBoxGroupManager(sortActions, checkBox1, checkBox2, checkBox3);
        }



        private void ClearDirectory(string path1, string path2)
        {
            if (Directory.Exists(path1))
            {
                foreach (var file in Directory.GetFiles(path1))
                {
                    File.Delete(file);
                }
            }
            if (Directory.Exists(path2))
            {
                foreach (var file in Directory.GetFiles(path2))
                {
                    File.Delete(file);
                }
            }
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    
                    textBox1.Clear();
                    file.DesireAmount = 0;
                    e.SuppressKeyPress = true;
                    return;
                }

                if (textBox1.Text == "-")
                {
                    return;
                }

                if (long.TryParse(textBox1.Text, out long sizeValue))
                {
                    if (sizeValue <= 0)
                    {
                        MessageBox.Show($"�������� ������ ����� �� ���� ���� ������� �� 0!");
                        textBox1.Clear();
                    }
                    else if (sizeValue > file.MaxAmount)
                    {
                        MessageBox.Show($"�������� ������ ����� �� ���� ���� ������ �� 150 �������! ");
                        textBox1.Clear();
                    }
                    else
                    {
                        file.DesireAmount = sizeValue;
                    }
                }
                else
                {
                    MessageBox.Show("������ ������� ��� ��� ������ �����!");
                    textBox1.Clear();
                }

                e.SuppressKeyPress = true;
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                   
                    textBox2.Clear();
                    file.LowerBound = 0;
                    e.SuppressKeyPress = true;
                    return;
                }

                if (textBox2.Text == "-")
                {
                    return;
                }

                if (int.TryParse(textBox2.Text, out int minBound))
                {
                    if (minBound < file.MinBound)
                    {
                        MessageBox.Show($"�������� �������� ��������� �� ���� ���� ������� �� {file.MinBound}!");
                        textBox2.Clear();
                    }
                    else if (file.UpperBound != 0 && minBound > file.UpperBound) 
                    {
                        MessageBox.Show($"̳������� �������� �� ���� ���� ������ �� ����������� �������� {file.UpperBound}!");
                        textBox2.Clear();
                    }
                    else
                    {
                        file.LowerBound = minBound;
                    }
                }
                else
                {
                    MessageBox.Show("������ ������� ��� ��� �������� ��� ���������!");
                    textBox2.Clear();
                }

                e.SuppressKeyPress = true;
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    textBox3.Clear();
                    file.UpperBound = 0;
                    e.SuppressKeyPress = true;
                    return;
                }
                if (textBox3.Text == "-")
                {
                    return;
                }

                if (int.TryParse(textBox3.Text, out int maxBound))
                {
                    if (file.LowerBound != 0 && maxBound <= file.LowerBound)
                    {
                        MessageBox.Show($"�������� ����������� ��������� �� ���� ���� ������� �� {file.LowerBound}!");
                        textBox3.Clear();
                    }
                    else if (maxBound > file.MaxBound)
                    {
                        MessageBox.Show($"�������� ����������� ��������� �� ���� ���� ������ �� {file.MaxBound}!");
                        textBox3.Clear();
                    }
                    else
                    {
                        file.UpperBound = maxBound;
                    }
                }
                else
                {
                    MessageBox.Show("������ ������� ��� ��� ����������� ��� ���������!");
                    textBox3.Clear();
                }

                e.SuppressKeyPress = true;
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
            {
                isAscending = false;
                checkBox8.Checked = false;
            }


        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked)
            {
                isAscending = true;
                checkBox7.Checked = false;
            }

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!checkBox7.Checked && !checkBox8.Checked)
            {
                MessageBox.Show("���� ����� ������ ������ ���������� �����!");
                return;
            }


            if (checkBoxGroupManager.CurrentAction == null)
            {
                MessageBox.Show("���� ����� ������ ������ ��������� �����!");
                return;
            }


            if (checkBoxGroupManager2.CurrentAction == null)
            {
                MessageBox.Show("���� �����, ������ ����� ����������!");
                return;
            }

            if (file.LowerBound == 0 || file.UpperBound == 0)
            {
                MessageBox.Show("���� �����, ��������� ����� ������� ��������� �����!");
                return;
            }



            checkBoxGroupManager.CurrentAction();
            checkBoxGroupManager2.CurrentAction();
            UpdateDiskOperations();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.LightBlue;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (sortedFilePaths == null || sortedFilePaths.Length == 0)
            {
                MessageBox.Show("����� �� �� ���� ����������! ");
                return;
            }
            string[] localDirectory = Directory.GetFiles(@"C:\Users\nnhf2\Desktop\������� ������\�����\");
            if (localDirectory.Length == 0)
            {
                MessageBox.Show("��������� �� ������ ����� ��� ����������! ");
                return;
            }
            FormDisplay formDisplay = new FormDisplay();
            formDisplay.SetArrays(initialFilePaths, sortedFilePaths);
            formDisplay.Show(); ;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string directoryPath = @"C:\Users\nnhf2\Desktop\������� ������\�����\";
            string tempDirectory = @"C:\Users\nnhf2\Desktop\������� ������\�����\tempFiles";
            ClearDirectory(directoryPath, tempDirectory);
            MessageBox.Show("���������� �������");
        }

        private void UpdateDiskOperations()
        {

            if (launcher.complexity > 0)
            {

                label14.Text = $"{launcher.complexity}";
            }

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

    }
}
