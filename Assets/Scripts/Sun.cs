using UnityEngine;

public class Sun : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private void Awake()
    {
        animator.SetTrigger("Rotate");
    }
}
