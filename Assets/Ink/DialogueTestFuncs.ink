INCLUDE Common.ink

EXTERNAL SetCharacterAlpha(alpha)
EXTERNAL evnt_FadeCharacter(startAlpha, endAlpha, time)
EXTERNAL evnt_CopArrest()

//Fallback+Wrapper functions
=== function SetCharacterAlpha(alpha) ===
#Set character alpha to {alpha}

=== function evnt_FadeCharacter(startAlpha, endAlpha, time) ===
~temp in_out = "out"
{startAlpha < endAlpha: 
    ~in_out = "in"
}
#NEXT_EVENT: Character fades {in_out} for {time} seconds

=== function FadeCharacter(startAlpha, endAlpha, time) ===
~ evnt_FadeCharacter(startAlpha, endAlpha, time)
~ SignalEvent()

=== function evnt_CopArrest() ===
#NEXT_EVENT: Player is arrested

=== function CopArrest() ===
~ evnt_CopArrest()
~ SignalEvent()

//Helper functions
=== function FadeInCharacter(time) ===
~ FadeCharacter(0, 1, time)

=== function FadeOutCharacter(time) ===
~ FadeCharacter(1, 0, time)