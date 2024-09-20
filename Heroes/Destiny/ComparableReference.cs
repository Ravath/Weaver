using System;
using System.Diagnostics;
using Weaver.Heroes.Body;
using Weaver.Heroes.Luck;

namespace Weaver.Heroes.Destiny;

public abstract class ComparableReference<T> : IComparable<T> where T : IComparable<T>
{
    public abstract T Value{ get; }

    public int CompareTo(T? other)
    {
        return Value.CompareTo(other);
    }

    public abstract string ToMacro();
}

public class PrimitiveReader<T> : ComparableReference<T> where T : IComparable<T>
{
    public T val;

    public override T Value { 
        get{
            return val; 
        }
    }

    public PrimitiveReader(T val)
    {
        Debug.Assert(val != null);
        this.val = val;
    }

    public override string ToMacro()
    {
        return Value.ToString() ?? "";
    }
}

public class RollReader : ComparableReference<int>
{
    public IRoll Roll { get; set; }
    public RollReader(IRoll roll)
    {
        Debug.Assert(roll != null);
        Roll = roll;
    }

    public override int Value {
        get {
            Roll.Roll();
            return Roll.NetResult;
        }
    }

    public override string ToMacro()
    {
        return string.Format("[{0}]", Roll.ToMacro());
    }
}

public class ValueModuleReader<T> : ComparableReference<T> where T : IComparable<T>
{
    public ModuleReference Ref;
    public string ModulePath;

    public override T Value { 
        get{
            return Ref.Module.GetRegisteredByPath<IValue<T>>(ModulePath).Value; 
        }
    }

    public ValueModuleReader(ModuleReference moduleRef, string modulePath)
    {
        Ref = moduleRef;
        ModulePath = modulePath;
    }
    
    public override string ToMacro()
    {
        return string.Format("{0}({1})", ModulePath, Ref.Module.GetRegisteredByPath<IValue<T>>(ModulePath).Value);
    }
}

public class ModuleReference
{
    public Module Module;
    public ModuleReference(Module module)
    {
        Module = module;
    }
}