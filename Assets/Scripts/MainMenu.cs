using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject initMenu;
    [SerializeField] private GameObject joinMenu;
    [SerializeField] private TMP_InputField codeInput;

    void Start()
    {
        initMenu.SetActive(true);
        joinMenu.SetActive(false);
    }

    public void OnHostButtonClicked()
    {
        Debug.Log("Host button clicked");
    }

    public void OnShowJoinButtonClicked()
    {
        Debug.Log("Show Join button clicked");
        initMenu.SetActive(false);
        joinMenu.SetActive(true);
    }

    public void OnJoinButtonClicked()
    {
        string joinCode = codeInput.text;
        Debug.Log($"Join button clicked with code: {joinCode}");
    }

    public void OnBackButtonClicked()
    {
        Debug.Log("Back button clicked");
        joinMenu.SetActive(false);
        initMenu.SetActive(true);
    }
}
