#pragma once

/* ToMultiByte : 입력받은 wstring 문자열을 string 문자열로 변환합니다. */
string ToMultiByte(const std::wstring& input) {
    // 로캘을 사용하여 wstring을 변환합니다.
    std::wstring_convert<std::codecvt_utf8_utf16<wchar_t>> converter;
    return converter.to_bytes(input);
}