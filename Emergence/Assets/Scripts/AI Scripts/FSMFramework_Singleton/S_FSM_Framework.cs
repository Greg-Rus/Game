﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//public enum Transition{
//	poiInSight, poiLost, poiInFireingRange, foundClosestWaypoint
	
//}

//public enum StateID{
//	Idle, StartPatrol, Patroling, Chasing, Attacking, Dead
//}


public abstract class S_FSMState
{
	protected Dictionary<Transition, StateID> map = new Dictionary<Transition, StateID> ();
	protected StateID stateID;
	public StateID ID { get { return stateID; } }
	
	public void AddTransition (Transition newTransition, StateID newId)
	{
		if (map.ContainsKey (newTransition)) 
		{
			Debug.LogError("Error: Transition " + newTransition + " already in map.");
			return;
		}
		map.Add (newTransition, newId);
		Debug.Log ("Added transition: " + newTransition);
	}
	
	public void DeleteTransition (Transition oldTransition)
	{
		if (map.ContainsKey (oldTransition)) 
		{
			map.Remove(oldTransition);
			return;
		}
		Debug.LogError("Error: Transition " + oldTransition + " not present in map.");
	}
	
	public StateID GetNextState(Transition trans)
	{
		return map[trans];
	}
	//public abstract void SetNPC(GameObject NPC);
	
	public abstract void Reason(GameObject PoI, GameObject NPC);
	
	public abstract void Act (GameObject PoI, GameObject NPC);
	
	public virtual void DoBeforeEntering() { }
	
	public virtual void DoBeforeExiting() { }
}

public class S_FSMSystem
{
	private List<S_FSMState> states;
	
	private StateID currentStateID;
	public StateID CurrentStateID { get { return currentStateID; } }
	
	private S_FSMState currentState;
	public S_FSMState CurrentState {get {return currentState;}}
	
	public S_FSMSystem()
	{
		states = new List<S_FSMState> ();
	}
	
	public void AddState(S_FSMState newState)
	{
		if (states.Count == 0) {
			states.Add(newState);
			currentState = newState;
			currentStateID = newState.ID;
			return;
		}
		
		foreach (S_FSMState knownState in states)
		{
			if(knownState.ID == newState.ID)
			{
				Debug.LogError("Error: State "+ newState.ID + " already in FSM");
			}
		}
		states.Add(newState);
	}
	
	public void DeleteState(StateID id)
	{
		foreach (S_FSMState knowStaste in states) 
		{
			if(knowStaste.ID == id)
			{
				states.Remove(knowStaste);
				return;
			}
		}
		Debug.LogError ("No state with ID: " + id + " present on state list");
	}
	
	public void PerformTransition (Transition trans)
	{
		StateID id = currentState.GetNextState (trans);
		currentStateID = id;
		
		foreach (S_FSMState state in states) 
		{
			if(state.ID == currentStateID)
			{
				currentState  = state;
				currentState.DoBeforeEntering();
				break;
			}
		}
	}
	
}