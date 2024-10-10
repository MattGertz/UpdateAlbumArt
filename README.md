Some media software (such as PowerAmp) requires album art to be baked into the actual media file, and not stored in a file in the same folder.  This is irritating when using one's phone to play music as the album covers simply don't show for certain songs (and doubly irritating 
in the car when you want to be able to quickly glance at the  screen to see what album is playing).  To address this, UpdateAlbumArt recursively checks a specified directory and its subdirectories to see if either folder.jpg or ZuneAlbumArt.jpg exists in it/them.  If an art file of those names exists, 
it checks any MP3 or WMA files to see if they have cover art embedded into the file.  If there is no embedded art, it embeds the art that's in the art file.  However, if the file already has **any** embedded art, it leaves it alone.

I won't be working on this anymore, so it's best to clone the code and keep your own copy.  It's easy to update the code to check for other art file names and to support other file types -- they're just hardcoded strings.  If I was less lazy, I'd add support for arbitrary string lists from the command line.  I'd probably also add some flags to control the verbosity of the output, and would rewrite it to 
flag media files that are missing both embedded art and an art file.  I'd also probably separate the exception handling to have error-specific messages as well.  But, like I say, I'm lazy, and I already "fixed" all of my media files with the current code, so...

This is a C# project (with solution file, so Visual Studio) that uses TagLib#.  To set this up, load the project, select it, open the NuGet package manager, and enter `Install-Package TagLibSharp -Version 2.3.0` (or whatever the current version is -- check elsewhere on GitHub for the latest). After that, 
you should be able to edit, build, debug, etc.

GitHub Copilot was used to make the code more elegant at times (the C#/Roslyn team has added a lot of useful functionality since I managed it back in the 2010's, and I was a bit out of shape code-wise).  The code ran successfully against my entire music collection 
(several hundred albums comprising over 10,000 songs, all nested in a Byzantine folder structure).  However, your mileage may vary; I won't accept blame for wiping out your album art, and I strongly suggest you debug through some test 
cases first before setting this code free on your music collection.  (Because you haven't bothered to back up your music collection to anything other than a constantly updating OneDrive-like cloud in the last three years, right?  Yeah, I thought as much....)

After running the code "for realz," I suggest re-scanning your entire music database in your media player to make sure that the art changes are reflected.  (But what do I know?  Maybe your player is awesome enough to deal with the changes.)

There's no license associated with this project -- the code is laughably small, basically sample-size, so licensing would be overkill.  Use however you like.

Good luck!
  --Matt--*
