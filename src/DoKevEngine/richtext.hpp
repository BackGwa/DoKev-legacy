
#include <iostream>
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

/* findword : 전체 문자열에서 특정 문자열을 찾아 인덱스를 반환합니다. */
int findword(string code, string word){
  return code.find(word);
}

/* line_counter : 코드 라인 문자열을 출력합니다. */
void line_counter(int line, bool number_show = true, bool newline = false) {
  cout << CYAN << BOLD;
  if (number_show)
    cout << line;
  else {
    int blank = to_string(line).length();
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

/* highlighter : 특정 문자 및 코드를 하이라이팅 문자를 출력합니다. */
void highlighter(int line, string code, string highlight, string MESSAGE = "", string COLOR = RED) {
  int hint = findword(code, highlight);
  line_counter(line, false);

  cout << COLOR << BOLD;

  for (int i = 0; i < hint; i++)
    cout << " ";
  for (int i = 0; i < highlight.length(); i++)
    cout << "^";

  cout << " " << MESSAGE;
  cout << RESET << endl;
}