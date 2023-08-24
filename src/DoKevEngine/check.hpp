#pragma once

#include <iostream>
#include <string>

#ifdef _WIN32
#include <io.h>
#else
#include <unistd.h>
#endif

using namespace std;

bool filecheck(string path) {
#ifdef _WIN32
    return (_access(path.c_str(), 0) == 0);
#else
    return (access(path.c_str(), F_OK) == 0);
#endif
}

bool blankcheck(string argument) {
    return argument.empty();
}