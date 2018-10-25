using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;
using System;

public class Select_Scene_Option : MonoBehaviour {

    public Button[] buttons;
    private int currIndex = 0;
    public VRTK_PanelMenuItemController Menu;

    enum Direction { up, down };

    private void Awake()
    {
        if (buttons.Length <= 0)
        {
            print("Have you put in buttons into select_scene_option?");
        }
        if (buttons == null || Menu == null)
        {
            print("Something isn't set correctly in select_scene_option");
        }

        Menu.PanelMenuItemSwipeBottom += Menu_PanelMenuItemSwipeBottom;
        Menu.PanelMenuItemSwipeTop += Menu_PanelMenuItemSwipeTop;
    }

    //Every time menu is reshown, the cancel button is selected
    public void ResetOption()
    {
        currIndex = buttons.Length - 1;
        buttons[currIndex].Select();
    }

    private void Menu_PanelMenuItemSwipeTop(object sender, PanelMenuItemControllerEventArgs e)
    {
        MoveButton(Direction.up);
    }

    private void Menu_PanelMenuItemSwipeBottom(object sender, PanelMenuItemControllerEventArgs e)
    {
        MoveButton(Direction.down);
    }

    //Called from Controller_Menu_Popup, executes a button click on the current button
    public void Execute()
    {
        buttons[currIndex].onClick.Invoke();
    }

    // Moves the button indexed and selected up or down
    private void MoveButton(Direction dir)
    {
        currIndex = (dir == Direction.down) ? Math.Min(buttons.Length - 1, currIndex + 1) : Math.Max(0, currIndex - 1);
        buttons[currIndex].Select();
    }


}
