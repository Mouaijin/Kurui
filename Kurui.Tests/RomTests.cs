using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Kurui.Core;

namespace Kurui.Tests
{
    class RomTests
    {
        public RomTests()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

        }
        [Test]
        public void Mbc1_1MB_Header_Test()
        {
            var bytes = File.ReadAllBytes("./Roms/rom_1MB.gb");
            Assert.NotNull(bytes);
            Mbc1Rom rom = new Mbc1Rom(bytes);
            Assert.AreEqual("mooneye-gb test", rom.Header.Title, "Title incorrect");
            Assert.False(rom.Header.Japanese, "Shouldn't be Japanese");
            Assert.AreEqual(8, rom.Header.RomBanks, "Wrong rom bank count");
            Assert.AreEqual(0, rom.Header.RamKB, "RAM shouldn't exist at all");
            Assert.AreEqual(128, rom.Header.RomKB, "ROM should be 128K");
            Assert.False(rom.Header.ColorOnly, "Shouldn't be a color rom");

        }

        [Test]
        public void Mbc1_1MB_ReadWrite_Test()
        {
            var bytes = File.ReadAllBytes("./Roms/rom_1MB.gb");
            Assert.NotNull(bytes);
            Mbc1Rom rom = new Mbc1Rom(bytes);
            //Bank 0 read
            Assert.AreEqual(0xFF, rom[0x3FF0].lo, "bank 0 lo byte incorrect");
            Assert.AreEqual(0x01FF, rom[0x3FFF].wide, "bank 0 wide incorrect");
            //Bank 1 read
            var imm = rom[0x4000]; 
            Assert.AreEqual(0x01, imm.lo, "bank 1 lo byte incorrect");
            Assert.AreEqual(0x7E, imm.hi, "bank 1 wide incorrect");
            //Bank swap
            rom[0x2001] = 2;
            Assert.AreEqual(0xFF02, rom[0x4000].wide, "bank 2 wide incorrect");
            rom[0x3FFF] = 3;
            Assert.AreEqual(0xFF03, rom[0x4000].wide, "bank 3 wide incorrect");
            //banking mode
            Assert.True(rom.romBankingMode, "rom banking should be true");
            rom[0x7000] = 1;
            Assert.False(rom.romBankingMode, "rom banking mode should be ram banking mode");
            //ram enable
            Assert.False(rom.ram.enabled, "ram should be disabled");
            rom[0x0100] = 10;
            Assert.True(rom.ram.enabled, "ram should be enabled");
            //ram write/read
            rom[0xA000] = 0xFF;
            rom[0xA001] = 0xEE;
            Assert.AreEqual(0xEEFF, rom[0xA000].wide, "ram bank 0 should start with FF EE");
            //swap to ram bank 1
            rom[0x4001] = 1;
            rom[0xA000] = 0xFF;
            Assert.AreEqual(0xFF, rom[0xA000].lo, "ram bank 1 should start with FF");
            //swap back to bank 0
            rom[0x4001] = 0;
            Assert.AreEqual(0xEEFF, rom[0xA000].wide, "ram bank 0 should still start with FF EE");



        }
    }
}
