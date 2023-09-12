#pragma once

/* CODE_AREA : 하나의 코드 영역인지, 확인합니다. */
string CODE_AREA(string line) {
    regex pattern("\"([^\"]*)\"|'([^']*)'|    ");
    smatch matches;
    string result;

    auto it = line.cbegin();

    while (regex_search(it, line.cend(), matches, pattern)) {
        const string match = matches[0];
        result += matches.prefix();

        if (match == "    ")
            result += "<-codearea->";
        else
            result += matches[0];

        it = matches[0].second;
    }

    result += string(it, line.cend());
    return result;
}

/* CODE_AREA_RETURN : 코드 영역 재구현합니다. */
string CODE_AREA_RETURN(string line) {
    regex pattern("\"([^\"]*)\"|'([^']*)'|<-codearea->");
    smatch matches;
    string result;
    string add;

    auto it = line.cbegin();

    while (regex_search(it, line.cend(), matches, pattern)) {
        const string match = matches[0];
        result += matches.prefix();

        if (match == "<-codearea->")
            add += "    ";
        else
            result += matches[0];

        it = matches[0].second;
    }

    result += string(it, line.cend());
    return add;
}

/* CODE_AREA_REMOVE : 코드 영역을 제거합니다. */
string CODE_AREA_REMOVE(string line) {
    regex pattern("\"([^\"]*)\"|'([^']*)'|<-codearea->");
    smatch matches;
    string result;

    auto it = line.cbegin();

    while (regex_search(it, line.cend(), matches, pattern)) {
        const string match = matches[0];
        result += matches.prefix();

        if (match == "<-codearea->")
            result += "";
        else
            result += matches[0];

        it = matches[0].second;
    }

    result += string(it, line.cend());
    return result;
}