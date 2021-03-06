dotnet tool install dotnet-reportgenerator-globaltool --tool-path tools
cd BlabberApp.ServicesTest
dotnet add package coverlet.msbuild
dotnet test /p:CollectCoverage=true /p:CoverLetOutput=TestResults /p:CoverletOutputFormat=lcov
.."/tools/reportgenerator" -reports:./TestResults.info -targetdir:./TestResults/
cd ../BlabberApp.DomainTest
dotnet add package coverlet.msbuild
dotnet test /p:CollectCoverage=true /p:CoverLetOutput=TestResults /p:CoverletOutputFormat=lcov
.."/tools/reportgenerator" -reports:./TestResults.info -targetdir:./TestResults/
cd ../BlabberApp.DataStoreTest
dotnet add package coverlet.msbuild
dotnet test /p:CollectCoverage=true /p:CoverLetOutput=TestResults /p:CoverletOutputFormat=lcov
.."/tools/reportgenerator" -reports:./TestResults.info -targetdir:./TestResults/
cd ../BlabberApp.ClientTest
dotnet add package coverlet.msbuild
dotnet test /p:CollectCoverage=true /p:CoverLetOutput=TestResults /p:CoverletOutputFormat=lcov
.."/tools/reportgenerator" -reports:./TestResults.info -targetdir:./TestResults/
cd ../BlabberApp.Client
dotnet run
