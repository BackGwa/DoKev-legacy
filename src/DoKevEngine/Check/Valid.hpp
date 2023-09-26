#pragma once

using namespace std;

/* Valid : 문자열의 구조가 같은지 비교합니다. */
bool Valid(const string line, const string syntax) {
    return line.find(syntax) != string::npos;
}