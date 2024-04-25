#pragma once

/* INPUT : 입력문인지, 검사하고 변경합니다. */
string INPUT(string line) {
    regex pattern("\"([^\"]*)\"|'([^']*)'|입력받아줘");
    smatch matches;
    string result;

    auto it = line.cbegin();

    while (regex_search(it, line.cend(), matches, pattern)) {
        const string match = matches[0];
        result += matches.prefix();

        if (match == "입력받아줘")
            result += "input";
        else
            result += matches[0];

        it = matches[0].second;
    }

    result += string(it, line.cend());
    return result;
}