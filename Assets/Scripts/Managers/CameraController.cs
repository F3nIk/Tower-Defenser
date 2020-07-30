using UnityEngine;

public class CameraController : MonoBehaviour
{
	[Header("Camera Attributes")]
	public float panSpeed = 10f;
	public float scrollSpeed = 500f;
	public float panBorderThickness = 10f;

	public float minX;
	public float maxX;

	public float minY;
	public float maxY;

	public float minZ;
	public float maxZ;


	private Vector3 defaultPosition;

	private void Awake()
	{
		defaultPosition = transform.position;
	}
	void Update()
    {
	/*
		if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
			transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);

		if(Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
			transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);

		if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
			transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);

		if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
			transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
			*/


		if (Input.GetKey("w") )
			transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);

		if (Input.GetKey("s") )
			transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);

		if (Input.GetKey("d") )
			transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);

		if (Input.GetKey("a") )
			transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			transform.position = defaultPosition;

			//TODO: reset rotation of the camera 
		}


		//Scroll camera scale

		float scroll = Input.GetAxis("Mouse ScrollWheel");

		Vector3 pos = transform.position;

		pos.y += scroll * scrollSpeed * Time.deltaTime;

		

		pos.y = Mathf.Clamp(pos.y, minY,maxY);
		pos.x = Mathf.Clamp(transform.position.x, minX, maxX);
		pos.z = Mathf.Clamp(transform.position.z, maxZ, minZ);

		transform.position = pos;
    }

}
