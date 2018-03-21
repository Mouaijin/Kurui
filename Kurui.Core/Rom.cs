using System;
using System.IO;
using System.Text;

namespace Kurui.Core
{
    internal class RomHeader
    {
        public string Title     { get; private set; }
        public bool   ColorOnly { get; private set; }
        public int    RomKB     { get; private set; }
        public int    RomBanks  { get; private set; }
        public int    RamKB     { get; private set; }
        public int    RamBanks  { get; private set; }
        public bool   Japanese  { get; private set; }

        public RomHeader(in byte[] bytes)
        {
            Title     = Encoding.ASCII.GetString(bytes, 0x0134, 15);
            ColorOnly = bytes[0x0143] == 0xC0;
            var romSize   = bytes[0x0148];
            var bankIndex = romSize > 8 ? romSize - 72 : romSize;
            RomKB    = new[] {32, 64, 128, 256, 512, 1024, 2048, 4096, 8192, 1152, 1280, 1536}[bankIndex];
            RomBanks = new[] {1, 4, 8, 16, 32, 64, 128, 256, 512, 72, 80, 96}[bankIndex];
            RamKB    = new[] {0, 2, 8, 32, 128, 64}[bytes[0x0149]];
            RamBanks = new[] {0, 1, 1, 4, 16, 8}[bytes[0x0148]];
            Japanese = bytes[0x014A] == 0;
        }
    }


    internal interface IRom
    {
        RomHeader Header { get; }

        ///Gets a byte or ushort from the rom
        Imm this[int index] { get; set; }
    }


    internal class RomFactory
    {
        public static IRom LoadRom(string path)
        {
            byte[] data = File.ReadAllBytes(path);

            switch (data[0x0147])
            {
                case 0:
                case 8:
                case 9: return new BasicRom(data);
                case 1:
                case 2: return new Mbc1Rom(data);
                case 0xf:
                case 0x10:
                case 0x11:
                case 0x12:
                case 0x13: return new Mbc3Rom(data);
                case 0x19:
                case 0x1a:
                case 0x1b:
                case 0x1c:
                case 0x1d:
                case 0x1e: return new Mbc5Rom(data);
                default:
                    throw new Exception("Unsupported cartridge type");
            }

            //00h ROM ONLY                 19h MBC5
            //01h MBC1                     1Ah MBC5+RAM
            //02h MBC1+RAM                 1Bh MBC5+RAM + BATTERY
            //03h MBC1+RAM + BATTERY         1Ch MBC5+RUMBLE
            //05h MBC2                     1Dh MBC5+RUMBLE + RAM
            //06h MBC2+BATTERY             1Eh MBC5+RUMBLE + RAM + BATTERY
            //08h ROM+RAM                  20h MBC6
            //09h ROM+RAM + BATTERY          22h MBC7+SENSOR + RUMBLE + RAM + BATTERY
            //0Bh MMM01
            //0Ch MMM01+RAM
            //0Dh MMM01+RAM + BATTERY
            //0Fh MBC3+TIMER + BATTERY
            //10h MBC3+TIMER + RAM + BATTERY   FCh POCKET CAMERA
            //11h                                  MBC3                     FDh BANDAI TAMA5
            //12h MBC3+RAM                 FEh     HuC3
            //13h MBC3+RAM + BATTERY         FFh HuC1+RAM + BATTERY
        }
    }

    class BasicRom : IRom
    {
        public  RomHeader Header { get; private set; }
        private byte[]    data = new byte[0x8000];
        private Ram8k     ram  = new Ram8k();

        public BasicRom(byte[] bytes)
        {
            Header = new RomHeader(in bytes);
            bytes.CopyTo(data, 0);
        }

        public Imm this[int index]
        {
            get
            {
                if (index >= 0xa000 && index <= 0xbfff)
                {
                    return ram[index - 0xa000];
                }

                return data.ReadImm(index);
            }
            set
            {
                if (index >= 0xa000 && index <= 0xbfff)
                {
                    ram[index - 0xa000] = value;
                }
            }
        }
    }

    class Mbc1Rom : IRom
    {
        public   RomHeader Header { get; private set; }
        private  byte[]    data           = new byte[0x3F8000];
        private  byte      bankIndex      = 1;
        internal Ram32k    ram            = new Ram32k();
        internal bool      romBankingMode = true;

        public Mbc1Rom(byte[] bytes)
        {
            Header = new RomHeader(in bytes);
            bytes.CopyTo(data, 0);
            //todo: load ram if present
        }

