namespace Kurui.Core
{
    internal class Interrupts
    {
        private InterruptRegister enable = 0, request = 0;
        private bool              ime    = false;

        public byte EnableRegister
        {
            get => enable;
            set => enable = value;
        }

        public byte RequestRegister
        {
            get => (byte) ( InterruptsEnabled ? request : 0 );
            set
            {
                if (InterruptsEnabled)
                    request = value;
            }
        }

        public void EnableInterrupts()
        {
            ime = true;
        }

        public void DisableInterrupts()
        {
            ime = false;
        }

        public bool InterruptsEnabled => ime;

        public bool VBlankRequest
        {
            get => InterruptsEnabled && request.VBlank;
            set
            {
                if (InterruptsEnabled) request.VBlank = value;
            }
        }

        public bool LCDRequest
        {
            get => InterruptsEnabled && request.LCD;
            set
            {
                if (InterruptsEnabled) request.LCD = value;
            }
        }

        public bool TimerRequest
        {
            get => InterruptsEnabled && request.Timer;
            set
            {
                if (InterruptsEnabled) request.Timer = value;
            }
        }

        public bool SerialRequest
        {
            get => InterruptsEnabled && request.Serial;
            set
            {
                if (InterruptsEnabled) request.Serial = value;
            }
        }

        public bool JoypadRequest
        {
            get => InterruptsEnabled && request.Joypad;
            set
            {
                if (InterruptsEnabled) request.Joypad = value;
            }
        }

        private struct InterruptRegister
        {
            private byte reg;

            private InterruptRegister(byte reg)
            {
                this.reg = reg;
            }

            public static implicit operator byte(InterruptRegister r) => r.reg;
            public static implicit operator InterruptRegister(byte b) => new InterruptRegister(b);

            public bool VBlank
            {
                get => ( reg & 1 ) == 1;
                set => reg = value ? reg.SetBit(0) : reg.ClearBit(0);
            }

            public bool LCD
            {
                get => ( reg >> 1 & 1 ) == 1;
                set => reg = value ? reg.SetBit(1) : reg.ClearBit(1);
            }

            public bool Timer
            {
                get => ( reg >> 2 & 1 ) == 1;
                set => reg = value ? reg.SetBit(2) : reg.ClearBit(2);
            }

            public bool Serial
            {
                get => ( reg >> 3 & 1 ) == 1;
                set => reg = value ? reg.SetBit(3) : reg.ClearBit(3);
            }

            public bool Joypad
            {
                get => ( reg >> 4 & 1 ) == 1;
                set => reg = value ? reg.SetBit(4) : reg.ClearBit(4);
            }

            public void Enable()  => reg = 255;
            public void Disable() => reg = 0;
        }
    }
}