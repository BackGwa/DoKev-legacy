import SyntaxError

# print parsing
def Print(codeValue, codeline):
    
    pars = []
    warntag = False
    wordcount = 0
    putif = False
    printif = False
    ReturnCode = ''
    ReturnText = ''
    EOFText = 0
    
    if('(' in codeValue):
        if(')' in codeValue):
    
            if(not("('" in codeValue or "')" in codeValue)):
                SyntaxError.err(codeline,codeValue,'GRMR')
                return None
            elif(not("')" in codeValue or '")' in codeValue)):
                SyntaxError.err(codeline,codeValue,'GRMR')
                return None

            else:
                if("'" in codeValue):
                    pars = codeValue.split("'")
                
                elif('"' in codeValue):
                    pars = codeValue.split('"')
                
        else:
            SyntaxError.err(codeline,codeValue,'GRMR')
            return None
        
    else:
        if('말해줘' in codeValue):
            SyntaxError.err(codeline,codeValue,'STRN')
            return None
        else:
            return None

    for word in pars:
        wordcount += 1
        if('라' in word or '라고' in word):
            putif = True
            EOFText = wordcount - 1
            pars[EOFText] = pars[EOFText].replace('라', '')
            pars[EOFText] = pars[EOFText].replace('고', '')
            
        if('말해줘' in word):
            printif = True
            pars[EOFText] = pars[EOFText].replace('말해줘', '')
        
    if(putif):
        if(printif):
            ReturnCode = ('print'+ ''.join(pars))
        else:
            return None
    else:
        SyntaxError.err(codeline,codeValue,'GRMR')
        return None
        
    print(pars)
    
    print(ReturnCode)

    return None