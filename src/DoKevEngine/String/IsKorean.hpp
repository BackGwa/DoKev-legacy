#pragma once

/* IsKorean : 입력 된 문자열이 한글인지 아닌지 확인합니다. */
bool IsKorean(const string& str) {
    regex korean("[가-힣]+");
    return regex_search(str, korean);
}