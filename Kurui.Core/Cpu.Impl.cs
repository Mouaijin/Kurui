using System;

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
        public void Add(ref byte destination, byte source, bool carry = false) { }
        public void Add(ref ushort destination, ushort source, bool carry = false) { }
        public void AddSP(sbyte offset) { }
        public void And(byte source) { }
        public void Bit(byte source) { }
        public void Call(ushort source, bool condition = true) { }
        public void Ccf() { }
        public void Cp(byte source) { }
        public void Cpl() { }
        public void Daa() { }
        public void Dec(ref byte destination) { }
        public void Dec(ref ushort destination) { }
        public void Di() { }
        public void Ei() { }
        public void Halt() { }
        public void Inc(ref byte destination) { }
        public void Inc(ref ushort destination) { }
        public void Jp(ushort source, bool condition = true) { }
        public void Jr(sbyte source,bool condition = true) { }
        public void Ld(ref byte destination, byte source) { }
        public void Ld(ref ushort destination, ushort source) { }
        public void Ldd(ref byte destination, byte source) { }
        public void Or(byte source) { }
        public void Pop(ref ushort destination) { }
        public void Push(ushort source) { }
        public void Res(ref byte destination, byte index) { }
        public void Ret(bool condition = true) { }
        public void Rl(ref byte destination, bool carry = false) { }
        public void Rla(bool carry = false) { }
        public void Rr(ref byte destination, bool carry = false) { }
        public void Rra(bool carry = false) { }
        public void Rst(byte source) { }
        public void Sub(ref byte destination, byte source, bool carry = false) { }
        public void Sub(ref ushort destination, ushort source, bool carry = false) { }
        public void Scf() { }
        public void Set(ref byte destination, byte index) { }
        public void Sla(ref byte destination, byte source) { }
        public void Sra(ref byte destination, byte source) { }
        public void Srl(ref byte destination, byte source) { }
        public void Stop() { }
        public void Swap(ref byte destination) { }
        public void Xor(byte source) { }

    }
}