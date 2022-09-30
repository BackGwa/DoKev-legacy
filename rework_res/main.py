import SyntaxError
import parsing

# import file
CodePath = input('dkv path >> ')

code = open(CodePath, 'r', encoding = 'utf-8')
lineArray = code.readlines()

lineValue = 0

# print parsing
def Print(codeValue):
    
    pars = []
    
    if(codeValue in "'"):
        pars = codeValue.split("'")
        
    elif(codeValue in '"'):
        pars = codeValue.split('"')
    else:
        return None
    
    print(pars)

# main
parsing(lineArray)