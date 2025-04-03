using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class M4a4item : MonoBehaviour
{
    // void OnTriggerEnter(Collider other)  
    // {
    //     if (GameManager.Instance.currgunitem == null)
    //     {
    //         Transform p2000Transform = other.transform.Find("Virtual Camera/gunstore/p2000");
    //         p2000Transform.GetComponent<GameObject>().SetActive(false); 
    //         Transform ak47Transform = other.transform.Find("Virtual Camera/gunstore/ak47");
    //         ak47Transform.GetComponent<GameObject>().SetActive(false); 
    //         Transform m4a4Transform = other.transform.Find("Virtual Camera/gunstore/m4a4");
    //         m4a4Transform.GetComponent<GameObject>().SetActive(true);
    //         GameObject currgameobject = this.gameObject;
    //         currgameobject.SetActive(false);
    //         GameManager.Instance.currgunitem = currgameobject;
    //     }
    //     else
    //     {
    //         if (GameManager.Instance.currgunitem != this.gameObject)
    //         {
    //             Transform p2000Transform = other.transform.Find("Virtual Camera/gunstore/p2000");
    //             p2000Transform.GetComponent<GameObject>().SetActive(false); 
    //             Transform ak47Transform = other.transform.Find("Virtual Camera/gunstore/ak47");
    //             ak47Transform.GetComponent<GameObject>().SetActive(false); 
    //             Transform m4a4Transform = other.transform.Find("Virtual Camera/gunstore/m4a4");
    //             m4a4Transform.GetComponent<GameObject>().SetActive(true);
    //             GameObject currgameobject = this.gameObject;
    //             currgameobject.SetActive(false);
    //             GameManager.Instance.currgunitem = currgameobject;
    //             GameManager.Instance.currgunitem.transform.position = other.transform.position;
    //             GameManager.Instance.currgunitem.SetActive(true);
    //         }
    //     }
    // }
        void OnTriggerEnter(Collider other)  
    {  
        // 确保其他对象不为 null  
        if (other == null) return;  
        
        GameObject agogunitem = GameManager.Instance.havemaingun(this.gameObject);

        if (agogunitem == null)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            if (agogunitem != this.gameObject)
            {
                this.gameObject.SetActive(false);
                // 更新替换下来武器的位置并激活  
                agogunitem.transform.position = this.transform.position+new Vector3(1,0,1);   
                agogunitem.SetActive(true); 
            }
        }
        HandleWeaponVisibility(other);

        // // 检查 GameManager 中的 currgunitem  
        // if (GameManager.Instance.currgunitem == null)  
        // {  
        //     // 隐藏和显示相应的武器  
        //     // HandleWeaponVisibility(other, "p2000", "ak47", 
        //     //     "m4a4", false, false, true);  
        //     HandleWeaponVisibility(other);
        //     DeactivateCurrentGameObject();  
        // }  
        // else  
        // {  
        //     if (GameManager.Instance.currgunitem != this.gameObject)  
        //     {  
        //         // HandleWeaponVisibility(other, "p2000", "ak47", 
        //         //     "m4a4", false, false, true); 
        //         HandleWeaponVisibility(other);
        //         GameObject agogunitem = GameManager.Instance.currgunitem;
        //         DeactivateCurrentGameObject();  
        //         // 更新替换下来武器的位置并激活  
        //         agogunitem.transform.position = this.transform.position+new Vector3(1,0,1);   
        //         agogunitem.SetActive(true); 
        //     }  
        // }  
        // //更换动画器
        // other.GetComponentInParent<PlayerController>().animator = other.gameObject.transform.
        //     Find("Virtual Camera/gunstore/m4a4").GetComponent<Animator>();
    } 
    
    // private void HandleWeaponVisibility(Collider other, string weaponToDeactivate, string weaponToActivate, string otherWeaponToDeactivate, bool deactivate, bool activate, bool otherDeactivate)  
    // {  
    //     Transform weaponToDeactivateTransform = other.transform.Find($"Virtual Camera/gunstore/{weaponToDeactivate}");  
    //     if (weaponToDeactivateTransform != null)  
    //     {  
    //         weaponToDeactivateTransform.gameObject.SetActive(deactivate); // 直接使用 gameObject  
    //     }  
    //
    //     Transform weaponToActivateTransform = other.transform.Find($"Virtual Camera/gunstore/{weaponToActivate}");  
    //     if (weaponToActivateTransform != null)  
    //     {  
    //         weaponToActivateTransform.gameObject.SetActive(activate); // 直接使用 gameObject  
    //     }  
    //
    //     Transform otherWeaponTransform = other.transform.Find($"Virtual Camera/gunstore/{otherWeaponToDeactivate}");  
    //     if (otherWeaponTransform != null)  
    //     {  
    //         otherWeaponTransform.gameObject.SetActive(otherDeactivate); // 直接使用 gameObject  
    //     }  
    // }  

    // private void DeactivateCurrentGameObject()  
    // {  
    //     GameObject currgameobject = this.gameObject;  
    //     currgameobject.SetActive(false);  
    //     GameManager.Instance.currgunitem = currgameobject;  
    // }

    private void HandleWeaponVisibility(Collider other)
    {
        other.gameObject.GetComponent<WeaponManager>().havemaingun(this.gameObject);
    }
}
