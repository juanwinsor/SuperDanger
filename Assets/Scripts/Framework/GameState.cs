using UnityEngine;
using System.Collections;



public class GameState : MonoBehaviour
{
	public GameStateNames stateName;

	private GameStateManager m_StateManager = null;


	//-- callbacks for derived states
	public virtual void OnGameStateEntry(){}
	public virtual void OnGameStateExit(){}


	public void StateEntry()
	{
		//-- callback down to derived class
		OnGameStateEntry();
	}

	public void StateExit()
	{
		//-- callback down to derived class
		OnGameStateExit();
	}


	public void SetManager( GameStateManager stateManager )
	{
		m_StateManager = stateManager;
	}

	public GameStateManager GetManager()
	{
		return m_StateManager;
	}
}
