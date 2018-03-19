using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Kurui.Tests")]
namespace Kurui.Core
{
    internal static class Utility
    {
        public static byte SetBit(this byte num, byte index)
        {
            return (byte) (num | (  1 << index ));
        }
        public static ushort SetBit(this ushort num, byte index)
        {
            return (ushort)(num | (1 << index));
        }

        public static byte ClearBit(this byte num, byte index)
        {
            return (byte) ( num & ~( 1 << index ) );
        }
        public static ushort ClearBit(this ushort num, byte index)
        {
            return (ushort)(num & ~(1 << index));
        }

        public static bool BitIsSet(this byte num, byte index) => ( ( num >> index ) & 1 ) == 1;
        public static bool BitIsSet(this ushort num, byte index) => ( ( num >> index ) & 1 ) == 1;

        public static Imm ReadImm(this byte[] bytes, int index)
        {
            if (index == bytes.Length - 1)
            {
                return bytes[index];
            }
            else
            {
                return new Imm{lo = bytes[index], hi = bytes[index + 1]};
            }
        }
    }
}
