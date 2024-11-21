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

        public int Cost_of_Items()  // dataGridView1�� ��� �࿡ ���� 5��° ���� ���� �ջ��ϴ� ����� ����
        {
            int sum = 0;
            int i = 0;

            for (i = 0; i < dataGridView1.Rows.Count; i++) // dataGridView1���� ����ŭ �ݺ�
            {
                sum = sum + Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value); // dataGridView1 i��° ���� 5��° ���� ��(index ��ȣ 4)�� 
            }                                                                      // ���������� ��ȯ�Ͽ� sum�� ������ ����

            return sum; // ��ǰ�� ������ ��ȯ
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Total() // �հ� �ڵ� ����ϴ� ������ ����
        {
            if (dataGridView1.Rows.Count > 0)            // ���� ���� �ϳ� �̻��� �� ����
                total.Text = Cost_of_Items().ToString(); // ��ǰ�� ������ ���������� ��ȯ�Ͽ� ���
        }

        private void Change() // dataGridView1�� �ִ� ��� ��ǰ���� �� ������ ����ϰ�,
                              // �� ����� ���� ������ ������ ���� �Ž������� ����ϰ�, �� ����� change�� ����ϴ� ����
        {
            int items, money;                                   // items, money�� ���� ���� ���� 
            if (dataGridView1.Rows.Count > 0)                   // ���� ���� �ϳ� �̻��� �� ����
            {
                items = Cost_of_Items();                        // Cost_of_items �޼ҵ带 ȣ���Ͽ� ��� ��ǰ ���� ��� �� ����� items ������ ����
                money = Convert.ToInt32(receivedMoney.Text);    // receivedMoney�� �ؽ�Ʈ ���� ���������� ��ȯ�Ͽ� money ������ ����
                change.Text = (money - items).ToString();       // ���������� ��ǰ�� ������ �� ���� ���ڿ��� ��ȯ�Ͽ� change.Text�� ��Ÿ��
            }
        }

        private void Quantity() // �����ͱ׸������ �� �࿡ ���� ��ǰ�� �̸��� Ȯ���ϰ�, 
                                // �ش� ��ǰ�� ��� ������ �ش� ��ǰ�� �Ǹ� ������ŭ �����ϴ� ����� ����
        {
            foreach (DataGridViewRow row in this.dataGridView1.Rows)  //dataGridView1�� �ִ� �� ���� �ݺ�
            {
                switch (row.Cells[1].Value) //�����ͱ׸������ �࿡�� 2��° ���� ��ǰ���� ��Ÿ��
                {
                    case "��������":
                        numericUpDown1.Value -= int.Parse(row.Cells[3].Value.ToString());
                        //(numericUpDown1�� ��) - (�����ͱ׸������ �࿡�� 4��°���� ����)
                        continue;
                    case "������":
                        numericUpDown2.Value -= int.Parse(row.Cells[3].Value.ToString());
                        continue;

                    case "�ٳ���":
                        numericUpDown3.Value -= int.Parse(row.Cells[3].Value.ToString());
                        continue;

                    case "����":
                        numericUpDown4.Value -= int.Parse(row.Cells[3].Value.ToString());
                        continue;

                    case "���θӽ�Ĺ":
                        numericUpDown5.Value -= int.Parse(row.Cells[3].Value.ToString());
                        continue;

                    case "����":
                        numericUpDown6.Value -= int.Parse(row.Cells[3].Value.ToString());
                        continue;

                    case "�޷�":
                        numericUpDown7.Value -= int.Parse(row.Cells[3].Value.ToString());
                        continue;

                    case "����":
                        numericUpDown8.Value -= int.Parse(row.Cells[3].Value.ToString());
                        continue;

                    case "��":
                        numericUpDown9.Value -= int.Parse(row.Cells[3].Value.ToString());
                        continue;

                    case "������":
                        numericUpDown10.Value -= int.Parse(row.Cells[3].Value.ToString());
                        continue;

                    case "����":
                        numericUpDown11.Value -= int.Parse(row.Cells[3].Value.ToString());
                        continue;

                    case "Ű��":
                        numericUpDown12.Value -= int.Parse(row.Cells[3].Value.ToString());
                        continue;
                }
            }
        }


        private void button29_Click(object sender, EventArgs e) // ����(���) Ŭ��
        {
            Change(); // �Ž��� �޼ҵ� ����
            Quantity(); // ���� �޼ҵ� ����
        }

        private void button28_Click(object sender, EventArgs e) // ������ ���
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

        private void PrintReceipt(object sender, PrintPageEventArgs e)  // �����ͷ� ����� �� ����ϴ� �޼���
        {
            Graphics graphics = e.Graphics;                             // Graphics�� �׷����� ����� �� ���� ����
            Font headerFont = new Font("Arial", 16, FontStyle.Bold);    // ���� ��Ʈ�� ũ�� ����
            Font itemFont = new Font("Arial", 10);                      // ��ǰ ��Ʈ�� ũ�� ����
            Brush brush = Brushes.Black;
            int pageWidth = 827; //���� ũ�� �ʺ� ����(A4 ������)
            int pageHeight = 1169; //���� ũ�� ���� ����(A4 ������)


            //������ ����
            string headerText = "[ �Ǹ� ������ ]";                            // ��� �ؽ�Ʈ�� headerText ������ ����
            float headerX = (pageWidth - graphics.MeasureString(headerText, headerFont).Width) / 2; // graphics.MeasureString �Լ��� ���ڿ��� �׷��Ȼ󿡼�
                                                                                                    // �󸶳� ���� ������ �������� ����Ͽ� ��� ��ġ
            float headerY = (float)(pageHeight * 0.0513);   // ������ ������ �� 5.13%�� �ش��ϴ� ��ġ�� ����, ���� �ӵ��� ���� float�� �� ��ȯ
            PointF center = new PointF(headerX, headerY);   // PointF ��ü ����
            graphics.DrawString(headerText, headerFont, brush, center); // DrawString �޼ҵ� ȣ���Ͽ� �ؽ�Ʈ ����

            string smallheaderText = "[ ���� ]";     // smallgeaderText��� ������ �����̶�� ���ڿ� ����
            headerFont = new Font("Arial", 10, FontStyle.Regular);
            headerX = (pageWidth - graphics.MeasureString(smallheaderText, headerFont).Width) / 2;
            headerY = headerY + 30; // 30��ŭ ��ġ ����
            center = new PointF(headerX, headerY);
            graphics.DrawString(smallheaderText, headerFont, brush, center);

            smallheaderText = "(��) �Ŷ� ���ϰ���";
            headerFont = new Font("Arial", 10, FontStyle.Regular);
            headerX = (pageWidth - graphics.MeasureString(smallheaderText, headerFont).Width) / 2;
            headerY = headerY + 40; // 40��ŭ ��ġ ����
            center = new PointF(headerX, headerY);
            graphics.DrawString(smallheaderText, headerFont, brush, center);

            smallheaderText = "����ڹ�ȣ : 111-22-33343";
            headerFont = new Font("Arial", 10, FontStyle.Regular);
            headerX = (float)(pageWidth * 0.331);
            headerY = headerY + 20;
            center = new PointF(headerX, headerY);
            graphics.DrawString(smallheaderText, headerFont, brush, center);

            smallheaderText = "��ǥ�� : �ŵ���         TEL : 051-999-5000";
            headerFont = new Font("Arial", 10, FontStyle.Regular);
            headerX = (pageWidth - graphics.MeasureString(smallheaderText, headerFont).Width) / 2;
            headerY = headerY + 20;
            center = new PointF(headerX, headerY);
            graphics.DrawString(smallheaderText, headerFont, brush, center);

            smallheaderText = "�λ걤���� ��� ����� 700���� 140";
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

            smallheaderText = "�ǸŽð� : " + DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            headerFont = new Font("Arial", 10, FontStyle.Regular);
            headerX = (pageWidth - graphics.MeasureString(smallheaderText, headerFont).Width) / 2;
            headerY = headerY + 20;
            center = new PointF(headerX, headerY);
            graphics.DrawString(smallheaderText, headerFont, brush, center);

            smallheaderText = "�ǸŻ�� : �ڹ���";
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

            smallheaderText = "��ǰ                              �ܰ�   ����    �ݾ�";
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



            float startX = (float)(pageWidth * 0.331);  // ǰ�� ����� ���� ��ġ ����
            float startY = headerY + 80;
            int lineHeight = 20; // �� ǰ���� ���� ���� 20���� ����

            decimal total = 0; // ǰ�� ���� �հ� �ʱ�ȭ

            // ������ ���� �׸���
            for (int i = 0; i < dataGridView1.Rows.Count; i++)              // �����ͱ׸������ ��� ���� �ݺ�
            {
                DataGridViewRow row = dataGridView1.Rows[i];                // ���� �ݺ����� ó���ؾ� �� �� ������

                string itemName = row.Cells[1].Value.ToString();            // �ε�����ȣ[1]�� ��ǰ �̸� ������
                int quantity = Convert.ToInt32(row.Cells[3].Value);         // �ε�����ȣ[3]�� ������ ���� ���·� ������
                decimal price = Convert.ToDecimal(row.Cells[2].Value);      // �ε�����ȣ[2]�� ��ǰ�� ������ ������


                smallheaderText = itemName;                                 // ��ǰ �̸��� ��� 
                headerFont = new Font("Arial", 10, FontStyle.Regular);
                //headerX = (float)(headerX * 0.581);
                headerY = headerY + 20;
                center = new PointF(headerX, headerY);
                graphics.DrawString(smallheaderText, headerFont, brush, center);

                // ���ڸ� ���ڿ��� �������µ� "N0"�� �̿��Ͽ� �޸��� �ִ� ���ڷ� ��ȯ
                // ������ 3�ڸ� �̸��� ���, ������ ���� �տ� �߰� �̷��� �ϸ� ������ ª�� �� ����, ����, �� ������ ���������� ����
                // ������ 3�ڸ� �̻� 5�ڸ� �̸��� ���, �տ� �� ���� ������ �߰�
                // ������ 5�ڸ� �̻��� ���, ���� ���� ������ �տ� �߰�
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
                //ǰ��� ���� �հ� ���
                headerFont = new Font("Arial", 10, FontStyle.Regular);
                headerX = (pageWidth - graphics.MeasureString(smallheaderText, headerFont).Width) / 2;
                headerY = headerY + 20;
                center = new PointF(headerX, headerY);
                graphics.DrawString(smallheaderText, headerFont, brush, center);


                total += price * quantity; // ǰ�� ���� �ջ�
            }

            smallheaderText = "----------------------------------------------------------";
            headerFont = new Font("Arial", 10, FontStyle.Regular);
            headerX = (pageWidth - graphics.MeasureString(smallheaderText, headerFont).Width) / 2;
            headerY = headerY + 20;
            center = new PointF(headerX, headerY);
            graphics.DrawString(smallheaderText, headerFont, brush, center);

            //�հ� ���
            int totalprice = (int)total;
            smallheaderText = $"��      �� :                  {totalprice.ToString("N0")}";
            headerFont = new Font("Arial", 16, FontStyle.Bold);
            headerX = (pageWidth - graphics.MeasureString(smallheaderText, headerFont).Width) / 2;
            headerY = headerY + 20;
            center = new PointF(headerX, headerY);
            graphics.DrawString(smallheaderText, headerFont, brush, center);

            //����ڰ� ������ �ݾװ� �Ž������� �ؽ�Ʈ�ڽ����� ������
            int amountPaid = int.Parse(receivedMoney.Text); // ����ڰ� ������ �ݾ�
            int changeValue = int.Parse(change.Text); // �Ž�����

            // ����ڰ� ������ �ݾ� ���
            smallheaderText = $"�� �� �� :                  {amountPaid.ToString("N0")}";
            headerFont = new Font("Arial", 16, FontStyle.Bold);
            headerY = headerY + 30;
            center = new PointF(headerX, headerY);
            graphics.DrawString(smallheaderText, headerFont, brush, center);

            // �Ž����� ���
            smallheaderText = $"�Ž����� :                  {changeValue.ToString("N0")}";
            headerFont = new Font("Arial", 16, FontStyle.Bold);
            headerY = headerY + 30;
            center = new PointF(headerX, headerY);
            graphics.DrawString(smallheaderText, headerFont, brush, center);


        }




        private void button27_Click(object sender, EventArgs e) //Reset ��ư
        {
            try
            {
                //���� ��, �Է»���, �հ�, �Ž���, �����ͱ׸���� �� �ʱ�ȭ
                receivedMoney.Text = "0";
                receivedMoney2.Text = "0";
                total.Text = "";
                change.Text = "";
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh(); //�����ͱ׸���� ȭ�� ������Ʈ
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NumbersOnly(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            if (receivedMoney.Text == "0" || receivedMoney.Text == "00") // ���� �� �ؽ�Ʈ�� 0�Ǵ� 00�� ��
            {
                receivedMoney.Text = "";
                receivedMoney.Text = b.Text; //��ĭ���� �ʱ�ȭ �� ���� ���
            }
            else
                receivedMoney.Text = receivedMoney.Text + b.Text; //�ƴ� ��� �״�� ���� ���

            if (receivedMoney2.Text == "0" || receivedMoney2.Text == "00")
            {
                receivedMoney2.Text = "";
                receivedMoney2.Text = b.Text;
            }
            else
                receivedMoney2.Text = receivedMoney2.Text + b.Text;
        }



        private void button13_Click(object sender, EventArgs e) // �Է°� �ʱ�ȭ ��ư
        {
            receivedMoney.Text = "0";
            receivedMoney2.Text = "0";
        }

        private void button30_Click(object sender, EventArgs e) // ������� (�׸� ����)
        {
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row); //�� ����
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int CostofItem = 1700;
            bool itemExists = false;
            int quantity = 0;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if ((string)row.Cells[1].Value == "��������")
                {
                    itemExists = true;
                    quantity = int.Parse((string)row.Cells[3].Value) + 1;
                    row.Cells[3].Value = quantity.ToString();
                    row.Cells[4].Value = (CostofItem * quantity).ToString();
                    break;
                    // "��������" ��ǰ�� ���� => �ش� ��ǰ�� ������ ������Ű�� ������ ������Ʈ�մϴ�.
                }
            }

            if (!itemExists) // "��������" ��ǰ�� �̹� �����ϴ��� Ȯ��
            {
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, "��������", CostofItem, "1", CostofItem);
                // ��ǰ�� �������� �ʴ� ��쿡�� ���ο� ���� �߰��մϴ�.
            }


            Total();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            int CostofItem = 830;
            bool itemExists = false;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if ((string)row.Cells[1].Value == "������")
                {
                    itemExists = true;
                    int quantity = int.Parse((string)row.Cells[3].Value) + 1;
                    row.Cells[3].Value = quantity.ToString();
                    row.Cells[4].Value = (CostofItem * quantity).ToString();
                    break;
                    // "������" ��ǰ�� ���� => �ش� ��ǰ�� ������ ������Ű�� ������ ������Ʈ�մϴ�.
                }
            }

            if (!itemExists) // "������" ��ǰ�� �̹� �����ϴ��� Ȯ��
            {
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, "������", CostofItem, "1", CostofItem);
                // ��ǰ�� �������� �ʴ� ��쿡�� ���ο� ���� �߰��մϴ�.
            }
            Total();
        }


        //���� ����
        private void button3_Click_1(object sender, EventArgs e)
        {
            int CostofItem = 3000;
            bool itemExists = false;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if ((string)row.Cells[1].Value == "�ٳ���")
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
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, "�ٳ���", CostofItem, "1", CostofItem);
            }
            Total();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int CostofItem = 2000;
            bool itemExists = false;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if ((string)row.Cells[1].Value == "����")
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
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, "����", CostofItem, "1", CostofItem);
            }
            Total();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int CostofItem = 2000;
            bool itemExists = false;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if ((string)row.Cells[1].Value == "����")
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
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, "����", CostofItem, "1", CostofItem);
            }
            Total();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int CostofItem = 1700;
            bool itemExists = false;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if ((string)row.Cells[1].Value == "�޷�")
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
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, "�޷�", CostofItem, "1", CostofItem);
            }
            Total();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int CostofItem = 2450;
            bool itemExists = false;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if ((string)row.Cells[1].Value == "����")
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
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, "����", CostofItem, "1", CostofItem);
            }
            Total();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int CostofItem = 5000;
            bool itemExists = false;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if ((string)row.Cells[1].Value == "���θӽ�Ĺ")
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
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, "���θӽ�Ĺ", CostofItem, "1", CostofItem);
            }
            Total();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int CostofItem = 650;
            bool itemExists = false;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if ((string)row.Cells[1].Value == "Ű��")
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
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, "Ű��", CostofItem, "1", CostofItem);
            }
            Total();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int CostofItem = 7000;
            bool itemExists = false;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if ((string)row.Cells[1].Value == "����")
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
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, "����", CostofItem, "1", CostofItem);
            }
            Total();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int CostofItem = 1800;
            bool itemExists = false;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if ((string)row.Cells[1].Value == "������")
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
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, "������", CostofItem, "1", CostofItem);
            }
            Total();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int CostofItem = 1800;
            bool itemExists = false;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if ((string)row.Cells[1].Value == "��")
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
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, "��", CostofItem, "1", CostofItem);
            }
            Total();
        }

        private void button25_Click(object sender, EventArgs e) // ��Һ����
        {
            int CostofItem = 50;
            bool itemExists = false;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if ((string)row.Cells[1].Value == "��Һ����")
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
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, "��Һ����", CostofItem, "1", CostofItem);
            }
            Total();
        }

        private void button26_Click(object sender, EventArgs e) // ��Һ�����
        {
            int CostofItem = 100;
            bool itemExists = false;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if ((string)row.Cells[1].Value == "��Һ�����")
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
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, "��Һ�����", CostofItem, "1", CostofItem);
            }
            Total();
        }


    }
}