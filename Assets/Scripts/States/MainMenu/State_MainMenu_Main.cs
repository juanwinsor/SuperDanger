using UnityEngine;
using System.Collections;

public class State_MainMenu_Main : GameState
{
	public override void OnGameStateEntry()
	{
		
	}
	
	public override void OnGameStateExit()
	{
		
	}
	
	void Update()
	{
		if( Input.GetKeyUp( KeyCode.O ) == true )
		{
			GetManager().ChangeState( GameStateNames.MAINMENU_OPTIONS );
		}
		if( Input.GetKeyUp( KeyCode.Space ) == true )
		{
			ContextManager.Instance.SetContext( ContextNames.GAME );
		}
	}
}
