using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchLevel : MonoBehaviour {

    public enum Scenes { Maze, DOF, Forest};

    public Scenes Scene;

    public void Change()
    {
        switch(Scene)
        {
            case Scenes.Maze:
                SceneManager.LoadSceneAsync(2);
                break;
            case Scenes.DOF:
                SceneManager.LoadSceneAsync(1);
                break;
            case Scenes.Forest:
                SceneManager.LoadSceneAsync(3);
                break;
        }
    }
}
