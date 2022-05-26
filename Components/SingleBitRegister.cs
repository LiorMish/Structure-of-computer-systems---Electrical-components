using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a register that can maintain 1 bit.
    class SingleBitRegister : Gate
    {
        public Wire Input { get; private set; }
        public Wire Output { get; private set; }
        //A bit setting the register operation to read or write
        public Wire Load { get; private set; }

        private MuxGate muxGate;
        private DFlipFlopGate flipFlopGate;

        public SingleBitRegister()
        {
            
            Input = new Wire();
            Load = new Wire();
            //your code here 
            Output = new Wire();
            muxGate = new MuxGate();
            flipFlopGate = new DFlipFlopGate();

            flipFlopGate.ConnectInput(muxGate.Output);

            muxGate.ConnectInput1(flipFlopGate.Output);
            muxGate.ConnectInput2(Input);
            muxGate.ConnectControl(Load);

            Output.ConnectInput(flipFlopGate.Output);
        }

        public void ConnectInput(Wire wInput)
        {
            Input.ConnectInput(wInput);
        }

      

        public void ConnectLoad(Wire wLoad)
        {
            Load.ConnectInput(wLoad);
        }


        public override bool TestGate()
        {
            Load.Value = 1;
            Input.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            Load.Value = 0;
            Input.Value = 0;
            if (this.Output.Value != 1)
            {
                return false;
            }
            Clock.ClockDown();
            Clock.ClockUp();
            if (this.Output.Value != 1)
            {
                return false;
            }
            Load.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            if (this.Output.Value != 0)
            {
                return false;
            }
            Input.Value = 1;
            if (this.Output.Value != 0)
            {
                return false;
            }
            Clock.ClockDown();
            Clock.ClockUp();
            if (this.Output.Value != 1)
            {
                return false;
            }
            return true;
        }
    }
}
