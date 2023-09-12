#pragma once

/* Substring : 유니코드 문자를 자릅니다. */
wstring Substring(const wstring& str, size_t startIdx, size_t length) {
    if (startIdx < str.length()) {
        return str.substr(startIdx, length);
    } else {
        return L" ";
    }
}