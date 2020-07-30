using UnityEngine;

/// <summary>
/// Class that contains data about every node in a game
/// </summary>
public class Node : MonoBehaviour
{
	public Color hoverColor;
	public bool IsAvailable = true;
	public Vector3 position;

	private Color mainColor = Color.white;

	private void OnMouseEnter()
	{
		if(gameObject.TryGetComponent<Renderer>(out var renderer)) renderer.material.color = hoverColor;
	}

	private void OnMouseExit()
	{
		if(gameObject.TryGetComponent<Renderer>(out var renderer)) renderer.material.color = mainColor;

	}

	private void OnMouseDown()
	{
		position = transform.position;
		GUIManager.instance.OpenTowersShop(gameObject);
	}
}
