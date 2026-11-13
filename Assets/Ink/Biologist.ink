INCLUDE DialogueTestFuncs.ink
# speaker: Biologist # portrait: shadow
~FadeInCharacter(1)
Hellooooo - do you selllll, plants hereeee? # speaker: Biologist # portrait: normal
*   [Yessssss?]
    -> introduction
*   [Nooooooo?]
    -> introduction
*   [Who are you?]
    -> introduction

=== introduction === 
I heard that I could get plants here, which would be really great because I really need them! # speaker: Biologist # portrait: normal

So I really hope that you have some that you might be willing to part with.

*   [Woah...slow down. What specifically did you need?] # speaker: Player 
        -> really_need
*   [Where did you hear such a thing? ]
        -> rumors
*   [I'm sorry. I don't sell real plants, just fake ones.]
        -> are_you_sure

=== really_need === 
I’m researching common flowers from before the collapse to see how we can reintroduce them to our ecosystems. # speaker: Biologist # portrait: normal

If we do that, it could help support the return of pollinators and more types of plants!

*   [(Sell Plant)] I think I know what you're talking about. # speaker: Player
        ->sell
*   [(Ask Questions)] I might have one of those, but can I ask a few questions to make sure it's the right one? # speaker: Player 
        ->questions
*   [(Refuse Service)] I don't have anything like that. You should probably leave.
        -> refuse

=== rumors ===
Well it’s just, there are rumors floating around, and you know how people talk about that kind of stuff hehehe. # speaker: Biologist # portrait: normal 

So I’ve been asking around and this is like the 6th place I’ve been to today. I really, REALLY need this okay?

*   [(Sell Plant)] I can help you, just don't go around making a scene, alright? # speaker: Player
        -> sell
*   [(Ask Questions)] Would you mind if I ask you a few questions real quick? # speaker: Player
        -> questions
*   [(Refuse Service)] No offense, but even if I did have anything like that you seem like the last person that I would want to sell it to... # speaker: Player 
        ->refuse

=== are_you_sure === 
Oh. Well, are you sure? I could go back there and make sure all of them are fake. Maybe you have one or two back there that are real! # speaker: Biologist # portrait: normal 

*   [(Sell Plant)] I might have some real ones mixed in there. You just can’t tell anyone else about it, deal? # speaker: Player
        -> sell
*   [(Ask Questions)] Well. I don’t just let anyone back here. You could be trying to rob me for all I know! # speaker: Player
        Let me ask you a few things and we’ll see.
        -> questions
*   [(Refuse Service)] Nope! All fake plants here! # speaker: Player
        If you aren’t interested in buying them at all then I’m gonna ask you to leave.
        -> refuse

==request==
+ [(Sell Plant)]
->sell
+ [(Keep Questioning)]
->questions
+ [(Refuse Service)]
->refuse    
    
=== refuse === 
    ~refuse_service()
    -> ending
=== sell === 
    ~sell_plant()
    -> ending
    
    ==questions==
* [What do you do for a living?]
->job

* [How old are you?]
->age

* [What do you think of the family that runs the city?]
->politics

* [What kind of plants are you looking for?]
->plants

+ [I'm done asking questions.]
->request

==job==
The bunker I grew up in was filled with all kinds of books on life before things went the way they did. I've started using them to do some research on how plants and animals are now. # speaker: Biologist

I'm hoping that I can bring things back to how they used to be. My Grandma has always loved some of the pictures in our books.
->questions

==age==
I'm 28, guess I'm starting to get kinda old huh? # speaker: Biologist
->questions

==politics==
I don't like some of their rules, it makes my research more difficult.  # speaker: Biologist
->questions

==plants==
Honestly, I will take anything. Finding living plants outside is next to impossible, so I'll need whatever I can get to jumpstart my research. # speaker: Biologist
->questions
    
=== ending == 
- ~FadeOutCharacter(1)
~Wait(0.4)
    -> END