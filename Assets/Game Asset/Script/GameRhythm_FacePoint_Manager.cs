using System.Collections.Generic;
using System.Threading.Tasks;
using GameCreator.Runtime.VisualScripting;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameRhythm_FacePoint_Manager : MonoBehaviour
{
    public List<GameRhythm_FacePoint> facePoints = new();
    [Header("Setup")] public float gameDuration = 4f;
    public float rhythmInterval = 1f;
    public float rhythmOffset = .3f;
    public float targetScore = 10;
    public List<Sprite> expressionSprites;
    public SpriteRenderer targetExpression;
    [Header("Runtime")] float currentScore;
    float currentTime;
    bool isRunning;
    GameRhythm_FacePoint lastPoint;
    public Actions MC_Up, MC_Down;
    public Actions action_success, action_fail;
    public Actions game_win, game_lose;

    [Button]
    void Initialize()
    {
        currentScore = 0;
        currentTime = 0;
        targetExpression.sprite = expressionSprites[Random.Range(0, expressionSprites.Count)];
        targetExpression.color = new Color(1, 1, 1, 0);
        facePoints.Clear();
        facePoints.AddRange(GetComponentsInChildren<GameRhythm_FacePoint>(true));
        foreach (var p in facePoints) p.InitScale();
    }

    public async Task StartGameAsync()
    {
        Initialize();
        await MC_Up.Run();
        isRunning = true;
        await GameLoop();
        FinishGame();
        await MC_Down.Run();
    }

    async Task GameLoop()
    {
        while (isRunning && currentTime < gameDuration)
        {
            currentTime += Time.deltaTime;
            ActivateRandomFacePoint();
            float wait = rhythmInterval + Random.Range(-rhythmOffset, rhythmOffset);
            await Task.Delay((int)(Mathf.Max(.05f, wait) * 1000));
        }
    }

    void ActivateRandomFacePoint()
    {
        if (facePoints.Count == 0) return;
        lastPoint = facePoints[Random.Range(0, facePoints.Count)];
        lastPoint.gameObject.SetActive(true);
        lastPoint.StartCountDown(OnPointScored);
    }

    void OnPointScored(float score)
    {
        currentScore += score;
        if (currentScore >= targetScore)
        {
            isRunning = false;
        }
        else if (score > 0)
        {
            action_success?.Run();
        }
        else
        {
            action_fail?.Run();
        }
    }

    void FinishGame()
    {
        Debug.Log($"Finished — Win: {currentScore >= targetScore}");
        if (currentScore >= targetScore)
        {
            game_win?.Run();
        }
        else
        {
            game_lose?.Run();
        }
    }
}