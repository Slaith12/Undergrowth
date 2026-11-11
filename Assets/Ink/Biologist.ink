INCLUDE DialogueTestFuncs.ink
# speaker: Biologist # portrait: shadow
~FadeInCharacter(1)
Hellooooo - do you selllll, plants hereeee? # speaker: Biologist # portrait: normal
*   Yessssss? # speaker: Player 
    -> introduction
*   Who are you? # speaker: Player 
    -> introduction
*   Nooooooo? # speaker: Player
    -> introduction

=== introduction === 
I heard that I could get plants here, which would be really great because I really need them so I really hope that you have some that you might be willing to part with. # speaker: Biologist # portrait: normal
*   Woah...slow down []. What specifically did you need? # speaker: Player 
        -> really_need
*   Where did you hear such a thing? 
        -> rumors
*   [I don't sell plants] I'm sorry. I don't sell real plants, just fake ones. 
        -> are_you_sure

=== really_need === 
I really need some Sphagneticola trilobata, I’m researching common flowers from before the collapse to see how we can reintroduce them to our ecosystems to support the return of pollinators and more types of plants! # speaker: Biologist # portrait: normal
*   [I think so] I think I know what one you're talking about. # speaker: Player
        ->sell
*   I might [] have it, but can I ask a few questions to make sure it's the right now? # speaker: Player 
        ->keep_questioning
*   I don't have [that] whatever that is. 
        -> refuse

=== rumors ===
Well it’s just, there are rumors floating around, and you know how people talk about that kind of stuff hehehe - so I’ve been asking around and this is like the 6th place I’ve been to today. I really, REALLY need this okay? # speaker: Biologist # portrait: normal 
*   I can help [] you, just don't go around making a scene, alright? # speaker: Player
        -> sell
*   [Ummmm, okay] Would you mind if I ask you a few questions real quick? # speaker: Player
        -> keep_questioning
*   [I wouldn't sell to you] No offense, but even if I did have anything like that you seem like the last person that I would want to sell it to... # speaker: Player 
        ->refuse

=== are_you_sure === 
Oh. Well, are you sure? I could go back there and make sure all of them are fake. Maybe you have one or two back there that are real! # speaker: Biologist # portrait: normal 
*   I might [] have some real ones mixed in there. You just can’t tell anyone else about it, deal? # speaker: Player
        -> sell
*   [I don't know you] Well. I don’t just let anyone back here. You could be trying to rob me for all I know! Let me ask you a few things and we’ll see. # speaker: Player
        -> keep_questioning
*   Nope! [] All fake plants here! If you aren’t interested in buying them at all then I’m gonna ask you to leave. # speaker: Player
        -> refuse

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