using UnityEngine;

public class LavaController : MonoBehaviour
{
    public float amount;

    private float time;
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            PlayerStickController stick = player.GetComponentInChildren<PlayerStickController>();
            time += Time.deltaTime;
            if (time>0.2f)
            {
                time = 0;
                stick.DecreaseScale(amount);
            }
        }
    }
}
