using UnityEngine;
/// <summary>
/// Base class for all projectiles
/// </summary>
public abstract class Bullet : MonoBehaviour
{
	protected int damage;
	protected float critChance;
	protected float critMultiplier;
	protected float speed;

	[Header("Unity Setup Fields")]
	[SerializeField]
	protected GameObject BulletImpact;

	protected Debuff debuff;

	protected Transform target;

	private float timer = 0;
	private float lifeTime = 10f;

	void Update()
	{
		if (target != null)
		{

			Vector3 dir = target.position - transform.position;
			transform.Translate(dir * speed * Time.deltaTime);


			if (Vector3.Distance(target.position, transform.position) <= 0.2f)
			{
				HitTheTarget();
				return;
			}

		}

		timer += Time.deltaTime;
		if (lifeTime <= timer) Destroy(gameObject);
	}

	public void MoveToTarget(Transform target)
	{
		this.target = target;
	}

	protected  void HitTheTarget()
	{
		if (BulletImpact != null)
		{
			GameObject particle = Instantiate(BulletImpact, transform.position, transform.rotation);
			Destroy(particle, 2.5f);
		}

		if (target.gameObject.TryGetComponent<Enemy>(out var enemy)) enemy.TakeDamage(damage,critChance,critMultiplier, debuff);

		Destroy(gameObject);
	}




	public virtual void Init(int damage, float critChance,float critMultiplier, float speed, Transform target, Debuff debuff = null)
	{
		this.damage = damage;
		this.critChance = critChance;
		this.critMultiplier = critMultiplier;
		this.speed = speed;
		this.target = target;
		this.debuff = debuff; 
	}

	public virtual void Init(int damage,GameObject Position,GameObject Target)
	{
		this.damage = damage;
	}




}
