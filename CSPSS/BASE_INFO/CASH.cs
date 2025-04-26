using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Reflection;
using XizheC;


namespace CSPSS.BASE_INFO
{
    public partial class CASH : Form
    {
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        protected int M_int_judge, t;
        basec bc = new basec();
        ExcelToCSHARP etc = new ExcelToCSHARP();
        Color c = System.Drawing.ColorTranslator.FromHtml("#efdaec");
        CEMPLOYEE_INFO cemployee_info = new CEMPLOYEE_INFO();
        private string _CSKEY;
        public string CSKEY
        {
            set { _CSKEY = value; }
            get { return _CSKEY; }

        }
        private static string _EMID;
        public static string EMID
        {
            set { _EMID = value; }
            get { return _EMID; }

        }
        private static string _ENAME;
        public static string ENAME
        {
            set { _ENAME = value; }
            get { return _ENAME; }

        }
        PERIOD pe = new PERIOD();
        private string _IDO;
        protected int select;
        public string IDO
        {
            set { _IDO = value; }
            get { return _IDO; }

        }
        private static bool _IF_DOUBLE_CLICK;
        public static bool IF_DOUBLE_CLICK
        {
            set { _IF_DOUBLE_CLICK = value; }
            get { return _IF_DOUBLE_CLICK; }

        }
        private string _ADD_OR_UPDATE;
        public string ADD_OR_UPDATE
        {
            set { _ADD_OR_UPDATE = value; }
            get { return _ADD_OR_UPDATE; }
        }
        private bool _IFExecutionSUCCESS;
        public bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }
        private string _CARDID;
        public string CARDID
        {
            set { _CARDID = value; }
            get { return _CARDID; }
        }
        Color c2 = System.Drawing.ColorTranslator.FromHtml("#990033");
        CCASH ccash = new CCASH();
        public CASH()
        {
            InitializeComponent();
          
           
        }
        private void CASH_Load(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "yyyy/MM/dd";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            textBox2.BackColor = Color.Yellow;
            textBox3.BackColor = Color.Yellow;
            comboBox1.BackColor = Color.Yellow;
            textBox3.Text = "0";
            textBox3.TextAlign = HorizontalAlignment.Right;
            textBox1.Text = IDO;
            bind();
            label2.Text = "";
            label2.ForeColor = c2;
            LENAME.Text = "";
        }
  