        public Imm this[int index]
        {
            get
            {
                switch (index)
                {
                    case int x when x <= 0x3FFF:
                        return data.ReadImm(index);
                    case int x when x <= 0x7FFF:
                        var indexoffset = index - 0x4000;
                        var bankoffset  = bankIndex * 0x4000;
                        return data.ReadImm(indexoffset + bankoffset);
                    case int x when x >= 0xA000 && x <= 0xBFFF:
                        return ram[index - 0xA000];
                    default: return 0;
                }
            }
            set
            {
                switch (index)
                {
                    case int x when x >= 0 && x <= 0x1FFF:
                        if (value == 0)
                            ram.Disable();
                        else
                            ram.Enable();
                        break;
                    case int x when x >= 0x2000 && x <= 0x3FFF:
                        byte register = (byte) ( value.lo & 0b11_111 );
                        //registers 0x20, 0x40, 0x60, 0x80 all are fake, and actually select bank+1
                        if (register % 32 == 0)
                        {
                            register++;
                        }

                        bankIndex = register;
                        break;
                    case int x when ( x >= 0xA000 && x <= 0xBFFF ):
                        ram[index - 0xA000] = value;
                        break;
                    case int x when x >= 0x6000 && x <= 0x7FFF:
                        romBankingMode = x == 0;
                        break;
                    case int x when x >= 0x4000 && x <= 0x5FFF:
                        if (!romBankingMode)
                        {
                            ram.SwapBank(value);
                            break;
                        }
                        else
                        {
                            bankIndex = (byte) ( ( bankIndex & 0b0011_1111 ) |
                                                 ( ( (byte) value ) & 0b1100_0000 )
                                               ); //set top two bits according to argument
                            break;
                        }

                    default:
                        break;
                }
            }
        }
    }

    class Mbc3Rom : IRom
    {
        public   RomHeader   Header { get; private set; }
        private  byte[]      data      = new byte[0x3F8000];
        private  byte        bankIndex = 1;
        internal Ram32kTimer ram       = new Ram32kTimer();

        public Mbc3Rom(byte[] bytes)
        {
            Header = new RomHeader(in bytes);
            bytes.CopyTo(data, 0);
            //todo: load ram if present
        }

        public Imm this[int index]
        {
            get
            {
                switch (index)
                {
                    case int x when x <= 0x3FFF:
                        return data.ReadImm(index);
                    case int x when x <= 0x7FFF:
                        var indexoffset = index - 0x4000;
                        var bankoffset  = bankIndex * 0x4000;
                        return data.ReadImm(indexoffset + bankoffset);
                    case int x when x >= 0xA000 && x <= 0xBFFF:
                        return ram[index - 0xA000];
                    default: return 0;
                }
            }
            set
            {
                switch (index)
                {
                    case int x when x >= 0 && x <= 0x1FFF:
                        if (value == 0)
                            ram.Disable();
                        else
                            ram.Enable();
                        break;
                    case int x when x >= 0x2000 && x <= 0x3FFF:
                        byte register = (byte) ( value.lo & 0b111_1111 );
                        if (register == 0)
                        {
                            register++;
                        }

                        bankIndex = register;
                        break;
                    case int x when ( x >= 0xA000 && x <= 0xBFFF ):
                        ram[index - 0xA000] = value;
                        break;
                    case int x when x >= 0x6000 && x <= 0x7FFF:
                        ram.Latch(value);
                        break;
                    case int x when x >= 0x4000 && x <= 0x5FFF:

                        ram.SwapBank(value);
                        break;


                    default:
                        break;
                }
            }
        }
    }

    class Mbc5Rom : IRom
    {
        public   RomHeader Header { get; private set; }
        private  byte[]    data      = new byte[0x3F8000];
        private  ushort    bankIndex = 1;
        internal Ram128k   ram       = new Ram128k();

        public Mbc5Rom(byte[] bytes)
        {
            Header = new RomHeader(in bytes);
            bytes.CopyTo(data, 0);
            //todo: load ram if present
        }

        public Imm this[int index]
        {
            get
            {
                switch (index)
                {
                    case int x when x <= 0x3FFF:
                        return data.ReadImm(index);
                    case int x when x <= 0x7FFF:
                        var indexoffset = index - 0x4000;
                        var bankoffset  = bankIndex * 0x4000;
                        return data.ReadImm(indexoffset + bankoffset);
                    case int x when x >= 0xA000 && x <= 0xBFFF:
                        return ram[index - 0xA000];
                    default: return 0;
                }
            }
            set
            {
                switch (index)
                {
                    case int x when x >= 0 && x <= 0x1FFF:
                        if (value == 0)
                            ram.Disable();
                        else
                            ram.Enable();
                        break;
                    case int x when x >= 0x2000 && x <= 0x2FFF:
                        bankIndex = (ushort) ( ( bankIndex & 0b11111111_00000000 ) | value.lo );
                        break;
                    case int x when x >= 0x3000 && x <= 0x3FFF:
                        bankIndex = (ushort) ( ( bankIndex & 0b00000000_11111111 ) | value.lo << 8 );
                        break;
                    case int x when ( x >= 0xA000 && x <= 0xBFFF ):
                        ram[index - 0xA000] = value;
                        break;

                    case int x when x >= 0x4000 && x <= 0x5FFF:

                        ram.SwapBank(value);
                        break;


                    default:
                        break;
                }
            }
        }
    }
}