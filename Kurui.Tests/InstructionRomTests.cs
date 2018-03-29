using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kurui.Core;
using NUnit.Framework;

namespace Kurui.Tests
{
    [TestFixture]
    class InstructionRomTests
    {
        public InstructionRomTests()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

        }
        [SetUp]
        public void Init()
        {

            Gameboy.Reset();
        }

        [Test]
        public void Special01()
        {
            Gameboy.LoadRom("./Roms/cpu_instrs/individual/01-special.gb");
            Gameboy.cpu.Run();
        }

        [Test]
        public void Interrupts02()
        {
            Gameboy.LoadRom("./Roms/cpu_instrs/individual/02-interrupts.gb");

        }

        [Test]
        public void OpSpHl03()
        {
            Gameboy.LoadRom("./Roms/cpu_instrs/individual/03-op sp,hl.gb");

        }

        [Test]
        public void OpRImm04()
        {
            Gameboy.LoadRom("./Roms/cpu_instrs/individual/04-op r,imm.gb");

        }

        [Test]
        public void OpRp05()
        {
            Gameboy.LoadRom("./Roms/cpu_instrs/individual/05-op rp.gb");

        }

        [Test]
        public void LdRR06()
        {
            Gameboy.LoadRom("./Roms/cpu_instrs/individual/06-ld r,r.gb");

        }

        [Test]
        public void JrJpCallRetRst07()
        {
            Gameboy.LoadRom("./Roms/cpu_instrs/individual/07-jr,jp,call,ret,rst.gb");

        }

        [Test]
        public void MiscInst08()
        {
            Gameboy.LoadRom("./Roms/cpu_instrs/individual/08-misc instrs.gb");

        }

        [Test]
        public void OpRR09()
        {
            Gameboy.LoadRom("./Roms/cpu_instrs/individual/09-op r,r.gb");

        }

        [Test]
        public void BitOps10()
        {

            Gameboy.LoadRom("./Roms/cpu_instrs/individual/10-bit ops.gb");
        }

        [Test]
        public void OpAHLm11()
        {
            Gameboy.LoadRom("./Roms/cpu_instrs/individual/11-op a,(hl).gb");

        }
    }
}
