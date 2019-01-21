using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;
using Priority_Queue;

namespace PathSearching1
{
    public partial class Form1 : Form
    {
        enum VStatus { UNDISCOVERED, VISITED };
        class Vertex : FastPriorityQueueNode
        {
            public int ID;
            public int x, y;
            public int type; //墙、路、起点、终点
            public int depth = 0;
            public double GCost;       //当前代价
            public double HCost;       //启发函数
            public int vCost = 1;      //走到该点需要花费的代价
            public VStatus status = VStatus.UNDISCOVERED;//访问情况，0为访问，1位未访问
            private Vertex Parent = null;
            public Vertex(int _x, int _y)
            {
                this.x = _x;
                this.y = _y;
            }
            public void SetParent(Vertex P)
            {
                this.Parent = P;
            }
            public Vertex GetParent()
            {
                return Parent;
            }
        }

        private int MapSize;
        private int ButtonSize = 20;
        private int Mode = 0; //0位设置障碍，1为设置起点，2为设置终点
        private int ModeSearch = 0; // 0为BFS，1为双向BFS，3为A*
        private int ModeDistance = 1; //0为欧式距离，1为曼哈顿距离，2为对角距离
        private int ModeDirection = 0; //0为四方向搜索，1为八方向搜索
        private int ModeObstale = 4; //0为灯牌，1为新浪微博，2为暖宝宝，3为老王，4为墙
        private bool ObsShow = true; //显示障碍物与不显示障碍物
        private int StartId;  //起点id 
        private int EndId;    //终点id
        private bool StartSet = false;  //是否设置起点
        private bool EndSet = false;    //是否设置终点
        private bool mulEndP = false;   //是否多个终点
        private bool EndPtrans = false; //是否将单个终点转移到多终点内
        private List<Vertex> EndPs = new List<Vertex>();
        int[,] mapData; // 0位路，1为墙
        public List<Button> MapButton = new List<Button>();
        public Form1()
        {
            InitializeComponent();
        }
        //重置游戏
        private void GameReset()
        {
            StartSet = false;
            EndSet = false;
            for (int i = 0; i < MapButton.Count(); i++)
            {
                MapButton[i].Dispose();
            }
            MapButton.Clear();
            EndPNum.Text = "0";
            EndPs.Clear();
        }
        //地图重置，即只保留起点、终点和障碍物
        private void MapReset()
        {
            for (int i = 0; i < MapButton.Count(); i++)
            {
                if (MapButton[i].BackColor != Color.Red &&
                    MapButton[i].BackColor != Color.Blue && MapButton[i].BackColor != Color.Purple)
                {
                    MapButton[i].BackColor = SystemColors.Control;
                }
                MapButton[i].Text = "";
            }
        }
        private void RunButton_Click(object sender, EventArgs e)
        {
            GameReset();
            MapSize = Convert.ToInt16(MapSizeTxt.Text);
            mapData = new int[MapSize, MapSize];
            for (int i = 0; i < MapSize; i++)
                for (int j = 0; j < MapSize; j++)
                {
                    Button bt = new Button();
                    bt.BackgroundImageLayout = ImageLayout.Stretch;
                    bt.Font = new Font("宋体", 6);
                    bt.Click += SetPath_Click;
                    bt.Name = Convert.ToString(i * MapSize + j);
                    bt.Location = new Point((j + 1) * ButtonSize, (i + 1) * ButtonSize);
                    bt.Size = new Size(ButtonSize, ButtonSize);
                    this.ButtonPanel.Controls.Add(bt);
                    MapButton.Add(bt);
                }
        }
        private void BFS(Vertex StatP, Vertex EndP)
        {
            List<Vertex> visitedNode = new List<Vertex>();
            List<Vertex> PathNode = new List<Vertex>();
            int[] dx;
            int[] dy;
            
            switch (ModeDirection)
            {
                case 0:
                    //四方向搜索
                    dx = new int[4];
                    dy = new int[4];
                    dx[0] = -1; dy[0] = 0;
                    dx[1] = 0; dy[1] = 1;
                    dx[2] = 0; dy[2] = -1;
                    dx[3] = 1; dy[3] = 0;
                    break;
                case 1:
                    //八方向搜索
                    dx = new int[8] { -1, 0, 1, -1, 1, -1, 0, 1 };
                    dy = new int[8] { 1, 1, 1, 0, 0, -1, -1, -1 };
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }

            
            //记录Status
            int[,] MatStatus = new int[MapSize, MapSize];
            Queue<Vertex> Q = new Queue<Vertex>();
            MatStatus[StatP.x, StatP.y] = 1;
            Q.Enqueue(StatP);
            visitedNode.Add(StatP);
            bool isfind = false;
            while (Q.Count() != 0)
            {
                Vertex vCurr = Q.Dequeue();

                for (int i = 0; i < dx.Length; i++)
                {
                    Vertex vNear = new Vertex(vCurr.x + dx[i], vCurr.y + dy[i]);

                    if (vNear.x < 0 || vNear.y < 0 || vNear.x >= MapSize || vNear.y >= MapSize)
                        continue;
                    //若未访问过且没有墙
                    if (MatStatus[vNear.x, vNear.y] == 0 && mapData[vNear.x,vNear.y] != 1)
                    {
                        vNear.ID = vNear.x * MapSize + vNear.y;
                        MatStatus[vNear.x, vNear.y] = 1; //访问
                        vNear.SetParent(vCurr); //可能出错...
                        Q.Enqueue(vNear);
                        visitedNode.Add(vNear);
                        if (vNear.x == EndP.x && vNear.y == EndP.y)
                        {
                            isfind = true;
                            Console.WriteLine("Find!");
                            break;
                        }
                        MapButton[vNear.x * MapSize + vNear.y].BackColor = Color.Gray;
                    }
                }
                MatStatus[vCurr.x, vCurr.y] = 1;
                if (isfind)
                {
                    Vertex pNode = visitedNode[visitedNode.Count() - 1];
                    while (pNode.x != StatP.x || pNode.y != StatP.y)
                    {
                        PathNode.Add(pNode);
                        pNode = pNode.GetParent();
                    }
                    PathNode.Add(StatP);
                    break;
                }
            }
            if(!isfind)
            {
                MessageBox.Show("不存在搜索路径！");
            }
            PathNode.Reverse();
            for (int i = 1; i < PathNode.Count() - 1; i++)
            {
                Console.Write(PathNode[i].x);
                Console.Write(',');
                Console.WriteLine(PathNode[i].y);
                MapButton[PathNode[i].x * MapSize + PathNode[i].y].BackColor = Color.Green;
            }
        }
        //双向BFS
        private void BiBFS(Vertex StatP, Vertex EndP)
        {
            List<Vertex> visitedNodeStart = new List<Vertex>();
            List<Vertex> visitedNodeEnd = new List<Vertex>();
            List<Vertex> PathNodeStart = new List<Vertex>();
            List<Vertex> PathNodeEnd = new List<Vertex>();
            int[] dx;
            int[] dy;
            switch (ModeDirection)
            {
                case 0:
                    //四方向搜索
                    dx = new int[4];
                    dy = new int[4];
                    dx[0] = -1; dy[0] = 0;
                    dx[1] = 0; dy[1] = 1;
                    dx[2] = 0; dy[2] = -1;
                    dx[3] = 1; dy[3] = 0;
                    break;
                case 1:
                    //八方向搜索
                    dx = new int[8] { -1, 0, 1, -1, 1, -1, 0, 1 };
                    dy = new int[8] { 1, 1, 1, 0, 0, -1, -1, -1 };
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
            //记录Status
            int[,] MatStatus = new int[MapSize, MapSize];
            Queue<Vertex> QStart = new Queue<Vertex>();
            Queue<Vertex> QEnd = new Queue<Vertex>();
            MatStatus[StatP.x, StatP.y] = 1;
            MatStatus[EndP.x, EndP.y] = 2;
            QStart.Enqueue(StatP);
            QEnd.Enqueue(EndP);
            visitedNodeStart.Add(StatP);
            visitedNodeEnd.Add(EndP);
            Vertex CrossPoint = new Vertex(StatP.x, StatP.y);
            bool isfind = false;
            while (QStart.Count() != 0 && QEnd.Count() != 0) 
            {
                if (QStart.Count() != 0) 
                {
                    Vertex vCurrStart = QStart.Dequeue();
                    Vertex vNear = null;
                    //处理从起点开始
                    for (int i = 0; i < dx.Length; i++)
                    {
                        vNear = new Vertex(vCurrStart.x + dx[i], vCurrStart.y + dy[i]);

                        if (vNear.x < 0 || vNear.y < 0 || vNear.x >= MapSize || vNear.y >= MapSize)
                            continue;
                        //若未访问过
                        if (MatStatus[vNear.x, vNear.y] == 0 && mapData[vNear.x, vNear.y] != 1)
                        {
                            vNear.ID = vNear.x * MapSize + vNear.y;
                            MatStatus[vNear.x, vNear.y] = 1; //Start访问
                            vNear.SetParent(vCurrStart);
                            QStart.Enqueue(vNear);
                            visitedNodeStart.Add(vNear);
                            MapButton[vNear.x * MapSize + vNear.y].BackColor = Color.Gray;
                        }
                        else if (MatStatus[vNear.x, vNear.y] == 2)    //End访问过
                        {
                            CrossPoint = vNear;
                            isfind = true;
                            break;
                        }
                    }
                    MatStatus[vCurrStart.x, vCurrStart.y] = 1;
                    //如果找到了，pathnode搞一波
                    if (isfind)
                    {
                        //先找从Start到vCurrStart
                        Vertex pNode = vCurrStart;
                        while(pNode.x != StatP.x || pNode.y != StatP.y)
                        {
                            PathNodeStart.Add(pNode);
                            pNode = pNode.GetParent();
                        }
                        PathNodeStart.Add(StatP);
                        PathNodeStart.Reverse();
                        pNode = vNear;
                        for (int i = 0; i < visitedNodeEnd.Count(); i++) 
                        {
                            if (visitedNodeEnd[i].x == vNear.x && visitedNodeEnd[i].y == vNear.y)
                            {
                                pNode = visitedNodeEnd[i];
                                break;
                            }
                        }
                        while (pNode.x != EndP.x || pNode.y != EndP.y) 
                        {
                            PathNodeStart.Add(pNode);
                            pNode = pNode.GetParent();
                        }
                        PathNodeStart.Add(EndP);
                        break;
                    }
                }
                if (QEnd.Count() != 0) 
                {
                    Vertex vCurrEnd = QEnd.Dequeue();
                    Vertex vNearEnd = null;
                    //处理从终点开始
                    for (int i = 0; i < dx.Length; i++)
                    {
                        vNearEnd = new Vertex(vCurrEnd.x + dx[i], vCurrEnd.y + dy[i]);
                        if (vNearEnd.x < 0 || vNearEnd.y < 0 || vNearEnd.x >= MapSize || vNearEnd.y >= MapSize)
                            continue;
                        //若未访问过
                        if (MatStatus[vNearEnd.x, vNearEnd.y] == 0 && mapData[vNearEnd.x, vNearEnd.y] != 1)
                        {
                            vNearEnd.ID = vNearEnd.x * MapSize + vNearEnd.y;
                            MatStatus[vNearEnd.x, vNearEnd.y] = 2;
                            vNearEnd.SetParent(vCurrEnd);
                            QEnd.Enqueue(vNearEnd);
                            visitedNodeEnd.Add(vNearEnd);
                            MapButton[vNearEnd.x * MapSize + vNearEnd.y].BackColor = Color.OrangeRed;
                        }
                        else if(MatStatus[vNearEnd.x,vNearEnd.y] == 1)  //Start访问过
                        {
                            CrossPoint = vNearEnd;
                            isfind = true;
                            break;
                        }
                    }
                    MatStatus[vCurrEnd.x, vCurrEnd.y] = 2;
                    //如果找到了
                    if(isfind)
                    {
                        //先从CrossPoint搜索到StatP
                        Vertex pNode = vNearEnd;
                        for (int i = 0; i < visitedNodeStart.Count(); i++)
                        {
                            if(visitedNodeStart[i].x == pNode.x && visitedNodeStart[i].y == pNode.y)
                            {
                                pNode = visitedNodeStart[i];
                                break;
                            }
                        }
                        while(pNode.x != StatP.x || pNode.y != StatP.y)
                        {
                            PathNodeStart.Add(pNode);
                            pNode = pNode.GetParent();
                        }
                        PathNodeStart.Add(vNearEnd);
                        PathNodeStart.Reverse();

                        pNode = vCurrEnd;
                        while(pNode.x != EndP.x || pNode.y != EndP.y)
                        {
                            PathNodeStart.Add(pNode);
                            pNode = pNode.GetParent();
                        }
                        PathNodeStart.Add(EndP);
                        break;
                    }
                }
            }
            if (!isfind)
                MessageBox.Show("不存在搜索路径！");
            for (int i = 1; i < PathNodeStart.Count() - 1; i++)
            {
                MapButton[PathNodeStart[i].x * MapSize + PathNodeStart[i].y].BackColor = Color.Green;
            }
        }

        //A*算法（一致性）优先级队列
        private void AStarPriority(Vertex StatP, Vertex EndP, int ModeDistance)
        {
            int[] dx;
            int[] dy;
            switch (ModeDirection)
            {
                case 0:
                    //四方向搜索
                    dx = new int[4];
                    dy = new int[4];
                    dx[0] = -1; dy[0] = 0;
                    dx[1] = 0; dy[1] = 1;
                    dx[2] = 0; dy[2] = -1;
                    dx[3] = 1; dy[3] = 0;
                    break;
                case 1:
                    //八方向搜索
                    dx = new int[8] { -1, 0, 1, -1, 1, -1, 0, 1 };
                    dy = new int[8] { 1, 1, 1, 0, 0, -1, -1, -1 };
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
            //记录Status是否在开集，在为1
            int[,] MatStatus = new int[MapSize, MapSize];
            FastPriorityQueue<Vertex> OpenQueue = new FastPriorityQueue<Vertex>(10000);
            //SimplePriorityQueue<Vertex> OpenQueue = new SimplePriorityQueue<Vertex>();
            List<Vertex> CloseList = new List<Vertex>();
            List<Vertex> PathNode = new List<Vertex>();
            StatP.GCost = 0;
            StatP.HCost = Distance(StatP, EndP, ModeDistance);
            StatP.depth = 0;
            OpenQueue.Enqueue(StatP, Convert.ToSingle(StatP.GCost + StatP.HCost));
            MatStatus[StatP.x, StatP.y] = 1;
            bool isfind = false;
            while(true)
            {
                if (OpenQueue.Count() == 0)
                    break;
                Vertex vCurr = OpenQueue.Dequeue();
                MatStatus[vCurr.x, vCurr.y] = 0;
                CloseList.Add(vCurr);
                if (vCurr.x == EndP.x && vCurr.y == EndP.y)
                {
                    isfind = true;
                    Vertex pNode = vCurr;
                    while (pNode.x != StatP.x || pNode.y != StatP.y)
                    {
                        PathNode.Add(pNode);
                        pNode = pNode.GetParent();
                    }
                    PathNode.Add(StatP);
                    break;
                }
                for (int i = 0; i < dx.Length; i++)
                {
                    Vertex vNear = new Vertex(vCurr.x + dx[i], vCurr.y + dy[i]);
                    if (vNear.x < 0 || vNear.y < 0 || vNear.x >= MapSize || vNear.y >= MapSize)
                        continue;
                    //如果是墙，忽略 0为路
                    if (mapData[vNear.x, vNear.y] == 1)
                        continue;
                    //如果已经在闭集里
                    if (InCloseList(vNear, CloseList))
                        continue;
                    //如果不是路，则更新结点代价
                    if (mapData[vNear.x, vNear.y] != 0)
                    {
                        vNear.vCost = mapData[vNear.x, vNear.y];
                    }
                    double vNearGCost = vCurr.GCost + vNear.vCost;
                    //不在开集中，则加入到开集中
                    if (MatStatus[vNear.x,vNear.y] == 0)
                    {
                        vNear.depth = vCurr.depth + 1;
                        vNear.GCost = vNearGCost;
                        vNear.SetParent(vCurr);
                        vNear.HCost = Distance(vNear, EndP, ModeDistance);
                        OpenQueue.Enqueue(vNear, Convert.ToSingle(vNear.GCost + vNear.HCost));
                        MatStatus[vNear.x, vNear.y] = 1;
                        if (MapButton[vNear.x * MapSize + vNear.y].BackColor == SystemColors.Control)
                        {
                            MapButton[vNear.x * MapSize + vNear.y].BackColor = Color.Gray;
                            //MapButton[vNear.x * MapSize + vNear.y].Text = Convert.ToString(vNear.GCost);
                        }
                        continue;
                    }
                    else if (vNearGCost >= vNear.GCost)
                        continue;
                    //当前为最佳路径，做一波更新
                    vNear.SetParent(vCurr);
                    vNear.GCost = vNearGCost;
                    OpenQueue.Enqueue(vNear, Convert.ToSingle(vNear.GCost + vNear.HCost));
                }
            }
            if (!isfind)
                MessageBox.Show("没有找到搜索路径！");
            PathNode.Reverse();
            for (int i = 1; i < PathNode.Count - 1; i++)
            {
                MapButton[PathNode[i].x * MapSize + PathNode[i].y].BackColor = Color.Green;
            }
        }


        //A*算法（一致性）没用优先级队列
        private void AStar(Vertex StatP, Vertex EndP,int ModeDistance)
        {
            int[] dx;
            int[] dy;

            switch (ModeDirection)
            {
                case 0:
                    //四方向搜索
                    dx = new int[4];
                    dy = new int[4];
                    dx[0] = -1; dy[0] = 0;
                    dx[1] = 0; dy[1] = 1;
                    dx[2] = 0; dy[2] = -1;
                    dx[3] = 1; dy[3] = 0;
                    break;
                case 1:
                    //八方向搜索
                    dx = new int[8] { -1, 0, 1, -1, 1, -1, 0, 1 };
                    dy = new int[8] { 1, 1, 1, 0, 0, -1, -1, -1 };
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }

            //开启列表和关闭列表
            List<Vertex> OpenList = new List<Vertex>();
            List<Vertex> CloseList = new List<Vertex>();
            List<Vertex> PathNode = new List<Vertex>();
            StatP.GCost = 0;
            StatP.depth = 0;
            OpenList.Add(StatP);
            bool isfind = false;
            while(true)
            {
                if (OpenList.Count == 0)
                    break;
                Vertex vCurr = GetMinFFromOpenList(StatP, EndP, OpenList, ModeDistance);
                OpenList.Remove(vCurr);
                CloseList.Add(vCurr);
                if(vCurr.x == EndP.x && vCurr.y == EndP.y)
                {
                    isfind = true;
                    Vertex pNode = vCurr;
                    while (pNode.x != StatP.x || pNode.y != StatP.y)
                    {
                        PathNode.Add(pNode);
                        pNode = pNode.GetParent();
                    }
                    PathNode.Add(StatP);
                    break;
                }
                for (int i = 0; i < dx.Length; i++) 
                {
                    Vertex vNear = new Vertex(vCurr.x + dx[i], vCurr.y + dy[i]);
                    if (vNear.x < 0 || vNear.y < 0 || vNear.x >= MapSize || vNear.y >= MapSize)
                        continue;
                    //如果是墙，忽略 0为路
                    if (mapData[vNear.x, vNear.y] == 1)
                        continue;
                    //如果已经在闭集里
                    if (InCloseList(vNear, CloseList))
                        continue;
                    //如果不是路，则更新结点代价
                    if (mapData[vNear.x, vNear.y] != 0)
                    {
                        vNear.vCost = mapData[vNear.x, vNear.y];
                    }
                    double vNearGCost = vCurr.GCost + vNear.vCost;
                    //不在开集中，则加入到开集中
                    Vertex vTemp = InOpenList(vNear, OpenList);
                    if (vTemp == null)
                    {
                        vNear.depth = vCurr.depth + 1;
                        vNear.GCost = vNearGCost;
                        vNear.SetParent(vCurr);
                        OpenList.Add(vNear);
                        if(MapButton[vNear.x * MapSize + vNear.y].BackColor == SystemColors.Control)
                            MapButton[vNear.x * MapSize + vNear.y].BackColor = Color.Gray;
                        continue;
                    }
                    else if (vNearGCost >= vNear.GCost)
                        continue;
                    //当前为最佳路径，做一波更新
                    vNear.depth = vCurr.depth + 1;
                    vTemp.SetParent(vCurr);
                    vTemp.GCost = vNearGCost;
                }
            }
            if (!isfind)
                MessageBox.Show("没有找到搜索路径！");
            PathNode.Reverse();
            for (int i = 1; i < PathNode.Count - 1; i++)
            {
                MapButton[PathNode[i].x * MapSize + PathNode[i].y].BackColor = Color.Green;
            }

        }

        //判断顶点v是否在闭集中
        private bool InCloseList(Vertex v,List<Vertex> CloseList)
        {
            for (int i = 0; i < CloseList.Count(); i++)
            {
                if(CloseList[i].x == v.x && CloseList[i].y==v.y)
                {
                    return true;
                }
            }
            return false;
        }
        //判断顶点v是否在开集中
        private Vertex InOpenList(Vertex v, List<Vertex> OpenList)
        {
            for (int i = 0; i < OpenList.Count(); i++)
            {
                if (OpenList[i].x == v.x && OpenList[i].y == v.y)
                    return OpenList[i];
            }
            return null;
        }
        //从开启列表中查找F值最小的节点
        private Vertex GetMinFFromOpenList(Vertex StatP,Vertex EndP,List<Vertex> OpenList,int ModeDistance)
        {
            Vertex Pmin = null;
            for (int i = 0; i < OpenList.Count(); i++)
            {
                if (Pmin == null)
                {
                    Pmin = OpenList[i];
                    Pmin.HCost = Distance(Pmin, EndP, ModeDistance);
                    continue;
                }
                OpenList[i].HCost = Distance(OpenList[i], EndP, ModeDistance);
                if((Pmin.GCost + Pmin.HCost) > (OpenList[i].GCost + OpenList[i].HCost))
                    Pmin = OpenList[i];
            }
            return Pmin;
        }
        
        //定义距离（欧式距离和曼哈顿距离以及对角线距离）
        private double Distance(Vertex V1,Vertex V2,int ModeDistance)
        {
            switch(ModeDistance)
            {
                case 0:
                    return Math.Sqrt((V1.x - V2.x) * (V1.x - V2.x) + (V1.y - V2.y) * (V1.y - V2.y));
                case 1:
                    return Math.Abs(V1.x - V2.x) + Math.Abs(V1.y - V2.y);
                case 2:
                    return Math.Max(Math.Abs(V1.x - V2.x), Math.Abs(V1.y - V2.y));
                default:
                    throw new IndexOutOfRangeException();
            }
        }
        
        //深度受限搜索
        private bool DepthlimitedSearch(Vertex StatP, Vertex EndP, int limitedDepth)
        {
            MapReset();
            List<Vertex> visitedNode = new List<Vertex>();
            List<Vertex> PathNode = new List<Vertex>();
            int[] dx;
            int[] dy;
            switch (ModeDirection)
            {
                case 0:
                    //四方向搜索
                    dx = new int[4];
                    dy = new int[4];
                    dx[0] = -1; dy[0] = 0;
                    dx[1] = 0; dy[1] = 1;
                    dx[2] = 0; dy[2] = -1;
                    dx[3] = 1; dy[3] = 0;
                    break;
                case 1:
                    //八方向搜索
                    dx = new int[8] { -1, 0, 1, -1, 1, -1, 0, 1 };
                    dy = new int[8] { 1, 1, 1, 0, 0, -1, -1, -1 };
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
            //记录Status
            int[,] MatStatus = new int[MapSize, MapSize];
            int[,] MatDepth = new int[MapSize, MapSize];
            //初始化深度均为无穷
            for (int i = 0; i < MapSize; i++)
                for (int j = 0; j < MapSize; j++)
                    MatDepth[i, j] = 1000000;
            Stack<Vertex> S = new Stack<Vertex>();
            MatStatus[StatP.x, StatP.y] = 1;
            MatDepth[StatP.x, StatP.y] = 0;
            StatP.depth = 0;
            S.Push(StatP);
            visitedNode.Add(StatP);
            bool isfind = false;
            while(S.Count() != 0)
            {
                Vertex vCurr = S.Pop();
                for (int i = 0; i < dx.Length; i++)
                {
                    Vertex vNear = new Vertex(vCurr.x + dx[i], vCurr.y + dy[i]);
                    if (vNear.x < 0 || vNear.y < 0 || vNear.x >= MapSize || vNear.y >= MapSize)
                        continue;
                    vNear.depth = vCurr.depth + 1;
                    if (vNear.depth > limitedDepth)
                        continue;
                    //如果当前深度比之前所记录的深度要小，则重新访问
                    if ((vNear.depth <= MatDepth[vNear.x, vNear.y] || MatStatus[vNear.x, vNear.y] == 1 )&& !(vNear.x == StatP.x && vNear.y == StatP.y))
                        MatStatus[vNear.x, vNear.y] = 0;
                    //若未访问过且没有墙
                    if (MatStatus[vNear.x, vNear.y] == 0 && mapData[vNear.x, vNear.y] != 1)
                    {
                        vNear.ID = vNear.x * MapSize + vNear.y;
                        MatStatus[vNear.x, vNear.y] = 1; //访问
                        MatDepth[vNear.x, vNear.y] = vNear.depth;
                        vNear.SetParent(vCurr);
                        S.Push(vNear);
                        visitedNode.Add(vNear);
                        MapButton[vNear.x * MapSize + vNear.y].BackColor = Color.Gray;
                        //MapButton[vNear.x * MapSize + vNear.y].Text = Convert.ToString(vNear.depth);
                        if (vNear.x == EndP.x && vNear.y == EndP.y)
                        {
                            isfind = true;
                            Console.WriteLine("Find!");
                            break;
                        }

                    }
                }
                MatStatus[vCurr.x, vCurr.y] = 1;
                if (isfind)
                {
                    Vertex pNode = visitedNode[visitedNode.Count() - 1];
                    while (pNode.x != StatP.x || pNode.y != StatP.y)
                    {
                        PathNode.Add(pNode);
                        pNode = pNode.GetParent();
                    }
                    PathNode.Add(StatP);
                    break;
                }
            }
            if (!isfind)
            {
                return false;
            }
            PathNode.Reverse();
            for (int i = 1; i < PathNode.Count() - 1; i++)
            {
                MapButton[PathNode[i].x * MapSize + PathNode[i].y].BackColor = Color.Green;
            }
            return true;
        }

        //迭代加深搜索
        private void IterDFS(Vertex StatP, Vertex EndP, int limitedDepth)
        {
            for (int i = 0; i <= limitedDepth; i++)
            {
                if (DepthlimitedSearch(StatP, EndP, i))
                {
                    return;
                }
            }
            MessageBox.Show("不存在搜索路径！");
        }
        //贪心最佳优先算法
        private void GreedyBestFirstSearch(Vertex StatP, Vertex EndP, int ModeDistance)
        {
            List<Vertex> visitedNode = new List<Vertex>();
            List<Vertex> PathNode = new List<Vertex>();
            int[] dx;
            int[] dy;

            switch (ModeDirection)
            {
                case 0:
                    //四方向搜索
                    dx = new int[4];
                    dy = new int[4];
                    dx[0] = -1; dy[0] = 0;
                    dx[1] = 0; dy[1] = 1;
                    dx[2] = 0; dy[2] = -1;
                    dx[3] = 1; dy[3] = 0;
                    break;
                case 1:
                    //八方向搜索
                    dx = new int[8] { -1, 0, 1, -1, 1, -1, 0, 1 };
                    dy = new int[8] { 1, 1, 1, 0, 0, -1, -1, -1 };
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
            //记录Status
            int[,] MatStatus = new int[MapSize, MapSize];
            FastPriorityQueue<Vertex> Q = new FastPriorityQueue<Vertex>(10000);
            //SimplePriorityQueue<Vertex> Q = new SimplePriorityQueue<Vertex>();
            MatStatus[StatP.x, StatP.y] = 1;
            StatP.HCost = Distance(StatP, EndP, ModeDistance);
            Q.Enqueue(StatP, Convert.ToSingle(StatP.HCost));
            visitedNode.Add(StatP);
            bool isfind = false;
            while(Q.Count() != 0)
            {
                Vertex vCurr = Q.Dequeue();
                for (int i = 0; i < dx.Length; i++)
                {
                    Vertex vNear = new Vertex(vCurr.x + dx[i], vCurr.y + dy[i]);
                    if (vNear.x < 0 || vNear.y < 0 || vNear.x >= MapSize || vNear.y >= MapSize)
                        continue;
                    //若未访问过且没有墙
                    if (MatStatus[vNear.x, vNear.y] == 0 && mapData[vNear.x, vNear.y] != 1)
                    {
                        vNear.ID = vNear.x * MapSize + vNear.y;
                        MatStatus[vNear.x, vNear.y] = 1; //访问
                        vNear.SetParent(vCurr); //可能出错...
                        vNear.HCost = Distance(vNear, EndP, ModeDistance);
                        Q.Enqueue(vNear, Convert.ToSingle(vNear.HCost));
                        visitedNode.Add(vNear);
                        if (vNear.x == EndP.x && vNear.y == EndP.y)
                        {
                            isfind = true;
                            Console.WriteLine("Find!");
                            break;
                        }
                        MapButton[vNear.x * MapSize + vNear.y].BackColor = Color.Gray;
                    }
                }
                MatStatus[vCurr.x, vCurr.y] = 1;
                if (isfind)
                {
                    Vertex pNode = visitedNode[visitedNode.Count() - 1];
                    while (pNode.x != StatP.x || pNode.y != StatP.y)
                    {
                        PathNode.Add(pNode);
                        pNode = pNode.GetParent();
                    }
                    PathNode.Add(StatP);
                    break;
                }
            }
            if (!isfind)
            {
                MessageBox.Show("不存在搜索路径！");
            }
            PathNode.Reverse();
            for (int i = 1; i < PathNode.Count() - 1; i++)
                MapButton[PathNode[i].x * MapSize + PathNode[i].y].BackColor = Color.Green;
        }

        //Dijkstra
        private void DijkstraSearch(Vertex StatP,Vertex EndP,int ModeDistance)
        {
            int[] dx;
            int[] dy;
            switch (ModeDirection)
            {
                case 0:
                    //四方向搜索
                    dx = new int[4];
                    dy = new int[4];
                    dx[0] = -1; dy[0] = 0;
                    dx[1] = 0; dy[1] = 1;
                    dx[2] = 0; dy[2] = -1;
                    dx[3] = 1; dy[3] = 0;
                    break;
                case 1:
                    //八方向搜索
                    dx = new int[8] { -1, 0, 1, -1, 1, -1, 0, 1 };
                    dy = new int[8] { 1, 1, 1, 0, 0, -1, -1, -1 };
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
            //记录Status是否在开集，在为1
            int[,] MatStatus = new int[MapSize, MapSize];
            FastPriorityQueue<Vertex> OpenQueue = new FastPriorityQueue<Vertex>(10000);
            //SimplePriorityQueue<Vertex> OpenQueue = new SimplePriorityQueue<Vertex>();
            List<Vertex> CloseList = new List<Vertex>();
            List<Vertex> PathNode = new List<Vertex>();
            StatP.GCost = 0;
            StatP.depth = 0;
            OpenQueue.Enqueue(StatP, Convert.ToSingle(StatP.GCost));
            MatStatus[StatP.x, StatP.y] = 1;
            bool isfind = false;
            while (true)
            {
                if (OpenQueue.Count() == 0)
                    break;
                Vertex vCurr = OpenQueue.Dequeue();
                MatStatus[vCurr.x, vCurr.y] = 0;
                CloseList.Add(vCurr);
                if (vCurr.x == EndP.x && vCurr.y == EndP.y)
                {
                    isfind = true;
                    Vertex pNode = vCurr;
                    while (pNode.x != StatP.x || pNode.y != StatP.y)
                    {
                        PathNode.Add(pNode);
                        pNode = pNode.GetParent();
                    }
                    PathNode.Add(StatP);
                    break;
                }
                for (int i = 0; i < dx.Length; i++)
                {
                    Vertex vNear = new Vertex(vCurr.x + dx[i], vCurr.y + dy[i]);
                    if (vNear.x < 0 || vNear.y < 0 || vNear.x >= MapSize || vNear.y >= MapSize)
                        continue;
                    //如果是墙，忽略 0为路
                    if (mapData[vNear.x, vNear.y] == 1)
                        continue;
                    //如果已经在闭集里
                    if (InCloseList(vNear, CloseList))
                        continue;
                    //如果不是路，则更新结点代价
                    if (mapData[vNear.x, vNear.y] != 0) 
                    {
                        vNear.vCost = mapData[vNear.x, vNear.y];
                    }
                    double vNearGCost = vCurr.GCost + vNear.vCost;
                    //不在开集中，则加入到开集中
                    if (MatStatus[vNear.x, vNear.y] == 0)
                    {
                        vNear.depth = vCurr.depth + 1;
                        vNear.GCost = vNearGCost;
                        vNear.SetParent(vCurr);
                        OpenQueue.Enqueue(vNear, Convert.ToSingle(vNear.GCost));
                        MatStatus[vNear.x, vNear.y] = 1;
                        if (MapButton[vNear.x * MapSize + vNear.y].BackColor == SystemColors.Control)
                        {
                            MapButton[vNear.x * MapSize + vNear.y].BackColor = Color.Gray;
                        }

                        continue;
                    }
                    else if (vNearGCost >= vNear.GCost)
                        continue;
                    //当前为最佳路径，做一波更新
                    vNear.SetParent(vCurr);
                    vNear.GCost = vNearGCost;
                    OpenQueue.Enqueue(vNear, Convert.ToSingle(vNear.GCost));
                }
            }
            if (!isfind)
                MessageBox.Show("没有找到搜索路径！");
            PathNode.Reverse();
            for (int i = 1; i < PathNode.Count - 1; i++)
            {
                MapButton[PathNode[i].x * MapSize + PathNode[i].y].BackColor = Color.Green;
            }
        }

        //模拟退火
        private void SAAlgorithm(Vertex StatP, List<Vertex> EndP, int ModeDistance)
        {
            //先使用A*算法找出起点到每个终点的距离
            List<Vertex>[] PathNodes = new List<Vertex>[EndP.Count()];
            int[,] MatPath = new int[EndP.Count() + 1, EndP.Count() + 1];//记录邻接矩阵
            for (int i = 0; i < EndP.Count(); i++)
            {
                PathNodes[i] = FindPathforSAA(StatP, EndP[i], ModeDistance);
                if (PathNodes[i] == null)
                {
                    MatPath[0, i + 1] = 100000;
                    MatPath[i + 1, 0] = 100000;
                    continue;
                }
                MatPath[0, i + 1] = PathNodes[i].Count();
                MatPath[i + 1, 0] = PathNodes[i].Count();
            }
            //在使用A*算法算出每两个终点之间的距离
            List<Vertex>[,] PathNodesEnd = new List<Vertex>[EndP.Count(), EndP.Count()];
            for (int i = 0; i < EndP.Count(); i++)
                for (int j = i + 1; j < EndP.Count(); j++) 
                {
                    PathNodesEnd[i, j] = FindPathforSAA(EndP[i], EndP[j], ModeDistance);
                    PathNodesEnd[j, i] = FindPathforSAA(EndP[i], EndP[j], ModeDistance);
                    if(PathNodesEnd[j,i] == null)
                    {
                        MatPath[i + 1, j + 1] = 100000;
                        MatPath[j + 1, i + 1] = 100000;
                        continue;
                    }
                    MatPath[i + 1, j + 1] = PathNodesEnd[i, j].Count();
                    MatPath[j + 1, i + 1] = PathNodesEnd[i, j].Count();
                }
            double CurrT = 1000;    //当前温度
            double minT = 1;        //温度下限
            int iterL = 1000;       //每个温度的迭代次数
            double delta = 0.95;    //温度衰减系数
            int[] BestSolution = new int[EndP.Count() + 1]; //最优解
            int[] CurrSolution = new int[EndP.Count() + 1]; //当前解
            int MinPathLen = 0;     //最优解长度
            int CurrPathLen = 0;    //当前路径长度
            //初始化最优解
            for (int i = 0; i < BestSolution.Count(); i++)
            {
                BestSolution[i] = i;
            }
            //随机打乱顺序（保证起点不变）
            for (int i = 1; i < BestSolution.Count(); i++)
            {
                Random rand = new Random(GetRandomSeed());
                int index = rand.Next(1, BestSolution.Count());
                if(index != i)
                {
                    int temp = BestSolution[i];
                    BestSolution[i] = BestSolution[index];
                    BestSolution[index] = temp;
                }
            }

            //计算最短路径(即初始化的值)
            for (int i = 0; i < BestSolution.Count(); i++)
            {
                //最后一个连起点
                if (i != BestSolution.Count() - 1)
                {
                    MinPathLen += MatPath[BestSolution[i], BestSolution[i + 1]];
                }
                else
                {
                    MinPathLen += MatPath[BestSolution[i], BestSolution[0]];
                }
            }
            
            //模拟退火过程
            while(CurrT > minT)
            {
                for (int i = 0; i < iterL; i++)
                {
                    CurrPathLen = 0;
                    CurrSolution = FindNewSolution(BestSolution, MinPathLen);
                    //计算新解的路径长度
                    for (int j = 0; j < CurrSolution.Count(); j++)
                    {
                        if (j != CurrSolution.Count() - 1)
                            CurrPathLen += MatPath[CurrSolution[j], CurrSolution[j + 1]];
                        else
                            CurrPathLen += MatPath[CurrSolution[j], CurrSolution[0]];
                    }
                    if (CurrPathLen <= MinPathLen)
                    {
                        BestSolution = CurrSolution;
                        MinPathLen = CurrPathLen;
                    }
                    //按照Metropolis判断是否接受
                    else
                    {
                        double dEdividedT = (MinPathLen - CurrPathLen) / CurrT;
                        int P = (int)Math.Exp(dEdividedT) * 100;
                        Random rand = new Random(GetRandomSeed());
                        if(P > rand.Next(0,102))
                        {
                            BestSolution = CurrSolution;
                            MinPathLen = CurrPathLen;
                        }
                    }
                }
                CurrT *= delta;
            }
            // 显示搜索路径
            Color[] PathColor = new Color[5];
            PathColor[0] = Color.Green;
            PathColor[1] = Color.RosyBrown;
            PathColor[2] = Color.HotPink;
            PathColor[3] = Color.LightBlue;
            PathColor[4] = Color.Yellow;
            for (int i = 0; i < BestSolution.Count(); i++)
            {
                List<Vertex> Path = new List<Vertex>();
                if (i == 0)
                    Path = PathNodes[BestSolution[i + 1] - 1];
                else if (i == BestSolution.Count() - 1) 
                    Path = PathNodes[BestSolution[i] - 1];
                else
                    Path = PathNodesEnd[BestSolution[i] - 1, BestSolution[i + 1] - 1];
                if (Path == null)
                    break;
                for (int j = 1; j < Path.Count() - 1; j++)
                {
                    MapButton[Path[j].x * MapSize + Path[j].y].BackColor = PathColor[i % 5];
                    MapButton[Path[j].x * MapSize + Path[j].y].Text = Convert.ToString(i);
                }

            }
        }


        // 产生新解
        private int[] FindNewSolution(int[] BestSolution, int MinPathLen)
        {
            int[] NewSolution = new int[BestSolution.Count()];
            for (int i = 0; i < NewSolution.Count(); i++)
                NewSolution[i] = BestSolution[i];

            Random rand = new Random(GetRandomSeed());
            int ri = rand.Next(1, BestSolution.Count());
            int rj = rand.Next(1, BestSolution.Count());
            //使ri < rj
            if (ri > rj)
            {
                int temp = ri;
                ri = rj;
                rj = temp;
            }
            else if (ri == rj)
            {
                return NewSolution;
            }
            int mode = rand.Next(0, 3); //产生0~2
            switch(mode)
            {
                //随机交换
                case 0:
                    int temp = NewSolution[ri];
                    NewSolution[ri] = NewSolution[rj];
                    NewSolution[rj] = temp;
                    break;
                //随机逆置
                case 1:
                    for (int i = 0; i <= rj - ri; i++) 
                    {
                        NewSolution[ri + i] = BestSolution[rj - i];
                    }
                    break;
            }
            return NewSolution;
        }
        
        //获取随机种子
        static private int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        //寻找路径（A*优先级队列算法修改）
        private List<Vertex> FindPathforSAA(Vertex StatP, Vertex EndP, int ModeDistance)
        {
            int[] dx;
            int[] dy;
            switch (ModeDirection)
            {
                case 0:
                    //四方向搜索
                    dx = new int[4];
                    dy = new int[4];
                    dx[0] = -1; dy[0] = 0;
                    dx[1] = 0; dy[1] = 1;
                    dx[2] = 0; dy[2] = -1;
                    dx[3] = 1; dy[3] = 0;
                    break;
                case 1:
                    //八方向搜索
                    dx = new int[8] { -1, 0, 1, -1, 1, -1, 0, 1 };
                    dy = new int[8] { 1, 1, 1, 0, 0, -1, -1, -1 };
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
            //记录Status是否在开集，在为1
            int[,] MatStatus = new int[MapSize, MapSize];

            SimplePriorityQueue<Vertex> OpenQueue = new SimplePriorityQueue<Vertex>();
            List<Vertex> CloseList = new List<Vertex>();
            List<Vertex> PathNode = new List<Vertex>();
            StatP.GCost = 0;
            StatP.HCost = Distance(StatP, EndP, ModeDistance);
            StatP.depth = 0;
            OpenQueue.Enqueue(StatP, Convert.ToSingle(StatP.GCost + StatP.HCost));
            MatStatus[StatP.x, StatP.y] = 1;
            bool isfind = false;
            while (true)
            {
                if (OpenQueue.Count() == 0)
                    break;
                Vertex vCurr = OpenQueue.Dequeue();
                MatStatus[vCurr.x, vCurr.y] = 0;
                CloseList.Add(vCurr);
                if (vCurr.x == EndP.x && vCurr.y == EndP.y)
                {
                    isfind = true;
                    Vertex pNode = vCurr;
                    while (pNode.x != StatP.x || pNode.y != StatP.y)
                    {
                        PathNode.Add(pNode);
                        pNode = pNode.GetParent();
                    }
                    PathNode.Add(StatP);
                    break;
                }
                for (int i = 0; i < dx.Length; i++)
                {
                    Vertex vNear = new Vertex(vCurr.x + dx[i], vCurr.y + dy[i]);
                    if (vNear.x < 0 || vNear.y < 0 || vNear.x >= MapSize || vNear.y >= MapSize)
                        continue;
                    //如果是墙，忽略 0为路
                    if (mapData[vNear.x, vNear.y] == 1)
                        continue;
                    //如果已经在闭集里
                    if (InCloseList(vNear, CloseList))
                        continue;
                    //如果不是路，则更新结点代价
                    if (mapData[vNear.x, vNear.y] != 0)
                    {
                        vNear.vCost = mapData[vNear.x, vNear.y];
                    }
                    double vNearGCost = vCurr.GCost + vNear.vCost;
                    //不在开集中，则加入到开集中
                    if (MatStatus[vNear.x, vNear.y] == 0)
                    {
                        vNear.depth = vCurr.depth + 1;
                        vNear.GCost = vNearGCost;
                        vNear.SetParent(vCurr);
                        vNear.HCost = Distance(vNear, EndP, ModeDistance);
                        OpenQueue.Enqueue(vNear, Convert.ToSingle(vNear.GCost + vNear.HCost));
                        MatStatus[vNear.x, vNear.y] = 1;
                        continue;
                    }
                    else if (vNearGCost >= vNear.GCost)
                        continue;
                    //当前为最佳路径，做一波更新
                    vNear.depth = vCurr.depth + 1;
                    vNear.SetParent(vCurr);
                    vNear.GCost = vNearGCost;
                    OpenQueue.Enqueue(vNear, Convert.ToSingle(vNear.GCost + vNear.HCost));
                }
            }
            if (!isfind)
                return null;
            PathNode.Reverse();
            return PathNode;
        }

        // id转坐标
        private int[] ID2Corr(int id)
        {
            int[] corr = new int[2];
            corr[0] = id / MapSize;
            corr[1] = id % MapSize;
            return corr;
        }

        //设置事件函数
        private void SetPath_Click(object sender, EventArgs e)
        {
            Button senderButton = (Button)sender;
            int id = Convert.ToInt16(senderButton.Name);
            int[] corr = ID2Corr(id);

                
            switch (Mode)
            {
                case 0:
                    switch(ModeObstale)
                    {
                        //灯牌
                        case 0:
                            if(mapData[corr[0],corr[1]] == 0)
                            {
                                mapData[corr[0], corr[1]] = 2;
                                senderButton.BackgroundImage = Properties.Resources.dengpai;
                                senderButton.BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            else if(mapData[corr[0],corr[1]] == 2)
                            {
                                mapData[corr[0], corr[1]] = 0;
                                senderButton.BackgroundImage = null;
                            }                            
                            break;
                        //新浪微博
                        case 1:
                            if(mapData[corr[0],corr[1]] == 0)
                            {
                                mapData[corr[0], corr[1]] = 3;
                                senderButton.BackgroundImage = Properties.Resources.timg;
                                senderButton.BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            else if(mapData[corr[0],corr[1]] == 3)
                            {
                                mapData[corr[0], corr[1]] = 0;
                                senderButton.BackgroundImage = null;
                            }
                            break;
                        //暖宝宝
                        case 2:
                            if(mapData[corr[0], corr[1]] == 0)
                            {
                                mapData[corr[0], corr[1]] = 4;
                                senderButton.BackgroundImage = Properties.Resources.nuanbaobao;
                                senderButton.BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            else if(mapData[corr[0],corr[1]] == 4)
                            {
                                mapData[corr[0], corr[1]] = 0;
                                senderButton.BackgroundImage = null;
                            }
                            break;
                        //老王
                        case 3:
                            if (mapData[corr[0], corr[1]] == 0)
                            {
                                mapData[corr[0], corr[1]] = 5;
                                senderButton.BackgroundImage = Properties.Resources.laowang;
                                senderButton.BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            else if (mapData[corr[0], corr[1]] == 5)
                            {
                                mapData[corr[0], corr[1]] = 0;
                                senderButton.BackgroundImage = null;
                            }
                            break;
                        //墙
                        case 4:
                            if (senderButton.BackColor == Color.Red)
                            {
                                senderButton.BackColor = SystemColors.Control;
                                mapData[corr[0], corr[1]] = 0;
                            }
                            else if (senderButton.BackColor == SystemColors.Control)
                            {
                                senderButton.BackColor = Color.Red;
                                mapData[corr[0], corr[1]] = 1;
                            }
                            break;
                        default:
                            throw new IndexOutOfRangeException();
                    }

                    break;
                //设置起点
                case 1:
                    if(senderButton.BackColor == SystemColors.Control && StartSet == false)
                    {
                        senderButton.BackColor = Color.Blue;
                        senderButton.BackgroundImage = Properties.Resources.wxzright;
                        senderButton.BackgroundImageLayout = ImageLayout.Stretch;
                        StartSet = true;
                        StartId = id;
                    }
                    else if(senderButton.BackColor == Color.Blue && StartSet == true)
                    {
                        senderButton.BackColor = SystemColors.Control;
                        senderButton.BackgroundImage = null;
                        StartSet = false;
                    }
                    break;
                //设置终点
                case 2:
                    if(!mulEndP)
                    {
                        EndPNum.Text = "0";
                        if (senderButton.BackColor == SystemColors.Control && EndSet == false)
                        {
                            senderButton.BackColor = Color.Purple;
                            senderButton.BackgroundImage = Properties.Resources.hotdog;
                            senderButton.BackgroundImageLayout = ImageLayout.Stretch;
                            EndSet = true;
                            EndId = id;
                        }
                        else if (senderButton.BackColor == Color.Purple && EndSet == true)
                        {
                            senderButton.BackColor = SystemColors.Control;
                            senderButton.BackgroundImage = null;
                            EndSet = false;
                        }
                    }
                    else
                    {
                        if(senderButton.BackColor == SystemColors.Control)
                        {
                            senderButton.BackColor = Color.Purple;
                            senderButton.BackgroundImage = Properties.Resources.hotdog;
                            senderButton.BackgroundImageLayout = ImageLayout.Stretch;
                            Vertex EndPtmp = new Vertex(corr[0], corr[1]);
                            EndPs.Add(EndPtmp);
                        }
                        EndPNum.Text = Convert.ToString(EndPs.Count());
                    }

                    break;
                default:
                    throw new IndexOutOfRangeException();
            }

            if (!mulEndP)
            {
                if (StartSet && EndSet)
                    SearchButton.Enabled = true;
                else
                    SearchButton.Enabled = false;
            }
            else
            {
                if (StartSet && (EndPs.Count() > 1))
                    SAAbtn.Enabled = true;
                else
                    SAAbtn.Enabled = false;
            }

        }


        //搜索事件
        private void SearchButton_Click(object sender, EventArgs e)
        {
            MapReset();
            int[] StartCorr = ID2Corr(StartId);
            int[] EndCorr = ID2Corr(EndId);
            Vertex StatP = new Vertex(StartCorr[0], StartCorr[1]);
            Vertex EndP = new Vertex(EndCorr[0], EndCorr[1]);
            Stopwatch sw = new Stopwatch();
            sw.Reset();
            switch (ModeSearch)
            {
                case 0:
                    sw.Start();
                    BFS(StatP, EndP);
                    sw.Stop();
                    break;
                case 1:
                    sw.Start();
                    BiBFS(StatP, EndP);
                    sw.Stop();
                    break;
                case 2:
                    sw.Start();
                    AStar(StatP, EndP, ModeDistance);
                    sw.Stop();
                    break;
                case 3:
                    sw.Start();
                    AStarPriority(StatP, EndP, ModeDistance);
                    sw.Stop();
                    break;
                case 4:
                    sw.Start();
                    int depth = Convert.ToInt16(Depthtxt.Text);
                    IterDFS(StatP, EndP, depth);
                    sw.Stop();
                    break;
                case 5:
                    sw.Start();
                    GreedyBestFirstSearch(StatP, EndP, ModeDistance);
                    sw.Stop();
                    break;
                case 6:
                    sw.Start();
                    DijkstraSearch(StatP, EndP, ModeDistance);
                    sw.Stop();
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
            Timelabel.Text = Convert.ToString(sw.ElapsedMilliseconds);
        }

        private void WallRb_CheckedChanged(object sender, EventArgs e)
        {
            ObstaleRankBox.Visible = true;
            if (WallRb.Checked)
                Mode = 0;
        }

        private void SAERb_CheckedChanged(object sender, EventArgs e)
        {
            ObstaleRankBox.Visible = false;
            if (SAERb.Checked)
                Mode = 1;
        }

        private void Erb_CheckedChanged(object sender, EventArgs e)
        {
            ObstaleRankBox.Visible = false;
            if (Erb.Checked)
                Mode = 2;
        }

        private void BFSradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (BFSradioButton.Checked)
                ModeSearch = 0;
        }

        private void BiBFSradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (BiBFSradioButton.Checked)
                ModeSearch = 1;
        }

        private void AStarRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (AStarRadioButton.Checked)
                ModeSearch = 2;
        }

        private void MapFreshBtn_Click(object sender, EventArgs e)
        {
            MapReset();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                ModeDistance = 0;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                ModeDistance = 1;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
                ModeDistance = 2;
        }

        private void MapSizeTxt_TextChanged(object sender, EventArgs e)
        {
            int iMax = 100;
            if (MapSizeTxt.Text != null && MapSizeTxt.Text != "") 
            {
                if (int.Parse(MapSizeTxt.Text) > iMax)
                    MapSizeTxt.Text = "99";
            }

        }

        private void MapSizeTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //只允许输入退格键和数字
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void Depthtxt_TextChanged(object sender, EventArgs e)
        {
            int iMax = 10000;
            if (Depthtxt.Text != null && Depthtxt.Text != "")
            {
                if (int.Parse(Depthtxt.Text) > iMax)
                    Depthtxt.Text = "9999";
            }
        }

        private void Depthtxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //只允许输入退格键和数字
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
        private void SaveMapBtn_Click(object sender, EventArgs e)
        {
            bool isSave = true;
            saveFileDialog1.Filter = @"文本文件(*.txt)|*.txt";
            saveFileDialog1.FilterIndex = 1;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog1.FileName.ToString();
                if (fileName != null && fileName != "")
                {
                    string fileExtName = fileName.Substring(fileName.LastIndexOf(".") + 1).ToString();
                    if (fileExtName != "")
                    {
                        switch(fileExtName)
                        {
                            case "txt":
                                break;
                            default:
                                MessageBox.Show("只能保存为txt格式！");
                                isSave = false;
                                break;
                        }
                    }
                    if(isSave)
                    {
                        try
                        {
                            Print(fileName, mapData);
                            MessageBox.Show("地图保存成功！");
                        }
                        catch
                        {
                            MessageBox.Show("保存失败，数据可能已被删除！");
                        }
                    }
                }
            }
        }



        public static void Print(String path, int[,] mat)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    sw.Write(mat[i, j]);
                    if(j < mat.GetLength(1) - 1)
                        sw.Write(' ');
                }
                sw.Write("\n");
            }
            sw.Close();
            fs.Close();
        }

        private void LoadMapBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = @"文本文件(*.txt)|*.txt";
            openFileDialog1.RestoreDirectory = true;
            bool isLoad = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog1.FileName;
                StreamReader sr = new StreamReader(path, Encoding.Default);
                String[] data = { };
                char[] separator = { '\n', '\r', ' '};
                data = sr.ReadToEnd().Split(separator);
                //根据大小新建游戏，并附上障碍
                int datasize = (int)Math.Sqrt(data.Length);
                GameReset();
                MapSize = datasize;
                mapData = new int[MapSize, MapSize];
                for (int i = 0; i < MapSize; i++)
                    for (int j = 0; j < MapSize; j++)
                    {
                        Button bt = new Button();
                        bt.Click += SetPath_Click;
                        bt.Font = new Font("宋体", 6);
                        bt.BackgroundImageLayout = ImageLayout.Stretch;
                        bt.Name = Convert.ToString(i * MapSize + j);
                        bt.Location = new Point((j + 1) * ButtonSize, (i + 1) * ButtonSize);
                        bt.Size = new Size(ButtonSize, ButtonSize);
                        this.ButtonPanel.Controls.Add(bt);
                        MapButton.Add(bt);
                    }
                for (int i = 0; i < MapSize; i++)
                    for (int j = 0; j < MapSize; j++)
                    {
                        mapData[i, j] = (int)Convert.ToInt16(data[i * MapSize + j]);
                        switch(mapData[i,j])
                        {
                            case 0:
                                break;
                            case 1:
                                MapButton[i * MapSize + j].BackColor = Color.Red;
                                break;
                            case 2:
                                MapButton[i * MapSize + j].BackgroundImage = Properties.Resources.dengpai;
                                break;
                            case 3:
                                MapButton[i * MapSize + j].BackgroundImage = Properties.Resources.timg;
                                break;
                            case 4:
                                MapButton[i * MapSize + j].BackgroundImage = Properties.Resources.nuanbaobao;
                                break;
                            case 5:
                                MapButton[i * MapSize + j].BackgroundImage = Properties.Resources.laowang;
                                break;
                            default:
                                throw new IndexOutOfRangeException();
                        }
                    }
                        
                isLoad = true;
            }
            if(isLoad)
            {
                
            }
            
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
                ModeSearch = 3;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                ModeSearch = 4;
                MessageBox.Show("搜索时间可能过长，请慎用，否则程序可能会卡死，建议设置的地图小一点，或者起点与终点的距离近一点，效果与BFS类似。");
            }

        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked)
                ModeSearch = 5;
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton8.Checked)
                ModeDirection = 0;
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton9.Checked)
                ModeDirection = 1;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked)
                ModeSearch = 6;
        }

        private void radioButton14_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton14.Checked)
                ModeObstale = 0;
        }

        private void radioButton13_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton13.Checked)
                ModeObstale = 1;
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton12.Checked)
                ModeObstale = 2;
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton11.Checked)
                ModeObstale = 3;
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton10.Checked)
                ModeObstale = 4;
        }

        private void ObShowBtn_Click(object sender, EventArgs e)
        {
            if(ObsShow)
            {
                for (int i = 0; i < MapButton.Count(); i++)
                {
                    if (MapButton[i].BackgroundImage != null)
                    {
                        if (Convert.ToInt16(MapButton[i].Name) == StartId || Convert.ToInt16(MapButton[i].Name) == EndId)
                            continue;
                        if (MapButton[i].BackColor == Color.Purple)
                            continue;
                        MapButton[i].BackgroundImage = null;
                    }
                }
                ObsShow = false;
                ObShowBtn.Text = "显示障碍物";
            }
            else
            {
                ObsShow = true;
                ObShowBtn.Text = "隐藏障碍物";
                for (int i = 0; i < MapSize; i++)
                    for (int j = 0; j < MapSize; j++)
                    {
                        switch (mapData[i, j])
                        {
                            case 0:
                                break;
                            case 1:
                                break;
                            case 2:
                                MapButton[i * MapSize + j].BackgroundImage = Properties.Resources.dengpai;
                                break;
                            case 3:
                                MapButton[i * MapSize + j].BackgroundImage = Properties.Resources.timg;
                                break;
                            case 4:
                                MapButton[i * MapSize + j].BackgroundImage = Properties.Resources.nuanbaobao;
                                break;
                            case 5:
                                MapButton[i * MapSize + j].BackgroundImage = Properties.Resources.laowang;
                                break;
                            default:
                                throw new IndexOutOfRangeException();
                        }
                    }
            }
        }

        private void radioButton15_CheckedChanged(object sender, EventArgs e)
        {
            //把多起点全部删除
            EndPtrans = false;
            for (int i = 1; i < EndPs.Count(); i++)
            {
                MapButton[EndPs[i].x * MapSize + EndPs[i].y].BackColor = SystemColors.Control;
                MapButton[EndPs[i].x * MapSize + EndPs[i].y].BackgroundImage = null;
            }
            EndPs.Clear();
            if (radioButton15.Checked)
                mulEndP = false;
            SearchWayBox.Visible = true;
            SAAbtn.Visible = false;
            SearchButton.Visible = true;
            label1.Visible = false;
            EndPNum.Visible = false;
        }

        private void radioButton16_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton16.Checked)
                mulEndP = true;
            SearchWayBox.Visible = false;
            SAAbtn.Visible = true;
            SearchButton.Visible = false;
            label1.Visible = true;
            EndPNum.Visible = true;
            //保留之前设置的单终点
            if (EndSet && !EndPtrans)
            {
                int[] EndCorr = ID2Corr(EndId);
                Vertex EndP = new Vertex(EndCorr[0], EndCorr[1]);
                EndPs.Add(EndP);
                EndPtrans = true;
                EndPNum.Text = "1";
            }
        }

        private void SAAbtn_Click(object sender, EventArgs e)
        {
            MapReset();
            int[] StartCorr = ID2Corr(StartId);
            int[] EndCorr = ID2Corr(EndId);
            Vertex StatP = new Vertex(StartCorr[0], StartCorr[1]);
            Vertex EndP = new Vertex(EndCorr[0], EndCorr[1]);
            SAAlgorithm(StatP, EndPs, ModeDistance);
        }
    }
}
