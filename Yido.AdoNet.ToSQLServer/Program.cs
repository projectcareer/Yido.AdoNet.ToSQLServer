using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Net.Mail;
// See https://aka.ms/new-console-template for more information


//读取数据库连接字符串
string connStr = ConfigurationManager.ConnectionStrings["YidoConn"].ConnectionString;

//SqlConnection对象
/*SqlConnection对象
//构造函数实例化SqlConnection
SqlConnection sqlConnection = new SqlConnection(connStr);

//属性指定数据库连接字符串
//SqlConnection sqlConnection = new SqlConnection();
//sqlConnection.ConnectionString = connStr;

//打开数据库连接
sqlConnection.Open();
//关闭连接
sqlConnection.Close();
//异步打开数据库连接
await sqlConnection.OpenAsync();
//异步关闭数据库连接
await sqlConnection.CloseAsync();
*/

//SqlCommand对象
/*
using(SqlConnection conn = new SqlConnection(connStr))
{
    //打开数据库连接
    await conn.OpenAsync();
    //T-SQL语句
    string sql = "select * from Student";
    //实例化SqlCommand对象_构造函数实例化SqlCommand
    //SqlCommand sqlCmd = new SqlCommand(sql, conn);
    //使用属性实例化SqlCommand对象
    //SqlCommand sqlCmd = new SqlCommand();

    //CreateCommand()方法
    SqlCommand sqlCmd = conn.CreateCommand();

    sqlCmd.CommandText = sql; //指定SQL语句或存储过程
    sqlCmd.CommandType = System.Data.CommandType.Text;//命令类型是SQL语句
    //CommandType.StoredProcedure：指定要执行的是存储过程
    //CommandType.TableDirect:指定一个表明
    sqlCmd.Connection = conn; //指定SqlConnection对象

}
*/
//SqlDataReader对象
/*
using(SqlConnection sqlConn = new SqlConnection(connStr))
{
    //打开数据库连接
    //sqlConn.Open();
    //异步打开数据库连接
    await sqlConn.OpenAsync();
    //T-SQL语句
    string sql = "select * from Student";
    //创建SqlCommand对象
    SqlCommand cmd = sqlConn.CreateCommand();
    cmd.CommandText = sql;
    cmd.CommandType = System.Data.CommandType.Text;
    cmd.Connection = sqlConn;

    //获取数据流
    //SqlDataReader sdr = cmd.ExecuteReader();
    //while (sdr.Read())
    //{
    //    Console.WriteLine("{0} {1} {2} {3}", sdr[0], sdr[1], sdr[2], sdr[3]);
    //}
    ////关闭数据流
    //sdr.Close();
    //异步方法
    SqlDataReader sdr = await cmd.ExecuteReaderAsync();
    //while (await sdr.ReadAsync())
    //{
    //    Console.WriteLine("{0} {1} {2} {3}", sdr[0], sdr[1], sdr[2], sdr[3]);
    //}

    //SqlDataReader方法
    //GetOrdinal()方法可以根据表中指定的列名（字段名）得到对应的索引值。索引值从0开始
    while (await sdr.ReadAsync())
    {
        Console.WriteLine(sdr.GetOrdinal("SId"));
    }
    //GetName()方法根据索引值得到对应的列名

    await sdr.CloseAsync();
}
*/


