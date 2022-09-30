import parsingValue as parsing

def code(line, linevalue):
    
    EDITlinevalue = line
    print(EDITlinevalue)
    
    RETURN_VALUE = None
    
    if(RETURN_VALUE == None):
        RETURN_VALUE = parsing.STRING(EDITlinevalue, linevalue)
    
    if(RETURN_VALUE == None):
        COMMENT = parsing.COMMENT(EDITlinevalue, linevalue)
        if (COMMENT != None):
            EDITlinevalue = COMMENT
            RETURN_VALUE == None
        else:
            RETURN_VALUE == None
    
    if(RETURN_VALUE == None):
        RETURN_VALUE = parsing.PRINT(EDITlinevalue, linevalue)
    
    if(RETURN_VALUE == None):
        RETURN_VALUE = parsing.INPUT(EDITlinevalue, linevalue)
    
    return RETURN_VALUE