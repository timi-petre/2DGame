using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private Enemy[] _enemies;
    private static int _nextLevelIndex = 1;

    private void OnEnable() //oridecateori gameobject este enabled
    { //save off the enemys, si dupa ce cauta sa vada ca inamicii sunt morti , o  sa treaca la nivelul urmator
        _enemies = FindObjectsOfType<Enemy>();
    }

    // Update is called once per frame
    void Update()
    { //verificam daca enemys sunt morti
        foreach(Enemy enemy in _enemies)
        {
            if(enemy != null)
                return;
        }

        Debug.Log("You killed all enemys");

        _nextLevelIndex++;
        string nextLevelName = "Level" + _nextLevelIndex;
        SceneManager.LoadScene(nextLevelName);
    }
}
