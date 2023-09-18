#pragma once

/* PARTICAL_TOKEN : 조사인지, 검사하고 변경합니다. */
string PARTICAL_TOKEN(string line) {
    regex pattern("\"([^\"]*)\"|'([^']*)'|이라고 |라고 |을 |를 ");
    smatch matches;
    string result;

    auto it = line.cbegin();

    while (regex_search(it, line.cend(), matches, pattern)) {
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
            result += " = ";
        else
            result += matches[0];

        it = matches[0].second;
    }

    result += string(it, line.cend());
    return result;
}