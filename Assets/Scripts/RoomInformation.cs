using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649

public enum RoomSide { Top, Bottom, Left, Right };

[CreateAssetMenu(menuName = "Room")]
public class RoomInformation : ScriptableObject {
    [SerializeField] private Sides sidesOpen;



    public bool sideOpen(RoomSide roomSide) {
        if (roomSide == RoomSide.Top) {
            return sidesOpen.Top;
        } else if (roomSide == RoomSide.Bottom) {
            return sidesOpen.Bottom;
        } else if (roomSide == RoomSide.Left) {
            return sidesOpen.Left;
        } else if (roomSide == RoomSide.Right) {
            return sidesOpen.Right;
        }
        return false;
    }

    public static RoomSide GetSide (int num) {
        if (num == 0) {return RoomSide.Top;}
        if (num == 1) {return RoomSide.Bottom;}
        if (num == 2) { return RoomSide.Left; }
        return RoomSide.Right;
    }

    public bool invSideOpen(RoomSide roomSide) {
        if (roomSide == RoomSide.Top) {
            return sideOpen(RoomSide.Bottom);
        } else if (roomSide == RoomSide.Bottom) {
            return sideOpen(RoomSide.Top);
        } else if (roomSide == RoomSide.Left) {
            return sideOpen(RoomSide.Right);
        } else if (roomSide == RoomSide.Right) {
            return sideOpen(RoomSide.Left);
        }
        return false;
    }
}

[Serializable]
public struct Sides {
    [SerializeField] public bool Left;
    [SerializeField] public bool Right;
    [SerializeField] public bool Top;
    [SerializeField] public bool Bottom;
}