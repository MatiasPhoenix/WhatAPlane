using UnityEngine;

public class DeadFXAnimations : MonoBehaviour
{
    float deadTimer = 15;
   void Start()
   {
       Destroy(gameObject, deadTimer);
   }
}
