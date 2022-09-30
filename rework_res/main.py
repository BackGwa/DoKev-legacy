import parsing as pars

# import file
CodePath = input('dkv path >> ')
code = open(CodePath, 'r', encoding = 'utf-8')
lineArray = code.readlines()

pars.parsing(lineArray)