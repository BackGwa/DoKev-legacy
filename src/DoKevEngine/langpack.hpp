#pragma once

#include <iostream>
#include <string>
#include <vector>

// DEBUG MESSAGE
#define ERROR "오류: "
#define HELP "도움말: "

// UNKNOWN_OPTION
#define UNKNOWN_OPTION_TITLE              "올바르지 않은 옵션을 받았습니다"
#define UNKNOWN_OPTION_MESSAGE            "올바른 옵션이 아님"
#define UNKNOWN_OPTION_SUGGESTION         "다음 옵션 중 하나를 선택해 시도해보세요."

std::vector<std::pair<std::string, std::string>> UNKNOWN_OPTION_SUGGESTION_CONTENT = {
    {"-c <파일_경로>", "경로의 파일을 컴파일한 후, 실행합니다."},
    {"-i <실행_코드>", "실행 코드를 번역 후, 실행합니다."}
};

// UNKNOWN_PATH
#define UNKNOWN_PATH_TITLE              "파일 경로가 유효하지 않습니다."
#define UNKNOWN_PATH_MESSAGE            "올바른 경로가 아님"
#define UNKNOWN_PATH_SUGGESTION         "다음 사항을 확인해보세요."

std::vector<std::pair<std::string, std::string>> UNKNOWN_PATH_SUGGESTION_CONTENT = {
    {"파일 여부", "경로에 실제로 파일이 있는지 확인해보세요."},
    {"따옴표로 묶기", "경로에 띄어쓰기가 있다면, 경로를 따옴표로 묶어보세요."},
    {"오타 포함", "경로나 파일 이름에 오타가 포함되어 있는지, 확인해보세요."}
};