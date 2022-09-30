# warning syntaxError
def warn(line, Nowline, warnmsg):
    
    warnReturn = ''
    
    if(warnmsg == 'GRMR'):
        warnReturn = "문법에 문제가 발생했지만, 코드 실행에는 큰 문제를 가져다주지 않습니다."
    elif(warnmsg == 'PUT'):
        warnReturn = "'을/를' 혹은 '은/는'등 다소 어색한 문법이 존재합니다.\n이는 수정할 필요는 없지만, 되도록 수정하는 것이 좋습니다."
    elif(warnmsg == 'VOT'):
        warnReturn = "이러한 방식의 형변환을 거치게되면 값의 일부가 손실될 수 있습니다."
    elif(warnmsg == 'STR'):
        warnReturn = """"를 사용하여 문자열을 여닫지마세요. 현재까지는 큰 문제를 가져다주지 않지만 추후 문제가 발생할 수 있습니다."""
    else:
        warnReturn = warnmsg
    
    msg = f'[{line}번째 줄]\n========================================\n{warnReturn}\n>> {Nowline}'
    
    
    print('\033[33m' + f'\n{msg}\n' + '\033[0m')
    
    return msg

# syntaxError
def err(line, Nowline, errmsg):
    
    errReturn = ''
    
    if(errmsg == 'GRMR'):
        errReturn = "사용할 수 없는 문법이거나 존재하지 않는 함수입니다."
    elif(errmsg == 'STR'):
        errReturn = """"과 '을 혼용하여 사용하는 것은 허용되지 않습니다!"""
    elif(errmsg == 'TXT'):
        errReturn = "문자열이 괄호로 닫히지 못했습니다. ('문자열')... 와 같이 작성해주세요."
    else:
        errReturn = errmsg
    
    msg = f'[{line}번째 줄]\n========================================\n{errReturn}\n>>  {Nowline}'
    
    print('\033[31m' + f'{msg}\n' + '\033[0m')
    
    return msg

def returnErr(returnValue, type):
    
    if(type == 0):
        print('\n< 프로그램이 정상적으로 마무리 되었습니다! >\n')
    else: 
        print('\033[31m' + f'\n< 프로그램이 정상적으로 마무리되지 못했습니다! |   코드 : ({returnValue}) > \n' + '\033[0m')
    return 0