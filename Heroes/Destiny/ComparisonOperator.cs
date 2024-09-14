using System;

namespace Weaver.Heroes.Destiny;


public class Equality<T> : ComparisonOperator<T> where T : IComparable<T>
{
    public Equality(ComparableReference<T> v1, ComparableReference<T> v2) : base(v1, v2) { }

    public override bool Operator(T val1, T val2)
    {
        return val1.CompareTo(val2) == 0;
    }
    public override string OperatorString { get{ return "=="; } }
}

public class Inequality<T> : ComparisonOperator<T> where T : IComparable<T>
{
    public Inequality(ComparableReference<T> v1, ComparableReference<T> v2) : base(v1, v2) { }

    public override bool Operator(T val1, T val2)
    {
        return val1.CompareTo(val2) != 0;
    }
    public override string OperatorString { get{ return "!="; } }
}

public class Greater<T> : ComparisonOperator<T> where T : IComparable<T>
{
    public Greater(ComparableReference<T> v1, ComparableReference<T> v2) : base(v1, v2) { }

    public override bool Operator(T val1, T val2)
    {
        return val1.CompareTo(val2) > 0;
    }
    public override string OperatorString { get{ return ">"; } }
}

public class GreaterOrEqual<T> : ComparisonOperator<T> where T : IComparable<T>
{
    public GreaterOrEqual(ComparableReference<T> v1, ComparableReference<T> v2) : base(v1, v2) { }

    public override bool Operator(T val1, T val2)
    {
        return val1.CompareTo(val2) >= 0;
    }
    public override string OperatorString { get{ return ">="; } }
}

public class Lower<T> : ComparisonOperator<T> where T : IComparable<T>
{
    public Lower(ComparableReference<T> v1, ComparableReference<T> v2) : base(v1, v2) { }

    public override bool Operator(T val1, T val2)
    {
        return val1.CompareTo(val2) < 0;
    }
    public override string OperatorString { get{ return "<"; } }
}

public class LowerOrEqual<T> : ComparisonOperator<T> where T : IComparable<T>
{
    public LowerOrEqual(ComparableReference<T> v1, ComparableReference<T> v2) : base(v1, v2) { }

    public override bool Operator(T val1, T val2)
    {
        return val1.CompareTo(val2) <= 0;
    }
    public override string OperatorString { get{ return "<="; } }
}