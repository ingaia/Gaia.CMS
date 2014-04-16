using Gaia.Common;
using Gaia.Common.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Gaia.CMS.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine();

            // -- Help session ----------------------------------------------------------------------------------------
            if (args.Count() == 1 && (args[0].Compare("/h") || args[0].Compare("/help") || args[0].Compare("help")))
            {
                System.Console.WriteLine("Command usage: Gaia.CMS.Console.exe caminho\\baseConfig.json");

                return;
            }

            // -- If no config file was specified, ask for one --------------------------------------------------------
            var configFile = "";
            if (!args.Any() || args[0].Trim().IsNullOrEmpty())
            {
                System.Console.WriteLine("Arquivo de configuração não informado. Por favor informe o caminho do arquivo de configuração" +
                    " (ex: \"caminho\\baseConfig.json\"):");
                configFile = System.Console.ReadLine();

                System.Console.WriteLine();
            }
            else
                configFile = args[0];

            // -- Process config or exit if file was not found  or not specified --------------------------------------
            if (!configFile.IsNullOrEmpty())
            {
                if (System.IO.File.Exists(configFile))
                {
                    try
                    {
                        var basePath = Path.GetDirectoryName(configFile);

                        // -- check if baase parameters were informed -------------------------------------------------
                        var baseConfig = JsonHelper.Deserialize<BaseConfig>(File.ReadAllText(configFile));

                        if (baseConfig.SourcePath.IsNullOrEmpty() || !Directory.Exists(baseConfig.SourcePath))
                            System.Console.WriteLine("SourcePath não existe ou não informado (valor lido: \"" + baseConfig.SourcePath + "\"), abortando!");
                        else
                        {
                            if (baseConfig.TargetPath.IsNullOrEmpty() || !Directory.Exists(baseConfig.TargetPath))
                                System.Console.WriteLine("TargetPath não existe ou não informado (valor lido: \"" + baseConfig.TargetPath + "\"), abortando!");
                            else
                            {
                                var targetPath = Path.Combine(baseConfig.TargetPath, baseConfig.ClientDirName);

                                // -- loads and checks wether the image file is correctly configurated ----------------
                                var imgFile = Path.Combine(basePath, baseConfig.ImagesConfig.EndsWith(".json")
                                       ? baseConfig.ImagesConfig : baseConfig.ImagesConfig + ".json");

                                if (!File.Exists(imgFile))
                                    System.Console.WriteLine("imageConfig.json não existe ou não informado (valor lido: \"" + imgFile + "\"), abortando!");
                                else
                                {
                                    var imagesConfig = JsonHelper.Deserialize<ImagesConfig>(File.ReadAllText(imgFile));

                                    if (imagesConfig.LogoImage.IsNullOrEmpty() || !File.Exists(imagesConfig.LogoImage))
                                        System.Console.WriteLine("LogoImage não existe ou não informado (valor lido: \"" + imagesConfig.LogoImage + "\"), abortando!");
                                    else
                                    {
                                        if (imagesConfig.WaterMarkImage.IsNullOrEmpty() || !File.Exists(imagesConfig.WaterMarkImage))
                                            System.Console.WriteLine("WaterMarkImage não existe ou não informado (valor lido: \"" + imagesConfig.WaterMarkImage + "\"), abortando!");
                                        else
                                        {
                                            // -- checks wether the default config file is specified and exists -------
                                            var defaultConfigFile = Path.Combine(basePath, baseConfig.DefaultConfig.EndsWith(".json")
                                                    ? baseConfig.DefaultConfig : baseConfig.DefaultConfig + ".json");

                                            if (baseConfig.DefaultConfig.IsNullOrEmpty() || !File.Exists(defaultConfigFile))
                                                System.Console.WriteLine("DefaultConfig não existe ou não informado (valor lido: \"" + defaultConfigFile + "\"), abortando!");
                                            else
                                            {
                                                // -- checks wether the template config file is specified and exists --
                                                var templateConfigFile = Path.Combine(basePath, baseConfig.TemplateConfig.EndsWith(".json")
                                                            ? baseConfig.TemplateConfig : baseConfig.TemplateConfig + ".json");

                                                if (baseConfig.TemplateConfig.IsNullOrEmpty() || !File.Exists(templateConfigFile))
                                                    System.Console.WriteLine("TemplateConfig não existe ou não informado (valor lido: \"" + templateConfigFile + "\"), abortando!");
                                                else
                                                {
                                                    var overwrite = false;
                                                    var isEmpty = true;

                                                    // -- Check if destination is empty otherwise ask permission to overwrite -------------
                                                    if (Directory.Exists(targetPath) && Directory.EnumerateFileSystemEntries(targetPath).Any())
                                                    {
                                                        System.Console.WriteLine("Existe conteúdo no diretório de destino, deseja sobreescrever? (S/N)");
                                                        var key = System.Console.ReadKey();

                                                        System.Console.WriteLine();
                                                        System.Console.WriteLine();

                                                        if (string.Compare(key.Key.ToString(), "S", true) == 0)
                                                        {
                                                            overwrite = true;

                                                            System.Console.WriteLine("Usuário deseja sobreescrever conteúdo do destino! Continuando...");
                                                            System.Console.WriteLine();
                                                        }

                                                        isEmpty = false;
                                                    }

                                                    // -- if empty or can overwrite reads and aggregates the configs ----------------------
                                                    if (isEmpty || overwrite)
                                                    {
                                                        var defaultConfig = JsonHelper.Deserialize<Dictionary<string, object>>(File.ReadAllText(defaultConfigFile));
                                                        var modelConfig = JsonHelper.Deserialize<Dictionary<string, object>>(File.ReadAllText(templateConfigFile));

                                                        var fullConfig = new Dictionary<string, object>();
                                                        fullConfig = fullConfig.Merge(defaultConfig).Merge(modelConfig);

                                                        System.Console.WriteLine("Iniciando deploy do template \"" + baseConfig.TemplateName + "\"..." + Utils.CRLF);

                                                        // -- Executes the deployment -----------------------------------------------------
                                                        var done = false;

                                                        var td = new Thread(() =>
                                                        {
                                                            Deploy.Execute(baseConfig, imagesConfig, fullConfig);
#if DEBUG
                                                            Thread.Sleep(5000);
#endif
                                                            done = true;
                                                        }) { IsBackground = true };
                                                        td.Start();

                                                        while (!done)
                                                        {
                                                            UpdateCursor();
                                                            Thread.Sleep(500);
                                                        }

                                                        System.Console.WriteLine("\rDeploy do template \"" + baseConfig.TemplateName + "\" concluído com sucesso.");
                                                    }
                                                    else
                                                        System.Console.WriteLine("Deploy cancelado a pedido do usuário!");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        var sb = new StringBuilder();

                        sb.AppendLine("\rOcorreu um erro ao processar o template:" + Utils.CRLF);

                        sb.Append(ex.ToString());

                        System.Console.WriteLine(sb.ToString());
                    }
                }
                else
                    System.Console.WriteLine("Arquivo \"" + configFile + "\" não encontrado!");
            }
            else
                System.Console.WriteLine("Arquivo de configuração não informado, abortando!");

            System.Console.WriteLine(Utils.CRLF + "Pressione uma tecla para terminar.");

            // -- this while will avoid that a key stroke made before this points skips the last ReadKey --------------
            while (System.Console.KeyAvailable)
            {
                System.Console.ReadKey(false);
            }
            System.Console.ReadKey();
        }

        public static string _currentCursor = "";
        public static void UpdateCursor()
        {
            switch (_currentCursor)
            {
                case "-":
                    _currentCursor = "\\";
                    break;
                case "\\":
                    _currentCursor = "|";
                    break;
                case "|":
                    _currentCursor = "/";
                    break;
                case "/":
                    _currentCursor = "-";
                    break;
                default:
                    _currentCursor = "-";
                    break;
            }
            System.Console.Write("\r" + _currentCursor);
        }
    }
}
