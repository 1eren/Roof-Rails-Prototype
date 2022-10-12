using UnityEngine;

public class LavaController : MonoBehaviour
{
    public float amount;

    [SerializeField]private float destroyingDestiny = 0.3f;
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            PlayerStickController stick = player.GetComponentInChildren<PlayerStickController>();
            destroyingDestiny += Time.deltaTime;
            if (destroyingDestiny > 0.3f)
            {
                destroyingDestiny = 0;
                stick.DecreaseScale(amount);
            }
        }
    }
}
