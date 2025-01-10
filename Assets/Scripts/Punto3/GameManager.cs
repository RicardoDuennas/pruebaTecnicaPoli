using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Cube Settings")]
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float cubeLifetime = 1f;
    [SerializeField] private Vector2 spawnAreaSize = new Vector2(8f, 4f);

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI totalScoreText;
    [SerializeField] private TextMeshProUGUI yellowScoreText;
    [SerializeField] private TextMeshProUGUI blueScoreText;
    [SerializeField] private TextMeshProUGUI redScoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    
    [Header("Game Settings")]
    [SerializeField] private float gameTime = 60f;

    private float currentTime;
    private int totalScore = 0;
    private Dictionary<Color, int> colorScores = new Dictionary<Color, int>();
    private float nextSpawnTime = 0f;
    private bool isGameActive = false;

    private void Start()
    {
        // Inicializar puntuaciones por color
        colorScores[Color.yellow] = 0;
        colorScores[Color.blue] = 0;
        colorScores[Color.red] = 0;

        StartGame();
    }

    private void StartGame()
    {
        currentTime = gameTime;
        isGameActive = true;
        UpdateUI();
           // SpawnCube();
    }

    private void Update()
    {
        if (!isGameActive) return;

        // Actualizar tiempo
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            EndGame();
            return;
        }

        // Generar nuevos cubos
        if (Time.time >= nextSpawnTime)
        {
            SpawnCube();
            nextSpawnTime = Time.time + spawnInterval;
        }

        UpdateUI();
    }

    private void SpawnCube()
    {
        // Posición aleatoria dentro del área de spawn
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnAreaSize.x, spawnAreaSize.x),
            Random.Range(-spawnAreaSize.y, spawnAreaSize.y),
            0
        );

        GameObject cube = Instantiate(cubePrefab, randomPosition, Quaternion.identity);
        
        // Asignar color aleatorio
        Color[] colors = { Color.yellow, Color.blue, Color.red };
        Color randomColor = colors[Random.Range(0, colors.Length)];
        
        cube.GetComponent<SpriteRenderer>().material.color = randomColor;
        
        // Agregar componente para manejar el clic
        ClickableCube clickableCube = cube.AddComponent<ClickableCube>();
        clickableCube.Initialize(this, randomColor, cubeLifetime);
    }

    public void AddScore(Color color)
    {
        totalScore++;
        colorScores[color]++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (totalScoreText) totalScoreText.text = $"Total: {totalScore}";
        if (yellowScoreText) yellowScoreText.text = $"Amarillo: {colorScores[Color.yellow]}";
        if (blueScoreText) blueScoreText.text = $"Azul: {colorScores[Color.blue]}";
        if (redScoreText) redScoreText.text = $"Rojo: {colorScores[Color.red]}";
        if (timeText) timeText.text = $"Tiempo: {Mathf.Round(currentTime)}";
    }

    private void EndGame()
    {
        isGameActive = false;
        Debug.Log($"Juego terminado!\nPuntaje total: {totalScore}\n" +
                  $"Amarillos: {colorScores[Color.yellow]}\n" +
                  $"Azules: {colorScores[Color.blue]}\n" +
                  $"Rojos: {colorScores[Color.red]}");
    }
}