#pragma once

/* WHILE_TREE : 반복문을 처리합니다. */
string WHILE_TREE(string line) {
    return "while (" + Split(line, "<-than-while->")[0] + "):";
}

/* LOOP : 반복문을 검사하고 변경합니다. */
string LOOP(string line, int line_number, string before_code) {
    regex pattern("\"([^\"]*)\"|'([^']*)'|인 동안|인동안|빠져나와|빠져나오자|빠져나와줘|계속해|계속해줘");
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
        } else if (match == "인동안") {
            SyntaxWarning(line_number + 1,
                        WHILE_SYNTAX_WARNING_TITLE,
                        WHILE_SYNTAX_WARNING_MESSAGE,
                        before_code,
                        "인동안");
            result += "<-than-while->";
            while_tree = true;
        } else if (match == "빠져나와" || mmatch == "빠져나오자" || match == "빠져나와줘") {
            result += "break";
        } else if (match == "계속해" || match == "계속해줘") {
            result += "continue";
        } else
            result += matches[0];

        it = matches[0].second;
    }

    result += string(it, line.cend());

    if (while_tree)
        result = WHILE_TREE(result);
    
    return result;
}
