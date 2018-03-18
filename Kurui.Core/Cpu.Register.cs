using System;

namespace Kurui.Core
{
    internal partial class Cpu
    {
        internal Imm AF = 0x01B0, BC = 0x0013, DE = 0x00D8, HL = 0x014D, SP = 0xFFFE, PC = 0x0100;
        private ref byte A => ref AF.hi;
        private ref byte F => ref AF.lo;
        private ref byte B => ref BC.hi;
        private ref byte C => ref BC.lo;
        private ref byte D => ref DE.hi;
        private ref byte E => ref DE.lo;
        private ref byte H => ref HL.hi;
        private ref byte L => ref HL.lo;


        internal void SetZ(bool bit) => F = bit ? F.SetBit(7) : F.ClearBit(7);
        internal void SetN(bool bit) => F = bit ? F.SetBit(6) : F.ClearBit(6);
        internal void SetH(bool bit) => F = bit ? F.SetBit(5) : F.ClearBit(5);
        internal void SetC(bool bit) => F = bit ? F.SetBit(4) : F.ClearBit(4);
        internal bool GetZ() => F.BitIsSet(7);
        internal bool GetN() => F.BitIsSet(6);
        internal bool GetH() => F.BitIsSet(5);
        internal bool GetC() => F.BitIsSet(4);
        internal void ClearFlags() => F = 0;

        ///Returned from CPU functions to represent new CPU flags
        internal struct FlagSet
        {
            public byte flags;

            ///Always remember to set correct bools for flags not being set
            public FlagSet(bool z, bool n, bool h, bool c)
            {
                int _z = z ? 0b10000000 : 0;
                int _n = n ? 0b01000000 : 0;
                int _h = h ? 0b00100000 : 0;
                int _c = c ? 0b00010000 : 0;
                flags = (byte) ( _z | _n | _h | _c );
            }
            ///Raw constructor
            public FlagSet(byte reg)
            {
                flags = reg;
            }
            public static implicit operator FlagSet(byte value)
            {
                return new FlagSet(value);
            }
        }
    }
}