import SyntaxError
import parsing_line as pars

linevalue = 0

# parsing  
def parsing(lineArray):
    
    value = ''
    
    for Nowline in lineArray:
        
        value = pars.Print(Nowline)
        if(value == None):
            print('')
        else:
            break:
        
    return value