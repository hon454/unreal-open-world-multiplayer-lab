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

진행 중. 실제 완료 결과로 갱신한다.

## 검증 범위 밖

- Unreal 프로젝트 컴파일·게임 실행·Dedicated Server·Iris 동작.
- NAS 장애 복구와 백업 복원, 다른 사용자 계정의 권한 분리.
- 장기간 비용 절감액, 동시 다운로드 부하, 대형 자산 변경 누적에 따른 비용.
- GitHub 청구 대시보드의 사후 사용량 대조. 이 기록은 전송 목적지와 다운로드 무결성에 대한 검증이다.
