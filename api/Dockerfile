FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dev
WORKDIR /app

COPY src . 

WORKDIR OrderManagement.Api

RUN dotnet restore

EXPOSE ${API_PORT}

CMD dotnet watch --project OrderManagement.Api.csproj run --urls http://0.0.0.0:${API_PORT}
