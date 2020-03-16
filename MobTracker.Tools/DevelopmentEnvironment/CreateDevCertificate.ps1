#!/usr/bin/env pwsh

##########################################################################
# install mkcert and run 'mkcert -install' before executing this script
# must be executed from root folder of the project

$name = "localhost"
$mkcertdir = "$env:LOCALAPPDATA\mkcert"

# create directory for certificate
New-Item -Path $mkcertdir -Name $name -ItemType "directory"
Set-Location -Path $mkcertdir\$name

# make localhost certificate and output p12 file
mkcert -pkcs12 localhost 127.0.0.1 ::1

# import certificate to cert:\LocalMachine\My
$pwd = ConvertTo-SecureString "changeit" -AsPlainText -Force
$cert = Import-PfxCertificate -FilePath $mkcertdir\$name\localhost+3.p12 -CertStoreLocation cert:\LocalMachine\My -Password $pwd

# configure IIS Express ssl binding
$thumb = $cert.GetCertHashString()
netsh http delete sslcert ipport=0.0.0.0:44375
netsh http add sslcert ipport=0.0.0.0:44375 certhash=$thumb appid=`{214124cd-d05b-4309-9af9-9caa44b2b74a`}

# add certificate to localmachine root store
$StoreScope = 'LocalMachine'
$StoreName = 'Root'

$Store = New-Object -TypeName System.Security.Cryptography.X509Certificates.X509Store -ArgumentList $StoreName, $StoreScope
$Store.Open([System.Security.Cryptography.X509Certificates.OpenFlags]::ReadWrite)
$Store.Add($cert)

$Store.Close()


##########################################################################
#configure certificate for react app dev server
$keyPath = "./localhost-key.pem"
$certPath = "./localhost.pem"
$password = "changeit"
#$outPath = "$PSScriptRoot/mobtracker-admin-ui/node_modules/webpack-dev-server/ssl/server.pem"
$outPath = "C:\Users\Adam\source\repos\MobTracker\mobtracker-admin-ui\node_modules\webpack-dev-server\ssl/server.pem"

openssl pkcs12 -in $mkcertdir\$name\localhost+3.p12 -nocerts -out $keyPath -nodes -passin pass:$password
openssl pkcs12 -in $mkcertdir\$name\localhost+3.p12 -nokeys -out $certPath -nodes -passin pass:$password

$key = Get-Content .\localhost-key.pem
$cert = Get-Content .\localhost.pem
$key + $cert | Out-File $outPath -Encoding ASCII