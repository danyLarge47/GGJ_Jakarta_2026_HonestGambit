using System.Threading.Tasks;
using GameCreator.Runtime.VisualScripting;
using Sirenix.OdinInspector;
using UnityEngine;

public class Gameplay_Sequence : MonoBehaviour
{
    public GameRhythm_FacePoint_Manager gameplay_1;
    public Actions startingUp;
    
    public Actions opponentUp, opponentDown;
    public Actions backToMainMenu;
    public int gameCount = 3;
    public int bufferTime = 1200;

    [Button(ButtonSizes.Large)]
    public void StartGame()
    {
        gameCount = Random.Range(3, 6);
        _ = StartGameAsync();
    }

    async Task StartGameAsync()
    {
        startingUp.Run();
        
        for (int i = 0; i < gameCount; i++)
        {
            await CheckGame();
        }

        backToMainMenu.Run();
    }

    async Task CheckGame()
    {
        await gameplay_1.StartGameAsync();
        await opponentUp.Run();
        await Task.Delay(bufferTime);
        await opponentDown.Run();
    }
    
 
}