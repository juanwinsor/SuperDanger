using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ContextNames
{
	NONE,
	GAME,
	MAINMENU,
	SPLASHSCREEN
}

public class ContextManager : Singleton<ContextManager>
{
	private GameStateManager m_StateManager = null;

	private Dictionary<ContextNames,string> m_Contexts;

	public override void SingletonInitialize()
	{
		m_Contexts = new Dictionary<ContextNames, string>();

		//-- map context names to scenes
		m_Contexts[ContextNames.GAME] = "Game";
		m_Contexts[ContextNames.MAINMENU] = "MainMenu";
	}

	public override void SingletonDeinitialize()
	{

	}

	public void SetStateManager( GameStateManager stateManager )
	{
		m_StateManager = stateManager;
	}

	public GameStateManager GetStateManager()
	{
		return m_StateManager;
	}

	public void SetContext( ContextNames contextName )
	{
		if( m_Contexts.ContainsKey(contextName) == true )
		{
			Application.LoadLevel( m_Contexts[contextName] );
		}
	}
}
