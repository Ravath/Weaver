using System;
using System.Diagnostics;
using Weaver.Heroes.Body.Value.Modifier;

namespace Weaver.Heroes.Body.Value;

public enum ThresholdCrossingState {
    /// <summary>
    /// The value crossed the threshold and has now a greater value.
    /// </summary>
    GREATER,
    /// <summary>
    /// The value crossed the threshold and has now a lower value.
    /// </summary>
    LOWER,
    /// <summary>
    /// The value reached the threshold and has now the same value.
    /// </summary>
    EQUAL
}

public delegate void ThresholdCrossingEvent<T>(ThresholdModule<T> jauge, ThresholdCrossingState crossing)  where T : IComparable;

/// <summary>
/// A Value Threshold Module monitoring another Value Module.
/// The module value is the threshold.
/// </summary>
/// <typeparam name="T">The type of the value to monitor.</typeparam>
public class ThresholdModule<T> : ValueModule<T> where T : IComparable
{
    /// <summary>
    /// The last threshold crossing state.
    /// </summary>
    private ThresholdCrossingState _lastState = ThresholdCrossingState.EQUAL;
    /// <summary>
    /// same as _lastState, but can't be EQUAL.
    /// Null at initialisation if the monitored value equals the threshold.
    /// </summary>
    private ThresholdCrossingState? _lastStateStrict = null;
    /// <summary>
    /// The monitored value.
    /// </summary>
    private IModifiableValue<T> _checkedValue;

    /// <summary>
    /// Event fired if the monitored value reaches or crosses the threshold.
    /// </summary>
    public event ThresholdCrossingEvent<T>? OnJaugeCrossing;

    /// <summary>
    /// Event fired if the monitored value crosses the threshold
    /// but not if it only reaches it.
    /// </summary>
    public event ThresholdCrossingEvent<T>? OnStrictJaugeCrossing;

    /// <summary>
    /// The monitored value is inferior to the threshold.
    /// </summary>
    public bool IsInferior
    {
        get { return _checkedValue.Value.CompareTo(ModifiedValue) == -1; }
    }
    /// <summary>
    /// The monitored value is inferior or equal to the threshold.
    /// </summary>
    public bool IsInferiorOrEqual
    {
        get { return IsEqual || IsInferior; }
    }
    /// <summary>
    /// The monitored value is equal to the threshold.
    /// </summary>
    public bool IsEqual
    {
        get { return _checkedValue.Value.CompareTo(ModifiedValue) == 0; }
    }
    /// <summary>
    /// The monitored value is superior or equal to the threshold.
    /// </summary>
    public bool IsSuperiorOrEqual
    {
        get { return IsEqual || IsSuperior; }
    }
    /// <summary>
    /// The monitored value is superior to the threshold.
    /// </summary>
    public bool IsSuperior
    {
        get { return _checkedValue.Value.CompareTo(ModifiedValue) == 1; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="moduleName">The name of the threshold module.</param>
    /// <param name="checkedValue">The value module to monitor.</param>
    /// <param name="baseThresholdValue">The module value is the threshold value.</param>
    public ThresholdModule(string moduleName, IModifiableValue<T> checkedValue, T baseThresholdValue) : base(moduleName, baseThresholdValue)
    {
        _checkedValue = checkedValue;
        checkedValue.OnValueChanged += CheckLimits;
        BaseValue = baseThresholdValue;
        OnValueChanged += CheckLimits;
        _lastState = GetState();
        if(_lastState != ThresholdCrossingState.EQUAL)
            _lastStateStrict = _lastState;
    }

    private ThresholdCrossingState GetState()
    {
        if(IsSuperior)
        {
            return ThresholdCrossingState.GREATER;
        }
        else if(IsInferior)
        {
            return ThresholdCrossingState.LOWER;
        } 
        else
        {
            return ThresholdCrossingState.EQUAL;
        }
    }

    private void CheckLimits(IValue<T> _)
    {
        ThresholdCrossingState newState = GetState();
        if(newState != ThresholdCrossingState.EQUAL
        && newState != _lastStateStrict)
        {
            bool fireStrictEvent = _lastStateStrict != null;
            _lastStateStrict = newState;
            _lastState = newState;
            OnJaugeCrossing?.Invoke(this, newState);
            if(fireStrictEvent)
                OnStrictJaugeCrossing?.Invoke(this, newState);
        }
        else if(newState != _lastState)
        {
            _lastState = newState;
            OnJaugeCrossing?.Invoke(this, newState);
        }
    }

    #region Limiter Management
    private RefModifier<T>? _limiter = null;
    
    public void SetLimiter(bool active = true, bool upperLimiter = true, int priority = 4)
    {
        if(_limiter != null)
        // assume we have to create a new limiter with new values
        {
            _checkedValue.RemoveModifier(_limiter);
            _limiter = null;
        }
        if(active)
        {
            _limiter = IntModifier.LimiterModifier<T>(this, upperLimiter, priority);
            _checkedValue.AddModifier(_limiter);
        }
    }
    #endregion
}