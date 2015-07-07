using UnityEngine;
using System.Collections;

public class State_SplashScreen_Main : GameState
{
	private float accumulator = 0;

	public override void OnGameStateEntry()
	{
		
	}
	
	public override void OnGameStateExit()
	{
		
	}
	
	void Update()
	{
		accumulator += Time.deltaTime;

		if( accumulator > 4.0f )
		{
			ContextManager.Instance.SetContext( ContextNames.MAINMENU );
		}
	}
}
