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
	public const int N_OBSTACLES = 8;
	public const int BUFFER = 0;

	public GameObject PollutionPrefab;
	public GameObject WallPrefab;
	public GameObject Player;
	//public GameObject Playfield;
	private static Transform ASB_roof, ASBfloor;

	public double score;
	public TimeSpan timer;
	public double timer_time;

	public enum State { Play, Paused, GameOver, Won };

	#region Transform Shortcuts
	private float x { get {return transform.position.x;} set {transform.position = new Vector3(value, transform.position.y, transform.position.z);} }
	private float y { get {return transform.position.y;} set {transform.position = new Vector3(transform.position.x, value, transform.position.z);} }
	private float z { get {return transform.position.z;} set {transform.position = new Vector3(transform.position.x, transform.position.y, value);} }
	private float xo{ get {return transform.rotation.x;} /*set {transform.Rotate(value, transform.rotation.y, transform.rotation.z);}*/ }
	private float yo{ get {return transform.rotation.y;} /*set {transform.Rotate(transform.rotation.x, value, transform.rotation.z);}*/ }
	private float zo{ get {return transform.rotation.z;} /*set {transform.Rotate(transform.rotation.x, transform.rotation.y, value);}*/ }
	private float xl{ get {return transform.localScale.x;} set {transform.localScale = new Vector3(value, transform.localScale.y, transform.localScale.z);} }
	private float yl{ get {return transform.localScale.y;} set {transform.localScale = new Vector3(transform.localScale.x, value, transform.localScale.z);} }
	private float zl{ get {return transform.localScale.z;} set {transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, value);} }
	#endregion

	public float Roof {get{ return ASB_roof.GetComponent<Collider2D>().bounds.min.y; }}

	public float Floor{get{ return ASBfloor.GetComponent<Collider2D>().bounds.max.y; }}
		
	public float Left {get{ return ASB_roof.GetComponent<Collider2D>().bounds.min.x + BUFFER; }}

	public float Right{get{ return ASB_roof.GetComponent<Collider2D>().bounds.max.x - BUFFER; }}

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
				(	/*x + */UnityEngine.Random.Range(Left, Right),
					/*y + */UnityEngine.Random.Range(Floor, Roof)
				),
				transform.rotation
			).transform.parent = transform;
		}

		//for (int i=0; i < N_WALLS; ++i)
		//{	float wall_y;
		//	if (1 == UnityEngine.Random.Range(0, 1)) wall_y = Floor;
		//	else /*-------------------------------*/ wall_y = Roof;
		//	Instantiate
		//	(	WallPrefab,
		//		loc = new Vector2(x + UnityEngine.Random.Range(Left, Right), y + wall_y),
		//		transform.rotation
		//	).transform.parent = transform;
		//}
	}
}