INCLUDE DialogueTestFuncs.ink
#speaker: Kid # portrait: shadow
~FadeInCharacter(1)
->introduction

==introduction==
Yoooo, do you sell plants here? #speaker: Kid # portrait: shadow

* [You mind being quiet?]
->confused

* [What are you doing!?]
->confused

* [Huh?]
->confused

==confused==
What's wrong? #speaker: Kid #portrait: shadow

* [That's not something you just ask around here.]
->apologies

* [You need to be more discreet about that kind of stuff.]
->apologies

==apologies==
Oh, I'm sorry.  #speaker: Kid #portrait: normal

* [It's okay, what do you want?]
->request

* [What's your deal anyways? (Ask questions)]
->questions

* [(Turn Them Away)] I'm not comfortable having someone like you here, can you please leave? #speaker: Player
->ending

==request==
I just wanted some flowers for my girlfriend if you have anything like that. #speaker: Kid

* [(Sell Plant)] I've got something nice and special for you then. #speaker: Player
~sell_plant()
->ending

* [(Sell Fake Plant)] I've got this. #speaker: Player

(DEV NOTE: Fake Plants not implemented in current build.)
->ending

* [(Refuse Service)] I don't have anything for you. #speaker: Player
~refuse_service()
->ending

==questions==
* [What do you do for a living?]
->job

* [How old are you?]
->age

* [What do you think of the family that runs the city?]
->politics

+ [I'm done asking questions]
->request

==job==
My parents are getting pretty old, so I just help them out where ever I can. I don't really have a job though. #speaker: Kid
->questions

==age==
I'm 20. I was born in my parents bunker around the time this city started really picking up. #speaker: Kid
->questions

==politics==
I don't know? I don't really have an opinions on that kinda stuff I guess. They seem alright. #speaker: Kid
->questions


==ending==
- ~FadeOutCharacter(1)
~Wait(0.4)
->END