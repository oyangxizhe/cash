using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using XizheC;

namespace XizheC
{
    public class CCASH_ADD
    {
        basec bc = new basec();

        #region nature
   
        private string _WAREID;
        public string WAREID
        {
            set { _WAREID = value; }
            get { return _WAREID; }

        }
        private string _SKU;
        public string SKU
        {
            set { _SKU = value; }
            get { return _SKU; }

        }
        private string _sql;
        public string sql
        {
            set { _sql = value; }
            get { return _sql; }

        }
        private string _sqlo;
        public string sqlo
        {
            set { _sqlo = value; }
            get { return _sqlo; }

        }
        private string _sqlt;
        public string sqlt
        {
            set { _sqlt = value; }
            get { return _sqlt; }

        }
        private string _sqlth;
        public string sqlth
        {
            set { _sqlth = value; }
            get { return _sqlth; }

        }
        private string _sqlf;
        public string sqlf
        {
            set { _sqlf = value; }
            get { return _sqlf; }

        }
        private string _sqlfi;
        public string sqlfi
        {
            set { _sqlfi = value; }
            get { return _sqlfi; }

        }
        private string _MAKERID;
        public string MAKERID
        {
            set { _MAKERID = value; }
            get { return _MAKERID; }

        }

        private string _ErrowInfo;
        public string ErrowInfo
        {

            set { _ErrowInfo = value; }
            get { return _ErrowInfo; }

        }
        private string _WP_COUNT;
        public string WP_COUNT
        {
            set { _WP_COUNT = value; }
            get { return _WP_COUNT; }
        }
       
        private string _CSID;
        public string CSID
        {
            set { _CSID = value; }
            get { return _CSID; }
        }
        private string _EMID;
        public string EMID
        {
            set { _EMID = value; }
            get { return _EMID; }

        }
        private string _REMARK;
        public string REMARK
        {
            set { _REMARK = value; }
            get { return _REMARK; }

        }
        private string _CDKEY;
        public string CDKEY
        {
            set { _CDKEY = value; }
            get { return _CDKEY; }

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
        private string _CDID;
        public string CDID
        {
            set { _CDID = value; }
            get { return _CDID; }

        }
        private string _CARDID;
        public string CARDID
        {
            set { _CARDID = value; }
            get { return _CARDID; }

        }
        private string _HANDLER_ID;
        public string HANDLER_ID
        {
            set { _HANDLER_ID = value; }
            get { return _HANDLER_ID; }

        }
        private bool _IFExecutionSUCCESS;
        public bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }
        private string _BILL_DATE;
        public string BILL_DATE
        {
            set { _BILL_DATE = value; }
            get { return _BILL_DATE; }

        }
        #endregion
        #region sql
        string setsql = @"
SELECT 
A.CDID AS 冲值单号,
C.CARDID AS 卡号,
A.HANDLER_ID AS 经手人工号,
B.CASH AS 冲值金额,
A.BILL_DATE AS 单据日期,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.HANDLER_ID ) AS 经手人,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=B.MAKERID ) AS 制单人,
B.MAKERID 制单人工号,
B.DATE AS 制单日期
FROM CASH_ADD A 
LEFT JOIN GODE B ON A.CDKEY=B.GEKEY
LEFT JOIN CASH C ON B.CSID=C.CSID
";
        string setsqlo = @"
INSERT INTO 
CASH_ADD
(
CDKEY,
CDID,
HANDLER_ID,
BILL_DATE,
REMARK,
YEAR,
MONTH,
DAY
)
VALUES
(
@CDKEY,
@CDID,
@HANDLER_ID,
@BILL_DATE,
@REMARK,
@YEAR,
@MONTH,
@DAY
)
";
        string setsqlt = @"

";
        string setsqlth = @"
UPDATE CASH_ADD SET 
BILL_DATE=@BILL_DATE,
HANDLER_ID=@HANDLER_ID,
REMARK=@REMARK,
YEAR=@YEAR,
MONTH=@MONTH,
DAY=@DAY

";
        string setsqlf = @"
