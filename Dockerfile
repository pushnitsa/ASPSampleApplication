FROM mcr.microsoft.com/dotnet/aspnet:6.0

EXPOSE 80

WORKDIR /app

COPY src/ASPSampleApplication.Web/bin/Debug/net6.0/publish /app/.

COPY entrypoint.sh /app/.
RUN chmod +x ./entrypoint.sh
CMD /bin/bash ./entrypoint.sh

ENTRYPOINT ["dotnet", "ASPSampleApplication.Web.dll"]