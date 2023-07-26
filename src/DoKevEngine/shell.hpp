
#include "compiler.hpp"
#include "debugger.hpp"
#include "langpack.hpp"
#include "shell_interpreter.hpp"

using namespace std;

/* Shell_open : 쉘 인터페이스 환경을 엽니다. */ 
void Shell_open() {
  char command[128];
  cout << BOLD << "DoKev" << VERSION << " | " << SHELL_VERSION << BUILD << RESET << endl;
  cout << SHELL_MESSAGE << endl << endl;

  while(true) {
    cout << ">>> ";
    cin.getline(command, 128);

    if(!command_check(command)) {
      
    }
  }
}