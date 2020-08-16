using System;
using System.Collections.Generic;

namespace TestApp.Common.Interfaces
{
    public interface IModelDependency
    {
        public Guid Id { get; }
        public IEnumerable<IModelDependency> GetDependencies();
    }
}
