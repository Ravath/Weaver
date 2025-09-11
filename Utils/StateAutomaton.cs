using QUT.Gppg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weaver.Utils
{
    /// <summary>
    /// Delegate called when a state is entered or exited.
    /// </summary>
    public delegate void StateChangeDelegate();

    /// <summary>
    /// An automaton made of several states.
    /// </summary>
    /// <typeparam name="T">An enum of every state of the automaton.</typeparam>
    public class Automaton<T> where T : Enum
    {
        private readonly Dictionary<T, State<T>> _states = new();

        public T PreviousState;
        public T CurrentState { get; private set; }

        public Automaton()
        {
            var enum_values = typeof(T).GetEnumValues();
            foreach (T e in enum_values)
            {
                _states.Add(e, new State<T>());
            }
        }

        /// <summary>
        /// Add some delegates to a state.
        /// </summary>
        /// <param name="stateId">The state ID associated with these events.</param>
        /// <param name="inDelegate">The delegate to execute when the entering state event is raised.</param>
        /// <param name="outDelegate">The delegate to execute when the exiting state event is raised.</param>
        /// <returns></returns>
        public void AddStateEvents(T stateId, StateChangeDelegate? inDelegate = null, StateChangeDelegate? outDelegate = null)
        {
            if (inDelegate != null)
                _states[stateId].OnStateIn += inDelegate;
            if (outDelegate != null)
                _states[stateId].OnStateOut += outDelegate;
        }

        /// <summary>
        /// Declare a posible transition from one state to another.
        /// </summary>
        /// <param name="stateIdFrom"></param>
        /// <param name="stateIdTo"></param>
        /// <param name="reverse"></param>
        public void AddTransition(T stateIdFrom, T stateIdTo, bool reverse = true)
        {
            _states[stateIdFrom].Neighbours.Add(stateIdTo);
            if (reverse)
            {
                _states[stateIdTo].Neighbours.Add(stateIdFrom);
            }
        }

        /// <summary>
        /// Add delegates to the transition from one state to another.
        /// </summary>
        /// <param name="stateIdFrom">The starting state of the transition.</param>
        /// <param name="stateIdTo">The ending state of the transition.</param>
        /// <param name="toDelegate">The delegate to call when transiting from start to end.</param>
        /// <param name="reverseDelegate">The delegate to call when transiting from end to start.</param>
        public void AddTransitionEvents(T stateIdFrom, T stateIdTo, StateChangeDelegate? toDelegate = null, StateChangeDelegate? reverseDelegate = null)
        {
            if (toDelegate != null)
            {
                _states[stateIdFrom].OnStateTransition[stateIdTo].Add(toDelegate);
            }
            if (reverseDelegate != null)
            {
                _states[stateIdTo].OnStateTransition[stateIdFrom].Add(reverseDelegate);
            }
        }

        public void Start(T startState)
        {
            CurrentState = startState;
        }

        /// <summary>
        /// Start a transition from the current state to another.
        /// </summary>
        /// <param name="destination"></param>
        public void Transition(T destination)
        {
            PreviousState = CurrentState;
            CurrentState = destination;

            _states[PreviousState].Transition(destination);

            _states[destination].EnteringState();

        }
    }

    /// <summary>
    /// An automaton state, ID by an enum value.
    /// </summary>
    /// <typeparam name="T">Type of the state enum.</typeparam>
    class State<T> where T : Enum
    {
        /// <summary>
        /// Event raised when entering the state.
        /// </summary>
        public event StateChangeDelegate OnStateIn;

        /// <summary>
        /// Event raised when exiting the state.
        /// </summary>
        public event StateChangeDelegate OnStateOut;

        /// <summary>
        /// Event raised when a specific transition from one state to another occurs.
        /// </summary>
        public Dictionary<T, List<StateChangeDelegate>> OnStateTransition = new();

        /// <summary>
        /// The set of neighbour states that can be reached from this one.
        /// </summary>
        public HashSet<T> Neighbours = new();

        public State()
        {
            var enum_values = typeof(T).GetEnumValues();
            foreach (T e in enum_values)
            {
                OnStateTransition.Add(e, new List<StateChangeDelegate>());
            }
        }

        public State(StateChangeDelegate? inDelegate = null, StateChangeDelegate? outDelegate = null)
        {
            if(inDelegate != null)
                OnStateIn += inDelegate;
            if(outDelegate != null)
                OnStateOut += outDelegate;
        }

        internal void EnteringState()
        {
            OnStateIn?.Invoke();
        }

        internal void Transition(T destination)
        {
            OnStateOut?.Invoke();

            if (OnStateTransition.ContainsKey(destination))
            {
                foreach (var d in OnStateTransition[destination])
                {
                    d.Invoke();
                }
            }
        }


    }
}
