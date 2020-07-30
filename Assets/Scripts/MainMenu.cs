using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI versionLabel;
	[SerializeField]
	private Button loadGame;

    void Start()
    {
		versionLabel.text ="Version " + "1";
		OnLoadIsAvailable(loadGame);
    }

	public void NewGame()
	{
		SceneManager.LoadScene(1,LoadSceneMode.Single);
		SaveSystem.IsLoading = false;
	}

	public void LoadGame()
	{
		SceneManager.LoadScene(1, LoadSceneMode.Single);
		SaveSystem.IsLoading = true;
	}

	public void OnLoadIsAvailable(Button button)
	{
		if (SaveSystem.HasSave()) button.interactable = true;
		else button.interactable = false;
	}

	public void Exit()
	{
		Application.Quit();
	}
}
