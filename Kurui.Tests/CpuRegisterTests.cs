using System;
using Xunit;
using Register = Kurui.Core.Cpu.Register;

namespace Kurui.Tests
{
    public class CpuRegisterTests
    {
        [Fact]
        public void RegisterFieldsAlignedCorrectly()
        {
            Register reg = new Register();
            reg.hi = 0xff;
            reg.lo = 0x00;
            Assert.Equal(0xff00, reg.wide);

            reg.wide = 0x1ff1;
            Assert.Equal(0x1f, reg.hi);
            Assert.Equal(0xf1, reg.lo);
        }
    }
}
