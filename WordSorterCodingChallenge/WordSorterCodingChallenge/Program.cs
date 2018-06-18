/***********************************************************************************************************
 *                                          God First                                                      *
 * Created by: Dustin Ledbetter                                                                            *
 * Dates: 6-[14-16]-2018                                                                                   *
 * Type: C#.NET console application                                                                        *
 * Purpose: Takes a paragraph of text and returns a list of alphabetized unique words.                     *
 * Followed by how many times the word appeared in the paragraph and the sentence(s) the word appeared in. *
 **********************************************************************************************************/
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace WordSorterCodingChallenge
    {
        class Program
        {

        /// <summary>
        /// Contains string that holds the paragraph to be sorted and two vars to hold Split paragraphs into sentences and words.
        /// Processing is done to make words alphabetized and unique. It is also used to show the count of occurrences and which sentences they appear in.
        /// Printing of results is done with formatting and checks to make sure things are displayed properly.
        /// </summary>
        static void Main()
            {

                // Contains string that holds the paragraph to be sorted
                string paragraph = "Nyrn and Tyene may have reached King’s Landing by now, she mused, as she settled down crosslegged by the mouth of the cave to watch the falling rain. " +
                "If not they ought to be there soon. Three hundred seasoned spears had gone with them, over the Boneway, past the ruins of Summerhall, and up the kingsroad. " +
                "If the Lannisters had tried to spring their little trap in the kingswood, Lady Nym would have seen that it ended in disaster. Nor would the murderers have found their prey." +
                " Prince Trystane had remained safely back at Sunspear, after a tearful parting from Princess Myrcella. That accounts for one brother, thought Arianne, but where is Quentyn," +
                " if not with the griffin? Had he wed his dragon queen? King Quentyn. It still sounded silly. This new Daenerys Targaryen was younger than Arianne by half a dozen years." +
                " What would a maid that age want with her dull, bookish brother? Young girls dreamed of dashing knights with wicked smiles, not solemn boys who always did their duty." +
                " She will want Dorne, though. If she hopes to sit the Iron Throne, she must have Sunspear. If Quentyn was the price for that, this dragon queen would pay it. " +
                "What if she was at Griffin’s End with Connington, and all this about another Targaryen was just some sort of subtle ruse? Her brother could well be with her. " +
                "King Quentyn. Will I need to kneel to him?";
           
                // Splits the paragraph into sentences when it finds a ., ?, or ! and stores it into an array of sentences
                var sentences = Regex.Split(paragraph, @"(?<=[.?!])");

                // Splits the paragraph into words when it finds any deliminator specified
                var sortedListOfWords = paragraph.Split(new[] { ' ', ',', ':', '.', '?', '!' }, StringSplitOptions.RemoveEmptyEntries)

                    // Converts all string array elements to lowercase
                    .Select(w => w.ToLowerInvariant())

                    // Sorts the words in ascending/alphabetical order
                    .OrderBy(w => w)

                    // Groups the same words for occurrence counting
                    .GroupBy(w => w)

                    // Sets keys and the count of occurrences of each word
                    .Select(grp => new
                    {

                        // Sets unique words with key for processing use
                        UniqueWord = grp.Key,

                        // Sets occurrence count for each word
                        OccurrenceCount = grp.Count(),

                        // Finds the sentences that contain the Unique word in them to process in foreach loop 
                        matches = from sentence in sentences
                                  where sentence.ToLower().Contains(grp.Key)
                                  select sentence

                    })

                    // Removes all duplicates
                    .Distinct();

                // Cycles through the sorted list of words and prints them out to the console 
                foreach (var word in sortedListOfWords)
                {

                    // Prints out each unique word with the occurrence count below it in alphabetical order to the console 
                    Console.WriteLine("Unique Word: {0}" + "\n" + "Count:{1}" + "\n" + "Sentence:", word.UniqueWord, word.OccurrenceCount);

                // Iterate through the sentences found that contain possible matches
                foreach (String line in word.matches)
                {

                    // Calls ToLower() to ensure our comparisons will work
                    var lowered = line.ToLower();

                    // Splits the paragraph into words when it finds any deliminator specified
                    var containsMatchSentence = lowered.Split(new[] { ' ', ',', ':', '.', '?', '!' }, StringSplitOptions.RemoveEmptyEntries);

                    // Checks the sentence match against the current unique word to see if it is an exact match or not  
                    var exact = containsMatchSentence.Contains(word.UniqueWord);

                    // If exact variable is true (meaning we have an exact match) print the sentence to console 
                    if (exact)
                    {

                        // Trims sentences of unneeded white spaces before printing sentences to the console
                        Console.WriteLine("{0}", line.Trim());

                    }

                }

                // Used to add space between the outputs
                Console.WriteLine("\n");

            }

                // Pauses at end til user presses a key signifying program is complete
                Console.WriteLine("\n" + "Press any key to continue...");
                Console.ReadKey();

            }


     
    }
} // WordSorterCodingChallenge
