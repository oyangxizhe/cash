using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using XizheC;

namespace XizheC
{
    public class ExcelToCSHARP
    {

        private string _getsql;
        public string getsql
        {
            set { _getsql = value; }
            get { return _getsql; ; }

        }
        private string _COURSE_TYPE;
        public string COURSE_TYPE
        {
            set { _COURSE_TYPE = value; }
            get { return _COURSE_TYPE; }

        }
        private string _REMARK;
        public string REMARK
        {
            set { _REMARK = value; }
            get { return _REMARK; }

        }
        private bool _IFExecutionSUCCESS;
        public bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }
        private string _CSID;
        public string CSID
        {
            set { _CSID = value; }
            get { return _CSID; }
        }
        private string _CARDID;
        public string CARDID
        {
            set { _CARDID = value; }
            get { return _CARDID; }


        }
        private string _EMID;
        public  string EMID
        {
            set { _EMID = value; }
            get { return _EMID; }

        }
        private bool _IfFirstDetailCourse;
        public bool IfFirstDetailCourse
        {
            set { _IfFirstDetailCourse = value; }
            get { return _IfFirstDetailCourse; }
        }
        private bool _IFCONSULENZA;
        public bool IFCONSULENZA
        {
            set { _IFCONSULENZA = value; }
            get { return _IFCONSULENZA; }
        }
        private string _hint;
        public string hint
        {
            set { _hint = value; }
            get { return _hint; }
        }
        private string _ADD_OR_UPDATE;
        public string ADD_OR_UPDATE
        {
            set { _ADD_OR_UPDATE = value; }
            get { return _ADD_OR_UPDATE; }
        }
      private string _CASH;
        public string CASH
        {
            set { _CASH = value; }
            get { return _CASH; }

        }
          private string _HANDLER_ID;
        public string HANDLER_ID
        {
            set { _HANDLER_ID = value; }
            get { return _HANDLER_ID; }

        }
        private string _CSKEY;
        public string CSKEY
        {
            set { _CSKEY = value; }
            get { return _CSKEY; }

        }
        private string _BILL_DATE;
        public string BILL_DATE
        {
            set { _BILL_DATE = value; }
            get { return _BILL_DATE; }

        }
        string sql = @"
SELECT 
A.CSID AS CSID,
A.CARDID AS CARDID,
A.BILL_DATE AS BILL_DATE,
A.HANDLER_ID AS HANDLER_ID,
A.REMARK AS REMARK,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=B.MAKERID ) AS MAKER,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.HANDLER_ID ) AS HANDLER,
B.CASH,
B.USER_GROUP AS USER_GROUP,
B.MAKERID,
B.DATE
FROM CASH A 
LEFT JOIN GODE B ON A.CSKEY=B.GEKEY
";
        string sql1 = @"INSERT INTO CASH(
INSERT INTO 
CASH
(
CSKEY,
CSID,
HANDLER_ID,
BILL_DATE,
REMARK,
YEAR,
MONTH,
DAY
)
VALUES
(
@CSKEY,
@CSID,
@HANDLER_ID,
@BILL_DATE,
@REMARK,
@YEAR,
@MONTH,
@DAY
)
";


        string sql2 = @"UPDATE CASH SET 
