namespace Kurui.Core
{
    internal class Memory
    {
        private byte[] workRam = new byte[0x2000], 
                       hram = new byte[0x80],
            vram = new byte[0x2000],
            sprites = new byte[0x9F];

        public Imm this[int index]
        {
            get
            {
                if (index < 0x8000 || ( index >= 0xA000 && index < 0xC000 ))
                {
                    return Gameboy.cart[index];
                }

                if (index >= 0x8000 && index <= 0X9FFF) return vram.ReadImm(index - 0x8000); //todo: vram
                if (index >= 0xC000 && index <= 0XDFFF) return  workRam.ReadImm(index - 0xC000);
                if (index >= 0xE000 && index <= 0XFDFF) return workRam.ReadImm(index - 0xE000);
                if (index >= 0xFE00 && index <= 0XFE9F) return sprites.ReadImm(index - 0xFE00); //todo: sprite table
                if (index >= 0xFEA0 && index <= 0XFEFF) return 0; //unusable- 0s on DMG, random shit on CGB
                if (index >= 0xFF00 && index <= 0XFF7F) return 0;//todo: IO registers
                if (index >= 0xFF80 && index <= 0xFFFE) return hram.ReadImm(index - 0xFF80);
                if (index == 0xFF0F) return Gameboy.interrupts.RequestRegister;
                if (index == 0xFFFF) return Gameboy.interrupts.EnableRegister;
                return 0;
            }
            set
            {
                if (index < 0x8000 || (index >= 0xA000 && index < 0xC000))
                {
                    Gameboy.cart[index] = value;
                }

                if (index >= 0x8000 && index <= 0x9FFF) vram.WriteImm(value, index - 0x8000); //todo: vram
                if (index >= 0xC000 && index <= 0xDFFF) workRam.WriteImm(value, index - 0xC000);
                if (index >= 0xE000 && index <= 0xFDFF) workRam.WriteImm(value, index - 0xE000);
                //if (index >= 0xff00 && index <= 0xff7f) return 0; //todo: IO registers
                if (index >= 0xFF80 && index <= 0xFFFE) hram.WriteImm(value, index - 0xFF80);
                if (index == 0xFF0F) Gameboy.interrupts.RequestRegister = value;
                if (index == 0xFFFF) Gameboy.interrupts.EnableRegister = value;
            }
        }
    }
}
