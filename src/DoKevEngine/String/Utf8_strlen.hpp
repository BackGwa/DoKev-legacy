#pragma once

/* Utf8_strlen : 문자열 길이를 정상적으로 반환합니다. */
int Utf8_strlen(const string &str) {
    int len = 0;
    for (size_t i = 0; i < str.length(); ) {
        if ((str[i] & 0xC0) != 0x80) len++;
        i++;
    }
    return len;
}