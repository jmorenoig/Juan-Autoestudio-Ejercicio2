# Introducción  
La idea con este proyecto es crear un codigo que sirva para practicar Netcore, SQL Server, a su vez montarlo en Azure DevOps como práctica y integrarlo con SonarQube.
Este programa es un gestor de productos que permite, ingresar nuevos productos, editarlos y eliminerlos.  

# Inicio 
para poner en funcionamiento el código, se deben seguir los siguientes pasos:  

**Dependencias:** 

para que el Software corra correctamente se debe instalar primero las siquientes dependencias:
- [.NET Core](https://dotnet.microsoft.com/download) version 5.0, como lenguaje de programacion.
- [Visual Studio 2019](https://visualstudio.microsoft.com/es/vs/) como editor de código. 
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) como gestor de base de datos.



# Creación de Pipeline

- primero ingresamos a los pipelines en Azure DevOps, dando clic sobre la pestaña Pipelines 

![Image-30](https://i.postimg.cc/CxmFBcrd/Captura-15.png)

- luego sobre Nuevo Pipeline

![Image-31](https://i.postimg.cc/R0Y4GNBB/Captura-16.png)

- se nos abrira una nueva pestaña y damos clic sobre **Use el editor Clasico** y en la pestaña siguiente dejamos marcado tal cual y presionamos continuar 

![Image-32](https://i.postimg.cc/Xvqnw9tL/Captura-17.png)

![Image-32.1](https://i.postimg.cc/SR64ydzS/Captura-18.png)

- aplicamoe en la pestaña de ASP.NET Core, por el tipo de lenguaje que se uso en esta aplicacion

![Image-33](https://i.postimg.cc/XqS3Smq1/Captura-19.png)

- liego se nos creara la siquiente pestaña con los agentes de trabajo, debemos asegurarnos de tener un Restore, un Build, un Test, un publish y un publish artifact, en este caso no se requiere de un publish, ya que no es una aplicacion web, asi que marcara con error, por lo tanto se procede a desabilitar la selección

![Image-33](https://i.postimg.cc/BZH4cS7r/Captura-20.png)

![Image-33.1](https://i.postimg.cc/BZH4cS7r/Captura-20.png)
![Image-33.2](https://i.postimg.cc/FHrYBWYV/Captura-21.png)
![Image-33.3](https://i.postimg.cc/BQc8N9nv/Captura-21-1.png)

- cuando todo este listo procedemos a dar sobre guardar y correr

![Image-34.1](https://i.postimg.cc/26BmtrkB/Captura-22.png)

![Image-34.2](https://i.postimg.cc/JhJty9vm/Captura-23.png)

![Image-34.3](https://i.postimg.cc/zBrvTxYh/Captura-24.png)

# Integración proyecto .Net con SonarQube 
Inicialmente se configura el SonarQube con Azure DevOps, despues de estar configurado ingresamos al Pipeline 

![Image-35](https://i.postimg.cc/KvnxsTDv/Captura1.png)

Seleccionamos el proyecto 

![Image-36](https://i.postimg.cc/3xYYMwtj/Captura2.png)

Una vez seleccionado el repositorio, podemos elegir entre diferentes plantillas ya prehechas para diferentes finalidades. buscamos Prepare analysis on SonarQube

![Image-37](https://i.postimg.cc/hGGnj5Wf/Captura3.png)

Seleccionamos el servidor de Sonar ya configurado, despues se selecciona la forma de ejecutar el análisis, se asignan las propiedades adicionales, encargadas de hacer la conección con el Coverage y el Trx, quienes muestran el porcentage de coverage y la cantidad de unit test realizadas.

![Image-38](https://i.postimg.cc/qRPH1Bt3/Captura4.png)

![Image-38.1](https://i.postimg.cc/Bb29kWDw/Captura5.png)

```javascript
sonar.language=cs
sonar.verbose=true
sonar.sourceEncoding=UTF-8
sonar.cs.opencover.reportsPaths="TestResults/coverage.opencover.xml"
sonar.cs.vstest.reportsPaths=**/*.trx
```

estando configurado el Prepare analysis on SonarQube, en caso de ser nesesario se instalan los paquetes del reportgenerator y sonarscanner, teniendo en cuenta de la forma de llamarlo en el pipeline si es en windows con powerShell o en linux con Bash, en este caso como los paquetes estan instalados en el equipo local, se dejan anulados 

![Image-39](https://i.postimg.cc/1RFQfFTc/Captura6.png)

```javascript
dotnet tool install --global dotnet-sonarscanner  --version 4.8.0
dotnet tool install --global dotnet-reportgenerator-globaltool --version 5.0.0
```

Lo siguiente que necesitamos es añadir el paquete NuGet coverlet.msbuild a los proyectos de pruebas(dentro del proyecto en el back end). 

![Image-40](https://i.postimg.cc/43FghQsw-/Captura7.png)

Una vez hecho eso, vamos a tener que editar ligeramente la tarea de ejecución de las pruebas para que haciendo uso de ese paquete NuGet, genere el informe. Para eso, en la tarea de ‘test’ añadiremos

```javascript
--configuration $(BuildConfiguration) --no-build /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=$(Build.SourcesDirectory)/TestResults/  
```
A los argumentos. Esto lo que va a hacer es decirle al paquete NuGet que recolecte la cobertura y que la guarde con formato opencover.

![Image-41](https://i.postimg.cc/vBgd7xmy/Captura8.png)

Con esto listo, debemos convertir los formatos generados en open cover, a un tipo de reporte que pueda leer el reportgenerator para asi mostrarlo en SonarQube, se hace con el siquiente comando.

```javascript
reportgenerator -reports:TestResults/coverage.opencover.xml -targetdir:TestResults/Reports/ -reportTypes:"HTMLInline;Cobertura"
```
en este caso tambien se debe tener en cuenta de la forma de llamarlo en el pipeline si es en windows con powerShell o en linux con Bash

![Image-42](https://i.postimg.cc/K8fFmyW6/Captura9.png)

para finalizar se debe correr el análisis del código y Publicar resultado en SonarQube, para tener en cuenta, para hacer uso de sonar en una maquina local, se debe tener instalado el JDK de java versión 11.0.12

![Image-43](https://i.postimg.cc/zX11WBYm/Captura10.png)


# Dependency Check

En el siquiente enlace, podan consultar como integrar Azure DevOps con [Dependency Check](https://marketplace.visualstudio.com/items?itemName=dependency-check.dependencycheck)

# Despliegue en maquina virtual Azure

para realizar el despliegue en la maquina virtual de Azure lo primero que se debe hacer despues de tener activa la maquina virtual de azure es ponerle el nombre DNS a la maquina y fijar la IP, en el siguiente video se muestra como se realiza este proceso 

[Crear nombre DNS a maquinas virtuales en Microsoft Azure](https://www.youtube.com/watch?v=YX2yQYBLovM&t=78s)

Cuendo este configurado el DNS en la maquina virtual se debe habilitar el puerto 5986 quien es el encargado de darle permiso a la maquina local para acceder a la maquina del cliente por medio de un certificado, esto se puede hacer realizando los pasos que se veran en el siquiente video

[Habilitar conexión remota por PowerShell (PSSession) a VMs Windows Server 2019 en Azure](https://www.youtube.com/watch?v=1FZnxqhfLJs)

Tener precente los siquientes comandos usados en el video:

Habilitar puerto winrm
```javascript
New-NetFirewallRule -DisplayName "Allow PS Remoting" -Direction inbound -LocalPort 5986 -Protocol TCP -Action allow
```

generar Thumbprint:
```javascript
New-SelfSignedCertificate -DnsName NmbreDNS -CertStoreLocation Cert:\LocalMachine\My
```

crear certificado https: winrm create winrm/config/Listener?Address=*+Transport=HTTPS @{Hostname="Nmbre DNS"; CertificateThumbprint="Thumbprint generado anteriormente"}

Ingresar en la VM Azure desde powershell: 
```javascript
Enter-PSSession -ComputerName NmbreDNS -Credential Usuario -UseSSL
```

El siguiente comando es usado para copiar archivos directamente desde PS a la VM Azure:

```javascript
$pw = convertto-securestring -AsPlainText -Force -String "Contraseña"

$cred = new-object -typename System.Management.Automation.PSCredential -argumentlist "Usuario",$pw

$sessionOptions = New-PSSessionOption -SkipCACheck

$session = New-PSSession -ComputerName ads01.southcentralus.cloudapp.azure.com -Credential $cred -UseSSL -SessionOption $sessionOptions

Copy-Item -Path C:\Users\User\OneDrive\Documentos\Programas\Despliegue\Hola.txt -Destination C:\Despliegue -ToSession $session
```

Para realizar este proceso en un release, se debe crear el Tasks Dev y en este se debe colocar un archivo de ps para dar el permiso de ingresar a la VM Azure con el siquiente comando

```javascript
$pw = convertto-securestring -AsPlainText -Force -String "Contraseña"

$cred = new-object -typename System.Management.Automation.PSCredential -argumentlist "Usuario",$pw

$sessionOptions = New-PSSessionOption -SkipCACheck

$session = New-PSSession -ComputerName ads01.southcentralus.cloudapp.azure.com -Credential $cred -UseSSL -SessionOption $sessionOptions
```

cuando tenga dicho permiso, creamos un noevo Agent job llamado Windows Machine File Copy (WinRM) con la siquiente informacion

![Image](https://i.postimg.cc/HnK2KZGK/Captura.jpg)



