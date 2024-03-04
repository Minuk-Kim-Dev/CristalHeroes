using System;
using UnityEngine;
using System.Collections;
using MonsterLove.StateMachine;

public class ExampleBasic : MonoBehaviour
{
	public Transform target;
	//Declare which states we'd like use
	public enum MonsterStates
	{
		Idle,
		MoveIdle,
		Move,
		Attack
	}

	private bool ischeck = false;
	private bool attackcheck = false;
	public bool attackcubecheck = false;
	private StateMachine<MonsterStates, StateDriverUnity> fsm;
	private float speed = 5f;

	private void Awake()
	{

		//Initialize State Machine Engine		
		fsm = new StateMachine<MonsterStates, StateDriverUnity>(this);
		fsm.ChangeState(MonsterStates.Idle);
	}

	void Update()
	{
		fsm.Driver.Update.Invoke();
	}

	
	void Idle_Enter()
	{
		Debug.Log("Idle_Enter");
	}
	
	void Idle_Update()
	{
		
		Debug.Log(ischeck);
		if (ischeck)
		{
			
			fsm.ChangeState(MonsterStates.Move);
		}
	}
	void MoveIdle_Enter()
	{
		Debug.Log("Move_Idle");
	}
	
	void MoveIdle_Update()
	{
		if (!attackcubecheck)
		{
			fsm.ChangeState(MonsterStates.Move);
		}
		
	}
	void Move_Enter()
	{
		Debug.Log("Move_Enter");
	}
	
	void Move_Update()
	{
		if (attackcubecheck)
		{
			fsm.ChangeState(MonsterStates.MoveIdle);
		}
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target.position, step);
		if (attackcheck)
		{
			fsm.ChangeState(MonsterStates.Attack);
		}
		if (ischeck == false)
		{
			fsm.ChangeState(MonsterStates.Idle);
		}
		
	}
	void Attack_Enter()
	{
		Debug.Log("Move_Enter");
	}
	
	void Attack_Update()
	{
		gameObject.GetComponent<SpriteRenderer>().color=Color.red;
		if (!attackcheck)
		{
			fsm.ChangeState(MonsterStates.Move);
		}

	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log(other.gameObject.tag);
		if (other.gameObject.tag == "Dog")
		{
			ischeck = true;
		}
	}
	
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Dog")
		{
			ischeck = false;
		}
	}
	
	private void OnCollisionEnter2D(Collision2D other)
	{
	
		if (other.gameObject.tag == "Cat")
		{
			attackcheck = true;
		}
		
	}

	private void OnCollisionStay2D(Collision2D other)
	{

		if (other.gameObject.tag == "Cube")
		{
			Debug.Log(other.gameObject.GetComponent<ExampleBasic>().fsm.State.ToString());
			if (other.gameObject.GetComponent<ExampleBasic>().fsm.State.ToString() == "Attack"
			    ||other.gameObject.GetComponent<ExampleBasic>().fsm.State.ToString() =="MoveIdle")
			{
				attackcubecheck = true;
			}
		}
	}

	private void OnCollisionExit2D(Collision2D other)
	{
	
		if (other.gameObject.tag == "Cat")
		{
			attackcheck = false;
		}

		if (other.gameObject.tag == "Cube")
		{
			attackcubecheck = false;
		}
	}
	
}
