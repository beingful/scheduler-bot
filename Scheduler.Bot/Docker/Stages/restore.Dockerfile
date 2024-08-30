FROM mcr.microsoft.com/dotnet/sdk:8.0 as restore
WORKDIR /src
COPY ./*.sln .
COPY ./Slack.Bot.Api/*.csproj ./Slack.Bot.Api/
RUN dotnet restore