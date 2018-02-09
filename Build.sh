dotnet --info

dotnet build ContosoUniversity.CI.sln -c Release

pushd ContosoUniversity.IntegrationTests

dotnet xunit -configuration Release -nobuild --fx-version 2.0.5 || popd