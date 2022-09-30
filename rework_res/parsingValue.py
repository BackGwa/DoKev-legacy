import SyntaxError

# string
def STRING(codeValue, codeline):

    if("'" in codeValue or '"' in codeValue):
        if(not('(' in codeValue and ')' in codeValue)):
            if(not('("' in codeValue and '")' in codeValue)) or (not("('" in codeValue and "')" in codeValue)):
                SyntaxError.err(codeline,codeValue,'TXT')
                return -1
            
            if('"' in codeValue and "'" in codeValue):
                SyntaxError.err(codeline,codeValue,'STR')
                return -1

    else:
        return 0

# comment parsing
def COMMENT(codeValue, codeline):
    return 0
    
# print parsing
def PRINT(codeValue, codeline):
    return 0


# input parsing
def INPUT(codeValue, codeline):
    return 0
