using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a FullAdder, taking as input 3 bits - 2 numbers and a carry, and computing the result in the output, and the carry out.
    class FullAdder : TwoInputGate
    {
        public Wire CarryInput { get; private set; }
        public Wire CarryOutput { get; private set; }

        //your code here
        private HalfAdder half1;
        private HalfAdder half2;
        private OrGate orCarryOut;

        public FullAdder()
        {
            CarryInput = new Wire();
            //your code here
            CarryOutput = new Wire();
 
            half1 = new HalfAdder();
            half2 = new HalfAdder();
            orCarryOut = new OrGate();

            this.half1.ConnectInput1(Input1);
            this.half1.ConnectInput2(Input2);
            this.half2.Input1.ConnectInput(half1.Output);
            this.half2.Input2.ConnectInput(CarryInput);
            this.Output.ConnectInput(half2.Output);
            this.orCarryOut.Input1.ConnectInput(half1.CarryOutput);
            this.orCarryOut.Input2.ConnectInput(half2.CarryOutput);
            this.CarryOutput.ConnectInput(orCarryOut.Output);

        }


        public override string ToString()
        {
            return Input1.Value + "+" + Input2.Value + " (C" + CarryInput.Value + ") = " + Output.Value + " (C" + CarryOutput.Value + ")";
        }

        public override bool TestGate()
        {
            this.Input1.Value = 0;
            this.Input2.Value = 0;
            this.CarryInput.Value = 0;
            if (this.Output.Value != 0 || this.CarryOutput.Value != 0)
                return false;

            this.Input1.Value = 0;
            this.Input2.Value = 0;
            this.CarryInput.Value = 1;
            if (this.Output.Value != 1 || this.CarryOutput.Value != 0)
                return false;

            this.Input1.Value = 0;
            this.Input2.Value = 1;
            this.CarryInput.Value = 0;
            if (this.Output.Value != 1 || this.CarryOutput.Value != 0)
                return false;

            this.Input1.Value = 0;
            this.Input2.Value = 1;
            this.CarryInput.Value = 1;
            if (this.Output.Value != 0 || this.CarryOutput.Value != 1)
                return false;

            this.Input1.Value = 1;
            this.Input2.Value = 0;
            this.CarryInput.Value = 0;
            if (this.Output.Value != 1 || this.CarryOutput.Value != 0)
                return false;

            this.Input1.Value = 1;
            this.Input2.Value = 0;
            this.CarryInput.Value = 1;
            if (this.Output.Value != 0 || this.CarryOutput.Value != 1)
                return false;

            this.Input1.Value = 1;
            this.Input2.Value = 1;
            this.CarryInput.Value = 0;
            if (this.Output.Value != 0 || this.CarryOutput.Value != 1)
                return false;

            this.Input1.Value = 1;
            this.Input2.Value = 1;
            this.CarryInput.Value = 1;
            if (this.Output.Value != 1 || this.CarryOutput.Value != 1)
                return false;

            return true;
        }
    }
}
