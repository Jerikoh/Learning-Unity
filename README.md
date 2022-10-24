# Learning-Unity

### Folder: Desafíos
> Ejercicios efectuados tras cada nuevo aprendizaje.
##
### Folder: Proyecto Final
> Progresos de mi primer juego, aka **ɴᴜʟʟ ꜱᴘᴀᴄᴇ** / **ᴅᴇɴꜱᴇʀ ʟɪɢʜᴛꜱ**.
##
### Build
> Último build del proyecto en curso.
[Descargar (Google Drive)](https://drive.google.com/drive/folders/1wmCVdZ-ly5E-kStmxGM_Eeb8T-aoVywp?usp=sharing)
##


```
**ɴᴜʟʟ ꜱᴘᴀᴄᴇ** 
//you have no context
//press space
//stay
//quiet
```
+ *Posibles orientaciones:* horror survival, open world horror survival, pve/pvp shooter.
+ *Posibles fuentes de inspiración:* Outlast, Alien Isolation, Resident Evil 4, Dead Space -buscar mas scifi/space horror games-, no more room in hell, contagion, Obscure, cold fear, killing floor, nightmare of decay, evil within, silent hill 2, the forest, metro exodus, stalker, soma, cry of fear, lost in vivo, paratopic, madison, Yuki Onna, dusk, darkwood, lidar, alan wake, half life, blair witch, the medium, Gloomwood, Total Chaos, Divine Frequency, Cultic, Siren.

### To-Do (organize):
- Pulsos de sonar individuales, no contínuos; esto habilitaría a implementar algunas de las propuestas aquí listadas.
- Poder definir manualmente de cada pulso su frecuencia y radio, con sus costes de energía y "overheat" proporcionales, donde tras cada pulso el dispositivo deba "enfriarse" y si se genera antes otro pulso este consumiría mas energía en relacion al grado de sobrecalentamiento.
- Sistema de stamina, para no poder uir indefinidamente de algunos enemigos y tener que "racionarla".
- Tratando de reducir la presencia de UI, que la energía disponible se indique a traves del pitch de sonido de cada pulso (que en primer lugar debería poder emitir), disminuyendo a la par de la energía; y el estado de salud, vía efectos visuales y sonoros (screen shake y/o blur pulsantes con frecuencia relativa, latidos sutiles, quejidos relativos al grado de daño, etc). Desde ya, haría que la salud se muestre de forma cualitativa y no cuantitativa (mostrando un "estado" de salud y no un número).
- Item arrojable que emita pulsos temporalmente en un rango, a modo de flare, que al agotarse su energía debas ir a buscarlo y recargarlo -no sería descartable como una granada-, asignándole energía del total de tu reserva.
- Un tipo de proyectil quizá para arco/ballesta que revele su recorrido tras disparar, al emitir pulsos rápidamente en el trayecto (en una distancia finita).
- Capacidades de agacharse y asomarse ante paredes, estados que afectarían la capacidad de detección enemiga (menores velocidades y distancias de deteccion).
- Lograr que el control del radio de la pulsacion seria relativo a poder controlar el rango de vision y deteccion (como caminar lento o agacharse).
- Hacer que los enemigos, objetos recogibles, señales, etc. no "brillen" al ser escaneados, sino que su scan sea de un color "definidamente" sobre su modelo.
- Los items no deberian brillar constantemente, sino ser revelados en el pulso/scan, requiriendo que el player esté atento al entorno revelado en cada pulsacion.
- Lo revelado por cada pulsacion, o al menos items y/o enemigos, podrian permanecer mas tiempo visibles antes de desvanecerse, y el scan solo debería visualizar una "instantánea" del momento 3d, no simplemente hacerlo visible en tiempo real (no debería verse el movimiento de lo revelado)que se siga percibiendo el movimiento de lo iluminado; quizá también que lo capturado persista un breve momento.
- Cada estado y cambio de estado de criaturas -y player- debería implicar un sonido y animación.
- Los items pickeables faltantes: flares, salud (tres grados), munición (para cada arma y uno completo), "batería" (tres grados), llaves (comunes y para casos especificos).
- Menú de pausa, que tenga: level restart, opciones (con al menos capacidad para cambiar sensibilidad del mouse) salir al menu, salir del juego. Con efecto fade-in blur y muteando background sound.
- Grupos de animaciones idle y en movimiento para player desarmado y armado con melee, entre las que ir alternando segun lo equipado.
- Efectos al morir; un leve shake del "death text", se detiene un momento y antes de respawn inicia uno mas de salida, sumando a un sonido de aturdimiento que vuelve a la normalidad (algo como en CoD II). //recuperar un efecto parpadeante del scan que se daba como "bug"
- El pulso "de sonar" no debería moverse junto al jugador, sino que debería permanecer en la posicion de origen, y actualizarse en el próximo pulso.
- Cuando la onda deja de pasar por encima de los objetos, sus texturas deberían dejar de renderizarse o volverse transparentes, así la silueta de los objetos no "iluminados", no se superponen contra aquellos iluminados por la onda. //se conseguiria con pointcloud + raycasting o emisión de partículas
- Pulir y agregar estados de criaturas: idle, alerta, investigar, buscar, chase, feed.
- Actualmente la "IA" de criaturas, al perder de vista al player durante unos segundos, retoma su ruta o posición establecidas; estaría bueno que genere waypoints al azar en cierto rango y se dirija hacia algunos durante unos segundos, antes de volver a su ruta/posición.
- Sonido espacial realista y adaptativo.
- Recoger items e interactuar con otros de forma manual y no al colisionar, mostrando también un texto donde se diga de que se trata el item, o no para que el player tenga que discernir e identificarlo por si mismo.
- Más enemimgos; stalker, climber, heavy, runner, possessed... la estética rondaría "The Thing" o "Dead Space".
- Animaciónes vía inverse kinematics.
- Menu para confirmar respawn tras muerte.
- Implementar checkpoints.
- Scroll down/up para rotar armas equipadas.
- Sistema de inventario y selector (estilo Half Life).
- Los sonidos de los triggers correspondientes a la voz del player deberían yacer "en" el player -por el posicionamiento sonoro-, y deberían reproducirse de forma condicional (ej: que si estas en combate, no se activen los sonidos gatillados al acercarte a las puertas, o que algunos solo si los enemigos estan a X distancia, o si ya fueron reproducidos en X tiempo, o si aun no sucedió X cosa).
- Enemy ragdolls.
- Cambiar o pulir varios sonidos y animaciones.
- Hay cierto retraso en la reproducción de los sonidos, debería ver como evitarlo.
- Homogeneizar texturas, ya que son innecesarias al ser invisibles.
- Quitar produccion de sombras e iluminación.
- Implementar occlusion culling.
- Convertir en mp3 todos los archivos de audio.
- Rehacer el "spaghetti code"; en varios scripts solo procuré que cumplan su función in-game, pasando por alto si fue la forma mas óptima, escalable, reusable o legible de implementarlos.
...

##