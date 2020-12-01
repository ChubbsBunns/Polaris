using UnityEngine;

public class LevelFader : MonoBehaviour
{
    public Animator anim;

    public void FadeToLevel ()
    {
        anim.SetTrigger("levelFader");
    }
}
