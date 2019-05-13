using Abp.Application.Services;
using Abp.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbpDemo.Tests
{
    public class TestService : ApplicationService
    {
        public int TestAAA(int n)
        {
            //throw new Exception("");
            return n * n;
        }
        [DisableAuditing]
        public int TestBBB(int n)
        {
            return n * n;
        }
    }
}
