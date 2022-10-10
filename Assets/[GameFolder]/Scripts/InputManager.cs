using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;



public class InputManager : MonoBehaviour
{
	public UnityEvent OnTapDown = new UnityEvent();
	public UnityEvent OnTapUp = new UnityEvent();
	[ReadOnly] public bool isDown;


    private Vector3 startPosition;
    private Vector3 lastPosition;

    private Vector2 Movement
    {
        get
        {
            if (!isDown) return Vector2.zero;
            return Input.mousePosition - startPosition;
        }
    }

    private Vector2 Delta
    {
        get
        {
            var currentPosition = Input.mousePosition;
            var delta = currentPosition - lastPosition;
            lastPosition = currentPosition;
            return delta;
        }
    }


    public void Tick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnTapDown.Invoke();
            isDown = true;
            startPosition = Input.mousePosition;
            lastPosition = startPosition;
        }

        if (Input.GetMouseButton(0))
        {
            SendSlide();
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDown = false;
        }
    }

    public SlideData SendSlide()
    {
        return new SlideData()
        {
            movement = Movement,
            normalizedMovement = Vector3.Normalize(Movement),
            delta = Delta
        };
    }

}