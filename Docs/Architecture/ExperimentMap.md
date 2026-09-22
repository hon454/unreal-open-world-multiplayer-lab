# 실험 맵

맵: `/Game/Maps/L_MultiplayerTest`. Third Person 맵을 MCP로 복제한 뒤 지형을 재구성했다. 원본 템플릿은 보존한다.

전용 머티리얼 4개는 `/Game/Materials/MI_Lab_*`에 둔다.

## 배치와 제약

- 평지 240×120m: X -20~220m, Y -60~60m. 바닥 기준 Z=0.
- PlayerStart 2개: (0, -4, 1.1)m, (0, 4, 1.1)m. 모두 +X 방향. GameMode는 기존 BP_ThirdPersonGameMode를 재사용한다.
- 거리 구간: +X 방향 0~200m. 0·25·50·75·100·150·200m 표식. 표식은 위치 기준이며 Iris 필터 경계를 의미하지 않는다.
- 부하 구역: X 20~60m, Y 15~55m의 40×40m 빈 구역. 부하 생성기·제어 UI는 미구현이다.
- 이동 구역: Y=-30m에 높이 20·40·80·120cm 블록과 단높이 20cm 계단 5개.
- 템플릿의 World Partition 구조를 유지한다. 서버 스트리밍·스트리밍 아웃은 Disabled, 새 Lab 액터는 Is Spatially Loaded=false로 고정한다. 전역 Enable Streaming 변경은 하지 않았다.
- 에디터·게임·서버 기본 맵과 쿠킹 대상은 실험 맵으로 통일한다. 현재 실행 검증은 PIE만 대상으로 한다.
- NavMesh·AI 동선, 지형 스트리밍, Iris 필터 정책은 이 맵 구축 범위에 포함하지 않는다.

[검증 결과](../Results/ExperimentMap.md) · [멀티플레이 확인 절차](../Testing/MultiplayerBaseline.md)
