#pragma once

#include <iostream>
#include <string>
#include <vector>

// SHELL
#define SHELL_VERSION               "빌드 : "
#define SHELL_MESSAGE               "'도움말' 명령어를 통하여, 사용 방법을 확인 할 수 있습니다."

// HELP
#define HELP_TITLE                  "도움말"
#define HELP_MESSAGE                "'https://backgwa.github.io/DoKev/'에서 상세한 정보를 볼 수 있습니다."

// DEBUG
#define ERROR                       "오류: "
#define HELP                        "도움말: "
#define SYNTAX_ERROR                "아래 수정사항을 적용해보세요."
#define RECHECKING                  "다음과 같은 오류가 없는지 확인해보세요."

// UNKNOWN_COMMAND
#define UNKNOWN_COMMAND_TITLE       "알 수 없는 명령어입니다."
#define UNKNOWN_COMMAND_MESSAGE     "이러한 명령은 지원하지 않음"
#define UNKNOWN_COMMAND_INDEX       2

std::vector<std::pair<std::string, std::string>> UNKNOWN_COMMAND_SUGGESTION_CONTENT = {
  {"지원되지 않는 명령어", "다음 명령이 지원되는지 확인해보세요."},
  {"명령어에 오타 포함", "실행하려는 명령에 오타가 없는지 확인해보세요."}
};

// BLANK_PATH
#define BLANK_PATH_TITLE            "경로가 입력되지 않았습니다."
#define BLANK_PATH_MESSAGE          "이 곳에서 추가 인자를 받지 못 함"
#define BLANK_PATH_INDEX            1

std::vector<std::pair<std::string, std::string>> BLANK_PATH_SUGGESTION_CONTENT = {
  {"경로 미입력", "컴파일 대상 소스 코드의 경로가 필요합니다."}
};

// BLANK_CODE
#define BLANK_CODE_TITLE            "코드가 입력되지 않았습니다."
#define BLANK_CODE_MESSAGE          "이 곳에서 추가 인자를 받지 못 함"
#define BLANK_CODE_INDEX            1

std::vector<std::pair<std::string, std::string>> BLANK_CODE_SUGGESTION_CONTENT = {
  {"코드 미입력", "실행하려는 코드가 비어있지 않아야합니다."}
};

// UNKNOWN_OPTION
#define UNKNOWN_OPTION_TITLE        "올바르지 않은 옵션을 받았습니다."
#define UNKNOWN_OPTION_MESSAGE      "이런 옵션은 올바르지 못 함"
#define UNKNOWN_OPTION_SUGGESTION   "적용 가능한 옵션은 아래와 같습니다."
#define UNKNOWN_OPTION_INDEX        3

std::vector<std::pair<std::string, std::string>> UNKNOWN_OPTION_SUGGESTION_CONTENT = {
  {"-c <파일_경로>", "경로의 파일을 컴파일한 후, 실행합니다."},
  {"-i <실행_코드>", "실행 코드를 번역 후, 실행합니다."},
  {"-t", "컴파일러가 정상 작동하는지 확인합니다."}
};

// UNKNOWN_PATH
#define UNKNOWN_PATH_TITLE          "파일 경로가 유효하지 않습니다."
#define UNKNOWN_PATH_MESSAGE        "해당 경로는 유효하지 않음"
#define UNKNOWN_PATH_INDEX          3

std::vector<std::pair<std::string, std::string>> UNKNOWN_PATH_SUGGESTION_CONTENT = {
  {"파일 여부", "경로에 파일이 존재하는지 확인해보세요."},
  {"따옴표 미사용", "경로에 띄어쓰기가 있다면, 경로를 따옴표로 묶어보세요."},
  {"경로에 오타 포함", "경로나 파일 이름에 오타가 포함되어 있는지, 확인해보세요."}
};

// THROW
#define THROW_TITLE                 "코드가 중단되었습니다."
#define THROW_MESSAGE               "중단점 호출됨"
#define THROW_INDEX                 2

std::vector<std::tuple<std::string, std::string, bool>> THROW_SUGGESTION_CONTENT = {
  {"NEXT()", "다음 함수로 대체해보세요.", true},
  {"THROW()", "다음 코드를 제거해보세요.", false}
};