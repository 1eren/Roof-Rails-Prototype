using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanel : UIPanelBase
{
    private void OnEnable()
    {
        LevelManager.Instance.LevelStartEvent.AddListener(HidePanel);
    }
    private void OnDisable()
    {
        LevelManager.Instance.LevelStartEvent.RemoveListener(HidePanel);
    }
}
