using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanel : MonoBehaviour
{
    private void OnEnable()
    {
        if (LevelManager.Instance == null)
            return;
        LevelManager.Instance.OnLevelStart.AddListener(HidePanel);

    }
    private void OnDisable()
    {
        if (LevelManager.Instance == null)
            return;
        LevelManager.Instance.OnLevelStart.RemoveListener(HidePanel);
    }

    private void HidePanel()
    {
        gameObject.SetActive(false);
    }
    private void ShowPanel()
    {
        gameObject.SetActive(true);
    }
}
