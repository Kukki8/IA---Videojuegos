# IA---Videojuegos
# Lights on!

## Setting V 1.0

Se presenta un mundo oscuro donde el jugador debe rescatar `Cobardes` que se encuentran perdidos en el laberinto. El mismo esta inundado de `Fantasmas` y criaturas temibles que asustan a los `Cobardes`. A diferencia de ellos, el jugador cuenta con una `Vela` que le permite ahuyentar algunas de las criaturas, permitiendole asi, dirigirlos a una `Zona segura` donde los enemigos no podran alcanzarlos. Sin embargo, la `Vela` no solo se consume luego de un tiempo, sino tambien resulta inutil contra ciertos enemigos, quienes de igual forma podran apartar al jugador de los `Cobardes`.

## Personajes

### Vela/Jugador
- Controlado por el jugador
- Posee una `Vela` que se consume con el tiempo
- La `Vela` le permite ahuyentar `Fantasmas`
- Con la `Vela`, los `Cobardes` pueden seguir al jugador.
- Si la `Vela` se consume, los `Cobardes` seguiran al jugador por unos segundos, antes de `caer en panico` (Es decir, salir huyendo. Esto ocurre aun si un `Fantasma` no los persigue).

### Cobarde
- Su comportamiento normal es de `Wander`
- Cuando entra en el rango de un `Fantasma`, huye del mismo hasta que es alcanzado.
- Al ser alcanzado, sigue al `Fantasma`.
- De ser rescatado por la vela, sigue al jugador.
- Si el jugador logra llevar al `Cobarde` a la zona segura, el mismo escapa del laberinto
### Fantasma
- Patrulla por el mapa haciendo uso de `Path following`
- Una vez que un `Cobarde` entra en su zona, lo persigue hasta alcanzarlo.
- Una vez que alcanza al `Cobarde`, sigue su ruta.
- Solo puede `Poseer` a un `Cobarde` a la vez.
- Si el jugador entra en su rango con la `Vela`, el `Fantasma` huye en direccion contraria.
- No puede acceder a la `Zona segura`

### Aranita
- Es inmune al efecto de la `Vela`
- Puede secuestrar `Cobardes` incluso si los mismos estan siguiendo al jugador (con o sin la `Vela`)
- Pueden tener 2 `Cobardes` a la vez
- Vulnerabilidad: WIP
- No puede acceder a la `Zona segura`

## Objetivo
 Rescatar a todos los cobardes

 ## Condicion de derrota

 Perder a todos los cobardes 

 WIP
