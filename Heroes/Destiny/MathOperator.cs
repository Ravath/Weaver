using System;
using System.Diagnostics;

namespace Weaver.Heroes.Destiny;


public abstract class MathOperator<T> : ComparableReference<T> where T : IComparable<T>
{
    public ComparableReference<T> Op1;
    public ComparableReference<T> Op2;

    public MathOperator(ComparableReference<T> op1, ComparableReference<T> op2)
    {
        Debug.Assert(op1 != null);
        Debug.Assert(op2 != null);
        Op1 = op1;
        Op2 = op2;
    }

    public abstract string OperatorString { get; }

    public override string ToMacro()
    {
        return string.Format("{0}{1}{2}", Op1.ToString(), OperatorString, Op2.ToString());
    }
}

public class ReferenceOperator : ComparableReference<int>
{
    public ComparableReference<int> Op;

    public ReferenceOperator(ComparableReference<int> op)
    {
        Debug.Assert(op != null);
        Op = op;
    }

    public override int Value => Op.Value;

    public override string ToMacro()
    {
        return "(" + Op.ToMacro() + ")";
    }
}

public class NegativeOperator : ComparableReference<int>
{
    public ComparableReference<int> Op;

    public NegativeOperator(ComparableReference<int> op)
    {
        Debug.Assert(op != null);
        Op = op;
    }

    public override int Value => - Op.Value;

    public override string ToMacro()
    {
        return "-" + Op.ToMacro();
    }
}

public class AddOperator : MathOperator<int>
{
    public AddOperator(ComparableReference<int> op1, ComparableReference<int> op2) : base(op1, op2)
    {
    }

    public override int Value => Op1.Value + Op2.Value;

    public override string OperatorString => "+";
}

public class SubOperator : MathOperator<int>
{
    public SubOperator(ComparableReference<int> op1, ComparableReference<int> op2) : base(op1, op2)
    {
    }

    public override int Value => Op1.Value - Op2.Value;

    public override string OperatorString => "-";
}

public class MulOperator : MathOperator<int>
{
    public MulOperator(ComparableReference<int> op1, ComparableReference<int> op2) : base(op1, op2)
    {
    }

    public override int Value => Op1.Value * Op2.Value;
    
    public override string OperatorString => "*";
}

public class DivOperator : MathOperator<int>
{
    public DivOperator(ComparableReference<int> op1, ComparableReference<int> op2) : base(op1, op2)
    {
    }

    public override int Value => Op1.Value / Op2.Value;
    
    public override string OperatorString => "/";
}