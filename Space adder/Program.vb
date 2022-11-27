Imports System
Imports System.IO

Module Program
    Sub Main(args As String())

        'When it gets stuck it will output the last few words and a string of the next bit of text. Type the next 5 (ish) words and then input -1 to set it off again

        Dim Dict(20, 5000) As String
        Dim dictlength(20) As Integer
        Dim nextword As String
        Dim wordstack(5000) As String
        Dim wordstackpointer As Integer = 0
        Dim wordnum As Integer
        Dim found As Boolean
        Dim currentword As String = ""
        Dim compoundword As String
        Dim lastword As String
        Dim stepsback As Integer = 0
        Dim userchecktext As String = ""
        Dim userinput As String
        Dim spacetext As String = ""
        Dim starttext As String
        starttext = Console.ReadLine()

        Dim i As Integer = 0

        Dim filename As String = ("finaldict.txt")

        FileOpen(1, filename, OpenMode.Input)

        While Not EOF(1)
            nextword = LineInput(1)
            Dict(nextword.Length, dictlength(nextword.Length)) = nextword
            dictlength(nextword.Length) += 1
        End While
        FileClose(1)

        Do
            currentword += starttext.Substring(i, 1)          'Add a letter to current word
            wordnum = 0
            found = False

            Do                                                  'Check if its a word now
                If currentword = Dict(currentword.Length, wordnum) Then
                    wordstack(wordstackpointer) = currentword
                    wordstackpointer += 1
                    found = True
                    currentword = ""
                    stepsback = 0
                End If
                wordnum += 1
            Loop Until found = True Or wordnum >= dictlength(currentword.Length)

            If found = False And currentword.Length = 20 Then             'If its not and its 20 letters long
                i -= 20
                wordstackpointer -= 1
                stepsback += 1

                If wordstackpointer > -1 And stepsback < 5 Then              'And if it has backtracked 5 times
                    currentword = wordstack(wordstackpointer)
                Else                                                     'Ouput the thing for the user

                    If wordstackpointer > 2 Then
                        userchecktext += wordstack(wordstackpointer - 2)
                        userchecktext += " "
                        userchecktext += wordstack(wordstackpointer - 1)
                        userchecktext += " "
                        userchecktext += wordstack(wordstackpointer)
                        userchecktext += " "
                    End If

                    If i + 40 >= starttext.Length Then
                        userchecktext += starttext.Substring(i + 1, starttext.Length - i - 1)
                    Else
                        userchecktext += starttext.Substring(i + 1, 40)
                    End If

                    Console.WriteLine(userchecktext)

                    Do
                        userinput = Console.ReadLine()
                        If userinput <> "-1" Then
                            wordstackpointer += 1
                            wordstack(wordstackpointer) = userinput
                            i += userinput.Length
                            Console.WriteLine(i)
                        End If
                    Loop Until userinput = "-1"
                    userchecktext = ""
                    stepsback = 0
                    currentword = ""
                    wordstackpointer += 1
                End If
            End If

            i += 1
        Loop Until i = starttext.Length

        Dim j As Integer = 0

        Do                                                                         'Check to see if 2 words make a longer word (its more likely to be correct)
            compoundword = wordstack(j) + wordstack(j + 1)
            wordnum = 0
            found = False
            If compoundword.Length <= 20 Then
                Do
                    If compoundword = Dict(compoundword.Length, wordnum) Then
                        found = True
                    End If
                    wordnum += 1
                Loop Until found = True Or wordnum >= dictlength(compoundword.Length)
            End If

            If found = True Then
                spacetext += (compoundword + " ")
                j += 1
            Else                                                              'Else just add the word to the output string
                spacetext += (wordstack(j) + " ")
            End If
            j += 1
        Loop Until j >= wordstackpointer - 2

        lastword = wordstack(wordstackpointer - 1)
        lastword += currentword
        spacetext += lastword

        Console.WriteLine(spacetext)                                      'And output it
        Console.ReadLine()



    End Sub
End Module
