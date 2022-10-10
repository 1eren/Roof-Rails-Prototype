using UnityEngine;

public class LavaController : MonoBehaviour
{
    public float amount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            PlayerStickController stick = player.GetComponentInChildren<PlayerStickController>();
            stick.DecreaseScale(amount);
        }
    }
}
