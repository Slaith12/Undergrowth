INCLUDE DialogueTestFuncs.ink
#speaker: Kid # portrait: shadow
~FadeInCharacter(1)

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
->ending

* I've got this (sell fake plant)
->ending

* I don't have anything for you
->ending

==questions==
->ending

==ending==
->END