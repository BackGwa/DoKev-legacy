using System.Runtime.InteropServices;
using System.Diagnostics;

namespace DoKevEngine {

    class Program {
        static void Main(string[] args) {

            /* 컴파일러 화면 구성 */
            Console.Clear();
            Console.Title = "DoKev Runner";

            /* ini 유효성 확인 및 선언 */
            string version = "";
            string language = "";
            IniFile ini = new IniFile();
            IniFile lang = new IniFile();
            try {
                ini.Load($"{AppDomain.CurrentDomain.BaseDirectory}/config.ini");
                version = ini["builder"]["version"].ToString();
                language = ini["builder"]["language"].ToString();

                lang.Load($"{AppDomain.CurrentDomain.BaseDirectory}/lang/{language}.ini");
                log($"{Locale("builder-info")}\t\t", version);
            } catch {
                log("Fatal Error", "An error occurred during initialization.", "fatal");
                Console.ReadKey();
                return;
            }

            /* 시스템 아키텍쳐와 운영체제 정보 선언 */
            var ARCH = System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture;
            var OS = System.Environment.OSVersion.Platform.ToString();
            log($"{Locale("system-info")}\t", $"{OS} ({ARCH})");

            /* baseDirectory에 빌드 툴의 절대 경로 선언 */
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            log_t($"\n{Locale("buildtool-path")}", baseDirectory);

            /* 변환 과정에서 필요한 변수 선언 */
            bool converting = false;
            bool randomBool = false, osBool = false;
            string[] wline = new string[1];
            string[] ExceptList = new string[0];
            int ExceptNum = 0, fileindex = 0;
            string filePath = "";

            /* convert.dkv 유효성 확인을 위한 경로 선언 */
            try {
                filePath = Path.Combine(baseDirectory, "convert.dkv");
            } catch {
                write_t(Locale("file-invalid"), "fatal");
                Console.ReadKey();
                return;
            }

            /* convert.dkv 유효성 확인 */
            log(Locale("targetfile-check"), "");

            if (File.Exists(filePath)) {
                write_t($"{Locale("target-valid")}", "success", true);
                log(NowTime(), $"{Locale("start-build")}\n");
                Converter();
            } else {
                write_t(Locale("target-invalid"), "fatal");
                Console.ReadKey();
                return;
            }


            /* Converter :: convert.dkv 파일을 Python 코드로 빌드합니다. */
            void Converter() {

                log(NowTime(), Locale("build-init"));

                int counter = 0;
                string[] stringArray = new string[0];
                bool checking_bool = false;

                ExceptNum = 0;
                converting = false;
                Array.Clear(wline, 0, wline.Length);
                Array.Clear(ExceptList, 0, ExceptList.Length);

                log(NowTime(), $"{Locale("init-finish")}\n", "success");
                log(NowTime(), Locale("libchk-load"));

                foreach (string line in System.IO.File.ReadLines($"{baseDirectory}/convert.dkv")) {
                    log(NowTime(), $"{Locale("checking")} [{counter}/{stringArray.Length}]");

                    Array.Resize(ref stringArray, stringArray.Length + 1);
                    stringArray[counter] = line;

                    if (line.Contains("필요한 라이브러리는") && line.Contains("random")) randomBool = true;
                    if (line.Contains("필요한 라이브러리는") && line.Contains("os"))     osBool = true;

                    counter++;
                }

                log(NowTime(), $"{Locale("libchk-finish")}\n", "success");
                log(NowTime(), Locale("builder-load"));

                foreach (string line in stringArray) {

                    checking_bool = line.Contains("@\"");
                    string encoder = "", code = "";

                    Array.Clear(ExceptList, 0, ExceptList.Length);
                    converting = false;

                    if (line != "") log($"\n{NowTime()}", $"{Locale("convert-wait")} : {line.Replace("    ", "")}");

                    if (checking_bool) {
                        log(NowTime(), Locale("had-except"));

                        Array.Clear(ExceptList, 0, ExceptList.Length);
                        ExceptNum = 0;
                        converting = true;

                        try {
                            encoder = StringException(line);
                            code = encoder;

                            log(NowTime(), Locale("convert-literal"));
                            for (int i = 0; i < ExceptList.Length; i++) {
                                string fiv = ExceptList[i];

                                if (fiv != null) {
                                    foreach (string vbsline in System.IO.File.ReadLines($"{baseDirectory}/kev/exhand.kev")) {
                                        string[] stringSeparators = new string[] { " > " };
                                        string[] changeValue = vbsline.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);

                                        if (fiv.Contains(changeValue[0])) ExceptList[i] = fiv.Replace(changeValue[0], changeValue[1]);

                                        fiv = ExceptList[i];
                                    }
                                }
                            }
                        } catch {
                            code = line;
                            log(NowTime(), Locale("convert-error"), "fatal");
                        }

                    } else code = line;

                    ExceptNum = 0;

                    if (osBool) {
                        if (line != "") log(NowTime(), $"'os.kev' {Locale("convert-rule")}");
                        foreach (string vbsline in System.IO.File.ReadLines($"{baseDirectory}/kev/os.kev")) {
                            string[] stringSeparators = new string[] { " > " };
                            string[] changeValue = vbsline.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                            code = code.Replace(changeValue[0], changeValue[1]);
                        }
                    }

                    if (randomBool) {
                        if (line != "") log(NowTime(), $"'random.kev' {Locale("convert-rule")}");
                        foreach (string vbsline in System.IO.File.ReadLines($"{baseDirectory}/kev/random.kev")) {
                            string[] stringSeparators = new string[] { " > " };
                            string[] changeValue = vbsline.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                            code = code.Replace(changeValue[0], changeValue[1]);
                        }
                    }

                    if (line != "") log(NowTime(), $"'default.kev' {Locale("convert-rule")}");
                    foreach (string vbsline in System.IO.File.ReadLines($"{baseDirectory}/kev/default.kev")) {
                        string[] stringSeparators = new string[] { " > " };
                        string[] changeValue = vbsline.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                        code = code.Replace(changeValue[0], changeValue[1]);
                    }

                    if (converting) {
                        log(NowTime(), Locale("recovery-string"));
                        for (int i = 0; i < ExceptList.Length; i++) {
                            code = code.Replace("{>" + i + "<}", "\"" + ExceptList[i] + "\"");
                            code = code.Replace("@", "");
                        }
                    }

                    converting = false;
                    fileindex += 1;

                    if (code != "") log(NowTime(), $"{Locale("convert-result")} : {code.Replace("    ", "")}");

                    wline[wline.Length - 1] = code;
                    Array.Resize(ref wline, wline.Length + 1);

                }

                log($"\n{NowTime()}", Locale("build-finish"), "success", true);

                log(NowTime(), Locale("result-write-load"));
                StreamWriter writer;
                try {
                    writer = File.CreateText($"{baseDirectory}/export/convert.py");
                    foreach (string itemA in wline) writer.WriteLine(itemA);
                    writer.Close();
                    log(NowTime(), Locale("result-write-finish"), "success", true);

                    log(NowTime(), Locale("debugging-start"), "default", true);
                } catch {
                    log(NowTime(), Locale("result-write-error"), "fatal");
                }
                try {
                    Runner();
                } catch {
                    log($"\n{NowTime()}", Locale("debugging-error"), "fatal");
                }
                Console.ReadKey();
                return;
            }


