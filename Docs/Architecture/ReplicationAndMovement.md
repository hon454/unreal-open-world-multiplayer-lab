# 복제와 이동

상태: 설계 방향. Iris 활성화는 실행 시 별도 확인한다.

## 기본 선택

- Iris를 사용한다. Replication Graph는 범위에서 제외한다.
- 이동은 기존 CharacterMovementComponent를 활용한다.
- 첫 월드는 서버가 전체 로드한다. World Partition 스트리밍과 Iris 공간 필터링은 별도로 측정한다.
- 필터 범위 재진입 시 현재 상태 복원을 검증한다.
- Push Model과 런타임 정책 전환은 UE 5.8.2 지원 범위를 확인한 뒤 구체화한다.

## 후속 실험

- Sprint: 예측·서버 재현·보정 후 재시뮬레이션. 오류 재현 모드와 정상 기준을 분리한다.
- 지속 애니메이션: Action ID, Sequence ID, 서버 시작 시각, 재생 속도, 취소 상태로 표현하는 방안을 검토한다.
- Root Motion과 복잡한 전투는 후순위다.

[접속 경계](ConnectionFlow.md) · [실험 목록](../../README.md#실험-목록)
