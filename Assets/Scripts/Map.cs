using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Map : MonoBehaviour {

	public float spacing = 1.0f;
	public GameObject roomText;
	public Camera cam;

	public Dictionary<string, MapRoom> mapRooms = new Dictionary<string, MapRoom>();

	string[] map = {"Entrance", "north", "Office", "south", "west", "Atrium", "north", "north", "Basement"};
	Vector2 currentPos = Vector2.zero;
	LineRenderer lr;
	int lrPos = 1;
	Vector3 camMoveDir;
	Vector3 lastPos;

	void Awake()
	{
		lr = GetComponent<LineRenderer> ();

		InitialSetup ();
		GenerateMap ();
	}

	void InitialSetup()
	{
		camMoveDir = cam.transform.position;
		currentPos = transform.position;

		lr.positionCount = map.Length + 1;
		lr.SetPosition (0, currentPos);
	}

	void GenerateMap()
	{
		foreach(string word in map)
		{
			switch (word)
			{
			case "north":
				currentPos += Vector2.up * spacing;
				break;
			case "south":
				currentPos += Vector2.down * spacing;
				break;
			case "east":
				currentPos += Vector2.right * spacing;
				break;
			case "west":
				currentPos += Vector2.left * spacing;
				break;
			default:
				DrawRoom (word);
				break;

			}

			lr.SetPosition (lrPos, currentPos);
			lrPos++;
		}

	}

	void AddRoomToDict(MapRoom room)
	{
		room.roomPos = currentPos;
		room.discovered = false;

		mapRooms.Add (room.roomName, room);

	}

	void DrawRoom(string name)
	{
		GameObject go = Instantiate (roomText, currentPos, Quaternion.identity);
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
