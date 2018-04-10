﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotUpdater
{
    class UpdateBot
    {
        public async Task MainAsync()
        {
            await Info.LoadInfo();
            Console.WriteLine("Checking for updates...");
            await CheckUpdates.CheckVersion();
            string reply = string.Empty;
            if(Info.gitVersion.Equals(string.Empty) || Info.downloadURL.Equals(string.Empty))
            {
                Console.WriteLine("Update failed.");
                return;
            }
            if(!Info.gitVersion.Equals(Info.GetValue("version")))
            {
                while(true)
                {
                    Console.WriteLine("Update is avaible, do you wish to download it now? (y/n)");
                    reply = Console.ReadLine();
                    if(reply.Equals("y") || reply.Equals("n"))
                        break;
                }
                if(reply.Equals("y"))
                {
                    await Downloader.DownloadUpdate();
                    await Updater.UpdateFiles();
                }
                else
                    return;
            }
            else
                Console.WriteLine("Bot is up to date.");
            await Task.CompletedTask;
        }
    }
}