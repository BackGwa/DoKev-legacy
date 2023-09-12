#pragma once

#include <iostream>
#include "langpack.hpp"
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
                  int len = 1,
                  bool breaks = true
                  ) {

  cout << endl << RED << BOLD << ERROR << RESET << BOLD << TITLE << RESET << endl;
  line_counter(LINE, false, true);
  line_counter(LINE);
  cout << TARGET << endl;

  highlighter(LINE, TARGET, HIGHLIGHT, MESSAGE);

  separator(MESSAGE);

  cout << CYAN << BOLD << HELP << RESET << BOLD << SUGGESTION << RESET << endl;

  line_counter(LINE, false, true);

  for (int i = 0; i < len; i++) {
    line_counter(LINE, false);
    cout << GREEN << BOLD << SUGGESTION_CONTENT[i].first << ": " << RESET << SUGGESTION_CONTENT[i].second << endl;
  }
  if (breaks) exit(0);
  else cout << endl;
}

/* SyntaxError : 코드의 문법적 에러를 출력합니다. */
void SyntaxError(int LINE,
                string TITLE,
                string MESSAGE,
                string TARGET,
                string HIGHLIGHT,
                const std::vector<std::tuple<std::string, std::string, bool>>& SUGGESTION_CONTENT,
                int len = 1
                ) {

  cout << endl << RED << BOLD << ERROR << RESET << BOLD << TITLE << endl;
  line_counter(LINE, false, true);
  line_counter(LINE);
  cout << TARGET << endl;

  if(HIGHLIGHT == "EOW")
    highlighter(LINE, TARGET + " ", " ", MESSAGE);
  else
    highlighter(LINE, TARGET, HIGHLIGHT, MESSAGE);

  separator(MESSAGE);

  cout << CYAN << BOLD << HELP << RESET << BOLD << SYNTAX_ERROR << RESET << endl;

  for (int i = 0; i < len; i++) {
    const string SUGGESTION_CODE = get<0>(SUGGESTION_CONTENT[i]);
    const string SUGGESTION_CODE_MESSAGE = get<1>(SUGGESTION_CONTENT[i]);
    const string SAFE = (get<2>(SUGGESTION_CONTENT[i]) ? GREEN : RED);

    if (i != 0) cout << endl;
    line_counter(LINE, false, true);
    line_counter(LINE);
    cout << SUGGESTION_CODE << endl;
    line_counter(LINE, false);
    cout << SAFE << BOLD << "~ " << SUGGESTION_CODE_MESSAGE << RESET << endl;
  }

  exit(0);
}