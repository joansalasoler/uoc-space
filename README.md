PEC 3 - Un juego de artillería
==============================

¿Qué es?
--------

Una ampliación del juego implementado para la PEC 2 que añade nuevos sistemas
de partículas, animaciones y nuevas inteligencias para los enemigos.

Vídeo de demostración
---------------------

![Demo](Resources/demo.webm)

¿Qué se ha implementado?
------------------------

* Animación del protagonista y los enemigos.
* Sistemas de partículas de choque, bolas de fuego y lluvia.
* Inteligencia artificial de los Koopas y los Goombas.
* Posibilidad de disparar bolas de fuego.
* Estado del personaje con múltiples vidas.
* Etiquetas y capas de colisión.

Detalles de la implementación
-----------------------------

Las partes importantes del nuevo código son las siguientes:

* Shared/Controllers/MotionController: Controla el movimiento de los enemigos
  y las recompensas. Hace que estos se muevan continuamente en una dirección y
  si chocan con un objeto en su horizontal canvien de dirección.

* Shared/Controllers/MonsterController: Inteligencia artificial básica de los
  enemigos junto a Shared/Handlers/Actors/OnMonsterCollision. El comportamiento
  basico es que los enemigos se muevan en una dirección y cambien de sentido
  cuando colisionan con un objeto. Si colisionan con el jugador, se comprueba
  si la colisión ha sido con la cabeza o el cuerpo; si ha sido con la cabeza
  muere el monstruo, en caso contrario muere el jugador.

* Shared/Controllers/KoopaController: Inteligencia artificial de los caracoles.
  Extiende "MonsterController" para añadir la capacidad de que los enemigos de
  este tipo se escondan en su caparazón cuando el jugador les salta encima. Una
  vez escondidos en el caparazón pasan a ser artillería del jugador, pues este
  puede empujarlos y usarlos para matar a otros enemigos.

* Shared/Handlers/OnKoopaCollision: Parte de la inteligencia artificial de los
  caracoles encargada de la gestión de las colisiones. Si el jugador salta
  encima de un caparazón este empezará a moverse y rebotar por el terreno de
  juego si estaba parado o se parará si ya estaba moviéndose. Si un caparazón
  que se encontraba en movimiento choca contra el jugador o un enemigo en su 
  horizontal estos mueren.

* InputController: El método "ThrowFireball" es el encargado de lanzar la bolas
  de fuego cuando el protagonista ha ingerido una flor. Las bolas reciben
  inicialmente una fuerza de empuje proporcional a la velocidad del jugador
  además de una fuerza de giro. Cuando una bola choca contra un enemigo este
  muere y la bola desaparece. También desaparece una bola de fuego cuando choca
  con un objeto del terreno de juego en su horizontal (OnFireballCollision).

Por otra parte, los sistemas de partículas implementados son los siguientes:

* Partículas de polvo cuando el jugador choca contra algún objeto que le reporta
  una puntuación. Las partículas de polvo se crean en el método "OnPointsEarned"
  de la clase "Scenes/Game/ScoreboardController". Este es invocado por la clase
  "Shared/Handlers/OnRewardCollision" de la que heredan todos los enemigos, los
  bloques y las recompensas (setas, estrellas y flores).

* Partículas en forma de rastro (_trail_) cuando se lanza una bola de fuego. Estas
  siguen a la bola de fuego en su trayectoria.

* Partículas de estrellas en el personaje. Cuando el protagonista recoge una
  estrella estas partículas salen del actor mientras el power-up siga activo.
  Para un efecto más dramático estas partículas se crean en _world-space_
  de manera que dejan un rastro por dondequiera que el personaje pase.

* Partículas de lluvia. Cuando al jugador se le está agotando el tiempo de juego
  empezará a llover sobre el terreno. Estas partículas de lluvia se han creado
  como _sprites_ animados de manera que rebotan cuando colisionan con el suelo
  u otro objeto de la escena. Existe también una segunda capa de lluvia sin
  colisión como fondo del escenario.

