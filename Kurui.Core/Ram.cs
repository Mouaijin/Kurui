using System;

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
            set { }
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
        internal bool enabled = false;
        private byte bankIndex = 0;
        private byte[] bytes = new byte[0x8000];

        public Imm this[int index]
        {
            get => enabled ? bytes.ReadImm(0x2000 * bankIndex + index) : new Imm {wide = 0};
            set
            {
                if (enabled)
                {
                    bytes[0x2000 * bankIndex + index] = value;
                }
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

    internal class RTC
    {
        internal bool received0 = false;
        internal bool latched = false;
        internal DateTime latchData = DateTime.Now;
        internal DateTime dayCounterOffset = DateTime.Today;
        private DateTime GetDate() => latched ? latchData : DateTime.Now;

        public void Latch(int num)
        {
            if (!received0 && num == 0)
            {
                received0 = true;
            }

            else if (!latched && received0 && num == 1)
            {
                latched = true;
                received0 = false;
                latchData = DateTime.Now;
            }

            else if (latched && received0 && num == 1)
            {
                latched = false;
                received0 = false;
            }
            else if (received0 && num > 1)
            {
                received0 = false;
            }
        }

        public byte Seconds => (byte) GetDate().Second;
        public byte Minutes => (byte) GetDate().Minute;
        public byte Hours => (byte) GetDate().Hour;
        public byte Days => (byte) ( GetDate() - dayCounterOffset ).Days;
        public byte DaySignificantDigit => (byte) ( ( DateTime.Today - dayCounterOffset ).Days >> 7 & 1 );
        public byte DaysOverflow => (byte) ( ( DateTime.Today - dayCounterOffset ).Days >> 8 & 1 );
        public void ClearCountOffset() => dayCounterOffset = DateTime.Today;
        public byte Register => (byte) ( DaysOverflow << 7 | Convert.ToByte(latched) << 6 | DaySignificantDigit );
    }

    internal class Ram32kTimer : IRam
    {
        internal bool enabled = false;
        private byte bankIndex = 0;
        private RTC rtc = new RTC();
        private byte[] bytes = new byte[0x8000];

        public Imm this[int index]
        {
            get
            {
                if (enabled && bankIndex < 5)
                    return bytes.ReadImm(0x2000 * bankIndex + index);

                if (enabled && bankIndex > 5)
                {
                    switch (bankIndex)
                    {
                        case 0x08: return rtc.Seconds;
                        case 0x09: return rtc.Minutes;
                        case 0x0A: return rtc.Hours;
                        case 0x0B: return rtc.Days;
                        case 0x0C: return rtc.Register;
                    }
                }

                return 0;
            }

            set
            {
                if (enabled)
                {
                    bytes[0x2000 * bankIndex + index] = value;
                }
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

        public void Latch(int num) => rtc.Latch(num);
    }
}