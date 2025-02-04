using UnityEngine;

public class PlayerAwakeActivation : MonoBehaviour
{
    [Header("Player")]
    public PlayerController thisPlayerController;
    public PlayerShooting thisPlayerShooting;

    private void Start()
    {
        thisPlayerController.enabled = true;
        thisPlayerShooting.enabled = true;
    }
}
