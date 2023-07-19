
#include <iostream>
#include <string>
#include "compiler.h"
#include "debugger.h"

/* 함수 선언 */
int argv_check(int argc, char* argv[]);
int inerpreted(std::string Code);
int compile(std::string FilePath);
int shell();

/* main : DoKevEngine의 시작점입니다. */
int main(int argc, char* argv[]) {
    if (argc < 2)
        argv_check(argc, argv);
    else
        shell();

    return 0;
}

/* argv_check : 옵션 및 인자를 확인합니다. */
int argv_check(int argc, char* argv[]) {
    // 옵션 및 인자 받기
    std::string option = argv[1];
    std::string argument = argv[2];

    if      (option == "-i")    inerpreted(argument);
    else if (option == "-c")    compile(argument);

    return 1;
}

/* inerpreted : 코드를 인자로 받아 즉시 번역해 실행합니다. */
int inerpreted(std::string Code) {
    return 0;
}

/* compile : 파일 경로를 인자로 받아 컴파일 후 실행합니다. */
int compile(std::string FilePath) {
    return 0;
}

/* shell : DoKevEngine의 쉘 인터페이스를 실행합니다. */
int shell() {
    return 0;
}