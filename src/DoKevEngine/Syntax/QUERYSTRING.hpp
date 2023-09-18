#pragma once

/* QUERY_STRING : 조사인지, 검사하고 변경합니다. */
string QUERY_STRING(string line) {
    regex pattern("\"([^\"]*)\"|'([^']*)'|\\$");
    smatch matches;
    string result;

    auto it = line.cbegin();

    while (regex_search(it, line.cend(), matches, pattern)) {
        const string match = matches[0];
        result += matches.prefix();

        if (match == "$")
            result += "f";
        else
            result += matches[0];

        it = matches[0].second;
    }

    result += string(it, line.cend());
    return result;
}
