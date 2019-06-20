#!/bin/sh
sudo service zzukbot.web stop
sudo service zzukbot.server stop

cd /home/orfasanti/zzukbot4
sudo git pull origin master

cd /home/orfasanti/zzukbot4/ZzukBot.Web
sudo dotnet publish -c Release -o /var/aspnetcore/ZzukBot.Web

cd /home/orfasanti/zzukbot4/ZzukBot.Server
sudo dotnet publish -c Release -o /var/dotnetcore/ZzukBot.Server

sudo service zzukbot.web start
sudo service zzukbot.server start
