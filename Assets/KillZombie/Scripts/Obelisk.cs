using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Obelisk : MonoBehaviour
{
    public List<GameObject> tasks= new List<GameObject>();
    public List<string> names = new List<string>();

    public Material taskMaterial;

    [SerializeField] GameObject temp;

    public int number = 0;

    public bool gameComplete;

    void Start()
    {
        
    }

    void Update()
    {
        if(number == 5 && gameObject.activeInHierarchy)
        {
            gameComplete= true;
            Invoke(nameof(HideObelisk), 1f);
        }
    }

    public void putGameobject()
    {
        if(names.Count > 0)
        {
            foreach (var item in tasks)
            {
                if (names.Contains(item.name))
                {
                   print("burada");
                   temp = tasks.Where(obj=>obj.name == item.name).SingleOrDefault();
                   temp.GetComponent<Renderer>().material = taskMaterial;
                   number++;
                   names.Remove(item.name);
                }
            }
        }
    }

    void HideObelisk()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void getStringName(string name)
    {
        names.Add(name);
    }
}
