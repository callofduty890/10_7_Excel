using CSV_ClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace _03_XML_CSV
{
    public partial class Form1 : Form
    {
        //XML的使用
        public class XmlConfigUtil
        {
            #region 全局变量
            string _xmlPath;        //文件所在路径
            #endregion

            #region 构造函数
            /// <summary>
            /// 初始化一个配置
            /// </summary>
            /// <param name="xmlPath">配置所在路径</param>
            public XmlConfigUtil(string xmlPath)
            {
                _xmlPath = Path.GetFullPath(xmlPath);
            }
            #endregion

            #region 公有方法
            /// <summary>
            /// 写入配置
            /// </summary>
            /// <param name="value">写入的值</param>
            /// <param name="nodes">节点</param>
            public void Write(string value, params string[] nodes)
            {
                //初始化xml
                XmlDocument xmlDoc = new XmlDocument();
                if (File.Exists(_xmlPath))
                    xmlDoc.Load(_xmlPath);
                else
                    xmlDoc.LoadXml("<XmlConfig />");
                XmlNode xmlRoot = xmlDoc.ChildNodes[0];

                //新增、编辑 节点
                string xpath = string.Join("/", nodes);
                XmlNode node = xmlDoc.SelectSingleNode(xpath);
                if (node == null)    //新增节点
                {
                    node = makeXPath(xmlDoc, xmlRoot, xpath);
                }
                node.InnerText = value;

                //保存
                xmlDoc.Save(_xmlPath);
            }

            /// <summary>
            /// 读取配置
            /// </summary>
            /// <param name="nodes">节点</param>
            /// <returns></returns>
            public string Read(params string[] nodes)
            {
                XmlDocument xmlDoc = new XmlDocument();
                if (File.Exists(_xmlPath) == false)
                    return null;
                else
                    xmlDoc.Load(_xmlPath);

                string xpath = string.Join("/", nodes);
                XmlNode node = xmlDoc.SelectSingleNode("/XmlConfig/" + xpath);
                if (node == null)
                    return null;

                return node.InnerText;
            }
            #endregion

            #region 私有方法
            //递归根据 xpath 的方式进行创建节点
            static private XmlNode makeXPath(XmlDocument doc, XmlNode parent, string xpath)
            {

                // 在XPath抓住下一个节点的名称；父级如果是空的则返回
                string[] partsOfXPath = xpath.Trim('/').Split('/');
                string nextNodeInXPath = partsOfXPath.First();
                if (string.IsNullOrEmpty(nextNodeInXPath))
                    return parent;

                // 获取或从名称创建节点
                XmlNode node = parent.SelectSingleNode(nextNodeInXPath);
                if (node == null)
                    node = parent.AppendChild(doc.CreateElement(nextNodeInXPath));

                // 加入的阵列作为一个XPath表达式和递归余数
                string rest = String.Join("/", partsOfXPath.Skip(1).ToArray());
                return makeXPath(doc, node, rest);
            }
            #endregion
        }
        //序列化与反序列化：序列化->将内存中的对象(数据)保存成硬盘上的文件
        //二进制文件         ：反序列化->将硬盘中的文件加载到内存当中
        [Serializable]
        public class Student
        {
            private string studentName;
            private string studentClass;
            private string gender;
            private string year;
            public string StudentName { get => studentName; set => studentName = value; }
           
            public string Gender { get => gender; set => gender = value; }
            public string Year { get => year; set => year = value; }
            public string StudentClass { get => studentClass; set => studentClass = value; }
        }

        public Form1()
        {
            InitializeComponent();
        }
        //CSV 写入操作
        private void button2_Click(object sender, EventArgs e)
        {
            //规定写入的路径
            string path = "学生信息.csv";

            //第一行的内容
            string[] rowCells_1 = new string[4];
            //每一个格子的内容写入
            rowCells_1[0] = "学号";
            rowCells_1[1] = "姓名";
            rowCells_1[2] = "班级";
            rowCells_1[3] = "籍贯";

            //第二行的内容
            string[] rowCells_2 = new string[4];
            //每一个格子的内容写入
            rowCells_2[0] = "001";
            rowCells_2[1] = "助教";
            rowCells_2[2] = "语言班";
            rowCells_2[3] = "东莞";

            //存入
            List<string[]> rowList_Write = new List<string[]>();
            //添加每一行的内容
            rowList_Write.Add(rowCells_1);
            rowList_Write.Add(rowCells_2);

            //写入CSV当中 最后一个是不追加，覆盖式写入
            CSV_ClassLibrary.CSV.WriteCSV(path, rowList_Write, false);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string[]> row_List = CSV.ReadCSV("学生信息.csv");
            //显示
            for (int i = 0; i < row_List.Count; i++)
            {
                for (int j = 0; j < row_List[i].Length; j++)
                {
                    Console.Write(row_List[i][j] + " ");
                }
                Console.WriteLine();//换行
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //初始化XML
            XmlConfigUtil util = new XmlConfigUtil("1.xml");
            //保存要写入的值以及在XML中的路径
            util.Write(this.textBox1.Text, "information", "name");
            util.Write(this.textBox2.Text, "information", "class");
            util.Write(this.textBox3.Text, "information", "sex");
            util.Write(this.textBox4.Text, "information", "year");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //初始化XML
            XmlConfigUtil util = new XmlConfigUtil("1.xml");
            //保存要写入的值以及在XML中的路径
            this.textBox1.Text = util.Read("information", "name");
            this.textBox2.Text = util.Read("information", "class");
            this.textBox3.Text = util.Read("information", "sex");
            this.textBox4.Text = util.Read("information", "year");
        }
        //序列化
        private void button3_Click(object sender, EventArgs e)
        {
            //实例化对象类
            Student student = new Student();
            student.StudentName = this.textBox5.Text;
            student.StudentClass = this.textBox6.Text;
            student.Gender = this.textBox7.Text;
            student.Year = this.textBox8.Text;

            //【1】创建文件流
            FileStream fs = new FileStream("objStudent.stu", FileMode.Create);
            //【2】创建二进制格式转换器
            BinaryFormatter formatter = new BinaryFormatter();
            //【3】调用序列化方法
            formatter.Serialize(fs, student);
            //【4】关闭文件流 
            fs.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //【1】创建文件流
            FileStream fs = new FileStream("objStudent.stu", FileMode.Open);
            //【2】创建二进制格式转换器
            BinaryFormatter formatter = new BinaryFormatter();
            //【3】调用反序列化
            Student objstudent = (Student)formatter.Deserialize(fs);
            //【4】关闭文件流
            fs.Close();
            //赋值到控件上
            this.textBox5.Text = objstudent.StudentName;
            this.textBox6.Text = objstudent.StudentClass;
            this.textBox7.Text = objstudent.Gender;
            this.textBox8.Text = objstudent.Year;

        }
    }
}