INSERT INTO GODE
(
GEKEY,
GODEID,
CSID,
CASH,
DATE,
MAKERID,
YEAR,
MONTH,
DAY
)
VALUES
(
@GEKEY,
@GODEID,
@CSID,
@CASH,
@DATE,
@MAKERID,
@YEAR,
@MONTH,
@DAY
)
";
        string setsqlfi = @"
UPDATE GODE SET 
CSID=@CSID,
CASH=@CASH


";
        #endregion
           public CCASH_ADD()
        {
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
        
            sql = setsql;
            sqlo = setsqlo;
            sqlt = setsqlt;
            sqlth = setsqlth;
            sqlf = setsqlf;
            sqlfi = setsqlfi;
        }
        #region
        public string GETID()
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy/MM/dd HH:mm:ss").Replace("-", "/");
            string v1 = bc.numYM(10, 4, "0001", "select * from CASH_ADD", "CDID", "CD");
            string GETID = "";
            if (v1 != "Exceed Limited")
            {
                GETID = v1;
                //bc.getcom("INSERT INTO VOUCHER_GETID(VOID,DATE,YEAR,MONTH,DAY) VALUES ('" + v1 + "','" + varDate + "','" + year + "','" + month + "','" + day + "')");
            }
            return GETID;
        }
        #endregion
        #region SQlcommandE
        protected void SQlcommandE(string sql)
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");
            //string varMakerID = bc.getOnlyString("SELECT EMID FROM USERINFO WHERE USID='" + n2 + "'");
            string varMakerID = EMID;
            SqlConnection sqlcon = bc.getcon();
            SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
            sqlcom.Parameters.Add("@CDKEY", SqlDbType.VarChar, 20).Value = CDKEY;
            sqlcom.Parameters.Add("@CDID", SqlDbType.VarChar, 20).Value = CDID;
            sqlcom.Parameters.Add("@CARDID", SqlDbType.VarChar, 20).Value = CARDID;
            sqlcom.Parameters.Add("@HANDLER_ID", SqlDbType.VarChar, 20).Value = HANDLER_ID;
            sqlcom.Parameters.Add("@BILL_DATE", SqlDbType.VarChar, 20).Value = BILL_DATE;
            sqlcom.Parameters.Add("@REMARK", SqlDbType.VarChar, 1000).Value = REMARK;
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
            string varDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");

            string varMakerID = EMID;

            SqlConnection sqlcon = bc.getcon();
            SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
            sqlcom.Parameters.Add("@GEKEY", SqlDbType.VarChar, 20).Value = GEKEY;
            sqlcom.Parameters.Add("@GODEID", SqlDbType.VarChar, 20).Value = CDID;
            sqlcom.Parameters.Add("@CDID", SqlDbType.VarChar, 20).Value = CDID;
            sqlcom.Parameters.Add("@CSID", SqlDbType.VarChar, 20).Value = CSID;
            sqlcom.Parameters.Add("@CASH", SqlDbType.VarChar, 20).Value = CASH;
            sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = varMakerID;
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
            sqlcon.Open();
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
        }
        #endregion
        #region save
        public void save()
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy/MM/dd HH:mm:ss").Replace("-", "/");
            //string varMakerID;
            if (!bc.exists("SELECT CDID FROM CASH_ADD WHERE CDID='" + CDID + "'"))
            {
          
                    IFExecution_SUCCESS = true;
                    SQlcommandE(sqlo);
                    SQlcommandE(sqlf, CDKEY);
                    ADD_OR_UPDATE = "ADD";
            }
            else
            {
               if (bc.JuageDeleteCASH_MoreThanStorageCASH(CDID, CSID ))
                {
                    ErrowInfo = bc.ErrowInfo;

                }
                else
                {
                    IFExecution_SUCCESS = true;
                    SQlcommandE(this.sqlth + " WHERE CDID='" + CDID + "'");
                    SQlcommandE(this.sqlfi + " WHERE GODEID='" + CDID + "'", CDKEY);
                    ADD_OR_UPDATE = "UPDATE";

                }

            }
         
        }
        #endregion
       
    }
}
