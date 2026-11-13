INCLUDE DialogueTestFuncs.ink

#speaker: Journalist #portrait: shadow
~FadeInCharacter(1)
->intro
=== intro=== 
Hello there! #speaker: Journalist #portrait: shadow

I’m looking for a very prestigious establishment that sells quite uncommon wares. Any clue where I might be able to find such a place?
->intro_choice

=== intro_choice ===
* [Well, this place here definitely fits that description.]
    Well you can certainly help me then! #speaker: Journalist #portrait: normal
    ->intro_question
    
* [What kind of wares?]
    ->intro_response
    
* [Sorry, I can’t help you.]
    Really? Can't even poke your brain a little? #speaker: Journalist #portrait: normal
    ->intro_question

=== intro_response === 
Just a little something to spruce up my news room. Know any place like that? #speaker: Journalist
->intro_choice

=== intro_question === 
 You see I'm looking for someone who is supposedly selling real, LIVE, ACTUAL... PLANTS!!! Not these cheap fake ones you've got here. #speaker: Journalist
 
 Can you imagine the kind of buzz a story like that would get? I just need some proof.
-> intro_question_choice

=== intro_question_choice ===
* [Who are you exactly?] #speaker: Player
    ->background

* [(Sell Plant)]
    ->sell

* [(Ask Questions)] I might have something, but would you mind if I asked you a few questions real quick? #speaker: Player
    ->questions

* [(Turn Them Away)]
    ->refuse

=== proof === 
Excellent! This is just the kind of thing I was looking for.

I'll see you around sometime soon. I'm sure I'll need more proof at some point if I'm going to get people to believe me.
->ending

=== background === 
I'm a journalist! #speaker: Journalist

Well, as much of a journalist as you can be in this city. There isn't exactly any sort of news stations or anything I work for.

Most of the things I report on I can't even really share without getting censored or worse. Still, I enjoy it enough to keep doing it.

So, ya got anything for me?
-> intro_question_choice
    
==request==
+ [(Sell Plant)]
->sell
+ [(Keep Questioning)]
->questions
+ [(Turn Them Away)]
->refuse    
    
=== refuse === 
    Hmm, I don’t think I can help you. Maybe go somewhere else? #speaker: Player
    ~refuse_service()
    -> ending
=== sell === 
     I might have something you could use. #speaker: Player
    ~sell_plant()
    -> proof
    
    ==questions==
* [What do you do for a living?]
->job

* [How old are you?]
->age

* [What do you think of the family that runs the city?]
->politics

+ [I'm done asking questions.]
->request

==job==
...Seriously? You might not be a detective or anything, but you can't take a wild guess? 
->questions

==age==
I'm 36, now less talky more showy please and thank you.
->questions

==politics==
They are the absolute worst, but it'll make for a good story when someone finally pushes them out so I'm not gonna complain too much I suppose.
->questions

=== ending == 
- ~FadeOutCharacter(1)
~Wait(0.4)
-> END