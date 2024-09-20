using System;
using System.Collections;
using System.Diagnostics;

namespace Weaver.Heroes.Destiny;

public interface ICondition
{
    bool IsTrue{ get; }
    string ToMacro();
}

public class Flag : ICondition
{
    public bool IsTrue{ get; set; }

    public Flag(bool isTrue = true)
    {
        IsTrue = isTrue;
    }
    
    public string ToMacro()
    {
        return IsTrue.ToString();
    }
}

public class Not : ICondition
{
    public ICondition Cond;
    public bool IsTrue{ get { return !Cond.IsTrue; } }

    public Not(ICondition cond)
    {
        Cond = cond;
    }
    
    public string ToMacro()
    {
        return "!" + Cond.ToString();
    }
}

public class RefCondition : ICondition
{
    public ICondition Cond;
    public bool IsTrue{ get { return Cond.IsTrue; } }

    public RefCondition(ICondition cond)
    {
        Cond = cond;
    }

    public string ToMacro()
    {
        return Cond.ToMacro();
    }
}


public abstract class LogicOperator : ICondition
{
    public ICondition Left;
    public ICondition Right;

    public LogicOperator( ICondition left, ICondition right )
    {
        Left = left;
        Right = right;
    }

    public bool IsTrue {
        get {
            return Operator(Left, Right);
        }
    }

    public abstract bool Operator(ICondition left, ICondition right);
    public abstract string OperatorString { get; }

    public string ToMacro()
    {
        Debug.Assert(Left != null);
        Debug.Assert(Right != null);
        return string.Format("{0}{1}{2}", Left.ToString(), OperatorString, Left.ToString());
    }
}

public abstract class ComparisonOperator<T> : ICondition where T : IComparable<T>
{
    public T Left { get { return RefLeft.Value; } }
    public T Right { get { return RefRight.Value; } }
    public ComparableReference<T> RefLeft;
    public ComparableReference<T> RefRight;

    public ComparisonOperator( ComparableReference<T> left, ComparableReference<T> right )
    {
        Debug.Assert(left != null);
        Debug.Assert(right != null);
        RefLeft = left;
        RefRight = right;
    }

    public bool IsTrue {
        get {
            return Operator(Left, Right);
        }
    }

    public abstract bool Operator(T left, T right);
    public abstract string OperatorString { get; }

    public string ToMacro()
    {
        return string.Format("{0}{1}{2}", Left.ToString(), OperatorString, Right.ToString());
    }
}