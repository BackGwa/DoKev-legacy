
#define DOKEV_VERSION "Developer Version 8"

#include <iostream>
#include <string>
#include "shell.h"
#include "compiler.h"
#include "debugger.h"

/* 함수 선언 */
void argv_isValid(int argc, char* argv[]);
void inerpreted(std::string Code);
void compile(std::string FilePath);
void shell();

/* main : DoKevEngine의 시작점입니다. */
int main(int argc, char* argv[]) {
    if (argc > 1 && argc < 3)
        argv_isValid(argc, argv);
    else
        shell();

    return 0;
}

/* argv_isValid : 옵션 및 인자를 확인합니다. */
void argv_isValid(int argc, char* argv[]) {
    // 옵션 및 인자 받기
    std::string option = argv[1];
    std::string argument = argv[2];

    if      (option == "-i") inerpreted(argument);
    else if (option == "-c") compile(argument);
}

/* inerpreted : 코드를 인자로 받아 즉시 번역해 실행합니다. */
void inerpreted(std::string Code) {
    std::cout << "인터프리트 실행" << std::endl;
}

/* compile : 파일 경로를 인자로 받아 컴파일 후 실행합니다. */
void compile(std::string FilePath) {
    std::cout << "컴파일 실행" << std::endl;
}

/* shell : DoKevEngine의 쉘 인터페이스를 실행합니다. */
void shell() {
    std::cout << "쉘 인터페이스 실행" << std::endl;
}