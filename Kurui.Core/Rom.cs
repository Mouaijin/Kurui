using System;
using System.Collections.Generic;
using System.Text;

namespace Kurui.Core
{
    internal class RomHeader
    {
        public string Title { get; private set; }
        public bool ColorOnly { get; private set; }
        public int RomKB { get; private set; }
        public int RomBanks { get; private set; }
        public int RamKB { get; private set; }
        public int RamBanks { get; private set; }
        public bool Japanese { get; private set; }

        public RomHeader(in byte[] bytes)
        {
            Title = Encoding.ASCII.GetString(bytes, 0x0104, 0x0133 - 0x0104);
            ColorOnly = bytes[0x0143] == 0xC0;
            var romSize = bytes[0x0148];
            var bankIndex = romSize > 8 ? romSize - 72 : romSize;
            RomKB = 1 << romSize;
            RomBanks = new[] {1, 4, 8, 16, 32, 64, 128, 256, 512, 72, 80, 96}[bankIndex];
            RamKB = new[] {0, 2, 8, 32, 128, 64}[bytes[0x0149]];
            RamBanks = new[] {0, 1, 1, 4, 16, 8}[bytes[0x0148]];
            Japanese = bytes[0x014A] == 0;
        }
    }

    internal interface IRam
    {

    }
    internal interface IRom
    {
        RomHeader Header { get;  }
        ///Gets a byte or ushort from the rom
        Imm this[int index] { get; set; }

    }
}

