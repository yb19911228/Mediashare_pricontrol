using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySQLDriverCS;

namespace Mediashare_pricontrol
{
    public partial class Form1 : Form
    {
        public string DB_Address="10.1.71.175", DB_User="root", DB_Pass="123456", DB_Database="search2", Login="0";
        public Form1()
        {
            InitializeComponent();
        }

        //关闭按钮
        private void 关闭ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        //登录参数清空按钮
        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_id.Clear();
            textBox_pass.Clear();
        }

        //关于按钮
        private void 关于ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Zjicm,All Rights Reserved!");
        }

        //数据库连接测试
        public Boolean MysqlTest()
        {
            Boolean ReturnValue;
            MySQLConnection conn = null;
            conn = new MySQLConnection(new MySQLConnectionString(DB_Address, DB_Database, DB_User, DB_Pass).AsString);
 
            try
            {
               
               conn.Open();
                ReturnValue = true;
            }
            catch (Exception e)
            {
                //MessageBox.Show("数据库连接失败，请检查参数");
                ReturnValue = false;
            }
            conn.Close();
            ReturnValue = true;
            return ReturnValue;
        }

        //数据库读取
        public string MysqlRead(string Query)
        {
            string ReturnValue = null;
            MySQLConnection conn = null;
            MySQLCommand commn = new MySQLCommand("set names utf-8", conn);
            conn = new MySQLConnection(new MySQLConnectionString(DB_Address, DB_Database, DB_User, DB_Pass).AsString);
            string query = "select * from video";
            conn.Open();
            MySQLCommand cmd = new MySQLCommand(query, conn);

            MySQLDataReader reader = cmd.ExecuteReaderEx();
            while (reader.Read())
            {
                ReturnValue = reader.GetString(0);
            }
            conn.Close();
            return ReturnValue;

        }

        //数据库写入
        public void MysqlWrite(string Query)
        {
            MySQLConnection conn = null;
            //MySQLCommand commn = new MySQLCommand("set names utf-8", conn);
            conn = new MySQLConnection(new MySQLConnectionString(DB_Address, DB_Database, DB_User, DB_Pass).AsString);
            //string query = "select * from video";
            MySQLCommand cmd = new MySQLCommand(Query, conn);
            conn.Open();
            cmd.ExecuteNonQuery();//执行数据库代码
            conn.Close();
        }

        private void 数据库测试ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MysqlTest() == true)
                MessageBox.Show("数据库连接成功，请登录");
            else
                MessageBox.Show("数据库连接失败，请修改参数");
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login = "0";
            MessageBox.Show("已推出");
        }

        private void button_comfirm_Click(object sender, EventArgs e)
        {
            string LoginId = textBox_id.Text;
            string LoginQuery = "SELECT password FROM user WHERE username=" + "'" + LoginId +"'";
            if (MysqlRead(LoginQuery) == textBox_pass.Text)
                MessageBox.Show("登录成功");
            else
                MessageBox.Show("登录失败，请检查帐户或用户名");
            MessageBox.Show(LoginQuery);
        }
    }

}
