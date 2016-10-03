FROM microsoft/dotnet:latest
ENV name SecuritiesService
COPY src/$name /root/$name
RUN cd /root/$name && dotnet restore && dotnet build && dotnet publish
RUN cp -rf /root/$name/bin/Debug/netcoreapp1.0/publish/* /root/
EXPOSE 16555/tcp
ENTRYPOINT dotnet /root/${name}.dll
