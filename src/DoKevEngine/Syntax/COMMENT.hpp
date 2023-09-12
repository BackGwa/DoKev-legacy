#pragma once

/* COMMENT : 주석을 제거합니다. */
string COMMENT(string line) {
    regex pattern("\"([^\"]*)\"|'([^']*)'|#");
    smatch matches;
    string result;

    auto it = line.cbegin();

    while (regex_search(it, line.cend(), matches, pattern)) {
        const string match = matches[0];
        result += matches.prefix();

        if (match == "#") {
            result += "";
            return result;
        } else {
            result += matches[0];
        }

        it = matches[0].second;
    }

    result += string(it, line.cend());
    return result;
}