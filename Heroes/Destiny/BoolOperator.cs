using System;

namespace Weaver.Heroes.Destiny;


public class And : LogicOperator
{
    public And(ICondition v1, ICondition v2) : base(v1, v2) { }

    public override bool Operator(ICondition val1, ICondition val2)
    {
        return val1.IsTrue && val2.IsTrue;
    }
    public override string OperatorString { get{ return "&&"; } }
}

public class Or : LogicOperator
{
    public Or(ICondition v1, ICondition v2) : base(v1, v2) { }

    public override bool Operator(ICondition val1, ICondition val2)
    {
        return val1.IsTrue || val2.IsTrue;
    }
    public override string OperatorString { get{ return "||"; } }
}