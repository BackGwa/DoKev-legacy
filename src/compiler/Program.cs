using System.Runtime.InteropServices;
using System.Diagnostics;


namespace DoKevEngine {

    class Program {
        static void Main() {

            /* 컴파일러 화면 구성 */
            Console.Clear();
            Console.Title = "DoKev Runner";


            /* 로거 선언 */
            string logdirectory = AppDomain.CurrentDomain.BaseDirectory + "/log";
            bool logercreated = false;

            DirectoryInfo di = new DirectoryInfo(logdirectory);
            StreamWriter? logger;
            logger = null;

            try {
                if (!di.Exists) di.Create();
                DateTime logtime = DateTime.Now;
                logger = File.CreateText($"{logdirectory}/{logtime.ToString("yyyy-MM-dd HH_mm_ss")}.log");

                logercreated = true;
            } catch (Exception e) {
                log($"Logger Usage Warning", $"Failed to create log folder.\n", "warning");
                log("\nFatal Error", e.Message, "fatal");
            }


            /* ini 유효성 확인 및 선언 */
            IniFile ini = new IniFile();
            IniFile lang = new IniFile();
            try {
                ini.Load($"{AppDomain.CurrentDomain.BaseDirectory}/config.ini");
                lang.Load($"{AppDomain.CurrentDomain.BaseDirectory}/{cfg("folder", "language")}/{cfg("builder", "language")}.ini");

                log(Locale("info", "builder"), cfg("builder", "version"));
            } catch (Exception e) {
                log("Fatal Error", "An error occurred during initialization.", "fatal");
                log("\nFatal Error", e.Message, "fatal");
                Console.ReadKey(); return;
            }


            /* 시스템 아키텍쳐와 운영체제 정보 선언 */
            var ARCH = RuntimeInformation.ProcessArchitecture;
            var OS = Environment.OSVersion.Platform.ToString();

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

            /* 변환에 필요한 변수 선언 */
            bool randomBool = false, osBool = false;
            string[] WriteLine = new string[1];
            string[] ExceptList = new string[0];
            int fileindex = 0;
            string filePath;


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

            int ExceptNum;
            /* Converter
             * 설정 된 dkv 파일을 Python 코드로 빌드합니다. */
            void Converter() {
                log(NowTime(), Locale("build", "initializing"));

                /* 파싱에 필요한 변수 선언 */
                Parser parser = new Parser();

                int line_counter = 1;
                string LIBNAME = "";

                log(NowTime(), $"{Locale("build", "initialized")}\n", "success");

                /* 코드를 파싱하여 변환 */
                foreach (string code in File.ReadLines($"{baseDirectory}/{targetfile}")) {

                    /* 코드가 공백이면, 파서 실행 패스 */
                    if (code.Replace(" ", "") == "") {
                        line_counter++; continue;
                    }

                    /* 라이브러리 체크 */
                    if (parser.LIBCHK(code)) {
                        LIBNAME = parser.LIBPARSER(code);
                        if (parser.LIBKR(LIBNAME) == "random") {
                            randomBool = true;
                        }
                        if (parser.LIBKR(LIBNAME) == "os") {
                            osBool = true;
                        }
                    }

                    /* 파서 실행 */
                    string Result = code;

                    /* 예외 처리 필요 유무 확인 */
                    bool isExcept = Result.Contains("$\"");

                    /* 예외 처리 초기화 */
                    Array.Clear(ExceptList, 0, ExceptList.Length);

                    /* 변환 과정에서 필요한 변수 선언 */
                    bool Excepting = false;

                    /* 변환 대기열 */
                    log($"\n{NowTime()}", $"{Locale("convert", "wait")} : {Result.Replace("    ", "")}");

                    /* 예외 처리 */
                    if (isExcept) {
                        log(NowTime(), Locale("except", "ishas"));

                        ExceptNum = 0;
                        Excepting = true;

                        try {
                            Result = StringException(Result);

                            /* 문자열 리터럴 처리 */
                            log(NowTime(), Locale("convert", "literal"));
                            for (int i = 0; i < ExceptList.Length; i++)
                            {
                                if (ExceptList[i] != null) ExceptList[i] = parser.LITERAL_PARSER(ExceptList[i]);
                            }
                        } catch {
                            /* 예외 처리 변환 오류 시 */
                            log(NowTime(), Locale("convert", "error"), "fatal");
                            Writelog("", true);
                            return;
                        }
                    }

                    /* RANDOM 모듈 사용 시 파싱 작업 */
                    if (randomBool) {
                        log(NowTime(), $"'RANDOM' {Locale("convert", "rules")}");
                        Result = parser.RANDOM_PARSER(Result);
                    }

                    /* OS 모듈 사용 시 파싱 작업 */
                    if (osBool) {
                        log(NowTime(), $"'OS' {Locale("convert", "rules")}");
                        Result = parser.OS_PARSER(Result);
                    }

                    /* 내장 함수 파싱 작업 */
                    log(NowTime(), $"'DEFAULT' {Locale("convert", "rules")}");
                    Result = parser.PARSER(Result);

                    /* 예외처리 복구 */
                    if (Excepting) {
                        log(NowTime(), Locale("except", "recovery"));
                        for (int i = 0; i < ExceptList.Length; i++) {
                            Result = Result.Replace("{>" + i + "<}", "\"" + ExceptList[i] + "\"");
                            Result = Result.Replace("$", "");
                        }
                    }

                    /* 예외처리 종료 */
                    Excepting = false;
                    fileindex += 1;

                    log(NowTime(), $"{Locale("convert", "result")} : {Result.Replace("    ", "")}");

                    /* 파싱 된 코드 작성 */
                    WriteLine[WriteLine.Length - 1] = Result;
                    Array.Resize(ref WriteLine, WriteLine.Length + 1);

                    /* 라인 추가 */
                    line_counter++;
                }

                log($"\n{NowTime()}", Locale("build", "finish"), "success", true);

                /* 빌드 파일 작성 */
                log(NowTime(), Locale("file", "ready"));
                StreamWriter writer;

                try {
                    writer = File.CreateText($"{baseDirectory}/{cfg("folder", "export")}/{exportfile}");
                    foreach (string Code in WriteLine) writer.WriteLine(Code);
                    writer.Close();
                    log(NowTime(), Locale("file", "finish"), "success", true);
                } catch {
                    log(NowTime(), Locale("file", "error"), "fatal");
                    Writelog("", true);
                    return;
                }

                /* 디버깅 시작 */
                try {
                    log(NowTime(), Locale("debug", "start"), "default", true);
                    Runner();
                } catch {
                    log($"\n{NowTime()}", Locale("debug", "error"), "fatal");
                    Writelog("", true);
                    return;
                }
            }


            /* StringException
             * 한글 문자열 예외처리를 분석하고 처리합니다. */
            string StringException(string sourceString) {
                string[] ExceptReturn = new string[16];
                string ExceptValue = " ", ifRemaining = sourceString;

                ExceptReturn = sourceString.Split("$\"", StringSplitOptions.None);
                Array.Resize(ref ExceptReturn, ExceptReturn.Length + 1);

                ExceptReturn = ExceptReturn[1].Split("\"", StringSplitOptions.None);
                ExceptValue = ExceptReturn[0];

                Array.Resize(ref ExceptList, ExceptList.Length + 1);
                ExceptList[ExceptNum] = ExceptValue;

                ifRemaining = ifRemaining.Replace("$\"" + ExceptValue + "\"", "{>" + ExceptNum + "<}");

                ExceptNum += 1;

                if (ifRemaining.Contains("$\"")) return StringException(ifRemaining);
                else return ifRemaining;
            }


            /* Runner
             * 빌드된 파일을 설정 인터프리터 환경에 맞게 실행합니다. */
            void Runner() {
                Process module = new Process();

                if (cfg("interpreter", "custom") != "true" && 
                    cfg("interpreter", "custom") == "false") {
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
                if (logercreated) {
                    if (close) logger.Close();
                    else logger.WriteLine(log);
                }
            }


            /* NowTime
             * 현재 시간에 대한 정밀한 값을 반환합니다. */
            string NowTime() {
                DateTime now = DateTime.Now;
                return $"[{now:HH:mm:ss.fff}]";
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


            /* CreateLine
             * 구분자 기호를 카운트 횟수만큼 출력합니다. */
            void CreateLine(int count) {
                for (int i = 1; i <= count; i++) Console.Write("_");
                Console.Write("\n\n");
            }

        }   /* Main Function */

    }       /* Program Class */

}           /* DoKevEngine namespace */