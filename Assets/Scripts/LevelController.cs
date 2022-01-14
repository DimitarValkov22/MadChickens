using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    Monster[] monsters;
    [SerializeField] string nextLevelName;

    void OnEnable()
    {
        monsters = FindObjectsOfType<Monster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MonstersAreAllDead())
            GoToNextLevel();
    }

     void GoToNextLevel()
    {
        Debug.Log("Go to level " + nextLevelName);
        SceneManager.LoadScene(nextLevelName);
    }

      bool MonstersAreAllDead()
    {
        foreach (var monster in monsters)
        {
            if (monster.gameObject.activeSelf)
                return false;
        }
        return true;
    }
}
