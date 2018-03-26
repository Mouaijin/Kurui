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
            int result = SP.wide + offset;
            SetN(false);
            if (offset > 0)
            {
                SetC(result > 0xFFFF);
                SetH((HL.wide & 0x00FF) + offset > 0x00FF);
            }
            if (offset < 0)
            {
                SetC(result < 0);
                SetH((HL.wide & 0x00FF) - offset < 0);
            }

            SP.wide = (ushort) result;
        }

        public void And(byte source)
        {
            A = (byte) (A & source);
            SetZ(A == 0);
            SetC(false);
            SetN(false);
            SetH(true);
        }

        public void Bit(byte source, byte index)
        {
            SetZ(!source.BitIsSet(index));
            SetN(false);
            SetH(true);
        }

        public void Call(ushort source, bool condition = true)
        {
        }

        public void Ccf()
        {
        }

        public void Cp(byte source)
        {
            int result =  A - source;
            SetN(true);
            SubCarry(result);
            SubHalfCarry(A, source, false);
            SetZ(result == 0);
        }

        public void Cpl()
        {
        }

        public void Daa()
        {
        }

        public void Dec(ref byte destination)
        {
            SubHalfCarry(destination, destination - 1, false);
            SetN(false);
            destination -= 1;
            SetZ(destination == 0);
        }

        public void Dec(ref ushort destination)
        {
            destination -= 1;
        }

        public void Di()
        {
            Gameboy.interrupts.DisableInterrupts();
        }

        public void Ei()
        {
            Gameboy.interrupts.EnableInterrupts();

        }

        public void Halt()
        {
        }

        public void Inc(ref byte destination)
        {
            AddHalfCarry(destination, destination+1, false);
            SetN(false);
            destination += 1;
            SetZ(destination == 0);
        }

        public void Inc(ref ushort destination)
        {
            destination += 1;
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
            A = (byte)(A | source);
            SetZ(A == 0);
            SetC(false);
            SetN(false);
            SetH(false);
        }

        public void Pop(ref ushort destination)
        {
        }

        public void Push(ushort source)
        {
        }

        public void Res(ref byte destination, byte index)
        {
            destination.ClearBit(index);
        }

        public void Ret(bool condition = true)
        {
        }

        public void Rl(ref byte destination)
        {
            int carry = GetC() ? 1 : 0;
            SetC(destination.BitIsSet(7));
            destination = (byte) ((destination << 1) | carry);
            SetN(false);
            SetH(false);
            SetZ(destination == 0);
        }
        public void Rlc(ref byte destination)
        {
            SetC(destination.BitIsSet(7));
            int flipped = destination.BitIsSet(7) ? 1 : 0;
            destination = (byte) ((destination << 1) | flipped);
            SetN(false);
            SetH(false);
            SetZ(destination == 0);
        }


        public void Rr(ref byte destination)
        {
            int carry = GetC() ? 128 : 0;
            SetC(destination.BitIsSet(0));
            destination = (byte)((destination >> 1) | carry);
            SetN(false);
            SetH(false);
            SetZ(destination == 0);
        }
        public void Rrc(ref byte destination)
        {
            SetC(destination.BitIsSet(0));
            int flipped = destination.BitIsSet(0) ? 128 : 0;
            destination = (byte)((destination >> 1) | flipped);
            SetN(false);
            SetH(false);
            SetZ(destination == 0);
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

        public void Scf()
        {
        }

        public void Set(ref byte destination, byte index)
        {
            destination.SetBit(index);
        }

        public void Sla(ref byte destination)
        {

            SetC(destination.BitIsSet(7));
            destination = (byte)(destination << 1);
            SetN(false);
            SetH(false);
            SetZ(destination == 0);
        }

        public void Sra(ref byte destination, byte source)
        {
            int carry = destination & 128;
            SetC(destination.BitIsSet(0));
            destination = (byte)((destination >> 1) | carry);
            SetN(false);
            SetH(false);
            SetZ(destination == 0);
        }

        public void Srl(ref byte destination, byte source)
        {

            SetC(destination.BitIsSet(0));
            destination = (byte)(destination >> 1);
            SetN(false);
            SetH(false);
            SetZ(destination == 0);
        }

        public void Stop()
        {
        }

        public void Swap(ref byte destination)
        {
        }

        public void Xor(byte source)
        {
            A = (byte)(A ^ source);
            SetZ(A == 0);
            SetC(false);
            SetN(false);
            SetH(false);
        }
    }
}