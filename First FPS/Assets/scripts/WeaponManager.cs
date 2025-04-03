using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject currgun;//当前手中拿到的武器

    public GameObject currmaingun;//当前玩家拥有的主武器这里的主武器指的是玩家身上的武器

    private void Awake()
    {
        currmaingun = null;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //武器切换
    public void CuttingGun(int gunkey)
    {
        if (gunkey == 1 && currmaingun != null)
        {
                currgun = currmaingun;
        }

        if (gunkey == 2)
        {
            currgun = gameObject.transform.Find("Virtual Camera/gunstore/p2000").gameObject;
        }
    }
    //拾起主武器
    public void havemaingun(GameObject maingun)
    {
        if (maingun != null)
        {
            if (currmaingun == null)
            {
                currmaingun = gameObject.transform.Find($"Virtual Camera/gunstore/{maingun.name}").gameObject;
                //currmaingun = maingun;//这里不能直接等于是因为maingun是场景武器，不是角色身上的武器。
                currgun = currmaingun;  
                gameObject.transform.Find("Virtual Camera/gunstore/p2000").gameObject.SetActive(false);
                gameObject.transform.Find($"Virtual Camera/gunstore/{maingun.name}").gameObject.SetActive(true);
            }
            else
            {
                if (currmaingun != maingun)
                {
                    gameObject.transform.Find("Virtual Camera/gunstore/p2000").gameObject.SetActive(false);
                    gameObject.transform.Find($"Virtual Camera/gunstore/{currmaingun.name}").gameObject.SetActive(false);
                    gameObject.transform.Find($"Virtual Camera/gunstore/{maingun.name}").gameObject.SetActive(true);
                    currmaingun = gameObject.transform.Find($"Virtual Camera/gunstore/{maingun.name}").gameObject;
                    //currmaingun = maingun;
                    currgun = currmaingun;
                }
            }
        }
    }
}
