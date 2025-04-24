using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    //[SerializeField] private AudioClip checkpointSound; //checkpoint Sound!
    private Transform currentCheckPoint; //Stores the Latest checkpoint
    private Health playerHealth; //Reset Health upon Respawn

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
    }

    public void Respawn()
    {
        transform.position = currentCheckPoint.position; //Move player to last checkpoint position
       // playerHealth.Respawn(); //Restore player health and restore animations

        //Move camera to checkpoint (for ts to w*rk we need to place it as child of the room)
    }

    //Activate checkpoint

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.transform.tag == "CheckPoint")
       {
              currentCheckPoint = collision.transform; //Stores the checkpoint that we activated
              //SoundManager.instance.PlaySound(checkpointSound) //checkpoint Sound!
              collision.GetComponent<Collider2D>().enabled = false; //Deactivate checkpoint collider
              //collision.GetComponent<Animator>().SetTrigger("appear"); //Trigger checkpoint animation
       }
    }
}
