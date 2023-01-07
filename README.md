
# ZwiftWorld
Zwift changes the available worlds on a schedule so not all worlds are availabe at any time. Only the default world is always enabled.

If you have a favorite world other than the default world that you like to ride frequently you can't because you'd have to wait until the desired world is on schedule again. This app does not change Zwifts schedule but it lets you change the default world to any of the Zwift worlds.

### How to use
- Make sure zwift is not running.
- Run ZwiftWorld.exe. It will display the list of worlds together with their id number.
- Type the id number of the desired world and press enter.


### How it works
Zwift uses a xml file with preferences which is located at `C:\Users\%Username%\Documents\Zwift`, the file is called `prefs.xml`.
You could set the default world manually by adding `<WORLD>Number of the world</WORLD>` right below `<ZWIFT>` like this:
```
<ZWIFT>
	<WORLD>5</WORLD>
	... other data ...
</ZWIFT>
```
but using this little app makes it easier. Enjoy!