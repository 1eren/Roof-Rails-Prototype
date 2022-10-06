using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanel : MonoBehaviour
{
    private bool isclickedToPlay;

    private void Update()
    {
        if (!isclickedToPlay && Input.GetMouseButtonUp(0))
            StartGame();
    }
    private void StartGame()
    {
        gameObject.SetActive(false);
        isclickedToPlay = true;
        LevelManager.Instance.OnLevelStart.Invoke();
    }
}
