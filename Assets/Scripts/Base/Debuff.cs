using UnityEngine;

public abstract class Debuff
{
	[Header("Debaff setup fields")]
	public string Name;
	public float Duration;
	public float SlowDown;
	public int DPS;

	public virtual string GetDescription()
	{
		return "Spell: None";
	}
}
public class Fire: Debuff
{
	public Fire()
	{
		Name = "Fire";
		Duration = 5f;
		SlowDown = 1;
		DPS = 2;
	}

	public Fire(int level)
	{
		Name = "Fire";
		Duration = 5f + (int)(level / 5);
		Duration = Mathf.Clamp(Duration, 5f, 10f);
		SlowDown = 1;

		DPS = 2 + level/2;
		DPS = Mathf.Clamp(DPS, 2, 40);
	}

	public override string GetDescription()
	{
		return "<color=#F53434>Spell: Burns the enemy, inflicting " + DPS + " damage every second" + "\n" +
		"Duration: " + Duration + "</color=#F53434>";
	}
}

public class Poison : Debuff
{
	public Poison()
	{
		Name = "Poison";
		Duration = 5f;
		SlowDown = 0.85f;
		DPS = 1;
	}

	public Poison(int level)
	{
		Name = "Poison";
		Duration = 5f + (int)(level / 5);
		Duration = Mathf.Clamp(Duration, 5f, 10f);
		SlowDown = 0.9f - 0.1f * (int)(level / 5);
		SlowDown = Mathf.Clamp(SlowDown, 0.6f, 0.9f);

		DPS = 1 + level/3;
		DPS = Mathf.Clamp(DPS, 1, 20);
	}

	public override string GetDescription()
	{
		return "<color=#7CA817>Spell: Poisons an enemy, inflicting " + DPS + " damage every second and slows by " +(1-SlowDown)*100 + "%" + "\n" +
		"Duration: " + Duration + "</color=#7CA817>";
	}
}

public class Ice : Debuff
{
	public Ice()
	{
		Name = "Ice";
		Duration = 3f;
		SlowDown = 0.8f;
		DPS = 0;
	}

	public Ice(int level)
	{
		Name = "Ice";
		Duration = 3f + (int)(level / 5);
		Duration = Mathf.Clamp(Duration, 3f, 10f);
		SlowDown = 0.8f - 0.1f * (int)(level / 5);
		SlowDown = Mathf.Clamp(SlowDown, 0.35f, 0.8f);

		DPS = 0;
	}

	public override string GetDescription()
	{
		return "<color=#5DC6FA>Spell: Slows an enemy by " + (1-SlowDown)*100 + "%" + "\n" + 
		"Duration: " + Duration + "</color=#5DC6FA>";
	}

}
