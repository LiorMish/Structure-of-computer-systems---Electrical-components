using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This gate implements the or operation. To implement it, follow the example in the And gate.
    class OrGate : TwoInputGate
    {
        //your code here 
        private NotGate notX;
        private NotGate notY;
        private NotGate not;
        private AndGate and;

        public OrGate()
        {
            //your code here 
            //init the gates
            and = new AndGate();
            notX = new NotGate();
            notY = new NotGate();
            not = new NotGate();


            //set the output of the not gate 
            Output.ConnectInput(not.Output);
            //set the inputs of the not gates
            notX.ConnectInput(Input1);
            notY.ConnectInput(Input2);
            //set the inputs and the output of the and gate
            and.ConnectInput1(notX.Output);
            and.ConnectInput2(notY.Output);
            not.ConnectInput(and.Output);

        }


        public override string ToString()
        {
            return "Or " + Input1.Value + "," + Input2.Value + " -> " + Output.Value;
        }

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
            if (Output.Value != 1)
                return false;
            return true;
        }
    }

}
