using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements an adder, receving as input two n bit numbers, and outputing the sum of the two numbers
    class MultiBitAdder : Gate
    {
        //Word size - number of bits in each input
        public int Size { get; private set; }

        public WireSet Input1 { get; private set; }
        public WireSet Input2 { get; private set; }
        public WireSet Output { get; private set; }
        //An overflow bit for the summation computation
        public Wire Overflow { get; private set; }

        //your code here
        private FullAdder[] fullAdders;


        public MultiBitAdder(int iSize)
        {
            Size = iSize;
            Input1 = new WireSet(Size);
            Input2 = new WireSet(Size);
            Output = new WireSet(Size);
            //your code here
            fullAdders = new FullAdder[Size];
            Overflow = new Wire();
            for (int i = 0; i < Size; i++)
                fullAdders[i] = new FullAdder();

            fullAdders[0].CarryInput.ConnectInput(new Wire());
            fullAdders[0].ConnectInput1(Input1[0]);
            fullAdders[0].ConnectInput2(Input2[0]);
            Output[0].ConnectInput(fullAdders[0].Output);

            for (int i = 1; i < Size; i++)
            {
                fullAdders[i].CarryInput.ConnectInput(fullAdders[i - 1].CarryOutput);
                fullAdders[i].ConnectInput1(Input1[i]);
                fullAdders[i].ConnectInput2(Input2[i]);
                Output[i].ConnectInput(fullAdders[i].Output);
            }

             Overflow = fullAdders[Size - 1].CarryOutput;

        }

        public override string ToString()
        {
            // return Input1 + "(" + Input1.Get2sComplement() + ")" + " + " + Input2 + "(" + Input2.Get2sComplement() + ")" + " = " + Output + "(" + Output.Get2sComplement() + ")";
            return Input1 + "(" + Input1.GetValue() + ")" + " + " + Input2 + "(" + Input2.GetValue() + ")" + " = " + Output + "(" + Output.GetValue() + ")";

        }

        public void ConnectInput1(WireSet wInput)
        {
            Input1.ConnectInput(wInput);
        }
        public void ConnectInput2(WireSet wInput)
        {
            Input2.ConnectInput(wInput);
        }


        public override bool TestGate()
        {
            Input1.SetValue(1);
            Input2.SetValue(1);

            for (int i = 0; i < Size; i++)
            {
                if (i == 1)
                {
                    if (Output[i].Value != 1)
                    {
                        return false;
                    }
                }
                else if (Output[i].Value != 0)
                {
                    return false;
                }
            }


            Input1.SetValue(0);
            Input2.SetValue(0);

            for (int i = 0; i < Size; i++)
            {
                if (Output[i].Value == 1)
                {
                    return false;
                }
            }


            Input1.SetValue(1);
            Input2.SetValue(0);

            for (int i = 0; i < Size; i++)
            {
                if (i == 0)
                {
                    if (Output[i].Value != 1)
                    {
                        return false;
                    }
                }
                else if (Output[i].Value != 0)
                {
                    return false;
                }
            }


            Input1.SetValue(0);
            Input2.SetValue(1);

            for (int i = 0; i < Size; i++)
            {
                if (i == 0)
                {
                    if (Output[i].Value != 1)
                    {
                        return false;
                    }
                }
                else if (Output[i].Value != 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
