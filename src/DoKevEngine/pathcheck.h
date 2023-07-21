
#include <iostream>
#include <unistd.h>

using namespace std;

bool filecheck(string path) {
    if(access(path.c_str(), F_OK) == 0){
        return true;
    }else{
        return false;
    }
}