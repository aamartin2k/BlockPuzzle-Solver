
using System;

namespace BPSolver.Enums
{

    public enum CommandAction
    { Select, Undo, Delete, Play }

    public enum SequenceAction
    { First, Previous, Next, Last }

    public enum PieceColor
    { None, One, Two, Three, Four, Five, Six, Seven, Eight, Nine }

    [Serializable]
    public enum PieceName
    {
        None,
        One,
        TwoHor, TwoVert,
        ThreeHor, ThreeVert,
        ThreeLOne, ThreeLTwo, ThreeLThree, ThreeLFour,
        Four,
        FourT1, FourT2, FourT3, FourT4,
        FourJ1, FourJ2, FourJ3, FourJ4,
        FourL1, FourL2, FourL3, FourL4,
        FourS1, FourS2, 
        FourZ1, FourZ2,
        FourHor, FourVert,
        FiveHor, FiveVert,
        FiveL1, FiveL2, FiveL3, FiveL4,
        Nine
    }


}