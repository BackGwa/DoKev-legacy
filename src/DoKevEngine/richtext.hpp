
#include <iostream>
using namespace std;

/* 색상 정의 */
#define RED     "\x1B[91m"
#define GREEN   "\x1B[92m"
#define YELLOW  "\x1B[93m"
#define CYAN    "\x1B[96m"
#define RESET   "\x1B[0m"

/* line_counter : 코드 라인 문자열을 출력합니다. */
void line_counter(int line, bool number_show = true) {
  cout << CYAN;
  if (number_show)
    cout << line;
  else {
    int blank = to_string(line).length();
    for (int i = 0; i < blank; i++)
      cout << " ";
  }
  cout << " ∥ ";
  cout << RESET;
}

/* highlighter : 특정 문자 및 코드를 하이라이팅 문자를 출력합니다. */
void highlighter(int line, string code, string highlight) {
  
  int hint = code.find(highlight);
  line_counter(line, false);

  cout << RED;

  for (int i = 0; i < hint; i++) {
    cout << " ";
  }
  for (int i = 0; i < highlight.length(); i++) {
    cout << "^";
  }

  cout << RESET;
}