import parsing

# import file
CodePath = input('dkv path >> ')

code = open(CodePath, 'r', encoding = 'utf-8')
lineArray = code.readlines()

lineValue = 0

# main
parsing(lineArray)