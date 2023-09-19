#pragma once

#include <fstream>
#include <regex>

#include "check.hpp"
#include "debugger.hpp"

#include "Syntax/COMMENT.hpp"
#include "Syntax/CODEAREA.hpp"
#include "Syntax/PARTICAL.hpp"
#include "Syntax/VERB.hpp"
#include "Syntax/QUERYSTRING.hpp"
#include "Syntax/VARIABLE.hpp"

#include "String/Valid.hpp"
#include "String/BlankRemove.hpp"

int line_number = 0;
vector<string> codelist;
string before_code;

/* openfile : 파일을 읽어 codelist 전역 변수에 저장합니다. */
void openfile(string filepath, string TARGET) {
    ifstream file(filepath);
    
    // 파일 읽기 실패 시 오류 출력
    if(file.fail()) {
        StandardError(0,
            FILE_OPEN_ERROR_TITLE ,
            FILE_OPEN_ERROR_MESSAGE,
            TARGET,
            filepath,
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

/* BRACKET : 괄호의 유무를 검사합니다. */
bool BRACKET(string line) {
    regex pattern("(\"[^\"]*\")|('[^']*')");

    string stripped_str = regex_replace(line, pattern, "");

    int LC = 0, RC = 0;

    for (char c : stripped_str) {
        if (c == '(') LC++;
        else if (c == ')') RC++;
    }

    // 괄호가 열리지 않았을때, 오류 발생
    if(LC < RC) {
        SyntaxError(line_number + 1,
            CLOSE_RIGHT_BRACKET_TITLE,
            CLOSE_RIGHT_BRACKET_MESSAGE,
            before_code,
            ")",
            CLOSE_RIGHT_BRACKET_SUGGESTION_CONTENT,
            CLOSE_RIGHT_BRACKET_INDEX);
    }

    // 괄호가 닫히지 않았을때, 오류 발생
    if(LC > RC) {
        SyntaxError(line_number + 1,
            CLOSE_LEFT_BRACKET_TITLE,
            CLOSE_LEFT_BRACKET_MESSAGE,
            before_code,
            "(",
            CLOSE_LEFT_BRACKET_SUGGESTION_CONTENT,
            CLOSE_LEFT_BRACKET_INDEX);
    }

    return LC == RC && LC >= 1;
}

/* PRINT_TOKEN : 출력문 토큰 확인 */
string PRINT_TOKEN(string line) {
    regex pattern("\"([^\"]*)\"|'([^']*)'|말|보여|출력");
    smatch matches;
    string result;

    auto it = line.cbegin();

    while (regex_search(it, line.cend(), matches, pattern)) {
        const string match = matches[0];
        result += matches.prefix();

        if (match == "말" || match == "보여" || match == "출력")
            result += "<-print->";
        else
            result += matches[0];
        it = matches[0].second;
    }

    result += string(it, line.cend());
    return result;
}


/* IS_VARIABLE : 변수를 생성하는데, 오류를 검사하고, 처리합니다. */
string IS_VARIABLE(string line) {

    // 변수에 대입 중인지 확인
    if(line.contains("<-assignment_operator->")) {

        // 대입 연산을 중복으로 사용하면, 오류 처리
        vector<string> T = Split(line, "<-assignment_operator->");
         if(T.size() > 2) {
            SyntaxError(line_number + 1,
                    VARIABLE_OPERATOR_OVERFLOW_TITLE,
                    VARIABLE_OPERATOR_OVERFLOW_MESSAGE,
                    before_code,
                    "EOW",
                    VARIABLE_OPERATOR_OVERFLOW_SUGGESTION_CONTENT,
                    VARIABLE_OPERATOR_OVERFLOW_INDEX);
         }

        // 문장 보정자가 있다면, 처리하기
        line = VARIABLE_SUPPORT(line);

        // 문장 보정자를 중복으로 쓰면 오류 처리
        T = Split(line, "<-variable_support->");
        if(T.size() > 2) {
            SyntaxError(line_number + 1,
                    VARIABLE_DEFINE_OVERFLOW_TITLE,
                    VARIABLE_DEFINE_OVERFLOW_MESSAGE,
                    before_code,
                    "EOW",
                    VARIABLE_DEFINE_OVERFLOW_SUGGESTION_CONTENT,
                    VARIABLE_DEFINE_OVERFLOW_INDEX);
        }

        // 문장 보정자가 문자열 끝에 도달하지 않으면, 오류 처리
        string T_Token = "-variable_support->";
        T[1].replace(T[1].find(T_Token), T_Token.length(), "");
        T[1] = BlankRemove(T[1]);
 
        if(T[1] > 0) {
            SyntaxError(line_number + 1,
                    VARIABLE_SUPPORT_POSERR_TITLE,
                    VARIABLE_SUPPORT_POSERR_MESSAGE,
                    before_code,
                    "EOW",
                    VARIABLE_SUPPORT_POSERR_SUGGESTION_CONTENT,
                    VARIABLE_SUPPORT_POSERR_INDEX);
        }

        // 문장 보정자 토큰 삭제
        line = REMOVE_SUPPORT_TOKEN(line);

        // 대입 연산자 토큰 삭제
        line = REMOVE_VARIABLE_TOKEN(line);
    }

    return line;
}

/* PRINT : 출력문인지, 검사하고 변경합니다. */
string PRINT(string line) {
    line = PARTICAL_TOKEN(line);
    line = VERB_TOKEN(line);
    line = PRINT_TOKEN(line);

    // 출력문이 아니면, 파서 종료
    if (!line.contains("<-print->"))
        return line;

    // 조사가 없으면, 오류 발생
    if (!line.contains("<-particle->"))
        SyntaxError(line_number + 1,
                    NOT_CONTAIN_PARTICLE_TITLE,
                    NOT_CONTAIN_PARTICLE_MESSAGE,
                    before_code,
                    "M",
                    NOT_CONTAIN_PARTICLE_SUGGESTION_CONTENT,
                    NOT_CONTAIN_PARTICLE_INDEX);

    // 동사가 없으면, 오류 발생
    if (!line.contains("<-verb->"))
        SyntaxError(line_number + 1,
                    NOT_CONTAIN_VERB_TITLE,
                    NOT_CONTAIN_VERB_MESSAGE,
                    before_code,
                    "EOW",
                    NOT_CONTAIN_VERB_SUGGESTION_CONTENT,
                    NOT_CONTAIN_VERB_INDEX);

    // 문법에 맞는지 확인
    if (!Valid(line, "<-particle-><-print-><-verb->"))
        SyntaxError(line_number + 1,
                    UNVALID_SOV_TITLE,
                    UNVALID_SOV_MESSAGE,
                    before_code,
                    before_code,
                    UNVALID_SOV_SUGGESTION_CONTENT,
                    UNVALID_SOV_INDEX);

    // 조사를 기준으로 코드를 나눔
    vector<string> token_split = Split(line, "<-particle->");

    // 괄호가 있는지 검사
    string result;
    if (BRACKET(CODE_AREA_REMOVE(token_split[0]))) {
        result = "print" + CODE_AREA_REMOVE(token_split[0]);
    } else {
        result = "print(" + CODE_AREA_REMOVE(token_split[0]) + ")";

        SyntaxWarning(line_number + 1,
                        CONTAIN_BRACKET_TITLE,
                        CONTAIN_BRACKET_MESSAGE,
                        before_code,
                        "M");
    }

    return CODE_AREA_RETURN(token_split[0]) + result;
}

void execute_code(vector<string> execute_list) {

    // 파일 및 디렉토리 없을 시 생성
    if (!filecheck("bin/execute.py")) {
        bool iscreated = filesystem::create_directory("bin");
        if (!iscreated) {
            StandardError(0,
                            DIR_CREATE_ERROR_TITLE,
                            DIR_CREATE_ERROR_MESSAGE,
                            filesystem::current_path(),
                            filesystem::current_path(),
                            RECHECKING,
                            DIR_CREATE_ERROR_SUGGESTION_CONTENT,
                            DIR_CREATE_ERROR_INDEX);
        }
    }

    // 파일 경로 설정
    ofstream file("bin/execute.py");

    // 파일을 열어서 쓰고 Python 인터프리터로 실행
    if (file.is_open() || !file.fail()) {
        for (const string &line : execute_list)
            file << line << endl;
        file.close();

        system("python3 -d bin/execute.py");
    } else {
        StandardError(0,
                        FILE_WRITE_ERROR_TITLE,
                        FILE_WRITE_ERROR_MESSAGE,
                        filesystem::current_path(),
                        filesystem::current_path(),
                        RECHECKING,
                        FILE_WRITE_ERROR_SUGGESTION_CONTENT,
                        FILE_WRITE_ERROR_INDEX);
    }
}

/* parsing : 코드를 확인, 변경하고, 검사합니다. */
void parsing(const int index, string line, const bool shell = false) {

    // 주석 제거
    before_code = COMMENT(line);

    // 대입연산 조사 처리
    line = ASSIGNMENT_OPERATOR(before_code);

    // 대입연산 처리된 변수 확인
    line = IS_VARIABLE(line);

    // 코드 블럭 제거
    line = CODE_AREA(line);

    // 출력문 처리
    line = PRINT(line);

    // 쿼리스트링 처리
    line = QUERY_STRING(line);

    // 찌꺼기 탭 문자 변경
    // line = CODE_AREA_RETURN(line) + CODE_AREA_REMOVE(line);
    line = CODE_AREA_REMOVE(line);


    // 코드 변경
    if (!shell)
        codelist[index] = line;
    else {
        codelist.push_back(line);
        execute_code(codelist);
        codelist.clear();
    }
}

/* compile : 파일을 입력받아 컴파일합니다.*/
void compile(const string file_path, const string TARGET) {
    openfile(file_path, TARGET);

    for (const string &line : codelist) {
        parsing(line_number, line);
        line_number++;
    }

    execute_code(codelist);
}