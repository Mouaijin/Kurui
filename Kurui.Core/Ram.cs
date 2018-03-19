namespace Kurui.Core
{
    internal interface IRam
    {
        Imm this[int index] { get; set; }
        void Enable();
        void Disable();
        void SwapBank(byte index);
    }

    internal class NoRam : IRam
    {
        public Imm this[int index]
        {
            get => 0;
            set
            {
            }
        }

        public void Enable()
        {
        }

        public void Disable()
        {
        }

        public void SwapBank(byte index)
        {
        }
    }

    internal class Ram32k : IRam
    {
        private bool enabled = false;
        private byte bankIndex = 0;
        private byte[] bytes = new byte[0x8000];

        public Imm this[int index]
        {
            get => bytes.ReadImm(0x2000 * bankIndex + index);
            set{
                //tofo
            }
        }

        public void Enable()
        {
            enabled = true;
        }

        public void Disable()
        {
            enabled = false;
        }

        public void SwapBank(byte index)
        {
            bankIndex = index;
        }
    }

}
