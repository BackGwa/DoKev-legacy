import code
import SyntaxError

# print parsing
def Print(codeValue, codeline):
    
    pars = []
    warntag = False
    wordcount = 0
    putif = False
    printif = False
    ReturnCode = ''
    
    if('(' in codeValue):
        if(')' in codeValue):
    
            if("'" in codeValue):
                pars = codeValue.split("'")
            
            elif('"' in codeValue):
                pars = codeValue.split('"')
                
        else:
            SyntaxError.err(codeline,codeValue,'GRMR')
            return None
        
    else:
        if('말해줘' in codeValue or '보여줘' in codeValue):
            SyntaxError.err(codeline,codeValue,'STRN')
            return None
        else:
            return None

    for word in pars:
        wordcount += 1
        if('라' in word or '라고' in word):
            putif = True
        if('말해줘' in word or '보여줘' in word):
            printif = True
        
    if(putif):
        if(printif):
            ReturnCode = 'print' + '(' + pars[1] + ')'
        else:
            return None
    else:
        SyntaxError.err(codeline,codeValue,'GRMR')
        return None
        
    print(pars)
    
    print(ReturnCode)

    return None