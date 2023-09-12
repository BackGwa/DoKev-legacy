
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

    string str_command(command);

    // 쉘 인터페이스 exit() 방지
    if(str_command.contains("exit()")) exit(0);
    
    parsing(line, command, true);
    line++;
  }
}