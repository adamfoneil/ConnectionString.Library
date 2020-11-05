using AO.ConnectionStrings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
{
    [TestClass]
    public class ConnectionStringTests
    {
        const string connectionWithPwd = "Server=whatever;Database=thisdb;User Id=adamo;Password=*gkdZ7W1#QsN@EAH";
        const string connectionWithoutPwd = "Server=(localdb)\\mssqllocaldb;Database=AnotherDb;Integrated Security=true";        

        [TestMethod]
        public void Redacted()
        {
            Assert.IsTrue(ConnectionString.Redact(connectionWithoutPwd).Equals("Server=(localdb)\\mssqllocaldb;Database=AnotherDb;Integrated Security=true"));
            Assert.IsTrue(ConnectionString.Redact(connectionWithPwd).Equals("Server=whatever;Database=thisdb;User Id=&lt;redacted&gt;;Password=&lt;redacted&gt;"));
        }

        [TestMethod]
        public void ServerName()
        {            
            Assert.IsTrue(ConnectionString.Server(connectionWithPwd).Equals("whatever"));
            Assert.IsTrue(ConnectionString.Server(connectionWithoutPwd).Equals("(localdb)\\mssqllocaldb"));
        }

        [TestMethod]
        public void DatabaseName()
        {
            Assert.IsTrue(ConnectionString.Database(connectionWithoutPwd).Equals("AnotherDb"));
            Assert.IsTrue(ConnectionString.Database(connectionWithPwd).Equals("thisdb"));
        }

        [TestMethod]
        public void IsSensitive()
        {            
            Assert.IsTrue(ConnectionString.IsSensitive(connectionWithPwd, out _));
        }

        [TestMethod]
        public void NotSensitive()
        {
            Assert.IsTrue(!ConnectionString.IsSensitive(connectionWithoutPwd, out _));
        }
    }
}
