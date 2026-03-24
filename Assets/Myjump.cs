    using UnityEngine;
    using UnityEngine.UI;

public class Myjmp : MonoBehaviour
{
    public Rigidbody rigidbody;
    public float power = 20f;
    public float timer = 0;
    public Text TextUI;

    void Start()
    {
        
    }

    void Update()
    {
        timer = timer + Time.deltaTime;
        TextUI.text = timer.ToString();
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            power = power + Random.Range(-100, 200);
            rigibody.AddForce(transform.up * Power);
        }
         if (this.gameObject.transform.position.y < 5 || this.gameObject.transform.position.y < -3)
        {
            Destroy(this.gameobject);
        }

            
    }
        
    

