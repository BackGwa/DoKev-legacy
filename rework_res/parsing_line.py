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
        print("NON-STRING")
        return None
    
    for nwa in pars:
        print(nwa)

    return pars