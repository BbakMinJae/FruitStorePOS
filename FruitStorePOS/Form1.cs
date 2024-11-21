using System.Drawing.Printing;
using System.Security.Cryptography.X509Certificates;

namespace FruitStorePOS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public int Cost_of_Items()  // dataGridView1의 모든 행에 대해 5번째 셀의 값을 합산하는 기능을 수행
        {
            int sum = 0;
            int i = 0;

            for (i = 0; i < dataGridView1.Rows.Count; i++) // dataGridView1행의 수만큼 반복
            {
                sum = sum + Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value); // dataGridView1 i번째 행의 5번째 셀의 값(index 번호 4)을 
            }                                                                      // 정수형으로 변환하여 sum의 변수에 저장

            return sum; // 상품의 총합을 반환
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Total() // 합계 자동 출력하는 역할을 수행
        {
            if (dataGridView1.Rows.Count > 0)            // 행의 수가 하나 이상일 때 실행
                total.Text = Cost_of_Items().ToString(); // 상품의 총합을 문자형으로 변환하여 출력
        }

        private void Change() // dataGridView1에 있는 모든 상품들의 총 가격을 계산하고,
                              // 그 결과를 고객이 지불한 돈에서 빼서 거스름돈을 계산하고, 그 결과를 change에 출력하는 역할
        {
            int items, money;                                   // items, money를 정수 변수 선언 
            if (dataGridView1.Rows.Count > 0)                   // 행의 수가 하나 이상일 때 실행
            {
                items = Cost_of_Items();                        // Cost_of_items 메소드를 호출하여 모든 상품 가격 계산 후 결과를 items 변수에 저장
                money = Convert.ToInt32(receivedMoney.Text);    // receivedMoney의 텍스트 값을 정수형으로 변환하여 money 변수에 저장
                change.Text = (money - items).ToString();       // 받은돈에서 상품의 총합을 뺀 값을 문자열로 변환하여 change.Text로 나타냄
            }
        }

        private void Quantity() // 데이터그리드뷰의 각 행에 대해 상품의 이름을 확인하고, 
                                // 해당 상품의 재고 수량을 해당 상품의 판매 수량만큼 차감하는 기능을 수행
        {
            foreach (DataGridViewRow row in this.dataGridView1.Rows)  //dataGridView1에 있는 각 행을 반복
            {
                switch (row.Cells[1].Value) //데이터그리드뷰의 행에서 2번째 셀인 상품명을 나타냄
                {
                    case "얼음골사과":
                        numericUpDown1.Value -= int.Parse(row.Cells[3].Value.ToString());
                        //(numericUpDown1의 값) - (데이터그리드뷰의 행에서 4번째셀인 수량)
                        continue;
                    case "오렌지":
                        numericUpDown2.Value -= int.Parse(row.Cells[3].Value.ToString());
                        continue;

                    case "바나나":
                        numericUpDown3.Value -= int.Parse(row.Cells[3].Value.ToString());
                        continue;

                    case "딸기":
                        numericUpDown4.Value -= int.Parse(row.Cells[3].Value.ToString());
                        continue;

                    case "샤인머스캣":
                        numericUpDown5.Value -= int.Parse(row.Cells[3].Value.ToString());
                        continue;

                    case "포도":
                        numericUpDown6.Value -= int.Parse(row.Cells[3].Value.ToString());
                        continue;

                    case "메론":
                        numericUpDown7.Value -= int.Parse(row.Cells[3].Value.ToString());
                        continue;

                    case "수박":
                        numericUpDown8.Value -= int.Parse(row.Cells[3].Value.ToString());
                        continue;

                    case "배":
                        numericUpDown9.Value -= int.Parse(row.Cells[3].Value.ToString());
                        continue;

                    case "복숭아":
                        numericUpDown10.Value -= int.Parse(row.Cells[3].Value.ToString());
                        continue;

                    case "망고":
                        numericUpDown11.Value -= int.Parse(row.Cells[3].Value.ToString());
                        continue;

                    case "키위":
                        numericUpDown12.Value -= int.Parse(row.Cells[3].Value.ToString());
                        continue;
                }
            }
        }


        private void button29_Click(object sender, EventArgs e) // 현금(계산) 클릭
        {
            Change(); // 거스름 메소드 실행
            Quantity(); // 수량 메소드 실행
        }

        private void button28_Click(object sender, EventArgs e) // 영수증 출력
        {
            try
            {
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrintPage += new PrintPageEventHandler(PrintReceipt);

                PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
                printPreviewDialog.Document = printDocument;
                printPreviewDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PrintReceipt(object sender, PrintPageEventArgs e)  // 프린터로 출력할 때 사용하는 메서드
        {
            Graphics graphics = e.Graphics;                             // Graphics는 그래픽을 사용할 때 쓰는 도구
            Font headerFont = new Font("Arial", 16, FontStyle.Bold);    // 제목 폰트와 크기 설정
            Font itemFont = new Font("Arial", 10);                      // 상품 폰트와 크기 설정
            Brush brush = Brushes.Black;
            int pageWidth = 827; //용지 크기 너비 설정(A4 사이즈)
            int pageHeight = 1169; //용지 크기 높이 설정(A4 사이즈)


            //영수증 형식
            string headerText = "[ 판매 영수증 ]";                            // 헤더 텍스트를 headerText 변수에 저장
            float headerX = (pageWidth - graphics.MeasureString(headerText, headerFont).Width) / 2; // graphics.MeasureString 함수는 문자열이 그래픽상에서
                                                                                                    // 얼마나 많은 공간을 차지할지 계산하여 가운데 배치
            float headerY = (float)(pageHeight * 0.0513);   // 페이지 높이의 약 5.13%에 해당하는 위치로 설정, 연산 속도를 위해 float로 형 변환
            PointF center = new PointF(headerX, headerY);   // PointF 객체 생성
            graphics.DrawString(headerText, headerFont, brush, center); // DrawString 메소드 호출하여 텍스트 생성

            string smallheaderText = "[ 고객용 ]";     // smallgeaderText라는 변수에 고객용이라는 문자열 저장
            headerFont = new Font("Arial", 10, FontStyle.Regular);
            headerX = (pageWidth - graphics.MeasureString(smallheaderText, headerFont).Width) / 2;
            headerY = headerY + 30; // 30만큼 위치 증가
            center = new PointF(headerX, headerY);
            graphics.DrawString(smallheaderText, headerFont, brush, center);

            smallheaderText = "(주) 신라 과일가게";
            headerFont = new Font("Arial", 10, FontStyle.Regular);
            headerX = (pageWidth - graphics.MeasureString(smallheaderText, headerFont).Width) / 2;
            headerY = headerY + 40; // 40만큼 위치 증가
            center = new PointF(headerX, headerY);
            graphics.DrawString(smallheaderText, headerFont, brush, center);

            smallheaderText = "사업자번호 : 111-22-33343";
            headerFont = new Font("Arial", 10, FontStyle.Regular);
            headerX = (float)(pageWidth * 0.331);
            headerY = headerY + 20;
            center = new PointF(headerX, headerY);
            graphics.DrawString(smallheaderText, headerFont, brush, center);

            smallheaderText = "대표자 : 신동재         TEL : 051-999-5000";
            headerFont = new Font("Arial", 10, FontStyle.Regular);
            headerX = (pageWidth - graphics.MeasureString(smallheaderText, headerFont).Width) / 2;
            headerY = headerY + 20;
            center = new PointF(headerX, headerY);
            graphics.DrawString(smallheaderText, headerFont, brush, center);

            smallheaderText = "부산광역시 사상구 백양대로 700번길 140";
            headerFont = new Font("Arial", 10, FontStyle.Regular);
            headerX = (pageWidth - graphics.MeasureString(smallheaderText, headerFont).Width) / 2;
            headerY = headerY + 20;
            center = new PointF(headerX, headerY);
            graphics.DrawString(smallheaderText, headerFont, brush, center);

            smallheaderText = "----------------------------------------------------------";
            headerFont = new Font("Arial", 10, FontStyle.Regular);
            headerX = (pageWidth - graphics.MeasureString(smallheaderText, headerFont).Width) / 2;
            headerY = headerY + 20;
            center = new PointF(headerX, headerY);
            graphics.DrawString(smallheaderText, headerFont, brush, center);

            smallheaderText = "판매시간 : " + DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            headerFont = new Font("Arial", 10, FontStyle.Regular);
            headerX = (pageWidth - graphics.MeasureString(smallheaderText, headerFont).Width) / 2;
            headerY = headerY + 20;
            center = new PointF(headerX, headerY);
            graphics.DrawString(smallheaderText, headerFont, brush, center);

            smallheaderText = "판매사원 : 박민재";
            headerFont = new Font("Arial", 10, FontStyle.Regular);
            headerX = (pageWidth - graphics.MeasureString(smallheaderText, headerFont).Width) / 2;
            headerY = headerY + 20;
            center = new PointF(headerX, headerY);
            graphics.DrawString(smallheaderText, headerFont, brush, center);

            smallheaderText = "==================================";
            headerFont = new Font("Arial", 10, FontStyle.Regular);
            headerX = (pageWidth - graphics.MeasureString(smallheaderText, headerFont).Width) / 2;
            headerY = headerY + 20;
            center = new PointF(headerX, headerY);
            graphics.DrawString(smallheaderText, headerFont, brush, center);

            smallheaderText = "상품                              단가   수량    금액";
            headerFont = new Font("Arial", 10, FontStyle.Regular);
            headerX = (pageWidth - graphics.MeasureString(smallheaderText, headerFont).Width) / 2;
            headerY = headerY + 20;
            center = new PointF(headerX, headerY);
            graphics.DrawString(smallheaderText, headerFont, brush, center);

            smallheaderText = "==================================";
            headerFont = new Font("Arial", 10, FontStyle.Regular);
            headerX = (pageWidth - graphics.MeasureString(smallheaderText, headerFont).Width) / 2;
            headerY = headerY + 20;
            center = new PointF(headerX, headerY);
            graphics.DrawString(smallheaderText, headerFont, brush, center);



            float startX = (float)(pageWidth * 0.331);  // 품목 목록의 시작 위치 설정
            float startY = headerY + 80;
            int lineHeight = 20; // 각 품목간의 수직 간격 20으로 설정

            decimal total = 0; // 품목 가격 합계 초기화

            // 영수증 내용 그리기
            for (int i = 0; i < dataGridView1.Rows.Count; i++)              // 데이터그리드뷰의 모든 행을 반복
            {
                DataGridViewRow row = dataGridView1.Rows[i];                // 현재 반복에러 처리해야 할 행 가져옴

                string itemName = row.Cells[1].Value.ToString();            // 인덱스번호[1]로 상품 이름 가져옴
                int quantity = Convert.ToInt32(row.Cells[3].Value);         // 인덱스번호[3]로 수량을 정수 형태로 가져옴
                decimal price = Convert.ToDecimal(row.Cells[2].Value);      // 인덱스번호[2]로 상품의 가격을 가져옴


                smallheaderText = itemName;                                 // 상품 이름을 출력 
                headerFont = new Font("Arial", 10, FontStyle.Regular);
                //headerX = (float)(headerX * 0.581);
                headerY = headerY + 20;
                center = new PointF(headerX, headerY);
                graphics.DrawString(smallheaderText, headerFont, brush, center);

                // 숫자를 문자열로 가져오는데 "N0"를 이용하여 콤마가 있는 숫자로 변환
                // 가격이 3자리 미만일 경우, 공백이 많이 앞에 추가 이렇게 하면 가격이 짧을 때 가격, 수량, 총 가격이 오른쪽으로 정렬
                // 가격이 3자리 이상 5자리 미만일 경우, 앞에 더 적은 공백이 추가
                // 가격이 5자리 이상일 경우, 가장 적은 공백이 앞에 추가
                if (price.ToString("N0").Length < 3)
                {
                    smallheaderText = "                                         " + price.ToString("N0") + "       " + quantity.ToString() + "       " + (quantity * price).ToString("N0");
                }
                else if (price.ToString("N0").Length < 5)
                {
                    smallheaderText = "                                       " + price.ToString("N0") + "       " + quantity.ToString() + "       " + (quantity * price).ToString("N0");
                }
                else
                {
                    smallheaderText = "                                    " + price.ToString("N0") + "       " + quantity.ToString() + "       " + (quantity * price).ToString("N0");
                }
                //품목과 수량 합계 출력
                headerFont = new Font("Arial", 10, FontStyle.Regular);
                headerX = (pageWidth - graphics.MeasureString(smallheaderText, headerFont).Width) / 2;
                headerY = headerY + 20;
                center = new PointF(headerX, headerY);
                graphics.DrawString(smallheaderText, headerFont, brush, center);


                total += price * quantity; // 품목 가격 합산
            }

            smallheaderText = "----------------------------------------------------------";
            headerFont = new Font("Arial", 10, FontStyle.Regular);
            headerX = (pageWidth - graphics.MeasureString(smallheaderText, headerFont).Width) / 2;
            headerY = headerY + 20;
            center = new PointF(headerX, headerY);
            graphics.DrawString(smallheaderText, headerFont, brush, center);

            //합계 출력
            int totalprice = (int)total;
            smallheaderText = $"합      계 :                  {totalprice.ToString("N0")}";
            headerFont = new Font("Arial", 16, FontStyle.Bold);
            headerX = (pageWidth - graphics.MeasureString(smallheaderText, headerFont).Width) / 2;
            headerY = headerY + 20;
            center = new PointF(headerX, headerY);
            graphics.DrawString(smallheaderText, headerFont, brush, center);

            //사용자가 지불한 금액과 거스름돈을 텍스트박스에서 가져옴
            int amountPaid = int.Parse(receivedMoney.Text); // 사용자가 지불한 금액
            int changeValue = int.Parse(change.Text); // 거스름돈

            // 사용자가 지불한 금액 출력
            smallheaderText = $"받 은 돈 :                  {amountPaid.ToString("N0")}";
            headerFont = new Font("Arial", 16, FontStyle.Bold);
            headerY = headerY + 30;
            center = new PointF(headerX, headerY);
            graphics.DrawString(smallheaderText, headerFont, brush, center);

            // 거스름돈 출력
            smallheaderText = $"거스름돈 :                  {changeValue.ToString("N0")}";
            headerFont = new Font("Arial", 16, FontStyle.Bold);
            headerY = headerY + 30;
            center = new PointF(headerX, headerY);
            graphics.DrawString(smallheaderText, headerFont, brush, center);


        }




        private void button27_Click(object sender, EventArgs e) //Reset 버튼
        {
            try
            {
                //받은 돈, 입력사항, 합계, 거스름, 데이터그리드뷰 행 초기화
                receivedMoney.Text = "0";
                receivedMoney2.Text = "0";
                total.Text = "";
                change.Text = "";
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh(); //데이터그리드뷰 화면 업데이트
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NumbersOnly(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            if (receivedMoney.Text == "0" || receivedMoney.Text == "00") // 받은 돈 텍스트가 0또는 00일 때
            {
                receivedMoney.Text = "";
                receivedMoney.Text = b.Text; //빈칸으로 초기화 후 숫자 출력
            }
            else
                receivedMoney.Text = receivedMoney.Text + b.Text; //아닐 경우 그대로 숫자 출력

            if (receivedMoney2.Text == "0" || receivedMoney2.Text == "00")
            {
                receivedMoney2.Text = "";
                receivedMoney2.Text = b.Text;
            }
            else
                receivedMoney2.Text = receivedMoney2.Text + b.Text;
        }



        private void button13_Click(object sender, EventArgs e) // 입력값 초기화 버튼
        {
            receivedMoney.Text = "0";
            receivedMoney2.Text = "0";
        }

        private void button30_Click(object sender, EventArgs e) // 선택취소 (항목값 삭제)
        {
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row); //행 삭제
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int CostofItem = 1700;
            bool itemExists = false;
            int quantity = 0;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if ((string)row.Cells[1].Value == "얼음골사과")
                {
                    itemExists = true;
                    quantity = int.Parse((string)row.Cells[3].Value) + 1;
                    row.Cells[3].Value = quantity.ToString();
                    row.Cells[4].Value = (CostofItem * quantity).ToString();
                    break;
                    // "얼음골사과" 상품이 존재 => 해당 상품의 수량을 증가시키고 가격을 업데이트합니다.
                }
            }

            if (!itemExists) // "얼음골사과" 상품이 이미 존재하는지 확인
            {
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, "얼음골사과", CostofItem, "1", CostofItem);
                // 상품이 존재하지 않는 경우에는 새로운 행을 추가합니다.
            }


            Total();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            int CostofItem = 830;
            bool itemExists = false;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if ((string)row.Cells[1].Value == "오렌지")
                {
                    itemExists = true;
                    int quantity = int.Parse((string)row.Cells[3].Value) + 1;
                    row.Cells[3].Value = quantity.ToString();
                    row.Cells[4].Value = (CostofItem * quantity).ToString();
                    break;
                    // "오렌지" 상품이 존재 => 해당 상품의 수량을 증가시키고 가격을 업데이트합니다.
                }
            }

            if (!itemExists) // "오렌지" 상품이 이미 존재하는지 확인
            {
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, "오렌지", CostofItem, "1", CostofItem);
                // 상품이 존재하지 않는 경우에는 새로운 행을 추가합니다.
            }
            Total();
        }


        //이하 동일
        private void button3_Click_1(object sender, EventArgs e)
        {
            int CostofItem = 3000;
            bool itemExists = false;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if ((string)row.Cells[1].Value == "바나나")
                {
                    itemExists = true;
                    int quantity = int.Parse((string)row.Cells[3].Value) + 1;
                    row.Cells[3].Value = quantity.ToString();
                    row.Cells[4].Value = (CostofItem * quantity).ToString();
                    break;
                }
            }

            if (!itemExists)
            {
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, "바나나", CostofItem, "1", CostofItem);
            }
            Total();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int CostofItem = 2000;
            bool itemExists = false;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if ((string)row.Cells[1].Value == "딸기")
                {
                    itemExists = true;
                    int quantity = int.Parse((string)row.Cells[3].Value) + 1;
                    row.Cells[3].Value = quantity.ToString();
                    row.Cells[4].Value = (CostofItem * quantity).ToString();
                    break;
                }
            }

            if (!itemExists)
            {
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, "딸기", CostofItem, "1", CostofItem);
            }
            Total();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int CostofItem = 2000;
            bool itemExists = false;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if ((string)row.Cells[1].Value == "수박")
                {
                    itemExists = true;
                    int quantity = int.Parse((string)row.Cells[3].Value) + 1;
                    row.Cells[3].Value = quantity.ToString();
                    row.Cells[4].Value = (CostofItem * quantity).ToString();
                    break;
                }
            }

            if (!itemExists)
            {
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, "수박", CostofItem, "1", CostofItem);
            }
            Total();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int CostofItem = 1700;
            bool itemExists = false;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if ((string)row.Cells[1].Value == "메론")
                {
                    itemExists = true;
                    int quantity = int.Parse((string)row.Cells[3].Value) + 1;
                    row.Cells[3].Value = quantity.ToString();
                    row.Cells[4].Value = (CostofItem * quantity).ToString();
                    break;
                }
            }

            if (!itemExists)
            {
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, "메론", CostofItem, "1", CostofItem);
            }
            Total();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int CostofItem = 2450;
            bool itemExists = false;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if ((string)row.Cells[1].Value == "포도")
                {
                    itemExists = true;
                    int quantity = int.Parse((string)row.Cells[3].Value) + 1;
                    row.Cells[3].Value = quantity.ToString();
                    row.Cells[4].Value = (CostofItem * quantity).ToString();
                    break;
                }
            }

            if (!itemExists)
            {
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, "포도", CostofItem, "1", CostofItem);
            }
            Total();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int CostofItem = 5000;
            bool itemExists = false;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if ((string)row.Cells[1].Value == "샤인머스캣")
                {
                    itemExists = true;
                    int quantity = int.Parse((string)row.Cells[3].Value) + 1;
                    row.Cells[3].Value = quantity.ToString();
                    row.Cells[4].Value = (CostofItem * quantity).ToString();
                    break;
                }
            }

            if (!itemExists)
            {
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, "샤인머스캣", CostofItem, "1", CostofItem);
            }
            Total();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int CostofItem = 650;
            bool itemExists = false;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if ((string)row.Cells[1].Value == "키위")
                {
                    itemExists = true;
                    int quantity = int.Parse((string)row.Cells[3].Value) + 1;
                    row.Cells[3].Value = quantity.ToString();
                    row.Cells[4].Value = (CostofItem * quantity).ToString();
                    break;
                }
            }

            if (!itemExists)
            {
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, "키위", CostofItem, "1", CostofItem);
            }
            Total();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int CostofItem = 7000;
            bool itemExists = false;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if ((string)row.Cells[1].Value == "망고")
                {
                    itemExists = true;
                    int quantity = int.Parse((string)row.Cells[3].Value) + 1;
                    row.Cells[3].Value = quantity.ToString();
                    row.Cells[4].Value = (CostofItem * quantity).ToString();
                    break;
                }
            }

            if (!itemExists)
            {
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, "망고", CostofItem, "1", CostofItem);
            }
            Total();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int CostofItem = 1800;
            bool itemExists = false;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if ((string)row.Cells[1].Value == "복숭아")
                {
                    itemExists = true;
                    int quantity = int.Parse((string)row.Cells[3].Value) + 1;
                    row.Cells[3].Value = quantity.ToString();
                    row.Cells[4].Value = (CostofItem * quantity).ToString();
                    break;
                }
            }

            if (!itemExists)
            {
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, "복숭아", CostofItem, "1", CostofItem);
            }
            Total();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int CostofItem = 1800;
            bool itemExists = false;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if ((string)row.Cells[1].Value == "배")
                {
                    itemExists = true;
                    int quantity = int.Parse((string)row.Cells[3].Value) + 1;
                    row.Cells[3].Value = quantity.ToString();
                    row.Cells[4].Value = (CostofItem * quantity).ToString();
                    break;
                }
            }

            if (!itemExists)
            {
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, "배", CostofItem, "1", CostofItem);
            }
            Total();
        }

        private void button25_Click(object sender, EventArgs e) // 비닐봉투小
        {
            int CostofItem = 50;
            bool itemExists = false;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if ((string)row.Cells[1].Value == "비닐봉투小")
                {
                    itemExists = true;
                    int quantity = int.Parse((string)row.Cells[3].Value) + 1;
                    row.Cells[3].Value = quantity.ToString();
                    row.Cells[4].Value = (CostofItem * quantity).ToString();
                    break;
                }
            }

            if (!itemExists)
            {
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, "비닐봉투小", CostofItem, "1", CostofItem);
            }
            Total();
        }

        private void button26_Click(object sender, EventArgs e) // 비닐봉투大
        {
            int CostofItem = 100;
            bool itemExists = false;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if ((string)row.Cells[1].Value == "비닐봉투大")
                {
                    itemExists = true;
                    int quantity = int.Parse((string)row.Cells[3].Value) + 1;
                    row.Cells[3].Value = quantity.ToString();
                    row.Cells[4].Value = (CostofItem * quantity).ToString();
                    break;
                }
            }

            if (!itemExists)
            {
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, "비닐봉투大", CostofItem, "1", CostofItem);
            }
            Total();
        }


    }
}