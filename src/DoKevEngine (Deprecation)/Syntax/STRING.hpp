#pragma once

/* STRING : 문자열 리터럴을 변환합니다. */
string STRING(string line) {
    string result;

    result = regex_replace(line, regex("\\줄"), "n");
    result = regex_replace(result, regex("\\ㅈ"), "n");

    result = regex_replace(result, regex("\\탭"), "t");
    result = regex_replace(result, regex("\\ㅌ"), "t");

    result = regex_replace(result, regex("\\뒤"), "b");
    result = regex_replace(result, regex("\\ㄷ"), "b");

    result = regex_replace(result, regex("\\앞"), "r");
    result = regex_replace(result, regex("\\ㅇ"), "r");

    return result;
}