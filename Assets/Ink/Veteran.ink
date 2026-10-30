INCLUDE DialogueTestFuncs.ink

# speaker: Veteran # portrait: shadow
~FadeInCharacter(1)
Hey # speaker: Veteran # portrait: shadow
*   Hey! # speaker: Player
        -> introduction
*   Helloooo? # speaker: Player
        -> introduction
*   Um...hi... # speaker: Player
        -> introduction

=== introduction ===
I was told that you sell something here that I can't find anywhere else. # speaker: Veteran # portrait: normal
*   Depends [] on what you're looking for. # speaker: Player 
        -> corner
* You could say that # speaker: Player
        -> got_back
* [What?] I have no idea what you're talking about.
        -> looking_to_buy


=== corner === 
I’m just looking for something that I could keep in the corner, that might not be too hard to take care of for a fella like me. # speaker Veteran # portrait: normal
*   I've got just the thing # speaker: Player 
        -> sell
*   I might [] have something, but let me ask you a question or two # speaker: Player 
        -> keep_questioning
*   [No] I don't think I have anything like that. # speaker: Player 
        -> refuse


=== got_back ===
I know what you've got back there... Don't worry, I'm retired now. I'm just looking for something to keep me busy. That ease your mind at all? # speaker: Veteran # portrait: normal
*   I'll trust you []. Just keep this between us, 'kay? # speaker: Player
        -> sell
*   I might [] have something, but let me just ask you a question or two to see if I can actually trust you. # speaker: Player 
        -> keep_questioning
*   [I don't trust that] That doesn't really make you any more trustworthy, I'm gonna have to ask you to leave. # speaker: Player
        -> refuse

    

=== looking_to_buy ===
I’m just a customer looking to buy what you’ve got in stock. If you aren’t interested in selling, maybe don’t open a store. # speaker: Veteran # portrait: normal
*   I got it... [] you aren't exactly the most trustworthy looking guy. # speaker: Player
        -> sell
*   [It's my decision] I can sell to whomever I like to, and before I decide to sell to you, I'm gonna ask you a few questions. You can either answer them, or leave. # speaker: Player 
        -> keep_questioning
*   [Leave.] Well maybe this isn't what you thought it was. Leave before I call someone. # speaker: Player 
        ->refuse

=== refuse === 
    ~refuse_service()
    -> ending
=== sell === 
    ~sell_plant()
    -> ending
=== keep_questioning ===
    -> introduction
    
=== ending == 
- ~FadeOutCharacter(1)
~Wait(0.4)
    -> END
    