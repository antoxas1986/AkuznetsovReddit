using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace AkuznetsovReddit.UnitTest.Helpers
{
    public class DbSetHelper
    {
        public Mock<DbSet<T>> GetDbSet<T>(List<T> entities) where T : class
        {
            IQueryable<T> data = entities.AsQueryable();
            Mock<DbSet<T>> mockSet = new Mock<DbSet<T>>();

            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet;
        }
    }
}
