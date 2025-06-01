using UnityEngine;

public class FoodObject : CellObject

{

    public int AmountGranted = 1;

    public override void PlayerEntered()
    {
        Destroy(gameObject);

        GameManager.Instance.ChangeFood(AmountGranted);

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
