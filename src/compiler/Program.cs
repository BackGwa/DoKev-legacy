using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Text;

namespace DoKevEngine {

    class Program {
        static void Main(string[] args) {

            /* 컴파일러 화면 구성 */
            Console.Clear();
            Console.Title = "DoKev Runner";


            /* 로거 선언 */
            string logdirectory = AppDomain.CurrentDomain.BaseDirectory + "/log";
            DirectoryInfo di = new DirectoryInfo(logdirectory);
            if (!di.Exists) di.Create();

            DateTime logtime = DateTime.Now;

            StreamWriter logger;
            logger = File.CreateText($"{logdirectory}/{logtime.ToString("yyyy-MM-dd HH_mm_ss")}.log");


            /* ini 유효성 확인 및 선언 */
            IniFile ini = new IniFile();
            IniFile lang = new IniFile();
            try {
                ini.Load($"{AppDomain.CurrentDomain.BaseDirectory}/config.ini");
                lang.Load($"{AppDomain.CurrentDomain.BaseDirectory}/{cfg("folder", "language")}/{cfg("builder", "language")}.ini");

                log(Locale("info", "builder"), cfg("builder", "version"));
            } catch {
                log("Fatal Error", "An error occurred during initialization.", "fatal");
                Console.ReadKey(); return;
            }


            /* 시스템 아키텍쳐와 운영체제 정보 선언 */
            var ARCH = System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture;
            var OS = System.Environment.OSVersion.Platform.ToString();

            log(Locale("info", "system"), $"{OS} ({ARCH})");
            log(Locale("info", "lang"), cfg("builder", "language"));


            /* baseDirectory에 빌드 툴의 절대 경로 선언 */
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            log($"\n{Locale("info", "path")}", baseDirectory);


            /* 빌드 파일 폴더 유무 확인 */
            string exportfolder = AppDomain.CurrentDomain.BaseDirectory + $"{cfg("folder", "export")}";
            DirectoryInfo ef = new DirectoryInfo(exportfolder);
            if (!ef.Exists) {
                log($"\n{Locale("createfolder", "title")}", Locale("createfolder", "invalid"), "warning");
                try {
                    ef.Create();
                    log($"{Locale("createfolder", "title")}", $"{Locale("createfolder", "create")}\n", "success");
                } catch {
                    log($"{Locale("createfolder", "title")}", $"{Locale("createfolder", "error")}\n", "fatal");
                }
            }


            /* filename 설정 및 선언 */
            string targetfile = cfg("filename", "target");
            string exportfile = cfg("filename", "export");


            /* 변환 과정에서 필요한 변수 선언 */
            bool converting = false;
            bool randomBool = false, osBool = false;
            string[] wline = new string[1];
            string[] ExceptList = new string[0];
            int ExceptNum = 0, fileindex = 0;
            string filePath = "";


            /* targetfile 유효성 확인을 위한 경로 선언 */
            try {
                filePath = Path.Combine(baseDirectory, targetfile);
            } catch {
                log(Locale("target", "error"), "fatal");
                Writelog("", true);
                return;
            }


            /* targetfile 유효성 확인 */
            if (File.Exists(filePath)) {
                log(Locale("target", "isvalid"), $"{targetfile} {Locale("target", "valid")}", "success", true);
                log(NowTime(), $"{Locale("build", "start")}\n");
                Converter();
            } else {
                log(Locale("target", "isvalid"), $"{targetfile} {Locale("target", "invalid")}", "fatal");
                Writelog("", true);
                return;
            }


            /* Converter
             * convert.dkv 파일을 Python 코드로 빌드합니다. */
            void Converter() {

                log(NowTime(), Locale("build", "initializing"));

                int counter = 0;
                string[] stringArray = new string[0];
                bool checking_bool = false;

                ExceptNum = 0;
                converting = false;
                Array.Clear(wline, 0, wline.Length);
                Array.Clear(ExceptList, 0, ExceptList.Length);

                log(NowTime(), $"{Locale("build", "initialized")}\n", "success");
                log(NowTime(), Locale("lib", "ready"));

                foreach (string line in System.IO.File.ReadLines($"{baseDirectory}/{targetfile}")) {
                    if(line.Replace(" ", "") != "") {
                        log(NowTime(), $"[{counter}] : {Locale("lib", "check")}");

                        Array.Resize(ref stringArray, stringArray.Length + 1);
                        stringArray[counter] = line;

                        if (line.Contains("필요한 라이브러리는") && line.Contains("random")) randomBool = true;
                        if (line.Contains("필요한 라이브러리는") && line.Contains("os")) osBool = true;

                        counter++;
                    }
                }

                log(NowTime(), Locale("lib", "finish"), "success");

                foreach (string line in stringArray) {

                    if (line.Replace(" ", "") == "") continue;

                    checking_bool = line.Contains("@\"");
                    string encoder = "", code = "";

                    Array.Clear(ExceptList, 0, ExceptList.Length);
                    converting = false;

                    log($"\n{NowTime()}", $"{Locale("convert", "wait")} : {line.Replace("    ", "")}");

                    if (checking_bool) {
                        log(NowTime(), Locale("except", "ishas"));

                        Array.Clear(ExceptList, 0, ExceptList.Length);
                        ExceptNum = 0;
                        converting = true;

                        try {
                            encoder = StringException(line);
                            code = encoder;

                            log(NowTime(), Locale("convert", "literal"));
                            for (int i = 0; i < ExceptList.Length; i++) {
                                string fiv = ExceptList[i];

                                if (fiv != null) {
                                    foreach (string vbsline in System.IO.File.ReadLines($"{baseDirectory}/{cfg("folder", "kev")}/exhand.kev")) {
                                        string[] changeValue = vbsline.Split(" > ", StringSplitOptions.RemoveEmptyEntries);

                                        if (fiv.Contains(changeValue[0])) ExceptList[i] = fiv.Replace(changeValue[0], changeValue[1]);

                                        fiv = ExceptList[i];
                                    }
                                }
                            }
                        } catch {
                            code = line;
                            log(NowTime(), Locale("convert", "error"), "fatal");
                            Writelog("", true);
                            return;
                        }

                    } else code = line;

                    ExceptNum = 0;

                    if (osBool) {
                        log(NowTime(), $"'os.kev' {Locale("convert", "rules")}");
                        foreach (string vbsline in System.IO.File.ReadLines($"{baseDirectory}/{cfg("folder", "kev")}/os.kev")) {
                            string[] changeValue = vbsline.Split(" > ", StringSplitOptions.RemoveEmptyEntries);
                            code = code.Replace(changeValue[0], changeValue[1]);
                        }
                    }

                    if (randomBool) {
                        log(NowTime(), $"'random.kev' {Locale("convert", "rules")}");
                        foreach (string vbsline in System.IO.File.ReadLines($"{baseDirectory}/{cfg("folder", "kev")}/random.kev")) {
                            string[] changeValue = vbsline.Split(" > ", StringSplitOptions.RemoveEmptyEntries);
                            code = code.Replace(changeValue[0], changeValue[1]);
                        }
                    }

                    log(NowTime(), $"'default.kev' {Locale("convert", "rules")}");
                    foreach (string vbsline in System.IO.File.ReadLines($"{baseDirectory}/{cfg("folder", "kev")}/default.kev")) {
                        string[] changeValue = vbsline.Split(" > ", StringSplitOptions.RemoveEmptyEntries);
                        code = code.Replace(changeValue[0], changeValue[1]);
                    }

                    if (converting) {
                        log(NowTime(), Locale("except", "recovery"));
                        for (int i = 0; i < ExceptList.Length; i++) {
                            code = code.Replace("{>" + i + "<}", "\"" + ExceptList[i] + "\"");
                            code = code.Replace("@", "");
                        }
                    }

                    converting = false;
                    fileindex += 1;

                    log(NowTime(), $"{Locale("convert", "result")} : {code.Replace("    ", "")}");

                    wline[wline.Length - 1] = code;
                    Array.Resize(ref wline, wline.Length + 1);

                }

                log($"\n{NowTime()}", Locale("build", "finish"), "success", true);

                log(NowTime(), Locale("file", "ready"));
                StreamWriter writer;
                try {
                    writer = File.CreateText($"{baseDirectory}/{cfg("folder", "export")}/{exportfile}");
                    foreach (string itemA in wline) writer.WriteLine(itemA);
                    writer.Close();
                    log(NowTime(), Locale("file", "finish"), "success", true);
                } catch {
                    log(NowTime(), Locale("file", "error"), "fatal");
                    Writelog("", true);
                    return;
                }
                try {
                    log(NowTime(), Locale("debug", "start"), "default", true);
                    Runner();
                } catch {
                    log($"\n{NowTime()}", Locale("debug", "error"), "fatal");
                    Writelog("", true);
                    return;
                }
                return;
            }


            /* StringException
             * 한글 문자열 예외처리를 분석하고 처리합니다. */
            string StringException(string sourceString) {
                string[] ExceptReturn = new string[16];
                string ExceptValue = " ", AfterString = " ", ifRemaining = sourceString;

                ExceptReturn = sourceString.Split("@\"", StringSplitOptions.None);
                Array.Resize(ref ExceptReturn, ExceptReturn.Length + 1);

                AfterString = ExceptReturn[1];
                ExceptReturn = AfterString.Split("\"", StringSplitOptions.None);
                ExceptValue = ExceptReturn[0];

                Array.Resize(ref ExceptList, ExceptList.Length + 1);
                ExceptList[ExceptNum] = ExceptValue;

                ifRemaining = ifRemaining.Replace("@\"" + ExceptValue + "\"", "{>" + ExceptNum + "<}");

                ExceptNum += 1;

                if (ifRemaining.Contains("@\"")) return StringException(ifRemaining);
                else return ifRemaining;
            }


            /* Runner
             * 빌드된 파일을 설정 인터프리터 환경에 맞게 실행합니다. */
            void Runner() {
                Process module = new Process();

                if (cfg("interpreter", "custom") != "true") {
                    if (OS == "Unix") module.StartInfo.FileName = "python3";
                    else module.StartInfo.FileName = $"{baseDirectory}/Python/{(ARCH == Architecture.Arm64 ? "ARM" : "x86")}/python.exe";
                    module.StartInfo.Arguments = $"-d \"{baseDirectory}/{cfg("folder", "export")}/{exportfile}\"";
                } else {
                    module.StartInfo.FileName = cfg("interpreter", "path");
                    module.StartInfo.Arguments = $"{cfg("interpreter", "arguments")} \"{baseDirectory}/{cfg("folder", "export")}/{exportfile}\"";
                }

                /* 로거 종료 및 닫기 */
                Writelog("", true);

                /* 인터프리터 실행 */
                module.Start();
                module.WaitForExit();
                Console.ReadKey();
            }


            /* Locale
             * 값에 따라 언어 설정에 맞는 텍스트를 반환합니다. */
            string Locale(string key, string value) {
                return lang[key][value].ToString();
            }


            /* cfg
             * config의 설정 값을 반환합니다. */
            string cfg(string key, string value) {
                return ini[key][value].ToString();
            }


            /* CreateLine
             * 구분자 기호를 카운트 횟수만큼 출력합니다. */
            void CreateLine(int count) {
                for (int i = 1; i <= count; i++) Console.Write("_");
                Console.Write("\n\n");
            }


            /* setColor
             * 콘솔의 색상을 유형에 따라 변경합니다. */
            void setColor(string type) {
                switch (type) {
                    case "default":
                        Console.ResetColor(); break;
                    case "success":
                        Console.ForegroundColor = ConsoleColor.Green; break;
                    case "warning":
                        Console.ForegroundColor = ConsoleColor.Yellow; break;
                    case "fatal":
                        Console.ForegroundColor = ConsoleColor.Red; break;
                }
            }
            

            /* log
             * 빌드 정보나 결과를 한 줄로 출력합니다. */
            void log(string text, string details, string type = "default", bool createline = false) {
                Console.Write($"{text} > ");
                setColor(type);
                Console.Write($"{details}\n");
                Console.ResetColor();
                if (createline) CreateLine(50);

                Writelog($"{text} > {details}");
            }


            /* Writelog
             * 로그 파일을 작성합니다. */
            void Writelog(string log, bool close = false) {
                if (close) logger.Close();
                else logger.WriteLine(log);
            }


            /* NowTime
             * 현재 시간에 대한 정밀한 값을 반환합니다. */
            string NowTime() {
                DateTime now = DateTime.Now;
                return $"[{now.ToString("HH:mm:ss.fff")}]";
            }


        }   /* Main Function */

    }       /* Program Class */

}           /* DoKevEngine namespace */