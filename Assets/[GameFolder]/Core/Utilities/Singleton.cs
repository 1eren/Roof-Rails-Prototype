using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

	public static T Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (T)FindObjectOfType(typeof(T));
				if (_instance == null)
				{
					_instance = (new GameObject(typeof(T).Name)).AddComponent<T>();
				}
				//DontDestroyOnLoad(m_Instance.gameObject);
			}
			return _instance;
		}
	}

	void OnDisable()
	{
		if (!this.gameObject.scene.isLoaded) return;
		// Instantiate objects here
	}
}
