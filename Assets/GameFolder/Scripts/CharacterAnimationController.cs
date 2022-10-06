using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private Animator anim;
    public Animator Anim => anim == null ? anim = GetComponent<Animator>() : anim;
    private void OnEnable()
    {
        if (LevelManager.Instance == null) return;
        LevelManager.Instance.OnLevelStart.AddListener(()=>Anim.SetTrigger("HoldingRun"));
    }
    private void OnDisable()
    {
        if (LevelManager.Instance == null) return;
        LevelManager.Instance.OnLevelStart.RemoveListener(()=>Anim.SetTrigger("HoldingRun"));
    }

}
