using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Configuration;

namespace AutoLevelAdo2._1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Создание строки подключения
            var connectionStringObject = ConfigurationManager.ConnectionStrings["marketConnectionString"];

            DbProviderFactory factory = DbProviderFactories.GetFactory(connectionStringObject.ProviderName);
            DataSet marketDataSet = new DataSet("Market");
            DbDataAdapter adapter = factory.CreateDataAdapter();
            
            //Создание подключения
            DbConnection connection = factory.CreateConnection();
            connection.ConnectionString = connectionStringObject.ConnectionString;

            //Создание выполняемой команды
            DbCommand selectCommand = connection.CreateCommand();
            selectCommand.CommandText = "SELECT * FROM Reviews";

                //Создание подобия первой команды
            DbCommandBuilder builder = factory.CreateCommandBuilder();
            builder.DataAdapter = adapter;

            //Выполнение команды
            adapter.SelectCommand = selectCommand;
            adapter.Fill(marketDataSet, "Reviews");


            //Добавление нововй записи
            DataRow firstRow = marketDataSet.Tables["Reviews"].NewRow();
            firstRow["UserId"] = 4;
            firstRow["ProductId"] = 2;
            firstRow["Text"] = "Ваш софт топ";
            firstRow["Mark"] = 2;
            firstRow["CreationDate"] = DateTime.Now;
            firstRow["DeletedDate"] = DBNull.Value;
            firstRow["IsDeleted"] = false;
            
            //Загрузка данных
            marketDataSet.Tables["Reviews"].Rows.Add(firstRow);

            adapter.Update(marketDataSet, "Reviews");
            marketDataSet.AcceptChanges();
        }
    }
}
