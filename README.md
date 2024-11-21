# FruitStorePOS
used C# Windows form
2023.06.15 - 2학년 2학기 팀프로젝트(2인1조)

![image](https://github.com/user-attachments/assets/053a9530-47bd-44f4-910d-a44487fdfa9a)

## FruitStorePOS 프로젝트 설명
FruitStorePOS는 과일 가게를 운영하기 위해 개발된 간단한 POS(Point of Sale) 시스템입니다. 
이 프로그램은 Windows Forms와 C#을 사용하여 개발되었으며, 사용자가 직관적으로 상품을 등록하고 결제를 진행하며, 영수증을 출력할 수 있도록 설계되었습니다. 
주요 화면은 상품 목록, 결제 패드, 상품 재고 관리, 총 금액 계산 등을 포함합니다.

## 주요 기능
1. 상품 등록 및 판매
  화면 하단에 있는 버튼(예: 사과, 오렌지 등)을 클릭하면 상품이 판매 목록에 추가됩니다.
  각 상품은 단가, 수량, 총 금액이 자동 계산되어 화면에 표시됩니다.

2. 결제 기능
  오른쪽의 숫자 키패드를 사용하여 고객이 지불한 금액을 입력합니다.
  현금 버튼을 누르면 총 금액과 받은 금액, 거스름돈이 자동으로 계산됩니다.
  결제 후 선택적으로 영수증을 출력할 수 있습니다.

3. 영수증 출력
  영수증에는 거래 시간, 상품명, 단가, 수량, 총 금액, 받은 금액, 거스름돈 등이 표시됩니다.
  사용자가 프린트 미리 보기를 통해 확인 후 실제로 인쇄할 수 있습니다.

4. 재고 관리
  하단의 재고 섹션에서 현재 남아 있는 재고를 실시간으로 확인할 수 있습니다.
  상품이 판매되면 재고가 자동으로 차감됩니다.

5. UI 편의성
  직관적이고 깔끔한 인터페이스로 POS를 처음 사용하는 사람도 쉽게 사용할 수 있습니다.
  Reset 버튼으로 판매를 초기화하거나 선택취소로 특정 상품만 삭제할 수 있습니다.

## 구현 과정의 어려움 및 해결
1. 재고 관리
  문제: 상품 판매 시 재고 차감과 실시간 업데이트가 제대로 이루어지지 않았습니다.
  해결: 각 상품의 재고를 Dictionary로 관리하여, 버튼 클릭 이벤트와 재고 차감 로직을 연결했습니다.

2. 영수증 출력
  문제: 영수증에 데이터가 제대로 표시되지 않거나 미리보기 화면이 깨지는 문제가 발생했습니다.
  해결: PrintDocument 클래스를 활용하고, 영수증 레이아웃을 Graphics 객체를 사용해 세밀하게 조정했습니다.
  
3. UI 디자인
  문제: 버튼과 재고 리스트를 배치하면서 화면 요소들이 겹치는 문제가 있었습니다.
  해결: Windows Forms의 TableLayoutPanel과 FlowLayoutPanel을 활용하여 동적으로 화면 크기에 맞게 레이아웃을 조정했습니다.

4. 데이터 정합성
  문제: 사용자가 동시에 여러 버튼을 누르거나 입력값에 문제가 생기면 오류가 발생했습니다.
  해결: 입력값 검증 및 예외 처리 로직을 추가하여 안정성을 높였습니다.

## 사용된 기술 및 코드 구조
1. C#
  Windows Forms 환경에서 이벤트 기반 프로그래밍으로 버튼 클릭, 결제 처리 등의 로직 구현.

2. 데이터 관리
  상품과 재고 정보는 내부 데이터 구조로 관리(예: List 및 Dictionary).
  각 상품에 대해 별도의 클래스를 설계하여 정보(단가, 재고 등)를 캡슐화.

3. 출력 기능
  영수증 출력은 PrintPreviewDialog와 PrintDocument를 활용.
  사용자의 입력 데이터를 포맷팅하여 출력.

4. UI 요소
  버튼 클릭 이벤트와 텍스트박스의 데이터 연동.
  DataGridView로 판매 목록을 표시하고, 실시간으로 총 금액 계산.

## 향후 개선 방향
1. 상품 관리 기능 추가<br>
  새로운 상품을 추가하거나 삭제하는 기능을 구현해 더욱 유연한 POS 시스템으로 발전 가능.

2. 데이터베이스 연동
  현재 재고 및 판매 정보는 메모리에 저장되므로, 데이터베이스를 도입하여 데이터의 지속성을 높이고 분석 기능을 추가할 수 있습니다.

3. 멀티 디바이스 지원
  Windows 환경뿐 아니라 태블릿이나 웹 기반 환경에서도 사용할 수 있도록 확장.

4. 할인 및 프로모션 기능
  특정 상품에 대해 할인율을 적용하는 기능 추가.

## 프로젝트의 의의
FruitStorePOS는 소규모 사업자를 위한 간단하고 사용하기 쉬운 POS 시스템을 목표로 설계되었습니다. 
실무에서 사용할 수 있는 기능 구현을 통해 POS 시스템의 기본 구조와 작동 원리를 이해하고, 
실질적인 문제 해결 능력을 키울 수 있는 좋은 학습 프로젝트였습니다.

![image](https://github.com/user-attachments/assets/b67b98e5-0e79-452b-a9bd-88ff0e20acb3)

![image](https://github.com/user-attachments/assets/d4a7a8fb-1348-40d8-b1fe-14002c126b9f)
