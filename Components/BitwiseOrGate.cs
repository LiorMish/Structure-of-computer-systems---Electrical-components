using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A two input bitwise gate takes as input two WireSets containing n wires, and computes a bitwise function - z_i=f(x_i,y_i)
    class BitwiseOrGate : BitwiseTwoInputGate
    {
        //your code here
        private OrGate[] orGates;

        public BitwiseOrGate(int iSize)
            : base(iSize)
        {
            //your code here
            orGates = new OrGate[iSize];
            for (int i = 0; i < orGates.Length; i++)
                orGates[i] = new OrGate();

            for (int i = 0; i < iSize; i++)
            {
                orGates[i].ConnectInput1(this.Input1[i]);
                orGates[i].ConnectInput2(this.Input2[i]);
            }

            for (int i = 0; i < iSize; i++)
            {
                Output[i].ConnectInput(orGates[i].Output);
            }
        }

        //an implementation of the ToString method is called, e.g. when we use Console.WriteLine(or)
        //this is very helpful during debugging
        public override string ToString()
        {
            return "Or " + Input1 + ", " + Input2 + " -> " + Output;
        }

        public override bool TestGate()
        {
            Input1[0].Value = 0;
            Input2[0].Value = 0;
            if (Output[0].Value != 0)
                return false;
            Input1[0].Value = 0;
            Input2[0].Value = 1;
            if (Output[0].Value != 1)
                return false;
            Input1[0].Value = 1;
            Input2[0].Value = 0;
            if (Output[0].Value != 1)
                return false;
            Input1[0].Value = 1;
            Input2[0].Value = 1;
            if (Output[0].Value != 1)
                return false;
            return true;
        }
    }
}
