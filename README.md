<h1 align="center">
  <img src="https://raw.githubusercontent.com/nosignals/neko/main/img/neko.png" alt="neko" width="500">
</h1>

<p align="center">
	<a target="_blank" href="#">
   <img src="https://img.shields.io/github/v/release/nosignals/neko?label=Neko%20%7C%20Build&color=yellow">
 </a>
	<a target="_blank" href="#">
   <img src="https://img.shields.io/github/downloads/nosignals/neko/total?label=Total%20Downloader&labelColor=blue">
 </a>
</p>

<p align="center">
  XRAY/V2ray, Shadowsocks, ShadowsocksR, etc.</br>
  Mihomo based Proxy
</p>

Depedencies
---
  - ` php-cli `

How to Install
---
1. Download tar.gz files on Release, [here](https://github.com/nosignals/neko/releases)
2. Extract tar.gz Files & Go to Neko Folder ` tar xzvf neko_xxx.tar.gz `
3. Open Terminal at Neko Files
4. Install Depedencies ` sudo apt -y install php php-cli `
5. Change Permission ` chmod +x install.sh `
6. Run at Terminal ` ./install.sh ` using Normal Connectivity (Internet).
7. Wait until Done
8. Done

Usage
---
* Neko Directory
  * Base Directory ` /home/hostname/.neko `
  * Clash Config Directory ` /home/hostname/.neko/config `
    * Proxy Config Directory ` /home/hostname/.neko/proxy_provider `
    * Rules Config Directory ` /home/hostname/.neko/rule_provider `
  * Core & Script Directory ` /home/hostname/.neko/core `
  * Web Directory ` /home/hostname/.neko/www `
    
 * Script Usage
   * Running PHP & Clash Core ` sudo ./neko -ps `
   * Running PHP Server ` sudo ./neko -p `
   * Running Clash Core ` sudo ./neko -s `
   * Kill Clash Core ` sudo ./neko -k `
   * Clean PHP Server & Clash Core ` sudo ./neko -c `
 ```
      -s : Start Mihomo Proxy
      -p : Start PHP Server
      -k : Kill/Stop Mihomo Proxy
      -r : Restarting Mihomo Proxy
      -c : cleanup (kill mihomo + php server)
      -v : Version
      -h : help (this text)
 ```
  * Running at Startup (not tested)
    1. Copy ` neko ` at extracted zip file to ` /etc/init.d/ `
    2. ` sudo cp ./neko /etc/init.d/neko `
    3. change permission ` sudo chmod 775 /etc/init.d/neko `
    
About
---
nosignal is gone

Credit
---
- nosignals

Screenshoot
---
<details><summary>Home</summary>
 <p>
  <img src="https://raw.githubusercontent.com/nosignals/neko/main/img/home.png" alt="home">
 </p>
</details>

<details><summary>Dasboard</summary>
 <p>
  <img src="https://raw.githubusercontent.com/nosignals/neko/main/img/dashboard.png" alt="dash">
 </p>
</details>

<details><summary>Config - Home</summary>
  <img src="https://raw.githubusercontent.com/nosignals/neko/main/img/config.png" alt="cfg">
</details>
<details><summary>Config - Proxy</summary>
  <img src="https://raw.githubusercontent.com/nosignals/neko/main/img/config-proxy.png" alt="proxy">
</details>
<details><summary>Config - Rules</summary>
  <img src="https://raw.githubusercontent.com/nosignals/neko/main/img/config-rules.png" alt="rules">
</details>
<details><summary>Config - Converter</summary>
  <img src="https://raw.githubusercontent.com/nosignals/neko/main/img/config-converter.png" alt="conv">
</details>

<details><summary>Settings</summary>
  <img src="https://raw.githubusercontent.com/nosignals/neko/main/img/setting.png" alt="setting">
</details>
