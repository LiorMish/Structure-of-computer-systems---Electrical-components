using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a mux with k input, each input with n wires. The output also has n wires.

    class BitwiseMultiwayMux : Gate
    {
        //Word size - number of bits in each output
        public int Size { get; private set; }

        //The number of control bits needed for k outputs
        public int ControlBits { get; private set; }

        public WireSet Output { get; private set; }
        public WireSet Control { get; private set; }
        public WireSet[] Inputs { get; private set; }

        //your code here
        private BitwiseMux[] bitwiseMuxes;

        public BitwiseMultiwayMux(int iSize, int cControlBits)
        {
            Size = iSize;
            Output = new WireSet(Size);
            Control = new WireSet(cControlBits);
            Inputs = new WireSet[(int)Math.Pow(2, cControlBits)];

            for (int i = 0; i < Inputs.Length; i++)
            {
                Inputs[i] = new WireSet(Size);
            }

            //your code here
            bitwiseMuxes = new BitwiseMux[(int)Math.Pow(2, cControlBits) - 1];

            for (int i = 0; i < bitwiseMuxes.Length; i++)
                bitwiseMuxes[i] = new BitwiseMux(Size);

            Output.ConnectInput(bitwiseMuxes[bitwiseMuxes.Length - 1].Output);

            int controlsCounter = 0;
            int muxesCounter = 0;
            int muxesPerLevel = bitwiseMuxes.Length / 2 + 1;
            while (controlsCounter < Control.Size)
            {
                for (int i = 0; i < muxesPerLevel; i++)
                {
                    bitwiseMuxes[muxesCounter].ConnectControl(Control[controlsCounter]);
                    muxesCounter++;
                }
                muxesPerLevel = muxesPerLevel / 2;
                controlsCounter++;
            }


            muxesCounter = 0;
            for (int i = 0; i < Inputs.Length; i = i + 2)
            {
                bitwiseMuxes[muxesCounter].ConnectInput1(Inputs[i]);
                bitwiseMuxes[muxesCounter].ConnectInput2(Inputs[i + 1]);
                muxesCounter++;
            }

            int index = 0;
            for (int i = muxesCounter; i < bitwiseMuxes.Length; i++)
            {
                bitwiseMuxes[i].ConnectInput1(bitwiseMuxes[index].Output);
                bitwiseMuxes[i].ConnectInput2(bitwiseMuxes[index + 1].Output);
                index = index + 2;
            }



        }


        public void ConnectInput(int i, WireSet wsInput)
        {
            Inputs[i].ConnectInput(wsInput);
        }
        public void ConnectControl(WireSet wsControl)
        {
            Control.ConnectInput(wsControl);
        }



        public override bool TestGate()
        {

            for (int i = 0; i < Size; i++)
            {
                Inputs[0][i].Value = 1;
            }

            for (int i = 0; i < Control.Size; i++)
            {
                Control[i].Value = 0;
            }

            for (int i = 0; i < Size; i++)
            {
                if (this.Output[i].Value != 1)
                {
                    return false;
                }
            }


            for (int i = 0; i < Control.Size; i++)
            {
                Control[i].Value = 1;
            }

            for (int i = 0; i < Size; i++)
            {
                if (this.Output[i].Value != 0)
                {
                    return false;
                }
            }

            for (int i = 0; i < Size; i++)
            {
                Inputs[0][i].Value = 0;
            }

            for (int i = 0; i < Size; i++)
            {
                Inputs[Inputs.Length - 1][i].Value = 1;
            }

            for (int i = 0; i < Size; i++)
            {
                if (this.Output[i].Value != 1)
                {
                    return false;
                }
            }

            for (int i = 0; i < Size; i++)
            {
                Inputs[Inputs.Length - 1][i].Value = 0;
            }

            for (int i = 0; i < Size; i++)
            {
                if (this.Output[i].Value != 0)
                {
                    return false;
                }
            }
            return true;
        }


    }
}
