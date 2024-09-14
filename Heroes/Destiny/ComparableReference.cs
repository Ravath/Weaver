using System;
using Weaver.Heroes.Body;

namespace Weaver.Heroes.Destiny;

public abstract class ComparableReference<T> : IComparable<T> where T : IComparable<T>
{
    public abstract T Value{ get; }

    public int CompareTo(T? other)
    {
        return Value.CompareTo(other);
    }
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
        this.val = val;
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
}

public class ModuleReference
{
    public Module Module;
    public ModuleReference(Module module)
    {
        Module = module;
    }
}