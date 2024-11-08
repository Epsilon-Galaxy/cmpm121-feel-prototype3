using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [HideInInspector] public Controls controls;

    private void Awake()
    {
        instance = this;
    }
}
