﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {

    #region singleton
    static EventManager _instance = null;

    public static EventManager Instance {
        get {
            if (_instance == null)
                _instance = FindObjectOfType(typeof(EventManager)) as EventManager;

            return _instance;
        }
    }
    #endregion

    public delegate void EventDelegate<T>(T e) where T : GameEvent;

    #region private members
    [SerializeField] private bool _limitQueueProcessing = false;

    [SerializeField] private float _queueProcessTime = 0.0f;

    private Dictionary<System.Type, EventDelegate> _delegates = new Dictionary<System.Type, EventDelegate>();

    private Dictionary<System.Delegate, EventDelegate> _delegateLookup = new Dictionary<System.Delegate, EventDelegate>();

    private Dictionary<System.Delegate, System.Delegate> _onceLookups = new Dictionary<System.Delegate, System.Delegate>();

    private Queue _eventQueue = new Queue();

    private delegate void EventDelegate(GameEvent e);
    #endregion

    #region public methods
    public void AddListener<T>(EventDelegate<T> eventDelegate) where T : GameEvent {
        AddDelegate<T>(eventDelegate);
    }

    public void AddListenerOnce<T>(EventDelegate<T> eventDelegate) where T : GameEvent {
        EventDelegate result = AddDelegate<T>(eventDelegate);

        if (result != null)
            _onceLookups[result] = eventDelegate;
    }

    public void RemoveListener<T>(EventDelegate<T> eventDelegate) where T : GameEvent {
        if (_delegateLookup.Count == 0)
            return;

        if (_delegateLookup.TryGetValue(eventDelegate, out EventDelegate internalDelegate)) {
            if (_delegates.TryGetValue(typeof(T), out EventDelegate tempDelegate)) {
                tempDelegate -= internalDelegate;

                if (tempDelegate == null)
                    _delegates.Remove(typeof(T));
                else
                    _delegates[typeof(T)] = tempDelegate;
            }

            _delegateLookup.Remove(eventDelegate);
        }
    }

    public void RemoveAll() {
        _delegates.Clear();
        _delegateLookup.Clear();
        _onceLookups.Clear();
    }

    public bool HasListener<T>(EventDelegate<T> eventDelegate) where T : GameEvent {
        return _delegateLookup.ContainsKey(eventDelegate);
    }

    public void TriggerEvent(GameEvent e) {
#if UNITY_EDITOR
        Debug.Log("GameEvent " + e.ToString() + " triggered.");
#endif

        if (_delegates.TryGetValue(e.GetType(), out EventDelegate eventDelegate)) {
            eventDelegate.Invoke(e);

            // Remove listeners which should only be called once
            foreach (EventDelegate k in _delegates[e.GetType()].GetInvocationList())
                if (_onceLookups.ContainsKey(k)) {
                    _delegates[e.GetType()] -= k;

                    if (_delegates[e.GetType()] == null)
                        _delegates.Remove(e.GetType());

                    _delegateLookup.Remove(_onceLookups[k]);
                    _onceLookups.Remove(k);
                }

        } else {
            Debug.LogWarning("GameEvent: " + e.GetType() + " has no listeners");
        }
    }

    public bool QueueEvent(GameEvent eventToQueue) {
        if (!_delegates.ContainsKey(eventToQueue.GetType())) {
            Debug.LogWarning("EventManager: QueueEvent failed due to no listeners for event: " + eventToQueue.GetType());
            return false;
        }

        _eventQueue.Enqueue(eventToQueue);
        return true;
    }
    #endregion

    #region private methods
    private EventDelegate AddDelegate<T>(EventDelegate<T> eventDelegate) where T : GameEvent {
        // Check if this delegate is already registered
        if (_delegateLookup.ContainsKey(eventDelegate))
            return null;

        // Create a new non-generic delegate which calls our generic one. This is the delegate we actually invoke.
        EventDelegate internalDelegate = (e) => eventDelegate((T)e);
        _delegateLookup[eventDelegate] = internalDelegate;

        if (_delegates.TryGetValue(typeof(T), out EventDelegate tempDelegate))
            _delegates[typeof(T)] = tempDelegate += internalDelegate;
        else
            _delegates[typeof(T)] = internalDelegate;

        return internalDelegate;
    }

    private void Awake() {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);

        } else {
            DestroyImmediate(gameObject);
        }
    }

    //Every update cycle the queue is processed, if the queue processing is limited,
    //a maximum processing time per update can be set after which the events will have
    //to be processed next update loop.
    private void Update() {
        float timer = 0.0f;
        while (_eventQueue.Count > 0) {
            if (_limitQueueProcessing)
                if (timer > _queueProcessTime)
                    return;

            GameEvent dequeuedEvent = _eventQueue.Dequeue() as GameEvent;
            TriggerEvent(dequeuedEvent);

            if (_limitQueueProcessing)
                timer += Time.deltaTime;
        }
    }

    private void OnApplicationQuit() {
        RemoveAll();
        _eventQueue.Clear();
        _instance = null;
    }
    #endregion
}