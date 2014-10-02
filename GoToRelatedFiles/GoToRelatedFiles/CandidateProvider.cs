using System.Collections.Generic;
using GoToRelatedFiles;

public static class CandidateProvider
{
    public static IEnumerable<string> GetCandidates(IEnumerable<string> typeNamesInFile, IEnumerable<string> postfixs)
    {
        var candidates = new List<string>();

        foreach (var fileName in typeNamesInFile)
        {
            var strippedFileName = StripFileName(fileName, postfixs);

            BuildCandidatesList(postfixs, candidates, strippedFileName);
        }

        return candidates;
    }

    private static string StripFileName(string fileName, IEnumerable<string> postfixs)
    {
        string strippedFileName = fileName;

        foreach (var postfix in postfixs)
        {
            if (fileName.Contains(postfix))
            {
                return fileName.Substring(0, fileName.IndexOf(postfix));
            }
        }

        return fileName;
    }

    private static void BuildCandidatesList(IEnumerable<string> postfixs, List<string> candidates, string strippedFileName)
    {
        candidates.Add(strippedFileName);

        foreach (var postfix in postfixs)
        {
            candidates.Add(strippedFileName + postfix);
        }
    }
}