using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// Manages custom events with custom arguments/parameters.
public class EventManager {

    // Save all event handlers
	private static Dictionary<string,EventHandler<MyEventArgs>> subscribers;

    // Constructor
    public EventManager(){
        subscribers = new Dictionary<string,EventHandler<MyEventArgs>>();
    }

	// EventManager.AddEventHandler(EVENT_NAME, new EventHandler<MyEventArgs>(METHOD_NAME));
	// > EVENT_NAME = Any string variable pertaining to an event, "OnTutorialComplete"
	// > METHOD_NAME = Context of the name of the method to be used when the event is triggered, public void Method(object sender, MyEventArgs args){}
	public void AddEventHandler(string eventName, EventHandler<MyEventArgs> method){
		if ( subscribers.ContainsKey(eventName) ){
			subscribers[eventName] += method;
		} else {
			subscribers.Add(eventName,method);
		}
	}

	// EventManager.RemoveEventHandler(EVENT_NAME, new EventHandler<MyEventArgs>(METHOD_NAME));
	// > EVENT_NAME = Any string variable pertaining to an event
	// > METHOD_NAME = Context of the name of the method to be removed
	public void RemoveEventHandler(string eventName, EventHandler<MyEventArgs> method){
		if ( subscribers.ContainsKey(eventName) ){
			subscribers[eventName] -= method;
			if ( subscribers[eventName] == null ) subscribers.Remove(eventName);
		}
	}

	// EventManager.TriggerEvent("Hello",EventArgs.Empty);
	// EventManager.TriggerEvent("Hello",new MyEventArgs(ARRAY_LIST));
	// > ARRAY_LIST = An arraylist variable that is used to send data to events
	public void TriggerEvent(string eventName, MyEventArgs args){
		if ( subscribers.ContainsKey(eventName) ) subscribers[eventName].Invoke(this,args);
	}
}

// You can modify variables used in MyEventArgs
public class MyEventArgs : EventArgs {
	public ArrayList args;      // List of object arguments

    // Constructor
	public MyEventArgs(ArrayList a){
		args = a;
	}
}