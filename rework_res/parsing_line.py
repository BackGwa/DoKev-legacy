import code
import SyntaxError

# print parsing
def Print(codeValue):
    
    pars = []
    
    if('(' in codeValue):
        if(')' in codeValue):
    
            if("'" in codeValue):
                pars = codeValue.split("'")
            
            elif('"' in codeValue):
                pars = codeValue.split('"')
                
        else:
            SyntaxError.err(0,codeValue,'GRMR')
        
    else:
        if(')' in codeValue):
            SyntaxError.err(0,codeValue,'GRMR')
        else:
            
            if("'" in codeValue):
                pars = codeValue.split("'")
                SyntaxError.warn(0,codeValue,'TXT')
            
            elif('"' in codeValue):
                pars = codeValue.split('"')
                SyntaxError.warn(0,codeValue,'TXT')
            
            else:
                return None

    for word in pars:
        print(word)

    return pars