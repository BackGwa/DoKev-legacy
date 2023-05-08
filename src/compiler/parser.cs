using System.Text.RegularExpressions;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace DoKevEngine {
    public class Parser {

        /* 클래스 연결 */
        RichSupport rich = new RichSupport();

        /* 라이브러리 선언인지 확인 */
        public bool LIBCHK(string code) {
            if ((code.Contains("라이브러리")) || (code.Contains("모듈")) &&
                (code.Contains("필요해")) || (code.Contains("사용할래"))) {
                return true;
            } return false;
        }


        /* 라이브러리 선언 오류 확인 */
        bool LIB_ERRCHK(string code) {
            if ((code.Contains("라이브러리")) || (code.Contains("모듈"))) return true;
            return false;
        }


        /* 라이브러리 선언 구분 */
        public string LIBPARSER(string code) {
            string[] SPLIT = code.Split(" ");

            if (LIB_ERRCHK(SPLIT[0])) {
                rich.SyntaxError(code, "lib-space");
                return "";
            } else {
                return SPLIT[0];
            }
        }


        /* 라이브러리 명칭 영어로 변경 */
        public string LIBKR(string libname) {
            if (libname == "난수" || libname == "랜덤") return "random";
            if (libname == "운영체계") return "os";
            return libname;
        }


        /* 라이브러리 선언 파싱 */
        string IMPORT(string code) {
            if (LIBCHK(code)) return $"import {LIBKR(code.Split(" ")[0])}";
            return code;
        }


        /* 출력문 파싱 */
        string PRINT(string code) {
            code = Regex.Replace(code, "(말해줘|보여줘|출력해줘|출력해)", "print");
            code = Regex.Replace(code, "(맺음값|종단값)", "end");
            return code;
        }


        /* 입력문 파싱 */
        string INPUT(string code) {
            code = Regex.Replace(code, "(입력받아줘|입력받아)", "input");
            return code;
        }


        /* 형식문자 파싱 */
        string FORMAT(string code) {
            code = Regex.Replace(code, "(형식문자|포맷문자|형식|포맷)", "format");
            return code;
        }


        /* 대소문자 변경 파싱 */
        string UPPER_LOWER(string code) {
            code = Regex.Replace(code, "(대문자로|대문자)", "upper");
            code = Regex.Replace(code, "(소문자로|소문자)", "lower");
            return code;
        }


        /* WHILE 반복문 파싱 */
        string WHILE(string code) {
            code = Regex.Replace(code, "(반복해줘|반복해)", "while");
            return code;
        }

        /* FOR 반복문 파싱 */
        string FOR(string code) {
            code = Regex.Replace(code, "(증감반복해줘|증감반복해)", "for");
            code = Regex.Replace(code, "(이걸|저걸)", "in");
            code = Regex.Replace(code, "(이걸로|저걸로|으로)", ":");
            return code;
        }

        /* BREAK 반복문 파싱 */
        string BREAK(string code) {
            code = Regex.Replace(code, "(빠져나오자|빠져나와줘|빠져|나와|나가)", "break");
            return code;
        }


        /* 조건문 파싱 */
        string IFTHEN(string code) {
            code = Regex.Replace(code, "(혹시나|혹여나|혹시|만약에|만약)", "if");
            code = Regex.Replace(code, "(그게 아니면|그게 아니라면)", "elif");
            code = Regex.Replace(code, "(모두 아니면|모두 아니라면|다 아니면|다 아니라면)", "else:");
            code = Regex.Replace(code, "(이라면|라면|이면|면)", ":");
            return code;
        }


        /* 삼항연산자 파싱 */
        string TRINOMIAL(string code) {
            code = Regex.Replace(code, "(이려면)", "if");
            code = Regex.Replace(code, "(저게 아니면)", "else");
            return code;
        }


        /* 함수 선언 파싱 */
        string FUNCTION(string code) {
            code = Regex.Replace(code, "(약속하자|약속해|약속|선언하자|선언해|선언)", "def");
            code = Regex.Replace(code, "(함수는)", "");
            code = Regex.Replace(code, "(가 필요해|이 필요해)", ":");
            code = Regex.Replace(code, "(반환해줘|반환해|돌려줘|던져줘)", "return");
            return code;
        }


        /* RANGE 파싱 */
        string RANGE(string code) {
            code = Regex.Replace(code, "(범위값|범위)", "range");
            return code;
        }


        /* JOIN 파싱 */
        string JOIN(string code) {
            code = Regex.Replace(code, "(넣기|삽입)", "join");
            return code;
        }


        /* ROUND 파싱 */
        string ROUND(string code) {
            code = Regex.Replace(code, "(반올림)", "round");
            return code;
        }


        /* 파일 관리 파싱 */
        string FILE(string code) {
            code = Regex.Replace(code, "(파일열기)", "open");
            code = Regex.Replace(code, "(파일닫기)", "close");
            code = Regex.Replace(code, "(해석|인코딩 방식|인코딩)", "encoding");
            code = Regex.Replace(code, "(읽기확장)", "r+");
            code = Regex.Replace(code, "(쓰기확장)", "w+");
            code = Regex.Replace(code, "(읽기)", "r");
            code = Regex.Replace(code, "(쓰기)", "w");
            code = Regex.Replace(code, "(줄읽기|라인읽기)", "readlines");
            code = Regex.Replace(code, "(줄쓰기|라인쓰기)", "writelines");
            return code;
        }


        /* LOGIC 파싱 */
        string LOGIC(string code) {
            code = Regex.Replace(code, "(참|진짜|진실)", "True");
            code = Regex.Replace(code, "(거짓|가짜|허위)", "False");
            code = Regex.Replace(code, "(그리고)", "and");
            code = Regex.Replace(code, "(또는|또한)", "or");
            code = Regex.Replace(code, "(반대|거꾸로)", "not");
            return code;
        }

        /* VARIABLE 파싱 */
        string VARIABLE(string code) {
            code = Regex.Replace(code, "(은|는)", " =");
            code = Regex.Replace(code, "(이야|야)", "");
            return code;
        }


        /* CAST 파싱 */
        string CAST(string code) {
            code = Regex.Replace(code, "(정수형으로)", "int");
            code = Regex.Replace(code, "(실수형으로)", "float");
            code = Regex.Replace(code, "(문자열로)", "str");
            code = Regex.Replace(code, "(문자로)", "chr");
            code = Regex.Replace(code, "(불로)", "bool");
            return code;
        }


        /* DATA_TYPE 파싱 */
        string DATA_TYPE(string code) {
            code = Regex.Replace(code, "(정수)", "int");
            code = Regex.Replace(code, "(실수)", "float");
            code = Regex.Replace(code, "(문자열)", "str");
            code = Regex.Replace(code, "(문자)", "chr");
            code = Regex.Replace(code, "(불)", "bool");
            code = Regex.Replace(code, "(빈공간|널|논)", "None");
            return code;
        }


        /* CALC 파싱 */
        string CALC(string code) {
            code = Regex.Replace(code, "(더하기)", "+");
            code = Regex.Replace(code, "(빼기)", "-");
            code = Regex.Replace(code, "(곱하기)", "*");
            code = Regex.Replace(code, "(나누기)", "/");
            code = Regex.Replace(code, "(나머지)", "%");
            code = Regex.Replace(code, "(거듭 제곱|제곱)", "**");
            code = Regex.Replace(code, "(절댓값)", "abs");
            code = Regex.Replace(code, "(문자수식|수식)", "eval");
            return code;
        }


        /* 증감식 파싱 */
        string IDAF(string code) {
            code = Regex.Replace(code, "(증가해줘|증가)", "+= 1");
            code = Regex.Replace(code, "(감소해줘|감소)", "-= 1");
            return code;
        }


        /* 구분자 및 도움말 파싱 */
        string HELPTEXT(string code) {
            code = Regex.Replace(code, "(이랑|랑|과|와)", ", ");
            code = Regex.Replace(code, "(의)", ".");
            code = Regex.Replace(code, "(을|를|진짜로|아니)", "");
            return code;
        }


        /* 진법 파싱 */
        string NUMBER_SYSTEM(string code) {
            code = Regex.Replace(code, "(16진수|헥스)", "hex");
            code = Regex.Replace(code, "(8진수|악틀)", "oct");
            return code;
        }


        /* 아이템 관련 파싱 */
        string ITEMCALC(string code) {
            code = Regex.Replace(code, "(모으기|합치기|붙이기)", "append");
            code = Regex.Replace(code, "(문자길이|길이)", "len");
            return code;
        }


        /* 파서 */
        public string PARSER(string code = "") {
            code = IMPORT(code);
            code = UPPER_LOWER(code);
            code = PRINT(code);
            code = INPUT(code);
            code = FORMAT(code);
            code = CAST(code);
            code = FUNCTION(code);
            code = DATA_TYPE(code);
            code = VARIABLE(code);
            code = FOR(code);
            code = WHILE(code);
            code = BREAK(code);
            code = IFTHEN(code);
            code = LOGIC(code);
            code = TRINOMIAL(code);
            code = ITEMCALC(code);
            code = RANGE(code);
            code = JOIN(code);
            code = ROUND(code);
            code = FILE(code);
            code = CALC(code);
            code = IDAF(code);
            code = HELPTEXT(code);
            code = NUMBER_SYSTEM(code);
            code = LITERAL_PARSER(code);

            return code;
        }


        /* RANDOM 파서 */
        public string RANDOM_PARSER(string code) {
            code = Regex.Replace(code, "(랜덤정수|난수정수)", "randint");
            code = Regex.Replace(code, "(랜덤실수|난수실수)", "uniform");
            code = Regex.Replace(code, "(랜덤범위정수|난수범위정수|범위정수)", "randrange");
            code = Regex.Replace(code, "(여럿값선택)", "sample");
            code = Regex.Replace(code, "(선택)", "choice");
            code = Regex.Replace(code, "(랜덤|난수)", "random");
            return code;
        }


        /* OS 파서 */
        public string OS_PARSER(string code) {
            code = Regex.Replace(code, "(운영체계)", "os");
            code = Regex.Replace(code, "(현재경로)", "getcwd");
            code = Regex.Replace(code, "(폴더변경)", "chdir");
            code = Regex.Replace(code, "(파일목록)", "listdir");
            code = Regex.Replace(code, "(파일탐색)", "walk");
            code = Regex.Replace(code, "(명령)", "system");
            code = Regex.Replace(code, "(경로여부)", "path.exists");
            code = Regex.Replace(code, "(파일여부)", "path.isfile");
            code = Regex.Replace(code, "(경로생성)", "mkdir");
            code = Regex.Replace(code, "(경로변경)", "chdir");
            code = Regex.Replace(code, "(절대경로)", "path.abspath");
            code = Regex.Replace(code, "(경로명)", "path.dirname");
            code = Regex.Replace(code, "(파일명)", "path.basename");
            code = Regex.Replace(code, "(파일크기)", "path.getsize");
            code = Regex.Replace(code, "(파일삭제)", "remove");
            code = Regex.Replace(code, "(경로삭제)", "rmdir");
            code = Regex.Replace(code, "(이름변경)", "rename");
            code = Regex.Replace(code, "(상태)", "stat");

            return code;
        }

        /* 문자열 리터널 파서 */
        public string LITERAL_PARSER(string code) {
            code = code.Replace(@"\ㅈ", @"\n");
            code = code.Replace(@"\줄바꿈", @"\n");
            code = code.Replace(@"\줄", @"\n");

            code = code.Replace(@"\ㄷ", @"\b");
            code = code.Replace(@"\뒤로", @"\b");
            code = code.Replace(@"\뒤", @"\b");

            code = code.Replace(@"\ㅂ", @"\000");
            code = code.Replace(@"\빈공간", @"\000");
            code = code.Replace(@"\ㄴ", @"\000");
            code = code.Replace(@"\널", @"\000");

            code = code.Replace(@"\ㅌ", @"\t");
            code = code.Replace(@"\탭", @"\t");

            code = code.Replace(@"\ㅇ", @"\f");
            code = code.Replace(@"\앞", @"\f");
            code = code.Replace(@"\앞으로", @"\f");

            code = code.Replace(@"\ㄷ", @"\r");
            code = code.Replace(@"\다음", @"\r");
            code = code.Replace(@"\다음으로", @"\r");

            code = code.Replace(@"\ㅂ", @"\a");
            code = code.Replace(@"\벨", @"\a");
            code = code.Replace(@"\부저", @"\a");

            code = code.Replace(@"\ㅅ", @"\v");
            code = code.Replace(@"\수직탭", @"\v");

            return code;
        }

    }   /* Parser Class */

}       /* DoKevEngine namespace */