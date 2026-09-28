INCLUDE Common.ink

EXTERNAL SetCharacterAlpha(alpha)
EXTERNAL evnt_FadeCharacter(startAlpha, endAlpha, time)

//Fallback+Wrapper functions
=== function SetCharacterAlpha(alpha) ===
#Set character alpha to {alpha}

=== function evnt_FadeCharacter(startAlpha, endAlpha, time) ===

#NEXT_EVENT: Character fades {startAlpha < endAlpha: in } for {time} seconds

=== function FadeCharacter(startAlpha, endAlpha, time) ===
~ evnt_FadeCharacter(startAlpha, endAlpha, time)
~ SignalEvent()

//Helper functions
=== function FadeInCharacter(time) ===
~ FadeCharacter(0, 1, time)

=== function FadeOutCharacter(time) ===
~ FadeCharacter(1, 0, time)