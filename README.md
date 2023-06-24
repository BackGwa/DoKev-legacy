<br>

<div align="center">
  <img src="./res/banner.png" width='200'>
</div>

<br>

---

<br>

## **DoKev 배우기**
다음 <a href = 'https://backgwa.notion.site/DoKev-46bc63939be74aa28d3247ed2ec8d415'>링크</a>에서 DoKev에 대한 거의 모든 내용을 볼 수 있어요!

<br>

---

## **패치노트**
> 현재 정식 릴리즈의 최신 버전은 '4.25.0' 입니다.<br>
하지만, 현재 소스 코드를 기반으로 하는 최신 버전은 'RC 6.20' 입니다.

<br>

* 버전 : RC 6.20
```
* 중첩 함수 오류를 수정하였습니다.
```

* 버전 : RC 6.19
```
* 난수, 운영체계 라이브러리에 대한 직접적인 예외처리가 필요하지 않습니다.
* 삼항연산자가 기존 조건문과 충돌하던 문제를 수정하였습니다.
```

* 버전 : Canary-Release 23051801
```
* 예외처리는 더 이상 필요하지 않으며, 제거되었습니다. [다만, 현재까지는 모듈이 지원되지 않음]
* 출력과 함수의 문법이 개선되었습니다.
* 로거는 삭제되었습니다.
* 대체어를 추가하였습니다.
* 다양한 버그를 수정하였습니다.
* Windows에서 내장 파이썬을 제거하였습니다. [Python이 설치되어 있어야합니다!]
```

* 버전 : Pre-Release 6
```
* 디버거 기능이 좀 더 개선되었습니다.
* 영어/일본어 언어팩이 비활성화 되었습니다.
* 요상한 버그를 잡아냈습니다!

! 해당 버전은 매우 불안정합니다 !
아직 한참 개발 중이며 프로그램이 불안정하고 성능이 현저히 떨어질 수 있습니다!
```

* 버전 : Pre-Release 5
```
* 간단한 오류를 알 수 있는 디버거가 추가되었습니다. [* 열심히 개발 중!]

! 해당 버전은 매우 불안정합니다 !
아직 한참 개발 중이며 프로그램이 불안정하고 성능이 현저히 떨어질 수 있습니다!
```

* 버전 : Pre-Release 4
```
* 안정성과 보안을 위해서 *.kev 파일이 제거되고 내장 되었습니다.
* 빌더 성능이 개선되었습니다.
* 예외처리 방식이 @""에서 $""으로 변경되었습니다.
* 라이브러리를 가져오는 방식이 변경되었습니다.
  :: 필요한 라이브러리는 random이야 -> 난수 모듈이 필요해
```

* 버전 : Pre-Release 3
```
* 이제부터 일본어 언어팩을 지원합니다.
* 폴더 유효성 확인 기능을 향상 시켰습니다.
* 리눅스 환경을 정식으로 지원합니다.
```

* 버전 : Pre-Release 2
```
* config 파일에서 인터프리터 설정이 가능합니다.
```

* 버전 : Pre-Release 1
```
* 현지화 기능이 추가되었습니다.
* 이제부터 빌드 후 log 폴더에 기록을 남깁니다.
* 입력이 불가능하던 버그를 수정하였습니다.
```

* 버전 : PRE 5.04
```
* 이제부터 드디어 macOS에서도 DoKev를 사용할 수 있습니다!!! [ARM 칩셋만 지원 / Python　필요]
* 이제 DoKev에서 무슨 일이 일어나는지 로그를 확인 할 수 있습니다. [추후 로그 저장 기능 지원 예정]
* 체감은 안되지만, 좀 더 빠릿빠릿해졌습니다.
* 코드가 안정화되었고, 먼지 가득한 곳에서 날아다니던 나방을 잡았습니다.
```

* 버전 : PRE 4.26
```
* 이제부터 32비트 운영체제에서도 DoKev를 사용할 수 있습니다!
* 이제부터 windows on ARM이 정식 지원됩니다.
* .NET Framework 기반의 기존 컴파일러를 .NET Core로 대체하였습니다.
* 컴파일러 성능이 대폭 개선되었습니다.
```

* 버전 : 4.25.1
```
* .do 확장자에서 *.dkv 확장자로 변경되었습니다.
* 컴파일 성능이 개선되었습니다.
* 컴파일러가 경량화 되었습니다.
```

