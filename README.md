# TP2 - Programación de Videojuegos 1

- **Estudiante:** [Tu Nombre y Apellido]
- **Legajo / LU:** [Tu número]
- **Unity:** 6.3 LTS
- **Perspectiva:** Primera Persona
- **Fecha:** 23/10/2026

## Propuesta: Ghost Infiltration
Prototipo de infiltración en primera persona. El agente debe recorrer un terreno hostil, eludir a un centinela patrullero con un arma aturdidora, recolectar 3 módulos de datos y llegar a la terminal de extracción.

## Requisitos obligatorios
**Terreno:** desniveles esculpidos con Raise/Lower Terrain, texturizado con Terrain Layers y Layer `Ground`.

**Enemigo con NavMesh:** `NavMeshSurface` horneada sobre el terreno, `NavMeshAgent` y script `EnemyAI` con patrulla entre 3 waypoints y persecución al detectar al jugador.

**Raycast:** `PlayerMovement` usa un Raycast descendente desde `GroundCheck` contra la Layer `Ground` para validar el salto (sin salto infinito). `PlayerInteraction` proyecta un Raycast desde la cámara para interactuar con la terminal (Layer `Interactable`, tecla E).

**GameManager:** Singleton con máquina de estados (`Playing`, `Paused`, `GameOver`, `Victory`), control de vida, conteo de datos 3/3, pausa con Escape y notificaciones por consola y HUD.

## Opcionales implementados
- **A - Arma por Raycast:** clic izquierdo dispara un Raycast contra la Layer `Enemy`; al impactar, `Stun()` inmoviliza al centinela 4 segundos.
- **D - Zona de Barro:** trigger que reduce la velocidad del jugador mientras permanece dentro.
- **F - Gas Tóxico:** trigger que aplica daño continuo por segundo.

## Controles

Moverse	W, A, S, D
Mirar	Mouse
Saltar	Barra Espaciadora
Disparo aturdidor	Clic Izquierdo
Terminal Interactuar	E
Pausa	Escape

