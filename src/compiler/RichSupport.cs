
namespace DoKevEngine {
    public class RichSupport {

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
            for (int i = 1; i <= count; i++) Console.Write("-");
            Console.Write("\n");
        }


        /* log
         * 빌드 정보나 결과를 한 줄로 출력합니다. */
        public void Log(string text, string details, string type = "default", bool createline = false) {
            Console.Write($"{text} > ");
            setColor(type);
            Console.Write($"{details}\n");
            Console.ResetColor();
            if (createline) CreateLine(50);
        }


        /* NowTime
         * 현재 시간에 대한 정밀한 값을 반환합니다. */
        public string NowTime() {
            DateTime now = DateTime.Now;
            return $"[{now:HH:mm:ss.fff}]";
        }

        Config cf = new Config();

        /* SyntaxError
         * 문법적 오류에 대한 메세지를 표시합니다. */
        public void SyntaxError(string code, string type) {
            cf.ConfigSet();

            setColor("fatal");

            Console.WriteLine();
            CreateLine(75);
            Console.Write(cf.Text("error", "title"));
            Console.WriteLine(code.Replace("    ", ""));
            CreateLine(75);
            Console.WriteLine(cf.Text("error", $"{type}-message"));
            Console.WriteLine(cf.Text("error", $"fix"));
            Console.WriteLine($"\n{cf.Text("error", $"{type}-suggest")}");
            CreateLine(75);

            Console.ResetColor();
            Console.ReadKey();
        }

        public void SyntaxWarning(string code, string type) {
            cf.ConfigSet();

            setColor("warning");

            Console.WriteLine();
            CreateLine(75);
            Console.Write(cf.Text("warning", "title"));
            Console.WriteLine(code.Replace("    ", ""));
            CreateLine(75);
            Console.WriteLine(cf.Text("warning", type));
            CreateLine(75);

            Console.ResetColor();
            Console.WriteLine();
        }

    }   /* RichSupport Class */

}       /* DoKevEngine namespace */
