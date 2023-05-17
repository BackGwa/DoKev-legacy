using System.Text.RegularExpressions;

namespace DoKevEngine {
    public class Parser {

        /* 클래스 연결 */
        RichSupport rich = new RichSupport();

        /* 원본 코드 */
        string BeforeCode = "";

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
                rich.SyntaxError(BeforeCode, "lib-space");
                return "";
            } else return SPLIT[0];
        }


        /* 라이브러리 명칭 영어로 변경 */
        public string NAME_FIX(string libname) {
            switch (libname) {
                case "난수": return "random";
                case "랜덤": return "random";
                case "운영체계": return "os";
                default: return libname;
            }
        }


        /* 라이브러리 선언 파싱 */
        string IMPORT(string code) {
            if (LIBCHK(code)) return $"import {NAME_FIX(code.Split(" ")[0])}";
            return code;
        }


        /* 괄호 검사 */
        string BRACKET_S(string code) {

            /* 괄호가 존재하는지 검사 */
            if (!code.Contains("(") || !code.Contains(")")) {
                if (!code.Contains("(") && !code.Contains(")")) rich.SyntaxError(BeforeCode, "excluded-bracket");
                else if (!code.Contains("(")) rich.SyntaxError(BeforeCode, "excluded-left-bracket");
                else if (!code.Contains(")")) rich.SyntaxError(BeforeCode, "excluded-right-bracket");
                return "";
            }

            /* 괄호 갯수가 일치하는지 검사 */
            int L_bracket = code.Split(new string[] { "(" }, StringSplitOptions.None).Length - 1;
            int R_bracket = code.Split(new string[] { ")" }, StringSplitOptions.None).Length - 1;

            if (L_bracket - 1 < R_bracket - 1)      rich.SyntaxError(BeforeCode, "unmatched-left-bracket");
            else if (L_bracket - 1 > R_bracket - 1) rich.SyntaxError(BeforeCode, "unmatched-right-bracket");
            else return code;

            return "";
        }


        /* 문자열 검사 */
        string STRING_S(string code) {

            /* 문자열 포함 여부 검사 */
            if (!(code.Contains("\"") || code.Contains("'"))) return code;

            /* 문자열을 여닫았는지 확인 */
            string[] smallmark = code.Split(new string[] { "'" }, StringSplitOptions.None);
            string[] bigmark = code.Split(new string[] { "\"" }, StringSplitOptions.None);

            int smallmark_len = (smallmark.Length - 1 == 0) ? 2 : smallmark.Length - 1;
            int bigmark_len = (bigmark.Length - 1 == 0) ? 2 : bigmark.Length - 1;

            if (smallmark_len % 2 != 0 || bigmark_len % 2 != 0) {
                rich.SyntaxError(BeforeCode, "unmatched-mark");
                return "";
            } return code;
        }


        /* 출력문 파싱 */
        string PRINT(string code) {
            code = Regex.Replace(code, @"(['""])(?:\\\1|.)*?\1|말해줘|보여줘|출력해줘|출력해", 
                match => match.Value == "말해줘" || 
                         match.Value == "보여줘" ||
                         match.Value == "출력해줘" ||
                         match.Value == "출력해"
                         ? "print" : match.Value);

            if (code.Contains("print")) {
                code = OPTION_END(code);
                code = BRACKET_S(code);
            }
            return code;
        }


        /* 출력문 옵션 END 파싱 */
        string OPTION_END(string code) {
            code = Regex.Replace(code, @"(['""])(?:\\\1|.)*?\1|맺음값|종단값",
                match => match.Value == "맺음값" ||
                         match.Value == "종단값"
                         ? "end" : match.Value);
            return code;
        }


        /* 입력문 파싱 */
        string INPUT(string code) {
            code = Regex.Replace(code, @"(['""])(?:\\\1|.)*?\1|입력받아줘|입력받아",
                match => match.Value == "입력받아줘" ||
                         match.Value == "입력받아"
                         ? "input" : match.Value);

            if (code.Contains("input")) code = BRACKET_S(code);
            return code;
        }


        /* 형식문자 파싱 */
        string FORMAT(string code) {
            code = Regex.Replace(code, @"(['""])(?:\\\1|.)*?\1|형식문자|포맷문자|형식|포맷",
                match => match.Value == "형식문자" ||
                         match.Value == "포맷문자" ||
                         match.Value == "형식" ||
                         match.Value == "포맷"
                         ? "format" : match.Value);

            if (code.Contains("format")) code = BRACKET_S(code);
            return code;
        }


        /* 대소문자 변경 파싱 */
        string UPPER_LOWER(string code) {
            code = Regex.Replace(code, @"(['""])(?:\\\1|.)*?\1|대문자로|대문자",
                match => match.Value == "대문자로" ||
                         match.Value == "대문자"
                         ? "upper" : match.Value);

            code = Regex.Replace(code, @"(['""])(?:\\\1|.)*?\1|소문자로|소문자",
                match => match.Value == "소문자로" ||
                         match.Value == "소문자"
                         ? "lower" : match.Value);

            return code;
        }


        /* WHILE 반복문 파싱 */
        string WHILE(string code) {
            code = Regex.Replace(code, @"(['""])(?:\\\1|.)*?\1|반복해줘|반복해",
                match => match.Value == "반복해줘" ||
                         match.Value == "반복해"
                         ? "while" : match.Value);

            code = Regex.Replace(code, @"(['""])(?:\\\1|.)*?\1|인동안|인 동안",
                match => match.Value == "반복해줘" ||
                         match.Value == "반복해"
                         ? ":" : match.Value);

            return code;
        }


        /* FOR 반복문 파싱 */
        string FOR(string code) {
            code = Regex.Replace(code, @"(['""])(?:\\\1|.)*?\1|증감반복해줘|증감반복해|증감반복",
                match => match.Value == "증감반복해줘" ||
                         match.Value == "증감반복해" ||
                         match.Value == "증감반복"
                         ? "for" : match.Value);

            code = Regex.Replace(code, @"(['""])(?:\\\1|.)*?\1|이걸로|저걸로|으로",
                match => match.Value == "이걸로" ||
                         match.Value == "저걸로" ||
                         match.Value == "으로"
                         ? ":" : match.Value);

            code = Regex.Replace(code, @"(['""])(?:\\\1|.)*?\1|이걸|저걸",
                match => match.Value == "이걸" ||
                         match.Value == "저걸"
                         ? "in" : match.Value);

            return code;
        }


        /* BREAK 반복문 파싱 */
        string BREAK(string code) {
            code = Regex.Replace(code, @"(['""])(?:\\\1|.)*?\1|빠져나오자|빠져나와줘|빠져|나와|나가",
                match => match.Value == "빠져나오자" ||
                         match.Value == "빠져나와줘" ||
                         match.Value == "빠져" ||
                         match.Value == "나와" ||
                         match.Value == "나가"
                         ? "break" : match.Value);
            return code;
        }


        /* 조건문 파싱 */
        string IFTHEN(string code) {
            code = Regex.Replace(code, @"(['""])(?:\\\1|.)*?\1|혹시나|혹여나|혹시|만약에|만약",
                match => match.Value == "혹시나" ||
                         match.Value == "혹여나" ||
                         match.Value == "혹시" ||
                         match.Value == "만약에" ||
                         match.Value == "만약"
                         ? "if" : match.Value);

            code = Regex.Replace(code, @"(['""])(?:\\\1|.)*?\1|그게 아니고|그게 아니라면|그게 아니면",
                match => match.Value == "그게 아니고" ||
                         match.Value == "그게 아니라면" ||
                         match.Value == "그게 아니면"
                         ? "elif" : match.Value);

            code = Regex.Replace(code, @"(['""])(?:\\\1|.)*?\1|모두 아니라면|모두 아니면|다 아니라면|다 아니면",
                match => match.Value == "모두 아니라면" ||
                         match.Value == "모두 아니면" ||
                         match.Value == "다 아니라면" ||
                         match.Value == "다 아니면"
                         ? "else:" : match.Value);

            code = Regex.Replace(code, @"(['""])(?:\\\1|.)*?\1|이라면|라면|이면|면",
                match => match.Value == "이라면" ||
                         match.Value == "라면" ||
                         match.Value == "이면" ||
                         match.Value == "면"
                         ? ":" : match.Value);

            code = Regex.Replace(code, @"(['""])(?:\\\1|.)*?\1|아니 |진짜 |정말로 |정말 ",
                match => match.Value == "아니 " ||
                         match.Value == "진짜 " ||
                         match.Value == "정말로 " ||
                         match.Value == "정말 "
                         ? "" : match.Value);

            return code;
        }


        /* 삼항연산자 파싱 */
        string TRINOMIAL(string code) {
            code = Regex.Replace(code, @"(['""])(?:\\\1|.)*?\1|이려면|려면",
                match => match.Value == "이려면" ||
                         match.Value == "려면"
                         ? "if" : match.Value);

            code = Regex.Replace(code, @"(['""])(?:\\\1|.)*?\1|저게 아니면|저것이 아니면",
                match => match.Value == "저게 아니면" ||
                         match.Value == "저것이 아니면"
                         ? "else" : match.Value);

            return code;
        }


        /* 함수 선언 파싱 */
        string FUNCTION(string code) {
            code = Regex.Replace(code, @"(['""])(?:\\\1|.)*?\1|약속하자|약속해|약속|선언하자|선언해|선언",
                match => match.Value == "약속하자" ||
                         match.Value == "약속해" ||
                         match.Value == "약속" ||
                         match.Value == "선언하자" ||
                         match.Value == "선언해" ||
                         match.Value == "선언"
                         ? "def" : match.Value);

            code = Regex.Replace(code, @"(['""])(?:\\\1|.)*?\1|함수는",
                match => match.Value == "함수는"
                         ? "" : match.Value);

            code = Regex.Replace(code, @"(['""])(?:\\\1|.)*?\1|가 필요해|이 필요해",
                match => match.Value == "가 필요해" ||
                         match.Value == "이 필요해"
                         ? ":" : match.Value);

            code = Regex.Replace(code, @"(['""])(?:\\\1|.)*?\1|반환해줘|반환해|돌려줘|던져줘",
                match => match.Value == "반환해줘" ||
                         match.Value == "반환해" ||
                         match.Value == "돌려줘" ||
                         match.Value == "던져줘"
                         ? "return" : match.Value);

            return code;
        }


        /* RANGE 파싱 */
        string RANGE(string code) {
            code = Regex.Replace(code, @"(['""])(?:\\\1|.)*?\1|범위값|범위",
                match => match.Value == "범위값" ||
                         match.Value == "범위"
                         ? "range" : match.Value);

            if (code.Contains("range")) code = BRACKET_S(code);
            return code;
        }


        /* JOIN 파싱 */
        string JOIN(string code) {
            code = Regex.Replace(code, @"(['""])(?:\\\1|.)*?\1|넣기|삽입",
                match => match.Value == "넣기" ||
                         match.Value == "삽입"
                         ? "join" : match.Value);
            return code;
        }


        /* ROUND 파싱 */
        string ROUND(string code) {
            code = Regex.Replace(code, @"(['""])(?:\\\1|.)*?\1|반올림",
                match => match.Value == "반올림"
                         ? "round" : match.Value);

            if (code.Contains("round")) code = BRACKET_S(code);
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

            if (code.Contains("open") ||
                code.Contains("close") ||
                code.Contains("readlines") ||
                code.Contains("writelines")) code = BRACKET_S(code);

            return code;
        }


        /* LOGIC 파싱 */
        string LOGIC(string code) {
            code = Regex.Replace(code, "(참|진실)", "True");
            code = Regex.Replace(code, "(거짓|허위)", "False");
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
            if (code.Contains(" = ") && 
               (code.Contains("+= 1") || code.Contains("-= 1"))) {
                rich.SyntaxError(code, "idaf-assignment");
                return "";
            }
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
            if (code.Contains("append") ||
                code.Contains("len")) code = BRACKET_S(code);
            return code;
        }


        /* 파서 */
        public string PARSER(string code = "") {
            BeforeCode = code;

            code = STRING_S(code);

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
            code = Regex.Replace(code, "(섞기)", "shuffle");
            code = Regex.Replace(code, "(랜덤|난수)", "random");
            if (code.Contains("randint") ||
                code.Contains("uniform") ||
                code.Contains("randrange") ||
                code.Contains("sample") ||
                code.Contains("choice") ||
                code.Contains("shuffle")) code = BRACKET_S(code);
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