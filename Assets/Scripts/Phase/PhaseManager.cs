using UnityEngine;
using System.Collections.Generic;

public class PhaseManager : MonoBehaviour
{
    public float GameTime = 0f; //ゲーム内経過時間
    public Transform[] SpawnPoints; //敵のスポーン地点
    public Transform Player; //プレイヤー地点
    public List<MonoBehaviour> PhaseList = new List<MonoBehaviour>(); // 発生するフェーズを順番に格納(inspectorから登録)
    private List<IGamePhase> phaseList = new List<IGamePhase>(); // PhaseListをIGamePhase型にキャストしたリスト
    private int currentIndex; //現在のフェーズリストのインデックス

    private void Awake()
    {
        // MonoBehaviour → IGamePhase にキャストしてリストに格納
        foreach (var phase in PhaseList)
        {
            if (phase is IGamePhase gamePhase)
            {
                phaseList.Add(gamePhase);
            }
        }
    }

    private void Start()
    {
        currentIndex = 0;
        SwitchState();
    }

    private void Update()
    {
        if (currentIndex >= phaseList.Count) return;
        
        GameTime += Time.deltaTime;
        phaseList[currentIndex].UpdateState(this);
    }

    public void SwitchState()
    {
        if (currentIndex >= phaseList.Count) return;

        phaseList[currentIndex].EnterState(this);
    }

    public void NextState()
    {
        currentIndex++;
        SwitchState();
    }
}