UPDATE CASH SET 
BILL_DATE=@BILL_DATE,
HANDLER_ID=@HANDLER_ID,
REMARK=@REMARK,
YEAR=@YEAR,
MONTH=@MONTH,
DAY=@DAY
";
        basec bc = new basec();
        DataTable dt = new DataTable();
        DataTable dto = new DataTable();
        public ExcelToCSHARP()
        {
            IFExecution_SUCCESS = true;
            getsql = sql;
          

        }

        #region SQlcommandE
        protected void SQlcommandE(string sql)
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy/MM/dd HH:mm:ss").Replace("-", "/");
            //string varMakerID = bc.getOnlyString("SELECT EMID FROM USERINFO WHERE USID='" + n2 + "'");
            string varMakerID = EMID;
            SqlConnection sqlcon = bc.getcon();
            SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
            sqlcom.Parameters.Add("@CSKEY", SqlDbType.VarChar, 20).Value = CSKEY;
            sqlcom.Parameters.Add("@CSID", SqlDbType.VarChar, 20).Value = CSID ;
            sqlcom.Parameters.Add("@HANDLER_ID", SqlDbType.VarChar, 20).Value =HANDLER_ID ;
            sqlcom.Parameters.Add("@BILL_DATE", SqlDbType.VarChar, 20).Value = BILL_DATE ;
            sqlcom.Parameters.Add("@REMARK", SqlDbType.VarChar, 1000).Value =REMARK ;
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
            sqlcon.Open();
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
        }
        #endregion

        #region SQlcommandE
        protected void SQlcommandE(string sql, string GEKEY)
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");

            string varMakerID = EMID;

            SqlConnection sqlcon = bc.getcon();
            SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
            sqlcom.Parameters.Add("@GEKEY", SqlDbType.VarChar, 20).Value = GEKEY;
            sqlcom.Parameters.Add("@GODEID", SqlDbType.VarChar, 20).Value = CSID;
            sqlcom.Parameters.Add("@CSID", SqlDbType.VarChar, 20).Value = CSID;
            sqlcom.Parameters.Add("@CASH", SqlDbType.VarChar, 20).Value = CASH;
            sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@DEBIT_MAKERID", SqlDbType.VarChar, 20).Value = varMakerID;
            sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = varMakerID;
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
            sqlcon.Open();
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
        }
        #endregion
        private bool JuageCARDIDFormat(int i,DataTable  dt)
        {
            bool b = false;
            DataTable dtt = bc.getdt("SELECT * FROM CASH");
                if (JuageCARDIDFormatt(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(),dt.Rows[i][4].ToString(),i))
                {
                  
                    b = true;
                    //break;
                }
                for (int j = 0; j < dtt.Rows.Count; j++)
                {

                    if (dt.Rows[i][0].ToString() == dtt.Rows[j]["CARDID"].ToString())
                    {

                        MessageBox.Show("卡号：" + dt.Rows[i][0].ToString() + " 已经存在系统中！");
                        b = true;
                        break;

                    }
                    else if (dt.Rows[i][1].ToString() == dtt.Rows[j]["ACNAME"].ToString())
                    {

                        MessageBox.Show("科目名称：" + dt.Rows[i][1].ToString() + " 已经存在系统中！");
                        b = true;
                        break;

                    }
                }
            
            return b;
        }
        #region JuageCARDIDFormat()
        public bool JuageCARDIDFormatt(string CARDID, string ACNAME, string COURSE_TYPE, string BALANCE_DIRECTION,string CYCODE,int i)
        {
            List<string> list = this.getCOURSE_TYPE_INFO();
            List<string> list1 = this.getBALANCE_DIRECTION_INFO();
            int n = CARDID.Length;
            bool b = false;
          
            if (CARDID == "")
            {

                b = true;
                MessageBox.Show("第" + i + "行" + "卡号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (ACNAME == "")
            {
                b = true;
                MessageBox.Show("第" + i + "行" + "科目名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (COURSE_TYPE == "")
            {
                b = true;
                MessageBox.Show("卡号为" + CARDID + "的科目类别不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (BALANCE_DIRECTION == "")
            {
                b = true;
                MessageBox.Show("卡号为" + CARDID + "的借贷方向不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (bc.JuageIfAllowKEYIN(list, COURSE_TYPE, "科目类别不存在！"))
            {
                b = true;

            }
            else if (bc.JuageIfAllowKEYIN(list1, BALANCE_DIRECTION, "余额方向只能为：借,贷"))
            {
                b = true;

            }
            else if (bc.yesno(CARDID) == 0)
            {
                b = true;
                MessageBox.Show("卡号：" + CARDID + "只能输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (n != 4 && n != 7 && n != 10 && n != 13 && n != 16 && n != 19)
            {
                b = true;
                MessageBox.Show("卡号格式不正确，需为4-3-3-3-3-3！" + Convert.ToString(n), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (checkLastCARDIDIfNoExists("CASH", "CARDID", CARDID, "") == 0)
            {
                b = true;
            }

            else if (CYCODE =="")
            {
                b = true;
                MessageBox.Show("第" + i + "行" + "币别不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (!bc.exists("SELECT * FROM CURRENCY_MST WHERE CYCODE='" + CYCODE + "'"))
            {
                b = true;
                MessageBox.Show("第" + i + "行" + "币别不存在于系统中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            return b;
        }
        #endregion

        #region JuageCARDIDFormat()
        public bool JuageCARDIDFormatt(string CARDID, string ACNAME, string COURSE_TYPE, string BALANCE_DIRECTION)
        {
            List<string> list = this.getCOURSE_TYPE_INFO();
            List<string> list1 = this.getBALANCE_DIRECTION_INFO();
            int n = CARDID.Length;
            bool b = false;
            if (CARDID == "")
            {

                b = true;
                MessageBox.Show("卡号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (ACNAME == "")
            {
                b = true;
                MessageBox.Show("科目名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (COURSE_TYPE == "")
            {
                b = true;
                MessageBox.Show("科目类别不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (BALANCE_DIRECTION == "")
            {
                b = true;
                MessageBox.Show("借贷方向不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (bc.JuageIfAllowKEYIN(list, COURSE_TYPE, "科目类别不存在！"))
            {
                b = true;

            }
            else if (bc.JuageIfAllowKEYIN(list1, BALANCE_DIRECTION, "余额方向只能为：借,贷"))
            {
                b = true;

            }
            else if (bc.yesno(CARDID) == 0)
            {
                b = true;
                MessageBox.Show("卡号：" + CARDID + "只能输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (n != 4 && n != 7 && n != 10 && n != 13 && n != 16 && n != 19)
            {
                b = true;
                MessageBox.Show("卡号格式不正确，需为4-3-3-3-3-3！" + Convert.ToString(n), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (checkLastCARDIDIfNoExists("CASH", "CARDID", CARDID, "") == 0)
            {
                b = true;
            }
            return b;
        }
        #endregion
        #region importExcelToDataSet
        public static DataSet importExcelToDataSet(string FilePath, string tablename)
        {
            string strConn;
            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + FilePath + ";Extended Properties='Excel 8.0;HDR=No;IMEX=1'";
            OleDbConnection conn = new OleDbConnection(strConn);
            OleDbDataAdapter myCommand = new OleDbDataAdapter("SELECT * FROM [" + tablename + "] ", strConn);
            DataSet myDataSet = new DataSet();
            try
            {
                myCommand.Fill(myDataSet);
            }
            catch (Exception ex)
            {
                MessageBox.Show("error," + ex.Message);
            }
            return myDataSet;
        }
        #endregion
        #region GetExcelFirstTableName
        public static string GetExcelFirstTableName(string excelFileName)
        {
            string tableName = null;
            if (File.Exists(excelFileName))
            {
                using (OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet." +
                  "OLEDB.4.0;Extended Properties=\"Excel 8.0\";Data Source=" + excelFileName))
                {
                    conn.Open();
                    DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    tableName = dt.Rows[0][2].ToString().Trim();

                }
            }
            return tableName;
        }
        #endregion

        
 

        #region save
        public void save(string CSID, string CARDID, string ACNAME, string COURSE_TYPE, string BALANCE_DIRECTION,string CYCODE,string COURSE_NATURE)
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy/MM/dd HH:mm:ss").Replace("-", "/");
            string v1 = bc.getOnlyString("SELECT CARDID FROM CASH WHERE  CSID='" + CSID + "'");
        
          
            //string varMakerID;
            if (!bc.exists("SELECT CSID FROM CASH WHERE CSID='" + CSID + "'"))
            {
                if (bc.exists("SELECT * FROM CASH WHERE CARDID='" + CARDID + "'"))
                {
                    IFExecution_SUCCESS = false;
                   
                    hint = "卡号已经存在于系统！";

                }
             
                else
                {
                    IFExecution_SUCCESS = true;

                    SQlcommandE(sql1 );
                    ADD_OR_UPDATE = "ADD";
                }

            }
        
     
       
            else if (v1 != CARDID )
            {
                if (bc.exists("SELECT * FROM CASH WHERE CARDID='" + CARDID + "'"))
                {
                    IFExecution_SUCCESS = false;
                    hint = "卡号已经存在于系统！";

                }
         
                else
                {
                    IFExecution_SUCCESS = true;
                    SQlcommandE(sql2 + " WHERE CSID='" + CSID + "'");
                    ADD_OR_UPDATE = "UPDATE";

                }
            }
            else
            {
                IFExecution_SUCCESS = true;
                SQlcommandE(sql2 + " WHERE CSID='" + CSID + "'");
                ADD_OR_UPDATE = "UPDATE";


            }
        }
        #endregion
        #region GetCOURSE_TypeData
        public DataTable GetCOURSE_TypeData(int k)
        {



           
            try
            {
                dt = bc.getdt(sql + " WHERE SUBSTRING(CARDID,1,1)='" + Convert.ToString(k) + "' ORDER BY CARDID ASC");

            }
            catch (Exception)
            {

            }
            return dt;
        }
        #endregion
        #region GetCOURSE_LoadData
        public DataTable GetCOURSE_LoadData()
        {
            DataTable dto = new DataTable();
            dt = bc.getdt(sql + " ORDER BY CARDID ASC");
            if (dt.Rows.Count > 0)
            {
                dto = dt;
            }
            return dto;
        }
        #endregion
        #region Search()
        public DataTable Search(string CARDID, string ACNAME)
        {

            string sql1 = @" where A.CARDID like '%" + CARDID + "%' AND A.ACNAME LIKE '%" + ACNAME + "%' ORDER BY CARDID ASC";
            dt = basec.getdts(sql + sql1);
            return dt;
        }
        #endregion
        #region GetLastCourseAnd_CurrentCourseName
        public string GetLastCourseAnd_CurrentCourseName(string CARDID)
        {
            string v1, v2, v3, v4, v5;
            string GET_NEWACNAME = "";
            if (CARDID.Length > 0)
            {


                if (CARDID.Length == 7)
                {
                    v1 = bc.getOnlyString("SELECT ACNAME FROM CASH WHERE CARDID='" + CARDID.Substring(0, 4) + "'");
                    GET_NEWACNAME = v1 + " - " + bc.getOnlyString("SELECT ACNAME FROM CASH WHERE CARDID='" + CARDID.Substring(0, 7) + "'");

                }
                else if (CARDID.Length == 10)
                {
                    v1 = bc.getOnlyString("SELECT ACNAME FROM CASH WHERE CARDID='" + CARDID.Substring(0, 4) + "'");
                    v2 = bc.getOnlyString("SELECT ACNAME FROM CASH WHERE CARDID='" + CARDID.Substring(0, 7) + "'");
                    GET_NEWACNAME = v1 + " - " + v2 + " - " +
                        bc.getOnlyString("SELECT ACNAME FROM CASH WHERE CARDID='" + CARDID.Substring(0, 10) + "'");
                }
                else if (CARDID.Length == 13)
                {
                    v1 = bc.getOnlyString("SELECT ACNAME FROM CASH WHERE CARDID='" + CARDID.Substring(0, 4) + "'");
                    v2 = bc.getOnlyString("SELECT ACNAME FROM CASH WHERE CARDID='" + CARDID.Substring(0, 7) + "'");
                    v3 = bc.getOnlyString("SELECT ACNAME FROM CASH WHERE CARDID='" + CARDID.Substring(0, 10) + "'");
                    GET_NEWACNAME = v1 + " - " + v2 + " - " + v3 + " - " +
                        bc.getOnlyString("SELECT ACNAME FROM CASH WHERE CARDID='" + CARDID.Substring(0, 13) + "'");
                }
                else if (CARDID.Length == 16)
                {
                    v1 = bc.getOnlyString("SELECT ACNAME FROM CASH WHERE CARDID='" + CARDID.Substring(0, 4) + "'");
                    v2 = bc.getOnlyString("SELECT ACNAME FROM CASH WHERE CARDID='" + CARDID.Substring(0, 7) + "'");
                    v3 = bc.getOnlyString("SELECT ACNAME FROM CASH WHERE CARDID='" + CARDID.Substring(0, 10) + "'");
                    v4 = bc.getOnlyString("SELECT ACNAME FROM CASH WHERE CARDID='" + CARDID.Substring(0, 13) + "'");
                    GET_NEWACNAME = v1 + " - " + v2 + " - " + v3 + " - " + v4 + " - " +
                       bc.getOnlyString("SELECT ACNAME FROM CASH WHERE CARDID='" + CARDID.Substring(0, 16) + "'");
                }
                else if (CARDID.Length == 19)
                {
                    v1 = bc.getOnlyString("SELECT ACNAME FROM CASH WHERE CARDID='" + CARDID.Substring(0, 4) + "'");
                    v2 = bc.getOnlyString("SELECT ACNAME FROM CASH WHERE CARDID='" + CARDID.Substring(0, 7) + "'");
                    v3 = bc.getOnlyString("SELECT ACNAME FROM CASH WHERE CARDID='" + CARDID.Substring(0, 10) + "'");
                    v4 = bc.getOnlyString("SELECT ACNAME FROM CASH WHERE CARDID='" + CARDID.Substring(0, 13) + "'");
                    v5 = bc.getOnlyString("SELECT ACNAME FROM CASH WHERE CARDID='" + CARDID.Substring(0, 16) + "'");
                    GET_NEWACNAME = v1 + " - " + v2 + " - " + v3 + " - " + v4 + " - " + v5 + " - " +
                      bc.getOnlyString("SELECT ACNAME FROM CASH WHERE CARDID='" + CARDID.Substring(0, 19) + "'");
                }
                else
                {

                    GET_NEWACNAME = bc.getOnlyString("SELECT ACNAME FROM CASH WHERE CARDID='" + CARDID + "'");

                }

            }
            return GET_NEWACNAME;
        }
        #endregion
        #region dgvNoShowCourseType
        public DataTable dgvNoShowCourseType(DataTable dt)
        {

            DataTable dt4 = new DataTable();
            dt4.Columns.Add("CSID", typeof(string));
            dt4.Columns.Add("CARDID", typeof(string));
            dt4.Columns.Add("ACNAME", typeof(string));
            dt4.Columns.Add("MAKER", typeof(string));
            dt4.Columns.Add("DATE", typeof(string));
            dt4.Columns.Add("PARENT_NODEID", typeof(string));
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow dr1 = dt4.NewRow();
                    dr1["CSID"] = dr["CSID"].ToString();
                    dr1["CARDID"] = dr["CARDID"].ToString();
                    dr1["ACNAME"] = dr["ACNAME"].ToString();
                    dr1["MAKER"] = dr["MAKER"].ToString();
                    dr1["DATE"] = dr["DATE"].ToString();
                    dr1["PARENT_NODEID"] = dr["PARENT_NODEID"].ToString();
                    dt4.Rows.Add(dr1);

                }
            }
            return dt4;
        }
        #endregion
        public List<string> getCOURSE_TYPE_INFO()
        {
            List<string> list1 = new List<string>();
            string[] xw = new string[] { 
"流动资产",
"长期资产",
"流动负债",
"长期负债",
"共同",
"所有者权益",
"成本",
"营业收入",
"其它收益",
"其它损失",
"营业成本及税金",
"营业税金及附加",
"期间费用",
"所得税",
"以前年度损益调整"


            };
            for (int i = 0; i < xw.Length; i++)
            {

                list1.Add(xw[i]);
            }
            return list1;
        }
        #region getBALANCE_DIRECTION_INFO
        public List<string> getBALANCE_DIRECTION_INFO()
        {
            List<string> list1 = new List<string>();
            string[] xw = new string[] { "借", "贷" };
            for (int i = 0; i < xw.Length; i++)
            {

                list1.Add(xw[i]);
            }
            return list1;
        }
        #endregion
        public bool JuageFirstDetailCourse(string CARDID)
        {
            bool ju = false;
            dt = bc.getdt("SELECT * FROM CASH WHERE CARDID LIKE '%" + CARDID + "%'");
            if (dt.Rows.Count == 1)
            {
                ju = true;
                IfFirstDetailCourse = true;
            }
            return ju;
        }
        public bool JuageIf_CONSULENZA(string CSID)
        {
            bool ju = false;
            dt = bc.getdt("SELECT * FROM VOUCHER_DET WHERE CSID LIKE '%" + CSID + "%'");
            if (dt.Rows.Count > 0)
            {
                ju = true;
                IFCONSULENZA = true;

            }
            return ju;
        }
        #region CheckKeyInValueIfExistsDetailCourse
        public int CheckKeyInValueIfExistsDetailCourse(string TABLENAME, string COLUMN_NAME, string COLUMN_VALUE, string REMARK,string REMARKT)
        {
            int ju = 0;
            int len = COLUMN_VALUE.Length;
            int len1 = len + 3;
            DataTable dt = bc.getdt("SELECT *  FROM " + TABLENAME + " WHERE SUBSTRING(" + COLUMN_NAME + ",1," + len + 
                ")='"+COLUMN_VALUE+"'"+" AND LEN("+COLUMN_NAME+")="+len1);
           
            if (dt.Rows.Count == 1)
            {
                ju = 1;
                MessageBox.Show(REMARK + " " + COLUMN_VALUE + REMARKT , "提示", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            }
            else if (dt.Rows.Count > 1)
            {
                ju = 2;
                MessageBox.Show(REMARK + " " + COLUMN_VALUE + REMARKT, "提示", MessageBoxButtons.OK,
               MessageBoxIcon.Information);
            }
 
            return ju;
        }
        #endregion
        #region JuageOnlayOneDetailCourse
        public int JuageOnlayOneDetailCourse(string TABLENAME, string COLUMN_NAME, string COLUMN_VALUE, string REMARK)
        {
            int ju = 0;
            int len = COLUMN_VALUE.Length;
            int len2 = len - 3;
            hint = null;
            DataTable dt1 = bc.getdt("SELECT *  FROM " + TABLENAME + " WHERE SUBSTRING(" + COLUMN_NAME + " ,1," + len2 + ")" +
                "= SUBSTRING('" + COLUMN_VALUE + "' ,1," + len2 + ")" + " AND LEN(" + COLUMN_NAME + ")=" + len);
             if (dt1.Rows.Count == 1) /*no exists detail course and same grade only one course*/
            {
                ju = 3;
           
                if (JuageIf_CONSULENZA(dt1.Rows[0]["CSID"].ToString()))
                {

                    CSID = bc.getOnlyStringO("CASH", "CSID", "CARDID", COLUMN_VALUE.Substring(0, len2));
                    CARDID= bc.getOnlyStringO("CASH", "CARDID", "CARDID", COLUMN_VALUE.Substring(0, len2));
                    hint = REMARK + " " + COLUMN_VALUE +
                        " 为明细科目，且同级别下只有该科目一个，如果删除该科目该科目本年度发生的金额将结转到其上级科目" + CARDID + "上！是否要继续？";
                }
              
            }
            else if (dt1.Rows.Count > 1)
            {
                ju = 4;
                /*MessageBox.Show(REMARK + " " + COLUMN_VALUE + "为明细科目，且同级别下有多个同级科目！",
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);*/
            }
          
            return ju;
        }
        #endregion
        #region  checkLastCARDIDIfNoExists
        public int checkLastCARDIDIfNoExists(string TABLENAME, string COLUMN_NAME, string COLUMN_VALUE, string REMARK)
        {
            int ju = 0;
            int len = COLUMN_VALUE.Length;
            int len1 = len -3;
            if (len > 4)
            {
                DataTable dt = bc.getdt("SELECT *  FROM " + TABLENAME + " WHERE " + COLUMN_NAME + " =SUBSTRING('" + COLUMN_VALUE + "',1," + len1 + ")");
                if (dt.Rows.Count == 1)
                {
                    ju = 1;
                    CARDID = dt.Rows[0]["CARDID"].ToString();
                    CSID = dt.Rows[0]["CSID"].ToString();
                    JuageFirstDetailCourse(CARDID);
                    JuageIf_CONSULENZA(CSID);
                }
                else
                {
                    MessageBox.Show("卡号" + COLUMN_VALUE + "不存在上级科目！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {
                ju = 1;

            }
            return ju;
        }
        #endregion

        #region CHECK_DATATABLE_IF_EXISTS_DETAIL_COURSE()
        public bool CHECK_DATATABLE_IF_EXISTS_DETAIL_COURSE(DataTable dt)
        {
            bool b = false;

            for (int k = 0; k < dt.Rows.Count; k++)
            {
                if (juage(k,dt))
                {
                    b = true;
                    break;
                }
            }
            return b;
        }
        #endregion
        #region juage()
        private bool juage(int k,DataTable dt)
        {
            bool b = false;
            string v1 = dt.Rows[k]["卡号"].ToString();
            string v2 = dt.Rows[k]["累计借方"].ToString();
            string v3 = dt.Rows[k]["累计贷方"].ToString();
            string v4 = dt.Rows[k]["期初借方"].ToString();
            string v5 = dt.Rows[k]["期初贷方"].ToString();

            if ((v2 != "" || v3 != "" || v4 != "" || v5 != "") &&
                CheckKeyInValueIfExistsDetailCourse("CASH", "CARDID", v1, "卡号", "存在明细科目，需使用明细科目记帐！") == 1)
            {
                b = true;
            }
            return b;
        }
        #endregion
    }
}
