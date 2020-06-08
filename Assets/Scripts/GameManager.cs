using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject targetPrefab;
    public Text scoreText;

    int score = 0;

    void Start()
    {
        for (int i = 0; i < 3; i++) {
            var pos = new Vector3(Random.Range(-45, 45),
                Random.Range(1, 25),
                Random.Range(30, 50));
            Instantiate(targetPrefab, 
                Camera.main.transform.position + pos, 
                Quaternion.identity);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var pos = Input.mousePosition;
            var ray = Camera.main.ScreenPointToRay(pos);
            var bullet = Instantiate(bulletPrefab, Camera.main.transform.position, Quaternion.identity).GetComponent<BulletController>();
            // bullet.Shot(new Vector3(0, 500, 2000));
            Debug.Log($"screen: {pos}, dir: {ray.direction}");

            bullet.OnHit += () => {
                score++;
                scoreText.text = $"Score: {score}";
            };
            bullet.Shot(ray.direction.normalized * 3000);
        }
    }
}
