# Kurui

This is an abandoned attempt to write a Gameboy Emulator in C#.

Lacks audio/video.

Most of the main processing logic for the CPU is present, although several implementation strategies could be improved:

1. Parsing opcodes as structs/objects from the ROM bank, rather than processing raw bytes with a prodigious switch statement
2. Eliminating the `Imm` wrapper for data, and dealing with these variants via the parsed objects as mentioned in #1
3. Better re-creation of rom tests in C# to confirm functionality before integrating PPU/SPU

I would like to revisit this idea with a fresh slate in the future.
