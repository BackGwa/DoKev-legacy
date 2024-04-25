#pragma once

/* CAST : 형변환인지, 검사하고 변경합니다. */
string CAST(string line) {
    regex pattern("\"([^\"]*)\"|'([^']*)'|정수형으로|실수형으로|문자열로");
    smatch matches;
    string result;

    auto it = line.cbegin();

    while (regex_search(it, line.cend(), matches, pattern)) {
        const string match = matches[0];
        result += matches.prefix();

        if (match == "정수형으로")
            result += "int";
        else if (match == "실수형으로")
            result += "float";
        else if (match == "문자열로")
            result += "str";
        else
            result += matches[0];

        it = matches[0].second;
    }

    result += string(it, line.cend());
    return result;
}