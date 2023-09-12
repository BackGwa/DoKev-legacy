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
#define WARN                        "경고: "
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
  {"-i <실행_코드>", "실행 코드를 번역 후, 실행합니다."}
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

// UNVALID_SOV
#define UNVALID_SOV_TITLE       "이러한 문법은 허용되지 않습니다."
#define UNVALID_SOV_MESSAGE     "문법적 오류"
#define UNVALID_SOV_INDEX       2

std::vector<std::tuple<std::string, std::string, bool>> UNVALID_SOV_SUGGESTION_CONTENT = {
  {"SOV형 사용", "일부 함수는 SOV형으로 작성해야합니다.", true},
  {"단어초과 확인", "추가적인 단어 및 행동이 삽입되어 있는지 확인하세요.", false}
};

// FILE_OPEN_ERROR
#define FILE_OPEN_ERROR_TITLE       "파일을 여는 도중 오류가 발생하였습니다."
#define FILE_OPEN_ERROR_MESSAGE     "파일을 읽을 수 없음"
#define FILE_OPEN_ERROR_INDEX       2

std::vector<std::pair<std::string, std::string>> FILE_OPEN_ERROR_SUGGESTION_CONTENT = {
  {"파일 여부", "경로에 파일이 존재하는지 확인해보세요."},
  {"읽기 가능 여부", "파일이 읽을 수 있는 상태인지 확인해보세요."}
};

// FILE_WRITE_ERROR
#define FILE_WRITE_ERROR_TITLE       "쓰기를 실패하였습니다."
#define FILE_WRITE_ERROR_MESSAGE     "접근 실패"
#define FILE_WRITE_ERROR_INDEX       3

std::vector<std::pair<std::string, std::string>> FILE_WRITE_ERROR_SUGGESTION_CONTENT = {
  {"컴파일러 권한", "컴파일러가 파일을 생성 할 수 있는지, 확인해보세요."},
  {"파일 참조 여부", "다른 프로세스가 파일에 접근하고 있는지, 확인해보세요."},
  {"쓰기 가능 여부", "파일이 읽기 전용 상태는 아닌지, 확인해보세요."}
};

// DIR_CREATE_ERROR
#define DIR_CREATE_ERROR_TITLE       "디렉토리 생성에 실패하였습니다."
#define DIR_CREATE_ERROR_MESSAGE     "접근 실패"
#define DIR_CREATE_ERROR_INDEX       1

std::vector<std::pair<std::string, std::string>> DIR_CREATE_ERROR_SUGGESTION_CONTENT = {
  {"컴파일러 권한", "컴파일러가 디렉토리를 생성 할 수 있는지, 확인해보세요."}
};

// NOT_CONTAIN_PARTICLE
#define NOT_CONTAIN_PARTICLE_TITLE       "조사가 존재하지 않습니다."
#define NOT_CONTAIN_PARTICLE_MESSAGE     "조사가 존재해야함"
#define NOT_CONTAIN_PARTICLE_INDEX       1

std::vector<std::tuple<std::string, std::string, bool>> NOT_CONTAIN_PARTICLE_SUGGESTION_CONTENT = {
  {"(<값>)을 말해줘", "조사를 넣어 코드를 작성해보세요.", true}
};

// NOT_CONTAIN_VERB
#define NOT_CONTAIN_VERB_TITLE       "동사가 존재하지 않습니다."
#define NOT_CONTAIN_VERB_MESSAGE     "동사가 존재해야함"
#define NOT_CONTAIN_VERB_INDEX       1

std::vector<std::tuple<std::string, std::string, bool>> NOT_CONTAIN_VERB_SUGGESTION_CONTENT = {
  {"(<값>)을 말해줘", "동사를 넣어 코드를 작성해보세요.", true}
};

// CONTAIN_BRACKET
#define CONTAIN_BRACKET_TITLE       "괄호를 포함하는 것이 좋습니다."
#define CONTAIN_BRACKET_MESSAGE     "괄호가 권장됨"

// CLOSE_LEFT_BRACKET
#define CLOSE_LEFT_BRACKET_TITLE       "괄호가 닫히지 않았습니다."
#define CLOSE_LEFT_BRACKET_MESSAGE     "닫는 괄호가 없음"
#define CLOSE_LEFT_BRACKET_INDEX       1

std::vector<std::tuple<std::string, std::string, bool>> CLOSE_LEFT_BRACKET_SUGGESTION_CONTENT = {
  {"()", "다음과 같이 괄호는 닫혀야합니다.", true}
};

// CLOSE_RIGHT_BRACKET
#define CLOSE_RIGHT_BRACKET_TITLE       "괄호가 열리지 않았습니다."
#define CLOSE_RIGHT_BRACKET_MESSAGE     "여는 괄호가 없음"
#define CLOSE_RIGHT_BRACKET_INDEX       1

std::vector<std::tuple<std::string, std::string, bool>> CLOSE_RIGHT_BRACKET_SUGGESTION_CONTENT = {
  {"()", "다음과 같이 괄호는 열려야합니다.", true}
};