using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbpDemo.Tests
{
    public class TestService : ApplicationService
    {
        public int TestAAA(int n)
        {
            return n * n;
        }
    }
}
