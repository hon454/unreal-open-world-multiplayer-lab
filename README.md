# Unreal Open World Multiplayer Lab

UE5 Third Person 템플릿을 기반으로, Iris Dedicated Server의 동작과 성능을 재현 가능한 실험으로 검증하는 포트폴리오 프로젝트입니다.

## 시작하기

1. [프로젝트 시작 가이드](Docs/GettingStarted.md)와 [LFS 복원 절차](Docs/Storage.md)에 따라 프로젝트를 엽니다.
2. [개발 환경 기록](Docs/Environment.md)에 정확한 엔진 버전과 빌드 환경을 기록합니다.
3. 첫 목표는 **패키징된 서버 1개 + 실제 클라이언트 2개 + 액터 부하 버튼 + Insights 기록**입니다.

로컬 저장소: `G:\unreal-open-world-multiplayer-lab`

프로젝트: 저장소 루트의 `OpenWorldMultiLab.uproject`

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
OpenWorldMultiLab.uproject
Source/             # C++ 코드
Config/             # 프로젝트 설정
Content/            # 자산 (Gitea LFS)
Docs/               # 환경, 구조, 실험 절차, 분석 결과
Scripts/            # 추후 빌드·실행·수집 스크립트
Artifacts/          # 로컬 트레이스·로그·출력 (Git 제외)
Builds/             # 패키징 출력 (Git 제외)
```

[설계](Docs/Architecture.md) · [측정 결과 작성 규칙](Docs/Results/README.md) · [실험 문서 템플릿](Docs/Experiments/TEMPLATE.md)

## 비용 제약을 고려한 자산 저장 구조

Unreal 바이너리 자산의 버전과 다운로드가 누적될 때 발생하는 GitHub LFS 비용 부담을 줄이기 위해, 기존 NAS의 Gitea를 별도 LFS 서버로 구성했습니다. GitHub에는 코드·문서·커밋 이력과 작은 LFS 포인터를 유지하고 실제 자산은 NAS에 저장합니다.

| 역할 | 저장 위치 |
|---|---|
| C++·설정·README·LFS 포인터 | GitHub |
| `.uasset`·`.umap` 등 실제 바이너리 | NAS Gitea LFS |
| 빌드·캐시·원본 프로파일링 기록 | 로컬, Git 제외 |

`.gitattributes`로 자산을 분류하고 `.lfsconfig`로 업로드·다운로드 서버를 지정했습니다. 목적지 검사 스크립트와 독립된 캐시에서의 다운로드·SHA-256 비교로 설정을 검증합니다. 자산 저장소도 공개하여 별도 Gitea 계정 없이 다운로드할 수 있습니다. 업로드는 권한 있는 사용자만 수행하며, NAS의 저장·전송·백업 책임과 가용성 의존성도 문서화했습니다. 실제 금액 절감 효과는 아직 측정하지 않았습니다.

**검증 결과:** 753개 자산(약 134.4 MiB)을 Gitea에 저장하고, 빈 캐시에서 전부 복원해 크기·SHA-256 일치와 LFS 무결성을 확인했습니다. Git에는 자산 포인터 약 95.2 KiB가 저장됩니다.

[설계 결정·인증·복원·백업 절차](Docs/Storage.md) · [실제 검증 근거](Docs/Results/StorageValidation.md)

## Unreal MCP

에디터 작업에 활용할 수 있도록 Unreal MCP와 All Toolsets를 활성화하고, Codex 연결 설정을 추가했습니다.

## 버전 관리

- `.uasset`, `.umap` 등 바이너리 자산은 Git LFS로 관리하며 목적지는 NAS Gitea입니다.
- `.gitignore`는 [공식 UnrealEngine.gitignore](https://github.com/github/gitignore/blob/main/UnrealEngine.gitignore) 원본을 기본으로 하고, 하단에 프로젝트 추가 규칙을 분리했습니다. 공식 `Build`·`SourceArt`·`*_BuiltData.uasset` 제외 규칙도 유지합니다.
- `Binaries`, `Intermediate`, `Saved`, `DerivedDataCache`, `.sln`·`.slnx`, 원본 트레이스는 제외합니다.
- World Partition의 `__ExternalActors__`, `__ExternalObjects__` 자산도 커밋 대상입니다.
- 엔진 소스는 이 저장소 밖에 둡니다. GitHub 공개 범위와 별개로 서드파티 자산의 배포 조건을 따릅니다.
- `.vsconfig`는 개발 도구 재현을 위한 공유 파일로 추적하도록 하단 예외를 추가했습니다. `.slnx`·추가 IDE 생성물·로컬 결과·트레이스·환경 파일은 제외합니다.
- 자산 푸시 전 `Scripts/Test-LfsRouting.ps1`, `git status --short`, `git lfs status`로 목적지와 변경분을 확인합니다.

## 공식 참고 자료

- [Dedicated Server 구축](https://dev.epicgames.com/documentation/en-us/unreal-engine/setting-up-dedicated-servers-in-unreal-engine)
- [Iris 마이그레이션](https://dev.epicgames.com/documentation/unreal-engine/migrate-to-iris-in-unreal-engine)
- [네트워크 캐릭터 이동](https://dev.epicgames.com/documentation/unreal-engine/understanding-networked-movement-in-the-character-movement-component-for-unreal-engine)
- [Network Emulation](https://dev.epicgames.com/documentation/unreal-engine/using-network-emulation-in-unreal-engine)

문서는 프로젝트에 지정된 UE 5.8을 기준으로 확인하되, 정확한 패치와 소스 빌드 환경은 확정 후 기록합니다.
