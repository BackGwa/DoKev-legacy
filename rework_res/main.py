import parsing

# import file
CodePath = input('dkv path >> ')
code = open(CodePath, 'r', encoding = 'utf-8')
lineArray = code.readlines()

# main
parsing(lineArray)