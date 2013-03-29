/*
 * Copyright (c) 2007 Jonathan Bradshaw
 * 
 * This software is provided 'as-is', without any express or implied warranty. 
 * In no event will the authors be held liable for any damages arising from the use of this software.
 * Permission is granted to anyone to use this software for any purpose, including commercial 
 * applications, and to alter it and redistribute it freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not claim that you wrote 
 *    the original software. If you use this software in a product, an acknowledgment in the 
 *    product documentation would be appreciated but is not required.
 * 2. Altered source versions must be plainly marked as such, and must not be misrepresented as
 *    being the original software.
 * 3. This notice may not be removed or altered from any source distribution.
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using VmcController.AddIn.Commands;
using Microsoft.MediaCenter;

namespace VmcController.AddIn
{
    public interface ICommand
    {
        string ShowSyntax();
        OpResult Execute(string param);
    }

    /// <summary>
    /// Manages the list of available remote commands
    /// </summary>
    public class RemoteCommands
    {
        private Dictionary<string, ICommand> m_commands = new Dictionary<string, ICommand>();

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteCommands"/> class.
        /// </summary>
        public RemoteCommands()
        {
            m_commands.Add("=== Input Commands: ==========", null);
            m_commands.Add("button-rec", new SendKeyCmd('R', false, true, false));
            m_commands.Add("button-left", new SendKeyCmd(0x25));
            m_commands.Add("button-up", new SendKeyCmd(0x26));
            m_commands.Add("button-right", new SendKeyCmd(0x27));
            m_commands.Add("button-down", new SendKeyCmd(0x28));
            m_commands.Add("button-ok", new SendKeyCmd(0x0d));
            m_commands.Add("button-back", new SendKeyCmd(0x08));
            m_commands.Add("button-info", new SendKeyCmd('D', false, true, false));
            m_commands.Add("button-ch-plus", new SendKeyCmd(0xbb, false, true, false));
            m_commands.Add("button-ch-minus", new SendKeyCmd(0xbd, false, true, false));
            m_commands.Add("button-dvdmenu", new SendKeyCmd('M', true, true, false));
            m_commands.Add("button-dvdaudio", new SendKeyCmd('A', true, true, false));
            m_commands.Add("button-dvdsubtitle", new SendKeyCmd('U', true, true, false));
            m_commands.Add("button-cc", new SendKeyCmd('C', true, true, false));
            m_commands.Add("button-pause", new SendKeyCmd('P', false, true, false));
            m_commands.Add("button-play", new SendKeyCmd('P', true, true, false));
            m_commands.Add("button-stop", new SendKeyCmd('S', true, true, false));
            m_commands.Add("button-skipback", new SendKeyCmd('B', false, true, false));
            m_commands.Add("button-skipfwd", new SendKeyCmd('F', false, true, false));
            m_commands.Add("button-rew", new SendKeyCmd('B', true, true, false));
            m_commands.Add("button-fwd", new SendKeyCmd('F', true, true, false));
            m_commands.Add("button-zoom", new SendKeyCmd('Z', true, true, false));
            m_commands.Add("button-num-0", new SendKeyCmd(0x60));
            m_commands.Add("button-num-1", new SendKeyCmd(0x61));
            m_commands.Add("button-num-2", new SendKeyCmd(0x62));
            m_commands.Add("button-num-3", new SendKeyCmd(0x63));
            m_commands.Add("button-num-4", new SendKeyCmd(0x64));
            m_commands.Add("button-num-5", new SendKeyCmd(0x65));
            m_commands.Add("button-num-6", new SendKeyCmd(0x66));
            m_commands.Add("button-num-7", new SendKeyCmd(0x67));
            m_commands.Add("button-num-8", new SendKeyCmd(0x68));
            m_commands.Add("button-num-9", new SendKeyCmd(0x69));
            m_commands.Add("button-num-star", new SendKeyCmd('3', true, false, false));
            m_commands.Add("button-num-number", new SendKeyCmd('8', true, false, false));
            m_commands.Add("button-clear", new SendKeyCmd(0x1b));
            m_commands.Add("type", new SendStringCmd());

            m_commands.Add("=== Misc Commands: ==========", null);
            m_commands.Add("dvdrom", new DvdRomCmd());
            m_commands.Add("msgbox", new MsgBoxCmd());
            m_commands.Add("msgboxrich", new MsgBoxRichCmd());
            m_commands.Add("notbox", new NotBoxCmd());
            m_commands.Add("notboxrich", new NotBoxRichCmd());
            m_commands.Add("goto", new NavigateToPage());
            m_commands.Add("announce", new AnnounceCmd());
            m_commands.Add("run-macro", new MacroCmd());
            m_commands.Add("suspend", new SuspendCmd());

            m_commands.Add("=== Media Experience Commands: ==========", null);
            m_commands.Add("fullscreen", new FullScreenCmd());
            m_commands.Add("mediametadata", new MediaMetaDataCmd());
            m_commands.Add("playrate", new PlayRateCmd(true));
            m_commands.Add("playrate-get", new PlayRateCmd(false));
            m_commands.Add("playstate-get", new PlayRateCmd(false, true));
            m_commands.Add("position", new PositionCmd(true));
            m_commands.Add("position-get", new PositionCmd(false));

            m_commands.Add("=== Environment Commands: ==========", null);
            m_commands.Add("version", new VersionInfoCmd());
            m_commands.Add("capabilities", new CapabilitiesCmd());
            m_commands.Add("changer-load", new ChangerCmd());

            m_commands.Add("=== Audio Mixer (Volume) Commands: ==========", null);
            m_commands.Add("volume", new Volume());

            m_commands.Add("=== Music Library Commands: ==========", null);
            m_commands.Add("music-list-artists", new musicCmd(musicCmd.LIST_ARTISTS));
            m_commands.Add("music-list-album-artists", new musicCmd(musicCmd.LIST_ALBUM_ARTISTS));
            m_commands.Add("music-list-albums", new musicCmd(musicCmd.LIST_ALBUMS));
            m_commands.Add("music-list-songs", new musicCmd(musicCmd.LIST_SONGS));
            m_commands.Add("music-list-details", new musicCmd(musicCmd.LIST_DETAILS));
            m_commands.Add("music-list-genres", new musicCmd(musicCmd.LIST_GENRES));
            m_commands.Add("music-list-recent", new musicCmd(musicCmd.LIST_RECENT));
            m_commands.Add("music-play", new musicCmd(musicCmd.PLAY));
            m_commands.Add("music-queue", new musicCmd(musicCmd.QUEUE));
            m_commands.Add("music-cover", new musicCmd(musicCmd.SERV_COVER));
            m_commands.Add("music-clear-cache", new musicCmd(musicCmd.CLEAR_CACHE));
            m_commands.Add("music-stats", new musicCmd(musicCmd.LIST_STATS));

            m_commands.Add("=== Audio Library Commands: (Deprecated - use Music Library Commands!) ==========", null);
            m_commands.Add("list-artists", new artistCmd(false));
            m_commands.Add("list-artist-songs", new artistCmd(false, false, false, artistCmd.show_artists + artistCmd.show_songs, artistCmd.by_artist));
            m_commands.Add("list-artist-albums", new artistCmd(false, false, false, artistCmd.show_artists + artistCmd.show_albums, artistCmd.by_artist));
            m_commands.Add("list-albums", new artistCmd(false, false, false, artistCmd.show_artists + artistCmd.show_albums));
            m_commands.Add("list-album-songs", new artistCmd(false, false, false, artistCmd.show_artists + artistCmd.show_albums + artistCmd.show_songs, artistCmd.by_album));
            m_commands.Add("list-all-custom", new artistCmd(false, false, false, 0, artistCmd.by_all));
            m_commands.Add("list-artist-custom", new artistCmd(false, false, false, 0, artistCmd.by_artist));
            m_commands.Add("list-album-custom", new artistCmd(false, false, false, 0, artistCmd.by_album));
            m_commands.Add("list-song-custom", new artistCmd(false, false, false, 0, artistCmd.by_track));
            m_commands.Add("play-audio-artist", new artistCmd(true, false, false, artistCmd.show_artists + artistCmd.show_songs, artistCmd.by_artist));
            m_commands.Add("play-audio-album", new artistCmd(true, false, false, artistCmd.show_artists + artistCmd.show_albums + artistCmd.show_songs, artistCmd.by_album));
            m_commands.Add("play-audio-song", new artistCmd(true, false, false, artistCmd.show_artists + artistCmd.show_albums + artistCmd.show_songs, artistCmd.by_track));
            m_commands.Add("queueaudio-artist", new artistCmd(true, true, false, artistCmd.show_artists + artistCmd.show_songs, artistCmd.by_artist));
            m_commands.Add("queueaudio-album", new artistCmd(true, true, false, artistCmd.show_artists + artistCmd.show_albums + artistCmd.show_songs, artistCmd.by_album));
            m_commands.Add("queueaudio-song", new artistCmd(true, true, false, artistCmd.show_artists + artistCmd.show_albums + artistCmd.show_songs, artistCmd.by_track));

            m_commands.Add("=== Photo Library Commands: ==========", null);
            m_commands.Add("photo-clear-cache", new photoCmd(photoCmd.CLEAR_CACHE));
            m_commands.Add("photo-list", new photoCmd(photoCmd.LIST_PHOTOS));
            m_commands.Add("photo-play", new photoCmd(photoCmd.PLAY_PHOTOS));
            m_commands.Add("photo-queue", new photoCmd(photoCmd.QUEUE_PHOTOS));
            m_commands.Add("photo-tag-list", new photoCmd(photoCmd.LIST_TAGS));
            m_commands.Add("photo-serv", new photoCmd(photoCmd.SERV_PHOTO));
            m_commands.Add("photo-stats", new photoCmd(photoCmd.SHOW_STATS));

            m_commands.Add("=== Play Commands: ==========", null);
            m_commands.Add("play-audio", new PlayMediaCmd(MediaType.Audio, false));
            m_commands.Add("queueaudio", new PlayMediaCmd(MediaType.Audio, true));
            m_commands.Add("play-video", new PlayMediaCmd(MediaType.Video, false));
            m_commands.Add("play-dvd", new PlayMediaCmd(MediaType.Dvd, false));
            m_commands.Add("play-radio", new PlayMediaCmd(MediaType.Radio, false));
            m_commands.Add("play-tv", new PlayMediaCmd(MediaType.TV, false));
            m_commands.Add("play-dvr", new PlayMediaCmd(MediaType.Dvr, false));

            m_commands.Add("=== Epg / Schedule Commands: ==========", null);
            //m_commands.Add("Epg disabled for this Win7 release", null);
            m_commands.Add("schedule", new ScheduleCmd());
            //m_commands.Add("epg-lineup", new EpgCmd(EpgResultsType.ChannelName));
            //m_commands.Add("epg-onnow", new EpgCmd(EpgResultsType.OnNow));
            //m_commands.Add("epg-details", new EpgCmd(EpgResultsType.Details));

            m_commands.Add("=== Window State Commands: ==========", null);
            m_commands.Add("window-close", new SysCommand(SysCommand.SC_CLOSE));
            m_commands.Add("window-minimize", new SysCommand(SysCommand.SC_MINIMIZE));
            m_commands.Add("window-maximize", new SysCommand(SysCommand.SC_MAXIMIZE));
            m_commands.Add("window-restore", new SysCommand(SysCommand.SC_RESTORE));

            m_commands.Add("=== Reporting Commands: ==========", null);
            m_commands.Add("format", new customCmd());

            m_commands.Add("=== Check Commands: ==========", null);
            m_commands.Add("nit", new NITInfoCmd());
        }

        /// <summary>
        /// Returns a multi-line list of commands and syntax
        /// </summary>
        /// <returns>string</returns>
        public OpResult CommandList()
        {
            return CommandList(0);
        }
        public OpResult CommandList(int port)
        {
            OpResult opResult = new OpResult(OpStatusCode.Ok);
            if (port != 0)
            {
                opResult.AppendFormat("=== Ports: ==========");
                opResult.AppendFormat("TCP/IP Socket port: {0}", port);
                opResult.AppendFormat("HTTP Server port: {0} (http://your_server:{1}/)", (port + 10), (port + 10));
            }
            opResult.AppendFormat("=== Connection Commands: ==========");
            opResult.AppendFormat("help - Shows this page");
            opResult.AppendFormat("exit - Closes the socket connection");
            foreach (KeyValuePair<string, ICommand> cmd in m_commands)
            {
                opResult.AppendFormat("{0} {1}", cmd.Key, (cmd.Value == null) ? "" : cmd.Value.ShowSyntax());
            }
            return opResult;
        }
        public OpResult CommandListHTML(int port)
        {
            OpResult opResult = new OpResult(OpStatusCode.Ok);

            string page_start =
                "<html><head><script LANGUAGE='JavaScript'>\r\n" +
                "function toggle (o){ var all=o.childNodes;  if (all[0].childNodes[0].innerText == '+') open(o,true);  else open(o,false);}\r\n" +
                "function open (o, b) { var all=o.childNodes;  if (b) {all[0].childNodes[0].innerText='-';all[1].style.display='inline';}  else {all[0].childNodes[0].innerText='+';all[1].style.display='none';}}\r\n" +
                "function toggleAll (b){ var all=document.childNodes[0].childNodes[1].childNodes; for (var i=0; i<all.length; i++) {if(all[i].id=='section') open(all[i],b)};}\r\n" +
                "</script></head>\r\n" +
                "<body><a id='top'>Jump to</a>: <a href='#commands'>Command List</a>, <a href='#examples'>Notes and Examples</a>, <a href='#bottom'>Bottom</a><hr><font size=+3><a id='commands'>Command List</a>:&nbsp;&nbsp;</font>[<a onclick='toggleAll(true);' >Open All</a>] | [<a onclick='toggleAll(false);' >Collapse All</a>]<hr>\r\n";
            string page_end =
                "</pre></span></div>\r\n" +
                "<br><hr><b><a id='examples'>Note: URLs must be correctly encoded</a></b><hr><br>\r\n" +
                "<b>Note - The following custom examples require that:</b><br>\r\n" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1 - Custom formats artist_browse and artist_list are defined in the &quot;music.template&quot; file<br>\r\n" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2 - The &quot;music.template&quot; file has been copied to the ehome directory (usually C:\\Windows\\ehome)<br>\r\n" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3 - Vista Media Center has been restarted after #1 and #2<br>\r\n" +
                "<br><b>Working track browser using custom formats: (can be slow... but this works as an album browser)</b><br>\r\n" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Display complete artist list linked to albums: <a href='music-list-details%20template:artist_list'>http://hostname:40510/music-list-details%20template:artist_list</a><br>\r\n" +
                "<br><b>Examples using artist filter: (warning can be very slow with large libraries)</b><br>\r\n" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;All artists: <a href='music-list-artists'>http://hostname:40510/music-list-artists</a><br>\r\n" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;All albums: <a href='music-list-albums'>http://hostname:40510/music-list-albums</a><br>\r\n" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;All albums by artists starting with the letter &quot;A&quot;: <a href='music-list-albums%20artist:a'>http://hostname:40510/music-list-albums%20artist:a</a><br>\r\n" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Play the tenth and thirteenth song in your collection: <a href='music-play%20indexes:10,13'>http://hostname:40510/music-play%20indexes:10,13</a><br>\r\n" +
                "<br><b>Examples using custom formats and artist match: (can be slow...)</b><br>\r\n" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Display pretty albums and tracks by the first artist starting with &quot;Jack&quot;: <a href='music-list-details%20template:artist_browse%20artist:jack'>http://hostname:40510/music-list-details%20template:artist_browse%20artist:jack</a><br>\r\n" +
                "<br><b>More help:</b><br>\r\n" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Help on the music commands: <a href='music-list-artists%20-help'>http://hostname:40510/music-list-artists%20-help</a><br>\r\n" +
                "<br><hr>\r\n" +
                "<a id='bottom'>Generated by</a>: Vista Media Center TCP/IP Controller (<a href='http://www.codeplex.com/VmcController'>vmcController Home</a>)\r\n" +
                "<hr>Jump to: <a href='#top'>Top</a>, <a href='#commands'>Command List</a>, <a href='#examples'>Notes and Examples</a><br>\r\n" +
                "<script LANGUAGE='JavaScript'>toggleAll(false);</script></body></html>\r\n";

            string header_start = "</pre></span></div><br><div id='section' onclick='toggle(this)' style='border:solid 1px black;'><font size=+1 style='font:15pt courier;'><span>+</span>";
            string header_end = "</font><span style='display:'><pre>";

            opResult.AppendFormat("{0}", page_start);

            if (port != 0)
            {
                opResult.AppendFormat("{0} Ports: {1}", header_start, header_end);
                opResult.AppendFormat("TCP/IP Socket port: {0}", port);
                opResult.AppendFormat("HTTP Server port: {0} (http://your_server:{1}/)", (port + 10), (port + 10));
            }
            opResult.AppendFormat("{0} Connection Commands: {1}", header_start, header_end);
            opResult.AppendFormat("<a href='/help'>help</a> - Shows this page");
            opResult.AppendFormat("<a href='/help'>exit</a> - Closes the socket connection");
            foreach (KeyValuePair<string, ICommand> cmd in m_commands)
            {
                if (cmd.Key.StartsWith("==="))
                    opResult.AppendFormat(cmd.Key.Replace("==========", header_end).Replace("===", header_start));
                else
                    opResult.AppendFormat("<a href='/{0}'>{1}</a> {2}",
                        cmd.Key, cmd.Key, (cmd.Value == null) ? "" : cmd.Value.ShowSyntax().Replace("<", "&lt;").Replace(">", "&gt;"));
            }
            opResult.AppendFormat("{0}", page_end);

            return opResult;
        }

        /// <summary>
        /// Executes a command with the given parameter string and returns a string return
        /// </summary>
        /// <param name="command">command name string</param>
        /// <param name="param">parameter string</param>
        /// <param name="result">string</param>
        /// <returns></returns>
        public OpResult Execute(String command, string param)
        {
            command = command.ToLower();
            if (m_commands.ContainsKey(command))
            {
                try
                {
                    return m_commands[command].Execute(param);
                }
                catch (Exception ex)
                {
                    OpResult opResult = new OpResult();
                    opResult.StatusCode = OpStatusCode.Exception;
                    opResult.StatusText = ex.Message;
                    opResult.AppendFormat(ex.Message);
                    return opResult;
                }
            }
            else
            {
                return new OpResult(OpStatusCode.BadRequest);
            }
        }
    }
}
