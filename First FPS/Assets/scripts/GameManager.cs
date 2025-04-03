using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //游戏总管理，单例模式
    public static GameManager Instance;
    
    public GameObject currgunitem;//当前拥有的主武器，这里的主武器指的是场景中的武器。

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        currgunitem = null;
    }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //拾取到主武器
    public GameObject havemaingun(GameObject maingun)
    {
        if (currgunitem == null)
        {
            currgunitem = maingun;
            return null;
        }
        else
        {
            if (currgunitem != maingun)
            {
                GameObject agogunitem = currgunitem;
                currgunitem = maingun;
                return agogunitem;
            }

            return maingun;
        }
    }
}
