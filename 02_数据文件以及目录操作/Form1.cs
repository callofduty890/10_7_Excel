using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using INI_ClassLibrary;

namespace _02_数据文件以及目录操作
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //写入日志
        private void button1_Click(object sender, EventArgs e)
        {
            //创建文件流
            FileStream fs = new FileStream("D:\\myfile.txt", FileMode.Append);
            //创建写入器
            StreamWriter sw = new StreamWriter(fs);
            //以文件流的形式写入数据
            sw.WriteLine(DateTime.Now.ToString() + " [Form1] [button1_Click] 写入日志");
            //关闭写入器
            sw.Close();
            //关闭文件流
            fs.Close();
        }
        //读取日志
        private void button2_Click(object sender, EventArgs e)
        {
            //创建文件流
            FileStream fs = new FileStream("D:\\myfile.txt", FileMode.Open);
            //创建读取器
            StreamReader sr = new StreamReader(fs);
            //以流的方式读取数据
            this.textBox1.Text = sr.ReadToEnd();
            //关闭写入器
            sr.Close();
            //关闭文件流
            fs.Close();
        }
        //删除
        private void button3_Click(object sender, EventArgs e)
        {
            File.Delete(this.textBox2.Text.Trim());
        }
        //复制
        private void button4_Click(object sender, EventArgs e)
        {
            //判断前往的文件夹是否存在同名文件，如果存在删除对应文件
            if (File.Exists(this.textBox3.Text.Trim()))
            {
                File.Delete(this.textBox3.Text.Trim());
            }
            //复制新的文件到目标文件夹当中
            File.Copy(this.textBox2.Text.Trim(), this.textBox3.Text.Trim());
        }
        //移动文件
        private void button5_Click(object sender, EventArgs e)
        {
            File.Move(this.textBox2.Text.Trim(), this.textBox3.Text.Trim());
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //保存绝对路径
            string Save_File = System.AppDomain.CurrentDomain.BaseDirectory + "日志路径.ini";
            //保存信息
            INI.WritePrivateProfileString("日志配置", "源文件路径", this.textBox2.Text, Save_File);
            INI.WritePrivateProfileString("日志配置", "目的文件路径", this.textBox3.Text, Save_File);
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        //窗体加载的时候读取INI配置文件
        private void Form1_Load(object sender, EventArgs e)
        {
            //INI 的路径
            string Save_File = System.AppDomain.CurrentDomain.BaseDirectory + "日志路径.ini";
            //显示读取的内容
            this.textBox2.Text = INI.ContentValue("日志配置", "源文件路径", Save_File);
            this.textBox3.Text = INI.ContentValue("日志配置", "目的文件路径", Save_File);
        }
        //获取当前目录下的所有文件
        private void button6_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles("D:\\");
            this.textBox1.Clear();//清空
            foreach (string item in files)
            {
                this.textBox1.Text += item + "\r\n";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetDirectories("D:\\");
            this.textBox1.Clear();//清空
            foreach (string item in files)
            {
                this.textBox1.Text += item + "\r\n";
            }
        }
        //在指定目录下创建子目录
        private void button8_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 11; i++)
            {
                Directory.CreateDirectory("D:\\myfile\\" + i.ToString());
            }
        }
        //删除指定目录
        private void button9_Click(object sender, EventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo("D:\\myfile");
            dir.Delete(true);
        }
    }
}
