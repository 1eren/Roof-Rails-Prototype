using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private Animator anim;
    public Animator Anim => anim == null ? anim = GetComponent<Animator>() : anim;
    private void OnEnable()
    {
        LevelManager.Instance.LevelStartEvent.AddListener(Run);
        GameManager.Instance.WinEvent.AddListener(Dance);
    }
    private void OnDisable()
    {
        LevelManager.Instance.LevelStartEvent.RemoveListener(Run);
        GameManager.Instance.WinEvent.RemoveListener(Dance);
    }
    public void Hold()
    {
        Anim.SetTrigger(AnimationKeys.HOLD_ANIMATION);
    }
    public void Run()
    {
        Anim.SetTrigger(AnimationKeys.RUN_ANIMATION);
    }
    public void Dance()
    {
        Anim.SetTrigger(AnimationKeys.DANCE_ANIMATION);
    }
}
