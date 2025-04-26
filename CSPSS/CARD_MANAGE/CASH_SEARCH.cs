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


namespace CSPSS.CARD_MANAGE
{
    public partial class CASH_SEARCH : Form
    {
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        protected int M_int_judge, t;
        basec bc = new basec();
        ExcelToCSHARP etc = new ExcelToCSHARP();
        Color c = System.Drawing.ColorTranslator.FromHtml("#efdaec");
        CEMPLOYEE_INFO cemployee_info = new CEMPLOYEE_INFO();
        private string _CCKEY;
        public string CCKEY
        {
            set { _CCKEY = value; }
            get { return _CCKEY; }

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
      
        public CASH_SEARCH()
        {
            InitializeComponent();
          
           
        }

        private void CASH_SEARCH_Load(object sender, EventArgs e)
        {
           
            bind();
            label2.Text = "";
            label2.ForeColor = c2;
          
        }


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

            dt = bc.GET_CASH_TOTAL();
        
            dt = bc.GET_DT_TO_DV_TO_DT(dt, "", "卡号 LIKE '%" + comboBox2.Text + "%'");

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



          /*  dt3 = bc.getdt("SELECT CARDID FROM CASH ");
            AutoCompleteStringCollection inputInfoSource_o = new AutoCompleteStringCollection();
          
  
            foreach (DataRow dr in dt3.Rows)
            {

               
                inputInfoSource_o.Add(dr["CARDID"].ToString());


            }
            this.textBox2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBox2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBox2.AutoCompleteCustomSource = inputInfoSource_o;*/
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

          
          
           
        }

      
        #endregion



        #region dgvStateControl
        private void dgvStateControl()
        {


            dataGridView1.Columns["卡编号"].Width = 100;
            dataGridView1.Columns["卡号"].Width = 100;
         
          
            dataGridView1.Columns["余额"].Width = 80;

            dataGridView1.Columns["余额"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        
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
        private void btnSearch_Click(object sender, EventArgs e)
        {

            bind();

      

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

   
  
 

   
 

    

    

      
    }
}
