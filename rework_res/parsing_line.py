import code
import SyntaxError

# print parsing
def Print(codeValue):
    
    pars = []
    
    if('(' in codeValue):
    
        if('()' in codeValue):
    
            if("'" in codeValue):
                pars = codeValue.split("'")
            
            elif('"' in codeValue):
                pars = codeValue.split('"')
                SyntaxError.warn(0,word,'TXT')
                
        else:
            SyntaxError.err(0,word,'GRMR')
        
    else:
        return None

    for word in pars:
        if(word in '('):
            if(word in ')'):
                print('PASS')
            else:
                SyntaxError.err(0,word,'GRMR')
        else:
            SyntaxError.warn(0,word,'TXT')

    return pars