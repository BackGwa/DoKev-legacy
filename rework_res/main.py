import parsing as pars

# import file
# CodePath = input('dkv path >> ')
CodePath = 'exam.dkv'
code = open(CodePath, 'r', encoding = 'utf-8')
lineArray = code.readlines()

#while(1):
#    line = input('CODE >> ')
#    pars.parsing(line)

linevalue = 0

for line in lineArray:
    
    linevalue += 1
    
    pars.parsing(line, linevalue)