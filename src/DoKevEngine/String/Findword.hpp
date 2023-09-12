#pragma once

/* Findword : 전체 문자열에서 특정 문자열을 찾아 인덱스를 반환합니다. */
int Findword(const string code, const string word) {
    size_t found = ToUnicode(code).rfind(ToUnicode(word));
    if (found != wstring::npos) {
        return static_cast<int>(found);
    }
    return -1;
}