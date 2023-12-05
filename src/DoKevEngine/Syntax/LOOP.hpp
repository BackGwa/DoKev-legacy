#pragma once

/* LOOP : 반복문을 검사하고 변경합니다. */
string LOOP(string line) {
    regex pattern("\"([^\"]*)\"|'([^']*)'|인 동안");
    smatch matches;
    string result;

    auto it = line.cbegin();
    bool while_tree = false;

    while (regex_search(it, line.cend(), matches, pattern)) {
        const string match = matches[0];
        result += matches.prefix();

        if (match == "인 동안") {
            result += "<-than-while->";
            while_tree = true;
        } else
            result += matches[0];

        it = matches[0].second;
    }

    result += string(it, line.cend());

    if (while_tree)
        result = "while (" + Split(result, "<-than-while->")[0] + "):";
    
    return result;
}
