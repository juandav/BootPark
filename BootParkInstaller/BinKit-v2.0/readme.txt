===Thanks for using BinKit 2.0===

This software is a combination of Command Promt/Bash Scripts...
a GPL FFmpeg binary executable and a FLVMeta binary executable to make easier 
to encode your FLV videos to work great on the 
Stream Video Player Plug-In for WordPress.

==How to use==

=Windows=
* Unzip all file contents to your PC.
* Depending on your video aspect ratio open the "4x3" folder for standard 4:3 
  video or the "16x9" for 16:9 widrescreen video.
* Drag your video file to the encoding preset script you want to use.
* Wait for the encoding to finish to start another encoding.

=Linux=
* Unzip all file contents to your PC.
* Depending on your video aspect ratio open the "4x3" folder for standard 4:3 
  video or the "16x9" for 16:9 widrescreen video.
* Open the "Terminal".
* Drag the preset ".sh" you want to use to the Terminal window.
* Drag your video file to the Terminal window.
* Go to the Terminal windows and press the [ENTER] key.
* Wait for the encoding to finish to start another encoding.

=Macintosh=
* Unzip all file contents to your PC.
* Depending on your video aspect ratio open the "4x3" folder for standard 4:3 
  video or the "16x9" for 16:9 widrescreen video.
* Open the "Terminal".
* Drag the preset ".sh" you want to use to the Terminal window.
* Drag your video file to the Terminal window.
* Go to the Terminal windows and press the [ENTER] key.
* Wait for the encoding to finish to start another encoding.


==What it does==
The BinKit 2.0 encodes any video to high quality H.264 video and AAC audio 
using FLV encapsulation, the FLV video file then is injected with the MetaData
needed in order to work with pseudo-streaming technique.

==How to customize==
Because the magic is on the Command Prompt/Bash scripts you can change some 
settings on any .bat or .sh file, the settings you can change are:

* Video Size.
* Display Aspect Ratio.
* Frame Rate.
* Key Frame Distance.
* Video Bitrate.
* Video Max Bitrate.
* Audio Channels.
* Audio Rate and Audio Bitrate.

Each setting is defined on an environment variable, for an example; on Windows
Batch Script you can change the video size from 432x240 to 640x480 modifying
the "SET SIZE=" variable value from:

SET SIZE=432x240

To:

SET SIZE=640x480

On Linux an Macintosh you can change the "SIZE=" variable value from:

SIZE=432x240

To:

SIZE=640x480

As you can see the modification and use of this Command Prompt/Bash scripts 
are very easy, if you need more help and information on the 
pseudo-streaming video encoding visit:

http://rodrigopolo.com/about/wp-stream-video/encoding-stream-video



Copyright 2010 by RodrigoPolo.com

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation; either version 2 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
