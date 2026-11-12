INCLUDE DialogueTestFuncs.ink

#speaker: Journalist #portrait: shadow
~FadeInCharacter(1)
->intro
=== intro=== 
Hello there! I’m looking for a very prestigious establishment that sells quite uncommon wares. Any clue where I might be able to find such a place? #speaker: Journalist #portrait: shadow

* Well, this place here definitely fits that description. #speaker: Player
    ->intro_question
    
* What kind of wares? #speaker: Player
    ->intro_response
    
* I can’t help you. #speaker: Player
    ->intro_question

=== intro_response === 
Just a little something to spruce up my news room. #speaker: Journalist
->intro

=== intro_question === 
Well you can certainly help me then! You see I'm looking for someone who is supposedly selling real, LIVE, ACTUAL...plants. Not these cheap fake ones you've got here. Can you imagine the kind of buzz a story like that would get? I just need some proof. #speaker: Journalist #portrait: normal

* Who are you exactly? #speaker: Player
    ->background

* I might have something you could use (sell plant) #speaker: Player
    ~sell_plant()
    ->proof

* I might have something, but would you mind if I asked you a few questions real quick? (keep questioning) #speaker: Player
    ->questions

* I don’t think I have anything like that. (refuse service) #speaker: Player
    ~refuse_service()
    ->ending

=== proof === 
Excellent! This is just the kind of thing I was looking for. I'll see you around sometime soon. I'm sure I'll need more proof at some point if I'm going to get people to believe me.
->ending

=== background === 
I'm a journalist! Well, as much of a journalist as you can be in this city. There isn't exactly any sort of news stations or anything I work for. Most of the things I report on I can't even really share without getting censored or worse. Still, I enjoy it enough to keep doing it. #speaker: Journalist #portrait: normal

* I see #speaker: Player
    -> intro_question
    
==request==
* sell plant
->sell
* keep questioning
->questions
* refuse service
->refuse    
    
=== refuse === 
    ~refuse_service()
    -> ending
=== sell === 
    ~sell_plant()
    -> ending
    
    ==questions==
* What do you do for a living?
->job

* How old are you?
->age

* What do you think of the family that runs the city?
->politics

+ I'm done asking questions
->request

==job==
...Seriously? You might not be a detective or anything, but you can't take a wild guess? 
->questions

==age==
I'm 36, now less talky more selly please and thank you.
->questions

==politics==
They are the absolute worst, but it'll make for a good story when someone finally pushes them out so I'm not gonna complain too much I suppose.
->questions

=== ending == 
- ~FadeOutCharacter(1)
~Wait(0.4)
-> END