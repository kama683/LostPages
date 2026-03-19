using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PagesManager : MonoBehaviour
{
    public Image[] pieces;

    private Color openColor = Color.white;
    private Color lockedColor = new Color(0.2f, 0.2f, 0.2f, 0.15f);

    private void Start()
    {

        PlayerPrefs.DeleteKey("UnlockedClosedPieces");
        PlayerPrefs.DeleteKey("NextLevel");
        PlayerPrefs.Save();//вот эти трое чтобы сбросить прогресс игры

        // сколько уже дополнительно открыто после прохождения уровней
        int unlockedClosedPieces = PlayerPrefs.GetInt("UnlockedClosedPieces", 0);

        // стартово открыты 1,3,5
        bool[] opened = new bool[6]
        {
            true,   // Piece_1
            false,  // Piece_2
            true,   // Piece_3
            false,  // Piece_4
            true,   // Piece_5
            false   // Piece_6
        };

        // по мере прохождения открываем 2, потом 4, потом 6
        if (unlockedClosedPieces >= 1) opened[1] = true; // Piece_2
        if (unlockedClosedPieces >= 2) opened[3] = true; // Piece_4
        if (unlockedClosedPieces >= 3) opened[5] = true; // Piece_6

        for (int i = 0; i < pieces.Length; i++)
        {
            if (pieces[i] == null) continue;
            pieces[i].color = opened[i] ? openColor : lockedColor;
        }
    }

    public void ContinueGame()
    {
        string nextLevel = PlayerPrefs.GetString("NextLevel", "Level_1");
        SceneManager.LoadScene(nextLevel);
    }
}