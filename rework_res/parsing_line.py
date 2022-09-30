import SyntaxError

# print parsing
def Print(codeValue):
    
    pars = []
    
    if(codeValue in "'"):
        pars = codeValue.split("'")
        
    elif(codeValue in '"'):
        pars = codeValue.split('"')
    else:
        return None

    print(pars)