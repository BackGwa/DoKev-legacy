import SyntaxError
import parsing_line as pars

lineValue = 0

# parsing  
def parsing(lineArray):
    
    lineValue += 1
    
    for Nowline in lineArray:
        pars.Print(Nowline)