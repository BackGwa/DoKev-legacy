
#include <cstdlib>
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
    cout << BOLD << "⟩⟩⟩ " << RESET;
    cin.getline(command, 128);

    if(!command_check(command)) {
      if(system(command))
        StandardError(0,
          UNKNOWN_COMMAND_TITLE,
          UNKNOWN_COMMAND_MESSAGE,
          command,
          command,
          RECHECKING,
          UNKNOWN_COMMAND_SUGGESTION_CONTENT,
          UNKNOWN_COMMAND_INDEX,
          false);
    }
  }
}