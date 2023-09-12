#pragma once

/* valid : 문자열의 구조가 같은지 비교합니다. */
bool valid(const string line, const string syntax) {
    return line.contains(syntax);
}