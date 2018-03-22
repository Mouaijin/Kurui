using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurui.Core
{
    class Joypad
    {
        private bool buttonMode = true; //false = direction mode

        public bool DownPressed { get; set; }
        public bool UpPressed { get; set; }
        public bool LeftPressed { get; set; }
        public bool RightPressed { get; set; }

        public bool SelectPressed { get; set; }
        public bool StartPressed { get; set; }
        public bool APressed { get; set; }
        public bool BPressed { get; set; }

        public void SetButtonMode() => buttonMode = true;
        public void SetDirectionMode() => buttonMode = false;

        public byte JoypadRegister => buttonMode ? Concat(APressed, BPressed, SelectPressed, StartPressed) : Concat(RightPressed, LeftPressed, UpPressed, DownPressed);
        private byte Concat(bool a, bool b, bool c, bool d)
        {
            byte ab =(byte) (a ? 1 : 0);
            byte bb = (byte) ( b ? 2: 0 );
            byte cb = (byte) ( c ? 4 : 0 );
            byte db = (byte) ( d ? 8 : 0 );
            return (byte) ( ab | bb | cb | db );
        }

    }
    //Bit 7 - Not used
    //Bit 6 - Not used
    //Bit 5 - P15 Select Button Keys(0=Select)
    //Bit 4 - P14 Select Direction Keys(0=Select)
    //Bit 3 - P13 Input Down or Start(0=Pressed) (Read Only)
    //Bit 2 - P12 Input Up or Select(0=Pressed) (Read Only)
    //Bit 1 - P11 Input Left or Button B(0=Pressed) (Read Only)
    //Bit 0 - P10 Input Right or Button A(0=Pressed) (Read Only)
}
