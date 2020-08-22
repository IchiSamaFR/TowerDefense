using UnityEngine;


/*
 * Waypoints followed by enemies
 * Every waypoints are stocked in points
 */
public class Waypoints : MonoBehaviour
{
    public static Transform[] points;

    public PlayerStats toInstance;
    public static PlayerStats playerStats;

    void Awake()
    {
        playerStats = toInstance;

        points = new Transform[transform.childCount];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}
