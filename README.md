# Learning-Unity

### Folder: Practice
> Some exercises involved in my learning process
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

### To-Do:
+ homogeneizar texturas, ya que serán innecesarias
+ implementar acciones y animaciones del player (usar/recoger*, disparar, cuerpo a cuerpo, reacciones a daño, muerte, etc.) *ver si para esto se peude usar esa "nueva" técnica de IK
+ implementar IA y animaciones del enemy, y decidir su modelo
+ de ser necesario implementar al menos tres variantes de enemy (crawler, sneaky, climber, tank, etc.)
+ implementar throwable que haga de flare
+ ver si puedo con un solo "pp detector script" renderizar cualquier otra cantidad, sin hacer uno para cada onda/pulso
+ ver si puedo hacer que los proyectiles dejen un trace que revele el mundo por un breve momento
+ pickeables: flares, salud, munición, "batería", llaves, mensajes/códigos
+ asegurarme de poder controlar las variables del shader in runtime
+ asegurarme de que la onda no persiga al jugador y que permanezca en la posición de origen, sinó tendré que hacer un placeholder que se posicione en la posicion del jugador cada cierto tiempo y que tambíen se desactive antes de que emita otro pulso
+ ver si se puede poner de otro color sobre ciertos objetos, sin lo que representa actualmente (como la otra implementación con layered rendering?)
+ ver/preguntar si se puede detectar una especie de colision y provocar un evento (quizá con layer rendering?); otra es usar raycasting cuando se instancia el emmiter en la posición del jugador, pero no iría tan acorde con la onda y su propagación
##