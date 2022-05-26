using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class represents a set of n wires (a cable)
    class WireSet
    {
        //Word size - number of bits in the register
        public int Size { get; private set; }
        
        public bool InputConected { get; private set; }

        //An indexer providing access to a single wire in the wireset
        public Wire this[int i]
        {
            get
            {
                return m_aWires[i];
            }
        }
        private Wire[] m_aWires;
        
        public WireSet(int iSize)
        {
            Size = iSize;
            InputConected = false;
            m_aWires = new Wire[iSize];
            for (int i = 0; i < m_aWires.Length; i++)
                m_aWires[i] = new Wire();
        }
        public override string ToString()
        {
            string s = "[";
            for (int i = m_aWires.Length - 1; i >= 0; i--)
                s += m_aWires[i].Value;
            s += "]";
            return s;
        }

        //Transform a positive integer value into binary and set the wires accordingly, with 0 being the LSB
        public void SetValue(int iValue)
        {
            int i = 0;
            while (i < this.m_aWires.Length)
            {
                if (iValue % 2 ==1)
                    this.m_aWires[i].Value = 1;
                else 
                    this.m_aWires[i].Value = 0;
                iValue = iValue / 2;
                i++;
            }
        }

        //Transform the binary code into a positive integer
        public int GetValue()
        {
            int value = 0;

            for (int i=0; i < this.m_aWires.Length; i++)
                value = value + (this.m_aWires[i].Value * (int)Math.Pow(2, i));
 
            return value;
        }

        //Transform an integer value into binary using 2`s complement and set the wires accordingly, with 0 being the LSB
        public void Set2sComplement(int iValue)
        {
            if (iValue >= 0)
            {
                SetValue(iValue);
            }
            else
            {
                int positiveValue = iValue * (-1);
                SetValue(positiveValue);

                for (int i=0; i<this.m_aWires.Length; i++)
                {
                    if (m_aWires[i].Value == 0)
                        m_aWires[i].Value = 1;
                    else m_aWires[i].Value = 0;
                }

                for (int i = 0; i < this.m_aWires.Length; i++)
                {
                    if (m_aWires[i].Value == 0)
                    {
                        m_aWires[i].Value = 1;
                        break;
                    }
                    else
                        m_aWires[i].Value = 0;
                }

            }

        }

        //Transform the binary code in 2`s complement into an integer
        public int Get2sComplement()
        {
            int value = 0;

            if (this.m_aWires[this.m_aWires.Length-1].Value == 0)
                value = GetValue();
            else
            {

                for (int i = 0; i < this.m_aWires.Length; i++)
                {
                    if (m_aWires[i].Value == 0)
                        m_aWires[i].Value = 1;
                    else m_aWires[i].Value = 0;
                }

                int carry = 1;

                for (int i = 0; i < m_aWires.Length; i++)
                {
                    if (m_aWires[i].Value + carry == 2){
                        carry = 1;
                        m_aWires[i].Value = 0;
                    }
                    else if (m_aWires[i].Value + carry == 1)
                    {
                        carry = 0;
                        m_aWires[i].Value = 1;
                    }
                    else
                    {
                        carry = 0;
                        m_aWires[i].Value = 0;
                    }

                }
                value = GetValue() * (-1);
            }
            Set2sComplement(value);
            return value;
        }

        public void ConnectInput(WireSet wIn)
        {
            if (InputConected)
                throw new InvalidOperationException("Cannot connect a wire to more than one inputs");
            if(wIn.Size != Size)
                throw new InvalidOperationException("Cannot connect two wiresets of different sizes.");
            for (int i = 0; i < m_aWires.Length; i++)
                m_aWires[i].ConnectInput(wIn[i]);

            InputConected = true;
            
        }

    }
}
