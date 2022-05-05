using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    int inputKeyIndex;
    public float jumpForce = 2.5f;
    Vector2 step;

    bool animIsPlaying;//解决连点问题
    public LayerMask whatIsGoal;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        step = FindObjectOfType<GameManager>().step;
    }

    // Update is called once per frame
    void Update()
    {
        if (animIsPlaying) return;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            inputKeyIndex = 1;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            inputKeyIndex = -1;
    }
    private void FixedUpdate()
    {
        if (inputKeyIndex != 0&&!animIsPlaying)
        {
            var jumpAnim = rb.DOJump(rb.position + new Vector2(step.x * inputKeyIndex, step.y), jumpForce, 1, 0.15f).SetEase(Ease.OutCubic).OnComplete(()=> { animIsPlaying = false; });
            inputKeyIndex = 0;
            animIsPlaying = jumpAnim.IsPlaying();
        }
        // animIsPlaying = false;
        if (!animIsPlaying)
        {
            if (Physics2D.CircleCast(transform.position, 0.2f, Vector2.zero, 0f, whatIsGoal))
                print("You Win! Your Time is : " + Time.timeSinceLevelLoad);
            if (!Physics2D.CircleCast(transform.position, 0.2f, Vector2.zero, 0f))
            {
                print("Game Over");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
                
        }
    }
    
}
