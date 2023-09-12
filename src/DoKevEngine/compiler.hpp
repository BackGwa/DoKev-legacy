#pragma once

#include <cstdlib>
#include <fstream>
#include <vector>
#include <string>
#include <regex>
#include <sstream>
#include <filesystem>
#include "debugger.hpp"
#include "check.hpp"

using namespace std;

int line_number = 0;
vector<string> codelist;
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

/* split : 문자열을 나눕니다. */
vector<string> split(string s, string divid) {
	vector<string> v;
	int start = 0;
	int d = s.find(divid);
	while (d != -1){
		v.push_back(s.substr(start, d - start));
		start = d + 1;
		d = s.find(divid, start);
	} 
	v.push_back(s.substr(start, d - start));

	return v;
}

/* BRACKET : 괄호의 유무를 검사합니다. */
bool BRACKET(string line){
    regex pattern("\"[^\"]*\"|'[^']*'");

    sregex_iterator it(line.begin(), line.end(), pattern);
    sregex_iterator end;

    int LC = 0;
    int RC = 0;

    for (; it != end; ++it) {
        smatch match = *it;
        string matchedText = match.str();

        for (char c : matchedText) {
            if (c == '(') {
                LC++;
            } else if (c == ')') {
                RC++;
            }
        }
    }
    return LC == RC && LC >= 1;

}

    /* COMMENT : 주석을 제거합니다. */
    string COMMENT(string line)
    {
        regex pattern("\"([^\"]*)\"|'([^']*)'|#");
        smatch matches;
        string result;

        auto it = line.cbegin();

        while (regex_search(it, line.cend(), matches, pattern))
        {
            const string match = matches[0];
            result += matches.prefix();

            if (match == "#")
            {
                result += "";
                return result;
            }
            else
            {
                result += matches[0];
            }

            it = matches[0].second;
        }

        result += string(it, line.cend());
        return result;
    }

    /* PARTICAL : 조사인지, 검사하고 변경합니다. */
    string PARTICAL_TOKEN(string line)
    {
        regex pattern("\"([^\"]*)\"|'([^']*)'|이라고 |라고 |을 |를 ");
        smatch matches;
        string result;

        auto it = line.cbegin();

        while (regex_search(it, line.cend(), matches, pattern))
        {
            const string match = matches[0];
            result += matches.prefix();

            if (match == "이라고 " || match == "라고 " || match == "을 " || match == "를 ")
                result += "<-particle->";
            else
                result += matches[0];

            it = matches[0].second;
        }

        result += string(it, line.cend());
        return result;
    }

    /* CODE_AREA : 하나의 코드 영역인지, 확인합니다. */
    string CODE_AREA(string line)
    {
        regex pattern("\"([^\"]*)\"|'([^']*)'|    ");
        smatch matches;
        string result;

        auto it = line.cbegin();

        while (regex_search(it, line.cend(), matches, pattern))
        {
            const string match = matches[0];
            result += matches.prefix();

            if (match == "    ")
                result += "<-codearea->";
            else
                result += matches[0];

            it = matches[0].second;
        }

        result += string(it, line.cend());
        return result;
    }

    /* CODE_AREA_RETURN : 코드 영역 재구현합니다. */
    string CODE_AREA_RETURN(string line)
    {
        regex pattern("\"([^\"]*)\"|'([^']*)'|<-codearea->");
        smatch matches;
        string result;
        string add;

        auto it = line.cbegin();

        while (regex_search(it, line.cend(), matches, pattern))
        {
            const string match = matches[0];
            result += matches.prefix();

            if (match == "<-codearea->")
                add += "    ";
            else
                result += matches[0];

            it = matches[0].second;
        }

        result += string(it, line.cend());
        return add;
    }

    /* CODE_AREA_REMOVE : 코드 영역을 제거합니다. */
    string CODE_AREA_REMOVE(string line)
    {
        regex pattern("\"([^\"]*)\"|'([^']*)'|<-codearea->");
        smatch matches;
        string result;

        auto it = line.cbegin();

        while (regex_search(it, line.cend(), matches, pattern))
        {
            const string match = matches[0];
            result += matches.prefix();

            if (match == "<-codearea->")
                result += "";
            else
                result += matches[0];

            it = matches[0].second;
        }

        result += string(it, line.cend());
        return result;
    }

    /* VERB : 동사인지, 검사하고 변경합니다. */
    string VERB_TOKEN(string line)
    {
        regex pattern("\"([^\"]*)\"|'([^']*)'|해줘|줘|하고|해주고|주고|고|해");
        smatch matches;
        string result;

        auto it = line.cbegin();

        while (regex_search(it, line.cend(), matches, pattern))
        {
            const string match = matches[0];
            result += matches.prefix();

            if (match == "해줘" || match == "줘" || match == "하고" || match == "해주고" || match == "주고" || match == "고" || match == "해")
                result += "<-verb->";
            else
                result += matches[0];

            it = matches[0].second;
        }

        result += string(it, line.cend());
        return result;
    }

    string PRINT_TOKEN(string line)
    {
        regex pattern("\"([^\"]*)\"|'([^']*)'|말|보여|출력");
        smatch matches;
        string result;

        auto it = line.cbegin();

        while (regex_search(it, line.cend(), matches, pattern))
        {
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

    /* PRINT : 출력문인지, 검사하고 변경합니다. */
    string PRINT(string line)
    {
        line = PARTICAL_TOKEN(line);
        line = VERB_TOKEN(line);
        line = PRINT_TOKEN(line);

        // 출력문이 아니면, 파서 종료
        if (!line.contains("<-print->"))
        {
            return line;
        }

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

        // 조사를 기준으로 코드를 나눔
        vector<string> token_split = split(line, "<-particle->");

        // 괄호가 있는지 검사
        string result;
        if (BRACKET(CODE_AREA_REMOVE(token_split[0])))
        {
            result = "print" + CODE_AREA_REMOVE(token_split[0]);
        }
        else
        {
            result = "print(" + CODE_AREA_REMOVE(token_split[0]) + ")";

            SyntaxWarning(line_number + 1,
                          CONTAIN_BRACKET_TITLE,
                          CONTAIN_BRACKET_MESSAGE,
                          before_code,
                          "M");
        }

        return CODE_AREA_RETURN(token_split[0]) + result;
    }

    void execute_code(vector<string> execute_list)
    {

        // 파일 및 디렉토리 없을 시 생성
        if (!filecheck("bin/execute.py"))
        {
            bool iscreated = filesystem::create_directory("bin");
            if (!iscreated)
            {
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
        if (file.is_open() || !file.fail())
        {
            for (const string &line : execute_list)
                file << line << endl;
            file.close();

            system("python3 -d bin/execute.py");
        }
        else
        {
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
    void parsing(int index, string line, bool shell = false)
    {

        // 전처리를 한 후 코드 저장
        before_code = COMMENT(line);
        line = CODE_AREA(before_code);

        // 출력문 처리
        line = PRINT(line);

        // 찌꺼기 탭 문자 변경
        line = CODE_AREA_RETURN(line) + CODE_AREA_REMOVE(line);

        // 코드 변경
        if (!shell)
        {
            codelist[index] = line;
        }
        else
        {
            codelist.push_back(line);
            execute_code(codelist);
            codelist.clear();
        }
    }

    /* compile : 파일을 입력받아 컴파일합니다.*/
    void compile(string file_path, string TARGET, string MAKER)
    {
        openfile(file_path, TARGET, MAKER);

        for (const string &line : codelist)
        {
            parsing(line_number, line);
            line_number++;
        }

        execute_code(codelist);
    }