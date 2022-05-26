using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class is used to implement the ALU
    class ALU : Gate
    {
        //The word size = number of bit in the input and output
        public int Size { get; private set; }

        //Input and output n bit numbers
        //inputs
        public WireSet InputX { get; private set; }
        public WireSet InputY { get; private set; }
        public WireSet Control { get; private set; }

        //outputs
        public WireSet Output { get; private set; }
        public Wire Zero { get; private set; }
        public Wire Negative { get; private set; }


        //your code here

        private BitwiseMultiwayMux outputControlMux;
        private BitwiseNotGate notX;
        private BitwiseNotGate notY;
        private MultiBitAdder minusXAdder;
        private MultiBitAdder minusYAdder;
        private MultiBitAdder x1Adder;
        private MultiBitAdder y1Adder;
        private MultiBitAdder x1Sub;
        private MultiBitAdder y1Sub;
        private MultiBitAdder xyAdder;
        private MultiBitAdder xySub;
        private MultiBitAdder yxSub;
        private BitwiseOrGate orBitwise;
        private BitwiseAndGate andBitwise;
        private BitwiseOrGate logicOrBitwise;
        private WireSet zeroes;
        private WireSet one;
        private WireSet minusOne;
        public ALU(int iSize)
        {
            Size = iSize;
            InputX = new WireSet(Size);
            InputY = new WireSet(Size);
            Control = new WireSet(6);
            Zero = new Wire();
            Output = new WireSet(Size);
            Negative = new Wire();

            //Create and connect all the internal components
            outputControlMux = new BitwiseMultiwayMux(Size, Control.Size);
            outputControlMux.ConnectControl(Control);

            //0 (0)
            zeroes = new WireSet(Size);
            zeroes.SetValue(0);
            outputControlMux.ConnectInput(0, zeroes);

            //1 (1)
            one = new WireSet(Size);
            one.SetValue(1);
            outputControlMux.ConnectInput(1, one);

            //x (2)
            outputControlMux.ConnectInput(2, InputX);

            //y (3)
            outputControlMux.ConnectInput(3, InputY);

            //not x (4)
            notX = new BitwiseNotGate(Size);
            notX.ConnectInput(InputX);
            outputControlMux.ConnectInput(4, notX.Output);

            //not y (5)
            notY = new BitwiseNotGate(Size);
            notY.ConnectInput(InputY);
            outputControlMux.ConnectInput(5, notY.Output);

            //minus x (6)
            minusXAdder = new MultiBitAdder(Size);
            minusXAdder.ConnectInput1(notX.Output);
            minusXAdder.ConnectInput2(one);
            outputControlMux.ConnectInput(6, minusXAdder.Output);

            //minus y (7)
            minusYAdder = new MultiBitAdder(Size);
            minusYAdder.ConnectInput1(notY.Output);
            minusYAdder.ConnectInput2(one);
            outputControlMux.ConnectInput(7, minusYAdder.Output);

            //x+1 (8)
            x1Adder = new MultiBitAdder(Size);
            x1Adder.ConnectInput1(InputX);
            x1Adder.ConnectInput2(one);
            outputControlMux.ConnectInput(8, x1Adder.Output);

            //y+1 (9)
            y1Adder = new MultiBitAdder(Size);
            y1Adder.ConnectInput1(InputY);
            y1Adder.ConnectInput2(one);
            outputControlMux.ConnectInput(9, y1Adder.Output);

            // create -1 wire set
            minusOne = new WireSet(Size);
            minusOne.Set2sComplement(-1);

            //x-1 (10)
            x1Sub = new MultiBitAdder(Size);
            x1Sub.ConnectInput1(InputX);
            x1Sub.ConnectInput2(minusOne);
            outputControlMux.ConnectInput(10, x1Sub.Output);

            //y-1 (11)
            y1Sub = new MultiBitAdder(Size);
            y1Sub.ConnectInput1(InputY);
            y1Sub.ConnectInput2(minusOne);
            outputControlMux.ConnectInput(11, y1Sub.Output);

            //x+y (12)
            xyAdder = new MultiBitAdder(Size);
            xyAdder.ConnectInput1(InputX);
            xyAdder.ConnectInput2(InputY);
            outputControlMux.ConnectInput(12, xyAdder.Output);

            //x-y (13)
            xySub = new MultiBitAdder(Size);
            xySub.ConnectInput1(InputX);
            xySub.ConnectInput2(minusYAdder.Output);
            outputControlMux.ConnectInput(13, xySub.Output);

            //y-x (14)
            yxSub = new MultiBitAdder(Size);
            yxSub.ConnectInput1(InputY);
            yxSub.ConnectInput2(minusXAdder.Output);
            outputControlMux.ConnectInput(14, yxSub.Output);

            //and bitwise (15)
            andBitwise = new BitwiseAndGate(Size);
            andBitwise.ConnectInput1(InputX);
            andBitwise.ConnectInput2(InputY);
            outputControlMux.ConnectInput(15, andBitwise.Output);

            //logical and (16)
            MultiBitOrGate multiBitOrGate1 = new MultiBitOrGate(Size);
            multiBitOrGate1.ConnectInput(InputX);
            MultiBitOrGate multiBitOrGate2 = new MultiBitOrGate(Size);
            multiBitOrGate2.ConnectInput(InputY);
            AndGate and = new AndGate();
            and.ConnectInput1(multiBitOrGate1.Output);
            and.ConnectInput2(multiBitOrGate2.Output);
            BitwiseMux muxLogic = new BitwiseMux(Size);
            muxLogic.ConnectInput1(zeroes);
            muxLogic.ConnectInput2(one);
            muxLogic.ConnectControl(and.Output);
            outputControlMux.ConnectInput(16, muxLogic.Output);

            //or bitwise (17)
            orBitwise = new BitwiseOrGate(Size);
            orBitwise.ConnectInput1(InputX);
            orBitwise.ConnectInput2(InputY);
            outputControlMux.ConnectInput(17, orBitwise.Output);

            //logical or (18)
            logicOrBitwise = new BitwiseOrGate(Size);
            logicOrBitwise.ConnectInput1(InputX);
            logicOrBitwise.ConnectInput2(InputY);
            outputControlMux.ConnectInput(18, logicOrBitwise.Output);

            //mux output
            Output.ConnectInput(outputControlMux.Output);

            //zero output
            MultiBitOrGate multiBitOrGateOut = new MultiBitOrGate(Size);
            multiBitOrGateOut.ConnectInput(Output);
            Zero = multiBitOrGateOut.Output;
            
            //negative output
            Negative = Output[Size - 1];
        }




        public override bool TestGate()
        {
            throw new NotImplementedException();
        }
    }
}
