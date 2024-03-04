using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_UnitMovement : MonoBehaviour
{
    public GameObject mouseEffect;
    Unit unit;
    Tank_fsm tank_fsm;
    public Vector2Int bottomLeft, topRight, startPos, targetPos;
    public List<Node> FinalNodeList;
    public bool allowDiagonal, dontCrossCorner;
    public bool selected = false;
    int sizeX, sizeY;
    Node[,] NodeArray;
    Node StartNode, TargetNode, CurNode;
    List<Node> OpenList, ClosedList;
    public Transform StartTR;
    public Vector2Int TargetTR;
    public GameObject Des;
    public bool arrived = true;
    public bool aClick = false;
    public bool only_move = false;
    float MaxDistance = 15f;
    Vector3 MousePosition;
    Camera Camera;
    GameObject gameManager;
    private Camera myCam;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myCam = Camera.main;
        unit = GetComponent<Unit>();
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        StartTR = GetComponent<Transform>();
        tank_fsm = GetComponent<Tank_fsm>();
        Des = GameObject.Find("Destination");
        gameManager = GameObject.Find("GameManager");
        bottomLeft.x = -25;
        bottomLeft.y = -15;
        topRight.x = 25;
        topRight.y = 15;
    }
    public void PathFinding()
    {
        startPos = Vector2Int.RoundToInt(StartTR.position);
        // NodeArray�� ũ�� �����ְ�, isWall, x, y ����
        sizeX = topRight.x - bottomLeft.x + 1;
        sizeY = topRight.y - bottomLeft.y + 1;
        NodeArray = new Node[sizeX, sizeY];

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                bool isWall = false;
                foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(i + bottomLeft.x, j + bottomLeft.y), 0.4f))
                    if (col.gameObject.layer == LayerMask.NameToLayer("Wall")) isWall = true;

                NodeArray[i, j] = new Node(isWall, i + bottomLeft.x, j + bottomLeft.y);
            }
        }


        // ���۰� �� ���, ��������Ʈ�� ��������Ʈ, ����������Ʈ �ʱ�ȭ
        StartNode = NodeArray[startPos.x - bottomLeft.x, startPos.y - bottomLeft.y];
        TargetNode = NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y];

        OpenList = new List<Node>() { StartNode };
        ClosedList = new List<Node>();
        FinalNodeList = new List<Node>();


        while (OpenList.Count > 0)
        {
            // ��������Ʈ �� ���� F�� �۰� F�� ���ٸ� H�� ���� �� ������� �ϰ� ��������Ʈ���� ��������Ʈ�� �ű��
            CurNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
                if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H) CurNode = OpenList[i];

            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);


            // ������
            if (CurNode == TargetNode)
            {
                Node TargetCurNode = TargetNode;
                while (TargetCurNode != StartNode)
                {
                    FinalNodeList.Add(TargetCurNode);
                    TargetCurNode = TargetCurNode.ParentNode;
                }
                FinalNodeList.Add(StartNode);
                FinalNodeList.Reverse();

                //for (int i = 0; i < FinalNodeList.Count; i++) print(i + "��°�� " + FinalNodeList[i].x + ", " + FinalNodeList[i].y);
                return;
            }


            // �֢آע�
            if (allowDiagonal)
            {
                OpenListAdd(CurNode.x + 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y - 1);
                OpenListAdd(CurNode.x + 1, CurNode.y - 1);
            }

            // �� �� �� ��
            OpenListAdd(CurNode.x, CurNode.y + 1);
            OpenListAdd(CurNode.x + 1, CurNode.y);
            OpenListAdd(CurNode.x, CurNode.y - 1);
            OpenListAdd(CurNode.x - 1, CurNode.y);
        }
    }

    void OpenListAdd(int checkX, int checkY)
    {
        // �����¿� ������ ����� �ʰ�, ���� �ƴϸ鼭, ��������Ʈ�� ���ٸ�
        if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.y && checkY < topRight.y + 1 && !NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isWall && !ClosedList.Contains(NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]))
        {
            // �밢�� ����, �� ���̷� ��� �ȵ�
            if (allowDiagonal) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall && NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;

            // �ڳʸ� �������� ���� ������, �̵� �߿� �������� ��ֹ��� ������ �ȵ�
            if (dontCrossCorner) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall || NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;

            // �̿��忡 �ְ�, ������ 10, �밢���� 14���
            Node NeighborNode = NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
            int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 14);


            // �̵������ �̿���G���� �۰ų� �Ǵ� ��������Ʈ�� �̿��尡 ���ٸ� G, H, ParentNode�� ���� �� ��������Ʈ�� �߰�
            if (MoveCost < NeighborNode.G || !OpenList.Contains(NeighborNode))
            {
                NeighborNode.G = MoveCost;
                NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetNode.x) + Mathf.Abs(NeighborNode.y - TargetNode.y)) * 10;
                NeighborNode.ParentNode = CurNode;

                OpenList.Add(NeighborNode);
            }
        }
    }

    void OnDrawGizmos()
    {
        if (FinalNodeList.Count != 0) for (int i = 0; i < FinalNodeList.Count - 1; i++)
                Gizmos.DrawLine(new Vector2(FinalNodeList[i].x, FinalNodeList[i].y), new Vector2(FinalNodeList[i + 1].x, FinalNodeList[i + 1].y));
    }

    void Update()
    {
        if (selected == true && !unit.newChar)
        {
            if (Input.GetMouseButtonDown(1))
            {
                MousePosition = Input.mousePosition;
                MousePosition = myCam.ScreenToWorldPoint(MousePosition);
                GameObject mouse = Instantiate(mouseEffect);
                mouse.GetComponent<Transform>().position = new Vector3(MousePosition.x, MousePosition.y, 0);
            }
            if (Input.GetMouseButton(1) && gameManager.GetComponent<GameManager>().isBattle == false)
            {
                MousePosition = Input.mousePosition;
                MousePosition = myCam.ScreenToWorldPoint(MousePosition);
                LayerMask layerMask = LayerMask.GetMask("ReadyArea");
                RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, MaxDistance, layerMask);
                Physics2D.queriesHitTriggers = true;

                if (hit.collider != null)
                {
                    tank_fsm.target = null;
                    only_move = true;
                    TargetTR = new Vector2Int((int)Des.transform.position.x, (int)Des.transform.position.y);
                    //TargetTR = EmptyPathfinding(TargetTR);
                    targetPos = Vector2Int.RoundToInt(TargetTR);
                    arrived = false;
                }
                //GameObject mouse = Instantiate(mouseEffect);
                //mouse.GetComponent<Transform>().position = new Vector3(MousePosition.x, MousePosition.y, 0);
            }
            else if (Input.GetMouseButton(1) && gameManager.GetComponent<GameManager>().isBattle == true)
            {
                MousePosition = Input.mousePosition;
                MousePosition = Camera.ScreenToWorldPoint(MousePosition);
                RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, MaxDistance);
                if (hit)
                {
                    Des.transform.position = MousePosition;
                    TargetTR = new Vector2Int((int)Des.transform.position.x, (int)Des.transform.position.y);
                    targetPos = Vector2Int.RoundToInt(TargetTR);
                    only_move = false;
                    arrived = false;
                }
                else
                {
                    tank_fsm.target = null;
                    only_move = true;
                    TargetTR = new Vector2Int((int)Des.transform.position.x, (int)Des.transform.position.y);
                    //TargetTR = EmptyPathfinding(TargetTR);
                    targetPos = Vector2Int.RoundToInt(TargetTR);
                    arrived = false;
                }
            }

            if (tank_fsm.target == null)
            {
                if (aClick == true)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (tank_fsm.fight == false)
                        {
                            MousePosition = Input.mousePosition;
                            MousePosition = Camera.ScreenToWorldPoint(MousePosition);
                            RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, MaxDistance);
                            if (!hit)
                            {
                                Des.transform.position = MousePosition;
                                TargetTR = new Vector2Int((int)Des.transform.position.x, (int)Des.transform.position.y);
                                targetPos = Vector2Int.RoundToInt(TargetTR);
                                only_move = false;
                                arrived = false;
                                aClick = false;
                            }
                            GameObject mouse = Instantiate(mouseEffect);
                            mouse.GetComponent<Transform>().position = new Vector3(MousePosition.x, MousePosition.y, 0);
                        }
                    }
                }
            }
        }
        if (arrived == false)
        {
            //ChangeMass();
            if (Input.GetKeyDown("s")) 
            {
                PlayerStop();
            }
            PathFinding();
        }
        else
        {
            rb.mass = 0.1f;
            only_move = false;
        }

        if (FinalNodeList.Count - 1 != 0 && FinalNodeList.Count - 1 >= 0)
        {
            Vector2 target = new Vector2(FinalNodeList[1].x, FinalNodeList[1].y);
            StartTR.position = Vector2.MoveTowards(StartTR.position, target, unit.speed * Time.deltaTime);
        }
        else if (FinalNodeList.Count - 1 == 0)
        {
            if (startPos.x == targetPos.x && startPos.y == targetPos.y)
            {
                arrived = true;
            }
        }
    }
    public void PlayerStop()
    {
        TargetTR = new Vector2Int((int)gameObject.transform.position.x, (int)gameObject.transform.position.y);
        targetPos = Vector2Int.RoundToInt(TargetTR);
        arrived = true;
    }

    public void GoEnemy()
    {
        arrived = false;
        only_move = false;
        TargetTR = new Vector2Int((int)tank_fsm.target.gameObject.transform.position.x, (int)tank_fsm.target.gameObject.transform.position.y);
        targetPos = Vector2Int.RoundToInt(TargetTR);
    }

    //void ChangeMass()
    //{
    //    Vector2 now = new Vector2(transform.position.x, transform.position.y);
    //    Vector2 next = new Vector2(targetPos.x, targetPos.y);
    //    float length = Vector2.Distance(now, next);
    //    rb.mass = 10 / length;
    //}

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    string tag = collision.gameObject.tag;
    //    if (tag.StartsWith("dps"))
    //    {
    //        if (targetPos == collision.gameObject.GetComponent<UnitMovement>().targetPos && collision.gameObject.GetComponent<UnitMovement>().arrived == true)
    //        {
    //            print("1");
    //            FinalNodeList.Clear();
    //            PlayerStop();
    //        }
    //    }
    //    else if (tag.StartsWith("heal"))
    //    {
    //        if (targetPos == collision.gameObject.GetComponent<Heal_Unitmovement>().targetPos && collision.gameObject.GetComponent<Heal_Unitmovement>().arrived == true)
    //        {
    //            print("1");
    //            FinalNodeList.Clear();
    //            PlayerStop();
    //        }
    //    }
    //    else if (tag.StartsWith("tank"))
    //    {
    //        if (targetPos == collision.gameObject.GetComponent<Tank_UnitMovement>().targetPos && collision.gameObject.GetComponent<Tank_UnitMovement>().arrived == true)
    //        {
    //            print("tank");
    //            FinalNodeList.Clear();
    //            PlayerStop();
    //        }
    //    }
    //}
}
