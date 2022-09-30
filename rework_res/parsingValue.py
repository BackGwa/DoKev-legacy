import SyntaxError

# string
def STRING(codeValue, codeline):

    if("'" in codeValue or '"' in codeValue):
        if('"' in codeValue and "'" in codeValue):
            return None
            
        if(not('(' in codeValue and ')' in codeValue)):
            SyntaxError.err(codeline,codeValue,'TXT')
            return -1
        
    else:
        return None

# comment parsing
def COMMENT(codeValue, codeline):
    return None
    
# print parsing
def PRINT(codeValue, codeline):
    return None


# input parsing
def INPUT(codeValue, codeline):
    return None
