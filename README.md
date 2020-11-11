PEC 2 - Un juego de plataformas
===============================

¿Qué es?
--------

Un clon del juego de plataformas Super Mario Bros.

Nos situamos en algún lugar remoto de la Vía Láctea. Año terrestre 1958. Nalesa,
nuestra protagonista, es enviada al satélite Bubbles para su prospección. Su
objetivo, izar la bandera de la Confederación para que sus camaradas sepan que
es seguro el desembarco en tierra firme. La misión no será fácil.

Podremos desplazarnos por el satélite usando los cursores de nuestro controlador
de juegos. Haremos que Nalesa salte pulsando el botón A/Espacio y que corra
manteniendo presionado el botón B/Control. Pulsando el botón B/Control en
ráfagas cortas el personaje escupirá pelotas de ping-pong en llamas; siempre y
cuando haya tomado antes una buena dosis de la Flor del Fuego.

Vídeo de demostración
---------------------

![Demo](Resources/demo.webm)

¿Qué se ha implementado?
------------------------

* Mecánicas de movimiento del avatar.
* Lógica y movimiento de los enemigos.
* Seguimiento del personaje con la cámara.
* Campo de juego en diferentes capas de colisión.
* Muerte de los personajes y vidas múltiples.
* Cronometro que limita el tiempo de juego.
* Lógica de recompensas, power-ups y bloques rotos.
* Interfaz con recuento de puntos, monedas, vidas y tiempo.
* Animaciones, sonidos y efectos de partículas.

Detalles de la implementación
-----------------------------

El terreno de juego se ha implementado usando por una parte varias capas de
_tile maps_ (terreno pisable y no pisable) y por otra sprites con colisionadores
(bloques, power-ups y personajes). A su vez, los objetos se organizan en
distintas capas de colisión (Player Dome, Monster Dome y Shared Dome).

En cuanto a la implementación, los controladores más relevantes són los
siguientes que pueden encontrarse en el paquete Shared/Controllers.

* InputController - Hace que el personaje se mueva por el campo de juego según
  las ordenes del usuario. El protagonista puede caminar, correr, saltar o
  lanzar bolas de ping-pong en llamas.

* PlayerController - Lógica de estado del personaje, animación de sus
  movimientos y recolección de power-ups.

* MonsterController - Lógica de animación de los enemigos y de su estado. El
  movimiento de los enemigos se implementa en el controlador MotionController.

Así mismo, en el paquete Shared/Handlers se implementa la lógica de eventos y
colisiones del personaje con su entorno. Los más importantes son los siguientes.

* OnMonsterCollision - Cuando el personaje choca con un enemigo, decide si se va
  a dañar al personaje o al enemigo y en qué grado.

* OnGiftboxCollision - Si el protagonista choca contra una caja que contiene uno
  o mas premios activa el siguiente premio en la cola.

* OnBrickKeyTrigger - Para activar una caja de premio se delega en esta clase la
  colisión del personaje con su llave (un colisionador interior de la caja), de
  manera que sólo se activará si el personaje choca con la caja por debajo y la
  mueve a cierta altura. La caja contiene además un Spring Joint para devolverla
  a su lugar después de la colisión.

* OnRewardCollision - La mayoría de bloques contienen premios (monedad, puntos
  o power-ups). Está clase, de la que derivan todos los premios, es la que los
  entrega al protagonista guardándolos en su cartera (PlayerWallet).

Para los sonidos se ha creado el servicio Services/AudioService que contiene
un singleton encargado de reproducirlos en alguno de los múltiples AudioSource
que contienen los objetos del juego.

Para poder centralizar los sonidos, estos se configuran en la clase AudioTheme y
se les asigna un nombre de evento. Luego las clases de Shared/Handlers los
reproducen pasando un nombre de evento y AudioSource al servicio AudioService.

La última versión
-----------------

Puede encontrar información sobre la última versión de este software y su
desarrollo actual en https://gitlab.com/joansala/uoc-platformer

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
