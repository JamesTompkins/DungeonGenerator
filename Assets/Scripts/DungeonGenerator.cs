using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649

public class DungeonGenerator : MonoBehaviour {
    //input information
    private static int maxDepth = 20;
    [SerializeField] private Room[] rooms;
    [SerializeField] private GameObject[] doors;
    [SerializeField] private GameObject grass;
    private Room thisRoom;

    //list of which room fits which side
    private List<List<Room>> sideRooms = new List<List<Room>>();
    //list of all rooms generated
    private List<Room> generatedRooms = new List<Room>();

    //roomoffset distances
    private Vector3Int[] offsets = {
        new Vector3Int(0, 0, 18),
        new Vector3Int(0, 0, -18),
        new Vector3Int(-18, 0, 0),
        new Vector3Int(18, 0, 0)
    };

    //offsets for coreners when creating grass
    private Vector3Int[] corners = {
        new Vector3Int (-18,0,18),
        new Vector3Int (18,0,18),
        new Vector3Int (-18,0,-18),
        new Vector3Int (18,0,-18)
    };

    private int[] counts = { 0, 0, 0, 0 };
    private static int amount = 0;

    void Start() {
        //look through all prefabs provided and add them to sideRooms
        FindRooms();

        GenerateRooms(thisRoom, Vector3Int.zero, 0);

        finalPass();

        Debug.Log(amount + " rooms created");
    }

    void GenerateRooms(Room room, Vector3Int pos, int depth) {
        depth++;                                        //keeps track of distance from origin
        amount++;                                       //total number of rooms
        RoomInformation info = room.roomInformation;    //information about which sides are open
        Room newRoom;                                   //the object to be instantiated, changes for each side

        //loop each side
        for (int i = 0; i < 4; i++) {
            //side from index
            RoomSide side = RoomInformation.GetSide(i);
            if (info.sideOpen(side)) {
                //accessing inverse list to find room that will fit
                int index = InverseIndex(i);
                newRoom = sideRooms[index][Random.Range(0, counts[index])];
                HandleSide( pos, side, newRoom, depth);
            }
        }
    }

    int InverseIndex(int i) {
        if (i==0) {
            return 1;
        }
        if (i == 1) {
            return 0;
        }
        if (i == 2) {
            return 3;
        }
        return 2;
    }

    void finalPass() {
        foreach (Room room in generatedRooms) {
            Transform thisRoom = room.transform;
            Vector3Int roomPosition = Vector3Int.RoundToInt(thisRoom.position);

            for (int i = 0; i < 4; i++) {
                Vector3Int testPosition = roomPosition + offsets[i];
                RoomSide side = RoomInformation.GetSide(i);

                //******PLACING GRASS******
                GameObject testObject = GetRoom(testPosition);
                if (!testObject) {
                    CreateGrass(testPosition);
                }

                //******PLACING DOORS******
                testObject = GetRoom(testPosition);
                //Debug.Log(room.gameObject + " " + testObject.gameObject + " this open: " + room.roomInformation.sideOpen(side) + " other open: " + !testObject.GetComponent<Room>().roomInformation.invSideOpen(side));
                if (room.roomInformation.sideOpen(side)) {
                    if (!testObject.GetComponent<Room>().roomInformation.invSideOpen(side)) {
                        CreateDoor(thisRoom, side);
                    }
                }

                //******PLACING GRASS CORNERS******
                Vector3Int cornerPosition = roomPosition + corners[i];
                if (!GetRoom(cornerPosition)) {
                    CreateGrass(cornerPosition);
                }
            }
        }
    }

    //Looks to see if new room should be created on each side of room
    void HandleSide(Vector3Int pos, RoomSide side, Room room, int depth) {
        Vector3Int newPos = pos + offsets[(int)side];

        if (!PosFree(newPos)) {
            return;
        }

        if (depth > maxDepth) {
            return;
        }

        //create room
        CreateRoom(room, newPos, depth);
        return;
    }

    void CreateRoom(Room newRoom, Vector3Int pos, int depth) {
        GameObject generatedRoom = Instantiate(newRoom.gameObject, pos, Quaternion.identity, transform.parent);
        generatedRoom.name = pos.ToString();
        generatedRooms.Add(generatedRoom.GetComponent<Room>());
        GenerateRooms(newRoom, pos, depth);
    }

    void CreateGrass(Vector3Int pos) {
        if (PosFree(pos)) {
            Instantiate(grass, pos, Quaternion.identity, transform.parent).name = pos.ToString();
        }
    }
    void CreateDoor(Transform transform, RoomSide side) {
        GameObject instance = doors[(int)side];
        Instantiate(instance, transform, false);
    }

    bool PosFree(Vector3Int newPos) {
        return GameObject.Find(newPos.ToString()) == null;
    }

    GameObject GetRoom(Vector3Int newPos) {
        return GameObject.Find(newPos.ToString());
    }

    void FindRooms() {
        thisRoom = GetComponent<Room>();

        //making sure origin is on list to be post processed
        generatedRooms.Add(thisRoom);

        foreach (Room room in rooms) {
            RoomInformation currentInfo = room.roomInformation;

            for (int i = 0; i < 4; i++) {
                sideRooms.Add(new List<Room>());
                if (currentInfo.sideOpen(RoomInformation.GetSide(i))) {
                    sideRooms[i].Add(room);
                    counts[i]++;
                }
            }
        }
    }
}
