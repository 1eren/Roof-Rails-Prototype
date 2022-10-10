using Cinemachine;
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
        GameManager.Instance.FallEvent.AddListener(Fall);
    }
    private void OnDisable()
    {
        GameManager.Instance.FallEvent.RemoveListener(Fall);
    }
    public void Hold()
    {
        GetComponent<CharacterAnimationController>().Hold();
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
        FindObjectOfType<CinemachineVirtualCamera>().Follow = null;
    }
}
