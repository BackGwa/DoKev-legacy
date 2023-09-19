#pragma once

/* BlankRemove : 문자열의 모든 공백을 제거합니다. */
string BlankRemove(string code) {
    code.erase(std::remove_if(code.begin(), code.end(), ::isspace), code.end());
    return code;
}