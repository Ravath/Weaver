using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Weaver.Heroes.Body;

/// <summary>
/// A named Module.
/// Modules are linked in an arborescence
/// that can be accessed with namepaths.
/// <para>namepath format : modulename(.modulename).</para>
/// <para>Uses Json Serialisation annotations.</para>
/// </summary>
public class Module
{
    private Dictionary<string, Module> _submodules = new();

    /// <summary>
    /// The name of the Module.
    /// </summary>
    [JsonIgnore]
    public string ModuleName { get; private set; }

    /// <summary>
    /// The name of the Module and its parents.
    /// </summary>
    [JsonIgnore]
    public string FullModuleName { get {
        if(Parent == null)
            return ModuleName;
        return Parent.FullModuleName + "." + ModuleName;
    } }
    /// <summary>
    /// The module's parent in the arborescence.
    /// </summary>
    [JsonIgnore]
    public Module? Parent { get; private set; }
    
    public Module(string moduleName)
    {
        ModuleName = moduleName;
    }

    /// <summary>
    /// Add the given module to the arborescence.
    /// </summary>
    /// <param name="na"></param>
    /// <exception cref="ArgumentException"></exception>
    public Module Register(Module na)
    {
        if(_submodules.ContainsKey(na.ModuleName))
            throw new ArgumentException(na.ModuleName + " is already registered");
        _submodules.Add(na.ModuleName, na);
        na.Parent = this;
        return na;
    }

    /// <summary>
    /// Add the given module to the arborescence.
    /// </summary>
    /// <param name="na"></param>
    /// <exception cref="ArgumentException"></exception>
    public void Unregister(Module na)
    {
        if(!_submodules.ContainsKey(na.ModuleName))
            throw new ArgumentException(na.ModuleName + " is not registered");
        _submodules.Remove(na.ModuleName);
        na.Parent = null;
    }

    /// <summary>
    /// Check if the module contains a direct child with the given name.
    /// </summary>
    /// <param name="moduleName">A module name.</param>
    /// <returns>True if found.</returns>
    public virtual bool HasRegistered(string moduleName)
    {
        return _submodules.ContainsKey(moduleName);
    }

    /// <summary>
    /// Get the direct child module with the given name.
    /// </summary>
    /// <param name="moduleName">A module name.</param>
    /// <returns>A module.</returns>
    public virtual Module GetRegistered(string moduleName)
    {
        return _submodules[moduleName];
    }

    /// <summary>
    /// Use a namepath to get a module among the subarborescence of modules.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fullpath"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public T GetRegisteredByPath<T>(string fullpath)
    {
        // Find in arborescence
        string[] path = fullpath.Split('.');
        if(path[0] == ModuleName)
            path = path[1..];
        Module current = this;
        foreach (string attname in path)
        {
            current = current.GetRegistered(attname);
        }

        // Convert and return
        if(current is T return_value)
        {
            return return_value;
        }
        else
        {
            throw new ArgumentException(string.Format("Can't find {0} of type {1}.",
                fullpath,
                current?.GetType().ToString() ?? "Not found"));
        }
    }

    /// <summary>
    /// Unregister every children.
    /// </summary>
    public void UnregisterAll()
    {
        foreach(Module child in _submodules.Values.ToList())
        {
            Unregister(child);
        }
    }

    public IEnumerable<T> GetChildren<T>() where T : Module
    {
        foreach (Module child in _submodules.Values) {
            if(child is T tc)
                yield return tc;
            foreach (T sub in child.GetChildren<T>()) {
                yield return sub;
            }
        }
    }
}