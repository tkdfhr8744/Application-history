using System;
using System.Collections;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace 애플리케이션_테스트
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void connopen()
        {
            Assert.AreEqual(true, Connection());
        }

        [TestMethod]
        public void Connclose()
        {
            Assert.AreEqual(true, ConnectionClose());
        }

        [TestMethod]
        public void select()
        {
            Assert.AreEqual(3, SelectData());
        }

        [TestMethod]
        public void update()
        {
            Assert.AreEqual(true, NonQuery("update[Rule] set rName = '구디', rDesc = '가산디지털단지', modDate = getDate() where rNo = 1"));

        }
        [TestMethod]
        public void insert()
        {
            Assert.AreEqual(true, NonQuery("insert into [Rule] (rName, rDesc) values ('구디','구디아카데미');"));
        }
        [TestMethod]
        public void controler()
        {
            tbtest();
        }
        public void tbtest()
        {
            Panel pn = new Panel();
            TextBox textbox1 = new TextBox();
            Hashtable hashtable = new Hashtable();
            hashtable.Add("width", 45);
            hashtable.Add("point", new Point(0, 0));
            hashtable.Add("color", Color.Silver);
            hashtable.Add("name", "textBox1");
            hashtable.Add("enabled", false);
            textbox1 = getTextBox(hashtable, pn);
        }
        public TextBox getTextBox(Hashtable hashtable, Control parentDomain)
        {
            TextBox textBox = new TextBox();
            textBox.Width = Convert.ToInt32(hashtable["width"].ToString());
            Console.WriteLine(textBox.Width.ToString() + "(width 값입력)");
            textBox.Location = (Point)hashtable["point"];
            Console.WriteLine(textBox.Location + "(포인트)");
            textBox.BackColor = (Color)hashtable["color"];
            Console.WriteLine(textBox.BackColor + "(컬러)");
            textBox.Name = hashtable["name"].ToString();
            Console.WriteLine(textBox.Name + "(이름)");
            textBox.Enabled = (bool)hashtable["enabled"];
            parentDomain.Controls.Add(textBox);
            Console.WriteLine();
            return textBox;
        }

        SqlConnection conn;
        public bool ConnectionClose()
        {
            Connection();
            try
            {
                conn.Close();
                Console.WriteLine("끊기 성공");
                return true;
            }
            catch
            {
                Console.WriteLine("끊기 실패");
                return false;
            }
        }
        private bool Connection()
        {
            string host = "(localdb)\\ProjectsV13";
            string user = "root";
            string password = "1234";
            string db = "gdc";

            string connStr = string.Format("server={0};uid={1};password={2};database={3}", host, user, password, db);
            conn = new SqlConnection(connStr);

            try
            {
                conn = new SqlConnection();
                conn.ConnectionString = connStr;
                conn.Open();
                Console.WriteLine("연결성공");
                return true;
            }
            catch
            {
                conn.Close();
                //MessageBox.Show("MS-SQL 연결 실패!");
                return false;
            }
        }
        public SqlDataReader Reader(string sql)
        {
            try
            {
                SqlCommand comm = new SqlCommand(sql, conn);
                return comm.ExecuteReader();
            }
            catch
            {
                return null;
            }
        }
        public int SelectData()
        {
            Connection();
            string sql = "select rNo, rName, rDesc, delYn, regDate, modDate from [Rule];";
            SqlDataReader sdr = Reader(sql);
            int cnt = 0;
            while (sdr.Read())
            {
                cnt++;
            }
            return cnt;
        }
        public bool NonQuery(string sql)
        {
            Connection();
            try
            {
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
