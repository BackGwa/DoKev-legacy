
#include "compiler.hpp"
#include "debugger.hpp"
#include "langpack.hpp"
#include "shell_interpreter.hpp"

using namespace std;

/* Shell_open : 쉘 인터페이스 환경을 엽니다. */ 
void Shell_open() {
  char command[128];
  cout << "DoKev" << VERSION << " - " << BUILD << endl << endl;

  while(true) {
    cout << ">>> ";
    cin.getline(command, 128);

    command_check(command);
  }
}