using UnityEngine;

public class Coin : MonoBehaviour
{
	[SerializeField]
	private int value;

	private bool isTaken = false;
	private float timer = 0;
	[SerializeField]
	private float timeToAutoCollect;
	[SerializeField]
	private float animationSpeed;
	[SerializeField]
	private float maxYtoDestroy;

	private void Update()
	{
		timer += Time.deltaTime;
		if (isTaken || timer >= timeToAutoCollect) transform.Translate((Vector3.up + Vector3.left) * animationSpeed * Time.deltaTime, Space.World);

		if (transform.position.y >= maxYtoDestroy)
		{
			PlayerManager.instance.AddMoney(value);
			Destroy(gameObject);
		}
	}

	private void OnMouseEnter()
	{
		isTaken = true;
	}

}
