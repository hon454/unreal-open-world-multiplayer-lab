# Unreal Open World Multiplayer Lab

UE5 Third Person 템플릿을 기반으로, Iris Dedicated Server의 동작과 성능을 재현 가능한 실험으로 검증하는 포트폴리오 프로젝트입니다.

> 현재 상태: **저장소와 문서 골격 준비 완료 / Unreal 프로젝트 생성 전**.
> 아래 기능은 구현 계획입니다. 실행 파일, 서버 접속, Iris 활성화, 성능 개선은 아직 검증하지 않았습니다.

## 시작하기

1. [최초 프로젝트 생성 가이드](Docs/GettingStarted.md)를 따라 `UnrealOpenWorldLab` C++ Third Person 프로젝트를 생성합니다.
2. [개발 환경 기록](Docs/Environment.md)에 정확한 엔진 버전과 빌드 환경을 기록합니다.
3. 첫 목표는 **패키징된 서버 1개 + 실제 클라이언트 2개 + 액터 부하 버튼 + Insights 기록**입니다.

로컬 저장소: `G:\unreal-open-world-multiplayer-lab`

예정 프로젝트: `UnrealOpenWorldLab/UnrealOpenWorldLab.uproject`

## 무엇을 보여주는가

- 서버 권한에 따른 부하 생성과 상태 복제.
- 상시 UMG 테스트 패널로 실험 실행, 설정 적용 확인, 결과 기록.
- Iris 공간 필터링, Dormancy, Push Model, Prioritization의 효과와 한계.
- Sprint 예측·서버 보정과 애니메이션 진행 상태의 동기화.
- 작은 World Partition 필드에서 클라이언트 스트리밍과 복제 범위 재진입 검증.
- 동일 조건으로 반복한 측정 결과와 Unreal Insights 호출 경로 분석.

## 실험 목록

| 실험 | 목적 | 상태 | 실측 결과 |
|---|---|---|---|
| [01. 액터 부하](Docs/Experiments/01-ActorLoad/README.md) | 생성 순간과 유지·변경 비용 분리 | 계획 | 미측정 |
| [02. Dormancy](Docs/Experiments/02-Dormancy/README.md) | 변화가 드문 액터의 처리 비용 감소 | 계획 | 미측정 |
| [03. 공간 필터링](Docs/Experiments/03-SpatialFiltering/README.md) | 연결별 복제 대상 제한 | 계획 | 미측정 |
| [04. Sprint 예측](Docs/Experiments/04-SprintPrediction/README.md) | 이동 보정 원인 재현과 개선 | 계획 | 미측정 |
| [05. 애니메이션 동기화](Docs/Experiments/05-AnimationSync/README.md) | 중간 진입·반복·취소 상태 복원 | 계획 | 미측정 |
| [06. Push Model·우선순위](Docs/Experiments/06-IrisPolicies/README.md) | 변경 감지 비용과 전송 기회 배분 | 후속 계획 | 미측정 |

## 구조

```text
UnrealOpenWorldLab/  # 사용자가 Unreal Editor에서 생성할 프로젝트
Docs/               # 환경, 구조, 실험 절차, 분석 결과
Scripts/            # 추후 빌드·실행·수집 스크립트
Artifacts/          # 로컬 트레이스·로그·출력 (Git 제외)
Builds/             # 패키징 출력 (Git 제외)
```

[설계](Docs/Architecture.md) · [측정 결과 작성 규칙](Docs/Results/README.md) · [실험 문서 템플릿](Docs/Experiments/TEMPLATE.md)

## 버전 관리

- `.uasset`, `.umap` 등 바이너리 자산은 Git LFS로 관리합니다.
- `Source`, `Config`, `Content`, 필요한 `Build` 메타데이터와 플러그인 소스를 추적합니다.
- `Binaries`, `Intermediate`, `Saved`, `DerivedDataCache`, `.sln`, 원본 트레이스는 제외합니다.
- World Partition의 `__ExternalActors__`, `__ExternalObjects__` 자산도 커밋 대상입니다.
- 엔진 소스는 이 저장소 밖에 둡니다. GitHub 공개 범위와 별개로 서드파티 자산의 배포 조건을 따릅니다.
- 첫 커밋 전 `git status --short`와 `git lfs status`로 포함될 파일을 확인합니다.

## 공식 참고 자료

- [Dedicated Server 구축](https://dev.epicgames.com/documentation/en-us/unreal-engine/setting-up-dedicated-servers-in-unreal-engine)
- [Iris 마이그레이션](https://dev.epicgames.com/documentation/unreal-engine/migrate-to-iris-in-unreal-engine)
- [네트워크 캐릭터 이동](https://dev.epicgames.com/documentation/unreal-engine/understanding-networked-movement-in-the-character-movement-component-for-unreal-engine)
- [Network Emulation](https://dev.epicgames.com/documentation/unreal-engine/using-network-emulation-in-unreal-engine)

문서는 선택한 엔진 버전에 맞춰 확인합니다. 엔진 버전은 아직 확정하지 않았습니다.
