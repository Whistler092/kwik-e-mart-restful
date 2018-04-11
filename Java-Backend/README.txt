-Instalar JDK 8
    Abrir la terminal Ctrl+Alt+T

    Actualizar el repositorio:

    sudo add-apt-repository ppa:openjdk-r/ppa
    sudo apt-get update

    Correr los siguientes comandos en la terminal:

    sudo apt-get install openjdk-8-jdk
    sudo apt-get install openjdk-8-source

    Escriba la línea de comando como se muestra a continuación

    apt-cache search jdk

    (Note: openjdk-8-jdk es un ejemplo, se puede cambiar por la version requerida.)

    Para el "JAVA_HOME" (Variable de entorno) escriba la línea de comando como se muestra a continuación...

    export JAVA_HOME=/usr/lib/jvm/java-8-openjdk

    (Nota: "/usr/lib/jvm/java-8-openjdk" es un ejemplo, se debe colocar la ruta de instalacion.)

    For "PATH" (Environment Variable) type command as shown below, in "Terminal" using your installation path...

    export PATH=$PATH:/usr/lib/jvm/java-8-openjdk/bin

    (Note: "/usr/lib/jvm/java-8-openjdk" is symbolically used here just for demostration. You should use your path as per your installation.)

    Check for "open jdk" installation, just type command in "Terminal" as shown below

    javac -version

https://github.com/callicoder/spring-boot-mysql-rest-api-tutorial/blob/master/src/main/java/com/example/easynotes/model/Note.java

https://medium.com/@itsromiljain/dockerize-rest-spring-boot-application-with-hibernate-having-database-as-mysql-579abcc4edc4