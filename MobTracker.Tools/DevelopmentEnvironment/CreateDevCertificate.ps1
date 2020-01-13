# install mkcert and run 'mkcert -install' before executing this script

$Name = "localhost"
$Mkcertdir = "$env:LOCALAPPDATA\mkcert"

# create directory for certificate
New-Item -Path $Mkcertdir -Name $Name -ItemType "directory"
Set-Location -Path $Mkcertdir\$Name

# make localhost certificate and output p12 file
mkcert -pkcs12 localhost 127.0.0.1 ::1 10.0.2.2

# import certificate to cert:\CurrentUser\Root
$Pwd = ConvertTo-SecureString "changeit" -AsPlainText -Force
$Cert = Import-PfxCertificate -FilePath $Mkcertdir\$Name\localhost+3.p12 -CertStoreLocation cert:\LocalMachine\My -Password $Pwd

# configure IIS Express ssl binding
$Thumb = $Cert.GetCertHashString()
netsh http delete sslcert ipport=0.0.0.0:44375
netsh http add sslcert ipport=0.0.0.0:44375 certhash=$Thumb appid=`{214124cd-d05b-4309-9af9-9caa44b2b74a`}

# add certificate to localmachine root store
$StoreScope = 'LocalMachine'
$StoreName = 'Root'

$Store = New-Object -TypeName System.Security.Cryptography.X509Certificates.X509Store -ArgumentList $StoreName, $StoreScope
$Store.Open([System.Security.Cryptography.X509Certificates.OpenFlags]::ReadWrite)
$Store.Add($Cert)

$Store.Close()