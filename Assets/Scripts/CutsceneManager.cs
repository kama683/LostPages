using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CutsceneManager : MonoBehaviour
{
    [Header("Настройки катсцены")]
    public GameObject[] slides;
    public string[] subtitles;
    public TextMeshProUGUI subtitleTextUI;
    
    [Header("Имя следующей сцены")]
    public string nextSceneName = "Level_1";

    private int currentSlideIndex = 0;

    void Start()
    {
        // ЖЕЛЕЗОБЕТОННЫЙ ФИКС: Принудительно выключаем ВСЕ картинки при старте игры
        for (int i = 0; i < slides.Length; i++)
        {
            if (slides[i] != null)
            {
                slides[i].SetActive(false);
            }
        }

        // И только после этого включаем самую первую картинку и текст
        UpdateCutsceneUI();
    }

    void Update()
    {
        // Переключаем по клику ЛКМ или пробелу
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            NextSlide();
        }
    }

    void NextSlide()
    {
        // Прячем текущую картинку
        if (currentSlideIndex < slides.Length && slides[currentSlideIndex] != null)
        {
            slides[currentSlideIndex].SetActive(false);
        }
        
        currentSlideIndex++;

        // Если слайды еще остались - показываем следующий
        if (currentSlideIndex < slides.Length)
        {
            UpdateCutsceneUI();
        }
        else
        {
            // Если слайды закончились - грузим уровень 1
            LoadNextScene();
        }
    }

    void UpdateCutsceneUI()
    {
        // Включаем нужную картинку
        if (currentSlideIndex < slides.Length && slides[currentSlideIndex] != null)
        {
            slides[currentSlideIndex].SetActive(true);
        }
        
        // Меняем текст, если он есть в массиве
        if (currentSlideIndex < subtitles.Length)
        {
            subtitleTextUI.text = subtitles[currentSlideIndex];
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}