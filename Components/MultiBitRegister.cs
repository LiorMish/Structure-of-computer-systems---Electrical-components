using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class represents an n bit register that can maintain an n bit number
    class MultiBitRegister : Gate
    {
        public WireSet Input { get; private set; }
        public WireSet Output { get; private set; }
        //A bit setting the register operation to read or write
        public Wire Load { get; private set; }

        //Word size - number of bits in the register
        public int Size { get; private set; }

        private SingleBitRegister [] nBitRegister;

        public MultiBitRegister(int iSize)
        {
            Size = iSize;
            Input = new WireSet(Size);
            Output = new WireSet(Size);
            Load = new Wire();
            //your code here
            nBitRegister = new SingleBitRegister[Size];

            for (int i = 0; i < Size; i++)
            {
                nBitRegister[i] = new SingleBitRegister();
                nBitRegister[i].ConnectInput(Input[i]);
                nBitRegister[i].ConnectLoad(Load);
                Output[i].ConnectInput(nBitRegister[i].Output);
            }

        }

        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }

        
        public override string ToString()
        {
            return Output.ToString();
        }


        public override bool TestGate()
        {
            Input.SetValue(1);
            Load.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            if (this.Output.GetValue() != 1)
            {
                return false;
            }
            Input.SetValue(0);
            Load.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            if (this.Output.GetValue() != 0)
            {
                return false;
            }
            Load.Value = 0;
            Clock.ClockDown();
            Clock.ClockUp();
            if (this.Output.GetValue() != 0)
            {
                return false;
            }
            return true;
        }
    }
}
