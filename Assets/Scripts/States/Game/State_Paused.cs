﻿using UnityEngine;
using System.Collections;

public class State_Paused : GameState
{
	public override void OnGameStateEntry()
	{
		
	}
	
	public override void OnGameStateExit()
	{
		
	}

	void Update()
	{
		if( Input.GetKeyUp( KeyCode.P ) == true )
		{
			GetManager().ChangeState( GameStateNames.GAME_PLAYING );
		}
	}
}
