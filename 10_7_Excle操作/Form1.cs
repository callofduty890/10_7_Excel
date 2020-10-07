using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excle;

namespace _10_7_Excle操作
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //创建Excel表格
        private void button5_Click(object sender, EventArgs e)
        {
            //sql语句
            string sql = "CREATE TABLE 学生信息([学号] INT,[姓名] VarChar,[班级] Varchar,[电话号码] VarChar,[状态] VarChar)";
            //执行SQL语句
            if (Excel.Upadate(sql) == 0)
            {
                MessageBox.Show("创建Excel文件成功", "提示");
            }
            else
            {
                MessageBox.Show("修改失败", "修改提示");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

            //查询语句
            string sql = "select 学号,姓名,班级,电话号码 from [学生信息$] where 状态='正常' ";
            //执行查询
            this.dataGridView1.DataSource = Excel.GetDataTable(sql);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //sql语句
            string sql = "CREATE TABLE 学生信息([学号] INT,[姓名] VarChar,[班级] Varchar,[电话号码] VarChar,[状态] VarChar)";
            //执行SQL语句
            Excel.Upadate(sql);
        }
        //增
        private void button1_Click_1(object sender, EventArgs e)
        {
            //构建sql语句
            string sql = "insert into [学生信息$] (学号,姓名,班级,电话号码,状态)values({0},'{1}','{2}','{3}','{4}')";
            sql = string.Format(sql, this.textBox1.Text, this.textBox2.Text, this.textBox3.Text, this.textBox4.Text, "正常");
            //执行SQL语句
            if (Excel.Upadate(sql) > 0)
            {
                //执行查询->查询全部内容
                this.dataGridView1.DataSource = Excel.GetDataTable("select 学号,姓名,班级,电话号码 from [学生信息$] where 状态='正常' ");
            }
            else
            {
                MessageBox.Show("插入数据失败", "提示信息");
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //获取当前活动的单元格
            string index = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            //删除
            string sql = "UPDATE [学生信息$] set 状态='删除' where 学号="+index;
            //执行SQL语句
            Excel.Upadate(sql);
            //查询执行结果
            this.dataGridView1.DataSource = Excel.GetDataTable("select 学号,姓名,班级,电话号码 from [学生信息$] where 状态='正常' ");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //修改
            string sql = "UPDATE [学生信息$] set  姓名='{0}' ,班级='{1}' ,电话号码='{2}',状态='正常' where 学号={3}";
            sql = string.Format(sql, this.textBox2.Text, this.textBox3.Text, this.textBox4.Text, this.textBox1.Text);
            //执行SQL语句
            Excel.Upadate(sql);
            //查询执行结果
            this.dataGridView1.DataSource = Excel.GetDataTable("select 学号,姓名,班级,电话号码 from [学生信息$] where 状态='正常' ");
        }
    }
}
