
namespace DoKevEngine
{
    public class Syntax {

        /* ERROR
         * 문법 오류를 표시합니다. */
        public void ERROR(int line, string code, string type) {
            switch (type) {
                case "LIB-SPACE":
                    ERRMSG(line, code, "모듈 이름과 가져오기 문을 붙여서 작성하지 마세요.");
                break;
            }
        }


        /* ERRMSG 
         * 문법 오류 메세지를 출력합니다. */
        void ERRMSG(int line, string code, string message) {
            setColor("fatal");
            Console.WriteLine($"\n[{line}] ->\t{code}");
            CreateLine(50);
            Console.WriteLine("[구문 오류]");
            Console.WriteLine(message);
            setColor("default");
            Console.WriteLine();
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

    }   /* Syntax Class */
}       /* DoKevEngine namespace */
