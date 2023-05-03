using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;

namespace dkcv
{
    class Program
    {
        static void Main(string[] args)
        {

            var ARCH = System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture;
            var OS = System.Environment.OSVersion.Platform.ToString();

            string[] wline = new string[1];
            string[] variableB = new string[0];
            int indexF = 0, fileindex = 0;
            bool converting = false, randomBool = false, osBool = false;

            void Converter()
            {

                int counter = 0;
                string[] stringArray = new string[0];

                indexF = 0;
                converting = false;
                Array.Clear(wline, 0, wline.Length);
                Array.Clear(variableB, 0, variableB.Length);

                foreach (string line in System.IO.File.ReadLines("convert.dkv"))
                {
                    Array.Resize(ref stringArray, stringArray.Length + 1);
                    stringArray[counter] = line;
                    if (line.Contains("필요한 라이브러리는") && line.Contains("random"))
                    {
                        randomBool = true;
                    }
                    if (line.Contains("필요한 라이브러리는") && line.Contains("os"))
                    {
                        osBool = true;
                    }
                    counter++;
                }

                bool checking_bool = false;

                foreach (string line in stringArray)
                {
                    checking_bool = line.Contains("@\"");
                    string encoder = "", Ecdr = "";

                    Array.Clear(variableB, 0, variableB.Length);
                    converting = false;

                    if (checking_bool)
                    {
                        Array.Clear(variableB, 0, variableB.Length);
                        indexF = 0;
                        converting = true;
                        encoder = checking(line);
                        Ecdr = encoder;

                        for (int i = 0; i < variableB.Length; i++)
                        {
                            string fiv = "";
                            fiv = variableB[i];
                            if (fiv != null)
                            {
                                foreach (string vbsline in System.IO.File.ReadLines("kev\\exhand.kev"))
                                {
                                    string[] stringSeparators = new string[] { " > " };
                                    string[] changeValue = vbsline.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                                    if (fiv.Contains(changeValue[0]))
                                    {
                                        variableB[i] = fiv.Replace(changeValue[0], changeValue[1]);
                                    }
                                    fiv = variableB[i];
                                }
                            }
                        }
                    }
                    else Ecdr = line;

                    indexF = 0;

                    if (osBool)
                    {
                        foreach (string vbsline in System.IO.File.ReadLines("kev\\os.kev"))
                        {
                            string[] stringSeparators = new string[] { " > " };
                            string[] changeValue = vbsline.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                            Ecdr = Ecdr.Replace(changeValue[0], changeValue[1]);
                        }
                    }

                    if (randomBool)
                    {
                        foreach (string vbsline in System.IO.File.ReadLines("kev\\random.kev"))
                        {
                            string[] stringSeparators = new string[] { " > " };
                            string[] changeValue = vbsline.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                            Ecdr = Ecdr.Replace(changeValue[0], changeValue[1]);
                        }
                    }

                    foreach (string vbsline in System.IO.File.ReadLines("kev\\default.kev"))
                    {
                        string[] stringSeparators = new string[] { " > " };
                        string[] changeValue = vbsline.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                        Ecdr = Ecdr.Replace(changeValue[0], changeValue[1]);
                    }

                    if (converting)
                    {
                        for (int i = 0; i < variableB.Length; i++)
                        {
                            Ecdr = Ecdr.Replace("^IX" + i + "^", "\"" + variableB[i] + "\"");
                            Ecdr = Ecdr.Replace("@", "");
                        }
                    }

                    converting = false;
                    fileindex += 1;

                    wline[wline.Length - 1] = Ecdr;
                    Array.Resize(ref wline, wline.Length + 1);

                }

                StreamWriter writer;
                writer = File.CreateText("export\\convert.py");

                foreach (string itemA in wline)
                {
                    writer.WriteLine(itemA);
                }

                writer.Close();

                ProcessStartInfo psi = new ProcessStartInfo();
                string pathdo = System.AppContext.BaseDirectory;
                string py = System.AppContext.BaseDirectory;

                if (OS == "Unix")
                {
                    Process.Start("python3 -d export/convert.py");
                }
                else
                {
                    if (ARCH == Architecture.Arm64)
                    {
                        py = py + "Python\\ARM\\Windows\\python.exe";
                    }
                    else
                    {
                        py = py + "Python\\x86\\Windows\\python.exe";
                    }
                    pathdo = pathdo + "export\\convert.py";
                    psi.FileName = @py;
                    psi.Arguments = "\"" + pathdo + "\"" + " -d";
                    Process.Start(psi);
                }
                Console.ReadLine();
            }

            string checking(string sourceString)
            {

                string[] returnAiv = new string[48];
                string aiv = " ", right = " ", non_change = sourceString;

                string[] stringSeparators = new string[] { "@\"" };
                returnAiv = sourceString.Split(stringSeparators, StringSplitOptions.None);
                Array.Resize(ref returnAiv, returnAiv.Length + 1);

                stringSeparators = new string[] { "\"" };
                right = returnAiv[1];

                returnAiv = right.Split(stringSeparators, StringSplitOptions.None);

                aiv = returnAiv[0];

                Array.Resize(ref variableB, variableB.Length + 1);
                variableB[indexF] = aiv;

                non_change = non_change.Replace("@\"" + aiv + "\"", "^IX" + indexF + "^");

                indexF += 1;

                if (non_change.Contains("@\""))
                {
                    return checking(non_change);
                }
                else
                {
                    return non_change;
                }
            }

            FileInfo dofilePath = new FileInfo("convert.dkv");
            if (dofilePath.Exists) Converter();
            else return;

        }
    }
}