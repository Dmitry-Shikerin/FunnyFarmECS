﻿using System.Collections.Generic;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.MyUiFramework.Dictionaries
{
    public class SerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField, HideInInspector] private List<TKey> _keys = new List<TKey>();
        [SerializeField, HideInInspector] private List<TValue> _values = new List<TValue>();

        public void OnAfterDeserialize()
        {
            Clear();
            
            for (int i = 0; i < _keys.Count && i < _values.Count; i++) 
                this[_keys[i]] = _values[i];
        }

        public void OnBeforeSerialize()
        {
            _keys.Clear();
            _values.Clear();

            foreach (KeyValuePair<TKey, TValue> item in this)
            {
                _keys.Add(item.Key);
                _values.Add(item.Value);
            }
        }
    }
}