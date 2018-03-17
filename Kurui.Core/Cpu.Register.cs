using System;
using System.Runtime.InteropServices;

namespace Kurui.Core
{
    internal partial class Cpu
    {
        private Register AF, BC, DE, HL, SP, PC;
        private ref byte A => ref AF.hi;
        private ref byte F => ref AF.lo;
        private ref byte B => ref BC.hi;
        private ref byte C => ref BC.lo;
        private ref byte D => ref DE.hi;
        private ref byte E => ref DE.lo;
        private ref byte H => ref HL.hi;
        private ref byte L => ref HL.lo;


        private void SetZ(bool bit) => F = bit ? F.SetBit(0) : F.ClearBit(0);
        private void SetN(bool bit) => F = bit ? F.SetBit(1) : F.ClearBit(1);
        private void SetH(bool bit) => F = bit ? F.SetBit(2) : F.ClearBit(2);
        private void SetC(bool bit) => F = bit ? F.SetBit(3) : F.ClearBit(3);
        private bool GetZ() => F.BitIsSet(0);
        private bool GetN() => F.BitIsSet(1);
        private bool GetH() => F.BitIsSet(2);
        private bool GetC() => F.BitIsSet(3);

        [StructLayout(LayoutKind.Explicit)]
        internal struct Register
        {
            [FieldOffset(1)] public byte hi;
            [FieldOffset(0)] public byte lo;
            [FieldOffset(0)] public ushort wide;
        }
    }
}