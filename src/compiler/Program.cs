using System.Runtime.InteropServices;
using System.Diagnostics;

namespace DoKevEngine {

    class Program {
        static void Main(string[] args) {

            /* 컴파일러 화면 구성 */
            Console.Clear();

            /* baseDirectory에 빌드 툴의 절대 경로 선언 */
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            log_t("빌드 툴 절대 경로", baseDirectory);

            /* 현재 날짜와 시간을 now, time에 선언 */
            DateTime now = DateTime.Now;
            string time = now.ToString("tt hh:mm:ss");

            /* 시스템 아키텍쳐와 운영체제 정보 선언 */
            var ARCH = System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture;
            var OS = System.Environment.OSVersion.Platform.ToString();

            string[] wline = new string[1];
            string[] ExceptList = new string[0];
            int ExceptNum = 0, fileindex = 0;
            bool converting = false;
            bool randomBool = false, osBool = false;

            /* convert.dkv 유효성 확인을 위한 경로 선언 */
            string filePath = Path.Combine(baseDirectory, "convert.dkv");


            /* convert.dkv 유효성 확인 */
            if (File.Exists(filePath)) {
                log_t("빌드 대상 파일 확인", "빌드 대상 파일이 존재합니다.", "success", true);
                log("빌드를 시작하였습니다", $"{time}", "default", true);
                Converter();
            }
            else {
                log_t("빌드 대상 파일 확인", "빌드 대상 파일이 존재하지 않습니다.", "fatal");
                return;
            }


            /* Converter :: convert.dkv 파일을 Python 코드로 빌드합니다. */
            void Converter() {

                int counter = 0;
                string[] stringArray = new string[0];

                ExceptNum = 0;
                converting = false;
                Array.Clear(wline, 0, wline.Length);
                Array.Clear(ExceptList, 0, ExceptList.Length);

                foreach (string line in System.IO.File.ReadLines($"{baseDirectory}/convert.dkv")) {
                    Array.Resize(ref stringArray, stringArray.Length + 1);
                    stringArray[counter] = line;

                    if (line.Contains("필요한 라이브러리는") && line.Contains("random")) randomBool = true;
                    if (line.Contains("필요한 라이브러리는") && line.Contains("os"))     osBool = true;

                    counter++;
                }

                bool checking_bool = false;

                foreach (string line in stringArray) {
                    checking_bool = line.Contains("@\"");
                    string encoder = "", Ecdr = "";

                    Array.Clear(ExceptList, 0, ExceptList.Length);
                    converting = false;

                    if (checking_bool) {
                        Array.Clear(ExceptList, 0, ExceptList.Length);
                        ExceptNum = 0;
                        converting = true;
                        encoder = StringException(line);
                        Ecdr = encoder;

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
                    }
                    else Ecdr = line;

                    ExceptNum = 0;

                    if (osBool) {
                        foreach (string vbsline in System.IO.File.ReadLines($"{baseDirectory}/kev/os.kev")) {
                            string[] stringSeparators = new string[] { " > " };
                            string[] changeValue = vbsline.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                            Ecdr = Ecdr.Replace(changeValue[0], changeValue[1]);
                        }
                    }

                    if (randomBool) {
                        foreach (string vbsline in System.IO.File.ReadLines($"{baseDirectory}/kev/random.kev")) {
                            string[] stringSeparators = new string[] { " > " };
                            string[] changeValue = vbsline.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                            Ecdr = Ecdr.Replace(changeValue[0], changeValue[1]);
                        }
                    }

                    foreach (string vbsline in System.IO.File.ReadLines($"{baseDirectory}/kev/default.kev")) {
                        string[] stringSeparators = new string[] { " > " };
                        string[] changeValue = vbsline.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                        Ecdr = Ecdr.Replace(changeValue[0], changeValue[1]);
                    }

                    if (converting) {
                        for (int i = 0; i < ExceptList.Length; i++) {
                            Ecdr = Ecdr.Replace("{>" + i + "<}", "\"" + ExceptList[i] + "\"");
                            Ecdr = Ecdr.Replace("@", "");
                        }
                    }

                    converting = false;
                    fileindex += 1;

                    wline[wline.Length - 1] = Ecdr;
                    Array.Resize(ref wline, wline.Length + 1);

                }

                StreamWriter writer;
                writer = File.CreateText($"{baseDirectory}/export/convert.py");

                foreach (string itemA in wline) writer.WriteLine(itemA);

                writer.Close();
                Runner();
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

                if (OS == "Unix")   PythonPath = "python3";
                else                PythonPath = $"-d {baseDirectory}/Python/{(ARCH == Architecture.Arm64 ? "ARM" : "x86")}/python.exe";

                module.StartInfo.FileName   = PythonPath;
                module.StartInfo.Arguments  = $"-d {baseDirectory}/export/convert.py";
                module.Start();

                Console.ReadLine();
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
                Console.Write($"{text} :: ");
                setColor(type);
                Console.Write($"{details}\n");
                Console.ResetColor();
                if (createline) CreateLine(50);
            }


            /* log_t :: 빌드 정보나 결과를 탭하여 출력합니다. */
            void log_t(string text, string details, string type = "default", bool createline = false) {
                Console.WriteLine($"{text} :");
                setColor(type);
                Console.WriteLine($"\t{details}\n");
                Console.ResetColor();
                if (createline) CreateLine(50);
            }

        } /* Main Function */

    } /* Program Class */

} /* DoKevEngine namespace */