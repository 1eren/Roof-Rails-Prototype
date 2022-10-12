using UnityEngine;
using DG.Tweening;

public class PlayerStickController : MonoBehaviour, ISliceable, IThrowable, IColorable
{
	private Tween resetPositionTween, scaleTween;
	[SerializeField] private float minimumStickScale = 0.1f;

	public float StickSize => transform.localScale.x;


	[SerializeField] private MeshRenderer mesh;
	public MeshRenderer Mesh => mesh;

	private void OnEnable()
	{
		GameManager.Instance.FallEvent.AddListener(Drop);
		GameManager.Instance.WinEvent.AddListener(Drop);
	}
	private void OnDisable()
	{
		if (LevelManager.Instance == null) return;
		GameManager.Instance.FallEvent.RemoveListener(Drop);
		GameManager.Instance.WinEvent.RemoveListener(Drop);
	}
	public void IncreaseScale(float amount)
	{
		scaleTween.Complete(); // complete tween if is still scaling
		scaleTween = transform.DOScale(transform.localScale + amount * Vector3.right, 0.1f);
	}
	public void DecreaseScale(float amount)
	{
		transform.localScale -= Vector3.right * amount;
		if (StickSize <= 0.1f)
		{
			GameManager.Instance.DeathEvent.Invoke();
			return;
		}
		Vector3 pos = transform.position;
		float slicingPoint = StickSize / 2f - amount;
		CreateNewPart(amount, new Vector3(pos.x + slicingPoint, pos.y, pos.z));
		CreateNewPart(amount, new Vector3(pos.x - slicingPoint, pos.y, pos.z));
	}
	public void Slice(Vector3 direction)
	{
		scaleTween.Kill();
		//finding difference and required scale for slicing
		float amount = (StickSize / 2) - Mathf.Abs(direction.x - transform.position.x);

		if (StickSize - amount < minimumStickScale)//if the stick will be very small we stopping at a small value
		{
			transform.localScale = new Vector3(minimumStickScale, transform.localScale.y, transform.localScale.z);
			return;
		}
		//find out if the obstacle is on the right or left 
		Vector3 roundedValue = direction.x > transform.position.x ? Vector3.right : Vector3.left;

		transform.localScale -= Vector3.right * amount;
		transform.position -= roundedValue * amount / 2;
		CreateNewPart(amount, new Vector3(direction.x, transform.position.y, transform.position.z));
	}
	public void CreateNewPart(float xScale, Vector3 hitPoint)
	{
		if (resetPositionTween != null)//kill tween if this function triggered twice
			resetPositionTween.Kill();

		Vector3 creatingPos = hitPoint;

		creatingPos += (hitPoint.x > transform.position.x ? Vector3.right * xScale / 2 : Vector3.left * xScale / 2);//set object  position by cutting pos
		GameObject newPart = PoolingSystem.Instance.InstantiateAPS("Stick", creatingPos);
		newPart.transform.localScale = new Vector3(xScale, newPart.transform.localScale.y, newPart.transform.localScale.z);

		//reset position after small delay
		resetPositionTween = transform.DOLocalMoveX(0, .5f).SetDelay(.5f);
		newPart.GetComponent<IThrowable>().Throw(Vector3.Normalize(hitPoint - transform.position + Vector3.up) * 2f);
	}

	private void Drop()
	{
		transform.parent = null;
		Throw(new Vector3(0.3f, 50, Random.Range(-4f, 4f)));
	}
	public void Throw(Vector3 force)
	{
		if (GetComponent<Rigidbody>() == null)
			gameObject.AddComponent<Rigidbody>();

		Rigidbody rb = GetComponent<Rigidbody>();
		rb.AddForce(force);
		GetComponent<CapsuleCollider>().height *= 0.95f;
	}
}
