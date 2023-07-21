
#include <iostream>
#include <string>
#include <vector>

// UNKNOWN_OPTION
#define UNKNOWN_OPTION_TITLE              "올바르지 않은 옵션을 받았습니다"
#define UNKNOWN_OPTION_MESSAGE            "올바른 옵션이 아님"
#define UNKNOWN_OPTION_SUGGESTION         "다음 옵션 중 하나를 선택해 시도해보세요."

std::vector<std::pair<std::string, std::string>> UNKNOWN_OPTION_SUGGESTION_CONTENT = {
    {"-c <파일_경로>", "경로의 파일을 컴파일한 후, 실행합니다."},
    {"-i <실행_코드>", "실행 코드를 번역 후, 실행합니다."}
};