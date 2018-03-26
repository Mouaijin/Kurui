using System;
using System.Runtime.CompilerServices;

namespace Kurui.Core
{
    internal partial class Cpu
    {
        /*
         Methods will receive an immediate value, if needed (calculated from memory if needed ELSEWHERE) (elided, if only one source possible)
         Methods will modify flags directly
         Methods will an Imm reference to modify in memory
         Methods will all be void
        */
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddCarry(int val) =>
            SetC(val > 0xFF);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddHalfCarry(int dest, int val, bool carry) =>
            SetH(carry && GetC()
                     ? ( dest & 0x0F ) + ( val & 0x0F ) + 1 > 0x0F
                     : ( dest & 0x0F ) + ( val & 0x0F ) > 0x0F);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SubCarry(int val) =>
            SetC(val < 0);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SubHalfCarry(int dest, int val, bool carry) =>
            SetH(carry && GetC() ? ( dest & 0x0F ) - ( val & 0x0F ) - 1 < 0 : ( dest & 0x0F ) - ( val & 0x0F ) < 0);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetFlags(bool? z = null, bool? n = null, bool? h = null, bool? c = null)
        {
            if (z.HasValue) SetZ(z.Value);
            if (n.HasValue) SetN(n.Value);
            if (h.HasValue) SetN(h.Value);
            if (c.HasValue) SetC(c.Value);
        }

        public void Add(ref byte destination, byte source, bool carry = false)
        {
            int result = carry && GetC() ? destination + source + 1 : destination + source;
            SetN(false);
            AddCarry(result);
            AddHalfCarry(destination, source, carry);
            SetZ(result == 0);
            destination = (byte) result;
        }


        public void Add(ushort source, bool carry = false)
        {
            int result = carry && GetC() ? HL.wide + source + 1 : HL.wide + source;
            SetN(false);
            SetC(result > 0xFFFF);
            SetH(carry && GetC()
                     ? ( HL.wide & 0x00FF ) + ( source & 0x00FF ) + 1 > 0x00FF
                     : ( HL.wide & 0x00FF ) + ( source & 0x00FF ) > 0x00FF);
            SetZ(result == 0);
            HL.wide = (ushort) result;
        }


        public void AddSP(sbyte offset)
        {
        }

        public void And(byte source)
        {
        }

        public void Bit(byte source)
        {
        }

        public void Call(ushort source, bool condition = true)
        {
        }

        public void Ccf()
        {
        }

        public void Cp(byte source)
        {
        }

        public void Cpl()
        {
        }

        public void Daa()
        {
        }

        public void Dec(ref byte destination)
        {
        }

        public void Dec(ref ushort destination)
        {
        }

        public void Di()
        {
        }

        public void Ei()
        {
        }

        public void Halt()
        {
        }

        public void Inc(ref byte destination)
        {
        }

        public void Inc(ref ushort destination)
        {
        }

        public void Jp(ushort source, bool condition = true)
        {
        }

        public void Jr(sbyte source, bool condition = true)
        {
        }

        public void Ld(ref byte destination, byte source)
        {
        }

        public void Ld(ref ushort destination, ushort source)
        {
        }

        public void Ldd(ref byte destination, byte source)
        {
        }

        public void Or(byte source)
        {
        }

        public void Pop(ref ushort destination)
        {
        }

        public void Push(ushort source)
        {
        }

        public void Res(ref byte destination, byte index)
        {
        }

        public void Ret(bool condition = true)
        {
        }

        public void Rl(ref byte destination, bool carry = false)
        {
        }

        public void Rla(bool carry = false)
        {
        }

        public void Rr(ref byte destination, bool carry = false)
        {
        }

        public void Rra(bool carry = false)
        {
        }

        public void Rst(byte source)
        {
        }

        public void Sub(ref byte destination, byte source, bool carry = false)
        {
            int result = carry && GetC() ? destination - source - 1 : destination - source;
            SetN(true);
            SubCarry(result);
            SubHalfCarry(destination, source, carry);
            SetZ(result == 0);
            destination = (byte) result;
        }

        public void Sub(ref ushort destination, ushort source, bool carry = false)
        {
        }

        public void Scf()
        {
        }

        public void Set(ref byte destination, byte index)
        {
        }

        public void Sla(ref byte destination, byte source)
        {
        }

        public void Sra(ref byte destination, byte source)
        {
        }

        public void Srl(ref byte destination, byte source)
        {
        }

        public void Stop()
        {
        }

        public void Swap(ref byte destination)
        {
        }

        public void Xor(byte source)
        {
        }
    }
}