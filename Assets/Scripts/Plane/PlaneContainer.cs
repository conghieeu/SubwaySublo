using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneContainer : MonoBehaviour
{
    public List<ChuongNgaiVat> obstacles = new List<ChuongNgaiVat>();

    public ChuongNgaiVat getObstacle(bool isStartObstacle)
    {
        if (obstacles.Count <= 0) return null;

        ChuongNgaiVat cnv = null;

        for (int i = 0; i < 200; i++)
        {
            int r = Random.Range(0, obstacles.Count);

            if (obstacles[r].isThisStartObstacle == isStartObstacle)
            {
                cnv = obstacles[r];
                break;
            }
        }

        obstacles.Remove(cnv);

        return cnv;
    }

    /// <summary> Trả Obstacle lại về kho </summary>
    public void addObstacle(ChuongNgaiVat obstacle)
    {
        obstacle.gameObject.SetActive(false);
        obstacle.transform.SetParent(this.transform);
        obstacles.Add(obstacle);
        obstacle.parentPlane = null;
    }
}
