using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a HalfAdder, taking as input 2 bits - 2 numbers and computing the result in the output, and the carry out.

    class HalfAdder : TwoInputGate
    {
        public Wire CarryOutput { get; private set; }

        //your code here
        private AndGate andCarry;
        private XorGate xorSum;


        public HalfAdder()
        {
            //your code here
            andCarry = new AndGate();
            xorSum = new XorGate();
            CarryOutput = new Wire();

            this.andCarry.ConnectInput1(this.Input1);
            this.andCarry.ConnectInput2(this.Input2);
            
            this.xorSum.ConnectInput1(Input1);
            this.xorSum.ConnectInput2(Input2);
            this.Output.ConnectInput(xorSum.Output);
            this.CarryOutput.ConnectInput(andCarry.Output);
        }


        public override string ToString()
        {
            return "HA " + Input1.Value + "," + Input2.Value + " -> " + Output.Value + " (C" + CarryOutput + ")";
        }

        public override bool TestGate()
        {
            this.Input1.Value = 0;
            this.Input2.Value = 0;
            if (this.Output.Value != 0 || this.CarryOutput.Value != 0)
                return false;

            this.Input1.Value = 0;
            this.Input2.Value = 1;
            if (this.Output.Value != 1 || this.CarryOutput.Value != 0)
                return false;

            this.Input1.Value = 1;
            this.Input2.Value = 0;
            if (this.Output.Value != 1 || this.CarryOutput.Value != 0)
                return false;

            this.Input1.Value = 1;
            this.Input2.Value = 1;
            if (this.Output.Value != 0 || this.CarryOutput.Value != 1)
                return false;

            return true;
        }
    }
}
