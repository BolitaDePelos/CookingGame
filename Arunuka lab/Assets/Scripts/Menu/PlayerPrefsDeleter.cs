using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsDeleter : MonoBehaviour
{
    public void Delete() 
    {
        PlayerPrefs.DeleteAll();
    }
}
