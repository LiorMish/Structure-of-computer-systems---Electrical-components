using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //Multibit gates take as input k bits, and compute a function over all bits - z=f(x_0,x_1,...,x_k)

    class MultiBitOrGate : MultiBitGate
    {
        //your code here
        private OrGate[] orGates;

        public MultiBitOrGate(int iInputCount)
            : base(iInputCount)
        {
            //your code here
            orGates = new OrGate[iInputCount - 1];
            for (int i = 0; i < orGates.Length; i++)
                orGates[i] = new OrGate();

            orGates[0].ConnectInput1(this.m_wsInput[0]);
            orGates[0].ConnectInput2(this.m_wsInput[1]);

            for (int i = 1; i < iInputCount - 1; i++)
            {
                orGates[i].ConnectInput1(orGates[i - 1].Output);
                orGates[i].ConnectInput2(this.m_wsInput[i + 1]);
            }
            Output = orGates[iInputCount - 2].Output;


        }

        public override bool TestGate()
        {
            for (int i = 0; i < this.m_wsInput.Size; i++)
                this.m_wsInput[i].Value = 0;

            if (Output.Value != 0)
                return false;

            for (int i = 0; i < this.m_wsInput.Size; i++)
                this.m_wsInput[i].Value = 1;

            if (Output.Value != 1)
                return false;
            return true;
        }
    }
}
