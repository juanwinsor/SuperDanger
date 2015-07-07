using UnityEngine;
using System.Collections;

public class Singleton<T> where T : Singleton<T>, new()
{
	private static T m_Instance;

	public static T Instance
	{
		get
		{
			if( m_Instance == null )
			{
				m_Instance = new T();
				m_Instance.SingletonInitialize();
			}

			return m_Instance;
		}
	}

	public static void UnloadInstance()
	{
		m_Instance.SingletonDeinitialize();
		m_Instance = default(T);
	}

	public virtual void SingletonInitialize(){}
	public virtual void SingletonDeinitialize(){}
}
