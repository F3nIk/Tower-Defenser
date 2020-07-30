using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Class that contains all GUI elements and methods for working with them.
/// </summary>
public class GUIManager : MonoBehaviour
{
	public static GUIManager instance;
	private GUIManager()
	{
		instance = this;
	}

	[Header("GUI")]
	[SerializeField]
	private TextMeshProUGUI announceNewWave;
	[SerializeField]
	private TextMeshProUGUI warningMessageBox;
	[SerializeField]
	private Image[] Hearts;
	[SerializeField]
	private GameObject gameOverPanel;
	[SerializeField]
	private Transform Map;
	[SerializeField]
	private GameObject waveIsReady;
	[SerializeField]
	private GameObject pauseWindow;

	[Header("Resources Labels")]
	[SerializeField]
	private TextMeshProUGUI moneyLabel;
	[SerializeField]
	private TextMeshProUGUI soulDustLabel;

	[Header("Tower Shop Window")]
	[SerializeField]
	private GameObject towersShop;

	[Header("Tower Upgrade Window")]
	[SerializeField]
	private GameObject towerUpgrade;
	[SerializeField]
	private Image towerIcon;
	[SerializeField]
	private TextMeshProUGUI towerDescription;
	[SerializeField]
	private TextMeshProUGUI towerUpgradeCost;
	[SerializeField]
	private TextMeshProUGUI towerSellCost;

	[Header("Statistics Window")]
	[SerializeField]
	private GameObject StatisticsWindow;
	[SerializeField]
	private TextMeshProUGUI totalEnemiesKilled;
	[SerializeField]
	private TextMeshProUGUI totalBossesKilled;
	[SerializeField]
	private TextMeshProUGUI totalGoldEarned;
	[SerializeField]
	private TextMeshProUGUI highestWave;
	//More objects in a future

	private Tower currentTower;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) Pause();
	}

	public void MoneyLabelUpdate(int money)
	{
		moneyLabel.text = money.ToString();
	}

	public void OpenTowersShop(GameObject node)
	{
		ChangeRaycastMap();
		towerUpgrade.SetActive(false);
		towersShop.SetActive(!towersShop.activeSelf);
		BuildManager.instance.currentNode = node;
	}
	public void CloseTowerShop()
	{
		ChangeRaycastMap();
		towersShop.SetActive(false);
	}

	public void AnnounceNewWave(int waveCount)
	{
		announceNewWave.text = "Wave " + waveCount;
		waveIsReady.SetActive(false);
	}

	public void NewWarningMessage(string message)
	{
		warningMessageBox.text = message;
	}

	public void OpenStatistic()
	{
		StatisticsWindow.SetActive(!StatisticsWindow.activeSelf);
		PlayerManager.instance.GetStatistics(out var enemy,out var bosses,out var gold,out var wave);
		totalEnemiesKilled.text = "Total enemies killed: " + enemy.ToString();
		totalBossesKilled.text = "Total bosses killed: " + bosses.ToString();
		totalGoldEarned.text = "Total gold earned: " + gold.ToString();
		highestWave.text ="Highest wave: " + wave.ToString();
	}

	public void BreakHeart(int countOfHearts)
	{
			Hearts[countOfHearts].enabled = false;		
	}

	public void GameOver()
	{
		ChangeRaycastMap();
		gameOverPanel.SetActive(true);
	}

	private void ChangeRaycastMap()
	{

		if (Map.GetChild(0).gameObject.layer == 0) foreach (Transform child in Map) child.gameObject.layer = 2;
		else if (Map.GetChild(0).gameObject.layer == 2) foreach (Transform child in Map) child.gameObject.layer = 0;
	}

	public void ReadyNewWave()
	{
		if (waveIsReady.TryGetComponent<Animator>(out var animator))
		{
			waveIsReady.SetActive(true);
			animator.Play("Pulsation");
		}
	}

	private void Pause()
	{
		ChangeRaycastMap();
		pauseWindow.SetActive(!pauseWindow.activeSelf);
		Time.timeScale = Time.timeScale == 1 ? 0 : 1;
	}

		public void ReturnToMainMenu()
	{
		pauseWindow.SetActive(false);
		SaveSystem.Save();
		SceneManager.LoadScene(0);
	}
	#region TowerUpgradeWindow
	public void OpenTowerUpgrade(GameObject obj)
	{
		ChangeRaycastMap();
		towersShop.SetActive(false);
		towerUpgrade.SetActive(!towerUpgrade.activeSelf);

		if (obj.TryGetComponent<Tower>(out var tower))
		{
			currentTower = tower;
			UpdateDescription(tower);
		}
	}

	public void CloseTowerUpgrade()
	{
		ChangeRaycastMap();
		towersShop.SetActive(false);
		towerUpgrade.SetActive(false);
	}

	private void UpdateDescription(Tower tower)
	{
		towerIcon.sprite = tower.Icon;
		towerDescription.text = tower.GetDescription();
		towerUpgradeCost.text = tower.UpgradeCost.ToString();
		towerSellCost.text = tower.SellCost.ToString();
	}

	public void Upgrade()
	{
		if(currentTower.UpgradeCost <= PlayerManager.instance.money)
		{
			PlayerManager.instance.AddMoney(-currentTower.UpgradeCost);
			currentTower.Upgrade();
			UpdateDescription(currentTower);
		}
	}

	public void Sell()
	{
		PlayerManager.instance.AddMoney(currentTower.SellCost);
		currentTower.Destroy();
		ChangeRaycastMap();
		towerUpgrade.SetActive(false);
	}
	#endregion

	public Tower Tower
	{
		get => default;
		set
		{
		}
	}
}
