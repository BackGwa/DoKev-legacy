#pragma once

#include <iostream>
#include <unistd.h>

using namespace std;

bool filecheck(string path) {
  return (access(path.c_str(), F_OK) == 0);
}

bool blankcheck(string argument) {
  return argument == "";
}