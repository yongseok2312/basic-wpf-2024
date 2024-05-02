# WPF 윈폼 개발 학습
IoT 개발자 WPF 학습리포지토리

## 1일차
- WPF(Window Presentation Foundation) 기본학습
    - Winforms 확장한 WPF
        - 이전의 Winforms는 이미지 비트맵방식(2D)
        - WPF 이미지 벡터방식
        - XAML 화면 디자인 - 안드로이드 개발시 Java XML로 화면디자인과 PyQt로 디자인과 동일

    - XAML(엑스에이엠엘,재몰) 
        - 여는 태그 <Window>, 닫는 태그 </Window>
        - 합치면 <Window/> - Window 태그 안에 다른 객체가 없단 뜻   
        - 여는 태그와 닫는 태그사이에 다른 태그(객체)를 넣어서 디자인

    - WPF 기본 사용법
        - windforms와는 다르게 코딩으로 디자인함

    - 레이아웃
        - Canvas - 미술에서 컨버스와 거의 유사
        - StackPanel - 스택으로 컨트롤을 쌓는 레이아웃
        - DockPanel - 컨트롤을 방향에 따라서 도킹시키는 레이아웃
        - Grid - WPF에서 가장 많이 사용하는 대표적인 레이아웃
        - Margin - 여백기능, 앵커링 기능 같이함


    - 디자인 코딩방법
    - 디자인, C#코드 완전 분리 개발 - MVVM 디자인패턴

## 2일차
- WPF 기본학습
    - 데이터 바인딩 - 데이터소스(DB, 엑셀, txt, 클라우드에 보관된 데이터원본)에 데이터를 쉽게 가져다쓰기 위해 데이터 핸들링 방법
        - xaml : {Binding Path = 속성, ElementName= 객체, Mode=(Oneway|Twoway),stringFormat{}{0:#,#}}
        - dataContext : 데이터를 담아서 전달하는 이름
        - 전통적인 윈폼 코드비하인드에서 데이터를 처리하는 것을 지양하기 위해서 - 디자인, 개발 부분 분리

## 3일차
- WPF에서 중요한 개념 (윈폼과 다른점)
    1. 데이터 바인딩 - 바인딩 키워드로 코드와 분리
    2. 옵저버 패턴 - 값이 변경된 사실을 사용자에게 분리
    3. 디자인 리소스 - 각 컨트롤마다 디자인(X), 리소스로 디자인 공유
        - 각 화면당 Resource - 자기 화면에만 적용되는 디자인
        - App.xaml Resource - 애플리케이션 전체에 적용되는 디자인
        - 리소스사전 - 공유할 디자인 내용이 많을 때 파일로 따로 지정
- WPF 기본학습
    - 데이터바인딩 마무리
    - 디자인 리소스
- WPF MVVM
