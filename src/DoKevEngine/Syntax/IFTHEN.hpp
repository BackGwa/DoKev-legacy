#pragma once


/* IF_TARGRT : 조건문의 값 대상을 토큰화합니다. */
string IF_TARGET(string line) {
    regex pattern("\"([^\"]*)\"|'([^']*)'|랑 |가 |보다 |와 |이 ");
    smatch matches;
    string result;

    auto it = line.cbegin();

    while (regex_search(it, line.cend(), matches, pattern)) {
        const string match = matches[0];
        result += matches.prefix();

        if (match == "랑 ")
            result += "<-first-value->";
        else if (match == "보다 " || match == "와 ")
            result += "<-first-value->";
        else if (match == "이 " || match == "가 ")
            result += "<-second-value->";
        else
            result += matches[0];

        it = matches[0].second;
    }

    result += string(it, line.cend());

    return result;
}

/* IF_NORMAL : 조건문을 토큰화합니다. */
string IF_NORMAL(string line) {
    regex pattern("\"([^\"]*)\"|'([^']*)'|크다면|작다면|같다면|같지않다면");
    smatch matches;
    string result;

    auto it = line.cbegin();

    while (regex_search(it, line.cend(), matches, pattern)) {
        const string match = matches[0];
        result += matches.prefix();

        if (match == "크다면")
            result += "<-big-then->";
        else if (match == "작다면")
            result += "<-small-then->";
        else if (match == "같다면")
            result += "<-equal-then->";
        else if (match == "같지않다면")
            result += "<-not-equal-then->";
        else
            result += matches[0];

        it = matches[0].second;
    }

    result += string(it, line.cend());

    if (result != line)
        result = IF_TARGET(result);
    
    return result;
}

/* IF_OR : 조건문의 OR 조건을 토큰화합니다. */
string IF_OR(string line) {
    regex pattern("\"([^\"]*)\"|'([^']*)'|크거나|작거나|같거나|같지않거나");
    smatch matches;
    string result;

    auto it = line.cbegin();

    while (regex_search(it, line.cend(), matches, pattern)) {
        const string match = matches[0];
        result += matches.prefix();

        if (match == "크거나")
            result += "<-big-or->";
        else if (match == "작거나")
            result += "<-small-or->";
        else if (match == "같거나")
            result += "<-equal-or->";
        else if (match == "같지않거나")
            result += "<-not-equal-or->";
        else
            result += matches[0];

        it = matches[0].second;
    }

    result += string(it, line.cend());
    return result;
}

/* IF_AND : 조건문의 AND 조건을 토큰화합니다. */
string IF_AND(string line) {
    regex pattern("\"([^\"]*)\"|'([^']*)'|크면서|작으면서|같으면서|같지않으면서");
    smatch matches;
    string result;

    auto it = line.cbegin();

    while (regex_search(it, line.cend(), matches, pattern)) {
        const string match = matches[0];
        result += matches.prefix();

        if (match == "크면서")
            result += "<-big-and->";
        else if (match == "작으면서")
            result += "<-small-and->";
        else if (match == "같으면서")
            result += "<-equal-and->";
        else if (match == "같지않으면서")
            result += "<-not-equal-and->";
        else
            result += matches[0];

        it = matches[0].second;
    }

    result += string(it, line.cend());
    return result;
}

/* IFTHEN : 조건문을 검사하고 변경합니다. */
string IFTHEN(string line) {
    string cache = line;

    cache = IF_NORMAL(cache);
    cache = IF_AND(cache);
    cache = IF_OR(cache);
    cout << GREEN << IF_OR(cache) << RESET << endl;

    return line;
}