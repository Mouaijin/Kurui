﻿using System;
using System.Runtime.CompilerServices;

namespace Kurui.Core
{
    //Base Cpu class
    internal partial class Cpu
    {
        internal void Step()
        {
            byte op = Gameboy.mmu[PC.wide];
            Imm arg = Gameboy.mmu[(ushort)(PC.wide + 1)];
            switch (op)
            {
                case 0x00: break; //NOP
                case 0x01:
                    BC.wide = arg.wide;
                    break;
                case 0x02:
                    Gameboy.mmu[BC.wide] = A;
                    break;
                case 0x03:
                    BC = Inc(BC.wide); break;
                case 0x04:
                    B = Inc(B);
                    break;
                case 0x05:
                    B = Dec(B);
                    break;
                case 0x06:
                    B = arg.lo;
                    break;
                case 0x07: Rlc(ref A);
                    break;
                case 0x08:
                    Gameboy.mmu[SP.wide] = arg.wide;
                    break;
                case 0x09: Add(BC.wide); break;
                case 0x0A:
                    A = Gameboy.mmu[BC.wide];
                    break;
                case 0x0B:
                    BC.wide = Dec(BC.wide);
                    break;
                case 0x0C:
                    C = Inc(C); break;
                case 0x0D:
                    C = Dec(C); break;
                case 0x0E:
                    C = arg.lo; break;
                case 0x0F: Rrc(ref A); break;
                case 0x10: Stop(); break;
                case 0x11:
                    DE.wide = arg.wide; break;
                case 0x12:
                    Gameboy.mmu[DE.wide] = arg.wide; break;
                case 0x13:
                    DE = Inc(DE.wide); break;
                case 0x14:
                    D = Inc(D); break;
                case 0x15:
                    D = Dec(D); break;
                case 0x16:
                    D = arg.lo; break;
                case 0x17:
                    Rl(ref A); break;
                case 0x18: Jr((sbyte) arg.lo); break;
                case 0x19: Add(DE.wide); break;
                case 0x1A:
                    A = Gameboy.mmu[DE.wide]; break;
                case 0x1B:
                    DE = Dec(DE); break;
                case 0x1C:
                    E = Inc(E);  break;
                case 0x1D:
                    E = Dec(E); break;
                case 0x1E:
                    E = arg.lo; break;
                case 0x1F: Rr(ref A); break;
                case 0x20: Jr((sbyte) arg.lo, !GetZ()); break;
                case 0x21:
                    HL = arg.wide; break;
                case 0x22:
                    Gameboy.mmu[HL] = A;
                    HL = Inc(HL.wide);
                    break;
                case 0x23:
                    HL = Inc(HL.wide); break;
                case 0x24:
                    H = Inc(H); break;
                case 0x25:
                    H = Dec(H); break;
                case 0x26:
                    H = arg.lo; break;
                case 0x27:
                    Daa(); break;
                case 0x28: Jr((sbyte) arg.lo, GetZ()); break;
                case 0x29: Add(HL.wide); break;
                case 0x2A:
                    A = Gameboy.mmu[HL];
                    HL = Inc(HL);
                    break;
                case 0x2B:
                    HL = Dec(HL); break;
                case 0x2C:
                    L = Inc(L); break;
                case 0x2D:
                    L = Dec(L);  break;
                case 0x2E:
                    L = arg.lo; break;
                case 0x2F:
                    Cpl(); break;
                case 0x30: Jr((sbyte) arg.lo, !GetC());  break;
                case 0x31:
                    SP = arg.wide; break;
                case 0x32:
                    Gameboy.mmu[HL] = A;
                    HL = Dec(HL);
                    break;
                case 0x33:
                    SP = Inc(SP.wide);  break;
                case 0x34:
                    Gameboy.mmu[HL] = Inc(Gameboy.mmu[HL.wide]);   break;
                case 0x35:Gameboy.mmu[HL] = Dec(Gameboy.mmu[HL.wide]); break;
                case 0x36:
                    Gameboy.mmu[HL] = arg.lo;
                    break;
                case 0x37: Scf(); break;
                case 0x38: Jr((sbyte) arg.lo, GetC()); break;
                case 0x39:
                    Add(SP.wide);  break;
                case 0x3A:
                    A = Gameboy.mmu[HL];
                    HL = Dec(HL);
                    break;
                case 0x3B:
                    SP.wide = Dec(SP.wide); break;
                case 0x3C:
                    A = Inc(A); break;
                case 0x3D:
                    A = Dec(A); break;
                case 0x3E:
                    A = arg.lo;
                    break;
                case 0x3F: Ccf(); break;
                case 0x40:
                    B = B; break;
                case 0x41:
                    B = C; break;
                case 0x42:
                    B = D;  break;
                case 0x43:
                    B = E;  break;
                case 0x44:
                    B = H;  break;
                case 0x45:
                    B = L;  break;
                case 0x46:
                    B = Gameboy.mmu[HL];  break;
                case 0x47:
                    B = A;  break;
                case 0x48:
                    C = B;  break;
                case 0x49:
                    C = C;  break;
                case 0x4A:
                    C = D;  break;
                case 0x4B:
                    C = E;  break;
                case 0x4C:
                    C = H;  break;
                case 0x4D:
                    C = L;  break;
                case 0x4E:
                    C = Gameboy.mmu[HL];  break;
                case 0x4F:
                    C = A; break;
                case 0x50:
                    D = B;  break;
                case 0x51:
                    D = C;  break;
                case 0x52:
                    D = D;  break;
                case 0x53:
                    D = E;  break;
                case 0x54:
                    D = H;  break;
                case 0x55:
                    D = L;  break;
                case 0x56:
                    D = Gameboy.mmu[HL];  break;
                case 0x57:
                    D = A; break;
                case 0x58:
                    E = B;  break;
                case 0x59:
                    E = C;  break;
                case 0x5A:
                    E = D;  break;
                case 0x5B:
                    E = E;  break;
                case 0x5C:
                    E = H;  break;
                case 0x5D:
                    E = L;  break;
                case 0x5E:
                    E = Gameboy.mmu[HL];  break;
                case 0x5F:
                    E = A;  break;
                case 0x60:
                    H = B;  break;
                case 0x61:
                    H = C;  break;
                case 0x62:
                    H = D;  break;
                case 0x63:
                    H = E;  break;
                case 0x64:
                    H = H;  break;
                case 0x65:
                    H = L;  break;
                case 0x66:
                    H = Gameboy.mmu[HL];  break;
                case 0x67:
                    H = A;  break;
                case 0x68:
                    L = B;  break;
                case 0x69:
                    L = C;  break;
                case 0x6A:
                    L = D;  break;
                case 0x6B:
                    L = E;  break;
                case 0x6C:
                    L = H;  break;
                case 0x6D:
                    L = L;  break;
                case 0x6E:
                    L = Gameboy.mmu[HL];  break;
                case 0x6F:
                    H = A;  break;
                case 0x70:Gameboy.mmu[HL] = B;  break;
                case 0x71:Gameboy.mmu[HL] = C; break;
                case 0x72:Gameboy.mmu[HL] = D; break;
                case 0x73:Gameboy.mmu[HL] = E; break;
                case 0x74:Gameboy.mmu[HL] = H; break;
                case 0x75:Gameboy.mmu[HL] = L; break;
                case 0x76: Halt(); break;
                case 0x77:Gameboy.mmu[HL] = A; break;
                case 0x78:
                    A = B; break;
                case 0x79:
                    A = C;  break;
                case 0x7A:
                    A = D;  break;
                case 0x7B:
                    A = E;  break;
                case 0x7C:
                    A = H;  break;
                case 0x7D:
                    A = L;  break;
                case 0x7E:
                    A = Gameboy.mmu[HL];  break;
                case 0x7F:
                    A = A; break;
                case 0x80:Add(B);  break;
                case 0x81: Add(C); break;
                case 0x82:
                    Add(D); break;
                case 0x83: Add(E); break;
                case 0x84:
                    Add(H); break;
                case 0x85:
                    Add(L); break;
                case 0x86: Add(Gameboy.mmu[HL]); break;
                case 0x87: Add(A);  break;
                case 0x88: Add(B, true);  break;
                case 0x89: Add(C, true); break;
                case 0x8A: Add(D, true); break;
                case 0x8B:
                    Add(E, true);  break;
                case 0x8C: Add(H, true); break;
                case 0x8D: Add(L, true); break;
                case 0x8E: Add(Gameboy.mmu[HL], true); break;
                case 0x8F: Add(A, true); break;
                case 0x90: Sub(B); break;
                case 0x91:
                    Sub(C); break;
                case 0x92:
                    Sub(D); break;
                case 0x93:
                    Sub(E); break;
                case 0x94:
                    Sub(H); break;
                case 0x95:
                    Sub(L);break;
                case 0x96: Sub(Gameboy.mmu[HL]);  break;
                case 0x97: Sub(A);  break;
                case 0x98:
                    Sub(B, true); break;
                case 0x99:
                    Sub(C, true); break;
                case 0x9A:
                    Sub(D, true); break;
                case 0x9B:
                    Sub(E, true); break;
                case 0x9C:
                    Sub(H, true); break;
                case 0x9D: Sub(L, true); break;
                case 0x9E:
                    Sub(Gameboy.mmu[HL], true); break;
                case 0x9F:
                    Sub(A, true); break;
                case 0xA0: And(B); break;
                case 0xA1: And(C); break;
                case 0xA2:
                    And(D); break;
                case 0xA3: And(E); break;
                case 0xA4:
                    And(H); break;
                case 0xA5:
                    And(L); break;
                case 0xA6:
                    And(Gameboy.mmu[HL]); break;
                case 0xA7:
                    And(A);  break;
                case 0xA8: Xor(B); break;
                case 0xA9:
                    Xor(C); break;
                case 0xAA: Xor(D); break;
                case 0xAB:
                    Xor(E); break;
                case 0xAC:
                    Xor(H); break;
                case 0xAD:
                    Xor(L); break;
                case 0xAE:
                    Xor(Gameboy.mmu[HL]); break;
                case 0xAF:
                    Xor(A); break;
                case 0xB0: Or(B);  break;
                case 0xB1:
                    Or(C); break;
                case 0xB2:
                    Or(D);  break;
                case 0xB3:
                    Or(E);  break;
                case 0xB4:
                    Or(H);  break;
                case 0xB5:
                    Or(L);  break;
                case 0xB6:
                    Or(Gameboy.mmu[HL]);  break;
                case 0xB7:
                    Or(A); break;
                case 0xB8: Cp(B);  break;
                case 0xB9:
                    Cp(C); break;
                case 0xBA:
                    Cp(D);  break;
                case 0xBB:
                    Cp(E);  break;
                case 0xBC:Cp(H);  break;
                case 0xBD:
                    Cp(L); break;
                case 0xBE:
                    Cp(Gameboy.mmu[HL]);  break;
                case 0xBF:
                    Cp(A);  break;
                case 0xC0:Ret(!GetZ());  break;
                case 0xC1:
                    Pop(ref BC.wide); break;
                case 0xC2:Jp(arg.wide, !GetZ());  break;
                case 0xC3:
                    Jp(arg.wide);  break;
                case 0xC4:Call(arg.wide, !GetZ());  break;
                case 0xC5:
                    Push(BC.wide);  break;
                case 0xC6: Add(arg.lo); break;
                case 0xC7: Rst(0); break;
                case 0xC8: Ret(GetZ());  break;
                case 0xC9:
                    Ret(); break;
                case 0xCA:Jp(arg.wide, GetZ());  break;
                case 0xCB: CbPrefix(arg.lo);
                    break;
                case 0xCC: Call(arg.wide, GetZ()); break;
                case 0xCD:
                    Call(arg.wide); break;
                case 0xCE: Add(arg.lo, true); break;
                case 0xCF:
                    Rst(8); break;
                case 0xD0:
                    Ret(!GetC()); break;
                case 0xD1:
                    Pop(ref DE.wide); break;
                case 0xD2:
                    Jp(arg.wide, !GetC()); break;
                case 0xD4: Call(arg.wide, !GetC());  break;
                case 0xD5:
                    Push(DE.wide); break;
                case 0xD6:
                    Sub(arg.lo); break;
                case 0xD7:
                    Rst(16); break;
                case 0xD8:
                    Ret(GetC());  break;
                case 0xD9:
                    Ret();
                    Gameboy.interrupts.EnableInterrupts();
                    break;
                case 0xDA: Jp(arg.wide, GetC()); break;
                case 0xDC:
                    Call(arg.wide, GetC());  break;
                case 0xDE: Sub(arg.lo, true);  break;
                case 0xDF: Rst(24); break;
                case 0xE0:
                    Gameboy.mmu[(ushort) ( 0xFF00 + arg.lo )] = A;  break;
                case 0xE1:
                    Pop(ref HL.wide);  break;
                case 0xE2:
                    Gameboy.mmu[(ushort)(0xFF00 + C)] = A; break;
                case 0xE5:
                    Push(HL); break;
                case 0xE6:
                    And(arg.lo); break;
                case 0xE7:
                    Rst(32); break;
                case 0xE8: AddSP((sbyte) arg.lo);  break;
                case 0xE9:
                    Jp(Gameboy.mmu[HL]); break;
                case 0xEA:
                    Gameboy.mmu[arg.wide] = A; break;
                case 0xEE: Xor(arg.lo); break;
                case 0xEF:
                    Rst(40); break;
                case 0xF0:
                    A = Gameboy.mmu[(ushort) ( 0xFF00 + arg.lo )];  break;
                case 0xF1: Pop(ref AF.wide); break;
                case 0xF2:
                    A = Gameboy.mmu[(ushort)(0xFF00 + C)]; break;
                case 0xF3: Di(); break;
                case 0xF5:
                    Push(AF.wide); break;
                case 0xF6: Or(arg.lo); break;
                case 0xF7:
                    Rst(48); break;
                case 0xF8:
                    HL.wide = SP.wide; break;
                case 0xF9:
                    SP.wide = HL.wide; break;
                case 0xFA:
                    A = Gameboy.mmu[arg.wide]; break;
                case 0xFB:
                    Ei(); break;
                case 0xFE: Cp(arg.lo); break;
                case 0xFF:
                    Rst(56); break;

            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CbPrefix(byte op)
        {
            byte hl;
            switch(op)

            {
                case 0x00: Rlc(ref B);  break;
                case 0x01:
                    Rlc(ref C); break;
                case 0x02: Rlc(ref D); break;
                case 0x03: Rlc(ref E); break;
                case 0x04: Rlc(ref H); break;
                case 0x05: Rlc(ref L); break;
                case 0x06:
                    hl = Gameboy.mmu[HL]; Rlc(ref hl);
                    Gameboy.mmu[HL] = hl; break;
                case 0x07: Rlc(ref A);  break;
                case 0x08: Rrc(ref B); break;
                case 0x09: Rrc(ref C); break;
                case 0x0A: Rrc(ref D); break;
                case 0x0B: Rrc(ref E); break;
                case 0x0C: Rrc(ref H); break;
                case 0x0D: Rrc(ref L); break;
                case 0x0E:
                    hl = Gameboy.mmu[HL]; Rrc(ref hl);
                    Gameboy.mmu[HL] = hl; break;
                case 0x0F: Rrc(ref A); break;
                case 0x10: Rl(ref B); break;
                case 0x11: Rl(ref C); break;
                case 0x12: Rl(ref D); break;
                case 0x13: Rl(ref E); break;
                case 0x14: Rl(ref H); break;
                case 0x15: Rl(ref L); break;
                case 0x16:
                    hl = Gameboy.mmu[HL]; Rl(ref hl);
                    Gameboy.mmu[HL] = hl; break;
                case 0x17: Rl(ref A); break;
                case 0x18: Rr(ref B); break;
                case 0x19: Rr(ref C); break;
                case 0x1A: Rr(ref D); break;
                case 0x1B: Rr(ref E); break;
                case 0x1C: Rr(ref H); break;
                case 0x1D: Rr(ref L); break;
                case 0x1E:
                    hl              = Gameboy.mmu[HL]; Rr(ref hl);
                    Gameboy.mmu[HL] = hl; break;
                case 0x1F: Rr(ref A); break;
                case 0x20: Sla(ref B); break;
                case 0x21: Sla(ref C); break;
                case 0x22: Sla(ref D); break;
                case 0x23: Sla(ref E); break;
                case 0x24: Sla(ref H); break;
                case 0x25: Sla(ref L); break;
                case 0x26:
                    hl              = Gameboy.mmu[HL]; Sla(ref hl);
                    Gameboy.mmu[HL] = hl; break;
                case 0x27: Sla(ref A); break;
                case 0x28: Sra(ref B); break;
                case 0x29: Sra(ref C); break;
                case 0x2A: Sra(ref D); break;
                case 0x2B: Sra(ref E); break;
                case 0x2C: Sra(ref H); break;
                case 0x2D: Sra(ref L); break;
                case 0x2E:
                    hl              = Gameboy.mmu[HL]; Sra(ref hl);
                    Gameboy.mmu[HL] = hl; break;
                case 0x2F: Sra(ref A); break;
                case 0x30:
                    B = Swap(B); break;
                case 0x31:
                    C = Swap(C); break;
                case 0x32:
                    D = Swap(D); break;
                case 0x33:
                    E = Swap(E); break;
                case 0x34:
                    H = Swap(H); break;
                case 0x35:
                    L = Swap(L); break;
                case 0x36:
                    Gameboy.mmu[HL] = Swap(Gameboy.mmu[HL]); break;
                case 0x37:
                    A = Swap(A); break;
                case 0x38: Srl(ref B); break;
                case 0x39: Srl(ref C); break;
                case 0x3A: Srl(ref D);break;
                case 0x3B: Srl(ref E); break;
                case 0x3C: Srl(ref H); break;
                case 0x3D: Srl(ref L); break;
                case 0x3E:
                    hl              = Gameboy.mmu[HL]; Srl(ref hl);
                    Gameboy.mmu[HL] = hl; break;
                case 0x3F: Srl(ref A);  break;
                case 0x40: Bit(B, 0); break;
                case 0x41:Bit(C, 0); break;
                case 0x42:Bit(D, 0); break;
                case 0x43:Bit(E, 0); break;
                case 0x44:Bit(H, 0); break;
                case 0x45:Bit(L, 0); break;
                case 0x46:Bit(Gameboy.mmu[HL], 0); break;
                case 0x47:Bit(A, 0); break;
                case 0x48:Bit(B, 1); break;
                case 0x49:Bit(C, 1); break;
                case 0x4A:Bit(D, 1); break;
                case 0x4B:Bit(E, 1); break;
                case 0x4C:Bit(H, 1); break;
                case 0x4D:Bit(L, 1); break;
                case 0x4E:Bit(Gameboy.mmu[HL], 1); break;
                case 0x4F:Bit(A, 1); break;
                case 0x50:Bit(B, 2); break;
                case 0x51:Bit(C, 2); break;
                case 0x52:Bit(D, 2); break;
                case 0x53:Bit(E, 2); break;
                case 0x54:Bit(H, 2); break;
                case 0x55:Bit(L, 2); break;
                case 0x56:Bit(Gameboy.mmu[HL], 2); break;
                case 0x57:Bit(A, 2); break;
                case 0x58:Bit(B, 3); break;
                case 0x59:Bit(C, 3); break;
                case 0x5A:Bit(D, 3); break;
                case 0x5B:Bit(E, 3); break;
                case 0x5C:Bit(H, 3); break;
                case 0x5D:Bit(L, 3); break;
                case 0x5E:Bit(Gameboy.mmu[HL], 3); break;
                case 0x5F:Bit(A, 3); break;
                case 0x60:Bit(B, 4); break;
                case 0x61:Bit(C, 4); break;
                case 0x62:Bit(D, 4); break;
                case 0x63:Bit(E, 4); break;
                case 0x64:Bit(H, 4); break;
                case 0x65:Bit(L, 4); break;
                case 0x66:Bit(Gameboy.mmu[HL], 4); break;
                case 0x67:Bit(A, 4); break;
                case 0x68:Bit(B, 5); break;
                case 0x69:Bit(C, 5); break;
                case 0x6A:Bit(D, 5); break;
                case 0x6B:Bit(E, 5); break;
                case 0x6C:Bit(H, 5); break;
                case 0x6D:Bit(L, 5); break;
                case 0x6E:Bit(Gameboy.mmu[HL], 5); break;
                case 0x6F:Bit(A, 5); break;
                case 0x70:Bit(B, 6); break;
                case 0x71:Bit(C, 6); break;
                case 0x72:Bit(D, 6); break;
                case 0x73:Bit(E, 6); break;
                case 0x74:Bit(H, 6); break;
                case 0x75:Bit(L, 6); break;
                case 0x76:Bit(Gameboy.mmu[HL], 6); break;
                case 0x77:Bit(A, 6); break;
                case 0x78:Bit(B, 7); break;
                case 0x79:Bit(C, 7); break;
                case 0x7A:Bit(D, 7); break;
                case 0x7B:Bit(H, 7); break;
                case 0x7C:Bit(L, 7); break;
                case 0x7D:Bit(Gameboy.mmu[HL], 7); break;
                case 0x7E:Bit(A, 7); break;
                case 0x7F:  break;
                case 0x80:  break;
                case 0x81:  break;
                case 0x82:  break;
                case 0x83:  break;
                case 0x84:  break;
                case 0x85:  break;
                case 0x86:  break;
                case 0x87:  break;
                case 0x88:  break;
                case 0x89:  break;
                case 0x8A:  break;
                case 0x8B:  break;
                case 0x8C:  break;
                case 0x8D:  break;
                case 0x8E:  break;
                case 0x8F:  break;
                case 0x90:  break;
                case 0x91:  break;
                case 0x92:  break;
                case 0x93:  break;
                case 0x94:  break;
                case 0x95:  break;
                case 0x96:  break;
                case 0x97:  break;
                case 0x98:  break;
                case 0x99:  break;
                case 0x9A:  break;
                case 0x9B:  break;
                case 0x9C:  break;
                case 0x9D:  break;
                case 0x9E:  break;
                case 0x9F:  break;
                case 0xA0:  break;
                case 0xA1:  break;
                case 0xA2:  break;
                case 0xA3:  break;
                case 0xA4:  break;
                case 0xA5:  break;
                case 0xA6:  break;
                case 0xA7:  break;
                case 0xA8:  break;
                case 0xA9:  break;
                case 0xAA:  break;
                case 0xAB:  break;
                case 0xAC:  break;
                case 0xAD:  break;
                case 0xAE:  break;
                case 0xAF:  break;
                case 0xB0:  break;
                case 0xB1:  break;
                case 0xB2:  break;
                case 0xB3:  break;
                case 0xB4:  break;
                case 0xB5:  break;
                case 0xB6:  break;
                case 0xB7:  break;
                case 0xB8:  break;
                case 0xB9:  break;
                case 0xBA:  break;
                case 0xBB:  break;
                case 0xBC:  break;
                case 0xBD:  break;
                case 0xBE:  break;
                case 0xBF:  break;
                case 0xC0:  break;
                case 0xC1:  break;
                case 0xC2:  break;
                case 0xC3:  break;
                case 0xC4:  break;
                case 0xC5:  break;
                case 0xC6:  break;
                case 0xC7:  break;
                case 0xC8:  break;
                case 0xC9:  break;
                case 0xCA:  break;
                case 0xCB:  break;
                case 0xCC:  break;
                case 0xCD:  break;
                case 0xCE:  break;
                case 0xCF:  break;
                case 0xD0:  break;
                case 0xD1:  break;
                case 0xD2:  break;
                case 0xD3:  break;
                case 0xD4:  break;
                case 0xD5:  break;
                case 0xD6:  break;
                case 0xD7:  break;
                case 0xD8:  break;
                case 0xD9:  break;
                case 0xDA:  break;
                case 0xDB:  break;
                case 0xDC:  break;
                case 0xDD:  break;
                case 0xDE:  break;
                case 0xDF:  break;
                case 0xE0:  break;
                case 0xE1:  break;
                case 0xE2:  break;
                case 0xE3:  break;
                case 0xE4:  break;
                case 0xE5:  break;
                case 0xE6:  break;
                case 0xE7:  break;
                case 0xE8:  break;
                case 0xE9:  break;
                case 0xEA:  break;
                case 0xEB:  break;
                case 0xEC:  break;
                case 0xED:  break;
                case 0xEE:  break;
                case 0xEF:  break;
                case 0xF0:  break;
                case 0xF1:  break;
                case 0xF2:  break;
                case 0xF3:  break;
                case 0xF4:  break;
                case 0xF5:  break;
                case 0xF6:  break;
                case 0xF7:  break;
                case 0xF8:  break;
                case 0xF9:  break;
                case 0xFA:  break;
                case 0xFB:  break;
                case 0xFC:  break;
                case 0xFD:  break;
                case 0xFE:  break;
                case 0xFF:  break;

            }
        }

    }


}