Para implementar la inteligencia artificial de los personajes se han usado
diferentes capas de colisión:

* Player: Etiqueta el jugador que colisiona contra todo excepto el dominio de
  los monstruos  ("Monster Dome") y las bolas de fuego ("Fireballs").

* Monster: Etiqueta a los enemigos y hace que estos colisionen contra todo
  excepto el dominio del jugador ("Player Dome").

* Player Dome: Etiqueta para los objectos con los que únicamente puede
  colisionar el jugador. Se etiquetan con ella las recompensas como son las
  flores, las setas y las estrellas.

* Monster Dome: Etiqueta para los objectos con los que únicamente pueden
  colisionar los monstruos. Se etiquetan con ella algunos colisionadores que
  establecen los límites de los precipicios y de esta manera los enemigos
  cambiarán de dirección al aproximarse a ellos en lugar de caer al vacío.

* Fireballs: Todas las bolas de fuego tienen esta etiqueta que hace que no
  colisionen entre ellas ni con el jugador. Tampoco colisionaran con los
  dominios del jugador y los monstruos.

* Shells: Cuando es activado el caparazón de un caracol esta pasa a ser su
  capa de colisión. Esto se hace para que el caparazón no colisione con la
  capa "Monster Dome" y así pueda caer al vacío cuando es impulsado.

La animación de los enemigos y el protagonista se hace con _sprites_. En el caso
del jugador, el controlador de animación contiene animaciones para caminar,
correr, saltar, morir, daño al actor y disparar. Los enemigos tienen animaciones
para caminar y morir. A los Koopa se les ha añadido además una animación para
esconderse en su caparazón.

La última versión
-----------------

Puede encontrar información sobre la última versión de este software y su
desarrollo actual en https://gitlab.com/joansala/uoc-artillery

Referencias
-----------

Todas las imágenes y sonidos del juego se han publicado con licéncias que
permiten la reutilización y distribución. Son propiedad intelectual de sus
respectivos authores. Algunos de los recursos se han creado o modificado
exclusivamente para su uso en el juego por el autor, Joan Sala Soler.

[1]  ALEX36917. 2020. Woman_small ow 1 [Efecto de sonido].
     Disponible en: https://freesound.org/people/alex36917/sounds/509722/

[2]  DUSTYROOM. 2014. Dustyroom Casual Game Sounds [Efectos de sonido].
     Disponible en: https://dustyroom.com/casual-game-sfx-freebie

[3]  FOOLBOYMEDIA. 2015. C64 Melody [Búcle musical]. Disponible en:
     https://freesound.org/people/FoolBoyMedia/sounds/275673/

[4]  HARAMIR, Anderson. 2017. Death [Efecto de sonido]. Disponible en:
     https://freesound.org/people/Haramir/sounds/404014/

[5]  KENNEY. 2019. Background Elements Redux [Imágenes]. Disponible en:
     https://www.kenney.nl/assets/background-elements-redux

[6]  KENNEY. 2016. Physics Assets [Imágenes]. Disponible en:
     https://www.kenney.nl/assets/physics-assets

[7]  KENNEY. 2013. Platformer Pack Redux [Imágenes]. Disponible en:
     https://www.kenney.nl/assets/platformer-pack-redux

[8]  LEGNALEGNA55. 2020. Female voice YA WHOO [Efecto de sonido].
     Disponible en: https://freesound.org/people/Legnalegna55/sounds/539216/

[9]  MCINERNEY, Matt. 2009. Orbitron [Tipografía]. Disponible en:
     https://www.theleagueofmoveabletype.com/orbitron

[10] NATHANAELSAMS. 2011. Running on grass with wet feet [Efecto de sonido].
     Disponible en: https://freesound.org/people/nathanaelsams/sounds/127955/

[11] NICK121087. 2014. Glass break [Efecto de sonido].
     Disponible en: https://freesound.org/people/nick121087/sounds/232176/

[12] NOX_SOUND. 2019. Ambiance River Flow Small Loop [Efecto de sonido].
     Disponible en: https://freesound.org/people/Nox_Sound/sounds/479321/
