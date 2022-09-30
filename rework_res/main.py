import parsing
import SyntaxError

CodePath = 'exam.dkv'
code = open(CodePath, 'r', encoding = 'utf-8')
lineArray = code.readlines()
linevalue = 0

for line in lineArray:
    linevalue += 1
    result = parsing.code(line, linevalue)
    
    if(result != None and result < 0):
        break
    
if(result != None):
    if(result < 0):
        SyntaxError.returnErr(result, 1)    
else:
    SyntaxError.returnErr(result, 0)