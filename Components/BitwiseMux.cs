using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A bitwise gate takes as input WireSets containing n wires, and computes a bitwise function - z_i=f(x_i)
    class BitwiseMux : BitwiseTwoInputGate
    {
        public Wire ControlInput { get; private set; }

        //your code here
        private MuxGate[] muxGates;

        public BitwiseMux(int iSize)
            : base(iSize)
        {
            ControlInput = new Wire();
            //your code here
            muxGates = new MuxGate[iSize];
            for (int i = 0; i < muxGates.Length; i++)
                muxGates[i] = new MuxGate();

            for (int i = 0; i < muxGates.Length; i++)
                muxGates[i].ConnectControl(ControlInput);

            for (int i = 0; i < iSize; i++)
            {
                muxGates[i].ConnectInput1(this.Input1[i]);
                muxGates[i].ConnectInput2(this.Input2[i]);
            }

            for (int i = 0; i < iSize; i++)
            {
                Output[i].ConnectInput(muxGates[i].Output);
            }
        }

        public void ConnectControl(Wire wControl)
        {
            ControlInput.ConnectInput(wControl);
        }



        public override string ToString()
        {
            return "Mux " + Input1 + "," + Input2 + ",C" + ControlInput.Value + " -> " + Output;
        }




        public override bool TestGate()
        {
            ControlInput.Value = 0;
            if (Output[0].Value != Input1[0].Value)
                return false;
            ControlInput.Value = 1;
            if (Output[0].Value != Input2[0].Value)
                return false;
            return true;
        }
    }
}
