
#include <cstdlib>
#include "compiler.hpp"
#include "debugger.hpp"
#include "langpack.hpp"

using namespace std;

/* Shell_open : 쉘 인터페이스 환경을 엽니다. */ 
void Shell_open() {
  int line = 1;
  char command[128];
  cout << BOLD << "DoKev" << VERSION << " | " << SHELL_VERSION << BUILD << RESET << endl;
  cout << SHELL_MESSAGE << endl << endl;

  while(true) {
    cout << BOLD << "⟩⟩⟩ " << RESET;
    cin.getline(command, 128);
    cout << parsing(line, command, true) << endl;
    line++;
  }
}