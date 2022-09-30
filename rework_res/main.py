# import file

CodePath = input('dkv path >> ')

code = open(CodePath, 'r', encoding = 'utf-8')
lineArray = code.readlines()

lineValue = 0

def Print(codeValue):
    
    pars = []
    
    if(codeValue in "'"):
        pars = codeValue.split("'")
        
    elif(codeValue in '"'):
        pars = codeValue.split('"')
    else:
        return None

def parsing():
    for Nowline in lineArray:
        print('')

def warn(line, Nowline, warnmsg):
    
    warnReturn = ''
    
    if(warnmsg == 'GRMR'):
        warnReturn = "문법에 문제가 발생했지만, 코드 실행에는 큰 문제를 가져다주지 않습니다."
    elif(warnmsg == 'PUT'):
        warnReturn = "'을/를' 혹은 '은/는'등 다소 어색한 문법이 존재합니다.\n이는 수정할 필요는 없지만, 되도록 수정하는 것이 좋습니다."
    elif(warnmsg == 'VOT'):
        warnReturn = "이러한 방식의 형변환을 거치게되면 값의 일부가 손실될 수 있습니다."
    else:
        warnReturn = warnmsg
    
    msg = f'{line} : {warnReturn}\n>> {Nowline}'
    
    return msg

def err(line, Nowline, errmsg):
    
    errReturn = ''
    
    if(errmsg == 'GRMR'):
        errReturn = "사용할 수 없는 문법이거나 존재하지 않는 함수입니다."
    elif(errmsg == 'STRN'):
        errReturn = "문자열을 인식하는데 문제가 발생했습니다."
    else:
        errReturn = errmsg
    
    msg = f'{line} : {errReturn}\n>> {Nowline}'
    
    return msg