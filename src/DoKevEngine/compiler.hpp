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
            FILE_OPEN_ERROR_SUGGESTION_CONTENT,
            FILE_OPEN_ERROR_INDEX);
    } else {
        // 파일을 읽어 codelist에 저장
        string line;
        while (std::getline(file, line)) {
            codelist.push_back(line);
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

/* PARTICAL : 조사인지, 검사하고 변경합니다. */
string PARTICAL_TOKEN(string line) {
    regex pattern("\"([^\"]*)\"|'([^']*)'|이라고 |라고 |을 |를 ");
    smatch matches;
    string result;

    auto it = line.cbegin();
    
    while (regex_search(it, line.cend(), matches, pattern)) {
        const string match = matches[0];
        result += matches.prefix();

        if (match == "이라고 " || match == "라고 " || match == "을 " || match == "를 " )
            result += "->[particle]->";
        else
            result += matches[0];

        it = matches[0].second;
    }

    result += string(it, line.cend());
    return result;
}

/* VERB : 동사인지, 검사하고 변경합니다. */
string VERB_TOKEN(string line) {
    regex pattern("\"([^\"]*)\"|'([^']*)'|줘|하고|해주고|주고|고");
    smatch matches;
    string result;

    auto it = line.cbegin();
    
    while (regex_search(it, line.cend(), matches, pattern)) {
        const string match = matches[0];
        result += matches.prefix();

        if (match == "줘" || match == "하고" || match == "해주고" || match == "주고" || match == "고" )
            result += "[verb]";
        else
            result += matches[0];

        it = matches[0].second;
    }

    result += string(it, line.cend());
    return result;
}

string PRINT_TOKEN(string line) {
    regex pattern("\"([^\"]*)\"|'([^']*)'|말해|보여|말한|출력|말하");
    smatch matches;
    string result;

    auto it = line.cbegin();
    
    while (regex_search(it, line.cend(), matches, pattern)) {
        const string match = matches[0];
        result += matches.prefix();

        if (match == "말해" || match == "보여" || match == "말한" || match == "출력" || match == "말하" )
            result += "[print]->";
        else
            result += matches[0];
        it = matches[0].second;
    }

    result += string(it, line.cend());
    return result;
}

/* PRINT : 출력문인지, 검사하고 변경합니다. */
string PRINT(string line) {
    line = PARTICAL_TOKEN(line);
    line = VERB_TOKEN(line);
    line = PRINT_TOKEN(line);

    // 출력문이 아니면, 파서 종료
    if(!line.contains("[print]->")) {
        return line;
    }

    // 조사가 없으면, 오류 발생
    if(!line.contains("->[particle]->"))
        SyntaxError(line_number + 1,
            NOT_CONTAIN_PARTICLE_TITLE,
            NOT_CONTAIN_PARTICLE_MESSAGE,
            before_code,
            "\"",
            NOT_CONTAIN_PARTICLE_SUGGESTION_CONTENT,
            NOT_CONTAIN_PARTICLE_INDEX
        );

    // 동사가 없으면, 오류 발생
    if(!line.contains("[verb]"))
        SyntaxError(line_number + 1,
            NOT_CONTAIN_VERB_TITLE,
            NOT_CONTAIN_VERB_MESSAGE,
            before_code,
            "EOW",
            NOT_CONTAIN_VERB_SUGGESTION_CONTENT,
            NOT_CONTAIN_VERB_INDEX
        );

    return line;
}

/* parsing : 코드를 확인, 변경하고, 검사합니다. */
string parsing(int index, string line, bool shell = false) {

    // 원본 코드
    before_code = line;

    // 주석 제거 처리
    line = COMMENT(line);

    // 출력문 처리
    line = PRINT(line);

    // 코드 변경
    if(!shell) codelist[index] = line;
    return line;
}

/* compile : 파일을 입력받아 컴파일합니다.*/
void compile(string file_path, string TARGET, string MAKER) {
    openfile(file_path, TARGET, MAKER);

    for (const string& line : codelist) {
        parsing(line_number, line);
        line_number++;
    }

    for (const string& line : codelist) {
        cout << line << endl;
    }
}