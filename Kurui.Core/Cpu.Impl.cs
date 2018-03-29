using System;
using System.Runtime.CompilerServices;

namespace Kurui.Core
{
    internal partial class Cpu
    {
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
        public void Add(byte source, bool carry = false)
        {
            int result = carry && GetC() ? A + source + 1 : A + source;
            SetN(false);
            AddCarry(result);
            AddHalfCarry(A, source, carry);
            SetZ(result == 0);
            A = (byte) result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void And(byte source)
        {
            A = (byte) (A & source);
            SetZ(A == 0);
            SetC(false);
            SetN(false);
            SetH(true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Bit(byte source, byte index)
        {
            SetZ(!source.BitIsSet(index));
            SetN(false);
            SetH(true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Call(ushort source, bool condition = true)
        {
            if (!condition) return;
            SP.wide -= 2;
            Gameboy.mmu[SP.wide] = new Imm(){wide = PC, writeWide = true};
            PC.wide = source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Ccf()
        {
            SetN(false);
            SetH(false);
            SetC(!GetC());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Cp(byte source)
        {
            int result =  A - source;
            SetN(true);
            SubCarry(result);
            SubHalfCarry(A, source, false);
            SetZ(result == 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Cpl()
        {
            A =(byte)(A ^ 0xFF);
            SetN(true);
            SetH(true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Daa()
        {
            //Stolen from reddit, so I'm sure it's broken
            int offset = 0;
            if (GetH() || ( !GetN() && ( A & 0x0F ) > 9 ))
            {
                offset = 6;
            }

            if (GetC() || ( !GetN() && A > 0x99 ))
            {
                offset |= 0x60;
                SetC(true);
            }

            A += (byte) (A + (GetN() ? -offset : offset));
            SetZ(A == 0);
            SetH(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte Dec(byte destination)
        {
            SubHalfCarry(destination, destination - 1, false);
            SetN(false);
            destination -= 1;
            SetZ(destination == 0);
            return destination;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort Dec(ushort destination)
        {
            destination -= 1;
            return destination;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Di()
        {
            Gameboy.interrupts.DisableInterrupts();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Ei()
        {
            Gameboy.interrupts.EnableInterrupts();

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Halt()
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte Inc(byte destination)
        {
            AddHalfCarry(destination, destination+1, false);
            SetN(false);
            destination += 1;
            SetZ(destination == 0);
            return destination;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort Inc(ushort destination)
        {
            destination += 1;
            return destination;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Jp(ushort source, bool condition = true)
        {
            if (!condition) return;
            PC.wide = source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Jr(sbyte source, bool condition = true)
        {
            if (!condition) return;
            PC.wide = (ushort) ( PC.wide + source );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void LdHl(sbyte offset)
        {
            int result = (SP.wide + offset);

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
            SetZ(false);
            SetN(false);
            HL.wide = (ushort) result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Or(byte source)
        {
            A = (byte)(A | source);
            SetZ(A == 0);
            SetC(false);
            SetN(false);
            SetH(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Pop(ref ushort destination)
        {
            SP.wide += 2;
            destination = Gameboy.mmu[SP.wide];
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Push(ushort source)
        {
            SP.wide -= 2;
            Gameboy.mmu[SP.wide] = new Imm(){wide = source, writeWide = true};
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte Res(byte destination, byte index)
        {
            destination.ClearBit(index);
            return destination;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Ret(bool condition = true)
        {
            if (!condition) return;
            PC = Gameboy.mmu[SP.wide];
            SP.wide += 2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Rl(ref byte destination)
        {
            int carry = GetC() ? 1 : 0;
            SetC(destination.BitIsSet(7));
            destination = (byte) ((destination << 1) | carry);
            SetN(false);
            SetH(false);
            SetZ(destination == 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Rlc(ref byte destination)
        {
            SetC(destination.BitIsSet(7));
            int flipped = destination.BitIsSet(7) ? 1 : 0;
            destination = (byte) ((destination << 1) | flipped);
            SetN(false);
            SetH(false);
            SetZ(destination == 0);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Rr(ref byte destination)
        {
            int carry = GetC() ? 128 : 0;
            SetC(destination.BitIsSet(0));
            destination = (byte)((destination >> 1) | carry);
            SetN(false);
            SetH(false);
            SetZ(destination == 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Rrc(ref byte destination)
        {
            SetC(destination.BitIsSet(0));
            int flipped = destination.BitIsSet(0) ? 128 : 0;
            destination = (byte)((destination >> 1) | flipped);
            SetN(false);
            SetH(false);
            SetZ(destination == 0);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Rst(byte source)
        {
            SP.wide -= 2;
            Gameboy.mmu[SP.wide] = new Imm() { wide = PC, writeWide = true };
            PC.wide = source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Sub(byte source, bool carry = false)
        {
            int result = carry && GetC() ? A - source - 1 : A - source;
            SetN(true);
            SubCarry(result);
            SubHalfCarry(A, source, carry);
            SetZ(result == 0);
            A = (byte) result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Scf()
        {
            SetN(false);
            SetH(false);
            SetC(true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte Set(byte destination, byte index)
        {
            destination.SetBit(index);
            return destination;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Sla(ref byte destination)
        {

            SetC(destination.BitIsSet(7));
            destination = (byte)(destination << 1);
            SetN(false);
            SetH(false);
            SetZ(destination == 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Sra(ref byte destination)
        {
            int carry = destination & 128;
            SetC(destination.BitIsSet(0));
            destination = (byte)((destination >> 1) | carry);
            SetN(false);
            SetH(false);
            SetZ(destination == 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Srl(ref byte destination)
        {

            SetC(destination.BitIsSet(0));
            destination = (byte)(destination >> 1);
            SetN(false);
            SetH(false);
            SetZ(destination == 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Stop()
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte Swap(byte destination)
        {
            int lo2hi = ( destination & 0x0F ) << 4;
            int hi2lo = ( destination & 0xF0 ) >> 4;
            SetN(false);
            SetH(false);
            SetC(false);
            destination = (byte) ( lo2hi | hi2lo );
            SetZ(destination == 0);
            return destination;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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