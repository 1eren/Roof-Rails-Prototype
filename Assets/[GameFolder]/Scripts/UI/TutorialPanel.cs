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
        if (LevelManager.Instance == null) return;
        LevelManager.Instance.LevelStartEvent.RemoveListener(HidePanel);
    }
}
