# 프로젝트 열기와 개발 시작

## 현재 구성

사용자가 Third Person 기반 C++ 프로젝트를 생성하고 내부 파일을 저장소 루트로 이동했다.
프로젝트 파일은 `OpenWorldMultiLab.uproject`이고 `EngineAssociation` 값은 `5.8`이다.
정확한 패치·엔진 빌드 방식과 게임 실행 성공 여부는 아직 별도 검증하지 않았다.

```text
G:\unreal-open-world-multiplayer-lab\OpenWorldMultiLab.uproject
```

프로젝트를 다시 만들거나 이름이 같은 하위 폴더를 추가할 필요는 없다.

## 새 PC에서 시작

1. [저장소·LFS 운영 가이드](Storage.md)의 clone 절차로 GitHub 코드와 Gitea 자산을 받는다.
2. 사용할 UE 5.8의 정확한 패치와 엔진 빌드 방식을 맞춘다.
3. 엔진 버전에 맞는 Visual Studio C++ 도구와 Windows SDK를 준비한다. `.vsconfig`는 공유 구성 참고 자료다.
4. `.uproject`의 엔진 연결을 확인하고 프로젝트 파일을 재생성한다. `.sln`과 `.slnx`는 Git에서 제외한다.
5. Editor 타깃을 컴파일하고 `.uproject`를 열어 기본 맵의 이동·시점·점프를 확인한다.
6. 에디터를 재실행해 정상 로드를 확인하고 [Environment.md](Environment.md)에 환경을 기록한다.

엔진 소스는 저장소 밖에 둔다. Dedicated Server 빌드 단계에서는 소스 빌드 엔진을 기준으로 도구와 타깃을 준비한다.

## 일상적인 변경 저장

1. `Scripts/Test-LfsRouting.ps1`로 LFS 목적지를 확인한다.
2. `git status --short`로 변경 범위를 확인한다.
3. 필요한 `Source`, `Config`, `Content`, `.uproject`와 문서를 스테이징한다.
4. `git lfs status`와 `git diff --cached --stat`를 확인한 뒤 커밋하고 푸시한다.

World Partition의 외부 액터·오브젝트 자산도 변경분에 포함한다. 공식 ignore가 제외하는 `*_BuiltData.uasset` 등은 필요한 경우 용도를 확인한 뒤 명시적인 예외를 추가한다.

## 다음 구현 순서

Server/Client 타깃 → 패키징된 서버와 실제 클라이언트 2개 접속 → Iris 활성화 검증 → 상시 패널과 [액터 부하 실험](Experiments/01-ActorLoad/README.md).

현재 템플릿에 포함된 Combat / Platforming / SideScrolling 예제 코드를 새로 개발한 네트워크 기능으로 간주하지 않는다. PIE 동작과 패키징된 Dedicated Server 검증도 분리해 기록한다.
