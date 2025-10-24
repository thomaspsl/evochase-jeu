using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviorReptile : MonoBehaviour, IEnemy
{
	private Animator anim;
	private Transform Target;
	private UnityEngine.AI.NavMeshAgent agent;
	private bool attacking = false;

	private float Distance;
	private float DistanceBase;
	private Vector3 basePositions;

	[SerializeField]
	private float chaseRange = 20;
	[SerializeField]
	private float attackRange = 5;
	[SerializeField]
	private float damage = 10;

	[SerializeField]
	private double attackRepeatTime = 2;
	[SerializeField]
	private double attackTime;
	[SerializeField]
	private AudioClip attackSound;
	[SerializeField]
	private AudioClip deathSound;

	private bool isDead = false;

	[SerializeField]
	private int currentHealth = 100;
	public int CurrentHealth { get => currentHealth; set => currentHealth = value; }


	// Use this for initialization
	void Start()
	{
		agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
		anim = GetComponent<Animator>();
		basePositions = transform.position;
		Target = GameObject.Find("Player").transform;
	}

	// Update is called once per frame
	void Update()
	{
		if (!isDead)
		{
			Distance = Vector3.Distance(Target.position, transform.position);
			DistanceBase = Vector3.Distance(basePositions, transform.position);

			if (currentHealth <= 0)
				Dead();

			else if (Distance > chaseRange && DistanceBase <= 1)
				idle();

			else if (Distance < chaseRange && Distance > attackRange)
				chase();

			else if (Distance < attackRange)
				attack();

			else if (Distance > chaseRange && DistanceBase > 1)
				BackBase();
		}

	}

	void attack()
	{
		agent.destination = transform.position;
		transform.LookAt(Target);

		if (Time.time > attackTime)
		{
			int n = Random.Range(0, 2);
            switch (n) 
			{
				case 0:
					anim.Play("Attack(1)");
					StartCoroutine(AttackAnimation("Attack(1)"));
					break;
				case 1:
					anim.Play("Attack(2)");
					StartCoroutine(AttackAnimation("Attack(2)"));
					break;
			}

			Target.GetComponent<PlayerInventory>().ApplyDamage(damage);

			Debug.Log("L'ennemi a envoyé " + damage + " points de dégats");
			attackTime = Time.time + attackRepeatTime;

		}
	}

	void idle()
	{
		anim.Play("Idle");
	}

	void chase()
	{
		if (!attacking)
		{
			agent.destination = Target.position;
			anim.Play("Run");
		}
	}

	public void BackBase()
	{
		agent.destination = basePositions;
		anim.Play("Run");
	}

	public void Dead()
	{
		GameObject.Find("deathSound").GetComponent<AudioSource>().PlayOneShot(deathSound, 0.7f);
		gameObject.GetComponent<CapsuleCollider>().enabled = false;
		isDead = true;
		agent.destination = transform.position;
		anim.Play("Dead");

		// apparition du loot
		//int randomNumber = Random.Range(0, loots.Length);
		//GameObject finalLoot = loots[randomNumber];
		//Instantiate(finalLoot, transform.position, transform.rotation);

		Destroy(transform.gameObject, 20);
	}

	IEnumerator AttackAnimation(string name)
	{
		GameObject.Find("attackSound").GetComponent<AudioSource>().PlayOneShot(attackSound, 0.7f);
		attacking = true;
		yield return new WaitForEndOfFrame();
		while (anim.GetCurrentAnimatorStateInfo(0).IsName(name) && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
		{
			yield return null;
		}
		attacking = false;
	}
}
