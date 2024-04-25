#pragma once

/* OPERATOR : 연산자를 변경합니다. */
string OPERATOR(string line) {
    regex pattern("\"([^\"]*)\"|'([^']*)'|그리고|또는|반대로|증가해줘|감소해줘|증가|감소|더하기|빼기|곱하기|나누기");
    smatch matches;
    string result;

    auto it = line.cbegin();

    while (regex_search(it, line.cend(), matches, pattern)) {
        const string match = matches[0];
        result += matches.prefix();

        if (match == "그리고")
            result += "and";
        else if (match == "또는")
            result += "or";
        else if (match == "반대로")
            result += "not";
        else if (match == "증가해줘" || match == "증가")
            result += "+= 1";
        else if (match == "감소해줘" || match == "감소")
            result += "-= 1";
        else if (match == "더하기")
            result += "+";
        else if (match == "빼기")
            result += "-";
        else if (match == "곱하기")
            result += "*";
        else if (match == "나누기")
            result += "/";
        else
            result += matches[0];

        it = matches[0].second;
    }

    result += string(it, line.cend());
    return result;
}