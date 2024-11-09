using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [HideInInspector] public Controls controls;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
