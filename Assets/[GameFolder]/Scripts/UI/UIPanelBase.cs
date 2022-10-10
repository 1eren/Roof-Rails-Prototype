using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelBase : MonoBehaviour
{
    protected void HidePanel()
    {
        gameObject.SetActive(false);
    }
    protected void ShowPanel()
    {
        gameObject.SetActive(true);
    }
}
