#pragma once

/* PARTICAL : 조사인지, 검사하고 변경합니다. */
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