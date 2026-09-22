# 실험 맵 구축 확인

2026-09-22, 로컬 UE 에디터 / Unreal MCP. 기능 준비 확인이며 성능 측정 결과가 아니다.

- `/Game/Maps/L_MultiplayerTest`와 전용 머티리얼 4개를 에디터 도구로 생성·저장했다. 새 실험 액터는 53개다.
- 맵 단독 저장에서 누락된 외부 액터는 전체 dirty asset 저장으로 복구했다. 재로드 후 Lab 액터 53개, PlayerStart 2개, 거리 표식 7개, 점프 블록 4개의 유지 여부를 확인했다.
- PlayerStart 2개, 간격 800cm, Z=110cm를 조회했다.
- Z=500cm에서 아래로 추적해 두 스폰 바닥 498cm, 부하 구역 바닥 499cm, 거리 구간 끝 바닥 498cm 지점의 충돌을 확인했다.
- 뷰포트에서 전체 배치와 구역별 색상·거리 표식을 확인했다.
- 기존 에디터 사용자 설정은 Standalone 1인이었다. MCP로 Play As Client, 2인, 단일 프로세스를 적용했다.
- PIE에서 Dedicated Server 월드와 클라이언트 월드 2개가 생성되고 Join succeeded 2건을 확인했다. 두 클라이언트 창에서 캐릭터 스폰을 관찰했다.
- 로그에서 IsServerStreamingEnabled=0, IsServerStreamingOutEnabled=0을 확인했다. 전역 스트리밍 비활성화와 동일한 의미로 해석하지 않는다.

- 2026-09-22 사용자가 두 클라이언트 간 양방향 이동·점프 반영을 직접 확인했다.

미검증: 장애물 통과, 접속 종료·재접속, Iris 활성화·필터링, 패키징 실행, 성능 수치.

[맵 배치](../Architecture/ExperimentMap.md) · [사용자 확인 절차](../Testing/MultiplayerBaseline.md#사용자-조작-확인)
