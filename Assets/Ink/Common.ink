//External Functions
EXTERNAL DebugLog(message)
EXTERNAL evnt_Wait(time)

//Fallback Functions (for previews)
=== function DebugLog(message) ===
#DEBUG: {message}

=== function evnt_Wait(time) ===
#NEXT_EVENT: Wait for {time} seconds

//Event wrappers
=== function SignalEvent() ===
!!EVENT!!

=== function Wait(time) ===
~ evnt_Wait(time)
~ SignalEvent()