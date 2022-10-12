using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private Animator anim;
    public Animator Anim => anim == null ? anim = GetComponent<Animator>() : anim;
    private void OnEnable()
    {
        LevelManager.Instance.LevelStartEvent.AddListener(Run);
        GameManager.Instance.GameWinEvent.AddListener(Dance);
        EventManager.EnteredRail.AddListener((x) => Hold());
        EventManager.ExitedRail.AddListener((x) => Run());
    }
    private void OnDisable()
    {
        if (LevelManager.Instance == null) return;
        LevelManager.Instance.LevelStartEvent.RemoveListener(Run);
        GameManager.Instance.GameWinEvent.RemoveListener(Dance);
        EventManager.EnteredRail.RemoveListener((x)=> Hold());
        EventManager.ExitedRail.RemoveListener((x)=> Run());
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
