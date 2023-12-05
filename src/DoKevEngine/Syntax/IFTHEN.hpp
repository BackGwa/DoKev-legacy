#pragma once

#include "../String/BlankRemove.hpp"

/* IF_TARGRT : 조건문의 값 대상을 토큰화합니다. */
string IF_TARGET(string line) {
    regex pattern("\"([^\"]*)\"|'([^']*)'|이랑 |랑 |가 |보다 |와 |과 |이 ");
    smatch matches;
    string result;

    auto it = line.cbegin();

    while (regex_search(it, line.cend(), matches, pattern)) {
        const string match = matches[0];
        result += matches.prefix();

        if (match == "이랑 " || match == "랑 ")
            result += "<-first-value->";
        else if (match == "보다 " || match == "와 " || match == "과 ")
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

/* IF_PARSING : 조건문을 파싱하여 변경합니다. */
string IF_PARSING(string line) {
    string result = "";
    
    vector<string> T = Split(line, "->");
    vector<string> R;
    vector<string> P;

    int condition_len = T.size();

    for (int arr_len = 0; condition_len > arr_len; arr_len++) {
        R = Split(T[arr_len], "<-");
        string value;

        if (arr_len != 0)
            value = ToMultiByte(Substring(ToUnicode(R[0]), 1, sizeof(ToUnicode(R[0]))));
        else
            value = R[0];

        string temp = value;
        BlankRemove(temp);

        if (!temp.empty())
            P.push_back(value);
        P.push_back(R[1]);
    }

    condition_len = P.size();

    for (int arr_len = 0; condition_len > arr_len; arr_len++) {
        string first = "";
        string second = "";
        string condition = "";
        
        if (P[arr_len] == "-first-value") {
            first = P[arr_len - 1];
            second = P[arr_len + 1];

            condition = P[arr_len + 3];

            if (condition == "-big-or")
                condition = first + " < " + second + " or ";

            else if (condition == "-small-or")
                condition = first + " > " + second + " or ";

            else if (condition == "-equal-or")
                condition = first + " == " + second + " or ";

            else if (condition == "-not-equal-or")
                condition = first + " != " + second + " or ";

            else if (condition == "-big-and")
                condition = first + " < " + second + " and ";

            else if (condition == "-small-and")
                condition = first + " > " + second + " and ";

            else if (condition == "-equal-and")
                condition = first + " == " + second + " and ";

            else if (condition == "-not-equal-and")
                condition = first + " != " + second + " and ";

            else if (condition == "-big-then")
                condition = first + " < " + second;

            else if (condition == "-small-then")
                condition = first + " > " + second;

            else if (condition == "-equal-then")
                condition = first + " == " + second;

            else if (condition == "-not-equal-then")
                condition = first + " != " + second;

            result += condition;
        }

    }
    
    return "if (" + result + "):";
}

string IF_TREE(string line) {
    const vector<string> T = Split(line, "<-then-if->");
    const string condition = T[0];
    
    return "if (" + condition + "):";
}

/* IFTHEN : 조건문을 검사하고 변경합니다. */
string IFTHEN(string line) {
    regex pattern("\"([^\"]*)\"|'([^']*)'|이라면|크다면|작다면|같다면|같지않다면|전부 아니라면|전부 아니면|그게 아니라면|그게 아니면");
    smatch matches;
    string result;

    auto it = line.cbegin();
    bool if_tree = false;
    bool else_tree = false;

    while (regex_search(it, line.cend(), matches, pattern)) {
        const string match = matches[0];
        result += matches.prefix();

        if (match == "이라면") {
            result += "<-then-if->";
            if_tree = true;
        }
        else if (match == "크다면")
            result += "<-big-then->";
        else if (match == "작다면")
            result += "<-small-then->";
        else if (match == "같다면")
            result += "<-equal-then->";
        else if (match == "같지않다면")
            result += "<-not-equal-then->";
        else if (match == "전부 아니라면" || match == "전부 아니면" || match == "그게 아니라면" || match == "그게 아니면") {
            result += "else:";
            else_tree = true;
        } else {
            result += matches[0];
        }

        it = matches[0].second;
    }

    result += string(it, line.cend());

    if (if_tree) {
        result = IF_TREE(result);
    } else if ((result != line) && !else_tree) {
        result = IF_TARGET(result);
        result = IF_AND(result);
        result = IF_OR(result);
        result = IF_PARSING(result);
    }
    
    return result;
}
