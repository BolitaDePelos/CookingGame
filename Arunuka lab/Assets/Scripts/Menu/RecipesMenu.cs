using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RecipesMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject RecipesMenuUI;
    public Book book;


    private void Start()
    {
        RecipesMenuUI.SetActive(false);
        book= GetComponent<Book>();
    }


    private void Update()
    {
        book = FindObjectOfType<Book>();

        if (InputManager.GetInstance().GetRecipesPressed())
        {
            if (GameIsPaused)
            {
                Continue();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        RecipesMenuUI.SetActive(true);
        GameIsPaused = true;
    }

    public void Continue()
    {
        book.currentPage = 0;
        RecipesMenuUI.SetActive(false);
        GameIsPaused = false;
       
    }

}
