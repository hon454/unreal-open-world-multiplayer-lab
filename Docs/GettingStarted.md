# 최초 프로젝트 생성

## 지금 준비된 것

GitHub 저장소, Unreal용 ignore, Git LFS 속성, 문서 구조만 준비되어 있습니다.
Unreal 프로젝트와 빌드 타깃, Iris 설정, 맵, 테스트 UI는 아직 없습니다.

## 1. 엔진과 개발 환경 선택

- 사용할 UE5 버전과 패치를 고정합니다. 소스 빌드라면 브랜치·커밋도 기록합니다.
- Dedicated Server 빌드 단계에서는 소스 빌드 엔진을 기준으로 진행합니다.
- 선택한 엔진 버전의 공식 지원 도구에 맞춰 Visual Studio C++ 게임 개발 도구와 Windows SDK를 준비합니다.
- 엔진 설치·소스는 저장소 밖에 둡니다.
- [Environment.md](Environment.md)의 미정 항목을 채웁니다.

## 2. Unreal Editor에서 생성

| 항목 | 선택 |
|---|---|
| 카테고리 / 템플릿 | Games / Third Person |
| 구현 언어 | C++ |
| Variant | 선택 항목이 있으면 기본 / None |
| 플랫폼 | Desktop |
| 품질 | Scalable 선택 가능 시 사용 |
| Starter Content | 선택 항목이 있으면 제외 |
| Ray Tracing | 선택 항목이 있으면 비활성화 |
| Project Location | `G:\unreal-open-world-multiplayer-lab` |
| Project Name | `UnrealOpenWorldLab` |

엔진 버전마다 생성 UI 항목이 다를 수 있습니다. C++ / Third Person / 경로 / 이름을 우선 확인합니다.

**생성될 최종 파일 경로를 확인한 뒤 Create를 누릅니다.**

```text
G:\unreal-open-world-multiplayer-lab\UnrealOpenWorldLab\UnrealOpenWorldLab.uproject
```

Project Location에 프로젝트 이름까지 두 번 넣지 않습니다. 저장소 안에서 별도 `git init`도 하지 않습니다.

## 3. 첫 실행 검증

1. C++ 컴파일을 완료합니다.
2. 기본 Third Person 맵에서 Play를 실행합니다.
3. 이동, 시점 회전, 점프가 동작하는지 확인합니다.
4. 에디터를 닫고 `.uproject`로 다시 열어 정상 로드되는지 확인합니다.
5. 이 단계에서는 Iris·서버 설정을 추측해 추가하지 않습니다. 엔진 버전 확인 후 정확한 설정을 적용합니다.

## 4. 최초 커밋

PowerShell에서 저장소 루트로 이동한 뒤 포함될 파일을 확인합니다.

```powershell
Set-Location 'G:\unreal-open-world-multiplayer-lab'
git status --short
git add UnrealOpenWorldLab Docs/Environment.md
git lfs status
git diff --cached --stat
```

`Source`, `Config`, `Content`, `.uproject`가 포함되고, `Binaries`, `Intermediate`, `Saved`는 포함되지 않아야 합니다.
Unreal 바이너리 자산이 LFS 대상으로 보이는지 확인합니다.

```powershell
git commit -m "feat: add C++ Third Person project baseline"
git push
```

## 5. 다음 작업

- 정확한 `.uproject` 경로와 엔진 버전·소스 빌드 경로를 공유합니다.
- 이후 Server/Client 타깃 → 패키징·접속 → Iris 활성화 검증 순서로 진행합니다.
- PIE 검증과 패키징된 Dedicated Server 검증은 따로 기록합니다.
- 첫 실험은 [액터 부하](Experiments/01-ActorLoad/README.md)입니다.
