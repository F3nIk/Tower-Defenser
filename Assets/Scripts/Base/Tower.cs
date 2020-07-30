using UnityEngine;

/// <summary>
/// Base abstract class for towers.
/// </summary>
public abstract class Tower : MonoBehaviour
{
	[Header("Tower attributes")]
	[SerializeField]
	public int ID;
	[SerializeField]
	public int level;
	[SerializeField]
	protected int maxLevel;
	[SerializeField]
	protected float baseAttackSpeed;
	protected float attackCountDown;
	[SerializeField]
	protected float baseRange;
	[SerializeField]
	protected int baseMinDamage;
	[SerializeField]
	protected int baseMaxDamage;
	[SerializeField]
	protected float criticalChance;
	[SerializeField]
	protected float criticalMultiplier;
	[SerializeField]
	protected float bulletSpeed;

	protected float attackSpeed;
	protected float range;
	protected int minDamage;
	protected int maxDamage;

	[Header("Levelup variables")]
	[SerializeField]
	protected int step;
	[SerializeField]
	protected float attackSpeedPerLevel;
	[SerializeField]
	protected float rangePerLevel;
	[SerializeField]
	protected float minDamagePerLevel;
	[SerializeField]
	protected float maxDamagePerLevel;
	[SerializeField]
	protected float upgradeCostPerLevel;
	[SerializeField]
	protected float sellCostPerLevel;

	protected Debuff debuff;

	[Header("Max values of towers variables")]
	[SerializeField]
	protected int minDamageMax;
	[SerializeField]
	protected int maxDamageMax;
	[SerializeField]
	protected float rangeMax;
	[SerializeField]
	protected float attackSpeedMax;

	[HideInInspector]
	public Node CurrentNode;
	[Header("Money values")]
	public int MoneyCost;
	public int UpgradeCost;
	public int SellCost;

	[Header("Unity Setup Fields")]
	public Sprite Icon;
	[SerializeField]
	protected GameObject bullet;
	[SerializeField]
	protected Transform firePoint;
	[SerializeField]
	protected GameObject ChoiceParticles;

	[Header("Tower awakes")]
	[SerializeField]
	protected GameObject SecondAwake;
	[SerializeField]
	protected GameObject ThirdAwake;

	protected Transform target;

	public Debuff Debuff
	{
		get => default;
		set
		{
		}
	}

	public Bullet Bullet
	{
		get => default;
		set
		{
		}
	}

	private void Awake()
	{
		InvokeRepeating("UpdateTarget", 0f, 0.5f);

		attackSpeed = baseAttackSpeed;
		range = baseRange;
		minDamage = baseMinDamage;
		maxDamage = baseMaxDamage;
	}

	private void Start()
	{
		
	}

	private void Update()
	{
		if (target == null) return;
		else
		{
			attackCountDown += Time.deltaTime;

			if (attackCountDown >= attackSpeed)
			{
				attackCountDown = 0;
				Attack();
			}
		}
	}

	protected virtual void Attack()
	{
		var projectile = Instantiate(bullet, firePoint.position,Quaternion.identity);
		if(projectile.TryGetComponent(out Bullet projectileScript))
		{
			projectileScript.Init(Random.Range(minDamage, maxDamage + 1),criticalChance,criticalMultiplier, bulletSpeed, target, debuff);
			projectileScript.MoveToTarget(target);

		}


	}

	protected virtual void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject nearestEnemy = null;
		float shortiesDistance = Mathf.Infinity;



		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

			if (distanceToEnemy < shortiesDistance)
			{
				shortiesDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortiesDistance <= range)
		{
			target = nearestEnemy.transform;
		}
		else
		{
			target = null;
		}
	}

	private void OnMouseEnter()
	{
		ChoiceParticles.SetActive(true);
	}

	private void OnMouseExit()
	{
		ChoiceParticles.SetActive(false);

	}

	private void OnMouseDown()
	{
		GUIManager.instance.OpenTowerUpgrade(gameObject);
	}

	public string GetDescription()
	{
		string desc = "Level: " + level + "/" + maxLevel + "\n" +
		"Damage: " + minDamage + "-" + maxDamage + "\n" +
		"Crit Chance: " + criticalChance * 100 + "%" + "\n" +
		"Crit  Multiplier: " + criticalMultiplier + "\n" +
		"Attack Speed: " + attackSpeed + "\n" + 
		"Range: " + range + "\n";

		 string spell = debuff!=null? debuff.GetDescription() : "None";

		return desc + spell;
	}

	public virtual void Upgrade()
	{
		if (level < maxLevel)
		{
			level++;

			attackSpeed = baseAttackSpeed - (level / step * attackSpeedPerLevel * level);
			range = baseRange + (level / step * rangePerLevel * level);
			minDamage = baseMinDamage + (int)(level / step * minDamagePerLevel * level);
			maxDamage = baseMaxDamage + (int)(level / step * maxDamagePerLevel * level);

			attackSpeed = Mathf.Clamp(attackSpeed, attackSpeedMax, baseAttackSpeed);
			range = Mathf.Clamp(range, baseRange, rangeMax);
			minDamage = Mathf.Clamp(minDamage, baseMinDamage, minDamageMax);
			maxDamage = Mathf.Clamp(maxDamage, baseMaxDamage, maxDamageMax);


			UpgradeCost = (int)(MoneyCost / 2 * Mathf.Pow(upgradeCostPerLevel, level));
			SellCost = (int)(MoneyCost / 2 * Mathf.Pow(sellCostPerLevel, level));
		}
		else GUIManager.instance.NewWarningMessage("Tower has max level!");

	}

	public virtual void LoadUpgrade()
	{
		attackSpeed = baseAttackSpeed - (level / step * attackSpeedPerLevel * level);
		range = baseRange + (level / step * rangePerLevel * level);
		minDamage = baseMinDamage + (int)(level / step * minDamagePerLevel * level);
		maxDamage = baseMaxDamage + (int)(level / step * maxDamagePerLevel * level);

		attackSpeed = Mathf.Clamp(attackSpeed, attackSpeedMax, baseAttackSpeed);
		range = Mathf.Clamp(range, baseRange, rangeMax);
		minDamage = Mathf.Clamp(minDamage, baseMinDamage, minDamageMax);
		maxDamage = Mathf.Clamp(maxDamage, baseMaxDamage, maxDamageMax);


		UpgradeCost = (int)(MoneyCost / 2 * Mathf.Pow(upgradeCostPerLevel, level));
		SellCost = (int)(MoneyCost/2 * Mathf.Pow(sellCostPerLevel, level));
	}

	public void Awaake()
	{
		if(level == maxLevel)
		{
				
		}
	}

	public void Destroy()
	{
		CurrentNode.IsAvailable = true;
		Destroy(gameObject);
	}
}
