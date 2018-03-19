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



    internal interface IRom
    {
        RomHeader Header { get; }

        ///Gets a byte or ushort from the rom
        Imm this[int index] { get; set; }

    }

    class BasicRom : IRom
    {
        public RomHeader Header { get; private set; }
        private byte[] data = new byte[0x8000];

        public BasicRom(byte[] bytes)
        {
            Header = new RomHeader(in bytes);
            bytes.CopyTo(data, 0);
        }

        public Imm this[int index]
        {
            get => data.ReadImm(index);
            set
            {
                /*do nothing, read only*/
            }
        }
    }

    class Mbc1Rom : IRom
    {
        public RomHeader Header { get; private set; }
        private byte[] data = new byte[0x3F8000];
        private byte bankIndex = 1;
        private IRam ram; //todo: create ram options and ram builder
        private bool romBankingMode = true;

        public Mbc1Rom(byte[] bytes)
        {
            Header = new RomHeader(in bytes);
            bytes.CopyTo(data, 0);
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
                        return data.ReadImm(index - bankIndex * 0x4000);
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
                        byte register = (byte)(value.lo | 0b11_111);
                        if (register % 32 == 0 )
                        {
                            register++;
                        }

                        bankIndex = register;
                        break;
                    case int x when (x >= 0xA000 && x <= 0xBFFF) :
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
                            bankIndex = (byte) ( ( bankIndex & 0b0011_1111 ) | ( ( (byte) value ) & 0b1100_0000 )); //set top two bits according to argument
                            break;
                        }

                    default:
                        break;


                }
            }
        }
    }
}