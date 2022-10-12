using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwitcher : MonoBehaviour
{
    public GameColor color;
    [SerializeField] private string destroyingParticleID;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerController player))
        {
            ColorManager.Instance.ChangeColor(color);
            PoolingSystem.Instance.InstantiateAPS(destroyingParticleID, player.transform.position);
            Destroy(gameObject);
        }
    }
}
