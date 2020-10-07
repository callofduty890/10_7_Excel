using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excle
{
    public class Excel
    {
        //构建链接语句-
        public static string  sConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=Excel表格.xls;" + "Extended Properties='Excel 8.0;HDR=Yes;IMEX=0'";

        //Provider : Access数据库的版本信息(链接数据库)
        //Data Source : 规定Excle表的名称以及在什么位置
        //Extended Properties : 规定Excel使用的版本。office2003使用7.0
        //HDR : 是否创建表头
        //IMEX : 文件可写入可读取

        public static DataTable GetDataTable(string sql)
        {
            //1.实例化链接数据库
            using (OleDbConnection ole_cnn = new OleDbConnection(sConnectionString))
            {
                //2.打开数据库->Access
                ole_cnn.Open();
                //3.创建操作对象
                using (OleDbCommand ole_cmd= ole_cnn.CreateCommand())
                {
                    //4.执行SQL语句
                    ole_cmd.CommandText = sql;
                    //5.返回执行SQL语句的结果
                    using (OleDbDataAdapter dapter= new OleDbDataAdapter(ole_cmd))
                    {
                        //创建dateset来接收结构
                        DataSet dr = new DataSet();
                        //填充数据
                        dapter.Fill(dr);
                        //返回表格当中的数据
                        return dr.Tables[0];
                    }

                }
                
            }
        }

    }
}