//SqlParameter对象
/*
 * Add()方法：将单个的SqlParameter对象添加到集合中
 * AddRange()：将SqlParameter类型的数组添加到集合中
 * AddWithValue()：将参数名称和对应的值添加到集合中

using(SqlConnection conn = new SqlConnection(connStr))
{
    await conn.OpenAsync();
    string sql = "Select * from Student where SName like '%'+@name+'%' AND Sage>@age";
    SqlCommand cmd = conn.CreateCommand();
    cmd.CommandText = sql;
    cmd.CommandType = System.Data.CommandType.Text;
    cmd.Connection = conn;

    //Add()方法添加参数
    ////配置参数
    //SqlParameter sqlParameter1 = new SqlParameter("@name", System.Data.SqlDbType.NVarChar);
    //SqlParameter sqlParameter2 = new SqlParameter("@age", System.Data.SqlDbType.Int);
    ////将参数添加到集合中
    //cmd.Parameters.Add(sqlParameter1);
    //cmd.Parameters.Add(sqlParameter2);
    ////给参数赋值
    //cmd.Parameters["@name"].Value = "大";
    //cmd.Parameters["@age"].Value = 21;

    //AddRange()方法添加参数
    //定义参数数组
    //SqlParameter[] sqlParameters =
    //{
    //    new SqlParameter("@name", System.Data.SqlDbType.NVarChar),
    //    new SqlParameter("@age", System.Data.SqlDbType.Int)
    //};
    ////将参数添加到集合中
    //cmd.Parameters.Add(sqlParameters);
    ////给参数赋值
    //cmd.Parameters["@name"].Value = "大";
    //cmd.Parameters["@age"].Value = 21;


    //AddWithValue()添加参数
    //直接添加参数并赋值
    cmd.Parameters.AddWithValue("@name", "大");
    cmd.Parameters.AddWithValue("@age", 21);



    //获取数据流
    SqlDataReader sdr = await cmd.ExecuteReaderAsync();
    while(await sdr.ReadAsync())
    {
        Console.WriteLine($"姓名= {await sdr.GetFieldValueAsync<string>(1)}, 年龄={await sdr.GetFieldValueAsync<int>(2)}");
    }
    await sdr.CloseAsync();

}
 */

//添加数据
/*
 * 

using(SqlConnection conn = new SqlConnection(connStr))
{
    await conn.OpenAsync();
    string sql = "INSERT into Student(Sname,SAge,SSex) VALUES(@name,@age,@sex)";
    SqlCommand cmd = conn.CreateCommand();
    cmd.CommandText = sql;
    cmd.CommandType = System.Data.CommandType.Text;
    cmd.Connection = conn;

    //添加参数
    SqlParameter[] sqlParameters =
    {
        new SqlParameter("@name", System.Data.SqlDbType.NVarChar),
        new SqlParameter("@age", System.Data.SqlDbType.Int),
        new SqlParameter("@sex", System.Data.SqlDbType.Bit)
    };
    //将参数添加到集合
    cmd.Parameters.AddRange(sqlParameters);
    //给参数赋值
    cmd.Parameters[0].Value = "刘墉";
    cmd.Parameters[1].Value = 85;
    cmd.Parameters[2].Value = 1;

    //执行添加
    int count = await cmd.ExecuteNonQueryAsync();
    if(count > 0)
    {
        Console.WriteLine("添加成功！");
    }
}
 */

//更新数据
/*
 * 

using(SqlConnection conn = new SqlConnection(connStr))
{
    await conn.OpenAsync();
    string sql = "Update Student set SAge = @age where SId = @id";

    SqlCommand cmd = conn.CreateCommand();
    cmd.CommandText = sql;
    cmd.CommandType = System.Data.CommandType.Text;
    cmd.Connection = conn;

    //添加参数
    SqlParameter[] sqlParameters =
    {
        new SqlParameter("@age", System.Data.SqlDbType.Int),
        new SqlParameter("@id", System.Data.SqlDbType.Int)
    };
    //将参数添加到集合
    cmd.Parameters.AddRange(sqlParameters);
    //给参数赋值
    cmd.Parameters[0].Value = 29;
    cmd.Parameters[1].Value = 4;

    //执行更新
    int count = await cmd.ExecuteNonQueryAsync();
    if(count > 0)
    {
        Console.WriteLine("更新成功!");
    }

}
 */
//删除数据
/*
 * 

using(SqlConnection conn = new SqlConnection(connStr))
{
    await conn.OpenAsync();
    string sql = "delete from Student where Sid= @id";

    SqlCommand cmd = conn.CreateCommand();
    cmd.CommandText = sql;
    cmd.CommandType = System.Data.CommandType.Text;
    cmd.Connection = conn;

    SqlParameter[] sqlParameters =
    {
        new SqlParameter("@id", System.Data.SqlDbType.Int)
    };
    cmd.Parameters.AddRange(sqlParameters);
    cmd.Parameters[0].Value = 4;

    int count = await cmd.ExecuteNonQueryAsync();
    if(count > 0)
    {
        Console.WriteLine("删除成功!");
    }
}
 */

