using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a memory unit, containing k registers, each of size n bits.
    class Memory : SequentialGate
    {
        //The address size determines the number of registers
        public int AddressSize { get; private set; }
        //The word size determines the number of bits in each register
        public int WordSize { get; private set; }

        //Data input and output - a number with n bits
        public WireSet Input { get; private set; }
        public WireSet Output { get; private set; }
        //The address of the active register
        public WireSet Address { get; private set; }
        //A bit setting the memory operation to read or write
        public Wire Load { get; private set; }

        //your code here
        private MultiBitRegister[] memory;
        private BitwiseMultiwayDemux writeControl;
        private BitwiseMultiwayMux readControl;
        private WireSet demuxInput;

        public Memory(int iAddressSize, int iWordSize)
        {
            AddressSize = iAddressSize;
            WordSize = iWordSize;

            Input = new WireSet(WordSize);
            Output = new WireSet(WordSize);
            Address = new WireSet(AddressSize);
            Load = new Wire();

            //your code here
            memory = new MultiBitRegister[(int)Math.Pow(2,AddressSize)];
            writeControl = new BitwiseMultiwayDemux(1, AddressSize);
            readControl = new BitwiseMultiwayMux(WordSize, AddressSize);

            demuxInput = new WireSet(1);
            demuxInput[0].ConnectInput(Load);
          

            writeControl.ConnectInput(demuxInput);
            writeControl.ConnectControl(Address);

            readControl.ConnectControl(Address);

            Output.ConnectInput(readControl.Output);


            for (int i = 0; i < memory.Length; i++)
            {
                memory[i] = new MultiBitRegister(WordSize);
                memory[i].ConnectInput(Input);
                memory[i].Load.ConnectInput(writeControl.Outputs[i][0]);
                readControl.Inputs[i].ConnectInput(memory[i].Output);
            }


        }

        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }
        public void ConnectAddress(WireSet wsAddress)
        {
            Address.ConnectInput(wsAddress);
        }


        public override void OnClockUp()
        {
        }

        public override void OnClockDown()
        {
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public override bool TestGate()
        {
            for (int i = 0; i < memory.Length; i++)
            {
                return memory[i].TestGate();
            }
            return true;
        }
    }
}
