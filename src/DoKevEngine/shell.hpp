
#include "compiler.hpp"
#include "debugger.hpp"
#include "langpack.hpp"

using namespace std;

/* Shell_open : 쉘 인터페이스 환경을 엽니다. */ 
void Shell_open() {
  string command = "";

  cout << "DoKev" << VERSION << " - " << BUILD << endl << endl;

  while(true) {
    cout << ">>> ";
    cin >> command;
    cout << command;
  }
}