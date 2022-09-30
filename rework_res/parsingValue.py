import SyntaxError

stringType = ''

# string
def STRING(codeValue, codeline):

    global stringType

    if("'" in codeValue or '"' in codeValue):
        if(not('(' in codeValue and ')' in codeValue)):
            SyntaxError.err(codeline,codeValue,'TXT')
            return -1
        else:
            if('"' in codeValue):
                SyntaxError.warn(codeline,codeValue,'STR', len(codeValue), codeValue.find('"'))
                stringType = '"'
            else:
                stringType = "'"
            
            if('"' in codeValue and "'" in codeValue):
                SyntaxError.err(codeline,codeValue,'STR')
                return -1
        
    else:
        stringType = "{NON-STRING}"
        return None

# comment parsing
def COMMENT(codeValue, codeline):
    
    global stringType
    
    if('#' in codeValue):
        if(stringType == '{NON-STRING}'):
            strplt = codeValue.split('#', 1)
            RETURN_VALUE = codeValue.replace(f'#{strplt[1]}', '')
            return RETURN_VALUE
        else:
            strplt = codeValue.split(stringType)

    return None
    
# print parsing
def PRINT(codeValue, codeline):
    return None


# input parsing
def INPUT(codeValue, codeline):
    return None
