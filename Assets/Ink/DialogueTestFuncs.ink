INCLUDE Common.ink

EXTERNAL SetCharacterAlpha(alpha)
EXTERNAL PlaySFX(sfxID)
EXTERNAL evnt_FadeCharacter(startAlpha, endAlpha, time)
EXTERNAL evnt_CopArrest()
EXTERNAL evnt_sell_plant()
EXTERNAL refuse_service()

//Fallback+Wrapper functions (See Common.ink for details)
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

=== function evnt_sell_plant() === 
#NEXT_EVENT: Player Sells plant 

=== function sell_plant() ===
~evnt_sell_plant()
~ SignalEvent()

=== function refuse_service() ===
#(Refuse Service)
//this function just records the action to the game manager and
//doesn't affect the conversation directly

=== function PlaySFX(sfxID) ===
#Play SFX: {sfxID}

//Helper functions
=== function FadeInCharacter(time) ===
~ PlaySFX("SFX_walkUp")
~ FadeCharacter(0, 1, time)

=== function FadeOutCharacter(time) ===
~ PlaySFX("SFX_walkAway")
~ FadeCharacter(1, 0, time)
