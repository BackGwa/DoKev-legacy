import code
import SyntaxError

# print parsing
def Print(codeValue):
    
    print(codeValue)
    pars = []
    
    if("'" in codeValue):
        pars = codeValue.split("'")
        print("STRING")
    elif('"' in codeValue):
        pars = codeValue.split('"')
        print("STRING")
    else:
        print("NONE STRING")
        return None

    return pars