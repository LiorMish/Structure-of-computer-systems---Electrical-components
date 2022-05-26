using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This bitwise gate takes as input one WireSet containing n wires, and computes a bitwise function - z_i=f(x_i)
    class BitwiseNotGate : Gate
    {
        public WireSet Input { get; private set; }
        public WireSet Output { get; private set; }
        public int Size { get; private set; }

        //your code here
        private NotGate[] notGates;

        public BitwiseNotGate(int iSize)
        {
            Size = iSize;
            Input = new WireSet(Size);
            Output = new WireSet(Size);
            //your code here
            notGates = new NotGate[Size];

            for (int i = 0; i < notGates.Length; i++)
                notGates[i] = new NotGate();

            for (int i = 0; i < Size; i++)
                notGates[i].ConnectInput(this.Input[i]);
              
            for (int i = 0; i < Size; i++)
            {
                Output[i].ConnectInput(notGates[i].Output);
            }


        }

        public void ConnectInput(WireSet ws)
        {
            Input.ConnectInput(ws);
        }

        //an implementation of the ToString method is called, e.g. when we use Console.WriteLine(not)
        //this is very helpful during debugging
        public override string ToString()
        {
            return "Not " + Input + " -> " + Output;
        }

        public override bool TestGate()
        {
            Input[0].Value = 0;
            Input[1].Value = 0;
            if (Output[0].Value != 1 && Output[1].Value != 1)
                return false;
            Input[0].Value = 0;
            Input[1].Value = 1;
            if (Output[0].Value != 1 && Output[1].Value != 0)
                return false;
            Input[0].Value = 1;
            Input[1].Value = 0;
            if (Output[0].Value != 0 && Output[1].Value != 1)
                return false;
            Input[0].Value = 1;
            Input[1].Value = 1;
            if (Output[0].Value != 0 && Output[1].Value != 0)
                return false;
            return true;
        }
    }
}
