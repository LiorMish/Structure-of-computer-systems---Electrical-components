using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This gate implements the xor operation. To implement it, follow the example in the And gate.
    class XorGate : TwoInputGate
    {
        //your code here
        private NotGate notX;
        private NotGate notY;
        private AndGate and1;
        private AndGate and2;
        private OrGate or;


        public XorGate()
        {
            //your code here
            //init the gates
            and1 = new AndGate();
            and2 = new AndGate();
            notX = new NotGate();
            notY = new NotGate();
            or = new OrGate();


            //set the inputs of the not gates
            notX.ConnectInput(Input1);
            notY.ConnectInput(Input2);
            //set the inputs and the output of the and gates
            and1.ConnectInput1(notX.Output);
            and1.ConnectInput2(Input2);
            and2.ConnectInput1(notY.Output);
            and2.ConnectInput2(Input1);
            //set the inputs and the output of thr or gate
            or.ConnectInput1(and1.Output);
            or.ConnectInput2(and2.Output);
            Output = or.Output;
        }

        //an implementation of the ToString method is called, e.g. when we use Console.WriteLine(xor)
        //this is very helpful during debugging
        public override string ToString()
        {
            return "Xor " + Input1.Value + "," + Input2.Value + " -> " + Output.Value;
        }


        //this method is used to test the gate. 
        //we simply check whether the truth table is properly implemented.
        public override bool TestGate()
        {
            Input1.Value = 0;
            Input2.Value = 0;
            if (Output.Value != 0)
                return false;
            Input1.Value = 0;
            Input2.Value = 1;
            if (Output.Value != 1)
                return false;
            Input1.Value = 1;
            Input2.Value = 0;
            if (Output.Value != 1)
                return false;
            Input1.Value = 1;
            Input2.Value = 1;
            if (Output.Value != 0)
                return false;
            return true;
        }
    }
}
