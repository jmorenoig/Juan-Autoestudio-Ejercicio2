pipeline{
    agent {label 'AgentLinux-21841f69'}
        stages {
            stage ('Liquibase') {
                steps {
                    sh "liquibase -v"
                    
                }
            }
            stage ('install .Net') {
                steps {
                    sh "dotnet tool install --global dotnet-sonarscanner | true"
                    sh "dotnet tool install --global dotnet-reportgenerator-globaltool | true"
                    
                }
            }
        
            stage ('Restore') {
                steps {
                    dir("CapaDatos"){
                        sh "dotnet restore"
                    }
                }
            }
            stage ('Build') {
                steps {
                    dir("CapaDatos"){
                        sh "dotnet build"
                    }
                }
            } 
            stage ('Test') {
                steps {
                    dir("TestProject"){
                        sh "dotnet test"
                    }
                }
            }      
        }
}