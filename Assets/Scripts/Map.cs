using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Map : MonoBehaviour {

	public float spacing = 1.0f;
	public GameObject roomText;
	public GameObject mapLine;
	public Camera cam;

	public Dictionary<string, MapRoom> mapRooms = new Dictionary<string, MapRoom>();

	string[] map = {"Entrance", "north", "Office", "south", "west", "Atrium", "north", "north", "Basement", "west", "west", "south", "Bedroom"};

	Vector2 currentPos = Vector2.zero;
	Vector3 camMoveDir;
	Vector3 lastPos;
	GameObject mapParent;

	void Awake()
	{
		InitialSetup ();
		GenerateMap ();
	}

	void InitialSetup()
	{
		camMoveDir = cam.transform.position;
		currentPos = transform.position;
	}

	void GenerateMap()
	{
		mapParent = new GameObject ("Map Sprites");

		GameObject line = null;
		Vector3 oldPos = Vector3.zero;

		foreach(string word in map)
		{
			line = InstantiateMapLine ();
			oldPos = currentPos;

			switch (word)
			{
			case "north":
				currentPos += Vector2.up * spacing;
				if (line != null) DrawLine (line, oldPos, currentPos);
				break;
			case "south":
				currentPos += Vector2.down * spacing;
				if (line != null) DrawLine (line, oldPos, currentPos);
				break;
			case "east":
				currentPos += Vector2.right * spacing;
				if (line != null) DrawLine (line, oldPos, currentPos);
				break;
			case "west":
				currentPos += Vector2.left * spacing;
				if (line != null) DrawLine (line, oldPos, currentPos);
				break;
			default:
				DrawRoom (word);
				break;

			}
		}
	}

	void DrawLine(GameObject line, Vector3 start, Vector3 end)
	{
		Vector3 center = (start + end) / 2f;
		line.transform.position = center;

		Vector3 dir = end - start;
		dir = Vector3.Normalize (dir);
		line.transform.right = dir;

		Vector3 scale = new Vector3 (1, 1, 1);
		scale.x = Vector3.Distance (start, end) + 1f;
		line.transform.localScale = scale;
	}

	void AddRoomToDict(MapRoom room)
	{
		room.roomPos = currentPos;
		room.discovered = false;

		mapRooms.Add (room.roomName, room);

	}

	GameObject InstantiateMapLine()
	{
		GameObject go = Instantiate (mapLine, currentPos, Quaternion.identity);
		go.transform.Translate (0, 0, -10);
		go.transform.SetParent (mapParent.transform);

		return go;
	}

	void DrawRoom(string name)
	{
		GameObject go = Instantiate (roomText, currentPos, Quaternion.identity);
		go.transform.SetParent (mapParent.transform);
		TextMeshPro tmp = go.GetComponent<TextMeshPro>();
		MapRoom room = go.GetComponent<MapRoom>();
		room.roomName = name;

		AddRoomToDict (room);

		tmp.text = name;
	}
		
	void Update()
	{
		if(cam.transform.position != camMoveDir)
		{
			cam.transform.position = Vector3.Lerp (cam.transform.position, camMoveDir, Time.deltaTime * 5);
		}
	}

	public void DebugMoveCamera()
	{
		if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			camMoveDir += new Vector3 (-1, 0, 0) * spacing;
		}
		else if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			camMoveDir += new Vector3 (1, 0, 0) * spacing;
		}
		else if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			camMoveDir += new Vector3 (0, 1, 0) * spacing;
		}
		else if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			camMoveDir += new Vector3 (0, -1, 0) * spacing;
		}
			
	}

	public void ChangeCurrentMapRoom(MapRoom room)
	{
		if(room.discovered == false)
		{
			room.DiscoverRoom ();
		}

		camMoveDir = new Vector3 (room.roomPos.x, room.roomPos.y, camMoveDir.z);
	}


		
}
