
#define DOKEV_VERSION "Developer Version 8"

#include <iostream>
#include <string>
#include "langpack.h"
#include "shell.h"
#include "compiler.h"
#include "debugger.h"

using namespace std;

/* 함수 선언 */
void argv_isValid(int argc, char *argv[]);
void inerpreted(string Code);
void compile(string FilePath);
void shell();

/* main : DoKevEngine의 시작점입니다. */
int main(int argc, char *argv[]) {
  if (argc == 3)
    argv_isValid(argc, argv);
  else
    shell();

  return 0;
}

/* argv_isValid : 옵션 및 인자를 확인합니다. */
void argv_isValid(int argc, char *argv[]) {
  string option = argv[1];     // 옵션 가져오기
  string argument = argv[2];   // 인자 가져오기

  if (option == "-i")
    inerpreted(argument);
  else if (option == "-c")
    compile(argument);
  else {
    string TARGET = "";

    for (int i = 0; i < argc; i++) {
      TARGET = TARGET + " " + argv[i];
    }

    StandardError(0,
                  UNKNOWN_OPTION_TITLE,
                  UNKNOWN_OPTION_MESSAGE,
                  TARGET,
                  option,
                  UNKNOWN_OPTION_SUGGESTION,
                  UNKNOWN_OPTION_SUGGESTION_CONTENT,
                  2);
  }
}

/* inerpreted : 코드를 인자로 받아 즉시 번역해 실행합니다. */
void inerpreted(string code) {
  cout << "인터프리트 실행" << endl;
}

/* compile : 파일 경로를 인자로 받아 컴파일 후 실행합니다. */
void compile(string file_path) {
  cout << "컴파일 실행" << endl;
}

/* shell : DoKevEngine의 쉘 인터페이스를 실행합니다. */
void shell() {
  cout << "쉘 인터페이스 실행" << endl;
}