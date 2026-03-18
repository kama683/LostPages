using UnityEngine;

public class WordPickup : MonoBehaviour
{
    public string wordName = "BRIDGE";
    public GameObject pressEText;

    private bool playerInside = false;

    private void Start()
    {
        if (pressEText != null)
            pressEText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = true;

            if (pressEText != null)
                pressEText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = false;

            if (pressEText != null)
                pressEText.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            if (pressEText != null)
                pressEText.SetActive(false);

            WordSystem.Instance.PickupWord(wordName);
            gameObject.SetActive(false);
        }
    }
}