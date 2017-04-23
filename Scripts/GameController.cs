using UnityEngine;
using System.Collections;

public enum GameState {
    Menu,
    Playing,
    End
}

public class GameController : MonoBehaviour {

    public GameObject startUI;
    public GameObject gameoverUI;


    public static int[] xOffsets = new int[3]{
                                  -3,0,3
                              };


    public static GameState gameState = GameState.Menu;

    private int score;
    private float startZ;
    private Transform player;

    void Awake() {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        startZ = player.position.z;
    }

    void Update() {
        if (GameController.gameState == GameState.Menu) {
            if (Input.GetMouseButtonDown(0)) {
                gameState = GameState.Playing;
                startUI.SetActive(false);
            }
        }
        if (GameController.gameState == GameState.End) {
            gameoverUI.SetActive(true);
        }
        score = (int)(player.position.z - startZ);
    }


    void OnGUI() {
        GUILayout.Label("Score : " + score);
    }

}
