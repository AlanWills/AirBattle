
# AirBattle

## Decisions

Most of the decisions I made for this game came from thinking about testing, possible extensions in the future and the capability for designers to tweak parameters.  One natural extension would be to add another player, so doing something like a singleton for the Player was off the table.  

Just generally with Unity, the more I've found you can make configurable in the editor through a UI (and out of code), the better.  This is why keys for firing/modifying the gun angle (for example) *can* be changed, even though the design spec had them as fixed keys.

Some parameters of the level, like the round timer, were not specified so I just used a sensible number configurable in the UI.

I did find the final playmode test requirement for checking the round ends to be a little vague.  I wasn't sure what requirements had to be fulfilled when the round ends, so I just did a very basic test to check my round ended event was fired.  This could be fleshed out to include other tests like checking game objects are disabled or input is disabled, but as a smoke check it was good enough.


## Time Taken

8 hours spread over two evenings.


## 3rd Party Assets

None, but I did grab a couple of scripts from my own store of generic useful Unity bits and bobs.  All scripts in the `Parameters` and `Events` folders I did not write during this test, but did modify to fit with the project (create asset menu paths + namespaces).