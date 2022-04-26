#Instruktioner by Johan
1. Försökt skapa en Azure för pipline först med en Docker som skulle pusha en image till Azure Container Registry men fick ett FETT felsvar där att inte prenumerationen gällde för detta så droppade det. Gick då in på en vanlig Docker image som jag tyvärr verkar lyckats skapa utan att få med telefonnr och får inte till kopplingen till SQL. 
Egen notering för den skapade bilden:
Image naming to docker.io/johan/myweb 
Körs med  docker run -d -p 3000:80 --name myWeb johan/myweb 

Ligger flera saker på resource group JohanRGAssTwo som vi bör ta bort när du kollat på detta - kostar pengar - gått på det billigaste på allt men vissa tillät bara produktion...

2. Alternativ två är att deploya med docker-compose som inte heller gått hela vägen men följande instruktion:

För att hämta hem programmet som ligger i Azure DevOps skapa en tom mapp på din hårddisk öppna mappen i förslagsvis VS-Code, starta ett terminal fönster och se till att du står i den aktuella mappen. 

Alternativt kan du även i  explorer fönstret när du skapat mappen bara skriva CMD direkt i sökvägsfältet för att starta en terminal med rätt sökväg.

För att clona ner programmet använd följande kommando:
Git clone https://AcademyAzure2022@dev.azure.com/AcademyAzure2022/Assestment%202%20-%20Johan/_git/Assestment%202%20-%20Johan

Med förslagsvis VS code starta ett terminalfönster navigera till _dotnet mappen och kör följande

docker-compose up -d 

Därefter för att bygga
docker-compose build

För att sedan växla och stänga 
docker-compose down

Och väldigt lätt och snabbt att starta igen med
docker-compose up

3. Det enda som jag får funka riktigt är dotnet build och sedan dotnet run.


# Instructions 

In the base folder of the project, run the following commands:  

>``dotnet build``   

>``dotnet run``   

This will start the app running on http://localhost:8080   
This can be changed in Properties/launchSettings.  

Access the app at 
 

To stop the app type Ctrl+c in your shell.  

The application now uses an environment variable to read the connection string.  
This way you can easily change the SQL server you want to use.  
If you want to set the environment variable in VS Code, open a terminal and run the command:  

>``$Env:ConnectionString="Server=localhost,1433;Database=Pluto;User Id=sa;Password=Password123;"``  

Note that this must be done for every terminal you use to start your application in.   

## Docker instructions

**NOTE:** You should change the ENV for the connection string in the Dockerfile to match your environment.  

To build the Docker image, make sure your in the project folder in a shell.   
Run the command:

>``docker build . -f .\Dockerfile.backend -t pattzor/myweb``

This will build your image, naming it pattzor/myweb.  
You could/should rename it to your own, like 'myname/myimage'.   

To run the image in a contianer, run this in a shell:

>``docker run -d -p 3000:80 --name myWeb pattzor/myweb``   

This will run the image pattzor/myweb, mapping the container port 80, to your laptop port 3000.   
You should now be able to reach the application: http://localhost:3000   

## Docker compose instructions  

To run the docker-compose file:  

>``docker-compose up -d``  

Make sure you run this command in the same folder as the docker-compose.yml file.  

To stop the containers:  

>``docker-compose down``  

