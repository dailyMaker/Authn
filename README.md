# Authn
ASP.NET Auth
![화면 캡처 2021-08-16 204241](https://user-images.githubusercontent.com/42315866/129558398-ea8fa3bc-e220-4f25-a72a-9019f0b047df.jpg)

## 구글 GCP와 연동하여 로그인
(진행순서) 1.OAuth동의화면 -&gt; 2.범위 -&gt; 3.테스트 사용자 -&gt; 4.요약

GCP에서 새프로젝트 만들기

사용자 인증 정보  > 사용자 사용자 인증 정보 만들기 선택

OAuth 클라이언트 ID 선택
  앱이름, 사용자지원이메일, 개발자 이메일 입력

범위
  범위 추가 또는 삭제 선택
  /auth/userinfo.email 체크
  /auth/userinfo.profile 체크
  /openid 체크

테스트 사용자

요약
##
사용자 인증 정보  > OAuth 클라이언트 ID 만들기

  어플리케이션유형: 웹애플리케이션 선택

  이름: 웹클라이언트 1

  승인된리다이렉션URI: https://localhost:5001/auth

생성된 OAuth클라이언트 ID와 보안 비밀번호 복사


