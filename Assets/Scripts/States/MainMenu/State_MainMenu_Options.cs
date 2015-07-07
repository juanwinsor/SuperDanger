using UnityEngine;
using System.Collections;

public class State_MainMenu_Options : GameState
{
	public override void OnGameStateEntry()
	{
		
	}
	
	public override void OnGameStateExit()
	{
		
	}
	
	void Update()
	{
		if( Input.GetKeyUp( KeyCode.Escape ) == true )
		{
			GetManager().ChangeState( GameStateNames.MAINMENU_MAIN );
		}
	}
}