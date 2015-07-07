using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameStateNames
{
	NONE,
	GAME_PAUSED,
	GAME_PLAYING,
	MAINMENU_MAIN,
	MAINMENU_OPTIONS,
	SPLASHSCREEN_MAIN,
	CUTSCENE_MAIN
}


public class GameStateManager : MonoBehaviour 
{
	public GameStateNames startingState;
	public List<GameState> states;

	private Dictionary<GameStateNames,GameState> m_StateMap;
	private GameStateNames m_CurrentState;

	// Use this for initialization
	void Start () 
	{
		ContextManager.Instance.SetStateManager( this );

		//-- initialize the state map
		m_StateMap = new Dictionary<GameStateNames, GameState>();

		//-- initialize the states
		for( int index = 0; index < states.Count; ++index )
		{
			//-- map the states to state names for easy lookup
			m_StateMap[states[index].stateName] = states[index];

			//-- disable the states gameobject
			states[index].gameObject.SetActive( false );

			//-- set its manager reference
			states[index].SetManager( this );
		}

		//-- go to the initial state
		ChangeState( startingState );
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void ChangeState( GameStateNames stateName )
	{
		//-- if not first time then exit the current state
		if( m_CurrentState != GameStateNames.NONE )
		{
			//-- allow the state to unload
			m_StateMap[m_CurrentState].StateExit();
			//-- disable the states gameobject
			m_StateMap[m_CurrentState].gameObject.SetActive( false );
		}

		//-- enable the new states gameobject
		m_StateMap[stateName].gameObject.SetActive( true );
		//-- call it the new states init function
		m_StateMap[stateName].StateEntry();

		//-- cache the new current state
		m_CurrentState = stateName;
	}

	public GameState GetCurrentState()
	{
		//-- retrieve the current state otherwise return null
		if( m_StateMap.ContainsKey(m_CurrentState) == true )
		{
			return m_StateMap[m_CurrentState];
		}
		else
		{
			return null;
		}
	}
}
