using ElseForty.splineplus;
using ElseForty.splineplus.animation;
using ElseForty.splineplus.animation.api;
using ElseForty.splineplus.animation.model;
using UnityEngine;

public class AnimationUI : MonoBehaviour
{
    public CharacterManager characterManager;

    int controlType;
    int navigationType;

    public void Start()
    {
        var baseFollower = characterManager.Characters[0].baseFollower;
        controlType = (int)baseFollower.GetControlType();
        navigationType = (int)baseFollower.GetNavigationType();
    }
    void OnGUI()
    {
        // Set the width and height of the buttons
        float buttonWidth = 100f;
        float buttonHeight = 50f;

        // Get reference to the base follower
        var baseFollower = characterManager.Characters[0].baseFollower;

        // Top-Left Corner - Walk and Run Buttons
        float buttonY = 10f;
        float walkButtonX = 10f;
        float runButtonX = walkButtonX + buttonWidth + 10f;

        // Create a button for Walk
        if (GUI.Button(new Rect(walkButtonX, buttonY, buttonWidth, buttonHeight), "Walk"))
        {
            SetTargetSpeed(2.5f);
        }

        // Create a button for Run
        if (GUI.Button(new Rect(runButtonX, buttonY, buttonWidth, buttonHeight), "Run"))
        {
            SetTargetSpeed(7f);
        }

        // Vertical space between sections
        float verticalSpacing = 20f;

        // Control Type Selection
        float controlLabelY = buttonY + buttonHeight + verticalSpacing;
        GUI.Label(new Rect(10, controlLabelY, 200, 20), "Control Type: ");
        controlType = GUI.SelectionGrid(new Rect(10, controlLabelY + 20f, 200, 60), controlType, new string[] { "Auto", "Keyboard", "Static" }, 1);
        baseFollower.SetControlType((ControlType_Enum)controlType);

        // Navigation Type Selection
        float navigationLabelY = controlLabelY + 60f + verticalSpacing;
        GUI.Label(new Rect(10, navigationLabelY, 200, 20), "Navigation Type: ");
        navigationType = GUI.SelectionGrid(new Rect(10, navigationLabelY + 20f, 200, 40), navigationType, new string[] { "Random", "Keyboard" }, 1);
        baseFollower.SetNavigationType((NavigationType_Enum)navigationType);

        // Bottom-Left Corner - Directional Buttons
        float buttonSize = 50f;
        float buttonSpacing = 10f;
        float startX = buttonSpacing;
        float startY = Screen.height - (buttonSize + buttonSpacing);

        if (controlType == (int)ControlType_Enum.Keyboard)
        {
            GUI.enabled = !Input.GetKey(KeyCode.UpArrow);
            GUI.Button(new Rect(startX + buttonSize + buttonSpacing, startY - buttonSize - buttonSpacing, buttonSize, buttonSize), "↑");

            GUI.enabled = !Input.GetKey(KeyCode.DownArrow);
            GUI.Button(new Rect(startX + buttonSize + buttonSpacing, startY, buttonSize, buttonSize), "↓");

        }
 
        if (navigationType == (int)NavigationType_Enum.Keyboard)
        {
            GUI.enabled = !Input.GetKey(KeyCode.LeftArrow);
            GUI.Button(new Rect(startX, startY, buttonSize, buttonSize), "←");
            GUI.enabled = !Input.GetKey(KeyCode.RightArrow);
            GUI.Button(new Rect(startX + (buttonSize + buttonSpacing) * 2, startY, buttonSize, buttonSize), "→");
        }

        // Re-enable GUI elements after disabling
        GUI.enabled = true;
    }
    void SetTargetSpeed(float targetSpeed)
    {
        characterManager.SetTargetSpeed(characterManager.Characters[0].baseFollower.GetGameObject(), targetSpeed);
    }
}