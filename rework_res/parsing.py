import parsingValue as parsing

def code(line, linevalue):
    
    RETURN_VALUE = parsing.PRINT(line, linevalue)
    
    if(RETURN_VALUE == None):
        RETURN_VALUE = parsing.INPUT(line, linevalue)
    
    return RETURN_VALUE