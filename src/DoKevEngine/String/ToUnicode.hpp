#pragma once
#include <codecvt>

/* ToUnicode : 입력받은 string 문자열을 wstring 문자열로 변환합니다. */
wstring ToUnicode(const string &str) {
    wstring_convert<codecvt_utf8<wchar_t>> converter;
    return converter.from_bytes(str);
}