# warning syntaxError
def warn(line, Nowline, warnmsg):
    
    warnReturn = ''
    
    if(warnmsg == 'GRMR'):
        warnReturn = "문법에 문제가 발생했지만, 코드 실행에는 큰 문제를 가져다주지 않습니다."
    elif(warnmsg == 'PUT'):
        warnReturn = "'을/를' 혹은 '은/는'등 다소 어색한 문법이 존재합니다.\n이는 수정할 필요는 없지만, 되도록 수정하는 것이 좋습니다."
    elif(warnmsg == 'VOT'):
        warnReturn = "이러한 방식의 형변환을 거치게되면 값의 일부가 손실될 수 있습니다."
    elif(warnmsg == 'TXT'):
        warnReturn = "이러한 방식으로 텍스트를 작성하는 것은 오류가 발생할 수 있습니다.\n('문자열')... 방식으로 작성하는 것을 추천합니다."
    else:
        warnReturn = warnmsg
    
    msg = f'[{line}번 라인]\n{warnReturn}\n>> {Nowline}'
    
    print('\033[33m' + f'\n{msg}\n' + '\033[0m')
    
    return msg

# syntaxError
def err(line, Nowline, errmsg):
    
    errReturn = ''
    
    if(errmsg == 'GRMR'):
        errReturn = "사용할 수 없는 문법이거나 존재하지 않는 함수입니다."
    elif(errmsg == 'STRN'):
        errReturn = "문자열을 인식하는데 문제가 발생했습니다."
    else:
        errReturn = errmsg
    
    msg = f'[{line}번 라인]\n{errReturn}\n>> {Nowline}'
    
    print('\033[31m' + f'\n{msg}\n' + '\033[0m')
    
    return msg
