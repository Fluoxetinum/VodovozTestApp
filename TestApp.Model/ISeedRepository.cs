using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Model
{
    public interface ISeedRepository
    {
        public void SeedData();
        public void ClearData();
    }
}
