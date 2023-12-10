# DIMENSIONAL RACER

#### 2023학년도 2학기 게임소프트웨어 팀3
***
### 프로젝트 팀원

| 이름     | 학과           | 역할         |
| -------- | -------------- | ------------ |
| 곽우진    | 소프트웨어학부 |       서버      |
| 김지환    | 소프트웨어학부 |       UI       |
| 윤재호    | 음악학부       |   클라이언트 / 사운드  |
***

### 개발 주제

우주선을 타고 다양한 차원을 포탈로 넘나들며 결승선까지 도달하는 3D 멀티플레이 레이싱 게임
***

### 기술 스택

Unity 2022.3.9f1 + Photon Engine & PlayFab
***

### 개발 기간

2023.9.14 ~ 2023.12.8
***

### 사용 에셋

> 클라이언트
- Modern UI Pack (유료) :
- Low Poly Ultimate Pack (유료) : 전체 맵 디자인
- Low-Poly Spaceships Set (무료) : 플레이어 기체
- Lowpoly Textures Pack (무료) : 맵 텍스쳐
- LowPoly Trees and Rocks (무료) : 숲 맵 디자인
- Handpainted Grass & Ground Textures (무료) : 숲 맵 텍스쳐
- Stylized Low Poly Animated Horse Pack : 숲 맵 장애물
- POLYDesert (무료) : 사막 맵 디자인
- Desert Kits 64 Sample (무료) : 사막 맵 디자인
- Free Low Poly Desert Pack (무료) : 사막 맵 디자인
- Ez Tornado (무료) : 사막 맵 장애물
- Lowpoly Modern City Buildings Set (무료) : 도시 맵 디자인
- Free Low Poly Simple Urban City 3D Asset Pack (무료) : 도시 맵 디자인
- Low Poly Road Pack (무료) : 도시 맵 도로
- Sky City Lite (무료) : 미래 맵 디자인
- Stylized Sci-fi Texture (무료) : 미래 맵 텍스쳐

> 서버
- Photon Unity Networking Free (무료) : Photon 을 활용한 실시간 동기화를 구현하기 위하여 사용했습니다.
- PlayFab SDK (무료) : PlayFab 을 이용한 사용자 계정, 서버, 매치메이킹 관리를 구현하기 위하여 사용했습니다.

### 설치 및 실행 방법

여기는 제가 구글 클라우드에 업로드할게요
***

### 씬 단위 프로젝트 구조 설명
> ### 1. Base 씬

    오브젝트가 파괴되지 않도록 설정하는 OnDestroyOnLoad 기능을 사용하기 위해 제작된 씬입니다.
    모든 씬에 필요한 계정 관리용 AccountManager, 설정값을 갖는 SettingManager 오브젝트에
    OnDestroyOnLoad를 부여하고 바로 Sign In 씬으로 이동합니다.

> ### 2. Sign In 씬


> ### 3. Lobby 씬


> ### 4. Ingame 씬
> #### <맵 디자인>
- 총 4종류의 맵이 존재하며, 각 맵은 동일한 크기의 terrain 하나씩으로 구성되어 있습니다.
- terrain에서 기초적인 지형과 주행 경로를 디자인하고, paint tree의 mass place 기능을 통해 기본적인 배경 오브젝트를 분포시킨 후 추가로 오브젝트를 배치하며 자연스럽게 수정하였습니다.
- 배경 오브젝트 배치 후 mesh renderer를 해제한 cube 오브젝트를 주행 경로를 따라 빈틈없이 배치하여 플레이어의 이탈을 막는 투명 벽을 구성하였습니다.
- 주행 경로의 시작과 끝 부분에는 포탈 오브젝트를 배치하여 해당 오브젝트를 통해 다른 맵으로 이동할 수 있도록 하였습니다.

> #### <장애물 디자인>
- 기본적으로 장애물을 생성하는 spawner 오브젝트를 경로 곳곳에 여러 개 배치하여 해당 범위 내에서 장애물이 랜덤하게 스폰되도록 구성하였습니다.
- 각 spawner는 boxcolider 범위를 지니고 있고, 게임이 시작되면 area = GetComponent<BoxCollider>로 boxcolider 범위를 지정한 뒤 Instantiate를 통해 해당 범위 내에서 장애물이 생성됩니다.
- 각 장애물의 종류, 개수, 위치는 미리 설정한 범위 내에서 random.range를 통해 랜덤하게 결정됩니다.
- 생성 후 이동하는 일부 장애물은 각 장애물 프리팹에 이동 방향과 속도가 설정된 코드를 추가로 삽입하고, destructionDelay로 생성 후 일정 시간이 지나면 소멸되도록 하였습니다.
- 각 맵 별로 맵의 특성에 맞춘 전용 장애물이 존재합니다 :
- 숲 : 길을 가로질러 이동하는 3종류의 말. 천천히 걷는 말 / 빠르게 걷는 말 / 점프하는 말 3종류가 존재하며, 길 옆에 horse spawner를 배치하고 이동 방향을 좁은 범위 내에서만 랜덤하게 설정되도록 하여 말이 엉뚱한 방향으로 나아가지 않도록 조정했습니다. 게임 시작 후 일정한 시간마다 몇 마리의 말이 지속적으로 생성되며, 각 말은 생성 후 일정 시간이 지나면 소멸합니다.
- 사막 : 도로 곳곳에 회오리바람이 생성됩니다. 역시 spawner 범위 내에서 랜덤한 위치에 랜덤한 숫자가 생성되도록 하였습니다.
- 도시 : 차가 이동하는 도로라는 컨셉에 맞게 설치형 장애물은 신호등/표지판 등으로 지면이 아닌 공중의 이동을 제한하도록 하였으며, 추가로 도로의 끝에서 차량이 생성되어 도로를 따라 이동하도록 구현하였습니다. 각 차량은 방향은 정해져 있으나 moveSpeed=random.range()를 통해 이동 속도가 랜덤하게 설정되며, 도로의 끝까지 이동한 뒤 소멸합니다. 승용차, 트럭, 버스 등 여러 종류의 차량이 랜덤하게 생성됩니다.
- 미래 : 중력이 없는 외계 행성 컨셉이기에 y축 범위가 없었던 다른 3개 맵과 달리 y축까지 포함해 3차원 공간 내에서 랜덤하게 장애물이 스폰되도록 하였으며, 추가로 맵 중앙에 거대한 자기부상열차 정류장을 디자인하여 일정 시간마다 경로를 가로질러 거대한 열차가 지나가도록 하였습니다. 해당 역에서는 경로가 3갈래로 나뉘고, 플레이어가 이동중인 열차의 진행을 고려하여 경로를 선택하도록 의도하였습니다.
***

### 사운드 설명
> #### <효과음>
- 샘플 음원을 daw를 통해 수정하여 게임 분위기에 맞는 소리로 제작하였습니다.
- 샘플은 splice(유료) 와 freesound(무료) 사이트를 이용하였고, daw는 studio one을 사용했습니다.
> #### <BGM>
- 타이틀 음악, 로비 음악, 인게임 음악 3종류를 각각 작곡하여 삽입했습니다.
- 각 음악은 인트로 + 반복 두 부분으로 나뉘며, 처음 씬 진입 시 인트로부터 재생된 후 반복 파트가 반복재생되도록 하였습니다.
- 인게임에서 4종류의 각각의 맵에서는 BGM과는 별개로 맵 특성에 맞춘 '환경음악'이 재생됩니다. 숲은 새가 지저귀는 소리, 사막은 바람소리, 도시는 차가 지나가는 소리, 미래는 몽환적인 기계음입니다.

