# SimpleModule (Unreal Engine 5 Project)

본 프로젝트는 언리얼 엔진 5(UE5)를 기반으로 작성된 C++ 게임플레이 프로젝트입니다.
게임의 핵심 로직과 재사용 가능한 커스텀 로그 시스템이 메인 모듈과 독립적인 플러그인으로 분리되어 설계되었습니다.

## 아키텍처 및 모듈 구조 (Architecture & Module Structure)

이 프로젝트는 에픽게임즈의 코딩 표준을 준수하여 시스템 간의 결합도를 낮추기 위해 **게임 모듈**과 **플러그인**으로 명확히 분리되어 있습니다.

![NBC Plugin Enabled](NBCPlugin.png)

### 모듈 의존성 그래프 (Dependency Graph)

- **SimpleModule** (Game Module)은 플러그인 내의 **MySpartaLog** 모듈에 의존성을 가집니다.
- **MyNBCLog** 플러그인은 다른 프로젝트로 손쉽게 마이그레이션(이식) 할 수 있도록 독립적으로 컴파일됩니다.

### 📁 디렉토리 구조 (Directory Structure)

```text
📦 SimpleModule
 ┣ 📂 Source
 ┃ ┗ 📂 SimpleModule          # [Game Main Module] 게임플레이 로직 (캐릭터, 게임모드, 컨트롤러 등)
 ┃    ┣ 📜 SimpleModule.Build.cs   -> (Dependency: "MySpartaLog" 플러그인 모듈 포함)
 ┃    ┣ 📜 SimpleModuleCharacter.cpp
 ┃    ┗ 📜 ...
 ┃
 ┣ 📂 Plugins
 ┃ ┗ 📂 MyNBCLog              # [Custom Plugin] 재사용 가능한 로깅 시스템 및 유틸리티 플러그인
 ┃    ┣ 📜 MyNBCLog.uplugin   -> (Modules: "MyNBCLog", "MySpartaLog" 등록)
 ┃    ┣ 📂 Source
 ┃    ┃ ┣ 📂 MyNBCLog         # [Plugin Module 1] 플러그인 자체 메인 시스템
 ┃    ┃ ┃ ┗ 📜 MyNBCLog.Build.cs
 ┃    ┃ ┗ 📂 MySpartaLog      # [Plugin Module 2] 스파르타 커스텀 로그 모듈 (게임 모듈에서 호출)
 ┃    ┃   ┗ 📜 MySpartaLog.Build.cs
 ┃    ┗ 📂 Resources          # 플러그인 전용 아이콘 및 리소스
```

## 주요 기능 및 실행 결과 (Features & Output)

플러그인으로 분리된 `MySpartaLog` 모듈을 통해, 메인 게임 로직에서 호출된 로그들이 에디터의 출력 로그(Output Log) 창에 카테고리화되어 명확하게 표시됩니다.

![Custom Module Log Output](MoudleLog.png)

**`MyNBCLog` Plugin**
    * **역할:** 엔진 기본 로그 시스템을 확장하거나 특정 포맷을 지원하기 위해 제작된 커스텀 플러그인입니다.
    * **하위 모듈 (`MyNBCLog`, `MySpartaLog`):** 각 모듈은 고유의 책임을 가지며, 특히 `MySpartaLog`는 메인 모듈(`SimpleModule`)에서 직접 `#include` 하여 사용하도록 설계되었습니다.


## 코딩 표준 및 주의사항 (Coding Standards)

* **의존성 주입 (Dependency):** 플러그인 모듈(`MySpartaLog`)은 절대 게임 모듈(`SimpleModule`)을 참조해서는 안 됩니다. (순환 참조 방지)
* **전방 선언 (Forward Declaration):** 헤더 파일(`.h`) 컴파일 시간 최적화를 위해 다른 클래스를 참조할 때는 가급적 전방 선언을 사용하고, 소스 파일(`.cpp`)에서 `#include` 하세요.
* **로깅 (Logging):** 디버깅 시에는 가급적 플러그인으로 만들어둔 커스텀 로그 함수를 활용해 주세요.
