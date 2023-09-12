
#define VERSION "8"
#define BUILD "달갗낟"

#include <iostream>
#include <string>
#include "langpack.hpp"
#include "shell.hpp"
#include "compiler.hpp"
#include "debugger.hpp"
#include "check.hpp"

using namespace std;

/* 함수 선언 */
void argv_isValid(int argc, char *argv[]);
void interpreted(string Code);
void shell();

/* main : DoKevEngine의 시작점입니다. */
int main(int argc, char *argv[]) {
  if (argc >= 2 && argc <= 3)
    argv_isValid(argc, argv);
  else
    shell();

  return 0;
}

/* argv_isValid : 옵션 및 인자를 확인합니다. */
void argv_isValid(int argc, char *argv[]) {

  string TARGET;
  for (int i = 0; i < argc; i++)
    TARGET = TARGET + " " + argv[i];

  const string option = argv[1];
  const string argument = (argc >= 3) ? argv[2] : "";

  if (option == "-c") {
    if (blankcheck(argument)) {
      // BLANK_PATH 오류 출력
      StandardError(0, BLANK_PATH_TITLE, BLANK_PATH_MESSAGE, TARGET, option, RECHECKING,
                    BLANK_PATH_SUGGESTION_CONTENT, BLANK_PATH_INDEX);

    } else if (!filecheck(argument)) {
      // UNKNOWN_PATH 오류 출력
      StandardError(0, UNKNOWN_PATH_TITLE, UNKNOWN_PATH_MESSAGE, TARGET, argument, RECHECKING,
                    UNKNOWN_PATH_SUGGESTION_CONTENT, UNKNOWN_PATH_INDEX);
                    
    } else {
      compile(argument, TARGET, argument);
    }
  } else if (option == "-i") {
    if (blankcheck(argument)) {
      // BLANK_CODE 오류 출력
      StandardError(0, BLANK_CODE_TITLE, BLANK_CODE_MESSAGE, TARGET, option, RECHECKING,
                    BLANK_CODE_SUGGESTION_CONTENT, BLANK_CODE_INDEX);
                    
    } else {
      interpreted(argument);
    }
  } else {
    // UNKNOWN_OPTION 오류 출력
    StandardError(0, UNKNOWN_OPTION_TITLE, UNKNOWN_OPTION_MESSAGE, TARGET, option,
                  UNKNOWN_OPTION_SUGGESTION, UNKNOWN_OPTION_SUGGESTION_CONTENT, UNKNOWN_OPTION_INDEX);
  }
}

/* interpreted : 코드를 인자로 받아 즉시 번역해 실행합니다. */
void interpreted(string code) {
  parsing(0, code, true);
}

/* shell : DoKevEngine의 쉘 인터페이스를 실행합니다. */
void shell() {
  Shell_open();
}