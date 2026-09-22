# GitHub 코드 저장소 + NAS Gitea LFS

## 문제와 선택

Unreal의 `.uasset`과 `.umap`은 바이너리 자산이다. 변경된 자산 버전을 계속 보관하고 여러 장비에서 다운로드하면 호스팅 서비스의 LFS 저장·전송 사용량이 늘어난다. 개인 포트폴리오를 지속적으로 개발하기 위해 GitHub LFS 사용량에 따른 비용 부담을 줄이는 구성을 선택했다.

코드 리뷰와 문서 열람은 GitHub에서 유지하고, 이미 운영 중인 NAS의 Gitea를 자산의 LFS 서버로 사용한다. `.gitattributes`가 어떤 파일을 LFS로 관리할지 정하고, `.lfsconfig`가 해당 객체의 업로드·다운로드 목적지를 정한다.

| 저장 대상 | 위치 |
|---|---|
| C++·Config·프로젝트 파일·문서·커밋 이력 | GitHub |
| 자산의 SHA-256과 크기를 가진 작은 LFS 포인터 | GitHub의 Git 객체 |
| `.uasset`·`.umap` 등의 실제 바이너리 | NAS Gitea LFS |
| 생성된 빌드·캐시·원본 프로파일링 트레이스 | 로컬, Git 제외 |

```text
Local project -- Git commits / LFS pointers --> GitHub
              -- LFS binary objects --------> NAS Gitea
```

이는 NAS 저장 공간, 전력, 회선, 백업 비용을 없애는 설계가 아니다. 이미 보유한 인프라로 자산 저장·전송을 이전하는 선택이다. 금액 기준 절감 효과와 장기 운영 신뢰성은 아직 측정하지 않았다.

## 적용 구성

- Git origin: `https://github.com/hon454/unreal-open-world-multiplayer-lab.git`
- Gitea: `https://gitea.jeonjihoon.dev/hon454/unreal-open-world-multiplayer-lab-lfs`
- Gitea 저장소 공개 범위: 공개 (2026-09-22 전환). 포트폴리오 사용자가 별도 계정 없이 자산을 복원할 수 있다.
- `.lfsconfig`의 `url`, `pushurl`: 위 Gitea 저장소의 `.git/info/lfs` 주소.
- 다운로드는 익명 접근을 허용한다. 업로드·수정은 권한 있는 사용자 인증이 필요하며 로컬 Git credential helper를 사용한다. 인증 정보는 커밋하지 않는다.
- 자산을 업로드할 개발자는 브라우저 로그인과 별도로 로컬 Git 인증을 준비한다.
- Git remote를 Gitea로 바꾸거나 Content를 별도 Git submodule로 분리하지 않는다.

Gitea 저장소는 LFS API와 객체 저장 용도로 사용하므로 Git 파일 목록이 비어 있어도 정상이다. Gitea LFS 관리 화면과 실제 다운로드로 자산 존재 여부를 확인한다. 서버 설정 파일을 직접 점검하지는 않았고, 실제 LFS 왕복 요청 성공으로 기능을 확인했다.

## 커밋·푸시 전 확인

```powershell
powershell -NoProfile -File .\Scripts\Test-LfsRouting.ps1
git status --short
git add .lfsconfig .gitattributes .gitignore OpenWorldMultiLab.uproject Config Source Content Docs README.md Scripts .vsconfig
git lfs status
git diff --cached --stat
git commit -m "Describe the actual change"
git push origin main
```

기존 Git LFS pre-push hook이 실제 객체를 설정된 Gitea 서버에 업로드하고 Git 커밋은 GitHub로 보낸다. 별도의 강제 hook은 추가하지 않았다. `Test-LfsRouting.ps1`은 수동 사전 검사이며 이를 생략한 임의 설정 변경까지 차단하지 않는다.

Git의 로컬·전역 설정은 `.lfsconfig`보다 우선한다. 검사 스크립트는 명시적인 LFS URL override와 `git lfs env`를 확인하고 목적지가 달라지면 실패한다. 설정 파일을 제거하거나 다른 주소로 덮어쓴 상태에서는 자산을 푸시하지 않는다. 자산은 반드시 LFS 포인터로 커밋해야 한다.

## 다른 PC에서 복원

Git과 Git LFS를 설치한다. GitHub 코드와 Gitea 자산 저장소는 모두 공개이므로 다운로드에 별도 계정이 필요하지 않다. 아래처럼 최초 checkout의 자동 다운로드를 건너뛰면 목적지를 확인한 뒤 자산을 받을 수 있다.

```powershell
$env:GIT_LFS_SKIP_SMUDGE = '1'
try {
    git clone https://github.com/hon454/unreal-open-world-multiplayer-lab.git
} finally {
    Remove-Item Env:GIT_LFS_SKIP_SMUDGE -ErrorAction SilentlyContinue
}
Set-Location unreal-open-world-multiplayer-lab
git lfs install --local
powershell -NoProfile -File .\Scripts\Test-LfsRouting.ps1
git lfs pull origin
git lfs fsck
```

이후 `OpenWorldMultiLab.uproject`를 연다. NAS에 연결할 수 없으면 코드와 포인터를 받아도 실제 자산을 복원할 수 없다. 공개 LFS 다운로드는 NAS의 회선·가용성에 의존한다. 리뷰용 영상·캡처·측정 요약은 GitHub 문서에서도 제공한다.

## 운영과 한계

- 백업에는 LFS 객체 저장소, Gitea 데이터베이스와 설정, Git 메타데이터를 함께 포함한다. 운영 환경의 실제 백업 주기와 복원 성공 여부는 별도로 검증해야 한다.
- 이 Gitea 저장소에는 프로젝트 Git 이력을 미러링하지 않으므로 Git 브랜치 참조만으로 LFS 객체를 불필요하다고 판단해 삭제하면 안 된다. 객체 정리 작업은 GitHub의 보존 이력과 함께 판단한다.
- 실제 다운로드에 성공한 작은 객체만으로 큰 파일·동시 전송 성능을 보장하지 않는다. 전송 오류 시 NAS 용량, reverse proxy의 요청 크기·timeout, TLS, Git 인증을 확인한다.
- GitHub 웹 UI와 ZIP 다운로드는 외부 LFS 자산의 완전한 복원 경로로 가정하지 않는다. 검증한 복원 절차는 Git clone 후 Git LFS pull이다.
- `.lfsconfig`의 서버 주소는 저장소 열람자에게 보인다. 암호·토큰은 포함하지 않는다.

## 검증 근거

[저장소·LFS 검증 기록](Results/StorageValidation.md)에 실제 검사 범위, 자산 수, 바이트 수, SHA-256 및 미검증 범위를 기록한다.

## 참고

- [Git LFS 서버 분리](https://github.com/git-lfs/git-lfs/blob/main/docs/api/server-discovery.md)
- [Git LFS 설정과 우선순위](https://github.com/git-lfs/git-lfs/blob/main/docs/man/git-lfs-config.adoc)
- [Gitea LFS 서버 설정](https://docs.gitea.com/administration/git-lfs-setup/)
