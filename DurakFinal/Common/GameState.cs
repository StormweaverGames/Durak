﻿using Durak.Common.Cards;
using System;
using System.Collections.Generic;
using Lidgren.Network;
using System.Linq;

namespace Durak.Common
{
    /// <summary>
    /// Represents the state of a game. This is a re-usable class that can be used by any card game
    /// </summary>
    public class GameState
    {
        /// <summary>
        /// Stores the string format for array element naming
        /// </summary>
        private const string ARRAY_FORMAT = "@{0}[{1}]";

        /// <summary>
        /// Stores the dictionary of parameters
        /// </summary>
        private Dictionary<string, StateParameter> myParameters;

        /// <summary>
        /// Stores a collection of state changed events
        /// </summary>
        private Dictionary<string, StateChangedEvent> myChangedEvents;

        /// <summary>
        /// Stores a collection of state equals events
        /// </summary>
        private Dictionary<Tuple<string, object>, StateChangedEvent> myStateEqualsEvents;
        
        /// <summary>
        /// Invoked when a single state withing this game state is changed
        /// </summary>
        public event EventHandler<StateParameter> OnStateChanged;
        /// <summary>
        /// Invoked when a single state withing this game state is changed, this event is unsilenceable, and is used
        /// for mostly UI purposes
        /// </summary>
        public event EventHandler<StateParameter> OnStateChangedUnSilenceable;
        /// <summary>
        /// Invoked when the state is cleared
        /// </summary>
        public event EventHandler OnCleared;
        
        /// <summary>
        /// Gets or sets whether the state should not raise events when parmaeters are set.
        /// Usefull for initialization
        /// </summary>
        public bool SilentSets
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a new instance of a game state
        /// </summary>
        public GameState()
        {
            myParameters = new Dictionary<string, StateParameter>();
            myChangedEvents = new Dictionary<string, StateChangedEvent>();
            myStateEqualsEvents = new Dictionary<Tuple<string, object>, StateChangedEvent>();
        }

