#pragma once

#include "String/Split.hpp"
#include "String/Utf8_strlen.hpp"
#include "String/ToUnicode.hpp"
#include "String/Substring.hpp"
#include "String/ToMultiByte.hpp"
#include "String/Findword.hpp"
#include "String/Valid.hpp"

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

/* line_counter : 코드 라인 문자열을 출력합니다. */
void line_counter(int line, bool number_show = true, bool newline = false) {
  cout << CYAN << BOLD;
  if (number_show)
    cout << line;
  else {
    int blank = Utf8_strlen(to_string(line));
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

/* endword : 문자 끝에 하이라이팅 표시를 출력합니다. */
void endword(int line, const string& code, const string& MESSAGE = "", const string& COLOR = RED) {
  line_counter(line, false);

  cout << COLOR << BOLD;

  for (int i = 0; i < Utf8_strlen(code); i++)
    if (IsKorean(ToMultiByte(Substring(ToUnicode(code), i, 1))))
      cout << "  ";
    else
      cout << " ";

  cout << "^";

  cout << " " << MESSAGE;
  cout << RESET << endl;
}

/* highlighter : 특정 문자 및 코드를 하이라이팅 문자를 출력합니다. */
void highlighter(int line, const string& code, const string& highlight, const string& MESSAGE = "", const string& COLOR = RED) {
  int hint;
  if(highlight == "M") {
    if (Valid(code, "\""))
      hint = Findword(code, "\"");

    else if (Valid(code, "\'"))
      hint = Findword(code, "\'");
  } else {
      hint = Findword(code, highlight);
  }
  line_counter(line, false);

  cout << COLOR << BOLD;
  
  for (int i = 0; i < hint; i++)
    if (IsKorean(ToMultiByte(Substring(ToUnicode(code), i, 1))))
      cout << "  ";
    else
      cout << " ";
    

  for (int i = 0; i < Utf8_strlen(highlight); i++)
    cout << "^";

  cout << " " << MESSAGE;
  cout << RESET << endl;
}