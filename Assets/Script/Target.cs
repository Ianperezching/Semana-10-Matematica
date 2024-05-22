using UnityEngine;

public class Target : MonoBehaviour
{
    public int points = 10; 

   
    private void OnTriggerEnter(Collider other)
    {
      
        if (other.gameObject.CompareTag("Projectile"))
        {
          
            ScoreManager.instance.AddPoints(points);
        
            Destroy(other.gameObject);
           
            Destroy(gameObject);
        }
    }
   
}
