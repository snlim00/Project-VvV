using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] itemArr;
    [SerializeField] private GameObject[] healItemArr;
    [SerializeField] private GameObject[] attackItemArr;

    // Start is called before the first frame update
    void Start()
    {
        ItemGeneration();
    }

    List<int> index = new List<int>();
    public void ItemGeneration()
    {
        index.Clear();
        for(int i = 0; i < 3; ++i)
        {
            int a = Random.Range(0, itemArr.Length);

            while(CheckIndex(a) == true)
            {
                a = Random.Range(0, itemArr.Length);
            }
            index.Add(a);

            Item go = Instantiate(itemArr[a]).GetComponent<Item>();
            go.SetPosition(i);
        }
    }

    bool CheckIndex(int a)
    {
        for (int j = 0; j < index.Count; ++j)
        {
            if (a == index[j])
                return true;
        }

        return false;
    }

    public void HealItemGeneration(Vector2 pos)
    {
        Item go = Instantiate(healItemArr[Random.Range(0, healItemArr.Length)]).GetComponent<Item>();
        go.SetPosition(pos);
    }

    public void AttackItemGeneration(Vector2 pos)
    {
        Item go = Instantiate(attackItemArr[Random.Range(0, attackItemArr.Length)]).GetComponent<Item>();
        go.SetPosition(pos);
    }
}
