INCLUDE DialogueTestFuncs.ink


~FadeInCharacter(1)
Pssssst. Hey ....hey! #speaker: Prepper #portrait: shadow
*   [Hey there!] 
        # speaker: Player 
        -> intro_question 
*   [What did you say?] 
        # speaker: Player 
        -> intro_question 
*   [Can I help you?] 
        # speaker: Player 
        -> intro_question 

=== intro_question === 
I heard you're the type of person that can help people "find" something that isn't easy to come across. #speaker: Prepper #portrait: shadow
*   [Well, what were you hoping to "find"?] 
        # speaker: Player 
        -> sustainable
*   [I might be able to...I also might not. Any reasons I have to even trust you with whatever I were to "find" for you?]
        # speaker: Player
        -> bunker
*   [I don't know where you got the idea that I was that kind of person, but it's not true.]
        # speaker: Player
        -> not_trying

=== sustainable ===
I need something and easy to maintain. Preferably something that doesn't require too much light to work. #speaker: Prepper # portrait: shadow
*   [I think I can find something that would work for you] 
        # speaker: Player
        -> sell
*   [I might have something, but let me just ask you a question or two to see if it would be a good fit]
        # speaker: Player 
        -> keep_questioning
*   [I'm sorry, I think you're at the wrong place. I don't really sell anything like that.]
        # speaker: Player 
        -> refuse

=== not_trying ===
Look, I'm not trying to start anything, I just want to get the thing I need and get out of here. # speaker: Prepper # portrait: shadow
*   [Alright, I've got something right here. But you better make yourself scarce.] 
        # speaker: Player 
        -> sell
*   [Well I don't know if I can trust you. Let me ask you a fiew things and we'll see if I'm the person you're looking for.]
        # speaker: Player
        -> keep_questioning
*   [I'm not that sort of person. Please leave.] 
        # speaker: Player 
        -> refuse



=== keep_questioning ===
    -> intro_question

=== bunker === 
I don't typically leave my bunker, so I would like to get what I came and get bach there as soon as possible. # speaker: Prepper # portrait: shadow 
*   [Let's make this quick then]
        # speaker: Player 
        -> sell
*   [Before we wrap this up I'd like to ask a few things]
        # speaker: Player 
        -> keep_questioning
*   [Well it looks like you'll have to head back empty-handed, I don't have anything for you] 
        # speaker: Player 
        -> refuse
=== refuse === 
    ~refuse_service()
    -> ending
=== sell === 
    ~sell_plant()
    -> ending

=== ending == 
- ~FadeOutCharacter(1)
~Wait(0.4)
-> END