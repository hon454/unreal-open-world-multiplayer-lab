# 멀티플레이 기반 준비

상태: 설정 준비. PIE 이동·복제 및 패키징 실행은 미검증.

- Server/Client 타깃은 기존 Game/Editor와 동일한 `BuildSettingsVersion.V7`, `EngineIncludeOrderVersion.Unreal5_8`을 사용한다.
- PIE 기본 설정은 Play As Client, 클라이언트 2개, 단일 프로세스, 별도 에디터 창이다. 기존 사용자 설정이 우선할 수 있다.
- 서버 기본 맵과 쿠킹 대상에 Third Person 맵을 지정했다. 전용 실험 맵은 후속 작업이다.
- 현재 작업 범위는 PIE다. 외부 실행 스크립트는 추가하지 않는다.
- Iris 활성화, 캐릭터 소유, 양방향 이동·점프 복제는 실행 근거 확보 전까지 미검증으로 남긴다.

[검증 절차](../Testing/MultiplayerBaseline.md)
