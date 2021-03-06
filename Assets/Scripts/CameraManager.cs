using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    static public CameraManager instance;

    public GameObject target;   //카메라가 따라갈 대상
    public float moveSpeed;     //카메라 속도
    private Vector3 targetPosition; //대상 현재 위치 값

    public BoxCollider2D bound;

    // 박스 컬라이더 영역의 최소 최대 x, y, z 값을 지님.
    private Vector3 minBound;
    private Vector3 maxBound;

    // 카메라의 반너비, 반높이 값을 지닐 변수.
    private float halfWidth;
    private float halfHeight;

    // 카메라의 반높이값을 구할 속성을 이용하기 위한 변수
    private Camera theCamera;

    // start보다 awake가 먼저 실행됨.
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject); //카메라 파괴 방지   
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        theCamera = GetComponent<Camera>();
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height; // 반너비 구하는 공식
    }

    // Update is called once per frame
    void Update()
    {
        if (target.gameObject != null)  //카메라 대상이 있다면
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);

            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);    //1초에 moveSpeed만큼 이동

            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);
        }
    }

    public void SetBound(BoxCollider2D newBound)
    {
        bound = newBound;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }
}