            /* Locale :: 값에 따라 언어 설정에 맞는 텍스트를 반환합니다. */
            string Locale(string value){
                return lang["text"][value].ToString();
            }


            /* StringException :: 한글 문자열 예외처리를 분석하고 처리합니다. */
            string StringException(string sourceString) {

                string[] ExceptReturn = new string[16];
                string ExceptValue = " ", AfterString = " ", ifRemaining = sourceString;

                string[] stringSeparators = new string[] { "@\"" };
                ExceptReturn = sourceString.Split(stringSeparators, StringSplitOptions.None);
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


            /* Runner :: 빌드된 Python 파일을 환경에 맞게 실행합니다. */
            void Runner() {
                Process module = new Process();
                string PythonPath;

                if (OS == "Unix") PythonPath = "python3";
                else PythonPath = $"{baseDirectory}/Python/{(ARCH == Architecture.Arm64 ? "ARM" : "x86")}/python.exe";

                module.StartInfo.FileName = PythonPath;
                module.StartInfo.Arguments = $"-d {baseDirectory}/export/convert.py";
                module.Start();
            }


            /* CreateLine :: 구분자 기호를 카운트 횟수만큼 출력합니다. */
            void CreateLine(int count) {
                for (int i = 1; i <= count; i++) Console.Write("_");
                Console.Write("\n\n");
            }


            /* setColor :: 콘솔의 색상을 유형에 따라 변경합니다. */
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


            /* log :: 빌드 정보나 결과를 한 줄로 출력합니다. */
            void log(string text, string details, string type = "default", bool createline = false) {
                Console.Write($"{text} > ");
                setColor(type);
                Console.Write($"{details}\n");
                Console.ResetColor();
                if (createline) CreateLine(50);
            }


            /* log_t :: 빌드 정보나 결과를 탭하여 출력합니다. */
            void log_t(string text, string details, string type = "default", bool createline = false) {
                Console.WriteLine($"{text} >");
                setColor(type);
                Console.WriteLine($"\t{details}\n");
                Console.ResetColor();
                if (createline) CreateLine(50);
            }


            /* write_t :: 내용을 탭하여 출력합니다. */
            void write_t(string details, string type = "default", bool createline = false) {
                setColor(type);
                Console.WriteLine($"\t{details}\n");
                Console.ResetColor();
                if (createline) CreateLine(50);
            }


            /* NowTime :: 현재 시간에 대한 정밀한 값을 반환합니다. */
            string NowTime() {
                DateTime now = DateTime.Now;
                return $"[{now.ToString("HH:mm:ss.fffff")}]";
            }


        }   /* Main Function */

    }       /* Program Class */

}           /* DoKevEngine namespace */