        /// <summary>
        /// Clears this game state for re-use
        /// </summary>
        public void Clear()
        {
            myParameters.Clear();

            if (OnCleared != null)
                OnCleared.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Adds a state changed listener to the given state parameter
        /// </summary>
        /// <param name="name">The name of the state to listen to</param>
        /// <param name="eventListener">The event to invoke on state change</param>
        public void AddStateChangedEvent(string name, StateChangedEvent eventListener)
        {
            if (!myChangedEvents.ContainsKey(name))
                myChangedEvents.Add(name, eventListener);
            else
                myChangedEvents[name] += eventListener;
        }

        /// <summary>
        /// Removed a state changed listener to the given state parameter
        /// </summary>
        /// <param name="name">The name of the state to listen to</param>
        /// <param name="eventListener">The event to invoke on state change</param>
        public void RemoveStateChangedEvent(string name, StateChangedEvent eventListener)
        {
            if (!myChangedEvents.ContainsKey(name))
                myChangedEvents.Add(name, eventListener);
            else
                myChangedEvents[name] -= eventListener;
        }
        
        /// <summary>
        /// Adds a state equals listener to the given state parameter
        /// </summary>
        /// <param name="name">The name of the state to listen to</param>
        /// <param name="value">The value to invoke on</param>
        /// <param name="eventListener">The event to invoke on state change</param>
        public void AddStateEqualsEvent(string name, object value, StateChangedEvent eventListener)
        {
            Tuple<string, object> key = new Tuple<string, object>(name, value);

            if (!myStateEqualsEvents.ContainsKey(key))
                myStateEqualsEvents.Add(key, eventListener);
            else
                myStateEqualsEvents[key] += eventListener;
        }

        /// <summary>
        /// Removed a state equals listener to the given state parameter
        /// </summary>
        /// <param name="name">The name of the state to listen to</param>
        /// <param name="value">The value to invoke on</param>
        /// <param name="eventListener">The event to invoke on state change</param>
        public void RemoveStateEqualsEvent(string name, object value, StateChangedEvent eventListener)
        {
            Tuple<string, object> key = new Tuple<string, object>(name, value);

            if (!myStateEqualsEvents.ContainsKey(key))
                myStateEqualsEvents.Add(key, eventListener);
            else
                myStateEqualsEvents[key] -= eventListener;
        }

        /// <summary>
        /// Adds a state changed listener to the given state parameter
        /// </summary>
        /// <param name="name">The name of the state to listen to</param>
        /// <param name="index">The index of the element to listen to</param>
        /// <param name="eventListener">The event to invoke on state change</param>
        public void AddStateChangedEvent(string name, int index, StateChangedEvent eventListener)
        {
            name = string.Format(ARRAY_FORMAT, name, index);

            if (!myChangedEvents.ContainsKey(name))
                myChangedEvents.Add(name, eventListener);
            else
                myChangedEvents[name] += eventListener;
        }

        /// <summary>
        /// Removed a state changed listener to the given state parameter
        /// </summary>
        /// <param name="name">The name of the state to listen to</param>
        /// <param name="index">The index of the element to listen to</param>
        /// <param name="eventListener">The event to invoke on state change</param>
        public void RemoveStateChangedEvent(string name, int index, StateChangedEvent eventListener)
        {
            name = string.Format(ARRAY_FORMAT, name, index);

            if (!myChangedEvents.ContainsKey(name))
                myChangedEvents.Add(name, eventListener);
            else
                myChangedEvents[name] -= eventListener;
        }

        /// <summary>
        /// Adds a state equals listener to the given state parameter
        /// </summary>
        /// <param name="name">The name of the state to listen to</param>
        /// <param name="index">The index of the element to listen to</param>
        /// <param name="value">The value to invoke on</param>
        /// <param name="eventListener">The event to invoke on state change</param>
        public void AddStateEqualsEvent(string name, int index, object value, StateChangedEvent eventListener)
        {
            name = string.Format(ARRAY_FORMAT, name, index);

            Tuple<string, object> key = new Tuple<string, object>(name, value);

            if (!myStateEqualsEvents.ContainsKey(key))
                myStateEqualsEvents.Add(key, eventListener);
            else
                myStateEqualsEvents[key] += eventListener;
        }

        /// <summary>
        /// Removed a state equals listener to the given state parameter
        /// </summary>
        /// <param name="name">The name of the state to listen to</param>
        /// <param name="index">The index of the element to listen to</param>
        /// <param name="value">The value to invoke on</param>
        /// <param name="eventListener">The event to invoke on state change</param>
        public void RemoveStateEqualsEvent(string name, int index, object value, StateChangedEvent eventListener)
        {
            name = string.Format(ARRAY_FORMAT, name, index);

            Tuple<string, object> key = new Tuple<string, object>(name, value);

            if (!myStateEqualsEvents.ContainsKey(key))
                myStateEqualsEvents.Add(key, eventListener);
            else
                myStateEqualsEvents[key] -= eventListener;
        }

        /// <summary>
        /// Gets the state parameter with the given name
        /// </summary>
        /// <param name="name">The name of the parameter</param>
        /// <param name="serverSide">Whether or not this game state is not syncronized</param>
        /// <returns>The parameter with the given name</returns>
        public StateParameter GetParameter<T>(string name, bool serverSide = false)
        {
            // If we don't have that parameter, make it
            if (!myParameters.ContainsKey(name))
                myParameters.Add(name, StateParameter.Construct<T>(name, (T)Activator.CreateInstance(typeof(T)), serverSide));

            // Get the parameter
            return myParameters[name];
        }

        /// <summary>
        /// Private method used by all sets that handles setting a parameter
        /// </summary>
        /// <typeparam name="T">The type of the parameter to set</typeparam>
        /// <param name="name">The name of the parameter to set</param>
        /// <param name="value">The value to set</param>
        /// <param name="serverSide">True if this parameter should be NOT syncronized with peers</param>
        private void InternalSet<T>(string name, T value, bool serverSide)
        {
            // If the parameter does not exist, add it, otherwise update it
            if (!myParameters.ContainsKey(name))
                myParameters.Add(name, StateParameter.Construct(name, value, !serverSide));
            else
            {
                myParameters[name].SetValueInternal(value);
            }

            InvokeUpdated(myParameters[name]);
        }

        /// <summary>
        /// Updates a state parameter in this game state
        /// </summary>
        /// <param name="parameter"></param>
        public void UpdateParam(StateParameter parameter)
        {
            if (myParameters.ContainsKey(parameter.Name))
                InternalSet(parameter.Name, parameter.RawValue, !parameter.IsSynced);
            else
                myParameters.Add(parameter.Name, parameter);
        }

        /// <summary>
        /// Sets the given parameter to a value
        /// </summary>
        /// <param name="name">The name of the parameter to set</param>
        /// <param name="value">The value to set</param>
        /// <param name="serverSide">True if this parameter should be NOT syncronized with peers</param>
        public void Set<T>(string name, T value, bool serverSide = false)
        {
            if (string.IsNullOrWhiteSpace(name) || name[0] == '@')
                throw new ArgumentException("Invalid name, cannot be empty or start with @");

            if (!StateParameter.SUPPORTED_TYPES.ContainsKey(typeof(T)))
                throw new ArgumentException("Type " + typeof(T) + " is not a supported type");

            InternalSet(name, value, serverSide);
        }

        /// <summary>
        /// Sets the given parameter array slot to a value
        /// </summary>
        /// <param name="name">The name of the parameter to set</param>
        /// <param name="index">The index in the array</param>
        /// <param name="value">The value to set</param>
        /// <param name="serverSide">True if this parameter should be NOT syncronized with peers</param>
        public void Set<T>(string name, int index, T value, bool serverSide = false)
        {
            if (!StateParameter.SUPPORTED_TYPES.ContainsKey(typeof(T)))
                throw new ArgumentException("Type " + typeof(T) + " is not a supported type");

            InternalSet(string.Format(ARRAY_FORMAT, name, index), value, serverSide);
        }

        /// <summary>
        /// Private method used by all gets that handles getting a parameter.
        /// Note that if a parameter is not defined, the default for that type is returned
        /// </summary>
        /// <typeparam name="T">The type of the parameter to get</typeparam>
        /// <param name="name">The name of the parameter to get</param>
        /// <param name="serverSide">True if this parameter should be NOT syncronized with peers</param>
        private T GetValueInternal<T>(string name, bool serverSide = false)
        {
            // If we have that parameter, then get it
            if (myParameters.ContainsKey(name))
            {
                // Get the object value
                object value = myParameters[name].RawValue;

                // Verify type before returning
                if (value == null)
                    return default(T);
                else if (typeof(T).IsAssignableFrom(value.GetType()))
                    return (T)value;
                else if (Utils.CanChangeType(value, typeof(T)))
                    return (T)Convert.ChangeType(value, typeof(T));
                else
                    throw new InvalidCastException(string.Format("Cannot cast {0} to {1}", value.GetType().Name, typeof(T).Name));
            }
            // Otherwise make a default one
            else
            {
                // Add and return a default parameter of type T
                myParameters.Add(name, StateParameter.Construct(name, default(T), !serverSide));
                return myParameters[name].GetValueInternal<T>();
            }
        }
        
        /// <summary>
        /// Gets the parameter with the given name as a byte
        /// </summary>
        /// <param name="name">The name of the parameter to get</param>
        /// <returns>The parameter with the given name</returns>
        public byte GetValueByte(string name)
        {
            return GetValueInternal<byte>(name);
        }
        /// <summary>
        /// Gets the parameter in an array with the given name as a byte
        /// </summary>
        /// <param name="name">The name of the parameter to get</param>
        /// <param name="index">The index in the array</param>
        /// <returns>The parameter with the given name</returns>
        public byte GetValueByte(string name, int index)
        {
            return GetValueInternal<byte>(string.Format(ARRAY_FORMAT, name, index));
        }
        /// <summary>
        /// Gets the parameter with the given name as a character
        /// </summary>
        /// <param name="name">The name of the parameter to get</param>
        /// <returns>The parameter with the given name</returns>
        public char GetValueChar(string name)
        {
            return GetValueInternal<char>(name);
        }
        /// <summary>
        /// Gets the parameter in an array with the given name as a character
        /// </summary>
        /// <param name="name">The name of the parameter to get</param>
        /// <param name="index">The index in the array</param>
        /// <returns>The parameter with the given name</returns>
        public char GetValueChar(string name, int index)
        {
            return GetValueInternal<char>(string.Format(ARRAY_FORMAT, name, index));
        }
        /// <summary>
        /// Gets the parameter with the given name as a 16 bit integer
        /// </summary>
        /// <param name="name">The name of the parameter to get</param>
        /// <returns>The parameter with the given name</returns>
        public short GetValueShort(string name)
        {
            return GetValueInternal<short>(name);
        }
        /// <summary>
        /// Gets the parameter in an array with the given name as a 16 bit integer
        /// </summary>
        /// <param name="name">The name of the parameter to get</param>
        /// <param name="index">The index in the array</param>
        /// <returns>The parameter with the given name</returns>
        public short GetValueShort(string name, int index)
        {
            return GetValueInternal<short>(string.Format(ARRAY_FORMAT, name, index));
        }
        /// <summary>
        /// Gets the parameter with the given name as a 32 bit integer
        /// </summary>
        /// <param name="name">The name of the parameter to get</param>
        /// <returns>The parameter with the given name</returns>
        public int GetValueInt(string name)
        {
            return GetValueInternal<int>(name);
        }
        /// <summary>
        /// Gets the parameter in an array with the given name as a 32 bit integer
        /// </summary>
        /// <param name="name">The name of the parameter to get</param>
        /// <param name="index">The index in the array</param>
        /// <returns>The parameter with the given name</returns>
        public int GetValueInt(string name, int index)
        {
            return GetValueInternal<int>(string.Format(ARRAY_FORMAT, name, index));
        }
        /// <summary>
        /// Gets the parameter with the given name as a boolean
        /// </summary>
        /// <param name="name">The name of the parameter to get</param>
        /// <returns>The parameter with the given name</returns>
        public bool GetValueBool(string name)
        {
            return GetValueInternal<bool>(name);
        }
        /// <summary>
        /// Gets the parameter in an array with the given name as a boolean
        /// </summary>
        /// <param name="name">The name of the parameter to get</param>
        /// <param name="index">The index in the array</param>
        /// <returns>The parameter with the given name</returns>
        public bool GetValueBool(string name, int index)
        {
            return GetValueInternal<bool>(string.Format(ARRAY_FORMAT, name, index));
        }
        /// <summary>
        /// Gets the parameter with the given name as a card rank
        /// </summary>
        /// <param name="name">The name of the parameter to get</param>
        /// <returns>The parameter with the given name</returns>
        public CardRank GetValueCardRank(string name)
        {
            return GetValueInternal<CardRank>(name);
        }
        /// <summary>
        /// Gets the parameter in an array with the given name as a card rank
        /// </summary>
        /// <param name="name">The name of the parameter to get</param>
        /// <param name="index">The index of the element with the array</param>
        /// <returns>The parameter with the given name</returns>
        public CardRank GetValueCardRank(string name, int index)
        {
            return GetValueInternal<CardRank>(string.Format(ARRAY_FORMAT, name, index));
        }
        /// <summary>
        /// Gets the parameter with the given name as a card suit
        /// </summary>
        /// <param name="name">The name of the parameter to get</param>
        /// <returns>The parameter with the given name</returns>
        public CardSuit GetValueCardSuit(string name)
        {
            return GetValueInternal<CardSuit>(name);
        }
        /// <summary>
        /// Gets the parameter in an array with the given name as a card suit
        /// </summary>
        /// <param name="name">The name of the parameter to get</param>
        /// <param name="index">The index in the array</param>
        /// <returns>The parameter with the given name</returns>
        public CardSuit GetValueCardSuit(string name, int index)
        {
            return GetValueInternal<CardSuit>(string.Format(ARRAY_FORMAT, name, index));
        }
        /// <summary>
        /// Gets the parameter with the given name as a string
        /// </summary>
        /// <param name="name">The name of the parameter to get</param>
        /// <returns>The parameter with the given name</returns>
        public string GetValueString(string name)
        {
            return GetValueInternal<string>(name);
        }
        /// <summary>
        /// Gets the parameter in an array with the given name as a string
        /// </summary>
        /// <param name="name">The name of the parameter to get</param>
        /// <param name="index">The index in the array</param>
        /// <returns>The parameter with the given name</returns>
        public string GetValueString(string name, int index)
        {
            return GetValueInternal<string>(string.Format(ARRAY_FORMAT, name, index));
        }
        /// <summary>
        /// Gets the parameter with the given name as a playing card
        /// </summary>
        /// <param name="name">The name of the parameter to get</param>
        /// <returns>The parameter with the given name</returns>
        public PlayingCard GetValueCard(string name)
        {
            return GetValueInternal<PlayingCard>(name);
        }
        /// <summary>
        /// Gets the parameter in an array with the given name as a playing card
        /// </summary>
        /// <param name="name">The name of the parameter to get</param>
        /// <param name="index">The index in the array</param>
        /// <returns>The parameter with the given name</returns>
        public PlayingCard GetValueCard(string name, int index)
        {
            return GetValueInternal<PlayingCard>(string.Format(ARRAY_FORMAT, name, index));
        }
        /// <summary>
        /// Gets the parameter with the given name as a playing card collection
        /// </summary>
        /// <param name="name">The name of the parameter to get</param>
        /// <returns>The parameter with the given name</returns>
        public CardCollection GetValueCardCollection(string name)
        {
            return GetValueInternal<CardCollection>(name);
        }

        /// <summary>
        /// Gets the internal parameter collection for this game state
        /// </summary>
        /// <returns>A state parameter array</returns>
        public StateParameter[] GetParameterCollection()
        {
            return myParameters.Values.ToArray();
        }
        
        /// <summary>
        /// Checks if a state parameter is equal to a value
        /// </summary>
        /// <param name="name">The name of the parameter to check</param>
        /// <param name="value">The value to check against</param>
        /// <returns>True if the objects are equal, false if otherwise</returns>
        public bool Equals(string name, object value)
        {
            if (myParameters.ContainsKey(name))
                if (myParameters[name].RawValue == null)
                    return value == null;
                else
                    return myParameters[name].RawValue.Equals(value);
            else
                return value == null;
        }

        /// <summary>
        /// Encodes this game state to a network message
        /// </summary>
        /// <param name="msg">The message to encode to</param>
        public void Encode(NetOutgoingMessage msg)
        {
            StateParameter[] toTransfer = myParameters.Values.Where(x => x.IsSynced).ToArray();

            // Write the number of parameters
            msg.Write((int)toTransfer.Length);
            
            // Write each parameter
            for(int index = 0; index < toTransfer.Length; index ++)
                toTransfer[index].Encode(msg);
        }

        /// <summary>
        /// Decodes this game state from the given message
        /// </summary>
        /// <param name="msg">the message to read from</param>
        public void Decode(NetIncomingMessage msg)
        {
            // Read the number of parameters
            int numParams = msg.ReadInt32();

            // Read each parameter
            for (int index = 0; index < numParams; index++)
                StateParameter.Decode(msg, this);
        }

        /// <summary>
        /// Decodes a game state from the given message
        /// </summary>
        /// <param name="msg">the message to read from</param>
        /// <returns>A game state decoded from the message</returns>
        public static GameState CreateDecode(NetIncomingMessage msg)
        {
            GameState result = new GameState();
            result.Decode(msg);
            return result;
        }

        /// <summary>
        /// Invokes the updated parameter event with the given paramater
        /// </summary>
        /// <param name="stateParameter">The parameter that has been updated</param>
        internal void InvokeUpdated(StateParameter stateParameter)
        {
            // if the parameter is not null
            if (stateParameter != null)
            {
                // if we are not in silent sets mode and we have a state changed event, invoke it
                if (!SilentSets && OnStateChanged != null)
                    OnStateChanged(this, stateParameter);

                // If we have an un-silencable event listening on changes, invoke it
                if (OnStateChangedUnSilenceable != null)
                    OnStateChangedUnSilenceable(this, stateParameter);

                // If we have a changed listener on the parameters name, invoke it
                if (myChangedEvents.ContainsKey(stateParameter.Name))
                    myChangedEvents[stateParameter.Name](this, stateParameter);
                else
                {
                    // Here we check to see if we have any array listeners
                    string name = myChangedEvents.Keys.FirstOrDefault(X => stateParameter.Name.Substring(1, stateParameter.Name.Length - 1) == X);
                    // If we found one, invoke that shit
                    if (name != null) { myChangedEvents[name](this, stateParameter); }
                }
                
                // Get the key from the parameter name
                Tuple<string, object> key = myStateEqualsEvents.Keys.FirstOrDefault(X => X.Item1 == stateParameter.Name);

                // If we have a listener, invoke it
                if (key != null && stateParameter.RawValue.Equals(key.Item2))
                {
                    myStateEqualsEvents[key](this, stateParameter);
                }
            }

        }
    }
}
