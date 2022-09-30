import code
import SyntaxError

# print parsing
def Print(codeValue):
    
    print(codeValue)
    pars = []
    
    if(codeValue in "'"):
        pars = codeValue.split("'")
        print("STRING")
    elif(codeValue in '"'):
        pars = codeValue.split('"')
        print("STRING")
    else:
        print("NONE STRING")
        return None

    return pars