//查询数据
/*
 * 

using(SqlConnection conn = new SqlConnection(connStr))
{
    await conn.OpenAsync();
    string sql = "select * from Student where SName like '%' + @name + '%' and SAge > @age";
    SqlCommand cmd = conn.CreateCommand();
    cmd.CommandText = sql;
    cmd.CommandType = System.Data.CommandType.Text;
    cmd.Connection = conn;

    SqlParameter[] sqlParameters =
    {
        new SqlParameter("@name", System.Data.SqlDbType.NVarChar),
        new SqlParameter("@age", System.Data.SqlDbType.Int)
    };
    cmd.Parameters.AddRange(sqlParameters);
    cmd.Parameters[0].Value = "大";
    cmd.Parameters[1].Value = 18;

    //查询数据
    SqlDataReader sdr = await cmd.ExecuteReaderAsync();
    while(await sdr.ReadAsync())
    {
        Console.WriteLine($"姓名:{sdr["SName"]}, 年龄:{sdr["SAge"]}");
    }
    await conn.CloseAsync();
}
 */

//SqlDataAdapter对象
/*
using(SqlConnection conn = new SqlConnection(connStr))
{
    await conn.OpenAsync();
    string sql = "select * from Student";
    ////实例化SqlDataAdapter对象，无参数
    //SqlDataAdapter sda = new SqlDataAdapter();
    //指定SelectCommand，包括T-SQL语句和SqlConnection对象
    //sda.SelectCommand = new SqlCommand(sql, conn);
    //实例化SqlCommand对象
    SqlCommand cmd = new SqlCommand(sql, conn);
    //实例化SqlDataAdapter，将SqlCommand对象传入
    SqlDataAdapter sda = new SqlDataAdapter(cmd);

    //实例化DataSet数据集对象
    DataSet sd = new DataSet();

    //也可以直接填充到DataTable对象中
    //DataTable dataTable = new DataTable();
    //填充到DataSet对象中
    sda.Fill(sd);
    //填充到DataTable对象中
    //sda.Fill(dataTable);
    //如果填充DataSet,获取DataTable对象取DataSet中第一个DataTable
    DataTable dt = sd.Tables[0];

    //获取数据
    foreach(DataRow row in dt.Rows)
    {
        Console.WriteLine($"{row["SId"]},{row["SName"]},{row["SAge"]},{row["SSex"]}");
    }
}
*/

//离线添加数据
/*
string sql = "select * from Student";
//实例化SqlDataAdapter对象，将T-SQL语句和数据库连接字符串传入
SqlDataAdapter sda = new SqlDataAdapter(sql, connStr);
//InsertCommand表示插入数据
sda.InsertCommand = new SqlCommandBuilder(sda).GetInsertCommand();
DataTable dt = new DataTable();
sda.Fill(dt);
//添加一行数据
DataRow dr = dt.NewRow();
//给列赋值
dr["SName"] = "王蓉";
dr["SAge"] = 23;
dr["SSex"] = 0;
//将新行添加到表中
dt.Rows.Add(dr);
//更新到数据库中
int count = sda.Update(dt);
if(count > 0)
{
    Console.WriteLine("插入数据成功.");
}
*/

//离线更新数据
/*

string sql = "select * from Student";
SqlDataAdapter sda = new SqlDataAdapter(sql, connStr);
sda.UpdateCommand = new SqlCommandBuilder(sda).GetUpdateCommand();
DataTable dataTable = new DataTable();
sda.Fill(dataTable);

foreach (DataRow row in dataTable.Rows)
{
    if (Convert.ToInt32(row["Sid"]) == 5)
    {
        row.BeginEdit();//开始编辑
        row["SName"] = "李小璐";
        row["SAge"] = 58;
        row["SSex"] = 0;
        row.EndEdit(); //结束编辑
    }
}
//更新到数据库中
int count = sda.Update(dataTable);
if(count > 0)
{
    Console.WriteLine("更新成功！");
}
 */

