using System.Runtime.InteropServices;

namespace Kurui.Core
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct Imm
    {
        [FieldOffset(1)] public byte   hi;
        [FieldOffset(0)] public byte   lo;
        [FieldOffset(0)] public ushort wide;

        public static implicit operator Imm(ushort wide)
        {
            return new Imm {wide = wide};
        }

        public static implicit operator Imm(byte lo)
        {
            return new Imm {lo = lo, hi = 0};
        }

        public static implicit operator byte(Imm i)
        {
            return i.lo;
        }

        public static implicit operator ushort(Imm i)
        {
            return i.wide;
        }

        public static Imm operator +(Imm a, Imm b)
        {
            return new Imm {wide = (ushort) ( a.wide + b.wide )};
        }

        public static Imm operator +(Imm a, byte b)
        {
            return new Imm {wide = (ushort) ( a.wide + b )};
        }

        public static Imm operator +(Imm a, sbyte b)
        {
            return new Imm {wide = (ushort) ( a.wide + b )};
        }

        public static Imm operator -(Imm a, Imm b)
        {
            return new Imm {wide = (ushort) ( a.wide - b.wide )};
        }

        public static Imm operator -(Imm a, byte b)
        {
            return new Imm {wide = (ushort) ( a.wide - b )};
        }
    }
}