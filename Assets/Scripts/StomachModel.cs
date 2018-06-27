#define INMODEL

using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Collections;
using UnityEngine;

public class StomachModel : MonoBehaviour {
	public const int N_WALLS = 5;
	public const int N_OBSTACLES = 20;
	public const int BUFFERFROMBOTTOM = 307;//(75/*height of vehicle*/ + 20/*buffer*/);

	public GameObject PollutionPrefab;
	public GameObject WallPrefab;
	public GameObject Player;
	//public GameObject Playfield;
	private static Transform ASB_roof, ASBfloor;

	public double score;
	public TimeSpan timer;
	public double timer_time;

	public enum State { Play, Paused, GameOver, Won };

	public float Roof {get{ return ASB_roof.transform.position.y - ASB_roof.GetComponent<Collider2D>().bounds.extents.y; }}

	public float Floor{get{ return ASBfloor.transform.position.y + ASBfloor.GetComponent<Collider2D>().bounds.extents.y; }}
		
	public float Left {get{ return -ASB_roof.GetComponent<Collider2D>().bounds.extents.x; }}

	public float Right{get{ return  ASB_roof.GetComponent<Collider2D>().bounds.extents.x; }}

	void Start ()
	{	ASB_roof = transform.GetChild(0);
		ASBfloor = transform.GetChild(1);
		if (!Player) Player = GameObject.Find("Player");        //Get easy access to the Player object

		GenerateObstacles();
		//for (int i=0; i < N_enemles; ++i) obstacles.Add(new Enemy());
	}
	
	void Update ()
	{	timer_time += Time.deltaTime/1000.0;
		//if (Player.bottom <= 0) GenerateObstacles();
	}

	public void GenerateObstacles()
	{	UnityEngine.Random rand = new UnityEngine.Random();
		Vector2 loc;
		for (int i=0; i < N_OBSTACLES; ++i)
		{	Instantiate
			(	PollutionPrefab,
				loc = new Vector2
				(	UnityEngine.Random.Range(Left, Right),
					UnityEngine.Random.Range(Floor, Roof)
				),
				transform.rotation
			).transform.parent = transform;
		}

		for (int i=0; i < N_WALLS; ++i)
		{	float wall_y;
			if (1 == UnityEngine.Random.Range(0, 1)) wall_y = Floor;
			else /*-------------------------------*/ wall_y = Roof;
			Instantiate
			(	WallPrefab,
				loc = new Vector2(UnityEngine.Random.Range(Left, Right), wall_y),
				transform.rotation
			).transform.parent = transform;
		}
	}
}