//离线删除数据
/*

string sql = "select * from Student";
SqlDataAdapter sda = new SqlDataAdapter(sql, connStr);
sda.DeleteCommand = new SqlCommandBuilder(sda).GetDeleteCommand();

DataTable dataTable = new DataTable();
sda.Fill(dataTable);
foreach (DataRow row in dataTable.Rows)
{
    if (Convert.ToInt32(row["SId"]) == 5)
    {
        //删除行
        row.Delete();
        break;
    }
}
//更新到数据库中
int count = sda.Update(dataTable);
if (count > 0)
{
    Console.WriteLine("删除成功！");
}
 */
//离线混合操作
/*
string sql = "select * from Student";
SqlDataAdapter sda = new SqlDataAdapter(sql, connStr);
sda.InsertCommand = new SqlCommandBuilder(sda).GetInsertCommand();
sda.UpdateCommand = new SqlCommandBuilder(sda).GetUpdateCommand();
sda.DeleteCommand = new SqlCommandBuilder(sda).GetDeleteCommand();
DataTable dt = new DataTable();
sda.Fill(dt);

//插入操作
DataRow dr = dt.NewRow();
dr[1] = "王宝强";
dr[2] = 38;
dr[3] = 1;
dt.Rows.Add(dr);

//更新操作
dr = dt.Rows[0];
dr["SName"] = "刘天王";

//删除操作
dr = dt.Rows[3];
dr.Delete();

//更新到数据库中
int count = sda.Update(dt);
if(count > 0)
{
    Console.WriteLine("操作成功！");
}
*/

//SqlTransaction对象-事务
/*
using(SqlConnection conn = new SqlConnection(connStr))
{
    await conn.OpenAsync();
    SqlCommand sqlCmd= conn.CreateCommand();
    SqlTransaction sqlTrain;
    //开始事务
    sqlTrain = conn.BeginTransaction();
    sqlCmd.Connection = conn; //数据库连接
    sqlCmd.Transaction = sqlTrain;//指定事务

    try
    {
        sqlCmd.CommandText = "Insert into Student(SName,SAge,SSex) values('光宇',11,1)";
        sqlCmd.ExecuteNonQuery();//执行T-sql语句
        sqlCmd.CommandText = "Insert into Student(SName,SAge,SSex) values('赵云','gaga',1)";
        sqlCmd.ExecuteNonQuery();
        sqlTrain.Commit(); //提交事务
        Console.WriteLine("两条数据100%的添加到数据库中了");
    }
    catch (Exception ex)
    {

        Console.WriteLine($"发生了错误: {ex.Message}");
        //回滚事务
        sqlTrain.Rollback();
    }
}
*/

//存储过程
//存储过程添加数据
using(SqlConnection conn = new SqlConnection(connStr))
{
    await conn.OpenAsync();
    SqlCommand cmd = conn.CreateCommand();
    //cmd.CommandText = "AddDataToStudent";
    cmd.CommandText = "UpdateDataToStudent";
    cmd.CommandType = CommandType.StoredProcedure;

    //将参数添加到集合
    SqlParameter[] sqlParameters =
    {
        new SqlParameter("@name", SqlDbType.VarChar,20),
        new SqlParameter("@age", SqlDbType.Int, 4),
        new SqlParameter("@sex", SqlDbType.Bit,1),
        new SqlParameter("@id", SqlDbType.Int, 4)
    };
    cmd.Parameters.AddRange(sqlParameters);
    cmd.Parameters[0].Value = "董胖子";
    cmd.Parameters[1].Value = 34;
    cmd.Parameters[2].Value = 0;
    cmd.Parameters[3].Value = 1;
    //执行存储过程
    int count = await cmd.ExecuteNonQueryAsync();
    if (count > 0)
    {
        Console.WriteLine("添加成功！");
    }
}

Console.ReadKey();
