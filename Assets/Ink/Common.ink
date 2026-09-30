//External Functions
EXTERNAL DebugLog(message)
EXTERNAL evnt_Wait(time)

//Fallback Functions
//When played in-engine, the external functions are used instead.
//These are only used when previewing the script in the Inky editor, and
//they describe what would be happening in-engine.
=== function DebugLog(message) ===
#DEBUG: {message}

=== function evnt_Wait(time) ===
#NEXT_EVENT: Wait for {time} seconds

//Event wrappers
//Calls external event functions while signaling to DialogueManager
//that these are events, so they can be handled correctly.
=== function SignalEvent() ===
!!EVENT!!

=== function Wait(time) ===
~ evnt_Wait(time)
~ SignalEvent()