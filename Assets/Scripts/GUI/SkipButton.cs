using UnityEngine;
using UnityEngine.UI;

public class SkipButton : MonoBehaviour
{
 

    private void EnableButton()
    {
		if(TryGetComponent<Button>(out var button)) button.enabled = true;
    }
	private void OnEnable()
	{
		Invoke("EnableButton", 0.5f);
	}
	private void OnDisable()
	{
		if (TryGetComponent<Button>(out var button)) button.enabled = false;
	}
}
