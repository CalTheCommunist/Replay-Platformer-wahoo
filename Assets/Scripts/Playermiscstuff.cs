using UnityEngine;
using UnityEngine.SceneManagement;

public class Playermiscstuff : MonoBehaviour
{
    [SerializeField] private bool reachedGoal;
    private bool isDead;
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        //na na ha ha imagine dying lmao couldn't be me LOLOLO this loads the lose scene if you hadn't noticed (temporary until i find something better.)
        if (collision.gameObject.CompareTag("killbox"))
        {
            SceneManager.LoadScene("loseScreen");
        }
        //oh wow you won, good job or sum like that yadda-yadda loads the win scene.
        if (collision.gameObject.CompareTag("Goal"))
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
}
