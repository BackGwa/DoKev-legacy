#pragma once

/* BOOL : 조사인지, 검사하고 변경합니다. */
string BOOL(string line) {
    regex pattern("\"([^\"]*)\"|'([^']*)'|참|거짓");
    smatch matches;
    string result;

    auto it = line.cbegin();

    while (regex_search(it, line.cend(), matches, pattern)) {
        const string match = matches[0];
        result += matches.prefix();

        if (match == "참")
            result += "True";
        else if (match == "거짓")
            result += "False";
        else
            result += matches[0];

        it = matches[0].second;
    }

    result += string(it, line.cend());
    return result;
}