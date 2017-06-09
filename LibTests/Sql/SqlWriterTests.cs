using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib.Sql;
using NUnit.Framework;

namespace LibTests.Sql
{
    [TestFixture]
    public class SqlToolsTests
    {
        [Test]
        public void GetSql_should_return_sql_text_for_command()
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "spDoThing";
            command.Parameters.Add(new SqlParameter { ParameterName = "@Foo", SqlDbType = SqlDbType.NVarChar, Value = "Hello" });
            command.Parameters.Add(new SqlParameter { ParameterName = "@Bar", SqlDbType = SqlDbType.Int, Value = 123 });
            command.Parameters.Add(new SqlParameter { ParameterName = "@Baz", SqlDbType = SqlDbType.Bit, Value = true });
            command.Parameters.Add(new SqlParameter { ParameterName = "@Qux", SqlDbType = SqlDbType.DateTime, Value = "1/3/2016" });

            var result = SqlTools.GetSql(command);

            Assert.That(result, Is.EqualTo("exec spDoThing\n@Foo='Hello'\n@Bar=123\n@Baz=1\n@Qux='1/3/2016'"));
        }

        [Test]
        public void GetSql_should_return_sql_text_for_command_with_output_params()
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "spDoThing";
            command.Parameters.Add(new SqlParameter { ParameterName = "@Foo", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Output });

            var result = SqlTools.GetSql(command);

            Assert.That(result, Is.EqualTo("exec spDoThing\n@Foo=@Foo output"));
        }

        [Test]
        public void GetSql_should_return_sql_text_for_command_with_inner_quotes()
        {
            var command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "spDoThing";
            command.Parameters.Add(new SqlParameter { ParameterName = "@Foo", SqlDbType = SqlDbType.NVarChar, Value = "Let's do this" });

            var result = SqlTools.GetSql(command);

            Assert.That(result, Is.EqualTo("exec spDoThing\n@Foo='Let''s do this'"));
        }

        [Test]
        public void GetSql_should_return_sql_text_for_command_verbosely()
        {
            var command = new SqlCommand();
            command.Connection = new SqlConnection("Initial Catalog=testdb;Data Source=localhost");
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "spDoThing";
            command.Parameters.Add(new SqlParameter { ParameterName = "@Foo", SqlDbType = SqlDbType.NVarChar, Value="Hello" });

            var result = SqlTools.GetSql(command, true);

            Assert.That(result, Is.EqualTo("--server: localhost\nuse testdb;\nexec spDoThing\n@Foo='Hello'"));
        }
    }
}
