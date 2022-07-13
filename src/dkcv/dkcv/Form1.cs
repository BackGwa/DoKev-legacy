using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;

namespace dkcv
{

    public partial class Form1 : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        [DllImport("user32")]
        private static extern bool SetForegroundWindow(IntPtr handle);

        private bool m_aeroEnabled;
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;

        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();

                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW;

                return cp;
            }
        }

        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 1,
                            rightWidth = 1,
                            topHeight = 1
                        };
                        DwmExtendFrameIntoClientArea(this.Handle, ref margins);

                    }
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)
                m.Result = (IntPtr)HTCAPTION;
        }

        public Form1()
        {
            m_aeroEnabled = false;
            this.FormBorderStyle = FormBorderStyle.None;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void Delay(int ms)
        {
            DateTime dateTimeNow = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, ms);
            DateTime dateTimeAdd = dateTimeNow.Add(duration);
            while (dateTimeAdd >= dateTimeNow)
            {
                System.Windows.Forms.Application.DoEvents();
                dateTimeNow = DateTime.Now;
            }
            return;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            FileInfo dofilePath = new FileInfo(@"C:\temp\convert.do");
            if (dofilePath.Exists){
                Converter();
            }
            else {
                Application.Exit();
            }
        }

        string[] wline = new string[1];
        string[] variableB = new string[0];

        public void Converter()
        {

            int counter = 0;
            string[] stringArray = new string[0];

            label1.Text = "초기화 하는 중...";
            Delay(500);

            foreach (string line in System.IO.File.ReadLines("convert.do"))
            {
                Array.Resize(ref stringArray, stringArray.Length + 1);
                stringArray[counter] = line;
                counter++;
            }

            string sourceString = " ", cutting = "";
            bool checking_bool = false;

            foreach (string line in stringArray)
            {
               // checking_bool = line.Contains("&&");
               string encoder = "", Ecdr = "";
                Ecdr = line;

                //Array.Clear(variableB, 0, variableB.Length);

                //if (checking_bool)
                //{
                //encoder = checking(line);



                //for (int i = 0; i <= variableB.Length; i++)
                //{
                //        Ecdr = line.Replace(encoder, "{dklib" + i + "}");
                //}

                //     foreach (string vbsline in System.IO.File.ReadLines("kev\\exhand.kev"))
                //    {
                //         string[] stringSeparators = new string[] { " > " };
                //       string[] changeValue = vbsline.Split(stringSeparators, StringSplitOptions.None);
                //       encoder = encoder.Replace(changeValue[0], changeValue[1]);
                // }
                // }

                foreach (string vbsline in System.IO.File.ReadLines("kev\\default.kev"))
                {
                    string[] stringSeparators = new string[] { " > " };
                    string[] changeValue = vbsline.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                    Ecdr = Ecdr.Replace(changeValue[0], changeValue[1]);
                }

                //for (int i = 0; i <= variableB.Length; i++)
              //  {
           //         Ecdr = Ecdr.Replace("dklib" + i + "}", variableB[i]);
             //   }

                wline[wline.Length - 1] = Ecdr;
                Console.WriteLine(wline[wline.Length - 1]);
                Array.Resize(ref wline, wline.Length + 1);

            }
            StreamWriter writer;
            writer = File.CreateText("export\\convert.py");
            writer.WriteLine("#!/usr/bin/env python");
            writer.WriteLine("# -*- coding: utf-8 -*-");
            foreach (string itemA in wline)
            {
                writer.WriteLine(itemA);
            }
            writer.Close();

            Delay(50);
            label1.Text = "디버깅이 시작되었습니다.";
            string exe_name = Application.StartupPath + "\\dkpl.exe";
            Process.Start(exe_name);
            Delay(250);
            Application.Exit();

        }


        public string checking(string sourceString)
        {
            string returnAiv = "";

            return returnAiv;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
