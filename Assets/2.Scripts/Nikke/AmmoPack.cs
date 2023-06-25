using UnityEngine;
using UnityEngine.UI;
// 총알을 충전하는 아이템
public class AmmoPack : MonoBehaviour, IItem {
    public int ammo = 300; // 충전할 총알 수
    public Text ammoText;
    public int ammoRemain; // 남은 전체 탄약
    public int magAmmo; // 현재 탄창에 남아있는 탄약
    public void Use(GameObject target) {
        // 전달 받은 게임 오브젝트로부터 PlayerShooter 컴포넌트를 가져오기 시도
        MGFire MG = target.GetComponent<MGFire>();
        //ammoText = GetComponent<UIManager>().ammoText;
        ammoRemain = target.GetComponent<MGFire>().ammoRemain;
        magAmmo = target.GetComponent<MGFire>().magAmmo;
        // PlayerShooter 컴포넌트가 있으며, 총 오브젝트가 존재하면
        if (MG != null && MG.currentGun != null)
        {
            // 총의 남은 탄환 수를 ammo 만큼 더한다
            MG.ammoRemain += ammo;
            ammoText.text = magAmmo + "/" + ammoRemain;
            Debug.Log("ammo get");

        }

        // 사용되었으므로, 자신을 파괴
        //Destroy(gameObject);
    }
}