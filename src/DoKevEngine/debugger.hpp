#pragma once

#include <iostream>
#include "langpack.hpp"
#include "richtext.hpp"

using namespace std;

/* BLANK_REMOVE : 불필요하게 많은 공백을 제거합니다. */
string BLANK_REMOVE(string line) {
    regex pattern("\"([^\"]*)\"|'([^']*)'|  ");
    smatch matches;
    string result;

    auto it = line.cbegin();
    
    while (regex_search(it, line.cend(), matches, pattern)) {
        const string match = matches[0];
        result += matches.prefix();

        if (match == "  ")
            result += "";
        else
            result += matches[0];

        it = matches[0].second;
    }

    result += string(it, line.cend());
    return result;
}

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
  cout << BLANK_REMOVE(TARGET) << endl;

  highlighter(LINE, BLANK_REMOVE(TARGET), HIGHLIGHT, MESSAGE);

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
  cout << BLANK_REMOVE(TARGET) << endl;

  if(HIGHLIGHT == "EOW")
    endword(LINE, BLANK_REMOVE(TARGET), MESSAGE);
  else if(HIGHLIGHT == "M")
    highlighter(LINE, BLANK_REMOVE(TARGET), "M", MESSAGE);
  else
    highlighter(LINE, BLANK_REMOVE(TARGET), HIGHLIGHT, MESSAGE);

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

/* SyntaxWarning : 코드의 문법적 경고를 출력합니다. */
void SyntaxWarning(int LINE,
                  string TITLE,
                  string MESSAGE,
                  string TARGET,
                  string HIGHLIGHT
                  ) {

  cout << endl << YELLOW << BOLD << WARN << RESET << BOLD << TITLE << endl;
  line_counter(LINE, false, true);
  line_counter(LINE);
  cout << BLANK_REMOVE(TARGET) << endl;

  if(HIGHLIGHT == "EOW")
    endword(LINE, BLANK_REMOVE(TARGET), MESSAGE);
  else if(HIGHLIGHT == "ALL")
    highlighter(LINE, BLANK_REMOVE(TARGET), BLANK_REMOVE(TARGET), MESSAGE);
  else
    highlighter(LINE, BLANK_REMOVE(TARGET), HIGHLIGHT, MESSAGE);

  separator(MESSAGE);
}