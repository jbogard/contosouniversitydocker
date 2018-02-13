# escape=`

# Use server core as an installer container to extract PowerShell,
# As this is a multi-stage build, this stage will eventually be thrown away
FROM microsoft/windowsservercore:1709  AS installer-env

# Arguments for installing powershell, must be defined in the container they are used
ARG PS_VERSION=6.0.1

ENV PS_DOWNLOAD_URL https://github.com/PowerShell/PowerShell/releases/download/v$PS_VERSION/PowerShell-$PS_VERSION-win-x64.zip

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

RUN if (!($env:PS_VERSION -match '^\d+\.\d+\.\d+(-\w+(\.\d+)?)?$' )) {throw ('PS_Version ({0}) must match the regex "^\d+\.\d+\.\d+(-\w+(\.\d+)?)?$"' -f $env:PS_VERSION)}
RUN Invoke-WebRequest $Env:PS_DOWNLOAD_URL -OutFile powershell.zip

RUN Expand-Archive powershell.zip -DestinationPath \PowerShell

# Install PowerShell into NanoServer
FROM microsoft/aspnetcore-build:2.0.5-2.1.4-nanoserver-1709

# Copy Powershell Core from the installer containter
ENV ProgramFiles C:\Program Files
COPY --from=installer-env ["\\PowerShell\\", "$ProgramFiles\\PowerShell"]

# Persist %PSCORE% ENV variable for user convenience
ENV PSCORE="$ProgramFiles\PowerShell\pwsh.exe"

# Set the path
RUN setx PATH "%PATH%;%ProgramFiles%\PowerShell"

CMD ["pwsh.exe"]