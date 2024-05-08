/* Import Library */
import kotlin.system.exitProcess


class Debuuger {
    /* Public Funtion */

    // Register : 디버그에 필요한 내용을 등록합니다.
    public fun Register(line: Int, code: String, content: Map): Unit {
        this.line = line
        this.code = code
        this.content = content
    }

    // StandardError : 표준 오류를 출력합니다.
    public fun StandardError(): Unit {
        exitProcess(127)
    }

    // SyntaxError : 문법 오류에 대한, 내용을 출력합니다.
    public fun SyntaxError(): Unit {
        exitProcess(1)
    }

    // RuntimeError : 코드 실행 중, 발생한 오류를 출력합니다.
    public fun RuntimeError(): Unit {
        exitProcess(1)
    }

    // SyntaxWarning : 문법 경고에 대한, 내용을 출력합니다.
    public fun SyntaxWarning(): Unit {

    }


    /* Private Funtion */

    // PrettyCode : 코드를 깔끔하게 변경하여, 반환합니다.
    private fun PrettyCode(code: String): String {
        return code
    }
}