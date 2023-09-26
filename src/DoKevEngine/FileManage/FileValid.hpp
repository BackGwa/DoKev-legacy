#pragma once

#ifdef _WIN32
#include <io.h>
#else
#include <unistd.h>
#endif

/* FileValid : 파일이 유효한지, 확인합니다. */
bool FileValid(const string& path) {
#ifdef _WIN32
    return (_access(path.c_str(), 0) == 0);
#else
    return (access(path.c_str(), F_OK) == 0);
#endif
}
