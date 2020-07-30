using UnityEngine;

/// <summary>
/// Class-container for waypoints. Necessary for orientation of enemies in space.
/// </summary>
public class Waypoints : MonoBehaviour
{
	public static Transform[] points;

	private void Awake()
	{
		points = new Transform[transform.childCount];
		for (int i = 0; i < points.Length; i++)
		{
			points[i] = transform.GetChild(i);
		}
	}
}
