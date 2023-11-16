using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneContainer : MonoBehaviour
{
    [SerializeField] List<ChuongNgaiVat> obstacles = new List<ChuongNgaiVat>();
    [SerializeField] List<ChuongNgaiVat> startObstacles = new List<ChuongNgaiVat>();

    public Transform model;

    public ChuongNgaiVat getStartObstacle()
    {
        if (startObstacles.Count <= 1)
        {
            Debug.LogWarning("Cảnh báo: Đang chỉ có một chướng ngại vật mẫu");
            return startObstacles[0];
        }

        ChuongNgaiVat cnv = null;

        do
        {
            int r = Random.Range(0, startObstacles.Count);

            if (this.startObstacles[r].parentPlane == null)
                cnv = this.startObstacles[r];
        }
        while (cnv == null);

        return cnv;
    }

    public ChuongNgaiVat getObstacle()
    {
        if (obstacles.Count <= 1)
        {
            Debug.LogWarning("Cảnh báo: Đang chỉ có một chướng ngại vật mẫu");
            return obstacles[0];
        }

        ChuongNgaiVat cnv = null;

        do
        {
            int r = Random.Range(0, obstacles.Count);

            if (this.obstacles[r].parentPlane == null)
                cnv = this.obstacles[r];
        }
        while (cnv == null);

        return cnv;
    }

    public void giveBackObstacle(ChuongNgaiVat obstacle, MainPlane plane)
    {
        obstacle.gameObject.SetActive(false);
        obstacle.transform.SetParent(plane.Model);
        obstacle.parentPlane = null;
    }
}
