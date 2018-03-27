using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kurui.Core;
using NUnit.Framework;

namespace Kurui.Tests
{
    [TestFixture]
    public class ImplTests
    {
        [SetUp]
        public void Setup()
        {
            Gameboy.Reset();
            Assert.AreEqual(1, Gameboy.cpu.A, "Pretest sanity check: A is incorrect");
            Assert.AreEqual(176, Gameboy.cpu.F, "Pretest sanity check: F is incorrect");
            Assert.AreEqual(0, Gameboy.cpu.B, "Pretest sanity check: B is incorrect");
            Assert.AreEqual(19, Gameboy.cpu.C, "Pretest sanity check: C is incorrect");
            Assert.AreEqual(0, Gameboy.cpu.D, "Pretest sanity check: D is incorrect");
            Assert.AreEqual(216, Gameboy.cpu.E, "Pretest sanity check: E is incorrect");
            Assert.AreEqual(1, Gameboy.cpu.H, "Pretest sanity check: H is incorrect");
            Assert.AreEqual(77, Gameboy.cpu.L, "Pretest sanity check: L is incorrect");

        }

        private void CheckFlags(bool Z, bool N, bool H, bool C)
        {
            Assert.AreEqual(Z,  Gameboy.cpu.GetZ(), "Z flag incorrect");
            Assert.AreEqual(N,  Gameboy.cpu.GetN(), "N flag incorrect");
            Assert.AreEqual(H,  Gameboy.cpu.GetH(), "H flag incorrect");
            Assert.AreEqual(C,  Gameboy.cpu.GetC(), "C flag incorrect");
        }

        private void AssertA(byte val)
        {
            Assert.AreEqual(val, Gameboy.cpu.A);
        }

        [Test]
        public void Add()
        {
            Gameboy.cpu.Add(Gameboy.cpu.F);
            Assert.AreEqual(177, Gameboy.cpu.A);
            CheckFlags(false, false, false, false);
            Gameboy.cpu.Add(14);
            CheckFlags(false, false, false, false);
            Gameboy.cpu.Add(1);
            Assert.AreEqual(192, Gameboy.cpu.A);
            CheckFlags(false, false, true, false);
            Gameboy.cpu.Add(255);
            Assert.AreEqual(191, Gameboy.cpu.A);
            CheckFlags(false, false, false, true);
            Gameboy.cpu.Add(10, true);
            Assert.AreEqual(202, Gameboy.cpu.A);
            CheckFlags(false, false, true, false);
            //16bit tests
            Assert.AreEqual(0x014D, Gameboy.cpu.HL.wide, "HL incorrect to begin 16-bit add tests");
            Assert.AreEqual(0x0013, Gameboy.cpu.BC.wide, "BC incorrect to being 16-bit add tests");
            Gameboy.cpu.Add(Gameboy.cpu.BC.wide);
            Assert.AreEqual(352, Gameboy.cpu.HL.wide);
            CheckFlags(false, false, false, false);
            Gameboy.cpu.Add(65535);
            Assert.AreEqual(351, Gameboy.cpu.HL.wide);
            CheckFlags(false, false, true, true);

        }


        [Test]
        public void AddSP()
        {

        }

        [Test]
        public void And()
        {
            Gameboy.cpu.And(1);
            AssertA(1);
            CheckFlags(false, false, true, false);
            Gameboy.cpu.And(0b11);
            AssertA(1);
            CheckFlags(false, false, true, false);
            Gameboy.cpu.And(0);
            CheckFlags(true, false, true, false);
            
        }

        [Test]
        public void Bit()
        {
            Assert.AreEqual(true, Gameboy.cpu.GetC());
            Gameboy.cpu.Bit(0b01, 0);
            CheckFlags(false, false, true, true);
            Gameboy.cpu.Bit(0b01, 1);
            CheckFlags(true, false, true, true);
        }

        [Test]
        public void CallRet() { }

        [Test]
        public void CcfScf() { }

        [Test]
        public void Cp() { }
        [Test]
        public void Cpl() { }
        [Test]
        public void Daa() { }
        [Test]
        public void Dec() { }

        [Test]
        public void DiEi()
        {
            Assert.False(Gameboy.interrupts.InterruptsEnabled);
            Gameboy.cpu.Ei();
            Assert.True(Gameboy.interrupts.InterruptsEnabled);
            Gameboy.cpu.Di();
            Assert.False(Gameboy.interrupts.InterruptsEnabled);
        }
        [Test]
        public void Inc() { }
        [Test]
        public void JpJr() { }
        [Test]
        public void Ld() { }
        [Test]
        public void Or() { }
        [Test]
        public void PopPush() { }
        [Test]
        public void ResSet() { }
        [Test]
        public void Rotates() { }

        [Test]
        public void Sub()
        {
            Gameboy.cpu.Sub(1);
            AssertA(0);
            CheckFlags(true, true, false, false);
            Gameboy.cpu.Sub(1);
            AssertA(255);
            CheckFlags(false, true, true, true);
            Gameboy.cpu.Sub(254, true);
            AssertA(0);
            CheckFlags(true, true, false, false);
            
        }
        [Test]
        public void Shifts() { }
        [Test]
        public void Swap() { }

        [Test]
        public void Xor()
        {
            Gameboy.cpu.Xor(7);
            AssertA(6);
            CheckFlags(false, false, false, false);
            Gameboy.cpu.Xor(200);
            AssertA(206);
            CheckFlags(false, false, false, false);
            Gameboy.cpu.Xor(206);
            AssertA(0);
            CheckFlags(true, false, false, false);
        }
    }
}
