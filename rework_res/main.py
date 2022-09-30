import parsing

# import file
# CodePath = input('dkv path >> ')
CodePath = 'exam.dkv'
code = open(CodePath, 'r', encoding = 'utf-8')
lineArray = code.readlines()
linevalue = 0

#while(1):
#    line = input('CODE >> ')
#    pars.parsing(line)

for line in lineArray:
    linevalue += 1
    parsing.code(line, linevalue)