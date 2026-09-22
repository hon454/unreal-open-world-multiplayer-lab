# 아키텍처

상태: 설계. 현재 검증 범위는 PIE다. 검증 절차와 결과는 Docs/Testing 및 Docs/Results에 보관한다.

- [접속 흐름](ConnectionFlow.md): 로컬/EOS 책임 분리와 인증 경계.
- [복제와 이동](ReplicationAndMovement.md): Iris와 이동·애니메이션 실험 방향.
- [검증 기준](../Testing/MultiplayerBaseline.md) · [개발 환경](../Environment.md) · [검증 결과](../Results/README.md)

## 현재 범위

- PIE 서버 1개·클라이언트 2개에서 각 캐릭터의 소유와 양방향 이동·점프 복제를 확인한다. 현재 미검증이다.
- 외부 프로세스 실행 스크립트·패키징 검증은 후속 단계로 미룬다.
- 템플릿 맵은 초기 PIE 확인에 사용한다. 부하·거리별 복제 실험 전에 전용 맵을 구성하며, 구체적인 배치와 World Partition 도입 시점은 미정이다.

## 후속 테스트 제어 계획

UMG → 소유 PlayerController의 Server RPC → 서버 테스트 관리자 → 결과 복제.

- 전역 실험은 테스트 운영자만 제어하며, 서버가 요청 수량·빈도를 검증한다.
- UI와 자동 A/B 실행은 같은 제어 로직을 사용한다.
- 요청값과 서버 적용값을 구분하고, 서버·클라이언트 트레이스를 공통 Run ID로 연결한다.

구현을 구체화할 때 별도 문서로 분리한다.
