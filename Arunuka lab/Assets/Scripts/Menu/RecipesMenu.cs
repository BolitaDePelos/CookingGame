using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
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
        Cursor.visible = true;
        GameIsPaused = true;
    }

    public void Continue()
    {
        AudioManager.Instance.PlayNextPageSound();
        book.currentPage = 0;
        RecipesMenuUI.SetActive(false);
        GameIsPaused = false;
        Cursor.visible = false;
       
    }

}
