using UnityEngine;

[CreateAssetMenu(menuName = GameLauncher.GameName + "/Game Properties", fileName = "GDD_GameProperties")]
public class GameProperties:ScriptableObject
{
    [Header("Player")]
    public float PlayerMoveSpeed = 4f;

    public float PlayerJumpStrength = 4;

    [Range(0,1)]
    public float PlayerRotationLerpFactor = .7f;

    [Header("Camera")]
    public float CameraFov = 70;

    public float CameraDistance = 5;

    public Vector3 CameraRelativeOffset;

    public Vector2 CameraPitchAxisRange = new Vector2(-180,180);

    [Header("Inputs")]
    public float MouseSensibility = 2f;

    // -------------------------------------------------------

    private static GameProperties instance;
    public static GameProperties Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = Resources.Load("GDD_GameProperties") as GameProperties;

            if (instance != null)
                return instance;

            Debug.LogWarning("Game Properties asset not found");

            return null;
        }
    }
}