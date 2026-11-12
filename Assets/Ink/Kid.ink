INCLUDE DialogueTestFuncs.ink
#speaker: Kid # portrait: shadow
~FadeInCharacter(1)
->introduction

==introduction==
Yoooo, do you sell plants here? #speaker: Kid # portrait: shadow

* You mind being quiet? #speaker: Player
->confused

* What are you doing!? #speaker: Player
->confused

* Will you shut up!? #speaker: Player
->confused

==confused==
What's wrong? #speaker: Kid #portrait: shadow

* You need to be more discreet about that kind of stuff.
->apologies

==apologies==
Oh, I'm sorry.

* It's okay, what do you want?
->request

* What's your deal anyways? (ask questions)
->questions

* I'm not comfortable having someone like you here, can you please leave?
->ending

==request==
I just wanted some flowers for my girlfriend if you have anything like that.

* I've got something nice and special for you then (sell plant)
~sell_plant()
->ending

* I've got this (sell fake plant)
->ending

* I don't have anything for you (refuse service)
~refuse_service()
->ending

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
My parents are getting pretty old, so I just help them out where ever I can. I don't really have a job though.
->questions

==age==
I'm 20, I was born in my parents bunker around the time this city started really picking up.
->questions

==politics==
I don't know? I don't really have an opinions on that kinda stuff I guess. They seem alright.
->questions


==ending==
- ~FadeOutCharacter(1)
~Wait(0.4)
->END