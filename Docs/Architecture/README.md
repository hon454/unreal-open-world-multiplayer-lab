# 아키텍처

상태: 설계. 구현·실측 결과는 각 작업에서 별도 기록한다.

- [접속 흐름](ConnectionFlow.md): 로컬/EOS 책임 분리와 인증 경계.
- [복제와 이동](ReplicationAndMovement.md): Iris와 이동·애니메이션 실험 방향.
- [첫 구현·검증 기준](../Testing/MultiplayerBaseline.md): TPP 서버 1개·클라이언트 2개.
- [개발 환경](../Environment.md) · [검증 결과](../Results/README.md)

## 후속 테스트 제어 계획

UMG → 소유 PlayerController의 Server RPC → 서버 테스트 관리자 → 결과 복제.

- 전역 실험은 테스트 운영자만 제어하며, 서버가 요청 수량·빈도를 검증한다.
- UI와 자동 A/B 실행은 같은 제어 로직을 사용한다.
- 요청값과 서버 적용값을 구분하고, 서버·클라이언트 트레이스를 공통 Run ID로 연결한다.

구현을 구체화할 때 별도 문서로 분리한다.
