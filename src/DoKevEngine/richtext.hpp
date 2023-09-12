
#include <locale>
#include <codecvt>

using namespace std;

/* 색상 정의 */
#define RED     "\x1B[91m"
#define GREEN   "\x1B[92m"
#define YELLOW  "\x1B[93m"
#define CYAN    "\x1B[96m"
#define GRAY    "\x1B[90m"
#define RESET   "\x1B[0m"

/* 폰트 정의 */
#define BOLD    "\x1B[1m"
#define UDLINE  "\x1B[9m"

/* utf8_strlen : 문자열 길이를 정상적으로 반환합니다. */
int utf8_strlen(const string &str) {
    int len = 0;
    for (size_t i = 0; i < str.length(); ) {
        if ((str[i] & 0xC0) != 0x80) len++;
        i++;
    }
    return len;
}

/* ToUnicode : 입력받은 string 문자열을 wstring 문자열로 변환합니다. */
wstring ToUnicode(const string &str) {
    wstring_convert<codecvt_utf8<wchar_t>> converter;
    return converter.from_bytes(str);
}

/* findword : 전체 문자열에서 특정 문자열을 찾아 인덱스를 반환합니다. */
int findword(const string code, const string word) {

    size_t found = ToUnicode(code).rfind(ToUnicode(word));
    if (found != wstring::npos) {
        return static_cast<int>(found);
    }
    return -1;
}

/* line_counter : 코드 라인 문자열을 출력합니다. */
void line_counter(int line, bool number_show = true, bool newline = false) {
  cout << CYAN << BOLD;
  if (number_show)
    cout << line;
  else {
    int blank = utf8_strlen(to_string(line));
    for (int i = 0; i < blank; i++)
      cout << " ";
  }
  cout << " | ";
  cout << RESET;
  if (newline) cout << endl;
}

/* separator : 구분자를 생성합니다. */
void separator(string MESSAGE) {
  cout << endl << GRAY << UDLINE;
  for (int i = 0; i < MESSAGE.length() * 2; i++)
    cout << " ";
  cout << RESET << endl << endl;
}

/* IsKorean : 입력 된 문자열이 한글인지 아닌지 확인합니다. */
bool IsKorean(const string& str) {
    regex korean("[가-힣]+");
    return regex_search(str, korean);
}

/* highlighter : 특정 문자 및 코드를 하이라이팅 문자를 출력합니다. */
void highlighter(int line, const string& code, const string& highlight, const string& MESSAGE = "", const string& COLOR = RED) {
  int hint = findword(code, highlight);
  line_counter(line, false);

  cout << COLOR << BOLD;
  
  for (int i = 0; i < hint; i++)
    cout << " ";
  for (int i = 0; i < utf8_strlen(highlight); i++)
    cout << "^";

  cout << " " << MESSAGE;
  cout << RESET << endl;
}