using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enums
{
    public class Tag
    {
        public static readonly string TagGoal = "Goal";
        public static readonly string TagDraggable = "Draggable";
        public static readonly string TagPlayer = "Player";
        public static readonly string TagEarth = "Earth";
        public static readonly string TagHouse = "house";
        public static readonly string TagRoad = "road";
        public static readonly string TagStone = "stone";
        public static readonly string TagClushka = "clushka";
    }
    public class Result
    {
    public static readonly string Win = "You Win Round";
    public static readonly string Lose = "You Lose!";
    public static readonly string Distance = "Distance exceeded";
    public static readonly string LastTry = "last try";
    public static readonly string JNPlased = "The Joystick is not placed inside a canvas";
    public static readonly string TTarget = "Take target";
    public static readonly string Thrown = "thrown";
    public static readonly string Jump = "jump";
    }
}
