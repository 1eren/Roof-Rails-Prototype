using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSidedStick : MonoBehaviour
{
    [OnValueChanged("SetSticksAttributes")]
    [SerializeField]private float distanceBetween=1f, scale =1;


    //distance and scale will be updated instantly when the distance& scale changes on inspector
    private void SetSticksAttributes()
    {
        foreach (Transform item in transform)
        {
            if (item.GetSiblingIndex() % 2 == 0)
                item.localPosition = new Vector3(distanceBetween, item.localPosition.y, item.localPosition.z);
            else
                item.localPosition = new Vector3(-distanceBetween, item.localPosition.y, item.localPosition.z);

            item.localScale = new Vector3(scale, item.localScale.y, item.localScale.z); 
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out CharacterAnimationController player))
        {
            player.Hold();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out CharacterAnimationController player))
        {
            player.Run();
        }
    }
}
