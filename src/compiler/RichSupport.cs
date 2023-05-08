
namespace DoKevEngine {
    public class RichSupport {

        /* LoggerSet
         * 로거 등록 */
        bool logercreated = false;
        StreamWriter? logger;

        public void LoggerSet() {
            string logdirectory = AppDomain.CurrentDomain.BaseDirectory + "/log";
            DirectoryInfo di = new DirectoryInfo(logdirectory);
            logger = null;

            try {
                if (!di.Exists) di.Create();
                DateTime logtime = DateTime.Now;
                logger = File.CreateText($"{logdirectory}/{logtime.ToString("yyyy-MM-dd HH_mm_ss")}.log");
                logercreated = true;
            } catch (Exception e) {
                Log($"Logger Usage Warning", $"Failed to create log folder.\n", "warning");
                Log("\nFatal Error", e.Message, "fatal");
            }
        }


        /* setColor
         * 콘솔의 색상을 유형에 따라 변경합니다. */
        public void setColor(string type) {
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
        public void CreateLine(int count) {
            for (int i = 1; i <= count; i++) Console.Write("_");
            Console.Write("\n\n");
        }


        /* log
         * 빌드 정보나 결과를 한 줄로 출력합니다. */
        public void Log(string text, string details, string type = "default", bool createline = false) {
            Console.Write($"{text} > ");
            setColor(type);
            Console.Write($"{details}\n");
            Console.ResetColor();
            if (createline) CreateLine(50);
            Writelog($"{text} > {details}");
        }


        /* Writelog
         * 로그 파일을 작성합니다. */
        public void Writelog(string log, bool close = false){
            if (logercreated) {
                if (close) logger.Close();
                else logger.WriteLine(log);
            }
        }

        /* NowTime
         * 현재 시간에 대한 정밀한 값을 반환합니다. */
        public string NowTime() {
            DateTime now = DateTime.Now;
            return $"[{now:HH:mm:ss.fff}]";
        }


        /* SyntaxError
         * 문법적 오류에 대한 메세지를 표시합니다. */
        public void SyntaxError(string code, string type) {

        }

    }   /* RichSupport Class */

}       /* DoKevEngine namespace */
