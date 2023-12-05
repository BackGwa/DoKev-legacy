#pragma once

/* WHILE_TREE : 반복문을 처리합니다. */
string WHILE_TREE(string line) {
    return "while (" + Split(line, "<-than-while->")[0] + "):";
}

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
        result = WHILE_TREE(result);
    
    return result;
}
