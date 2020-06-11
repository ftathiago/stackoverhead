dotnet tool install -g dotnet-reportgenerator-globaltool
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=./coverage.opencover.xml
reportgenerator -reports:**/coverage.opencover.xml -targetdir:coverage_report
coverage_report\index.html
