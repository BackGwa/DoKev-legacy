using System.Runtime.InteropServices;
using System.Diagnostics;


namespace DoKevEngine {

    partial class Program {
        static void Main() {

            /* 빌더 화면 구성 */
            Console.Clear();
            Console.Title = "DoKev Runner";

            /* 클래스 연결 */
            RichSupport rich = new RichSupport();
            Config cf = new Config();

            cf.ConfigSet();
            rich.LoggerSet();

            rich.Log(cf.Text("info", "builder"), (cf.Cfg("builder", "version")));

            var locale = cf.Text;
            var cfg = cf.Cfg;
            var log = rich.Log;
            var Writelog = rich.Writelog;
            var NowTime = rich.NowTime;


            /* 시스템 아키텍쳐와 운영체제 정보 선언 */
            var ARCH = RuntimeInformation.ProcessArchitecture;
            var OS = Environment.OSVersion.Platform.ToString();

            log(locale("info", "system"), $"{OS} ({ARCH})");
            log(locale("info", "lang"), cfg("builder", "language"));


            /* baseDirectory에 빌드 툴의 절대 경로 선언 */
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            log($"\n{locale("info", "path")}", baseDirectory);


            /* 빌드 파일 폴더 유무 확인 */
            string exportfolder = AppDomain.CurrentDomain.BaseDirectory + $"{cfg("folder", "export")}";
            DirectoryInfo ef = new DirectoryInfo(exportfolder);
            if (!ef.Exists) {
                log($"\n{locale("createfolder", "title")}", locale("createfolder", "invalid"), "warning");
                try {
                    ef.Create();
                    log($"{locale("createfolder", "title")}", $"{locale("createfolder", "create")}\n", "success");
                } catch {
                    log($"{locale("createfolder", "title")}", $"{locale("createfolder", "error")}\n", "fatal");
                }
            }


            /* filename 설정 및 선언 */
            string targetfile = cfg("filename", "target");
            string exportfile = cfg("filename", "export");


            /* 변환에 필요한 변수 선언 */
            bool Enable_random = false, Enable_os = false;
            string[] WriteLine = new string[1];
            string filePath;


            /* targetfile 유효성 확인을 위한 경로 선언 */
            try {
                filePath = Path.Combine(baseDirectory, targetfile);
            } catch {
                log(locale("target", "error"), "fatal");
                Writelog("", true);
                return;
            }


            /* targetfile 유효성 확인 */
            if (File.Exists(filePath)) {
                log(locale("target", "isvalid"), $"{targetfile} {locale("target", "valid")}", "success", true);
                log(NowTime(), $"{locale("build", "start")}\n");
                Converter();
            } else {
                log(locale("target", "isvalid"), $"{targetfile} {locale("target", "invalid")}", "fatal");
                Writelog("", true);
                return;
            }


            /* Converter
             * 설정 된 dkv 파일을 Python 코드로 빌드합니다. */
            void Converter() {
                log(NowTime(), locale("build", "initializing"));

                /* 파싱에 필요한 변수 선언 */
                Parser parser = new Parser();

                log(NowTime(), $"{locale("build", "initialized")}\n", "success");

                /* 코드를 파싱하여 변환 */
                foreach (string code in File.ReadLines($"{baseDirectory}/{targetfile}")) {

                    /* 코드가 공백이면, 파서 실행 패스 */
                    if (code.Replace(" ", "") == "") continue;

                    /* 라이브러리 확인 */
                    if (parser.LIBCHK(code)) {
                        switch (parser.NAME_FIX(parser.LIBPARSER(code))) {
                            case "random":
                                Enable_random = true;   break;
                            case "os":
                                Enable_os = true;       break;
                            case "":
                                Writelog("", true);     return;
                            default:
                                rich.SyntaxWarning(code, "unknown-lib");
                                break;
                        }
                    }


                    /* 파서 실행 */
                    string Result = code;

                    /* 변환 대기열 */
                    log($"\n{NowTime()}", $"{locale("convert", "wait")} : {Result.Replace("    ", "")}");

                    /* 내장 함수 파싱 작업 */
                    log(NowTime(), locale("convert", "rules"));

                    if (Enable_os)      Result = parser.OS_PARSER(Result);
                    if (Enable_random)  Result = parser.RANDOM_PARSER(Result);
                                        Result = parser.PARSER(Result);

                    if (Result == "")   return;

                    log(NowTime(), $"{locale("convert", "result")} : {Result.Replace("    ", "")}");

                    /* 파싱 된 코드 작성 */
                    WriteLine[WriteLine.Length - 1] = Result;
                    Array.Resize(ref WriteLine, WriteLine.Length + 1);

                }

                log($"\n{NowTime()}", locale("build", "finish"), "success", true);

                /* 빌드 파일 작성 */
                log(NowTime(), locale("file", "ready"));
                StreamWriter writer;

                try {
                    writer = File.CreateText($"{baseDirectory}/{cfg("folder", "export")}/{exportfile}");
                    foreach (string Code in WriteLine) writer.WriteLine(Code);
                    writer.Close();
                    log(NowTime(), locale("file", "finish"), "success", true);
                } catch {
                    log(NowTime(), locale("file", "error"), "fatal");
                    Writelog("", true);
                    return;
                }

                /* 디버깅 시작 */
                try {
                    log(NowTime(), locale("debug", "start"), "default", true);
                    Runner();
                } catch {
                    log($"\n{NowTime()}", locale("debug", "error"), "fatal");
                    Writelog("", true);
                    return;
                }
            }


            /* Runner
             * 빌드된 파일을 설정 인터프리터 환경에 맞게 실행합니다. */
            void Runner() {
                Process module = new Process();

                if (cfg("interpreter", "custom") == "false") {
                    if (OS == "Unix") module.StartInfo.FileName = "python3";
                    else module.StartInfo.FileName = "python";
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

        }   /* Main Function */

    }       /* Program Class */

}           /* DoKevEngine namespace */