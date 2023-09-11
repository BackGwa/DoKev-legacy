#pragma once

#include <fstream>
#include <vector>
#include <string>
#include <regex>
#include "debugger.hpp"

using namespace std;

int line_number = 0;
vector<std::string> codelist;
string before_code;

/* openfile : 파일을 읽어 codelist 전역 변수에 저장합니다. */
void openfile(string filepath, string TARGET, string MAKER) {
    ifstream file(filepath);
    
    // 파일 읽기 실패 시 오류 출력
    if(file.fail()) {
        StandardError(0,
            FILE_OPEN_ERROR_TITLE ,
            FILE_OPEN_ERROR_MESSAGE,
            TARGET,
            MAKER,
            RECHECKING,
            FILE_OPEN_ERROR_CONTENT,
            FILE_OPEN_ERROR_INDEX);
    } else {
        // 파일을 읽어 codelist에 저장
        string line;
        while (std::getline(file, line)) {
            if(line != "") codelist.push_back(line);
        }
        file.close();
    }
}

/* COMMENT : 주석을 제거합니다. */
string COMMENT(string line) {
    string result;
    bool insideString = false;

    for (size_t i = 0; i < line.length(); ++i) {
        char currentChar = line[i];

        if (!insideString) {
            // 주석 시작 확인 & '#' 이후의 문자 모두 무시
            if (currentChar == '#')
                while (i < line.length() && line[i] != '\n') ++i;

            // 문자열 시작 확인
            else if (currentChar == '"' || currentChar == '\'') {
                insideString = true;
                result += currentChar;
            }
            
            else result += currentChar;

        } else {
            // 종료 따옴표 찾기
            result += currentChar;
            if (currentChar == '"' || currentChar == '\'')
                insideString = false;
        }
    }

    return result;
}


/* PRINT : 출력문인지, 검사하고 변경합니다. */
string PRINT(string line) {
    // 파서 정규식
    regex pattern("\"([^\"]*)\"|'([^']*)'|말해줘");
    smatch matches;

    // 변경된 문자열 저장
    string result;

    // 문자열 처리
    auto it = line.cbegin();
    while (regex_search(it, line.cend(), matches, pattern)) {
        result += matches.prefix();
        if (matches[0] == "말해줘")
            result += "$(PRINT)";
        else
            result += matches[0];
        it = matches[0].second;
    }

    // 나머지 추가
    result += std::string(it, line.cend());

    return result;
}

/* parsing : 코드를 확인, 변경하고, 검사합니다. */
void parsing(int index, string line) {

    // 주석 제거 처리
    line = COMMENT(line);

    // 출력문 처리
    line = PRINT(line);

    // 코드 변경
    codelist[index] = line;
}

/* compile : 파일을 입력받아 컴파일합니다.*/
void compile(string file_path, string TARGET, string MAKER) {
    openfile(file_path, TARGET, MAKER);

    for (const string& line : codelist) {
        before_code = line;
        parsing(line_number, line);
        line_number++;
    }

    for (const string& line : codelist) {
        cout << line << endl;
    }
}