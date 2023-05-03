using System.Runtime.InteropServices;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace DoKevEngine {

    class Program {
        static void Main(string[] args) {

            /* 컴파일러 화면 구성 */
            Console.Clear();

            /* baseDirectory에 빌드 툴의 절대 경로 선언 */
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            log_t("빌드 툴 절대 경로", baseDirectory);

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
            log("빌드 대상 파일 확인", "");

            if (File.Exists(filePath)) {
                write_t("빌드 대상 파일이 존재합니다.", "success", true);
                log($"[{NowTime()}]", "빌드를 시작하였습니다\n");
                Converter();
            }
            else {
                write_t("빌드 대상 파일이 존재하지 않습니다.", "fatal");
                return;
            }


            /* Converter :: convert.dkv 파일을 Python 코드로 빌드합니다. */
            void Converter() {

                log($"[{NowTime()}]", "빌드 작업을 위해 초기화하고 있습니다.");

                int counter = 0;
                string[] stringArray = new string[0];
                bool checking_bool = false;

                ExceptNum = 0;
                converting = false;
                Array.Clear(wline, 0, wline.Length);
                Array.Clear(ExceptList, 0, ExceptList.Length);

                log($"[{NowTime()}]", "초기화 작업이 마무리 되었습니다.\n", "success");
                log($"[{NowTime()}]", "라이브러리 검사를 준비하고 있습니다.");

                foreach (string line in System.IO.File.ReadLines($"{baseDirectory}/convert.dkv")) {
                    log($"[{NowTime()}]", $"검사 중... [{counter}/{stringArray.Length}]");

                    Array.Resize(ref stringArray, stringArray.Length + 1);
                    stringArray[counter] = line;

                    if (line.Contains("필요한 라이브러리는") && line.Contains("random")) randomBool = true;
                    if (line.Contains("필요한 라이브러리는") && line.Contains("os"))     osBool = true;

                    counter++;
                }

                log($"[{NowTime()}]", "라이브러리 검사가 마무리 되었습니다.\n", "success");
                log($"[{NowTime()}]", "빌드 작업을 준비하고 있습니다.");

                foreach (string line in stringArray) {

                    checking_bool = line.Contains("@\"");
                    string encoder = "", Ecdr = "";

                    Array.Clear(ExceptList, 0, ExceptList.Length);
                    converting = false;

                    if (line != "") log($"\n[{NowTime()}]", $"변환 대기 -> {line.Replace("    ", "")}");

                    if (checking_bool) {
                        log($"[{NowTime()}]", "예외처리가 필요한 문자열을 발견하였습니다.");

                        Array.Clear(ExceptList, 0, ExceptList.Length);
                        ExceptNum = 0;
                        converting = true;

                        try {
                            encoder = StringException(line);
                            Ecdr = encoder;

                            log($"[{NowTime()}]", "문자열 리터널을 변환하고 있습니다.");
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
                            Ecdr = line;
                            log($"[{NowTime()}]", "작업을 진행하던 중 문제가 발생했습니다.", "fatal");
                        }

                    }
                    else Ecdr = line;

                    ExceptNum = 0;

                    if (osBool) {
                        if (line != "") log($"[{NowTime()}]", "'os.kev' 규칙에 따라 변환 작업 중");
                        foreach (string vbsline in System.IO.File.ReadLines($"{baseDirectory}/kev/os.kev")) {
                            string[] stringSeparators = new string[] { " > " };
                            string[] changeValue = vbsline.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                            Ecdr = Ecdr.Replace(changeValue[0], changeValue[1]);
                        }
                    }

                    if (randomBool) {
                        if (line != "") log($"[{NowTime()}]", "'random.kev' 규칙에 따라 변환 작업 중");
                        foreach (string vbsline in System.IO.File.ReadLines($"{baseDirectory}/kev/random.kev")) {
                            string[] stringSeparators = new string[] { " > " };
                            string[] changeValue = vbsline.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                            Ecdr = Ecdr.Replace(changeValue[0], changeValue[1]);
                        }
                    }

                    if (line != "") log($"[{NowTime()}]", "'default.kev' 규칙에 따라 변환 작업 중");
                    foreach (string vbsline in System.IO.File.ReadLines($"{baseDirectory}/kev/default.kev")) {
                        string[] stringSeparators = new string[] { " > " };
                        string[] changeValue = vbsline.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                        Ecdr = Ecdr.Replace(changeValue[0], changeValue[1]);
                    }

                    if (converting) {
                        log($"[{NowTime()}]", "문자열을 복원하고 있습니다.");
                        for (int i = 0; i < ExceptList.Length; i++) {
                            Ecdr = Ecdr.Replace("{>" + i + "<}", "\"" + ExceptList[i] + "\"");
                            Ecdr = Ecdr.Replace("@", "");
                        }
                    }

                    converting = false;
                    fileindex += 1;

                    if (Ecdr != "") log($"[{NowTime()}]", $"변환 결과 -> {Ecdr.Replace("    ", "")}");

                    wline[wline.Length - 1] = Ecdr;
                    Array.Resize(ref wline, wline.Length + 1);

                }

                log($"\n[{NowTime()}]", "빌드가 마무리되었습니다.", "success", true);

                log($"[{NowTime()}]", "빌드 파일을 작성을 준비하고 있습니다.");
                StreamWriter writer;
                try {
                    writer = File.CreateText($"{baseDirectory}/export/convert.py");
                    foreach (string itemA in wline) writer.WriteLine(itemA);
                    writer.Close();
                    log($"[{NowTime()}]", "빌드 파일이 성공적으로 작성되었습니다.", "success", true);

                    log($"[{NowTime()}]", "디버깅을 시작합니다.", "default", true);
                } catch {
                    log($"[{NowTime()}]", "빌드 파일을 작성하는데에 문제가 생겼습니다!", "fatal");
                }
                try {
                    Runner();
                } catch {
                    log($"\n[{NowTime()}]", "디버깅을 시작하는데 문제가 생겼습니다!", "fatal");
                }
                Console.ReadKey();
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
                Console.Write($"{text} : ");
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
                return now.ToString("HH:mm:ss.fff");
            }


        } /* Main Function */

    } /* Program Class */

} /* DoKevEngine namespace */