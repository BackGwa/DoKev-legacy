
#include <iostream>
using namespace std;

/* Windows 시스템 전용 */
#ifdef _WIN32
    #include <windows.h>
    HANDLE hConsole = GetStdHandle(STD_OUTPUT_HANDLE);
#endif

/* Unix 시스템 색상 정의 */
#define COLOR_RED       "\x1B[31m"
#define COLOR_GREEN     "\x1B[32m"
#define COLOR_YELLOW    "\x1B[33m"
#define COLOR_RESET     "\x1B[0m"

void RED() {
    #ifdef _WIN32
        SetConsoleTextAttribute(hConsole, FOREGROUND_RED);
    #endif
    cout << COLOR_RED;
}

void GREEN() {
    #ifdef _WIN32
        SetConsoleTextAttribute(hConsole, FOREGROUND_GREEN);
    #endif
    cout << COLOR_GREEN;
}

void YELLOW() {
    #ifdef _WIN32
        SetConsoleTextAttribute(hConsole, FOREGROUND_YELLOW);
    #endif
    cout << COLOR_YELLOW;
}

void RESET() {
    cout << COLOR_RESET;
}