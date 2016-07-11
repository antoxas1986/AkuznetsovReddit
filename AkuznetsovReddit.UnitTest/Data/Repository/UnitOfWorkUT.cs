using AkuznetsovReddit.Data.Context.Interfaces;
using AkuznetsovReddit.Data.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AkuznetsovReddit.UnitTest.Data.Repository
{
    public class UnitOfWorkUT
    {
        private Mock<IRedditContext> _db;
        private UnitOfWork _rut;

        public UnitOfWorkUT()
        {
            _db = new Mock<IRedditContext>();
            _rut = new UnitOfWork(_db.Object);
        }

        [Fact]
        public void Dispose_Valid()
        {
            _rut.Dispose();
            _db.Verify(d => d.Dispose(), Times.Once);
        }

        [Fact]
        public void SaveChanges()
        {
            _rut.SaveChanges();
            _db.Verify(d => d.SaveChanges(), Times.Once);
        }
    }
}
