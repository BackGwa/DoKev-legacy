#pragma once

#include <string>

/* ASSIGNMENT_OPERATOR : 대입연산 조사인지, 검사하고 변경합니다. */
string ASSIGNMENT_OPERATOR(string line) {
    regex pattern("\"([^\"]*)\"|'([^']*)'|은 |는 ");
    smatch matches;
    string result;

    auto it = line.cbegin();

    while (regex_search(it, line.cend(), matches, pattern)) {
        const string match = matches[0];
        result += matches.prefix();

        if (match == "은 " || match == "는 ")
            result += "<-assignment_operator->";
        else
            result += matches[0];

        it = matches[0].second;
    }

    result += string(it, line.cend());
    return result;
}

/* VARIABLE_SUPPORT : 변수를 생성할 때, 선택적인 문장 보정자를 토큰화합니다. */
string VARIABLE_SUPPORT(string line) {
    regex pattern("\"([^\"]*)\"|'([^']*)'|이야|야");
    smatch matches;
    string result;

    auto it = line.cbegin();

    while (regex_search(it, line.cend(), matches, pattern)) {
        const string match = matches[0];
        result += matches.prefix();

        if (match == "이야" || match == "야")
            result += "<-variable_support->";
        else
            result += matches[0];

        it = matches[0].second;
    }

    result += string(it, line.cend());
    return result;
}

/* REMOVE_VARIABLE_TOKEN : 변수의 대입연산자 토큰을 삭제합니다. */
string REMOVE_VARIABLE_TOKEN(string line) {
    regex pattern("\"([^\"]*)\"|'([^']*)'|<-assignment_operator->");
    smatch matches;
    string result;

    auto it = line.cbegin();

    while (regex_search(it, line.cend(), matches, pattern)) {
        const string match = matches[0];
        result += matches.prefix();

        if (match == "<-assignment_operator->")
            result += " = ";
        else
            result += matches[0];

        it = matches[0].second;
    }

    result += string(it, line.cend());
    return result;
}

/* REMOVE_SUPPORT_TOKEN : 변수의 문장 보정자 토큰을 삭제합니다. */
string REMOVE_SUPPORT_TOKEN(string line) {
    regex pattern("\"([^\"]*)\"|'([^']*)'|<-variable_support->");
    smatch matches;
    string result;

    auto it = line.cbegin();

    while (regex_search(it, line.cend(), matches, pattern)) {
        const string match = matches[0];
        result += matches.prefix();

        if (match == "<-variable_support->")
            result += "";
        else
            result += matches[0];

        it = matches[0].second;
    }

    result += string(it, line.cend());
    return result;
}