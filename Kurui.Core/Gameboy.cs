using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurui.Core
{
    public static class Gameboy
    {
        internal static IRom cart;
        internal static Memory mmu;
        internal static Cpu cpu;
        internal static Joypad joypad;
        internal static Interrupts interrupts;

        static Gameboy()
        {
            mmu = new Memory();
            cpu = new Cpu();
            joypad = new Joypad();
            interrupts = new Interrupts();
        }

        public static void LoadRom(string path) => cart = RomFactory.LoadRom(path);
    }
}
