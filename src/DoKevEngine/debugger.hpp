#pragma once

#include <iostream>
#include "richtext.hpp"

using namespace std;

/* StandardError : 표준 오류에 대한 내용을 출력합니다. */
void StandardError(int LINE,
                  string TITLE,
                  string MESSAGE,
                  string TARGET,
                  string HIGHLIGHT,
                  string SUGGESTION,
                  const std::vector<std::pair<std::string, std::string> >& SUGGESTION_CONTENT,
                  int len = 1
                  ) {

  cout << endl << RED << "오류: " << RESET << TITLE << endl;
  line_counter(LINE, false);
  cout << endl;
  line_counter(LINE);
  cout << TARGET << endl;

  highlighter(LINE, TARGET, HIGHLIGHT);

  cout << " " << RED << MESSAGE << RESET << endl << endl;

  cout << CYAN << "도움말: " << RESET << SUGGESTION << endl;

  line_counter(LINE, false);
  cout << endl;

  for (int i = 0; i < len; i++) {
    line_counter(LINE, false);
    cout << GREEN << SUGGESTION_CONTENT[i].first << RESET << " : ";
    cout << SUGGESTION_CONTENT[i].second << endl;
  }
  exit(0);
}