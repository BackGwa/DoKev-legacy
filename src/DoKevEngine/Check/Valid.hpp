#pragma once

/* Valid : 문자열의 구조가 같은지 비교합니다. */
bool Valid(const std::string& line, const std::string& syntax) {
    return line.find(syntax) != std::string::npos;
}