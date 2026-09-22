# 저장소·LFS 검증 기록

검증일: 2026-09-22. 게임 성능 측정과 별개로 버전 관리 경로를 검증한 기록이다.

## 환경

- Git: 2.45.2.windows.1
- Git LFS: 3.5.1
- Gitea: 1.25.2 (브라우저 UI)
- Git origin: GitHub, master 브랜치
- LFS upload/download endpoint: NAS Gitea 저장소의 `.git/info/lfs`

## 소형 자산 왕복 검증 — 통과

- 원본: `Content/Input/Actions/IA_Move.uasset`
- 크기: 1,385 bytes
- SHA-256: `85567b9d91ffd9f7106c3062557a6fa864a791233380353f63eb563470985add`
- 스테이징된 Git blob이 LFS 포인터인지 확인하고 해당 OID만 업로드했다.
- 프로젝트 캐시를 공유하지 않는 임시 Git 저장소에 포인터와 `.lfsconfig`를 넣었다.
- 비어 있는 별도 LFS 캐시에서 `git lfs pull origin`을 실행했다.
- 다운로드 파일 크기와 SHA-256이 원본과 일치했다.
- GitHub에는 이 검증용 임시 저장소나 바이너리를 업로드하지 않았다.

## 전체 자산 검증

완료: 전체 753개 자산을 Gitea에서 복원하고 모두 크기·SHA-256이 일치했다.

| 항목 | 결과 |
|---|---|
| 검증 대상 코드·자산 커밋 | `7b15f22ce5e45c690a31a0b667fb664f5dd9e596` |
| 자산 파일 / 고유 LFS 객체 | 753 / 753 |
| 실제 자산 크기 | 140,920,074 bytes (약 134.4 MiB) |
| Git에 저장된 자산 포인터 크기 합계 | 97,510 bytes (약 95.2 KiB) |
| 독립된 캐시에서 복원한 파일 | 753 / 753 |
| 모든 파일의 크기·SHA-256 비교 | 통과 |
| 원본 및 복원본 `git lfs fsck` | 통과 |
| LFS pull 경과 시간 | 51.16초 (단일 실행 관측값) |

GitHub에 보낼 Git 데이터에는 자산 원본 대신 포인터만 들어가는지 전체 Content blob의 크기와 LFS 목록을 대조했다. 97,510 bytes는 자산 포인터 내용의 합계이며 전체 Git 저장소 용량이나 호스팅 청구액이 아니다.

검증용 Git 이력은 로컬 저장소에서 `--no-hardlinks --no-checkout`으로 가져오고, smudge를 건너뛰어 체크아웃했다. Git origin을 GitHub 주소로 설정한 뒤 독립된 LFS 객체 캐시가 비어 있는지 확인했다. 이후 `git lfs pull origin`은 `.lfsconfig`에 따라 Gitea에서 실제 자산을 내려받았다. 로컬 원본의 `.git/lfs`를 복사하거나 공유하지 않았다.

51.16초는 현재 장비·회선·서버 상태의 단일 실행 결과이며 반복 성능 벤치마크가 아니다.

## 저장소 규칙 검증 — 통과

- 공식 UnrealEngine.gitignore 원본과 로컬 파일 상단을 대조했다 (줄바꿈 정규화 후 일치).
- 다운로드한 원본 SHA-256: `886c4b93ae6e6092b00a109c2d6cb85f39704a988c692d6b205e39114d7f4e79`.
- `.slnx`·빌드·캐시·원본 트레이스 제외와 `.vsconfig` 예외를 확인했다.
- `.uproject`, 일반 자산, World Partition 외부 액터·오브젝트, Build 아이콘이 제외되지 않는지 확인했다.
- 공식 `*_BuiltData.uasset` 및 일반 Build 출력 제외 규칙을 유지했다.
- LFS 목적지 검사 정상 통과와 잘못된 URL override를 거부하는 경우를 각각 확인했다.
- 작성·수정한 문서의 상대 링크와 공백 검사를 통과했다. 사용자가 생성한 템플릿 C++ 원문은 변경하지 않았다.

[기계 판독용 검증 결과](StorageValidation.json)

## 검증 범위 밖

- Unreal 프로젝트 컴파일·게임 실행·Dedicated Server·Iris 동작.
- NAS 장애 복구와 백업 복원, 다른 사용자 계정의 권한 분리.
- 장기간 비용 절감액, 동시 다운로드 부하, 대형 자산 변경 누적에 따른 비용.
- GitHub 청구 대시보드의 사후 사용량 대조. 이 기록은 전송 목적지와 다운로드 무결성에 대한 검증이다.