* 버전 : 4.25.0
```
* Python 3.11.3으로 모든 파일이 업데이트 되었습니다.
* 라이선스가 BSD-3에서 MIT로 변경되었습니다.
```

* 버전 : 230424.03
```
* random 모듈에 일부 대체어를 개선하고 추가하였습니다.
* 반올림 함수를 추가하였습니다.
* 파일 관리 함수들을 추가하였습니다.
* 문자열 합치기 함수를 추가하였습니다.
```

* 버전 : 230424.02
```
* 삼항연산자를 추가하였습니다.
* 옵션에 대한 대체어를 추가하였습니다.
```

* 버전 : 230424.01
```
* 내장 함수를 추가하였습니다.
[range, upper, lower, append]
```


* 버전 : 230224.01
```
* 치명적인 논리 버그를 수정하였습니다.
```

* 버전 : 220927.01
```
* dkcv.exe 성능개선
```

* 버전 : 220926.01
```
* 소소한 변경사항
```

* 버전 : 220718.02
```
* random.kev 파일이 수정되었습니다.
  [버그 수정]
```

* 버전 : 220718.01
```
* dkcv.exe가 새로운 버전으로 업데이트 되었습니다.
  [성능 개선 및 알고리즘 개선]
```

* 버전 : 220717.01
```
* 문법의 개선을 위해 *.kev 파일이 수정되었습니다.
  이제부터 대부분의 말이 대치됩니다.
```

* 버전 : 220715.04
```
* 소소한 버그 수정
```

* 버전 : 220715.03
```
* os 라이브러리가 기본으로 적용됩니다.
* dkpl.exe가 제거되고 'python 3.10.5 embed'가 적용됩니다.
```

* 버전 : 220715.02
```
* dkcv.exe가 새로운 버전으로 업데이트 되었습니다.
* 라이브러리 지원을 위해 새로운 *.kev 파일이 추가되었습니다. [os 모듈 지원]
* 일부 라이브러리가 한국어로 많은 기능을 지원합니다. [random 모듈 확장]
* 일부 어색한 문법의 개선을 위해 *.kev 파일이 수정되었습니다.
* 이제부터 예제코드는 출력으로만 제공됩니다.
```

* 버전 : 220715.01
```
* dkpl.exe가 새로운 버전으로 업데이트 되었습니다.
* 문자를 지원합니다. [str]
```

* 버전 : 220714.06
```
* 라이브러리가 작동하지 않던 버그를 수정했습니다.
```

* 버전 : 220714.05
```
* 기존 파이썬 라이브러리 일부를 지원합니다. [ironPython.stdlib]
  현재 한글을 지원하는 라이브러리는 <random>입니다.

  그 외 라이브러리는 영문으로 지원합니다.


* dkpl.exe가새로운 버전으로 업데이트 되었습니다.
* 라이브러리 지원을 위해 새로운 *.kev 파일이 추가되었습니다.
* 일부 어색한 문법의 개선을 위해 *.kev 파일이 수정되었습니다.
* 예제가 수정되었습니다.
```

* 버전 : 220714.04
```
* dkcv.exe의 버그를 수정하였습니다.
```

* 버전 : 220714.03
```
* 일부 어색한 문법의 개선을 위해 *.kev 파일이 수정되었습니다.
```

* 버전 : 220714.02
```
* dkcv.exe의 UI가 변경되었습니다.
* dkcv.exe의 버그를 수정하였습니다.
```

* 버전 : 220714.01
```
* 문자열 예외처리 기능이 추가되었습니다.

  @"입력할 문자" 와같이 따옴표 앞에 @를 붙이시면 해당 문자열을 예외처리 됩니다.  
  예외 처리는 한국어 문자열 입력 시에만 해주시면 됩니다.


* dkcv.exe의 UI가 변경되었습니다.
```

* 버전 : 220713.02
```
* 번역 충돌을 방지하기 위해 *.kev 파일이 수정되었습니다.
* for문이 추가되었습니다.
* 기본 예제 코드를 수정하였습니다.
```

* 버전 : 220713.01
```
* 일부 어색한 문법의 개선을 위해 *.kev 파일이 수정되었습니다.
* 일부 자료형을 더 추가하였습니다.
* 제어문자를 추가하였습니다.
* 계산 관련 함수를 일부 더 추가하였습니다.
* 기본 예제 코드를 수정하였습니다.
```
<br>

---

<br>

## **커피 사주기**
토스뱅크 1908-8294-2426
