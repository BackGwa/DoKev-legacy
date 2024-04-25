#pragma once

/* IsKorean : 입력 된 문자열이 한글인지 아닌지 확인합니다. */
bool IsKorean(const std::string& str) {
    static const std::regex korean("[가-힣]+");
    return std::regex_search(str, korean);
}