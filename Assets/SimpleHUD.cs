using UnityEngine;

public class SimpleHUD : MonoBehaviour
{
    private void OnGUI()
    {
        // Fondo / Caja de controles arriba a la izquierda
        GUI.Box(new Rect(10, 10, 240, 150), "CONTROLES (Ghost Infiltration)");
        GUI.Label(new Rect(20, 35, 220, 20), "• W, A, S, D: Moverse");
        GUI.Label(new Rect(20, 55, 220, 20), "• Espacio: Saltar (Suelo Raycast)");
        GUI.Label(new Rect(20, 75, 220, 20), "• E: Interactuar Terminal");
        GUI.Label(new Rect(20, 95, 220, 20), "• Clic Izq: Disparo Stun (Raycast)");
        GUI.Label(new Rect(20, 115, 220, 20), "• Escape: Pausa / Reanudar");

        // Estado y Puntuación arriba a la derecha
        if (GameManager.Instance != null)
        {
            GUI.Box(new Rect(Screen.width - 210, 10, 200, 60), "MISIÓN");
            GUI.Label(new Rect(Screen.width - 200, 35, 180, 20), 
                $"Datos: {GameManager.Instance.currentScore} / {GameManager.Instance.requiredScoreToWin}");

            // Cartel central si hay Pausa, Victoria o Derrota
            if (GameManager.Instance.currentState == GameState.Paused)
            {
                GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 30, 200, 60), "JUEGO PAUSADO");
                GUI.Label(new Rect(Screen.width / 2 - 70, Screen.height / 2, 160, 20), "Presiona ESC para volver");
            }
            else if (GameManager.Instance.currentState == GameState.Victory)
            {
                GUI.Box(new Rect(Screen.width / 2 - 120, Screen.height / 2 - 30, 240, 60), "¡MISIÓN CUMPLIDA!");
                GUI.Label(new Rect(Screen.width / 2 - 90, Screen.height / 2, 200, 20), "Extracción completada con éxito.");
            }
            else if (GameManager.Instance.currentState == GameState.GameOver)
            {
                GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 30, 200, 60), "AGENTE CAÍDO");
                GUI.Label(new Rect(Screen.width / 2 - 80, Screen.height / 2, 160, 20), "Misión fallida (Sin vida)");
            }
        }
    }
}