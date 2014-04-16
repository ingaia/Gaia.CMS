Gaia.CMS.Console HELP
=====================

**Índice:**

**[1 - Utilização do comando de deploy](https://github.com/I-Value/Gaia.CMS/edit/master/README.md#1---utiliza%C3%A7%C3%A3o-do-comando-de-deploy)**<br>
[1.1 - Configurando](https://github.com/I-Value/Gaia.CMS/edit/master/README.md#11---configurando)<br>
[1.2 - Executando](https://github.com/I-Value/Gaia.CMS/edit/master/README.md#12---executando)<br><br>
**[2 - Configuração do Template](https://github.com/I-Value/Gaia.CMS/edit/master/README.md#2---configura%C3%A7%C3%A3o-do-template)**<br>
[2.1 - Configurando os arquivos para serem processados pelo sistema](https://github.com/I-Value/Gaia.CMS/edit/master/README.md#21---configurando-os-arquivos-para-serem-processados-pelo-sistema)<br>
[2.2 - Utilizando o sistema de templates](https://github.com/I-Value/Gaia.CMS/edit/master/README.md#22---utilizando-o-sistema-de-templates)<br>
  *[Exemplo 1]()<br>

**[3 - Resumo de utilização](https://github.com/I-Value/Gaia.CMS/edit/master/README.md#3---resumo-de-utiliza%C3%A7%C3%A3o)**

=====================

##1 - Utilização do comando de deploy##

###1.1 - Configurando###

Os seguintes arquivos devem ser preenchidos:

- baseConfig.json:

Esse arquivo contém os parâmetros básicos para executar o Deploy do Template/Modelo escolhido. Este arquivo pode ser renomeado para atender as necessidades, bastando informar o nome na hora de executar o Deploy, por ex. caso deseje deixar pronto um config para o "Modelo01", podemos chamar o mesmo de "modelo01BaseConfig.json", assim o comando para executa-lo seria: 

```
IValue.CMS.Console.exe "caminho\modelo01BaseConfig.json"
```

Este arquivo deve ter o seguinte formato:

```JSON
{
"TemplateName" : "Modelo01",
"TemplateConfig" : "templateConfig",
"DefaultConfig" : "defaultConfig",
"ImagesConfig" : "imagesConfig",
"ModelFolder" : "modelo",
"DefaultTemplateFolder" : "default",
"ClientDirName" : "Cliente01",
"SourcePath" : "Y:\novopremium.com.br\web",
"TargetPath" : "Y:\"
}
```

**TemplateName:** É o nome do template (nome da pasta no diretório "SourcePath") a ser utilizado;<br>
**TemplateConfig:** Especifica o nome do arquivo de configuração para o Template (visa facilitar o processo de deploy permitindo deixar configurados arquivos para cada tipo de modelo);<br>
**DefaultConfig:** Especifica o nome do arquivo de configuração para o default (visa facilitar o processo de deploy permitindo deixar configurados arquivos para cada tipo de modelo);<br>
**ImagesConfig:** Especifica o nome do arquivo de configuração de imagens (visa facilitar o processo de deploy permitindo deixar configurados arquivos para cada tipo de modelo);<br>
**ClientDirName:** É o nome do diretório que será criado na pasta "TargetPath" para fazer o deploy do Template escolhido;<br>
**ModelFolder:** É o nome da pasta utilizada para fazer deploy do modelo dentro do template "default";<br>
**DefaultTemplateFolder:** É o nome da pasta do template "Default";<br>
**SourcePath:** É o diretório onde será encontrada a pasta “default” e a pasta do modelo especificado em “TemplateName”;<br>
**TargetPath:** É a pasta destino onde será feito o deploy do Template escolhido.<br>

- defaultConfig.json:

Esse arquivo contém as configurações a serem aplicadas durante o processo de Deploy. Este arquivo pode ter seu nome alterado, desde que seja devidamente apontado no campo "DefaultConfig" do arquivo "baseConfig.json".
OBS: O conteúdo deste arquivo não deve sobrepor o conteúdo do arquivo "templateConfig.json", caso isso ocorra, um dos dois será ignorado.

- templateConfig.json

Esse arquivo contém as configurações a serem aplicadas durante o processo de Deploy. Este arquivo pode ter seu nome alterado, desde que seja devidamente apontado no campo "TemplateConfig" do arquivo "baseConfig.json".
OBS: O conteúdo deste arquivo não deve sobrepor o conteúdo do arquivo "defaultConfig.json", caso isso ocorra, um dos dois será ignorado.

- imagesConfig.json

Este arquivo configura o processo de deploy do Logo e da Marca d'água. Este arquivo pode ter seu nome alterado, desde que seja devidamente apontado no campo "ImagesConfig" do arquivo "baseConfig.json".

Este arquivo deve ter o seguinte formato:
```JSON
{
"ImgFolder" : "img",
"LogoImage" : "C:\\Users\\jhonathan.almeida\\Projetos\\cms\\logo.png",
"LogoImages" : [
		{"OutputName": "img1.png", "Width" : "50", "Height" : "50"},
		{"OutputName": "img2.png", "Width" : "75", "Height" : "75"}
	],
"WaterMarkImage" : "C:\\Users\\jhonathan.almeida\\Projetos\\cms\\marca_dagua.png"
}
```

ImgFolder: Especifica o nome do diretório onde será feito o deploy das imagens;
LogoImage: Especifica qual o arquivo a ser utilizado como logo;
LogoImages: Especifica através dos parâmetros "OutputName", "Width" e "Height" quais são e o tamanho dos arquivos a serem gerados com base no arquivo de logo;
WaterMarkImage: Especifica qual o arquivo a ser utilizado como Marca D'Água.

###1.2 - Executando###

Para executar o processo de deploy, deve-se executar o seguinte comando: 
IValue.CMS.Console.exe "caminhoDoArquivo\modelo01BaseConfig.json".

Os arquivos de configuração devem estar na mesma pasta do arquivo "baseConfig.json".

##2 - Configuração do Template##

O sistema de CMS/Deploy de template utiliza o algoritmo de Templates Mustache na sua compilação .Net chamada Nustache.

Com este sistema utilizamos tags "{{nomeDaVariavel}}" para definir os templates a serem substituídos, com os valores especificados nos arquivos "defaultConfig.json" e "templateConfig.json".

###2.1 - Configurando os arquivos para serem processados pelo sistema###

Para que um arquivo seja marcado para ser processado pelo sistema, o mesmo deve conter a tag "<#CMS_TEMPLATE#>" declarada em algum local dentro do mesmo (de preferência nas primeiras linhas do arquivo), podendo a mesma estar dentro de um comentário, ex:

Arquivos HTML, XML, CONFIG, ASPX, ASCX, etc.: <!-- <#CMS_TEMPLATE#> -->
Arquivos CSS: /* <#CMS_TEMPLATE#> */

###2.2 - Utilizando o sistema de templates###

Utilizando os arquivos "defaultConfig.json" e "templateConfig.json", iremos criar variáveis para serem substituídas no momento do Deploy.

####Exemplo 1: arquivo CSS####

Considere o seguinte CSS:
body {
    background-color: #AFCC5E;
    width: 1004px;
    border: 1px solid black;
    font-size: 12px;
}

Neste exemplo iremos criar um template para um arquivo CSS, para substituir a cor background padrão do sistema, o tamanho da fonte, a largura e a cor da borda.
Obs.: Para o arquivo ser processado pelo sistema, lembre-se de adicionar a tag "/* <#CMS_TEMPLATE#> */" na primeira linha do arquivo.

Assumindo que este CSS faça parte do "Modelo01" do template, iremos criar as seguintes variáveis no arquivo "templateConfig.json":

{
"BodyBackgroundColor" : "#AFCC5E",
"BodyWidth" : "1004px",
"BodyFontSize" : "12px",
"BodyBorderColor" : "black"
}

Com base nestas variáveis, vamos atualizar o CSS para o seguinte:
body {
    background-color: {{BodyBackgroundColor}};
    width: {{BodyWidth}};
    border: 1px solid {{BodyBorderColor}};
    font-size: {{BodyFontSize}};
}

Ao executar o deploy, teremos o resultado desejado no arquivo CSS:
body {
    background-color: #AFCC5E;
    width: 1004px;
    border: 1px solid black;
    font-size: 12px;
}

####Exemplo 2: arquivo XML/Config####

Considere o seguinte XML (parcial):
<appSettings>
	<add key="cod_imb" value="2357" />
	.
	.
	.
</appSettings>

Neste exemplo iremos criar um template para um arquivo XML, para informar o código da imobiliária desejada. 
Obs: Para o arquivo ser processado pelo sistema, lembre-se de adicionar a tag "<!-- <#CMS_TEMPLATE#> -->" na primeira linha do arquivo.

Assumindo que este XML/Config faça parte do "default" do template, iremos criar a seguinte variável no arquivo "defaultConfig.json":

{
"CodImb" : "2375"
}

Com base nesta variável, vamos atualizar o XML/Config para o seguinte:
<appSettings>
	<add key="cod_imb" value="{{CodImb}}" />
	.
	.
	.
</appSettings>

Ao executar o deploy, teremos o resultado desejado no arquivo XML/Config:
<appSettings>
	<add key="cod_imb" value="2357" />
	.
	.
	.
</appSettings>

####Exemplo 3: arquivo HTML/ASPX/ASPX/etc####

Considere que na pagina "index.aspx" devemos gerar a seguinte tabela com base numa lista de contatos:
<table border="0">
	<tr>
		<td>SJC</td>
		<td>1111-9999</td>
		<td>Inacio</td>
	</tr>
	<tr>
		<td>Campinas</td>
		<td>2222-8888</td>
		<td>Mauro</td>
	</tr>
	<tr>
		<td>São Paulo</td>
		<td>3333-7777</td>
		<td>Fidélio</td>
	</tr>
</table>
Sendo as informações: Unidade, Telefone e Nome do Contato.

Neste exemplo iremos criar um template para um arquivo ASPX, para montar dinamicamente uma tabela de contatos da imobiliária. 
Obs: Para o arquivo ser processado pelo sistema, lembre-se de adicionar a tag "<!-- <#CMS_TEMPLATE#> -->" na primeira linha do arquivo.

Assumindo que este ASPX faça parte do "Modelo01" do template, iremos criar a seguinte estrutura no arquivo "templateConfig.json":

"ListaContatos" :[
	{"Unidade" : "SJC", "Telefone" : "1111-9999", "Nome" : "Inacio"},
	{"Unidade" : "Campinas", "Telefone" : "2222-8888", "Nome" : "Mauro"},
	{"Unidade" : "São Paulo", "Telefone" : "3333-7777", "Nome" : "Fidélio"}
]

Com base nesta estrutura, vamos atualizar o ASPX para o seguinte:
<table border="0">
	{{#ListaContatos}}
	<tr>
		<td>{{Unidade}}</td>
		<td>{{Telefone}}</td>
		<td>{{Nome}}</td>
	</tr>
	{{/ListaContatos}}
</table>

Ao executar o deploy, teremos o resultado desejado no arquivo "index.aspx":
<table border="0">
	<tr>
		<td>SJC</td>
		<td>1111-9999</td>
		<td>Inacio</td>
	</tr>
	<tr>
		<td>Campinas</td>
		<td>2222-8888</td>
		<td>Mauro</td>
	</tr>
	<tr>
		<td>São Paulo</td>
		<td>3333-7777</td>
		<td>Fidélio</td>
	</tr>
</table>

##3 - Resumo de utilização##

1 - Adicionar a tag de marcação de template ("<#CMS_TEMPLATE#>") no arquivo desejado (deve estar dentro de um comentário);
2 - Identifique as variáveis/valores que deseja transformar e template, e substitua no formato "{{nomeDaVariavel}}";
3 - No arquivo correspondente ("defaultConfig.json" ou "templateConfig.json") crie as variáveis identificadas anteriormente em notação JSON;
4 - Repita os passos 1 a 3 para todos os arquivos a serem transformados em template;
5 - Configure o arquivo "baseConfig.json" com as informações desejadas;
6 - Configure o arquivo "imagensConfig.json" com os arquivos Logo e Marca D'Água, e a lista de arquivos/tamanhos a serem gerados;
7 - execute a aplicação "IValue.CMS.Console.exe" informando o arquivo baseConfig.json.

Para utilização de lista de valores, a lista deve ser declarada no formato:
"NomeDaLista" :[
	{"Parametro01" : "valor01", "Parametro02" : "valor02", ... , "ParametroN" : "valorN"},
	{"Parametro01" : "valor01", "Parametro02" : "valor02", ... , "ParametroN" : "valorN"},
	.
	.
	.
	{"Parametro01" : "valor01", "Parametro02" : "valor02", ... , "ParametroN" : "valorN"}
]

Para consumir esta lista deve-se utilizar o seguinte template:
{{#NomeDaLista}}
	Processar os itens da lista conforme necessário, utilizando o seguinte formato {{NomeDoParametro}}
{{/NomeDaLista}}
