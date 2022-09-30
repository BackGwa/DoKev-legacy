import SyntaxError

# comment parsing
def COMMENT(codeValue, codeline):
    if('#' in codeValue):
        codeValue.split('#', 1)

# print parsing
def PRINT(codeValue, codeline):
    None

# input parsing
def INPUT(codeValue, codeline):
    None