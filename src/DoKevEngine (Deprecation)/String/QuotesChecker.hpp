#pragma once

/* QuotesChecker : 정상적인, 문자열인지 검사합니다. */
bool QuotesChecker(const std::string& code) {
    int singleQuoteCount = 0;
    int doubleQuoteCount = 0;

    for (char c : code) {
        if (c == '\'')      singleQuoteCount++;
        else if (c == '"')  doubleQuoteCount++;
    }

    return (singleQuoteCount % 2 == 0) && (doubleQuoteCount % 2 == 0);
}