import parsing

CodePath = 'exam.dkv'
code = open(CodePath, 'r', encoding = 'utf-8')
lineArray = code.readlines()
linevalue = 0

for line in lineArray:
    linevalue += 1
    parsing.code(line, linevalue)