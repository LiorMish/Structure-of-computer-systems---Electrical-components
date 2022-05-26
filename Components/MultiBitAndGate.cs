using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //Multibit gates take as input k bits, and compute a function over all bits - z=f(x_0,x_1,...,x_k)
    class MultiBitAndGate : MultiBitGate
    {
        //your code here
        private AndGate[] andGates;
        public MultiBitAndGate(int iInputCount)
            : base(iInputCount)
        {
            //your code here
            andGates = new AndGate[iInputCount - 1];
            for (int i = 0; i < andGates.Length; i++)
                andGates[i] = new AndGate();


            andGates[0].ConnectInput1(this.m_wsInput[0]);
            andGates[0].ConnectInput2(this.m_wsInput[1]);

            for (int i=1; i<iInputCount-1; i++)
            {
                andGates[i].ConnectInput1(andGates[i-1].Output);
                andGates[i].ConnectInput2(this.m_wsInput[i+1]);
            }
            Output = andGates[iInputCount - 2].Output;

        }


        public override bool TestGate()
        {
            for (int i = 0; i < this.m_wsInput.Size; i++)
                this.m_wsInput[i].Value = 1;
            if (Output.Value != 1)
                return false;
            for (int i = 0; i < this.m_wsInput.Size; i++)
                this.m_wsInput[i].Value = 0;
            if (Output.Value != 0)
                return false;
            return true;
        }
    }
}
