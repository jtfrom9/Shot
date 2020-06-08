using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject targetPrefab;
    public Canvas gameCanvas;
    public Canvas gameoverCanvas;
    public Text scoreText;
    public Text waveText;
    public Text gameoverText;

    int score = 0;
    int wave = 0;

    void Generate()
    {
        wave++;
        waveText.text = $"Wave: {wave}";
        for (int i = 0; i < 3; i++)
        {
            var pos = new Vector3(Random.Range(-45, 45),
                Random.Range(1, 25),
                Random.Range(30, 50));
            var go = Instantiate(targetPrefab,
                Camera.main.transform.position + pos,
                Quaternion.identity);

            go.GetComponent<TargetController>().OnDead += () =>
            {
                CancelInvoke();
                gameCanvas.gameObject.SetActive(false);
                gameoverCanvas.gameObject.SetActive(true);
                gameoverText.text = $"Wave: {wave}, Score: {score}";
            };
        }

        var next = 8.0f - wave * 0.3f;
        if(next < 1.0f) next = 1.0f;
        Invoke("Generate", next);
    }

    void Start()
    {
        scoreText.text = $"Score: 0";
        Invoke("Generate", 1);
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
                score += wave;
                scoreText.text = $"Score: {score}";
            };
            bullet.Shot(ray.direction.normalized * 3000);
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene("TerrainTest");
    }
}
