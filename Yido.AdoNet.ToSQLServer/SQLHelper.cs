using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Yido.AdoNet.ToSQLServer
{
    /// <summary>
    /// SQLHelper帮助类库
    /// </summary>
    public class SQLHelper
    {
        //读书数据库连接字符串
        static readonly string connStr = ConfigurationManager.ConnectionStrings["YidoConn"].ConnectionString;

        /// <summary>
        /// 执行添加、删除、更新操作
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string cmdText, CommandType commandType, params SqlParameter[] sqlParameters)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();//打开数据库连接
                SqlCommand sqlCmd = conn.CreateCommand();//创建SqlCommand对象
                sqlCmd.CommandText = cmdText; //设置SQL语句
                sqlCmd.CommandType = commandType; //设置命令类型
                sqlCmd.Parameters.AddRange(sqlParameters);
                return sqlCmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 异步执行添加、删除、更新操作
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdType"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        public static async Task<int> ExecuteNonQueryAsync(string cmdText, CommandType commandType, params SqlParameter[] sqlParameters)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                await conn.OpenAsync(); //异步打开数据库连接
                SqlCommand sqlCmd = conn.CreateCommand();
                sqlCmd.CommandText = cmdText;//设置SQL语句
                sqlCmd.CommandType = commandType; //设置命令类型
                sqlCmd.Parameters.AddRange(sqlParameters); //添加参数
                return await sqlCmd.ExecuteNonQueryAsync();

            }
        }

        /// <summary>
        /// 获取SqlDataReader数据流
        /// 使用完毕后，必须关闭SqlDataReader对象
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        public static SqlDataReader GetSqlDataReader(string cmdText,CommandType commandType, params SqlParameter[] sqlParameters)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();//打开数据库连接
            SqlCommand sqlCmd = conn.CreateCommand(); //创建SqlCommand对象
            sqlCmd.CommandText= cmdText; //设置SQL语句
            sqlCmd.CommandType = commandType; //设置命令类型
            sqlCmd.Parameters.AddRange(sqlParameters);//添加参数
            //关闭SqlDataReader对象后其依赖的数据库连接也关闭
            return sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// 异步获取SqlDataReader数据流
        /// 使用完毕后，必须关闭SqlDataReader对象
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        public static async Task<SqlDataReader> GetSqlDataReaderAsync(string cmdText,CommandType commandType,params SqlParameter[] sqlParameters)
        {
            SqlConnection conn = new SqlConnection(connStr);
            await conn.OpenAsync();//打开数据库连接
            SqlCommand sqlCmd = conn.CreateCommand();
            sqlCmd.CommandText= cmdText; //设置SQL语句
            sqlCmd.CommandType = commandType; //设置命令类型
            sqlCmd.Parameters.AddRange(sqlParameters); //添加参数
            //关闭SqlDataReader对象后其依赖的数据库连接也关闭
            return await sqlCmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// 获取首列首行的值
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string cmdText, CommandType commandType, params SqlParameter[] sqlParameters)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open(); //打开数据库连接
                SqlCommand sqlCmd = conn.CreateCommand();
                sqlCmd.CommandText= cmdText; //设置SQL语句
                sqlCmd.CommandType = commandType; //设置命令类型
                sqlCmd.Parameters.AddRange(sqlParameters);//添加参数
                return sqlCmd.ExecuteScalar();

            }
        }
        /// <summary>
        /// 异步获取首行首列的值
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<object?> ExecuteScalarAsync(string cmdText, CommandType commandType, params SqlParameter[] parameters)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                await conn.OpenAsync(); //打开数据库连接
                SqlCommand sqlCmd = conn.CreateCommand();
                sqlCmd.CommandText= cmdText; //设置SQL语句
                sqlCmd.CommandType = commandType; //设置命令类型
                sqlCmd.Parameters.AddRange(parameters); //添加参数

                return await sqlCmd.ExecuteScalarAsync();
            }
        }

        /// <summary>
        /// 获取DataTable数据表
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string cmdText, CommandType commandType, params SqlParameter[] sqlParameters)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand sqlCmd = conn.CreateCommand();
                sqlCmd.CommandText= cmdText; //设置SQL语句
                sqlCmd.CommandType = commandType; //设置命令类型
                sqlCmd.Parameters.AddRange(sqlParameters); //添加参数
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt;
            }
        }
    }
}
