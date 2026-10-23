using UnityEngine;

public class ExtractionTerminal : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        if (GameManager.Instance == null) return;

        if (GameManager.Instance.CanExtract())
        {
            Debug.Log("[Terminal] ¡Códigos validados! Iniciando extracción...");
            GameManager.Instance.SetState(GameState.Victory);
        }
        else
        {
            int faltantes = GameManager.Instance.requiredScoreToWin - GameManager.Instance.currentScore;
            Debug.Log($"[Terminal] Acceso denegado: Faltan {faltantes} paquetes de datos para desbloquear.");
        }
    }
}