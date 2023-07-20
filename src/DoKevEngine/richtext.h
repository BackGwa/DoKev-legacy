
#include <iostream>
using namespace std;

/* 색상 정의 */
#define COLOR_RED     "\x1B[31m"
#define COLOR_GREEN   "\x1B[32m"
#define COLOR_YELLOW  "\x1B[33m"
#define COLOR_RESET   "\x1B[0m"

/* 색상 변경 함수 */
void RED()    { cout << COLOR_RED; }
void GREEN()  { cout << COLOR_GREEN; }
void YELLOW() { cout << COLOR_YELLOW; }
void RESET()  { cout << COLOR_RESET; }