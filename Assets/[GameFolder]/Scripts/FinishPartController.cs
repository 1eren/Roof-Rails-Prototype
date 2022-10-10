using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPartController : MonoBehaviour
{
    public int multiplier;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerController player))
        {
            GameManager.Instance.WinEvent.Invoke();
        }
    }
}