        #region override enter
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter && ((!(ActiveControl is System.Windows.Forms.TextBox) ||
                !((System.Windows.Forms.TextBox)ActiveControl).AcceptsReturn)))
            {

             
                    SendKeys.SendWait("{Tab}");
                
                return true;
            }
            if (keyData == (Keys.Enter | Keys.Shift))
            {
                SendKeys.SendWait("+{Tab}");

                return true;
            }
        
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
        #region bind
        private void bind()
        {
            hint.Location = new Point(400, 100);
            hint.ForeColor = Color.Red;
            if (bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS) != "")
            {
                hint.Text = bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS);
            }
            else
            {
                hint.Text = "";
            }
            if (ADD_OR_UPDATE == "UPDATE")
            {
               
            }
            else
            {
              
            }
      
            dt = bc.getdt("SELECT * FROM CASH");
            dataGridView1.DataSource = bc.getdt(ccash.sql);
            dgvStateControl();
            textBox2.Focus();

            //this.WindowState = FormWindowState.Maximized;
            think();
            

        }
        #endregion
        #region think
        private void think()
        {

            dt2 = bc.getdt("SELECT CARDID FROM CASH ");
            AutoCompleteStringCollection inputInfoSource = new AutoCompleteStringCollection();
            AutoCompleteStringCollection inputInfoSource4 = new AutoCompleteStringCollection();
            comboBox2.Items.Clear();
                foreach (DataRow dr in dt2.Rows)
                {

                    comboBox2.Items.Add(dr["CARDID"].ToString());
                    inputInfoSource.Add(dr["CARDID"].ToString());


                }
            this.comboBox2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.comboBox2.AutoCompleteCustomSource = inputInfoSource;


        }
        #endregion
  

        
        #region bind1
        private void bind(DataTable dt)
        {

            try
            {
                if (dt.Rows.Count > 0)
                {
                    textBox1.Text = dt.Rows[0]["CSID"].ToString();
                    textBox2.Text = dt.Rows[0]["CARDID"].ToString();
                   
                    bind2(dt.Rows[0]["CARDID"].ToString());
                  
                }
                think();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }
        #endregion
        #region bind2
        private void bind2(string CARDID)
        {
       
        }
        #endregion
       
        #region save
        protected void save()
        {

            ccash.EMID = LOGIN.EMID;
            ccash.CSKEY = CSKEY;
            ccash.CSID = textBox1.Text;
            ccash.CARDID = textBox2.Text;
            ccash.HANDLER_ID = comboBox1.Text;
            ccash.BILL_DATE = dateTimePicker1.Value.ToString("yyyy/MM/dd").Replace ("-","/");
            ccash.CASH = textBox3.Text;
            ccash.REMARK = "";
            ccash.save();
            //hint.Text = ccash.ErrowInfo;
     
            ADD_OR_UPDATE = ccash.ADD_OR_UPDATE;
          
           
        }

        private void COURSE_TYPE_LOAD()
        {
            if (textBox2.Text.Length > 0)
            {
                int k = Convert.ToInt32(textBox2.Text.Substring(0, 1));
                dt = etc.GetCOURSE_TypeData(k);
            }

            if (dt.Rows.Count > 0)
            {

                //bind(dt);
            }
            else
            {
                textBox1.Text = "";
                ClearText();
               
            }
            think();
            textBox2.Focus();
       
          
   
            LoadAgain();
            //
        }
        #endregion



        #region dgvStateControl
        private void dgvStateControl()
        {
            
            //dataGridView1.Columns["汇率"].Width = 40;
            dataGridView1.Columns["卡编号"].Width = 100;
            dataGridView1.Columns["卡号"].Width = 100;
            //dataGridView1.Columns["日期"].Width = 80;
          
            dataGridView1.Columns["单据日期"].Width = 80;
           
      
            dataGridView1.Columns["开卡金额"].Width = 80;
            
            dataGridView1.Columns["制单人"].Width = 80;
            dataGridView1.Columns["制单日期"].Width = 120;
  
            dataGridView1.Columns["开卡金额"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        
            int i;
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Lavender;
            int numCols1 = dataGridView1.Columns.Count;
            for (i = 0; i < numCols1; i++)
            {

                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;


                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.Columns[i].HeaderCell.Style.BackColor = Color.Lavender;

            }
            for (i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[i].DefaultCellStyle.BackColor = Color.OldLace;
                i = i + 1;
            }

            for (i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (i == 6)
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }
                else
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }
                if (i == 0)
                {
                    dataGridView1.Columns[i].Visible = true;

                }
            }


        }
        #endregion

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("只能输入数字！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
        }
        #region excelprint
        private void btnExcelPrint_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #region btnadd

        #endregion
        #region loadagain
        private void LoadAgain()
        {
            ClearText();
            string a1 = ccash.GETID();
            if (a1 == "Exceed Limited")
            {
                MessageBox.Show("编码超出限制！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                textBox1.Text = a1;
            }
            //dataGridView1.DataSource = total1();
        }
        #endregion
        private void ClearText()
        {
            textBox2.Text = "";
            comboBox1.Text = "";
            LENAME.Text = "";
            textBox3.Text = "";
            dateTimePicker1.Text = DateTime.Now.ToString("yyyy/MM/dd").Replace("-", "/");
            textBox1.Text = ccash.GETID();
       
          
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            save1();

        }
        private void save1()
        {
            if (juage ())
            {

            }
   
            else
            {
                save();
                hint.Text = ccash.ErrowInfo;
                IFExecution_SUCCESS = etc.IFExecution_SUCCESS;
                if (ccash.IFExecution_SUCCESS && ccash.ADD_OR_UPDATE == "ADD")
                {
                    ClearText();
                    bind();
                }
                else if (ccash.IFExecution_SUCCESS && ccash.ADD_OR_UPDATE == "UPDATE")
                {
                    bind();

                }
            }

            try
            {
           
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
        }
        private bool juage()
        {

            bool b = false;
            CSKEY = bc.numYMD(20, 12, "000000000001", "select * from CASH", "CSKEY", "CS");
            if (textBox2.Text == "")
            {
                hint.Text  = "卡号不能为空！";
                b = true;
            }
            else if (!bc.exists("SELECT * FROM EMPLOYEEINFO WHERE EMID='" + comboBox1 .Text  + "'"))
            {
                hint.Text  = "员工工号不存在于系统中！";
                b = true;
            }
            else if (textBox3.Text  == "")
            {

                hint.Text  = "开卡金额不能为空！";
                b = true;
            }
            else if (bc.yesno(textBox3.Text ) == 0)
            {

                hint.Text = "开卡金额只能输入数字！";
                b = true;
            }

            else if (CSKEY == "Exceed Limited")
            {
                hint.Text  = "编码超出限制！";
                b = true;
            }
            else if (bc.JuageDeleteCASH_MoreThanStorageCASH(textBox1.Text, textBox1.Text))
            {
                hint.Text = bc.ErrowInfo;
            }
            /*else if (textBox10.textBox == "0.00")
            {
                hint.textBox = "归还套数需大于0！";
                b = false;
            }
           */
            return b;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #region btndel
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (bc.exists("SELECT * FROM CASH_ADD A LEFT JOIN GODE B ON A.CDID=B.GODEID WHERE B.CSID='" + textBox1.Text + "'"))
                {
                    hint.Text = "该卡号已经有冲值记录，不允许删除！";
                }
                else if (bc.JuageDeleteCASH_MoreThanStorageCASH(textBox1.Text, textBox1.Text))
                {
                    hint.Text = bc.ErrowInfo;
                }
                else
                {
                    basec.getcoms("DELETE CASH WHERE CSID='" + textBox1.Text + "'");
                    basec.getcoms("DELETE GODE WHERE GODEID='" + textBox1.Text + "'");

                    bind();
                }
            }
           
            try
            {
          

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            
        }
        #endregion
 
        private void button1_Click(object sender, EventArgs e)
        {
            dt = etc.GetCOURSE_TypeData(1);
            bind(dt);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dt = etc.GetCOURSE_TypeData(2);
            bind(dt);
        
        }
        private void button3_Click(object sender, EventArgs e)
        {
            dt = etc.GetCOURSE_TypeData(3);
            bind(dt);
        
        }
        private void button4_Click(object sender, EventArgs e)
        {
            dt = etc.GetCOURSE_TypeData(4);
            bind(dt);
          
        }



        private void btnSearch_Click(object sender, EventArgs e)
        {

            dt = bc.getdt(ccash.sql);
            dt = bc.GET_DT_TO_DV_TO_DT(dt, "", "卡号 LIKE '%"+ comboBox2 .Text + "%'");
            if (dt.Rows.Count > 0)
            {
                hint.Text = "";
                dataGridView1.DataSource = dt;
                dgvStateControl();
             
            }
            else
            {
                dataGridView1.DataSource = null;
                hint.Text = "不存在搜速记录";

            }

      

        }

        private void btnToExcel_Click(object sender, EventArgs e)
        {
           
        }
     private void aws(TreeNode trd )
     {
         //MessageBox.Show(trd.Text);
         if (trd.Text ==comboBox2 .Text )
         {
             trd.BackColor = c;
             trd.Checked = true;

          
         }
         foreach (TreeNode trd1 in trd.Nodes)
         {
          
             //MessageBox.Show(trd1.Text);
             aws(trd1);

         }



      }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {

            }
            else if (textBox2.Text.Length > 4)
            {

                bind2(textBox2.Text.Substring(0, 4));


            }

        }

     
        public void a5()
        {
            select = 1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            LoadAgain();
            textBox2.Focus();
    
        
        }
   
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar ==13)
            {
                save1();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            BASE_INFO.EMPLOYEE_INFO FRM = new CSPSS.BASE_INFO.EMPLOYEE_INFO();
            FRM.IDO = cemployee_info.GETID();
            FRM.CASH();
            FRM.ShowDialog();
            this.comboBox1.IntegralHeight = false;//使组合框不调整大小以显示其所有项
            this.comboBox1.DroppedDown = false;//使组合框不显示其下拉部分
            this.comboBox1.IntegralHeight = true;//恢复默认值
            if (IF_DOUBLE_CLICK)
            {
                comboBox1.Text = EMID;
                LENAME.Text = ENAME;
            }
            textBox3.Focus();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim() == "")
            {
            }
            else
            {
                textBox1.Text = Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim();
                textBox2.Text = Convert.ToString(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value).Trim();
                comboBox1.Text = Convert.ToString(dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value).Trim();
                textBox3.Text = Convert.ToString(dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value).Trim();
                dateTimePicker1.Text = Convert.ToString(dataGridView1[4, dataGridView1.CurrentCell.RowIndex].Value).Trim();
                LENAME.Text = Convert.ToString(dataGridView1[5, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            }
        }

    

      
    }
}
