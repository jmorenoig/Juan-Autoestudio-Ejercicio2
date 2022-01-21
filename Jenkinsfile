pipeline{
    agent any
        stages {
            stage ('install .Net') {
                steps {
                    sh "dotnet tool install --global dotnet-sonarscanner | true"
                    sh "dotnet tool install --global dotnet-reportgenerator-globaltool | true"
                    
                }
            }
        
            stage ('Restore') {
                steps {
                    dir("CapaPresentacion"){
                        sh "dotnet restore"
                    }
                }
            }
            stage ('Build') {
                steps {
                    dir("CapaPresentacion"){
                        sh "dotnet build"
                    }
                }
            }      
        }
}