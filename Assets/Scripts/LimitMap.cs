using UnityEngine;

public class LimitMap : MonoBehaviour
{
    public Vector2 mapLimits; // Limiti della mappa su X e Y (es. 10 e 5)
    public GameObject player1Prefab; // Prefab del primo giocatore
    public GameObject player2Prefab; // Prefab del secondo giocatore
    public GameObject player3Prefab; // Prefab del terzo giocatore
    public GameObject player4Prefab; // Prefab del quarto giocatore

    private void OnTriggerEnter(Collider collider)
    {
        // Verifica che l'oggetto abbia uno dei due tag validi
        if (collider.CompareTag("Player1") || collider.CompareTag("Player2") || collider.CompareTag("Player3") || collider.CompareTag("Player4"))
        {
            Vector3 currentPosition = collider.transform.position;
            Quaternion posQuaternion = collider.transform.rotation;
            float tempPosX = currentPosition.x;

            // Calcola la nuova posizione sull'asse opposto
            if (Mathf.Abs(currentPosition.x) > mapLimits.x)
            {
                currentPosition.x = -currentPosition.x; // Posizione sull'altro lato di X
            }

            if (Mathf.Abs(currentPosition.y) > mapLimits.y)
            {
                currentPosition.y = -currentPosition.y; // Posizione sull'altro lato di Y
            }

            // Determina quale prefab utilizzare e distrugge il vecchio GameObject
            if (collider.CompareTag("Player1"))
            {
                Destroy(collider.gameObject); // Distrugge il giocatore attuale
                if (tempPosX >= 0)
                {
                    Instantiate(player1Prefab, currentPosition, Quaternion.identity); // Crea il nuovo Player1
                }else
                { 
                    Instantiate(player1Prefab, currentPosition, posQuaternion); // Crea il nuovo Player1
                }
            }
            else if (collider.CompareTag("Player2"))
            {
                Destroy(collider.gameObject); // Distrugge il giocatore attuale
                if (tempPosX >= 0)
                {
                    Instantiate(player2Prefab, currentPosition, Quaternion.identity); // Crea il nuovo Player1
                }else
                { 
                    Instantiate(player2Prefab, currentPosition, posQuaternion); // Crea il nuovo Player1
                }
            }
            else if (collider.CompareTag("Player3"))
            {
                Destroy(collider.gameObject); // Distrugge il giocatore attuale
                if (tempPosX >= 0)
                {
                    Instantiate(player3Prefab, currentPosition, Quaternion.identity); // Crea il nuovo Player1
                }else
                { 
                    Instantiate(player3Prefab, currentPosition, posQuaternion); // Crea il nuovo Player1
                }
            }
            else if (collider.CompareTag("Player4"))
            {
                Destroy(collider.gameObject); // Distrugge il giocatore attuale
                if (tempPosX >= 0)
                {
                    Instantiate(player4Prefab, currentPosition, Quaternion.identity); // Crea il nuovo Player1
                }else
                { 
                    Instantiate(player4Prefab, currentPosition, posQuaternion); // Crea il nuovo Player1
                }
            }
        }
    }
}
