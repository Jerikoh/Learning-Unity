# Learning-Unity

### Folder: Practice
> Sandbox donde se cocinan los ejercicios prácticos y cada instancia del proyecto final
##
### Folder: Desafíos
> Ejercicios efectuados tras cada nuevo aprendizaje
##
### Folder: Proyecto Final
> Progresos de mi primer juego, aka **ɴᴜʟʟ ꜱᴘᴀᴄᴇ** 

```
**ɴᴜʟʟ ꜱᴘᴀᴄᴇ** 
//you have no context.
//press space
//stay
//quiet
```
+ Tres posibles orientaciones: horror survival, horde survival, pve/pvp shooter.
+ Inspiraciones posibles: outlast, isolation, re4, dead space -buscar mas scifi/space horror games-, no more room in hell, obscure?, el no-l4d, "el que es como el obscure pero en un barco"...
+ Escenarios posibles, en principio conceptual: highschool, forest, mansion, city, town...

### To-Do (organize):
+ homogeneizar texturas, ya que serán innecesarias
+ implementar acciones y animaciones del player (usar/recoger*, disparar, cuerpo a cuerpo, reacciones a daño, muerte, etc.) *ver si para esto se peude usar esa "nueva" técnica de IK
+ implementar IA (stealth, enemy vision/detection) y animaciones del enemy, y decidir su modelo
+ de ser necesario implementar al menos tres variantes de enemy (crawler, sneaky, climber, tank, etc.)
+ implementar throwable que haga de flare
+ ver si puedo con un solo "pp detector script" renderizar cualquier otra cantidad, sin hacer uno para cada onda/pulso
+ ver si puedo hacer que los proyectiles dejen un trace que revele el mundo por un breve momento
+ pickeables: flares, salud, munición, "batería", llaves, mensajes/códigos; y sus sistemas correspondientes
+ asegurarme de poder controlar las variables del shader in runtime
+ asegurarme de que la onda no persiga al jugador y que permanezca en la posición de origen, sinó tendré que hacer un placeholder que se posicione en la posicion del jugador cada cierto tiempo y que tambíen se desactive antes de que emita otro pulso
+ sobre lo anterior, caso disparos; para inicialización de proyectiles, activar el componente camara y el del proyectil tras spawnear el proyectil?
+ ver si se puede poner de otro color sobre ciertos objetos, sin lo que representa actualmente (como la otra implementación con layered rendering?)
+ ver/preguntar si se puede detectar una especie de colision y provocar un evento (quizá con layer rendering?); otra es usar raycasting cuando se instancia el emmiter en la posición del jugador, pero no iría tan acorde con la onda y su propagación
+ ver como iría la mecánica en cada "variante de juego"; al no moverse se vuelve "invisible", no castea scan, pero sigue siendo encontrable via colisión; si se hace manual el tiempo en que dura el raycasting se vuelve detectable (pensar si lo delata o no inmediatamente o por proximidad por cierto sonido nomas)
+ idem; se podria hacer manual el scanning, que sea como un "overheat", y que puedas manipular la amplitud y velocidad o algo asi en funcion de estos valores?
//pero MP no serivira al ser desync, o si? pudiendo reaccionar a los disparos sorpresivos del otro? y pudiendo quedarte quieto para ser indetectable?
+ aplicar sistema sonido holofónico //ese era el término?
##