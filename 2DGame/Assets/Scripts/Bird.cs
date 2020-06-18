using System.Collections; //list of objects
using System.Collections.Generic; //list of birds
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    Vector3 _initialPosition;
    private bool _birdWasLaunched;
    private float _timeSittingAround;

    [SerializeField] private float _launchPower = 500; //serializefiedl apare pe unity ca sa poti modifica valoarea
    

    private void Awake() //save off initial position
    {
        _initialPosition = transform.position; // vom lua pozitia initiala si o vom salva in initialPosition
    }

    private void Update()
    {
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition);
        
        if (_birdWasLaunched && 
            GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1) //velocity=viteza vector2 cat de repede se misca pe x axis si pe y axis,magnitude= cate de repede se misca combinat.
        {
            _timeSittingAround += Time.deltaTime;
        }

        // verifica pozitia si vezi daca pozitia fiecarui frame a iesit din limita acceptabila a bounds
        if (transform.position.y > 10 ||
            transform.position.y < -10 ||
            transform.position.x > 12 ||
            transform.position.x < -13 ||
            _timeSittingAround > 3)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<LineRenderer>().enabled = true;
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        
        Vector2 directionToInitialPosition = _initialPosition - transform.position;// pozitia initiala - pozitia curenta
        
        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * _launchPower);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        _birdWasLaunched = true;

        GetComponent<LineRenderer>().enabled = false;
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y); //nu se va modifica pozitia Z
    }
}
