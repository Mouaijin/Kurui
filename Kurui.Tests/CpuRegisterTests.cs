using System;
using Xunit;
using Cpu = Kurui.Core.Cpu;
using Kurui.Core;

namespace Kurui.Tests
{
    public class CpuRegisterTests
    {
        [Fact]
        public void RegisterFieldsAlignedCorrectly()
        {
            Imm reg = new Imm
            {
                hi = 0xff,
                lo = 0x00
            };
            Assert.Equal(0xff00, reg.wide);

            reg.wide = 0x1ff1;
            Assert.Equal(0x1f, reg.hi);
            Assert.Equal(0xf1, reg.lo);
        }

        [Fact]
        public void FlagsSetProperly()
        {
            Cpu cpu = new Cpu();

            Assert.True(cpu.GetZ());
            Assert.False(cpu.GetN());
            Assert.True(cpu.GetH());
            Assert.True(cpu.GetC());

            cpu.SetZ(false);
            Assert.False(cpu.GetZ());

            cpu.SetN(true);
            Assert.True(cpu.GetN());

            cpu.SetH(false);
            Assert.False(cpu.GetH());

            cpu.SetC(false);
            Assert.False(cpu.GetC());

        }
    }
}
