FROM mcr.microsoft.com/dotnet/sdk:8.0 as restore
WORKDIR /src
COPY ./*.sln .
COPY ./Unity.Publisher.Tool.Bot.Api/*.csproj ./Unity.Publisher.Tool.Bot.Api/
RUN dotnet restore