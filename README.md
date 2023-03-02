
# Batalla naval

Programar en Unity un videojuego en 3D llamado BatallaNaval

El juego consiste en la recreación de una batalla entre dos barcos de guerra que se cañonean entre si. El jugador maneja uno de los barcos mientras el otro barco es manejado por el ordenador.

La programación del barco enemigo está completada y no es necesario realizar ninguna modificación sobre ella. El comportamiento del barco enemigo se puede resumir como dirigirse hacia el barco del jugador escogiendo en ciertas condiciones cambios de rumbo al azar y manteniéndose a una distancia de él mientras realiza tiros de artillería en su dirección calculando la distancia de tiro de forma aproximada.

## Movimiento del barco

El barco del jugador se moverá de forma cinemática, sin emplear para ello el Rigidbody del mismo. Los movimientos consistirán en el desplazamiento hacia adelante y el giro. Con el objetivo de mantener el ejercicio un poco más simple, no se contempla el movimiento hacia atrás.   

El movimiento hacia adelante se controla por niveles de potencia. Los niveles van de 0 a 6 y en cada nivel la velocidad que se alcanza será el resultado de multiplicar una velocidad base de 0.8 m/s por el nivel de potencia establecido. Se debe controlar que el nivel de potencia se mantenga dentro de los márgenes especificados.   

El nivel de potencia se ajustará por el jugador usando las teclas W y S para subirlo y bajarlo respectivamente.  

El nivel de potencia seleccionado y la velocidad se mostrarán en el Dashboard como se detalla más abajo. Se debe controlar que el nivel de potencia se mantenga dentro de los márgenes especificados.   

El giro del barco se controlará mediante niveles de timón que van desde el -3 al 3 representando giros hacia la izquierda y hacia la derecha, respectivamente. El nivel de timón se ajustará por parte del jugador usando las teclas A y D para disminuir o aumentar el nivel, lo que significa girar hacia la izquierda o hacia la derecha, respectivamente. La velocidad base de giro será de 1.2 º/s y este valor se multiplicará por el nivel de timón para obtener la velocidad de giro del barco. Se debe controlar que el nivel de timón se mantenga dentro de los márgenes especificados.   

El giro del barco afecta a la velocidad de avance del mismo. Se considera que el hecho de girar provoca mayor resistencia al avance, por lo que para un nivel de potencia dado, la velocidad alcanzada disminuirá. La cantidad en la que disminuye la velocidad de avance será una cantidad base de 0.2 m/s, que se multiplicará por el valor de nivel de timón.   

Se deberá tener en cuenta además, que si el barco está parado no se puede girar y que, en este caso tampoco afecta el aumento de la resistencia al avance, ya que no hay avance, por lo que la velocidad permanecerá a cero.   

El nivel de timón aplicado se debe mostrar en el Dashboard como se detalla más abajo. Asimismo se deben reflejar los cambios en velocidad debidos al giro realizado.

## Torreta de cañones

El movimiento de la torreta de cañones se controla usando las teclas flecha izquierda y flecha derecha para controlar su giro en torno al eje vertical y las flechas arriba y abajo para el pivotaje vertical de los cañones. El elemento que se debe hacer rotar horizontalmente para girar la torreta es el GameObject Cabin Pivot. Para girar verticalmente los cañones se debe rotar el GameObject Cannon Pivot.  

La velocidad de giro horizontal de la torre es de 60 º/s y la velocidad de pivote de los cañones es de 40 º/s. No es necesario controlar los límites de giro de los cañones. Se comtrolará que los cañones no giren bajando más allá de la horizontal ni alcancen un ángulo de más de 75º respecto de esta. Se entiende la horizontal como el plano horizontal del objeto en el que están montados.  


## Disparo de los cañones

Pulsando la tecla “Space” se producirá el disparo simultáneo de los dos cañones. Para efectuar el disparo se deberá instanciar el prefab CannonBullet (incluido en el proyecto) en la posición y orientación señaladas por el componente ProjectileOrigin de cada cañón.   

Para impulsar el movimiento del proyectil, al nuevo objeto generado se le aplicará una fuerza en su dirección forward de 600 N.   

Al efectuar el disparo se debe activar, en cada uno de los cañones, el ParticleSystem incluido en el GameObject ShotExplosion.   

Además se animará un movimiento de retroceso de los cañones y blablabla.   

Tras efectuar un disparo se debe esperar 4 s de tiempo de carga antes de poder volver a disparar. Durante este tiempo el cañón no responderá a la tecla “Space”.   

El estado de disponibilidad o no para disparar se debe reflejar en el UI según se especifica más abajo   


## Flotación y balanceo del barco

Para imitar la flotación del barco en el mar, subiendo y bajando y balanceándose, crearemos una simulación cinemática de ambos movimientos.   

Para el movimiento vertical, completaremos el script ShipBuoyancyKinematic . Este script debe aplicar una aceleración proporcional a la distancia entre el punto de equilibrio de flotación, que por simplicidad supondremos a altura 0, y de sentido hacia ese punto de flotación. Esto es, si el barco está por debajo de 0, la aceleración será positiva y si está por encima será negativa. El script también dará el impulso inicial sacando el barco de su equilibrio para que este comience a subir y bajar.   

Para el balanceo, consideraremos que este siempre se produce como un giro en torno al eje Z del barco. La idea es similar a la de la flotación. El equilibrio del barco será con su eje Y alineado con el del mundo. Si el eje Y del barco forma un ángulo con el eje vertical del mundo, el barco experimentará una aceleración angular (usando físicas hablaríamos de un par o torque) proporcional al ángulo de inclinación, y en sentido de acercar el barco al punto de equilibrio. Para determinar el sentido de esta aceleración precisaremos usar el producto vectorial.    

Si el barco es impactado por una bomba esta ejercerá una fuerza que lo hará balancearse. Simularemos este comportamiento de modo cinemático. Cuando la bomba impacte con el barco usaremos la componente de su velocidad en el eje X del barco para calcular la aceleración angular ejercida en torno al eje Z. Esta aceleración, que será de gran magnitud, se ejercerá solo durante un periodo de tiempo muy pequeño, volviendo después el barco a su búsqueda del equilibrio.  

## Interfaz de usuario

En el interfaz de usuario se mostrará el nivel de potencia seleccionado y la velocidad alcanzada por el barco. La velocidad mostrada deberá actualizarse tanto cuando cambia el nivel de potencia seleccionado como cuando cambia el giro de timón aplicado, dado que este afecta a la velocidad. La velocidad debe mostrarse en nudos. El factor de conversión de m/s a nudos es 1.944.   

El nivel de timón aplicado deberá mostrarse mediante el número que indica cuanto se ha girado el timón, que siempre se mostrará con un valor positivo, mientras que se indicará con las letras L o R según el giro sea hacia la izquierda o hacia la derecha. Cuando el timón esté en posición neutral no se mostrará ninguna información.   

Estos dos valores se usarán mediante el sistema IMGUI de Unity, apareciendo en la parte inferior de la pantalla, apareciendo los elementos de izquierda a derecha en el orden citado.   

El estado de carga del cañón se mostrará con una imagen verde o roja en la parte inferior derecha de la pantalla. Para mostrar la imagen se usará un elemento Label sin texto.  
