using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47item : MonoBehaviour
{
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
    } 
    
    private void HandleWeaponVisibility(Collider other)
    {
        other.gameObject.GetComponent<WeaponManager>().havemaingun(this.gameObject);
    }
    // void OnTriggerEnter(Collider other)  
    // {  
    //     // 确保其他对象不为 null  
    //     if (other == null) return;  
    //
    //     // 检查 GameManager 中的 currgunitem  
    //     if (GameManager.Instance.currgunitem == null)  
    //     {  
    //         // 隐藏和显示相应的武器  
    //         HandleWeaponVisibility(other, "p2000", "ak47", 
    //             "m4a4", false, true, false);  
    //         DeactivateCurrentGameObject();  
    //     }  
    //     else  
    //     {  
    //         if (GameManager.Instance.currgunitem != this.gameObject)  
    //         {  
    //             HandleWeaponVisibility(other, "p2000", "ak47", 
    //                 "m4a4", false, true, false);  
    //             GameObject agogunitem = GameManager.Instance.currgunitem;
    //             DeactivateCurrentGameObject();  
    //             // 更新替换下来武器的位置并激活  
    //             agogunitem.transform.position = this.transform.position+new Vector3(1,0,1);  
    //             agogunitem.SetActive(true); 
    //         }  
    //     }  
    //     //更换动画器
    //     other.GetComponentInParent<PlayerController>().animator = other.gameObject.transform.
    //         Find("Virtual Camera/gunstore/ak47").GetComponent<Animator>();
    // } 
    //
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
    //
    // private void DeactivateCurrentGameObject()  
    // {  
    //     GameObject currgameobject = this.gameObject;  
    //     currgameobject.SetActive(false);  
    //     GameManager.Instance.currgunitem = currgameobject;  
    // }  
}
