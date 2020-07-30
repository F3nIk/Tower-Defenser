using TMPro;
using UnityEngine;

public class TextFading : MonoBehaviour
{

	private TextMeshProUGUI textMesh;
	private Color color;
	private float slowlySpeed = 0.4f;
    void Awake()
    {
		TryGetComponent(out textMesh);
		color = textMesh.color;

	}

    void Update()
    {
		if(!textMesh.text.Equals("") && textMesh.color.a > 0.25f)
		{
			textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, textMesh.color.a - Time.deltaTime * slowlySpeed);
			textMesh.transform.localScale =
			new Vector3(textMesh.transform.localScale.x - Time.deltaTime * slowlySpeed, textMesh.transform.localScale.y - Time.deltaTime * slowlySpeed);
		}																													
		else
		{
			textMesh.color = color;
			textMesh.text = "";
			textMesh.transform.localScale = new Vector3(1, 1);
		} 
    }
}
