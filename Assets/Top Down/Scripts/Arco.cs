using UnityEngine;

public class Arco : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire; //Se o jogador pode atirar ou não
    private float timer;
    public float timeBetweenFiring = 5f;
    

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
       mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (!canFire){

            timer += Time.deltaTime;

            if(timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;  
            }

        }

        if(Input.GetMouseButton(0) && canFire)
        {
            canFire = false;// não permite o jogador atirar imediatamente
            SoundManager.PlaySound(SoundType.ARCO);
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);

        }

    }
}
