INCLUDE DialogueTestFuncs.ink

#speaker: Prepper #portrait: shadow
~FadeInCharacter(1)
Pssssst. Hey ....hey! #speaker: Prepper #portrait: shadow
*   Hey there! # speaker: Player
        -> intro_question 
*   What did you say?  # speaker: Player 
        -> intro_question 
*   Can I help you? # speaker: Player 
        -> intro_question 

=== intro_question === 
I heard you're the type of person that can help people "find" something that isn't easy to come across. #speaker: Prepper #portrait: normal
*   Well, what were you hoping to "find"? # speaker: Player 
        -> sustainable
*   I might be able to... I also might not. Any reasons I have to even trust you with whatever I were to "find" for you? # speaker: Player
        -> bunker
*   Well, I'm not. I don't know where you got the idea that I was that kind of person, but it's not true. # speaker: Player
        -> not_trying

=== sustainable ===
I need something easy to maintain. Preferably something that doesn't require too much light to work. #speaker: Prepper # portrait: normal
*   I think I can find something that would work for you # speaker: Player
        -> sell
*   I might have something, but let me just ask you a question or two to see if it would be a good fit  # speaker: Player 
        -> questions
*   I'm sorry, I think you're at the wrong place. I don't really sell anything like that. # speaker: Player 
        -> refuse

=== not_trying ===
Look, I'm not trying to start anything, I just want to get the thing I need and get out of here. # speaker: Prepper # portrait: normal
*   Alright, I've got something right here. But you better make yourself scarce. # speaker: Player 
        -> sell
*   Well I don't know if I can trust you. Let me ask you a few things and we'll see if I'm the person you're looking for. # speaker: Player
        -> questions
*   I'm not that sort of person. Please leave. # speaker: Player 
        -> refuse



=== keep_questioning ===
    -> intro_question

=== bunker === 
I don't typically leave my bunker, so I would like to get what I came for and get back there as soon as possible. # speaker: Prepper # portrait: normal 
*   Let's make this quick then # speaker: Player 
        -> sell
*   Before we wrap this up I'd like to ask a few things # speaker: Player 
        -> questions
*   Well it looks like you'll have to head back empty-handed, I don't have anything for you # speaker: Player 
        -> refuse


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
I don't have a job, I try to stay out of the city as much as possible. Most days I just spend in my bunker.
->questions

==age==
I'm 25
->questions

==politics==
I don't trusts them at all. There is something up with them, so the less time I'm here the better.
->questions
    

=== ending == 
- ~FadeOutCharacter(1)
~Wait(0.4)
-> END