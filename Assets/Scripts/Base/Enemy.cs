using System.Collections;
using UnityEngine;
/// <summary>
/// Base class for Enemies
/// </summary>
public class Enemy : MonoBehaviour
{
	[Header("Enemy attributes")]
	[SerializeField]
	protected int Health;
	[SerializeField]
	protected float Speed;
	[SerializeField]
	protected int MoneyReward;
	[SerializeField]
	protected GameObject DeathParticle;
	[SerializeField]
	protected Animator ObjectAnimator;

	[Header("Debaff stats particles")]
	[SerializeField]
	protected GameObject poisonParticles;
	[SerializeField]
	protected GameObject fireParticles;
	[SerializeField]
	protected GameObject iceParticles;

	[SerializeField]
	protected GameObject healthBar;

	protected int maxHealth;
	protected float baseSpeed;
	protected Transform target;
	protected int targetIndex;
	protected float WaveMultiplier;

	private IEnumerator Debuff;

	private void Start()
    {
		InvokeRepeating("UpdateHealthBar", 0f, 2.5f);
		target = Waypoints.points[0];

		WaveMultiplier = 1 + WavesManager.instance.CountOfWave/50;
		Health *= (int)WaveMultiplier;
		MoneyReward *= (int)WaveMultiplier;
		baseSpeed = Speed;
		maxHealth = Health;
		ObjectAnimator.Play("Run");
    }

    
     private void Update()
    {
		Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * Speed * Time.deltaTime);

		if(Vector3.Distance(target.position,transform.position) <= 0.4f)
		{
			getNextTarget();
		}


	}

	protected void getNextTarget()
	{
		if (targetIndex >= Waypoints.points.Length - 1) ComingToCastle();
		else
		{
			targetIndex++;
			target = Waypoints.points[targetIndex];
		}
	}

	public void TakeDamage(int damage,float critChance, float critMult, Debuff debuff = null) //TODO: more mechanics
	{	
		if(debuff !=null)
		{
			Debuff = Dot(debuff);
			StartCoroutine(Debuff);
		}
		int dmg = (int)(Random.Range(0f, 1f) <= critChance ? damage * critMult : damage); 
		Health -= dmg;

		if (Health <= 0)
		{
			Death();
		}
		
	}

	protected virtual void ComingToCastle()
	{
		WavesManager.instance.BreakHeart();
		Destroy(gameObject);
	}

	protected virtual void Death()
	{
		for (int i = 0; i < MoneyReward; i++)
		{
			GameObject particle = Instantiate(DeathParticle, transform.position + new Vector3(Random.Range(0,0.2f), Random.Range(0,0.5f), Random.Range(0,0.2f)), transform.rotation);
		}
		PlayerManager.instance.EnemyKilled();
		Destroy(gameObject);
	}

	protected IEnumerator Dot(Debuff debuff)
	{
		int count = 0;
		if (debuff.Name == "Poison") poisonParticles.SetActive(true);
		if (debuff.Name == "Fire") fireParticles.SetActive(true);
		if (debuff.Name == "Ice") iceParticles.SetActive(true);

		while (count < debuff.Duration)
		{

			TakeDamage(debuff.DPS,0,0);
			Speed = baseSpeed * debuff.SlowDown;

			count++;
			yield return new WaitForSeconds(1f);
		}

		if (debuff.Name == "Poison") poisonParticles.SetActive(false);
		if (debuff.Name == "Fire") fireParticles.SetActive(false);
		if (debuff.Name == "Ice") iceParticles.SetActive(false);

		Speed = baseSpeed;

		StopCoroutine(Debuff);
	}

	protected void UpdateHealthBar()
	{
		if(healthBar !=null && healthBar.TryGetComponent<UnityEngine.UI.Image>(out var image))
		{
			image.fillAmount = (float)Health / (float)maxHealth; 	
		}
	}
}
