using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [ReadOnly]public bool isDeath;

    public bool isHolding;
    private void OnEnable()
    {
        if (LevelManager.Instance == null)
            return;
        EventManager.OnFailEvent.AddListener(Fall);
    }
    private void OnDisable()
    {
        if (LevelManager.Instance == null)
            return;
        EventManager.OnFailEvent.RemoveListener(Fall);
    }
    public void Hold()
    {
        isHolding = true;
    }
    public void Run()
    {
        GetComponent<CharacterAnimationController>().Run();
        isHolding = false;
    }
    public void Fall()
    {
        isDeath = true;
       
        Debug.Log("faiiil");
    }
}
